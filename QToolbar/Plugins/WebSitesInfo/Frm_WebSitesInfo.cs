using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System.IO;
using System.Diagnostics;
using QToolbar.Helpers;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace QToolbar.Plugins.WebSitesInfo
{
   public partial class Frm_WebSitesInfo : DevExpress.XtraEditors.XtraForm
   {
      private WebServerHelper _WebServerHelper;

      private SynchronizationContext _SyncContext;
      private BindingList<WebAPISiteInfo> _WebAPISites;

      public Frm_WebSitesInfo()
      {
         InitializeComponent();


         UXGridView.OptionsBehavior.Editable = false;
         UXGridView.OptionsView.RowAutoHeight = true;
         gridWebAPI.ViewRegistered += UXGrid_ViewRegistered;

         UXGridView.OptionsPrint.PrintDetails = true;
         UXGridView.OptionsPrint.ExpandAllDetails = true;
         UXGridView.OptionsPrint.ExpandAllGroups = true;

         _WebServerHelper = new WebServerHelper();
         _WebServerHelper.WebSiteInfoCollected += WebServerHelper_WebSiteInfoCollected;
         _SyncContext = SynchronizationContext.Current;
         _WebAPISites = new BindingList<WebAPISiteInfo>();

      }

      private void UXGrid_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
      {
        // e.View.DoubleClick += View_DoubleClick;
         ((GridView)e.View).BestFitColumns();
      }

      private void WebServerHelper_WebSiteInfoCollected(object sender, WebSiteInfoEventArgs args)
      {
         _SyncContext.Post((input) =>
         {
            if(input != null)
            {
               WebSiteInfoEventArgs inputArgs = (WebSiteInfoEventArgs)input;
               if (inputArgs.WebSiteInfo != null && inputArgs.WebSiteInfo is WebAPISiteInfo)
               {
                  _WebAPISites.Add((WebAPISiteInfo)inputArgs.WebSiteInfo);
               }
            }
            //UXGridView.BestFitColumns();
         }, args);
         backgroundWorker1.ReportProgress(_WebAPISites.Count);
      }

      private void Frm_WebSitesInfo_Load(object sender, EventArgs e)
      {
         _WebAPISites.Clear();
         gridWebAPI.DataSource = _WebAPISites;

         backgroundWorker1.RunWorkerAsync();
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         _WebServerHelper.LoadInfo(OptionsInstance.QCWebServers);
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         _SyncContext.Post((input) =>
         {
            UXGridView.BestFitColumns();
         }, e);
         
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         
      }
   }

}