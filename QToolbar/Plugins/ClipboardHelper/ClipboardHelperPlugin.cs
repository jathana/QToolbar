using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.ClipboardHelper
{

   [Plugin(pluginName: "Clipboard Helper", pluginDesc: @"Clipboard Helper.")]
   public class ClipboardHelperPlugin : QPlugin, IQPlugin
   {
      public ClipboardHelperPlugin() { }

      public bool Run()
      {
         bool retVal = true;

         PluginAttribute p= (PluginAttribute)this.GetType().GetCustomAttributes(false).Where(t => t is PluginAttribute).FirstOrDefault();
         var f = new Frm_ClipboardHelper()
         {
            Text = p.PluginName
         };
         f.Show();
            
         return retVal;
      }
      
   }
}
