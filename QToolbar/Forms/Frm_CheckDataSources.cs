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
using System.Data.SqlClient;
using System.IO;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

namespace QToolbar.Tools
{
   public partial class Frm_CheckDatasources : DevExpress.XtraEditors.XtraForm
   {
      private ConnectionInfo _Info;
      private StringBuilder connectionMsg = new StringBuilder();

      public Frm_CheckDatasources()
      {
         InitializeComponent();
      }


      public void Show(ConnectionInfo info)
      {
         _Info = info;         
         Text = $"Check datasources - {_Info.Server} . {_Info.Database}";
         Show();
         btnRun_ItemClick(null,null);
      }
            

      private void btnRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         try
         {

            btnRun.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
         }
         catch (Exception ex)
         {
            //progressPanel1.Visible = false;
            btnRun.Enabled = true;
         }
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         using (SqlConnection con = new SqlConnection($"{Utils.GetConnectionString(_Info.Server, _Info.Database)};Connection Timeout=1000"))
         {
            try
            {
               
               con.Open();

               DataSet fieldsByDefinition = GetDatasourcesColumnsByDefinition(con);

               StringBuilder builder = new StringBuilder("DECLARE @tmp_table TABLE(DSRC_CODE INT,DSF_FIELDNAME NVARCHAR(100));");
               foreach(DataRow row in fieldsByDefinition.Tables[0].Rows)
               {
                  builder.AppendLine($"INSERT INTO @tmp_table values({row.Field<int>("DSRC_CODE")},N'{row.Field<string>("name")}');");
               }

               builder.AppendLine(@"SELECT * 
                                    FROM TLK_DATASOURCES_FIELDS DSF WITH(NOLOCK)
                                    LEFT JOIN @tmp_table T ON T.DSRC_CODE=DSF.DSRC_CODE AND DSF.DSF_FIELDNAME=T.DSF_FIELDNAME
                                    WHERE DSF.DSF_ACTIVE=1
                                    AND T.DSF_FIELDNAME IS NULL
                                    ORDER BY DSF.DSRC_CODE, DSF.DSF_FIELDNAME");

               SqlDataAdapter adapter = new SqlDataAdapter(builder.ToString(), con);

               DataSet result = new DataSet();

               adapter.Fill(result);

               e.Result = result;
            }
            catch (Exception ex)
            {
               e.Result = ex;
            }
            finally
            {
               con.Close();
            }
         }
      }

      private DataSet GetDatasourcesColumnsByDefinition(SqlConnection con)
      {

         StringBuilder builder = new StringBuilder();
         builder.Append("SELECT DSRC_DEFINITION,DSRC_CODE  FROM TLK_DATASOURCES WITH(NOLOCK)");
         SqlDataAdapter adapter = new SqlDataAdapter(builder.ToString(), con);

         DataSet result = new DataSet();

         DataSet dataset = new DataSet();
         adapter.Fill(dataset);
         int i = 0;
         foreach (DataRow dsrcRow in dataset.Tables[0].Rows)
         {
            string sql = dsrcRow.Field<string>("DSRC_DEFINITION");
            SqlCommand sqlCmd = new SqlCommand("sp_describe_first_result_set", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = sqlCmd.Parameters.Add("@def", SqlDbType.NVarChar);
            parameter.Value = dsrcRow.Field<string>("DSRC_DEFINITION");

            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = sqlCmd;
            DataSet dataset2 = new DataSet();
            try
            {
               adapter2.Fill(dataset2);
               DataColumn col = new DataColumn("DSRC_CODE", typeof(int));
               col.DefaultValue = dsrcRow.Field<int>("DSRC_CODE");
               dataset2.Tables[0].Columns.Add(col);
               dataset2.AcceptChanges();
               result.Merge(dataset2);
               result.AcceptChanges();
            }
            catch
            {

            }
            finally
            {
               sqlCmd.Dispose();
               adapter2.Dispose();
            }
            i++;
            backgroundWorker1.ReportProgress(i);
         }
         return result;
      }
         

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         if (e.Result != null)
         {
            if (e.Result is DataSet)
            {
               DataSet ds = (DataSet)e.Result;
               gridView1.Columns.Clear();
               gridControl1.DataSource = ds.Tables[0];
               //grdInstallations.DataSource = ds.Tables[1];
            }
            else if (e.Result is Exception)
            {
               this.Focus();
               Exception ex = (Exception)e.Result;
               XtraMessageBox.Show(ex.Message);
            }
         }
         btnRun.Enabled = true;
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         this.Text = $"Check Datasources - {e.ProgressPercentage}";
      }
   }
}