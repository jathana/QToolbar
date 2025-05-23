using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using Microsoft.SqlServer.Management.Smo;
using QToolbar.Constants;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
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
         List<(string Server, string Database, string DatabaseSortName)> latestDB = new List<(string Server, string Database, string DatabaseSortName)>();

         // find latest collection plus db per server
         foreach (string devSQLInstance in devSQLInstances)
         {
            if (string.IsNullOrEmpty(devSQLInstance)) continue;
            using (SqlConnection connection = new SqlConnection(Utils.GetConnectionString(devSQLInstance)))
            {

               SqlCommand com = new SqlCommand(SQLQueriesConstants.GetDevDBsSQL(), connection);
               try
               {
                  connection.Open();
                  SqlDataAdapter adapter = new SqlDataAdapter(com);
                  DataTable table = new DataTable();
                  adapter.Fill(table);
                  if (table.Rows.Count > 0)
                  {
                     
                     latestDB.AddRange(table.AsEnumerable().Select(x => (devSQLInstance, x.Field<string>("DB_NAME"), NamesHelper.GetDatabaseSortName(x.Field<string>("DB_NAME"), new Char[] { '.', '_' }, '_', '0', 5))).ToArray());
                  }
               }
               catch (Exception ex)
               {
                  
               }
            }
         }
         var currentDb = latestDB.OrderByDescending(x => x.DatabaseSortName).FirstOrDefault();
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
