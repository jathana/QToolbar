using DevExpress.XtraEditors;
using QToolbar.Plugins.WebSitesInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.ClipboardHelper
{

   [Plugin(pluginName: "Web Sites Info", pluginDesc: @"Web Sites Information.")]
   public class WebSitesInfoPlugin : QPlugin, IQPlugin
   {
      public WebSitesInfoPlugin() { }

      public bool Run()
      {
         bool retVal = true;

         PluginAttribute p= (PluginAttribute)this.GetType().GetCustomAttributes(false).Where(t => t is PluginAttribute).FirstOrDefault();
         var f = new Frm_WebSitesInfo()
         {
            Text = p.PluginName
         };
         f.Show();
            
         return retVal;
      }


      private void ExportWebSites()
      {
         //% windir %\system32\inetsrv\appcmd list site / config / xml > c:\websites.xml
         string remoteCommand = @"%windir%\system32\inetsrv\appcmd list site /config /xml > c:\websites.xml";


      }
      
   }
}
