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

      public string EnvironmentsUrl { get; set; }
      public List<WebAPIEnvironment> WebAPIEnvironments { get; internal set; }

      /// <summary>
      /// Used by deserialization
      /// </summary>
      public WebAPISiteInfo()
      {
      }

      public WebAPISiteInfo(string host, Site site): base(host, site)
      {
         WebSiteType = WebSiteTypeEnum.WebAPI;
         _ConnectionStrings = new Dictionary<string, string>();
         _AppSettingsFile = GetAppSettingsFile();

         Url = GetSwaggerUrl();
         EnvironmentsUrl = GetEnvsUrl();
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
                  WebAPIEnvironments = JsonConvert.DeserializeObject<List<WebAPIEnvironment>>(json);
                  foreach (WebAPIEnvironment env in WebAPIEnvironments)
                  {
                     env.connectionString = _ConnectionStrings[env.connectionStringHash];
                     LoadEnvLogins(env);
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


      private void LoadEnvLogins(WebAPIEnvironment env)
      {
         try
         {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(env.connectionString);
            string initialCatalog = builder.InitialCatalog;
            builder.InitialCatalog = string.Empty;
            bool databaseExists = false;
            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
               string sql = $" SELECT COUNT(1) FROM master.dbo.sysdatabases WHERE name = '{initialCatalog}'";
               SqlCommand cmd = new SqlCommand(sql, con);
               try
               {
                  con.Open();
                  int result = (int)cmd.ExecuteScalar();
                  databaseExists = result > 0;
               }
               catch(Exception ex)
               {

               }
               finally
               {
                  con.Close();
               }
            }

            if (databaseExists)
            {


               using (SqlConnection con = new SqlConnection(env.connectionString))
               {
                  string sql = "SELECT APC_NAME,APC_COMMENTS FROM AT_API_CLIENT_CREDENTIALS WHERE APC_ACTIVE_FLAG=1 AND ISNULL(APC_COMMENTS,'')<>''";
                  SqlDataAdapter adapter = new SqlDataAdapter(sql, con);

                  DataSet dataset = new DataSet();
                  adapter.Fill(dataset);
                  foreach (DataRow row in dataset.Tables[0].Rows)
                  {
                     env.Logins.Add(new WebAPIEnvironmentLogin()
                     {
                        ClientId = row.Field<string>("APC_NAME") + "{[(" + env.connectionStringHash + ")]}",
                        ClientSecret = row.Field<string>("APC_COMMENTS")
                     });
                  }
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

      private string GetAppSettingsFile()
      {
         return $"{UNCPath}\\appsettings.json";
      }
   }
}
