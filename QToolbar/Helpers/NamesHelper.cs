using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   internal static class NamesHelper
   {
      public static string GetDatabaseSortName(string database, char[] delimiters, char joinDelimiter, char padChar, int padLength, bool discardNonNumbers = false)
      {
         string result = database;
         if (!string.IsNullOrEmpty(database))
         {
            List<string> db = database.Split(delimiters).ToList();
            List<string> res = new List<string>();
            for (int i = 0; i < db.Count; i++)
            {
               if (db[i].All(Char.IsDigit))
               {
                  res.Add(db[i].PadLeft(padLength, padChar));
               }
               else
               {
                  if (!discardNonNumbers)
                  {
                     res.Add(db[i]);
                  }
               }
            }
            result = string.Join(joinDelimiter.ToString(), res);
         }
         return result;
      }
   }
}
