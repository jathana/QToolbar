using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebAPIEnvironment
   {
      public string name { get; set; }
      public string description { get; set; }
      public string connectionStringHash { get; set; }
      public string connectionString { get; set; }
      public List<WebAPIEnvironmentLogin> Logins { get; } = new List<WebAPIEnvironmentLogin>();
      public WebSiteInfo WebSiteInfo { get; internal set; }
      public string EnvironmentUrl {
         get
         {
            if (WebSiteInfo != null && !string.IsNullOrEmpty(WebSiteInfo.Url))
            {
               return $"{WebSiteInfo.Url}?connectionStringHash={connectionStringHash}";
            }

            return string.Empty;
         }
      }
      public string LocalEnvironmentUrl
      {
         get
         {
            if (WebSiteInfo != null && !string.IsNullOrEmpty(WebSiteInfo.Url))
            {
               return $"https://localhost:44367/swagger/index.html?connectionStringHash={connectionStringHash}";
            }

            return string.Empty;
         }
      }


   }
}
