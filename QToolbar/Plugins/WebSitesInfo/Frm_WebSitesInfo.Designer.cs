namespace QToolbar.Plugins.WebSitesInfo
{
   partial class Frm_WebSitesInfo
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_WebSitesInfo));
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.UXTabControl = new DevExpress.XtraTab.XtraTabControl();
         this.tabWebAPI = new DevExpress.XtraTab.XtraTabPage();
         this.UXWebAPIGrid = new DevExpress.XtraGrid.GridControl();
         this.UXWebAPIGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.tabLegalApp = new DevExpress.XtraTab.XtraTabPage();
         this.UXLegalAppGrid = new DevExpress.XtraGrid.GridControl();
         this.UXLegalAppGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.tabIdentityServer = new DevExpress.XtraTab.XtraTabPage();
         this.UXIdentityServerGrid = new DevExpress.XtraGrid.GridControl();
         this.UXIdentityServerGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.tabWebOfficerClient = new DevExpress.XtraTab.XtraTabPage();
         this.UXWebOfficerClientGrid = new DevExpress.XtraGrid.GridControl();
         this.UXWebOfficerClientGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.bar1 = new DevExpress.XtraBars.Bar();
         this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
         this.bar3 = new DevExpress.XtraBars.Bar();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.UXTabControl)).BeginInit();
         this.UXTabControl.SuspendLayout();
         this.tabWebAPI.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.UXWebAPIGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXWebAPIGridView)).BeginInit();
         this.tabLegalApp.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.UXLegalAppGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXLegalAppGridView)).BeginInit();
         this.tabIdentityServer.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.UXIdentityServerGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXIdentityServerGridView)).BeginInit();
         this.tabWebOfficerClient.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.UXWebOfficerClientGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXWebOfficerClientGridView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.UXTabControl);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 24);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2857, 201, 554, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(806, 400);
         this.layoutControl1.TabIndex = 1;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // UXTabControl
         // 
         this.UXTabControl.Location = new System.Drawing.Point(12, 12);
         this.UXTabControl.Name = "UXTabControl";
         this.UXTabControl.SelectedTabPage = this.tabWebAPI;
         this.UXTabControl.Size = new System.Drawing.Size(782, 376);
         this.UXTabControl.TabIndex = 4;
         this.UXTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabWebAPI,
            this.tabLegalApp,
            this.tabIdentityServer,
            this.tabWebOfficerClient});
         // 
         // tabWebAPI
         // 
         this.tabWebAPI.Controls.Add(this.UXWebAPIGrid);
         this.tabWebAPI.Name = "tabWebAPI";
         this.tabWebAPI.Size = new System.Drawing.Size(780, 351);
         this.tabWebAPI.Text = "Web API";
         // 
         // UXWebAPIGrid
         // 
         this.UXWebAPIGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UXWebAPIGrid.Location = new System.Drawing.Point(0, 0);
         this.UXWebAPIGrid.MainView = this.UXWebAPIGridView;
         this.UXWebAPIGrid.Name = "UXWebAPIGrid";
         this.UXWebAPIGrid.Size = new System.Drawing.Size(780, 351);
         this.UXWebAPIGrid.TabIndex = 0;
         this.UXWebAPIGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UXWebAPIGridView});
         // 
         // UXWebAPIGridView
         // 
         this.UXWebAPIGridView.GridControl = this.UXWebAPIGrid;
         this.UXWebAPIGridView.Name = "UXWebAPIGridView";
         this.UXWebAPIGridView.DoubleClick += new System.EventHandler(this.UXGridView_DoubleClick);
         // 
         // tabLegalApp
         // 
         this.tabLegalApp.Controls.Add(this.UXLegalAppGrid);
         this.tabLegalApp.Name = "tabLegalApp";
         this.tabLegalApp.Size = new System.Drawing.Size(780, 351);
         this.tabLegalApp.Text = "Legal App";
         // 
         // UXLegalAppGrid
         // 
         this.UXLegalAppGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UXLegalAppGrid.Location = new System.Drawing.Point(0, 0);
         this.UXLegalAppGrid.MainView = this.UXLegalAppGridView;
         this.UXLegalAppGrid.Name = "UXLegalAppGrid";
         this.UXLegalAppGrid.Size = new System.Drawing.Size(780, 351);
         this.UXLegalAppGrid.TabIndex = 1;
         this.UXLegalAppGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UXLegalAppGridView});
         // 
         // UXLegalAppGridView
         // 
         this.UXLegalAppGridView.GridControl = this.UXLegalAppGrid;
         this.UXLegalAppGridView.Name = "UXLegalAppGridView";
         this.UXLegalAppGridView.DoubleClick += new System.EventHandler(this.View_DoubleClick);
         // 
         // tabIdentityServer
         // 
         this.tabIdentityServer.Controls.Add(this.UXIdentityServerGrid);
         this.tabIdentityServer.Name = "tabIdentityServer";
         this.tabIdentityServer.Size = new System.Drawing.Size(780, 351);
         this.tabIdentityServer.Text = "Identity Server";
         // 
         // UXIdentityServerGrid
         // 
         this.UXIdentityServerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UXIdentityServerGrid.Location = new System.Drawing.Point(0, 0);
         this.UXIdentityServerGrid.MainView = this.UXIdentityServerGridView;
         this.UXIdentityServerGrid.Name = "UXIdentityServerGrid";
         this.UXIdentityServerGrid.Size = new System.Drawing.Size(780, 351);
         this.UXIdentityServerGrid.TabIndex = 1;
         this.UXIdentityServerGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UXIdentityServerGridView});
         // 
         // UXIdentityServerGridView
         // 
         this.UXIdentityServerGridView.GridControl = this.UXIdentityServerGrid;
         this.UXIdentityServerGridView.Name = "UXIdentityServerGridView";
         this.UXIdentityServerGridView.DoubleClick += new System.EventHandler(this.View_DoubleClick);
         // 
         // tabWebOfficerClient
         // 
         this.tabWebOfficerClient.Controls.Add(this.UXWebOfficerClientGrid);
         this.tabWebOfficerClient.Name = "tabWebOfficerClient";
         this.tabWebOfficerClient.Size = new System.Drawing.Size(780, 351);
         this.tabWebOfficerClient.Text = "Web Officer Client";
         // 
         // UXWebOfficerClientGrid
         // 
         this.UXWebOfficerClientGrid.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UXWebOfficerClientGrid.Location = new System.Drawing.Point(0, 0);
         this.UXWebOfficerClientGrid.MainView = this.UXWebOfficerClientGridView;
         this.UXWebOfficerClientGrid.Name = "UXWebOfficerClientGrid";
         this.UXWebOfficerClientGrid.Size = new System.Drawing.Size(780, 351);
         this.UXWebOfficerClientGrid.TabIndex = 1;
         this.UXWebOfficerClientGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UXWebOfficerClientGridView});
         // 
         // UXWebOfficerClientGridView
         // 
         this.UXWebOfficerClientGridView.GridControl = this.UXWebOfficerClientGrid;
         this.UXWebOfficerClientGridView.Name = "UXWebOfficerClientGridView";
         this.UXWebOfficerClientGridView.DoubleClick += new System.EventHandler(this.View_DoubleClick);
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(806, 400);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.UXTabControl;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(786, 380);
         this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem1.TextVisible = false;
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.WorkerReportsProgress = true;
         this.backgroundWorker1.WorkerSupportsCancellation = true;
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
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
            this.barButtonItem1});
         this.barManager1.MaxItemId = 1;
         this.barManager1.StatusBar = this.bar3;
         // 
         // bar1
         // 
         this.bar1.BarName = "Tools";
         this.bar1.DockCol = 0;
         this.bar1.DockRow = 0;
         this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
         this.bar1.Text = "Tools";
         // 
         // barButtonItem1
         // 
         this.barButtonItem1.Id = 0;
         this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
         this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
         this.barButtonItem1.Name = "barButtonItem1";
         this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
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
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 424);
         this.barDockControlBottom.Manager = this.barManager1;
         this.barDockControlBottom.Size = new System.Drawing.Size(806, 20);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
         this.barDockControlLeft.Manager = this.barManager1;
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 400);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(806, 24);
         this.barDockControlRight.Manager = this.barManager1;
         this.barDockControlRight.Size = new System.Drawing.Size(0, 400);
         // 
         // Frm_WebSitesInfo
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(806, 444);
         this.Controls.Add(this.layoutControl1);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "Frm_WebSitesInfo";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Web Sites Info";
         this.TopMost = true;
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_WebSitesInfo_FormClosing);
         this.Load += new System.EventHandler(this.Frm_WebSitesInfo_Load);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.UXTabControl)).EndInit();
         this.UXTabControl.ResumeLayout(false);
         this.tabWebAPI.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.UXWebAPIGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXWebAPIGridView)).EndInit();
         this.tabLegalApp.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.UXLegalAppGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXLegalAppGridView)).EndInit();
         this.tabIdentityServer.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.UXIdentityServerGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXIdentityServerGridView)).EndInit();
         this.tabWebOfficerClient.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.UXWebOfficerClientGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXWebOfficerClientGridView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraTab.XtraTabControl UXTabControl;
      private DevExpress.XtraTab.XtraTabPage tabWebAPI;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraGrid.GridControl UXWebAPIGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView UXWebAPIGridView;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.Bar bar1;
      private DevExpress.XtraBars.Bar bar3;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraBars.BarButtonItem barButtonItem1;
      private DevExpress.XtraTab.XtraTabPage tabLegalApp;
      private DevExpress.XtraGrid.GridControl UXLegalAppGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView UXLegalAppGridView;
      private DevExpress.XtraTab.XtraTabPage tabIdentityServer;
      private DevExpress.XtraGrid.GridControl UXIdentityServerGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView UXIdentityServerGridView;
      private DevExpress.XtraTab.XtraTabPage tabWebOfficerClient;
      private DevExpress.XtraGrid.GridControl UXWebOfficerClientGrid;
      private DevExpress.XtraGrid.Views.Grid.GridView UXWebOfficerClientGridView;
   }
}