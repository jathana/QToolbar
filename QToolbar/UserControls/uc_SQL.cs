using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using QToolbar.Forms;
using System.Diagnostics;
using DevExpress.XtraPrinting.Preview.Native;

namespace QToolbar
{
   public partial class uc_SQL : DevExpress.XtraEditors.XtraUserControl
   {
      private const int MINIMUM_GRID_HEIGHT= 200;
      private List<GridControl> _Grids;
      private List<SplitterControl> _Splitters;
      private StringBuilder _Messages;

      private List<ConnectionInfo> _SelectedDatabases;


      public string QueryName { get; set; }
      public string Server { get; set; }
      public string Database { get; set; }
      public string Query { get; set; }
      public bool QueryRunImmediate { get; set; }
      public string ConnectionString
      {
         get
         {
            return Utils.GetConnectionString(Server, Database);
         }
      }

      public List<ConnectionInfo> Databases { get; set; }
      
      public List<ConnectionInfo> SelectedDatabases
      {
         get 
         { 
            return _SelectedDatabases; 
         }
         set 
         { 
            _SelectedDatabases = value; 
         }
      }
      public uc_SQL()
      {
         InitializeComponent();
         backgroundWorker1.WorkerSupportsCancellation = true;
         backgroundWorker1.WorkerReportsProgress = true;
         _Messages = new StringBuilder();
         _Grids = new List<GridControl>();
         _Splitters = new List<SplitterControl>();
         _SelectedDatabases = new List<ConnectionInfo>();
      }

      public void Initialize()
      {
         txtSQL.Text = Query;
         _SelectedDatabases.Clear();
         _SelectedDatabases.Add(new ConnectionInfo()
         {
            Server = Server,
            Database = Database
         });
      }

      public void Run()
      {
         try
         {
            btnRun.Enabled = false;
            progressPanel1.Description = $"Executing {QueryName}...";
            //progressPanel1.Visible = true;
            lgrProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            ClearGrids();
            ClearSplitters();
            
            backgroundWorker1.RunWorkerAsync(txtSQL.Text);
         }
         catch(Exception ex)
         {
            //progressPanel1.Visible = false;
            lgrProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            btnRun.Enabled = true;
         }

      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         try
         {  DataSet dataset = new DataSet();


            int tableIndex = 0;
            foreach (var database in _SelectedDatabases)
            {
               string connectionString = Utils.GetConnectionString(database.Server, database.Database);
               using (SqlConnection con = new SqlConnection(connectionString))
               {
                  _Messages.Clear();
                  con.FireInfoMessageEventOnUserErrors = true;
                  con.InfoMessage += Con_InfoMessage;

                  string batch = (string)e.Argument;

                  // Split batch by "GO" (case-insensitive, must be on its own line)
                  var statements = System.Text.RegularExpressions.Regex
                      .Split(batch, @"^\s*GO\s*$", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                      .Where(s => !string.IsNullOrWhiteSpace(s));

                  con.Open();
                  
                  foreach (var sql in statements)
                  {
                     using (SqlCommand cmd = new SqlCommand(sql, con))
                     {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                           do
                           {
                              string tableName = $"{tableIndex}.{database.Database} ";
                              DataTable dt = new DataTable(tableName);
                              //DataTable dt = new DataTable("Table" + tableIndex);

                              dt.Load(reader);
                              dataset.Tables.Add(dt);
                              tableIndex++;
                           } while (!reader.IsClosed && reader.NextResult());
                        }
                     }
                  }
               }
            }
            e.Result = dataset;
            
         }
         catch (Exception ex)
         {
            backgroundWorker1.ReportProgress(100, $"Server:{Server}\r\nDatabase:{Database}\r\n{Query}\r\nFailed, {ex.Message}.");
            e.Result = ex;
         }
      }

      private void Con_InfoMessage(object sender, SqlInfoMessageEventArgs e)
      {
         _Messages.AppendLine(e.Message);
      }

      
      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Result != null)
         {
            if (e.Result is DataSet)
            {
               DataSet ds = (DataSet)e.Result;
               LayoutGrids(ds);
            }
            else if (e.Result is Exception)
            {
               this.Focus();
               Exception ex = (Exception)e.Result;
               XtraMessageBox.Show(ex.Message);
            }
         }
         lgrProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
         memMessages.Text = _Messages.ToString();
         btnRun.Enabled = true;
      }
      

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
        
      }

      private void btnRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         Run();
         
      }

      private void ClearGrids()
      {
         for(int i=0;i<_Grids.Count;i++)
         {
            //pageResults.Controls.Remove(_Grids[i]);
            xtraScrollableControl1.Controls.Remove(_Grids[i]);
            _Grids[i] = null;
         }
         _Grids.Clear();
      }

      private void ClearSplitters()
      {
         for (int i = 0; i < _Splitters.Count; i++)
         {
            //pageResults.Controls.Remove(_Splitters[i]);
            xtraScrollableControl1.Controls.Remove(_Splitters[i]);
            _Splitters[i] = null;
         }
         _Splitters.Clear();
      }

      private void LayoutGrids(DataSet ds)
      {
         if(ds!=null)
         {
            if(ds.Tables.Count > 0)
            {

               DataTable currentTable;
               for (int i=0; i < ds.Tables.Count; i++)
               {
                  // add grid
                  GridControl grid = CreateGrid();
                  _Grids.Add(grid);
                  xtraScrollableControl1.Controls.Add(grid);
                  grid.Dock = i == 0 ? grid.Dock = DockStyle.Fill : grid.Dock = DockStyle.Top;

                  currentTable = ds.Tables[ds.Tables.Count - 1 - i];
                  grid.DataSource = currentTable;
                  ((GridView)grid.DefaultView).BestFitColumns();
                  grid.Views[0].ViewCaption = currentTable.TableName;

                  // add splitter
                  if (i < ds.Tables.Count - 1)
                  {
                     SplitterControl splitter = new SplitterControl();
                     xtraScrollableControl1.Controls.Add(splitter);
                     splitter.Dock = DockStyle.Top;
                     _Splitters.Add(splitter);
                  }

               }
            }
         }
      }

      private GridControl CreateGrid()
      {
         GridControl retVal = new GridControl();
         GridView gridView = new GridView();
         retVal.MainView = gridView;
         retVal.Name = "retVal";
         retVal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {gridView});
         retVal.KeyDown += grid_KeyDown;
         
         // 
         // gridView1
         // 
         gridView.GridControl = retVal;
         gridView.Name = "gridView1";
         gridView.OptionsBehavior.Editable = false;
         gridView.OptionsView.ColumnAutoWidth = false;
         gridView.OptionsView.ShowGroupPanel = false;
         gridView.OptionsView.ShowViewCaption = true;
         gridView.Appearance.ViewCaption.Options.UseFont = true;

         gridView.IndicatorWidth = 40;
         
         gridView.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;

         return retVal;
      }

      private void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
      {
         if (e.RowHandle >= 0)
            e.Info.DisplayText = e.RowHandle.ToString();
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

      private void btnSelectDatabasesToRun_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         Frm_SelectDatabases frm = new Frm_SelectDatabases();
         frm.ShowForm(Databases, _SelectedDatabases);

         frm.DatabasesSelected += (s, selectedDatabases) =>
         {
            _SelectedDatabases = selectedDatabases;
                        
         };
         
      }

      private void pageResults_SizeChanged(object sender, EventArgs e)
      {
         ResizeGrids();
      }

      private void ResizeGrids()
      {
         if (_Grids != null && _Grids.Count > 0)
         {
            // find layout info
            int splittersTotalHeight = _Splitters.Sum(s => s.Height);
            int gridHeight = Math.Max(MINIMUM_GRID_HEIGHT, ((int)pageResults.Height - splittersTotalHeight) / _Grids.Count);
            xtraScrollableControl1.Height = (gridHeight) * _Grids.Count + splittersTotalHeight;
            for (int i = 0; i < _Grids.Count; i++)
            {
               // resize grid
               GridControl grid = _Grids[i];
               grid.Height = gridHeight;
            }
         }
      }


   }
}
