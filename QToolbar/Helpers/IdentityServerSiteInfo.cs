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

      /// <summary>
      /// Used by deserialization
      /// </summary>
      public IdentityServerSiteInfo()
      {
      }

      public IdentityServerSiteInfo(string host, Site site): base(host, site)
      {
         Url = GetIdentityUrl();

         WebSiteType = WebSiteTypeEnum.IdentityServer;
      }
      

      private string GetIdentityUrl()
      {
         return $"{Protocol}://{Host}.qualco.int:{Port}";
      }
   }
}
