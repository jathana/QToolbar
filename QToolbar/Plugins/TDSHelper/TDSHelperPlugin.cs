using DevExpress.XtraEditors;
using QToolbar.Plugins.TDSHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Plugins.TDSHelper
{

   [Plugin(pluginName: "TDS Helper", pluginDesc: @"TDS Helper.")]
   public class TDSHelperPlugin : QPlugin, IQPlugin
   {
      public TDSHelperPlugin() { }

      public bool Run()
      {
         bool retVal = true;

         PluginAttribute p= (PluginAttribute)this.GetType().GetCustomAttributes(false).Where(t => t is PluginAttribute).FirstOrDefault();
         var f = new Frm_TDSHelper()
         {
            Text = p.PluginName
         };
         f.Show();
            
         return retVal;
      }


      
      
   }
}
