using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using QToolbar.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QToolbar.Buttons
{
   public class LegalLinksButton : ButtonBase
   {

      private struct LegalLinkData
      {
         public string Text;
         public string Url;
         public bool NewWebLegal;
         public string Database;

         public LegalLinkData(string text, string url, bool newWebLegal, string database)
         {
            Text = text;
            Url = url;
            NewWebLegal = newWebLegal;
            Database = database;
         }
      }
      public LegalLinksButton(BarManager barManager, BarSubItem menu) : base("", barManager, menu)
      {
      }


      public override void CreateMenuItems()
      {

         _Menu.ClearLinks();
         // load legal links
         try
         {
            string devInst = OptionsInstance.QASQLInstances;
            if (!string.IsNullOrEmpty(devInst))
            {
               List<string> devdbs = new List<string>();
               SortedList<string, LegalLinkData> menuItems = new SortedList<string, LegalLinkData>();

               string[] devInstArr = devInst.Split(',');
               Regex reg = new Regex("^QBCollection[_]Plus[_][A-Za-z]+[_][0-9]+[_][0-9]+[_]*[0-9]*[_]*[0-9]*$");
               Regex regVer = new Regex("[0-9]+[_][0-9]+[_]*[0-9]*[_]*[0-9]*$");
               BarSubItem cutOffMenu = new BarSubItem(_BarManager, "Other", 0);
               foreach (string sqlInst in devInstArr)
               {

                  string connectionString = Utils.GetConnectionString(sqlInst);

                  using (SqlConnection con = new SqlConnection(connectionString))
                  {
                     try
                     {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.databases WHERE name LIKE 'QBCollection_Plus_%'", con))
                        {
                           try
                           {
                              using (SqlDataReader dr = cmd.ExecuteReader())
                              {
                                 while (dr.Read())
                                 {
                                    if (reg.IsMatch(dr[0].ToString()))
                                    {
                                       devdbs.Add(dr[0].ToString());
                                    }
                                 }
                              }
                           }
                           catch { }
                        }

                        foreach (var db in devdbs)
                        {
                           string legalUrl = "";
                           string sysPrefLegalUrl = string.Empty;
                           bool sysPrefNewWebLegal = false;
                           try
                           {
                              using (SqlCommand cmd = new SqlCommand($"SELECT SPR_VALUE FROM [{db}].[dbo].AT_SYSTEM_PREF WHERE SPR_TYPE IN ('WEB_UI','LEGAL_APP_PROCESS_MAPPING_WS_URL')", con))
                              {
                                 using (SqlDataReader dr = cmd.ExecuteReader())
                                 {
                                    if (dr.Read() && dr[0] != null)
                                    {
                                       sysPrefLegalUrl = dr[0].ToString();
                                    }
                                    if (dr.Read() && dr[0] != null && dr[0].ToString().Equals("1"))
                                    {
                                       sysPrefNewWebLegal = true;
                                    }
                                 }
                                 if (!string.IsNullOrEmpty(sysPrefLegalUrl))
                                 {
                                    Uri uri = new Uri(sysPrefLegalUrl.Trim());
                                    if (sysPrefNewWebLegal)
                                    {
                                       legalUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}/environmentselector/";
                                    }
                                    else
                                    {
                                       legalUrl = $"{uri.Scheme}://{uri.Host}:{uri.Port}/QCLegalApplicationApp/";
                                    }
                                 }
                              }

                              string dbVer = "";
                              if (legalUrl != null)
                              {
                                 Match verMatchVer = regVer.Match(db);
                                 if (verMatchVer.Success)
                                 {
                                    dbVer = verMatchVer.Value.Replace("_", ".");
                                 }
                              }

                              if (!string.IsNullOrEmpty(dbVer) && !string.IsNullOrEmpty(legalUrl) && !sysPrefNewWebLegal)  // new legal does have the port in database
                              {
                                 if (menuItems.Values.AsEnumerable().Where(x => x.Url.Equals(legalUrl)).ToList().Count.Equals(0))
                                 {
                                    menuItems.Add(Utils.GetSortName(dbVer, new Char[] { '.' }, '.', ' ', 3, true), new LegalLinkData(dbVer, legalUrl, sysPrefNewWebLegal, db));
                                 }
                              }

                           }
                           catch { }
                        }
                        con.Close();
                     }
                     catch { }
                  }

               }
               var ordered = menuItems.Reverse();
               int i = 0;
               foreach (var item in ordered)
               {
                  if (i >= Options.OptionsInstance.MaxMenuItems)
                  {
                     AddLegalLinksItem(item.Value, cutOffMenu);
                  }
                  else
                  {
                     AddLegalLinksItem(item.Value, _Menu);
                  }
                  i++;
               }
               if (cutOffMenu.ItemLinks.Count > 0)
               {
                  _Menu.AddItem(cutOffMenu);
               }
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show("Failed to retrieve legal links");
         }
      }

      private void AddLegalLinksItem(LegalLinkData data, BarSubItem parent)
      {
         string newIndicator = data.NewWebLegal ? " (new) " : string.Empty;
         string inst = data.Database.Replace("QBCollection_Plus_", "");
         string caption = ClearVersionLeast(data.Text, '.');
         BarButtonItem legalLinkItem = new BarButtonItem(_BarManager, caption, 3);
         // legal links are shell commands
         legalLinkItem.ItemClick += MenuItemClick;
         legalLinkItem.Tag = data;
         legalLinkItem.SuperTip = new SuperToolTip();
         legalLinkItem.SuperTip.Items.Add(data.Database);
         parent.AddItem(legalLinkItem);
      }

      protected override void MenuItemClick(object sender, ItemClickEventArgs e)
      {
         try
         {
            LegalLinkData data = (LegalLinkData)e.Item.Tag;

            if (data.NewWebLegal)
            {
               System.Diagnostics.Process.Start(data.Url);
            }
            else
            {
               // only IE supports old legal
               System.Diagnostics.Process.Start("IEXPLORE.EXE", data.Url);
            }
         }
         catch (Exception ex)
         {
            XtraMessageBox.Show(ex.Message);
         }
      }


      private string ClearVersionLeast(string version, char delimiter)
      {
         if (!string.IsNullOrWhiteSpace(version))
         {
            var vals = version.Split(new char[] { delimiter });


            if (vals.Length == 4)
            {
               if (Int32.TryParse(vals[2], out int result2))
               {
                  if (result2 > 50)
                  {
                     return $"{vals[0]}.{vals[1]}.{vals[2]}";
                  }
               }
            }
            if (vals.Length == 3)
            {
               if (Int32.TryParse(vals[2], out int result2))
               {
                  if (result2 < 50)
                  {
                     return $"{vals[0]}.{vals[1]}";
                  }
               }
            }
         }
         return version;
      }


   }
}
