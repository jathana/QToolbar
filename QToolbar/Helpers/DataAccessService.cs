using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace QToolbar.Helpers
{
   public class DataAccessService
   {

      /// <summary>
      /// return the connection string of the current database
      /// </summary>
      /// <returns></returns>
      public ConnectionInfo GetCurrentDBConnectionInfo()
      {
         string[] devSQLInstances = OptionsInstance.DevSQLInstances.Split(',');
         List<(string Server, string Database)> latestDB = new List<(string Server, string Database)>();

         // find latest collection plus db per server
         foreach (string devSQLInstance in devSQLInstances)
         {
            if (string.IsNullOrEmpty(devSQLInstance)) continue;
            using (SqlConnection connection = new SqlConnection(Utils.GetConnectionString(devSQLInstance)))
            {

               SqlCommand com = new SqlCommand(@"SELECT [name] as DB_NAME
                                                       FROM sys.databases AS D

                                                       --[0 - 9]_[0 - 9]_[0 - 9]{0,5}
                                                       WHERE[name] LIKE 'Qbcollection_plus_[0-9]_[0-9]'

                                                       --[0 - 9]{2}_[0 - 9]_[0 - 9]{ 0,5}
                                                       OR[name] LIKE 'Qbcollection_plus_[0-9][0-9]_[0-9]'
                                                    ", connection);
               try
               {
                  connection.Open();
                  string result = (string)com.ExecuteScalar();
                  latestDB.Add((devSQLInstance, result));
               }
               catch (Exception ex)
               {
                  throw;
               }
            }
         }
         var currentDb = latestDB.OrderByDescending(x => x.Database).FirstOrDefault();
         ConnectionInfo connectionInfo = new ConnectionInfo();
         connectionInfo.InfoType = InfoType.Database;
         connectionInfo.Server = currentDb.Server;
         connectionInfo.Database = currentDb.Database;
         // version
         var version = currentDb.Database.Split('_');
         connectionInfo.Environment = $"{version[version.Length - 2]}.{version[version.Length - 1]}";
         connectionInfo.CFPath = $@"{OptionsInstance.QCSAdminFolder}\{connectionInfo.Environment}";



         return connectionInfo;
      }
   }
}
