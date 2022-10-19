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
   public class WebAPISiteInfo:WebSiteInfo
   {

      public class WebAPIEnv
      {
         public string name { get; set; }
         public string description { get; set; }
         public string connectionStringHash { get; set; }
      }

      public List<WebAPIEnv> WebAPIEnvironments { get; internal set; }

      public WebAPISiteInfo(string host, Site site): base(host, site)
      {
         Url = GetSwaggerUrl();

         AddProperty("Environments", GetEnvUrl());
         LoadEnvironments();
      }


      private void LoadEnvironments()
      {

         try
         {
            WebRequest wrGetEnvs;
            wrGetEnvs = WebRequest.Create(GetEnvUrl());


            using (Stream objStream = wrGetEnvs.GetResponse().GetResponseStream())
            {
               using (StreamReader objReader = new StreamReader(objStream))
               {
                  string json = objReader.ReadToEnd();
                  WebAPIEnvironments = JsonConvert.DeserializeObject<List<WebAPIEnv>>(json);

               }
            }
         }
         catch(Exception ex)
         {

         }
      }

      private string GetEnvUrl()
      {
         return $"{Protocol}://{Host}.qualco.int:{Port}/API/Environments";
      }
      private string GetSwaggerUrl()
      {
         return $"{Protocol}://{Host}.qualco.int:{Port}/swagger/index.html";
      }
   }
}
