using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebAPIEnvironment
   {
      public string name { get; set; }
      public string description { get; set; }
      public string connectionStringHash { get; set; }
      public string connectionString { get; set; }
      public List<WebAPIEnvironmentLogin> Logins { get; } = new List<WebAPIEnvironmentLogin>();
   }
}
