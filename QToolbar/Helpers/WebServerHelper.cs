using Microsoft.Web.Administration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebServerHelper
   {
      // QCRWebAPI_X_Y_Z
      private const string WEB_API_NAME_PATTERN = "QCRWebAPI[_][0-9]+[_][0-9]+[_]*[0-9]*";
      private const string IDENTITY_SERVER_PATTERN = "IdentityServer[_][0-9]+[_][0-9]+[_]*[0-9]*";
      private const string LEGAL_APP_PATTERN = "LegalApp[_][0-9]+[_][0-9]+[_]*[0-9]*";
      private const string WEB_OFFICER_CLIENT_PATTERN = "WebLegalOfficer[_][0-9]+[_][0-9]+[_]*[0-9]*[_]Client";


      private bool _CancelLoad;

      private int _HostsCount;
      private int _CurrentHostLoadedNumber;

      public List<WebSiteInfo> WebSites { get; internal set; }

      #region events
      public delegate void WebSiteInfoCollectedEventHandler(object sender, WebSiteInfoEventArgs args);
      public event WebSiteInfoCollectedEventHandler WebSiteInfoCollected;

      public delegate void ProcessingInfoCollectedEventHandler(object sender, ProcessingInfoCollectedEventArgs args);
      public event ProcessingInfoCollectedEventHandler ProcessingInfoCollected;
      #endregion

      

      public WebServerHelper()
      {
         WebSites = new List<WebSiteInfo>();
         _CancelLoad = false;
   }
      /// <summary>
      /// comma separated hosts list
      /// </summary>
      private string _HostsList;

      public bool ClearCache()
      {
         bool retVal = false;
         try
         {
            if (File.Exists(AppInstance.WebSitesCacheFile))
            {
               File.Delete(AppInstance.WebSitesCacheFile);
               retVal = true;
            }
         }
         catch(Exception ex)
         {
            retVal = false;
         }
         return retVal;
      }

      public void LoadInfo(string hostsList)
      {

         _CancelLoad = false;
         WebSites.Clear();
         _HostsList = hostsList;

         List<string> hosts = _HostsList.Split(',').ToList<string>();

         if (!ReadFromCache())
         {
            int i = 0;
            foreach (string host in hosts)
            {
               i++;
               _HostsCount = hosts.Count;
               _CurrentHostLoadedNumber = i;

               LoadInfoInternal(host);

               if (_CancelLoad) break;
               
            }
            if(!_CancelLoad)
            {
               WriteToCache();
            }
            
         }
      }

      public void CancelLoadInfo()
      {
         _CancelLoad = true;
      }

      
      private void OnWebSiteInfoCollected(WebSiteInfoEventArgs args)
      {
         if (WebSiteInfoCollected != null)
         {
            WebSiteInfoCollected(this, args);
         }
      }
      private void OnProcessingInfoCollected(ProcessingInfoCollectedEventArgs args)
      {
         if (ProcessingInfoCollected != null)
         {
            ProcessingInfoCollected(this, args);
         }
      }

      private void LoadInfoInternal(string host)
      {


         Regex webAPIReg = new Regex(WEB_API_NAME_PATTERN.ToLower());
         Regex identityReg = new Regex(IDENTITY_SERVER_PATTERN.ToLower());
         Regex legalAppReg = new Regex(LEGAL_APP_PATTERN.ToLower());
         Regex webOfficerClientReg = new Regex(WEB_OFFICER_CLIENT_PATTERN.ToLower());
         Regex allReg = new Regex($"{WEB_API_NAME_PATTERN.ToLower()}|{IDENTITY_SERVER_PATTERN.ToLower()}|{LEGAL_APP_PATTERN.ToLower()}|{WEB_OFFICER_CLIENT_PATTERN.ToLower()}");

         try
         {
            using (ServerManager mgr = ServerManager.OpenRemote(host))
            {
               var sites = mgr.Sites.Where(x => allReg.IsMatch(x.Name.ToLower())); // enrich regex to filter sites
               int sitesIndex = 0;
               foreach (var site in sites)  
               {
                  try
                  {
                     var siteName = site.Name;

                     if (site.Bindings != null && site.Bindings.Count > 0 && (site.Bindings[0]).EndPoint != null)
                     {
                        //if (!site.Name.Contains("12_3")) continue;
                        if (webAPIReg.IsMatch(site.Name.ToLower()))
                        {
                           WebAPISiteInfo siteInfo = new WebAPISiteInfo(host, site);                           
                           WebSites.Add(siteInfo);
                           OnWebSiteInfoCollected(new WebSiteInfoEventArgs(siteInfo));

                        }
                        else if (identityReg.IsMatch(site.Name.ToLower()))
                        {
                           IdentityServerSiteInfo siteInfo = new IdentityServerSiteInfo(host, site);
                           WebSites.Add(siteInfo);
                           OnWebSiteInfoCollected(new WebSiteInfoEventArgs(siteInfo));
                        }
                        else if (legalAppReg.IsMatch(site.Name.ToLower()))
                        {
                           LegalAppSiteInfo siteInfo = new LegalAppSiteInfo(host, site);
                           WebSites.Add(siteInfo);
                           OnWebSiteInfoCollected(new WebSiteInfoEventArgs(siteInfo));
                        }
                        else if (webOfficerClientReg.IsMatch(site.Name.ToLower()))
                        {
                           WebOfficerClientSiteInfo siteInfo = new WebOfficerClientSiteInfo(host, site);
                           WebSites.Add(siteInfo);
                           OnWebSiteInfoCollected(new WebSiteInfoEventArgs(siteInfo));
                        }

                     }
                  }
                  catch (Exception ex)
                  {

                  }
                  sitesIndex++;
                  OnProcessingInfoCollected(new ProcessingInfoCollectedEventArgs(_HostsCount, _CurrentHostLoadedNumber, sites.Count(), sitesIndex));
                  if (_CancelLoad) break;
               }
            }
         }
         catch (Exception ex)
         {
            //objEnv.Errors.AddError($"Error while fetching web server's cf information. IIS Management Console is needed to install. ({ex.Message})", "");
         }
      }

      private bool ReadFromCache()
      {
         bool retVal = false;
         try
         {
            if (File.Exists(AppInstance.WebSitesCacheFile))
            {
               string json = File.ReadAllText(AppInstance.WebSitesCacheFile);
               JsonSerializerSettings settings = new JsonSerializerSettings();
               settings.TypeNameHandling = TypeNameHandling.Auto;
               WebSites = JsonConvert.DeserializeObject<List<WebSiteInfo>>(json, settings);
               foreach(var siteInfo in WebSites)
               {
                  OnWebSiteInfoCollected(new WebSiteInfoEventArgs(siteInfo)); 
               }
               retVal = true;
            }
         }
         catch(Exception ex)
         {
            retVal = false;
         }
         return retVal;
      }

      private void WriteToCache()
      {
         try
         {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string json = JsonConvert.SerializeObject(WebSites, Formatting.Indented, settings);
            File.WriteAllText(AppInstance.WebSitesCacheFile, json);
         }
         catch(Exception ex)
         {

         }
      }
      
   }
}
