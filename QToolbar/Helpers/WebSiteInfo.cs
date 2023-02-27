using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebSiteInfo
   {
      [Display(AutoGenerateField = false)]
      public WebSiteTypeEnum WebSiteType { get; set; }
      public string Name { get; set; }
      public string Url { get; set; }

      public string Host { get; set; }
      public string Port { get; set; }
      
      public string Protocol { get; set; }

      public string PhysicalPath { get; set; }
      public string UNCPath { get; set; }

      public WebSiteInfo()
      {
      }

      public WebSiteInfo(string host, Site site)
      {
         Host = host;
         Name = GetSiteName(site);
         Port = GetSitePort(site);
         Protocol = GetSiteProtocol(site);
         PhysicalPath = GetSitePhysicalPath(site);
         UNCPath = GetSiteUNCPath(site);
      }

      public override string ToString()
      {
         return $"{Protocol}://{Host}/{Name}:{Port}";
      }

      
      

      private string GetSitePort(Site site)
      {
         string retVal = string.Empty;

         if (site != null && site.Bindings != null && site.Bindings.Count > 0 && (site.Bindings[0]).EndPoint != null)
         {
            retVal = (site.Bindings[0]).EndPoint.Port.ToString();
         }
         return retVal;
      }

      private string GetSiteProtocol(Site site)
      {
         string retVal = string.Empty;

         if (site != null && site.Bindings != null && site.Bindings.Count > 0 && (site.Bindings[0]).EndPoint != null)
         {
            retVal = site.Bindings[0].Protocol;
         }
         return retVal;
      }


      private string GetSiteName(Site site)
      {
         string retVal = string.Empty;

         if (site != null)
         {
            retVal = site.Name;
         }
         return retVal;
      }

      private string GetSitePhysicalPath(Site site)
      {
         return site.Applications["/"].VirtualDirectories["/"].PhysicalPath;
      }

      private string GetSiteUNCPath(Site site)
      {
         string physicalPath = GetSitePhysicalPath(site);
         return $"\\\\{Host}\\{physicalPath.Replace(":", "$")}";
      }

   }
}
