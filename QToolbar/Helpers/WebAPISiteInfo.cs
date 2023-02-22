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
         WebSiteType = WebSiteTypeEnum.WebAPI;

         Url = GetSwaggerUrl();

         AddProperty("Environments", GetEnvsUrl());
         LoadEnvironments();
      }


      private void LoadEnvironments()
      {

         try
         {
            WebRequest wrGetEnvs;
            wrGetEnvs = WebRequest.Create(GetEnvsUrl());


            using (Stream objStream = wrGetEnvs.GetResponse().GetResponseStream())
            {
               using (StreamReader objReader = new StreamReader(objStream))
               {
                  string json = objReader.ReadToEnd();
                  WebAPIEnvironments = JsonConvert.DeserializeObject<List<WebAPIEnv>>(json);
                  foreach (WebAPIEnv env in WebAPIEnvironments)
                  {
                     AddProperty(env.description, GetSwaggerEnvUrl(env.connectionStringHash));
                  }
               }
            }
         }
         catch(Exception ex)
         {

         }
      }

      private string GetEnvsUrl()
      {
         return $"{Protocol}://{Host}.qualco.int:{Port}/API/Environments";
      }

      private string GetSwaggerUrl()
      {
         return $"{Protocol}://{Host}.qualco.int:{Port}/swagger/index.html";
      }
      private string GetSwaggerEnvUrl(string connectionStringHash)
      { 
         return $"{Protocol}://{Host}.qualco.int:{Port}/swagger/index.html?connectionStringHash={connectionStringHash}";
      }
   }
}
