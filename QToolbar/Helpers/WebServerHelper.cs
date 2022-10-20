using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
      private bool _CancelLoad;

      public List<WebSiteInfo> WebSites { get; }

      #region events
      public delegate void WebSiteInfoCollectedEventHandler(object sender, WebSiteInfoEventArgs args);
      public event WebSiteInfoCollectedEventHandler WebSiteInfoCollected;
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
      public void LoadInfo(string hostsList)
      {

         _CancelLoad = false;
         WebSites.Clear();
         Regex reg = new Regex(WEB_API_NAME_PATTERN);
         _HostsList = hostsList;

         List<string> hosts = _HostsList.Split(',').ToList<string>();
         foreach(string host in hosts)
         {
            LoadInfoInternal(host);
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

      private void LoadInfoInternal(string host)
      {
         
         Regex webAPIReg = new Regex(WEB_API_NAME_PATTERN);
         Regex identityReg = new Regex(IDENTITY_SERVER_PATTERN);


         Regex allReg = new Regex($"{WEB_API_NAME_PATTERN}|{IDENTITY_SERVER_PATTERN}");

         try
         {
            using (ServerManager mgr = ServerManager.OpenRemote(host))
            {
               foreach (var site in mgr.Sites.Where(x=>allReg.IsMatch(x.Name)))  // enrich regex to filter sites
               {
                  try
                  {
                     if (site.Bindings != null && site.Bindings.Count > 0 && (site.Bindings[0]).EndPoint != null)
                     {
                        if (webAPIReg.IsMatch(site.Name))
                        {
                           WebAPISiteInfo siteInfo = new WebAPISiteInfo(host, site);
                           WebSites.Add(siteInfo);
                           OnWebSiteInfoCollected(new WebSiteInfoEventArgs(siteInfo));                           
                        }

                        if (identityReg.IsMatch(site.Name))
                        {
                           IdentityServerSiteInfo siteInfo = new IdentityServerSiteInfo(host, site);
                           WebSites.Add(siteInfo);
                           OnWebSiteInfoCollected(new WebSiteInfoEventArgs(siteInfo));
                        }
                     }
                  }
                  catch (Exception ex)
                  {

                  }
                  if (_CancelLoad) break;
               }

            }
         }
         catch (Exception ex)
         {
            //objEnv.Errors.AddError($"Error while fetching web server's cf information. IIS Management Console is needed to install. ({ex.Message})", "");
         }
      }


      
   }
}
