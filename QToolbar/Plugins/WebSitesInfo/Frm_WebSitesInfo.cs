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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using static QToolbar.Helpers.WebSiteInfo;
using DevExpress.XtraGrid;

namespace QToolbar.Plugins.WebSitesInfo
{
   public partial class Frm_WebSitesInfo : DevExpress.XtraEditors.XtraForm
   {
      private WebServerHelper _WebServerHelper;

      private SynchronizationContext _SyncContext;
      private BindingList<WebAPISiteInfo> _WebAPIWebSites;
      private BindingList<LegalAppSiteInfo> _LegalAppWebSites;
      private BindingList<IdentityServerSiteInfo> _IdentityServerWebSites;
      private BindingList<WebOfficerClientSiteInfo> _WebOfficerClientWebSites;

      public Frm_WebSitesInfo()
      {
         InitializeComponent();

         // UXWebAPIGridView
         UXWebAPIGrid.ViewRegistered += UXGrid_ViewRegistered;
         UXWebAPIGrid.KeyDown += grid_KeyDown;

         UXWebAPIGridView.OptionsBehavior.Editable = false;
         UXWebAPIGridView.OptionsView.RowAutoHeight = true;
         UXWebAPIGridView.OptionsPrint.PrintDetails = true;
         UXWebAPIGridView.OptionsPrint.ExpandAllDetails = true;
         UXWebAPIGridView.OptionsPrint.ExpandAllGroups = true;

         // UXLegalAppGridView
         UXLegalAppGrid.ViewRegistered += UXGrid_ViewRegistered;
         UXLegalAppGrid.KeyDown += grid_KeyDown;
         UXLegalAppGridView.OptionsBehavior.Editable = false;
         UXLegalAppGridView.OptionsView.RowAutoHeight = true;
         UXLegalAppGridView.OptionsPrint.PrintDetails = true;
         UXLegalAppGridView.OptionsPrint.ExpandAllDetails = true;
         UXLegalAppGridView.OptionsPrint.ExpandAllGroups = true;

         // UXIdentityServerView
         UXIdentityServerGrid.ViewRegistered += UXGrid_ViewRegistered;
         UXIdentityServerGrid.KeyDown += grid_KeyDown;
         UXIdentityServerGridView.OptionsBehavior.Editable = false;
         UXIdentityServerGridView.OptionsView.RowAutoHeight = true;
         UXIdentityServerGridView.OptionsPrint.PrintDetails = true;
         UXIdentityServerGridView.OptionsPrint.ExpandAllDetails = true;
         UXIdentityServerGridView.OptionsPrint.ExpandAllGroups = true;

         // UXWebOfficerClientView
         UXWebOfficerClientGrid.ViewRegistered += UXGrid_ViewRegistered;
         UXWebOfficerClientGrid.KeyDown += grid_KeyDown;
         UXWebOfficerClientGridView.OptionsBehavior.Editable = false;
         UXWebOfficerClientGridView.OptionsView.RowAutoHeight = true;
         UXWebOfficerClientGridView.OptionsPrint.PrintDetails = true;
         UXWebOfficerClientGridView.OptionsPrint.ExpandAllDetails = true;
         UXWebOfficerClientGridView.OptionsPrint.ExpandAllGroups = true;




         _WebServerHelper = new WebServerHelper();
         _WebServerHelper.WebSiteInfoCollected += WebServerHelper_WebSiteInfoCollected;
         _SyncContext = SynchronizationContext.Current;
         _WebAPIWebSites = new BindingList<WebAPISiteInfo>();
         _LegalAppWebSites = new BindingList<LegalAppSiteInfo>();
         _IdentityServerWebSites = new BindingList<IdentityServerSiteInfo>();
         _WebOfficerClientWebSites = new BindingList<WebOfficerClientSiteInfo>();
      }

      private void grid_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Control && e.KeyCode == Keys.C)
         {
            GridControl grid = (GridControl)sender;
            GridView view = grid.FocusedView as GridView;
            Clipboard.SetText(view.GetFocusedDisplayText());
            e.Handled = true;
         }
      }
      private void UXGrid_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
      {
         e.View.DoubleClick += View_DoubleClick;
         ((GridView)e.View).BestFitColumns();
      }

      private void WebServerHelper_WebSiteInfoCollected(object sender, WebSiteInfoEventArgs args)
      {
         _SyncContext.Post((input) =>
         {
            if(input != null)
            {
               WebSiteInfoEventArgs inputArgs = (WebSiteInfoEventArgs)input;
               if (inputArgs.WebSiteInfo != null)
               {
                  if(inputArgs.WebSiteInfo is WebAPISiteInfo)
                  {
                     _WebAPIWebSites.Add((WebAPISiteInfo)inputArgs.WebSiteInfo);
                     UXWebAPIGridView.BestFitColumns();
                  }
                  else if (inputArgs.WebSiteInfo is LegalAppSiteInfo)
                  {
                     _LegalAppWebSites.Add((LegalAppSiteInfo)inputArgs.WebSiteInfo);
                     UXLegalAppGridView.BestFitColumns();
                  }
                  else if (inputArgs.WebSiteInfo is IdentityServerSiteInfo)
                  {
                     _IdentityServerWebSites.Add((IdentityServerSiteInfo)inputArgs.WebSiteInfo);
                     UXIdentityServerGridView.BestFitColumns();
                  }
                  else if (inputArgs.WebSiteInfo is WebOfficerClientSiteInfo)
                  {
                     _WebOfficerClientWebSites.Add((WebOfficerClientSiteInfo)inputArgs.WebSiteInfo);
                     UXWebOfficerClientGridView.BestFitColumns();
                  }
               }
            }
            //UXGridView.BestFitColumns();
            
         }, args);
         //backgroundWorker1.ReportProgress(_WebSites.Count);
      }

      private void Frm_WebSitesInfo_Load(object sender, EventArgs e)
      {
         LoadWebSitesInfo();
      }

      private void LoadWebSitesInfo()
      {
         _WebAPIWebSites.Clear();
         _LegalAppWebSites.Clear();
         _IdentityServerWebSites.Clear();

         UXWebAPIGrid.DataSource = _WebAPIWebSites;
         UXLegalAppGrid.DataSource = _LegalAppWebSites;
         UXIdentityServerGrid.DataSource = _IdentityServerWebSites;
         UXWebOfficerClientGrid.DataSource = _WebOfficerClientWebSites;

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
            UXWebAPIGridView.BestFitColumns();
            UXLegalAppGridView.BestFitColumns();
            UXIdentityServerGridView.BestFitColumns();
            UXWebOfficerClientGridView.BestFitColumns();

         }, e);
         
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         
      }

      private void Frm_WebSitesInfo_FormClosing(object sender, FormClosingEventArgs e)
      {
         _WebServerHelper.CancelLoadInfo();
      }

      private void View_DoubleClick(object sender, EventArgs e)
      {
         GridView view = (GridView)sender;
         Point pt = view.GridControl.PointToClient(Control.MousePosition);
         DoRowDoubleClick(view, pt);
      }

      private void UXGridView_DoubleClick(object sender, EventArgs e)
      {
         GridView view = (GridView)sender;
         Point pt = view.GridControl.PointToClient(Control.MousePosition);
         DoRowDoubleClick(view, pt);
      }

      private void DoRowDoubleClick(GridView view, Point pt)
      {
         GridHitInfo info = view.CalcHitInfo(pt);
         if (info.InRow || info.InRowCell)
         {
            string cellText = view.FocusedValue.ToString();
            string url = string.Empty;
            
            if (info.RowInfo.RowKey is WebSiteInfo)
            {
               WebSiteInfo webSiteInfo = (WebSiteInfo)info.RowInfo.RowKey;

               switch (info.Column.GetCaption())
               {
                  // Master row - Url column
                  case "Url":                  
                     if (webSiteInfo.WebSiteType.Equals(WebSiteTypeEnum.LegalApp))
                     {
                        System.Diagnostics.Process.Start("IEXPLORE.EXE", cellText);
                     }
                     else
                     {
                        Open(cellText);
                     }
                     break;
               }
            }
         }
      }

      protected void Open(string cmd)
      {
         try
         {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            process.StartInfo.FileName = cmd;            
            process.StartInfo.UseShellExecute = true;
            process.Start();
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }

      private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         //LoadWebSitesInfo();
      }
      private void LoadWebSites()
      {
         // if cache file is present load from cache
         //if (File.Exists(AppInstance.WebSitesCacheFile))
         //{
         //   TreeNodeSerializer<ConnectionInfo> ser = new TreeNodeSerializer<ConnectionInfo>();
         //   TreeNode<ConnectionInfo> tree = ser.Deserialize(AppInstance.CFsTreeCacheFile);
         //   SetDBsInfo(tree);
         //   PopulateDBTree(tree);
         //}
         //else
         //{
         //   btnAdd.Enabled = false;
         //   treeDatabases.ClearNodes();
         //   treeDatabases.Cursor = Cursors.WaitCursor;
         //   EnableUI(false);
         //   // get all databases from cf
         //   backgroundWorker1.RunWorkerAsync();
         //}
      }
   }

}