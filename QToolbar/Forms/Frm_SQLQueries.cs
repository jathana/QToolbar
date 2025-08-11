using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using QToolbar.Options;
using DevExpress.XtraTreeList.Menu;
using DevExpress.XtraTreeList;
using QToolbar.Tools;
using QToolbar.Forms;
using QToolbar.AutoDoc;
using System.Text.RegularExpressions;
using QToolbar.Helpers;
using DevExpress.XtraBars;
using System.Diagnostics;

namespace QToolbar
{
   public partial class Frm_SQLQueries : DevExpress.XtraEditors.XtraForm
   {
      TreeNode<ConnectionInfo> _Tree = new TreeNode<ConnectionInfo>();
      List<ConnectionInfo> _DBs = new List<ConnectionInfo>();

      public Frm_SQLQueries()
      {
         InitializeComponent();

         backgroundWorker1.WorkerSupportsCancellation = true;
         backgroundWorker1.WorkerReportsProgress = true;
         treeDatabases.OptionsBehavior.Editable = false;
         treeDatabases.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
         treeDatabases.OptionsView.EnableAppearanceEvenRow = true;
         treeDatabases.OptionsView.EnableAppearanceOddRow = true;

         //treeDatabases.OptionsView.ShowColumns = false;
         

      }


      private void LoadDatabases()
      {
         // if cache file is present load from cache
         if (File.Exists(AppInstance.CFsTreeCacheFile))
         {
            TreeNodeSerializer<ConnectionInfo> ser = new TreeNodeSerializer<ConnectionInfo>();
            TreeNode<ConnectionInfo> tree = ser.Deserialize(AppInstance.CFsTreeCacheFile);
            SetDBsInfo(tree);
            PopulateDBTree(tree);
         }
         else
         {
            btnAdd.Enabled = false;
            treeDatabases.ClearNodes();
            treeDatabases.Cursor = Cursors.WaitCursor;
            EnableUI(false);
            // get all databases from cf
            backgroundWorker1.RunWorkerAsync();
         }
      }

      private void SetDBsInfo(TreeNode<ConnectionInfo> tree)
      {
         _DBs = tree.ToList().Where(i => i.InfoType == InfoType.Database).ToList();
      }

      private void Frm_SQLQueries_Load(object sender, EventArgs e)
      {
                  
         LoadDatabases();

         
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         // read all cf
         string folder = OptionsInstance.QCSAdminFolder;
         if (!string.IsNullOrWhiteSpace(folder))
         {
            if (Directory.Exists(folder))
            {
               try
               {
                  _Tree = new TreeNode<ConnectionInfo>();
                  List<string> dirs = new List<string>(Directory.EnumerateDirectories(folder));

                  // ensure current db is included
                  DataAccessService dataAccessService = new DataAccessService();
                  ConnectionInfo currentConnectionInfo = dataAccessService.GetCurrentDBConnectionInfo();
                  if (!dirs.Contains(currentConnectionInfo.Version))
                  {                     
                     dirs.Add(CreateFakeQBCAdminCFForCurrent(currentConnectionInfo));
                  }

                  List<Tuple<string, string>> x = new List<Tuple<string, string>>();
                  dirs.ForEach(item => x.Add(new Tuple<string,string>(GetSortName(item, new Char[] { '\\','.', ' ' }, '_', '0', 5,true), item)));
                  dirs = x.OrderByDescending(item => item.Item1).Select(item => item.Item2).ToList();

                  foreach (string dir in dirs)
                  {
                     TreeNode<ConnectionInfo> verNode = new TreeNode<ConnectionInfo>();
                     verNode.Data.CFPath = dir;
                     verNode.Data.InfoType = InfoType.Version;
                     verNode.Parent = _Tree;
                     _Tree.AddChild(verNode);
                     
                     // parse cf and get dbs
                     List<ConnectionInfo> dbs=null;
                     if (dir.Contains("Current_Fake_QBC_Admin"))
                     {
                        dbs = GetFakeCFDBs(dir);
                     }
                     else
                     {
                        dbs = GetCFDBs(Path.GetFileName(dir));
                     }

                     var servers=dbs.GroupBy(i => i.Server).ToList();
                     foreach (var server in servers)
                     {
                                               
                        List<ConnectionInfo> serverItems = server.ToList<ConnectionInfo>();
                        TreeNode<ConnectionInfo> serverNode = new TreeNode<ConnectionInfo>();
                        serverNode.Data.Server = server.Key;
                        serverNode.Parent = verNode;
                        serverNode.Data.InfoType = InfoType.Server;
                        verNode.AddChild(serverNode);

                        foreach (ConnectionInfo item in serverItems)
                        {
                           TreeNode<ConnectionInfo> child = new TreeNode<ConnectionInfo>();
                           child.Data = item;
                           child.Data.InfoType = InfoType.Database;
                           child.Parent = serverNode;
                           serverNode.AddChild(child);
                        }
                     }
                  }
                                    
                  e.Result = _Tree;
               }
               catch (Exception ex)
               {
                  XtraMessageBox.Show(string.Format($"Failed to retrieve subfolders of  {0} ({ex.Message})", folder));
               }
            }
         }

      }

      private string CreateFakeQBCAdminCFForCurrent(ConnectionInfo connectionInfo)
      {
         StringBuilder sb = new StringBuilder();
         sb.AppendLine("[servers]");
         sb.AppendLine($"{connectionInfo.Version.Replace(".", "_")}={connectionInfo.Server}");

         sb.AppendLine("[databasename]");
         sb.AppendLine($"{connectionInfo.Version.Replace(".", "_")}={connectionInfo.Database}");

         string path = $@"{AppInstance.CacheDirectory}\{connectionInfo.Version}";
         string filename = $@"{path}\Current_Fake_QBC_Admin.cf";
         Utils.EnsureFolder(path);

         File.WriteAllText(filename, sb.ToString());
         return filename;
      }

      private void FindConnectionInfo(TreeNode<ConnectionInfo> treeNode, InfoType infoType, string stringToSearch, ref TreeNode<ConnectionInfo> result)
      {         
         if (infoType== InfoType.Version && treeNode.Data.Version == stringToSearch)
         {
            result= treeNode;
            return;
         }

         foreach(var childNode in treeNode.Children) 
         {
            FindConnectionInfo(childNode, infoType, stringToSearch, ref result);
         }
         
      }





      private List<ConnectionInfo> GetCFDBs(string VerDirName)
      {
         List<ConnectionInfo> retVal = new List<ConnectionInfo>();

         string qcsAdminCFDir = Path.Combine(OptionsInstance.QCSAdminFolder, VerDirName, "Application Files");
         // find the newest dir
         if (Directory.Exists(qcsAdminCFDir))
         {
            string[] subdirs = Directory.GetDirectories(qcsAdminCFDir);
            string destDir = "";
            Version maxVersion = new Version(0, 0, 0, 0);
            foreach (string dir in subdirs)
            {
               Version curVersion = Utils.GetVersion(dir, "_", 1);

               if (curVersion != null)
               {
                  if (maxVersion < curVersion)
                  {
                     maxVersion = curVersion;
                     destDir = dir;
                  }
               }
               else
               {
                  //XtraMessageBox.Show($"Cannot parse directory name ({dir}).");
               }
            }
            string file = Path.Combine(destDir, "QBC_Admin.cf.deploy");
            IniFile cfFile = new IniFile(file, "#");
            Dictionary<string, string> servers = cfFile.GetSectionPairs("[servers]");
            Dictionary<string, string> dbnames = cfFile.GetSectionPairs("[databasename]");
            if (servers.Count != dbnames.Count)
            {
               //throw new Exception($"[Servers] and [DatabaseName] sections of {file} contain different number of items.");
            }
            else
            {
               foreach (KeyValuePair<string, string> server in servers)
               {
                  
                  try
                  {
                     ConnectionInfo info = new ConnectionInfo()
                     {
                        Environment = server.Key,
                        Server = server.Value,
                        Database = dbnames[server.Key],
                        DatabaseSortName = GetSortName(dbnames[server.Key], new Char[] { '.','_' },'_','0',5),
                        CFPath = destDir
                     };
                     retVal.Add(info);
                  }
                  catch(Exception ex)
                  {
                     //throw new Exception($"Not found server key {server.Key} in [Databases] in {file} .");
                  }
               }
            }
         }
         return retVal;
      }

      private List<ConnectionInfo> GetFakeCFDBs(string fakeCFFile)
      {
         List<ConnectionInfo> retVal = new List<ConnectionInfo>();

         // find the newest dir
         if (File.Exists(fakeCFFile))
         {            
            IniFile cfFile = new IniFile(fakeCFFile, "#");
            Dictionary<string, string> servers = cfFile.GetSectionPairs("[servers]");
            Dictionary<string, string> dbnames = cfFile.GetSectionPairs("[databasename]");
            if (servers.Count != dbnames.Count)
            {
               //throw new Exception($"[Servers] and [DatabaseName] sections of {file} contain different number of items.");
            }
            else
            {
               foreach (KeyValuePair<string, string> server in servers)
               {

                  try
                  {
                     ConnectionInfo info = new ConnectionInfo()
                     {
                        Environment = server.Key,
                        Server = server.Value,
                        Database = dbnames[server.Key],
                        DatabaseSortName = GetSortName(dbnames[server.Key], new Char[] { '.', '_' }, '_', '0', 5),
                        CFPath = fakeCFFile
                     };
                     retVal.Add(info);
                  }
                  catch (Exception ex)
                  {
                     //throw new Exception($"Not found server key {server.Key} in [Databases] in {file} .");
                  }
               }
            }
         }
         return retVal;
      }

      private string GetSortName(string database, char[] delimiters, char joinDelimiter,char padChar,int padLength, bool discardNonNumbers=false)
      {
         string result = database;
         if (!string.IsNullOrEmpty(database))
         {
            List<string> db = database.Split(delimiters).ToList();
            List<string> res = new List<string>();
            for (int i = 0; i < db.Count; i++)
            {
               if (db[i].All(Char.IsDigit))
               {
                  res.Add(db[i].PadLeft(padLength, padChar));
               }
               else
               {
                  if(!discardNonNumbers)
                  {
                     res.Add(db[i]);
                  }
               }
            }
            result = string.Join(joinDelimiter.ToString(), res);
         }
         return result;
      }

      private void EnableUI(bool enable)
      {
         txtFilter.Enabled = enable;
      }

      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         treeDatabases.ClearNodes();

          
         TreeNode<ConnectionInfo> tree = (TreeNode<ConnectionInfo>)e.Result;
         SetDBsInfo(tree);
         PopulateDBTree(tree);
         //AppInstance.CFDatabasesTree = tree;
         TreeNodeSerializer<ConnectionInfo> ser = new TreeNodeSerializer<ConnectionInfo>();
         ser.Serialize(tree, AppInstance.CFsTreeCacheFile);
         btnAdd.Enabled = true;         
         EnableUI(true);
         treeDatabases.Cursor = Cursors.Default;


         
      }

      private void PopulateDBTree(TreeNode<ConnectionInfo> tree)
      {
         treeDatabases.DataSource = tree;
         treeDatabases.ExpandAll();
         treeDatabases.BestFitColumns();
         
      }

      private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {

      }

      private void treeDatabases_VirtualTreeGetChildNodes(object sender, DevExpress.XtraTreeList.VirtualTreeGetChildNodesInfo e)
      {
         if (e.Node is TreeNode<ConnectionInfo>)
         {
            e.Children = ((TreeNode<ConnectionInfo>)e.Node).Children;
         }
      }

      private void treeDatabases_VirtualTreeGetCellValue(object sender, DevExpress.XtraTreeList.VirtualTreeGetCellValueInfo e)
      {
         TreeNode<ConnectionInfo> node = (TreeNode<ConnectionInfo>)e.Node;

         if (e.Column.Caption == "Name")
         {
            if (node.Data.InfoType == InfoType.Database)
            {
               e.CellData = node.Data.Database;
            }
            else if (node.Data.InfoType == InfoType.Server)
            {
               e.CellData = ((TreeNode<ConnectionInfo>)e.Node).Data.Server;
            }
            else if (node.Data.InfoType == InfoType.Version)
            {
               e.CellData = ((TreeNode<ConnectionInfo>)e.Node).Data.Version;
            }
         }
         else if (e.Column.Caption == "Server")
         {
            if (node.Data.InfoType == InfoType.Server)
            {
               e.CellData = node.Data.Server;
            }
         }
         else if (e.Column.Caption == "Database")
         {
            if (node.Data.InfoType == InfoType.Database)
            {
               e.CellData = node.Data.Database;
            }

         }

      }

      private void treeDatabases_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
      {
         if (e.Menu is TreeListNodeMenu)
         {
            BarEditItemLink filter = (BarEditItemLink)popupMenu1.ItemLinks.FirstOrDefault(i => i is BarEditItemLink);
            if (filter != null)
            {
               filter.EditValue = string.Empty;
            }

            while (true)
            {
               var item = popupMenu1.ItemLinks.FirstOrDefault(i => i is BarButtonItemLink);
               if (item == null)
               {
                  break;
               }
               popupMenu1.ItemLinks.Remove(item);
            }


            treeDatabases.FocusedNode = ((TreeListNodeMenu)e.Menu).Node;
            TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
            if (obj.Data.InfoType == InfoType.Database)
            {
               foreach (DataRow query in OptionsInstance.SQLQueries.Data.Rows)
               {
                  BarButtonItem mnuItem = new BarButtonItem(barManager1, query["Name"].ToString());
                  mnuItem.ItemClick += query_ItemClick;
                  mnuItem.Tag = query;

                  popupMenu1.AddItem(mnuItem);
               }

               // create criteria if dev current
               var devDBs = GetDevDBsConnectionInfo();

               if (devDBs.Count > 0 && devDBs[0].Database.ToLower().Equals(obj.Data.Database.ToLower()))
               {
                  BarButtonItem mnuItemCreateCriteria = new BarButtonItem(barManager1, "Create Criteria");
                  mnuItemCreateCriteria.ItemClick += createCriteria_ItemClick;
                  mnuItemCreateCriteria.Tag = obj.Data;
                  popupMenu1.AddItem(mnuItemCreateCriteria);
               }

               // script criteria only for dev dbs
               if (devDBs.Count > 0 && devDBs.Count(d => d.Database == obj.Data.Database) > 0)
               {
                  BarButtonItem mnuItemScriptCriteria = new BarButtonItem(barManager1, "Script Criteria");
                  mnuItemScriptCriteria.ItemClick += scriptCriteria_ItemClick;
                  mnuItemScriptCriteria.Tag = obj.Data;
                  popupMenu1.AddItem(mnuItemScriptCriteria);
               }


               // fields helper only for current
               if (devDBs.Count > 0 && devDBs[0].Database.ToLower().Equals(obj.Data.Database.ToLower()))
               {
                  BarButtonItem mnuItemFieldsHelper = new BarButtonItem(barManager1, "Fields Helper");
                  mnuItemFieldsHelper.ItemClick += fieldsHelper_ItemClick;
                  mnuItemFieldsHelper.Tag = obj.Data;
                  popupMenu1.AddItem(mnuItemFieldsHelper);
               }

               // auto doc only for dev dbs
               if (devDBs.Count > 0 && devDBs.Count(d => d.Database == obj.Data.Database) > 0)
               {
                  BarButtonItem mnuItemAutoDoc = new BarButtonItem(barManager1, "Auto Doc");
                  mnuItemAutoDoc.ItemClick += autoDoc_ItemClick;
                  mnuItemAutoDoc.Tag = obj.Data;
                  popupMenu1.AddItem(mnuItemAutoDoc);
               }

               // check datasources for dev dbs only
               if (devDBs.Count > 0 && devDBs.Count(d => d.Database == obj.Data.Database) > 0)
               {
                  BarButtonItem mnuCheckdatasources = new BarButtonItem(barManager1, "Check Datasources");
                  mnuCheckdatasources.ItemClick += checkDatasources_ItemClick;
                  mnuCheckdatasources.Tag = obj.Data;
                  popupMenu1.AddItem(mnuCheckdatasources);
               }

            }

            e.ShowCustomMenu(popupMenu1);
            filter.Focus();
         }
      }

      private List<ConnectionInfo> GetDevDBsConnectionInfo()
      {
         Regex reg = new Regex("^qbcollection[_]plus[_][0-9]+[_][0-9]+[_]*[0-9]*$");
         var result = _DBs.Where(d => reg.IsMatch(d.Database.ToLower()) &&
                                    OptionsInstance.DevSQLInstances.ToLower().Contains(d.Server.ToLower())).OrderByDescending(d => d.DatabaseSortName).ToList();

         return result;
      }

      private void query_ItemClick(object sender, ItemClickEventArgs e)
      {         
          
         StringBuilder builder = new StringBuilder();
         BarButtonItem mnuItem = (BarButtonItem)e.Item;
         uc_SQL ctr = new uc_SQL();

         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
         ConnectionInfo data = obj.Data;
         DataRow query = (DataRow)mnuItem.Tag;
         // sql text
         builder.AppendLine($"--       Query : {mnuItem.Caption}");
         builder.AppendLine($"--      Server : {data.Server}");
         builder.AppendLine($"--    Database : {data.Database}");
         builder.AppendLine($"--     Version : {data.Version}");
         builder.AppendLine($"-- Environment : {data.Environment}");
         builder.AppendLine();
         builder.AppendLine(query["SQL"].ToString());
         // sql control
         ctr.Query = builder.ToString();
         ctr.Server = data.Server;
         ctr.Database = data.Database;
         ctr.QueryName = mnuItem.Caption;
         ctr.Databases = _DBs;
         ctr.Initialize();

         if ( query["RunImmediate"] != DBNull.Value && Convert.ToBoolean(query["RunImmediate"])==true)
         {
            ctr.Run();
         }
         // tab 
         DevExpress.XtraBars.Docking2010.Views.Tabbed.Document doc=  (DevExpress.XtraBars.Docking2010.Views.Tabbed.Document)documentManager1.View.AddDocument(ctr);
         tabbedView1.Controller.Select(doc);
         doc.Caption = ctr.Database;
         

      }


      private void scriptCriteria_ItemClick(object sender, ItemClickEventArgs e)
      {
         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
         ConnectionInfo data = obj.Data;
         Frm_ScriptCriteria f = new Frm_ScriptCriteria();
         f.Show(data);
      }

      private void createCriteria_ItemClick(object sender, ItemClickEventArgs e)
      {
         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
         ConnectionInfo data = obj.Data;
         Frm_CriteriaHelper f = new Frm_CriteriaHelper();
         f.Show(data, _DBs);
      }

      private void fieldsHelper_ItemClick(object sender, ItemClickEventArgs e)
      {
         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
         ConnectionInfo data = obj.Data;
         Frm_FieldsHelper f = new Frm_FieldsHelper();
         f.Show(data, _DBs);
      }

      private void autoDoc_ItemClick(object sender, ItemClickEventArgs e)
      {
         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
         ConnectionInfo data = obj.Data;
         Frm_AutoDoc f = new Frm_AutoDoc();
         f.Show(data, _DBs);
      }

      private void checkDatasources_ItemClick(object sender, ItemClickEventArgs e)
      {
         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(treeDatabases.FocusedNode);
         ConnectionInfo data = obj.Data;
         Frm_CheckDatasources f = new Frm_CheckDatasources();
         f.Show(data);
      }
      private void treeDatabases_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
      {
         TreeNode<ConnectionInfo> obj = (TreeNode<ConnectionInfo>)treeDatabases.GetDataRecordByNode(e.Node);
         ConnectionInfo data = obj.Data;
         switch(data.InfoType)
         {
            case InfoType.Database: e.NodeImageIndex = imageCollection1.Images.Keys.IndexOf("db");break;
            case InfoType.Server: e.NodeImageIndex = imageCollection1.Images.Keys.IndexOf("server"); break;
            case InfoType.Version: e.NodeImageIndex = imageCollection1.Images.Keys.IndexOf("env"); break;

         }
      }

      private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         File.Delete(AppInstance.CFsTreeCacheFile);
         LoadDatabases();
      }

      private void treeDatabases_FilterNode(object sender, FilterNodeEventArgs e)
      {

         if (txtFilter.EditValue  != null && !string.IsNullOrEmpty(txtFilter.EditValue.ToString()))
         {
            e.Node.Visible = e.Node["Name"].ToString().ToLower().Contains(txtFilter.EditValue.ToString().ToLower());
            e.Handled = true;
         }
      }

      private void txtFilter_EditValueChanged(object sender, EventArgs e)
      {
         treeDatabases.FilterNodes();
      }

      private void repositoryMenuFilter_EditValueChanged(object sender, EventArgs e)
      {
         string editValue = ((TextEdit)sender).EditValue.ToString();
         Debug.WriteLine(((TextEdit)sender).EditValue);


         //popupMenu1.AddItem(new BarSubItem(barManager1,"ssss"));
         for (int i = 0; i < popupMenu1.ItemLinks.Count; i++)
         {
            if (popupMenu1.ItemLinks[i] is BarButtonItemLink)
            {
               popupMenu1.ItemLinks[i].Visible = popupMenu1.ItemLinks[i].Caption.ToLower().Contains(editValue.ToLower());
            }

         }
         popupMenu1.ItemLinks[0].Focus();
         popupMenu1.ItemLinks.First(i=>i is BarEditItemLink).Focus();

      }
      
      private void repositoryMenuFilter_KeyUp(object sender, KeyEventArgs e)
      {
      }

      private void repositoryMenuFilter_KeyDown(object sender, KeyEventArgs e)
      {
         // if the escape button was presse while filter had the focus close popup menu. 
         // This is needed since the popop menu is not hidden on escape button press and the filter had the focus. 
         if (e.KeyCode == Keys.Escape)
         {
            popupMenu1.HidePopup();
         }

      }
   }
}