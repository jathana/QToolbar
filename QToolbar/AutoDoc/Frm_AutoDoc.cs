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
using static QToolbar.AutoDoc.AutoDocModel;

namespace QToolbar.AutoDoc
{
   public partial class Frm_AutoDoc : DevExpress.XtraEditors.XtraForm
   {

      private ConnectionInfo _Info;
      private List<ConnectionInfo> _DBs;
      private Dictionary<string, Tuple<DataTable, string>> _Data = new Dictionary<string, Tuple<DataTable, string>>();

      private AutoDocModel _Model = new AutoDocModel();

      #region public
      public Frm_AutoDoc()
      {
         InitializeComponent();

         BindModel();
      }


      public void Show(ConnectionInfo info, List<ConnectionInfo> dbs)
      {
         _Info = info;
         _DBs = dbs;
         Text = $"Auto Doc - {_Info.Server} . {_Info.Database}";
         Show();

      }

      private void BindModel()
      {
         grdFields.DataSource = _Model.AT_FIELDS;

         
      }

      #endregion

      #region events
      private void btnAddField_Click(object sender, EventArgs e)
      {

         
            
         

      }
      #endregion
   }
}