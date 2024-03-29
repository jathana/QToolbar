﻿using System;
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

         _WebAPIWebSites = new BindingList<WebAPISiteInfo>();
         _LegalAppWebSites = new BindingList<LegalAppSiteInfo>();
         _IdentityServerWebSites = new BindingList<IdentityServerSiteInfo>();
         _WebOfficerClientWebSites = new BindingList<WebOfficerClientSiteInfo>();

         // UXWebAPIGridView
         UXWebAPIGrid.ViewRegistered += UXGrid_ViewRegistered;
         UXWebAPIGrid.KeyDown += grid_KeyDown;
         UXWebAPIGrid.DataSource = _WebAPIWebSites;
         UXWebAPIGridView.OptionsBehavior.Editable = false;
         UXWebAPIGridView.OptionsView.RowAutoHeight = true;
         UXWebAPIGridView.OptionsPrint.PrintDetails = true;
         UXWebAPIGridView.OptionsPrint.ExpandAllDetails = true;
         UXWebAPIGridView.OptionsPrint.ExpandAllGroups = true;

         // UXLegalAppGridView
         UXLegalAppGrid.ViewRegistered += UXGrid_ViewRegistered;
         UXLegalAppGrid.KeyDown += grid_KeyDown;
         UXLegalAppGrid.DataSource = _LegalAppWebSites;
         UXLegalAppGridView.OptionsBehavior.Editable = false;
         UXLegalAppGridView.OptionsView.RowAutoHeight = true;
         UXLegalAppGridView.OptionsPrint.PrintDetails = true;
         UXLegalAppGridView.OptionsPrint.ExpandAllDetails = true;
         UXLegalAppGridView.OptionsPrint.ExpandAllGroups = true;

         // UXIdentityServerView
         UXIdentityServerGrid.ViewRegistered += UXGrid_ViewRegistered;
         UXIdentityServerGrid.KeyDown += grid_KeyDown;
         UXIdentityServerGrid.DataSource = _IdentityServerWebSites;
         UXIdentityServerGridView.OptionsBehavior.Editable = false;
         UXIdentityServerGridView.OptionsView.RowAutoHeight = true;
         UXIdentityServerGridView.OptionsPrint.PrintDetails = true;
         UXIdentityServerGridView.OptionsPrint.ExpandAllDetails = true;
         UXIdentityServerGridView.OptionsPrint.ExpandAllGroups = true;

         // UXWebOfficerClientView
         UXWebOfficerClientGrid.ViewRegistered += UXGrid_ViewRegistered;
         UXWebOfficerClientGrid.KeyDown += grid_KeyDown;
         UXWebOfficerClientGrid.DataSource = _WebOfficerClientWebSites;
         UXWebOfficerClientGridView.OptionsBehavior.Editable = false;
         UXWebOfficerClientGridView.OptionsView.RowAutoHeight = true;
         UXWebOfficerClientGridView.OptionsPrint.PrintDetails = true;
         UXWebOfficerClientGridView.OptionsPrint.ExpandAllDetails = true;
         UXWebOfficerClientGridView.OptionsPrint.ExpandAllGroups = true;

         _WebServerHelper = new WebServerHelper();
         _WebServerHelper.WebSiteInfoCollected += WebServerHelper_WebSiteInfoCollected;
         _WebServerHelper.ProcessingInfoCollected += _WebServerHelper_ProcessingInfoCollected;
         _SyncContext = SynchronizationContext.Current;
         
      }

      private void _WebServerHelper_ProcessingInfoCollected(object sender, ProcessingInfoCollectedEventArgs args)
      {
         _SyncContext.Post((input) =>
         {

            Text = $"Web Sites Info - Hosts: {args.CurrentHostLoadedNumber}/{args.HostsCount},  Host Sites: {args.HostCurrentSiteLoadedNumber}/{args.HostSitesCount}";

         }, args);
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
            
         }, args);
         
      }

      private void Frm_WebSitesInfo_Load(object sender, EventArgs e)
      {
         LoadWebSitesInfo();
      }

      #region Background Worker

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {
            btnReloadWebSitesInformation.Enabled = false;
            btnCancelLoadWebSitesInformation.Enabled = true;
            _WebServerHelper.LoadInfo(OptionsInstance.QCWebServers);
         }
         catch
         {
            btnReloadWebSitesInformation.Enabled = true;
            btnCancelLoadWebSitesInformation.Enabled = false;
         }
         
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         _SyncContext.Post((input) =>
         {
            UXWebAPIGridView.BestFitColumns();
            UXLegalAppGridView.BestFitColumns();
            UXIdentityServerGridView.BestFitColumns();
            UXWebOfficerClientGridView.BestFitColumns();
            if(backgroundWorker1.CancellationPending)
            {
               _WebServerHelper.CancelLoadInfo();
            }

         }, e);
         
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         btnReloadWebSitesInformation.Enabled = true;
         btnCancelLoadWebSitesInformation.Enabled = false;
      }
      #endregion

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
               string columnCaption = info.Column.GetCaption();
               if (columnCaption.ToLower().Contains("url"))
               {
                  // old legal opens with ie only upon silverlight
                  if (webSiteInfo.WebSiteType.Equals(WebSiteTypeEnum.LegalApp))
                  {
                     Process.Start("IEXPLORE.EXE", cellText);
                  }
                  else
                  {
                     Open(cellText);
                  }
               }
            }
            if (info.RowInfo.RowKey is WebAPIEnvironment)
            {
               WebAPIEnvironment webAPIEnvironment = (WebAPIEnvironment)info.RowInfo.RowKey;
               string columnCaption = info.Column.GetCaption();
               if (columnCaption.ToLower().Contains("url"))
               {
                  Open(cellText);
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

      #region Buttons Click Handlers
      private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         _SyncContext.Post((input) =>
         {
            ReloadWebSitesInfo();
         }, e);
      }
      #endregion


      private void LoadWebSitesInfo()
      {
            backgroundWorker1.RunWorkerAsync();
      }
      private void ReloadWebSitesInfo()
      {
         if (backgroundWorker1.IsBusy)
         {
            backgroundWorker1.CancelAsync();
         }
         else
         {
            ClearData();
            _WebServerHelper.ClearCache();
            _WebServerHelper.CancelLoadInfo();
            backgroundWorker1.RunWorkerAsync();
         }
      }
      private void ClearData()
      {
         _WebAPIWebSites.Clear();
         _LegalAppWebSites.Clear();
         _IdentityServerWebSites.Clear();
         _WebOfficerClientWebSites.Clear();
   }

      private void btnCancelLoadWebSitesInformation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         btnCancelLoadWebSitesInformation.Enabled = false;
         _WebServerHelper.CancelLoadInfo();
      }
   }

}