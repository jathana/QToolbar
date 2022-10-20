using Microsoft.Web.Administration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class IdentityServerSiteInfo : WebSiteInfo
   {

      public class WebAPIEnv
      {
         public string name { get; set; }
         public string description { get; set; }
         public string connectionStringHash { get; set; }
      }
      

      public IdentityServerSiteInfo(string host, Site site): base(host, site)
      {
         Url = GetIdentityUrl();
      }


      

      private string GetIdentityUrl()
      {
         return $"{Protocol}://{Host}.qualco.int:{Port}";
      }
   }
}
