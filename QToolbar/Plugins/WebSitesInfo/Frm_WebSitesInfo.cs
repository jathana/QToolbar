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
      private BindingList<WebSiteInfo> _WebSites;

      public Frm_WebSitesInfo()
      {
         InitializeComponent();


         UXGridView.OptionsBehavior.Editable = false;
         UXGridView.OptionsView.RowAutoHeight = true;
         gridWebAPI.ViewRegistered += UXGrid_ViewRegistered;
         gridWebAPI.KeyDown += grid_KeyDown;

         UXGridView.OptionsPrint.PrintDetails = true;
         UXGridView.OptionsPrint.ExpandAllDetails = true;
         UXGridView.OptionsPrint.ExpandAllGroups = true;

         _WebServerHelper = new WebServerHelper();
         _WebServerHelper.WebSiteInfoCollected += WebServerHelper_WebSiteInfoCollected;
         _SyncContext = SynchronizationContext.Current;
         _WebSites = new BindingList<WebSiteInfo>();

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
                  _WebSites.Add((WebSiteInfo)inputArgs.WebSiteInfo);
               }
            }
            //UXGridView.BestFitColumns();
            UXGridView.BestFitColumns();
         }, args);
         backgroundWorker1.ReportProgress(_WebSites.Count);
      }

      private void Frm_WebSitesInfo_Load(object sender, EventArgs e)
      {
         LoadWebSitesInfo();
      }

      private void LoadWebSitesInfo()
      {
         _WebSites.Clear();
         gridWebAPI.DataSource = _WebSites;

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
            
            if (info.RowInfo.RowKey is SimpleProperty)
            {
               SimpleProperty simpleProperty = (SimpleProperty)info.RowInfo.RowKey;
               switch (info.Column.GetCaption())
               { 
                  // Child row - Properties, Value column
                  case "Value":
                        Open(cellText);
                     break;
               }
            }
            else if (info.RowInfo.RowKey is WebSiteInfo)
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