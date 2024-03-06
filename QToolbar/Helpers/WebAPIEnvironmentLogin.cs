using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebAPIEnvironmentLogin
   {
      public string ClientId { get; set; }
      public string ClientSecret { get; set; }

      public int? VendCode { get; set; }
      public int? ExtcCode { get; set; }
   }
}
