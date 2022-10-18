using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
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


      public List<WebSiteInfo> WebSites = new List<WebSiteInfo>();

      #region events
      public delegate void WebSiteInfoCollectedEventHandler(object sender, WebSiteInfoEventArgs args);
      public event WebSiteInfoCollectedEventHandler WebSiteInfoCollected;
      #endregion

      private string _Host;
      public void LoadInfo(string host)
      {

         WebSites.Clear();
         Regex reg = new Regex(WEB_API_NAME_PATTERN);
         _Host = host;
         try
         {

            // add cfs from web server

            using (ServerManager mgr = ServerManager.OpenRemote(_Host))
            {
               foreach (var s in mgr.Sites)
               {
                  try
                  {
                     //if (s.Bindings != null && s.Bindings.Count > 0 && (s.Bindings[0]).EndPoint != null && (s.Bindings[0]).EndPoint.Port.ToString().Equals(objEnv.AppWSUrlPort) && s.Name.StartsWith(envNameInWeb))
                     if (s.Bindings != null && s.Bindings.Count > 0 && (s.Bindings[0]).EndPoint != null &&  reg.IsMatch(s.Name))
                     {
                        WebAPISiteInfo siteInfo = new WebAPISiteInfo()
                        {
                           Host = _Host,
                           Name = s.Name,
                           Port = (s.Bindings[0]).EndPoint.Port.ToString(),
                           Protocol = s.Bindings[0].Protocol
                        };
                        
                        WebSites.Add(siteInfo);
                        OnWebSiteInfoCollected(new WebSiteInfoEventArgs(siteInfo));

                        //string qcwsPhPath = null;
                        //string toolkitPhPath = null;
                        //foreach (var a in s.Applications)
                        //{
                        //   var qcwsVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\Qualco\\QCSWS"));
                        //   if (qcwsVDir != null)
                        //   {
                        //      qcwsPhPath = qcwsVDir.PhysicalPath;
                        //      QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                        //      cfInfo.Name = "qbc.cf";
                        //      cfInfo.Repository = "QC";
                        //      cfInfo.Path = $"\\\\{webServer.Host}\\{qcwsPhPath.Replace(":", "$")}\\qbc.cf";
                        //      objEnv.CFs.Add(cfInfo);
                        //   }


                        //   var toolkitVDir = a.VirtualDirectories.FirstOrDefault(v => v.PhysicalPath.Contains("\\Qualco\\SCToolkitWS"));
                        //   if (toolkitVDir != null)
                        //   {
                        //      toolkitPhPath = toolkitVDir.PhysicalPath;
                        //      QEnvironment.CfInfo cfInfo = new QEnvironment.CfInfo();
                        //      cfInfo.Name = "qbc.cf";
                        //      cfInfo.Repository = "QC";
                        //      cfInfo.Path = $"\\\\{webServer.Host}\\{toolkitPhPath.Replace(":", "$")}\\qbc.cf";
                        //      objEnv.CFs.Add(cfInfo);

                        //   }
                        //}
                     }
                  }
                  catch (Exception ex)
                  {

                  }
               }

            }
         }
         catch (Exception ex)
         {
            //objEnv.Errors.AddError($"Error while fetching web server's cf information. IIS Management Console is needed to install. ({ex.Message})", "");
         }
      }

      private void OnWebSiteInfoCollected(WebSiteInfoEventArgs args)
      {
         if (WebSiteInfoCollected != null)
         {
            WebSiteInfoCollected(this, args);
         }
      }
   }
}
