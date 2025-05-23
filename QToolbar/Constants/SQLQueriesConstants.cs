using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Constants
{
   internal static class SQLQueriesConstants
   {
    
      public static string GetDevDBsSQL()
      {
         return
            @"SELECT [name] as DB_NAME
               FROM sys.databases AS D

               --[0 - 9]_[0 - 9]_[0 - 9]{0,5}
               WHERE[name] LIKE 'Qbcollection_plus_[0-9]_[0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9]_[0-9]_[0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9]_[0-9]_[0-9][0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9]_[0-9]_[0-9][0-9][0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9]_[0-9]_[0-9][0-9][0-9][0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9]_[0-9]_[0-9][0-9][0-9][0-9][0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9]_[0-9]_[0-9][0-9][0-9][0-9][0-9][0-9]'

               --[0 - 9]{2}_[0 - 9]_[0 - 9]{ 0,5}
               OR[name] LIKE 'Qbcollection_plus_[0-9][0-9]_[0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9][0-9]_[0-9]_[0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9][0-9]_[0-9]_[0-9][0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9][0-9]_[0-9]_[0-9][0-9][0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9][0-9]_[0-9]_[0-9][0-9][0-9][0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9][0-9]_[0-9]_[0-9][0-9][0-9][0-9][0-9]'

               OR[name] LIKE 'Qbcollection_plus_[0-9][0-9]_[0-9]_[0-9][0-9][0-9][0-9][0-9][0-9]'";
      }
   }
}
