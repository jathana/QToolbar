namespace QToolbar.Plugins.TDSHelper
{
   partial class Frm_TDSHelper
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TDSHelper));
         DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
         DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
         DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.UXFieldsGrid = new DevExpress.XtraGrid.GridControl();
         this.UXWebAPIGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.bar1 = new DevExpress.XtraBars.Bar();
         this.btnReloadWebSitesInformation = new DevExpress.XtraBars.BarButtonItem();
         this.btnCancelLoadWebSitesInformation = new DevExpress.XtraBars.BarButtonItem();
         this.bar3 = new DevExpress.XtraBars.Bar();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         this.UXViewsGrid = new DevExpress.XtraGrid.GridControl();
         this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.UXDatasourcesGrid = new DevExpress.XtraGrid.GridControl();
         this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.UXFieldsTabPage = new DevExpress.XtraTab.XtraTabPage();
         this.UXViewsTabPage = new DevExpress.XtraTab.XtraTabPage();
         this.UXDatasourcesTabPage = new DevExpress.XtraTab.XtraTabPage();
         this.USDuisTabPage = new DevExpress.XtraTab.XtraTabPage();
         this.UXDUIsGrid = new DevExpress.XtraGrid.GridControl();
         this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.UXFieldsGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXWebAPIGridView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXViewsGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXDatasourcesGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
         this.xtraTabControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         this.UXFieldsTabPage.SuspendLayout();
         this.UXViewsTabPage.SuspendLayout();
         this.UXDatasourcesTabPage.SuspendLayout();
         this.USDuisTabPage.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.UXDUIsGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.xtraTabControl1);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 24);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2447, 539, 554, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(806, 442);
         this.layoutControl1.TabIndex = 1;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // UXFieldsGrid
         // 
         this.UXFieldsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UXFieldsGrid.Location = new System.Drawing.Point(0, 0);
         this.UXFieldsGrid.MainView = this.UXWebAPIGridView;
         this.UXFieldsGrid.Name = "UXFieldsGrid";
         this.UXFieldsGrid.Size = new System.Drawing.Size(780, 393);
         this.UXFieldsGrid.TabIndex = 0;
         this.UXFieldsGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UXWebAPIGridView});
         // 
         // UXWebAPIGridView
         // 
         this.UXWebAPIGridView.GridControl = this.UXFieldsGrid;
         this.UXWebAPIGridView.Name = "UXWebAPIGridView";
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(806, 442);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.WorkerReportsProgress = true;
         this.backgroundWorker1.WorkerSupportsCancellation = true;
         // 
         // barManager1
         // 
         this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
         this.barManager1.DockControls.Add(this.barDockControlTop);
         this.barManager1.DockControls.Add(this.barDockControlBottom);
         this.barManager1.DockControls.Add(this.barDockControlLeft);
         this.barManager1.DockControls.Add(this.barDockControlRight);
         this.barManager1.Form = this;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnReloadWebSitesInformation,
            this.btnCancelLoadWebSitesInformation});
         this.barManager1.MaxItemId = 2;
         this.barManager1.StatusBar = this.bar3;
         // 
         // bar1
         // 
         this.bar1.BarName = "Tools";
         this.bar1.DockCol = 0;
         this.bar1.DockRow = 0;
         this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnReloadWebSitesInformation),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelLoadWebSitesInformation)});
         this.bar1.Text = "Tools";
         // 
         // btnReloadWebSitesInformation
         // 
         this.btnReloadWebSitesInformation.Id = 0;
         this.btnReloadWebSitesInformation.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadWebSitesInformation.ImageOptions.Image")));
         this.btnReloadWebSitesInformation.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnReloadWebSitesInformation.ImageOptions.LargeImage")));
         this.btnReloadWebSitesInformation.Name = "btnReloadWebSitesInformation";
         toolTipItem3.Text = "Reloads Web Sites Information";
         superToolTip3.Items.Add(toolTipItem3);
         this.btnReloadWebSitesInformation.SuperTip = superToolTip3;
         // 
         // btnCancelLoadWebSitesInformation
         // 
         this.btnCancelLoadWebSitesInformation.Id = 1;
         this.btnCancelLoadWebSitesInformation.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelLoadWebSitesInformation.ImageOptions.Image")));
         this.btnCancelLoadWebSitesInformation.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCancelLoadWebSitesInformation.ImageOptions.LargeImage")));
         this.btnCancelLoadWebSitesInformation.Name = "btnCancelLoadWebSitesInformation";
         toolTipItem4.Text = "Cancels Loading WebSites Information";
         superToolTip4.Items.Add(toolTipItem4);
         this.btnCancelLoadWebSitesInformation.SuperTip = superToolTip4;
         // 
         // bar3
         // 
         this.bar3.BarName = "Status bar";
         this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
         this.bar3.DockCol = 0;
         this.bar3.DockRow = 0;
         this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
         this.bar3.OptionsBar.AllowQuickCustomization = false;
         this.bar3.OptionsBar.DrawDragBorder = false;
         this.bar3.OptionsBar.UseWholeRow = true;
         this.bar3.Text = "Status bar";
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Manager = this.barManager1;
         this.barDockControlTop.Size = new System.Drawing.Size(806, 24);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 466);
         this.barDockControlBottom.Manager = this.barManager1;
         this.barDockControlBottom.Size = new System.Drawing.Size(806, 20);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
         this.barDockControlLeft.Manager = this.barManager1;
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 442);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(806, 24);
         this.barDockControlRight.Manager = this.barManager1;
         this.barDockControlRight.Size = new System.Drawing.Size(0, 442);
         // 
         // UXViewsGrid
         // 
         this.UXViewsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UXViewsGrid.Location = new System.Drawing.Point(0, 0);
         this.UXViewsGrid.MainView = this.gridView1;
         this.UXViewsGrid.Name = "UXViewsGrid";
         this.UXViewsGrid.Size = new System.Drawing.Size(780, 393);
         this.UXViewsGrid.TabIndex = 0;
         this.UXViewsGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
         // 
         // gridView1
         // 
         this.gridView1.GridControl = this.UXViewsGrid;
         this.gridView1.Name = "gridView1";
         // 
         // UXDatasourcesGrid
         // 
         this.UXDatasourcesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UXDatasourcesGrid.Location = new System.Drawing.Point(0, 0);
         this.UXDatasourcesGrid.MainView = this.gridView2;
         this.UXDatasourcesGrid.Name = "UXDatasourcesGrid";
         this.UXDatasourcesGrid.Size = new System.Drawing.Size(780, 393);
         this.UXDatasourcesGrid.TabIndex = 0;
         this.UXDatasourcesGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
         // 
         // gridView2
         // 
         this.gridView2.GridControl = this.UXDatasourcesGrid;
         this.gridView2.Name = "gridView2";
         // 
         // xtraTabControl1
         // 
         this.xtraTabControl1.Location = new System.Drawing.Point(12, 12);
         this.xtraTabControl1.Name = "xtraTabControl1";
         this.xtraTabControl1.SelectedTabPage = this.UXFieldsTabPage;
         this.xtraTabControl1.Size = new System.Drawing.Size(782, 418);
         this.xtraTabControl1.TabIndex = 4;
         this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.UXFieldsTabPage,
            this.UXViewsTabPage,
            this.UXDatasourcesTabPage,
            this.USDuisTabPage});
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.xtraTabControl1;
         this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(786, 422);
         this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem4.TextVisible = false;
         // 
         // UXFieldsTabPage
         // 
         this.UXFieldsTabPage.Controls.Add(this.UXFieldsGrid);
         this.UXFieldsTabPage.Name = "UXFieldsTabPage";
         this.UXFieldsTabPage.Size = new System.Drawing.Size(780, 393);
         this.UXFieldsTabPage.Text = "Fields";
         // 
         // UXViewsTabPage
         // 
         this.UXViewsTabPage.Controls.Add(this.UXViewsGrid);
         this.UXViewsTabPage.Name = "UXViewsTabPage";
         this.UXViewsTabPage.Size = new System.Drawing.Size(780, 393);
         this.UXViewsTabPage.Text = "Views";
         // 
         // UXDatasourcesTabPage
         // 
         this.UXDatasourcesTabPage.Controls.Add(this.UXDatasourcesGrid);
         this.UXDatasourcesTabPage.Name = "UXDatasourcesTabPage";
         this.UXDatasourcesTabPage.Size = new System.Drawing.Size(780, 393);
         this.UXDatasourcesTabPage.Text = "Datasources";
         // 
         // USDuisTabPage
         // 
         this.USDuisTabPage.Controls.Add(this.UXDUIsGrid);
         this.USDuisTabPage.Name = "USDuisTabPage";
         this.USDuisTabPage.Size = new System.Drawing.Size(780, 393);
         this.USDuisTabPage.Text = "DUIs";
         // 
         // UXDUIsGrid
         // 
         this.UXDUIsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UXDUIsGrid.Location = new System.Drawing.Point(0, 0);
         this.UXDUIsGrid.MainView = this.gridView3;
         this.UXDUIsGrid.Name = "UXDUIsGrid";
         this.UXDUIsGrid.Size = new System.Drawing.Size(780, 393);
         this.UXDUIsGrid.TabIndex = 1;
         this.UXDUIsGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
         // 
         // gridView3
         // 
         this.gridView3.GridControl = this.UXDUIsGrid;
         this.gridView3.Name = "gridView3";
         // 
         // Frm_TDSHelper
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(806, 486);
         this.Controls.Add(this.layoutControl1);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "Frm_TDSHelper";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "TDS Helper";
         this.TopMost = true;
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.UXFieldsGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXWebAPIGridView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXViewsGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXDatasourcesGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
         this.xtraTabControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         this.UXFieldsTabPage.ResumeLayout(false);
         this.UXViewsTabPage.ResumeLayout(false);
         this.UXDatasourcesTabPage.ResumeLayout(false);
         this.USDuisTabPage.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.UXDUIsGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraGrid.GridControl UXFieldsGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView UXWebAPIGridView;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.Bar bar1;
      private DevExpress.XtraBars.Bar bar3;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraBars.BarButtonItem btnReloadWebSitesInformation;
      private DevExpress.XtraBars.BarButtonItem btnCancelLoadWebSitesInformation;
      private DevExpress.XtraGrid.GridControl UXViewsGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
      private DevExpress.XtraGrid.GridControl UXDatasourcesGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
      private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
      private DevExpress.XtraTab.XtraTabPage UXFieldsTabPage;
      private DevExpress.XtraTab.XtraTabPage UXViewsTabPage;
      private DevExpress.XtraTab.XtraTabPage UXDatasourcesTabPage;
      private DevExpress.XtraTab.XtraTabPage USDuisTabPage;
      private DevExpress.XtraGrid.GridControl UXDUIsGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
   }
}