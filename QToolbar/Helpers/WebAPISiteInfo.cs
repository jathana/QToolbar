using Microsoft.Web.Administration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebAPISiteInfo:WebSiteInfo
   {
      private string _AppSettingsFile = string.Empty;
      /// <summary>
      /// Key : connection string hash, value: connection string
      /// </summary>
      private Dictionary<string, string> _ConnectionStrings;
      public class WebAPIEnv
      {
         public string name { get; set; }
         public string description { get; set; }
         public string connectionStringHash { get; set; }
         public string connectionString { get; set; }

         public SortedList<string, string> Logins { get; } = new SortedList<string, string>();
      }

      public List<WebAPIEnv> WebAPIEnvironments { get; internal set; }

      public WebAPISiteInfo(string host, Site site): base(host, site)
      {
         WebSiteType = WebSiteTypeEnum.WebAPI;
         _ConnectionStrings = new Dictionary<string, string>();
         _AppSettingsFile = $"{UNCPath}\\appsettings.json";

         Url = GetSwaggerUrl();

         AddProperty("Environments", GetEnvsUrl(), null);
         LoadConnectionStringsWithHashes();
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
                     env.connectionString = _ConnectionStrings[env.connectionStringHash];
                     LoadEnvLogins(env);
                     AddProperty(env.description, GetSwaggerEnvUrl(env.connectionStringHash), env.Logins.AsEnumerable().Select(i=>new SimpleProperty() { Name = i.Key, Value = i.Value }).ToList());
                  }
               }
            }
         }
         catch(Exception ex)
         {

         }
      }

      private string IncludeTransparentNetworkIPResolution(string connectionString)
      {
         string result = connectionString;

         if (!string.IsNullOrEmpty(connectionString))
         {
            if (!connectionString.EndsWith(";"))
            {
               result += ";";
            }
            result += "TransparentNetworkIPResolution = False;";
         }
         return result;
      }


      private void LoadEnvLogins(WebAPIEnv env)
      {
         try
         {
            using (SqlConnection con = new SqlConnection(env.connectionString))
            {
               string sql = "SELECT APC_NAME,APC_COMMENTS FROM AT_API_CLIENT_CREDENTIALS WHERE APC_ACTIVE_FLAG=1 AND ISNULL(APC_COMMENTS,'')<>''";
               SqlDataAdapter adapter = new SqlDataAdapter(sql, con);

               DataSet dataset = new DataSet();
               adapter.Fill(dataset);
               foreach(DataRow row in dataset.Tables[0].Rows)
               {
                  env.Logins.Add(row.Field<string>("APC_NAME") + "{[(" + env.connectionStringHash + ")]}", row.Field<string>("APC_COMMENTS"));
               }
               
            }
         }
         catch (Exception ex)
         {
         }
      }

      private void LoadConnectionStringsWithHashes()
      {
         try
         {
            if (File.Exists(_AppSettingsFile))
            {
               var appSettingsContent = File.ReadAllText(_AppSettingsFile);
               var appSettings = JsonConvert.DeserializeObject(appSettingsContent);
               var connectionStrings = ((JObject)appSettings)["ConnectionStrings"];
               foreach(JToken token in connectionStrings)
               {
                  JProperty property = (JProperty)token;
                  _ConnectionStrings.Add(property.Value.ToString().GetSHA256HashHex(), IncludeTransparentNetworkIPResolution(property.Value.ToString()));
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
