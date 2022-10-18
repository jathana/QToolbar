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

namespace QToolbar.Plugins.WebSitesInfo
{
   public partial class Frm_WebSitesInfo : DevExpress.XtraEditors.XtraForm
   {
      WebServerHelper WebServerHelper;
      SynchronizationContext _SyncContext;

      public Frm_WebSitesInfo()
      {
         InitializeComponent();


         UXGridView.OptionsBehavior.Editable = false;
         UXGridView.OptionsView.RowAutoHeight = true;
         //UXGridView.ViewRegistered += UXGrid_ViewRegistered;

         UXGridView.OptionsPrint.PrintDetails = true;
         UXGridView.OptionsPrint.ExpandAllDetails = true;
         UXGridView.OptionsPrint.ExpandAllGroups = true;

         WebServerHelper = new WebServerHelper();
         WebServerHelper.WebSiteInfoCollected += WebServerHelper_WebSiteInfoCollected;
         _SyncContext = SynchronizationContext.Current;
      }

      private void WebServerHelper_WebSiteInfoCollected(object sender, WebSiteInfoEventArgs args)
      {
         backgroundWorker1.ReportProgress(WebServerHelper.WebSites.Count);
      }

      private void Frm_WebSitesInfo_Load(object sender, EventArgs e)
      {
        
         gridWebAPI.DataSource = WebServerHelper.WebSites;

         backgroundWorker1.RunWorkerAsync();
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         WebServerHelper.LoadInfo("q-srv-w2k12r2");
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         _SyncContext.Post((input) =>
         {
            gridWebAPI.RefreshDataSource();
            UXGridView.BestFitColumns();
         }, e);
         
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {

      }
   }

}