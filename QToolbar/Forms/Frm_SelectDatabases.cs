using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QToolbar.Forms
{
   public partial class Frm_SelectDatabases : DevExpress.XtraEditors.XtraForm
   {
      private List<ConnectionInfo> _Databases;
      private List<ConnectionInfo> _SelectedDatabases;

      public Frm_SelectDatabases()
      {
         InitializeComponent();
      }

      public event EventHandler<List<ConnectionInfo>> DatabasesSelected;

      public List<ConnectionInfo> Databases 
      {
         get
         {
            return _Databases;
         }
         set
         {
            _Databases = value;
         }
      }


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

      public void ShowForm(List<ConnectionInfo> databases, List<ConnectionInfo> selectedDatabases)
      {
         _Databases = databases;
         _SelectedDatabases = selectedDatabases;
         PopulateDatabases(_Databases);
         Show();
      }

      private void PopulateDatabases(List<ConnectionInfo> databases)
      {
         if (databases != null)
         {
            // Convert List<ConnectionInfo> to DataTable for grid binding
            var dt = new DataTable();
            dt.Columns.Add("IsSelected", typeof(bool));
            dt.Columns.Add("Database", typeof(string));
            dt.Columns.Add("Server", typeof(string));
            foreach (var db in Databases)
            {
               dt.Rows.Add(SelectedDatabases.Any(d=>d.Database==db.Database && d.Server==db.Server), db.Database, db.Server);
            }
            grdDatabases.DataSource = dt;
         }
      }

      private void btnApply_Click(object sender, EventArgs e)
      {

         _SelectedDatabases = _SelectedDatabases == null ? new List<ConnectionInfo>() : _SelectedDatabases;
         _SelectedDatabases.Clear();

         var dt = grdDatabases.DataSource as DataTable;
         if (dt != null && _Databases != null)
         {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               var row = dt.Rows[i];
               if (row.Field<bool>("IsSelected"))
               {
                  // Find the matching ConnectionInfo by Database and Server
                  var db = _Databases.FirstOrDefault(d =>
                      d.Database == row.Field<string>("Database") &&
                      d.Server == row.Field<string>("Server"));

                  var selectedDB = _SelectedDatabases.FirstOrDefault(d =>
                      d.Database == row.Field<string>("Database") &&
                      d.Server == row.Field<string>("Server"));

                  if (db != null && selectedDB == null)
                  {
                     _SelectedDatabases.Add(db);
                  }
               }
            }
         }

         // Raise the event
         DatabasesSelected?.Invoke(this, _SelectedDatabases);

         this.Close();
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}