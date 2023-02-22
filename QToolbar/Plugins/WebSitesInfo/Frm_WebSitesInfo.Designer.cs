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
         this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
         this.UXTabControl = new DevExpress.XtraTab.XtraTabControl();
         this.tabWebAPI = new DevExpress.XtraTab.XtraTabPage();
         this.gridWebAPI = new DevExpress.XtraGrid.GridControl();
         this.UXGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
         this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.UXTabControl)).BeginInit();
         this.UXTabControl.SuspendLayout();
         this.tabWebAPI.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.gridWebAPI)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXGridView)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
         this.SuspendLayout();
         // 
         // layoutControl1
         // 
         this.layoutControl1.Controls.Add(this.UXTabControl);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2857, 201, 554, 400);
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(586, 368);
         this.layoutControl1.TabIndex = 1;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // UXTabControl
         // 
         this.UXTabControl.Location = new System.Drawing.Point(12, 12);
         this.UXTabControl.Name = "UXTabControl";
         this.UXTabControl.SelectedTabPage = this.tabWebAPI;
         this.UXTabControl.Size = new System.Drawing.Size(562, 344);
         this.UXTabControl.TabIndex = 4;
         this.UXTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabWebAPI,
            this.xtraTabPage2});
         // 
         // tabWebAPI
         // 
         this.tabWebAPI.Controls.Add(this.gridWebAPI);
         this.tabWebAPI.Name = "tabWebAPI";
         this.tabWebAPI.Size = new System.Drawing.Size(560, 319);
         this.tabWebAPI.Text = "Web API";
         // 
         // gridWebAPI
         // 
         this.gridWebAPI.Dock = System.Windows.Forms.DockStyle.Fill;
         this.gridWebAPI.Location = new System.Drawing.Point(0, 0);
         this.gridWebAPI.MainView = this.UXGridView;
         this.gridWebAPI.Name = "gridWebAPI";
         this.gridWebAPI.Size = new System.Drawing.Size(560, 319);
         this.gridWebAPI.TabIndex = 0;
         this.gridWebAPI.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UXGridView});
         // 
         // UXGridView
         // 
         this.UXGridView.GridControl = this.gridWebAPI;
         this.UXGridView.Name = "UXGridView";
         this.UXGridView.DoubleClick += new System.EventHandler(this.UXGridView_DoubleClick);
         // 
         // xtraTabPage2
         // 
         this.xtraTabPage2.Name = "xtraTabPage2";
         this.xtraTabPage2.Size = new System.Drawing.Size(560, 319);
         this.xtraTabPage2.Text = "xtraTabPage2";
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
         this.layoutControlGroup1.Name = "Root";
         this.layoutControlGroup1.Size = new System.Drawing.Size(586, 368);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItem1
         // 
         this.layoutControlItem1.Control = this.UXTabControl;
         this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem1.Name = "layoutControlItem1";
         this.layoutControlItem1.Size = new System.Drawing.Size(566, 348);
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
         // Frm_WebSitesInfo
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(586, 368);
         this.Controls.Add(this.layoutControl1);
         this.MaximizeBox = false;
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
         ((System.ComponentModel.ISupportInitialize)(this.gridWebAPI)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.UXGridView)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion
      private DevExpress.XtraLayout.LayoutControl layoutControl1;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraTab.XtraTabControl UXTabControl;
      private DevExpress.XtraTab.XtraTabPage tabWebAPI;
      private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
      private DevExpress.XtraGrid.GridControl gridWebAPI;
      private DevExpress.XtraGrid.Views.Grid.GridView UXGridView;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
   }
}