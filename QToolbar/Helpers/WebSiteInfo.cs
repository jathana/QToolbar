using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebSiteInfo
   {
      public string Host;
      public string Port;
      public string Name;
      public string Protocol;

      public override string ToString()
      {
         return $"{Protocol}://{Host}/{Name}:{Port}";
      }
   }
}
