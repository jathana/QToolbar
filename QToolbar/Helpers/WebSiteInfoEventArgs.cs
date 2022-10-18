using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebSiteInfoEventArgs : EventArgs
   {
      public WebSiteInfo WebSiteInfo { get; set; }

      public WebSiteInfoEventArgs(WebSiteInfo webSiteInfo)
      {
         WebSiteInfo = webSiteInfo;
      }
   }
}
