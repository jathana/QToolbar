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
   public class LegalAppSiteInfo : WebSiteInfo
   {
      public LegalAppSiteInfo(string host, Site site): base(host, site)
      {
         Url = GetUrl();
      }
      

      private string GetUrl()
      {
         return $"{Protocol}://{Host}:{Port}/QCLegalApplicationApp/";
      }
   }
}
