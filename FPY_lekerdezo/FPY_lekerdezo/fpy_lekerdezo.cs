using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPY_lekerdezo
{
    public partial class fpy_lekerdezo : Form
    {
        string connetionstringgen2 = "Data Source=10.207.40.200;Initial Catalog=Gen2;Persist Security Info=True;User ID=GEN2;Password=1234";
        bool tableswitch = false;
        string filename = "";
        string date = "";
        string tipus = "";

        public fpy_lekerdezo()
        {
            InitializeComponent();
        }

        private void fpy_lekerdezo_Load(object sender, EventArgs e)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(@"C:\temp\");
            file.Directory.Create();

            datepicker_from.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1 , 6, 00, 00);
            datepicker_to.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 59, 59);
            
            //datepicker_to.Value = DateTime.Today;

            CultureInfo USlanguage = CultureInfo.CreateSpecificCulture("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = USlanguage;
            InputLanguage l = InputLanguage.FromCulture(USlanguage);
            InputLanguage.CurrentInputLanguage = l;

            tipus_combobox.Items.Add("VOLVO");
            tipus_combobox.Items.Add("BMW");

            datepicker_from.Format = DateTimePickerFormat.Custom;
            datepicker_from.CustomFormat = "MM/dd/yyyy HH:mm:ss";

            datepicker_to.Format = DateTimePickerFormat.Custom;
            datepicker_to.CustomFormat = "MM/dd/yyyy HH:mm:ss";

            tipus_combobox.Text = "VOLVO";

            try
            {
                string query1 = "Select * from [dbo].[Operations]";

                SqlConnection cnn = new SqlConnection(connetionstringgen2);
                cnn.Open();
                SqlCommand objcmdm = new SqlCommand(query1, cnn);
                objcmdm.ExecuteNonQuery();
                SqlDataAdapter adpm = new SqlDataAdapter(objcmdm);
                DataTable dtm = new DataTable();
                adpm.Fill(dtm);
                eredmenyek_seged_datagridview.DataSource = dtm;
                eredmenyek_seged_datagridview.Refresh();
                // pictureBox1.Visible = false;
                cnn.Close();


                if (eredmenyek_seged_datagridview.RowCount > 1)
                {
                    for (int i = 0; i < eredmenyek_seged_datagridview.RowCount - 1; i++)
                    {

                        muvelet_checklist.Items.Add(eredmenyek_seged_datagridview.Rows[i].Cells["Operation_ID"].Value.ToString() + " - " + eredmenyek_seged_datagridview.Rows[i].Cells["Operation_Msg"].Value.ToString());
                        //muvelet_comboBox1.Items.Add(eredmenyek_seged_datagridview.Rows[i].Cells["Operation_ID"].Value.ToString() + " - " + eredmenyek_seged_datagridview.Rows[i].Cells["Operation_Msg"].Value.ToString());

                    }


                }

            }
            catch (Exception ex) { MessageBox.Show("Nem várt hiba a hiba oka:\n" + ex.Message); }
        }

        private void lekerdez_but_Click(object sender, EventArgs e)
        {
            tableswitch = false;
            try
            {

                date = Convert.ToString(datepicker_from.Text).Substring(0, 10).Replace("/", "_");
                tipus = tipus_combobox.Text.ToString();
                filename = date + "_" + tipus + "_C.csv";
                /*****************Create CSV******************/
                if (!File.Exists(@"C:\temp\" + filename))
                {
                    string csvbase = tipus_combobox.Text.ToString() + "\r\n"
                      + Convert.ToString(datepicker_from.Text).Substring(0, 10) + "\r\n"
                      + "Operation_ID: ; \r\n"
                      + "Tesztelt darabszam: ; \r\n"
                      + "Elsore kiesett: ;\r\n"
                      + "FPY: ;\r\n"
                      + "TLR: ;\r\n"
                      + "Valos kieses: ;\r\n"
                      + "Valos kieses [%] : ;\r\n\r\n";
                    File.WriteAllText(@"C:\temp\" + filename, csvbase);
                }
                string heatsinkquery = "Select HeatSinkData.HeatSink_ID as ID "
                                        + ", HeatSinkData.Workstation"
                                        + ", HeatSinkData.Operation_ID"
                                        + ", Operations.Operation_Msg"
                                        + ", HeatSinkData.Result"
                                        + ", HeatSinkData.Value"
                                        + ", HeatSinkData.Limit_Min"
                                        + ", HeatSinkData.Limit_Max"
                                        + ", HeatSinkData.Date"
                                        + ", HeatSinkData.Operator"
                                        + ", HeatSinkData.Note"
                                        + " from [dbo].[HeatSinkData] JOIN [dbo].[Operations]"
                                        + " ON HeatSinkData.Operation_ID=Operations.Operation_ID"
                                        + " INNER JOIN Gen2.dbo.Main ON HeatSinkData.HeatSink_ID=main.HeatSink_ID"
                                        + " WHERE";
                string housingquery = "Select HousingData.Housing_ID as ID "
                                        + ", HousingData.Workstation"
                                        + ", HousingData.Operation_ID"
                                        + ", Operations.Operation_Msg"
                                        + ", HousingData.Result"
                                        + ", HousingData.Value"
                                        + ", HousingData.Limit_Min"
                                        + ", HousingData.Limit_Max"
                                        + ", HousingData.Date"
                                        + ", HousingData.Operator"
                                        + ", HousingData.Note"
                                        + " from [dbo].[HousingData] JOIN [dbo].[Operations]"
                                        + " ON HousingData.Operation_ID=Operations.Operation_ID"
                                        + " INNER JOIN Gen2.dbo.Main ON HousingData.Housing_ID=main.Housing_ID"
                                        + " WHERE";

                heatsinkquery += "AND HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "'";
                housingquery += "AND HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "'";



                for (int i = 0; i < muvelet_checklist.Items.Count; i++)
                {
                    tableswitch = false;
                    if (muvelet_checklist.GetItemChecked(i))
                    {
                        string[] splitted_value = muvelet_checklist.Items[i].ToString().Split(' ', '-');
                        string op = splitted_value[0];
                        string q1 = heatsinkquery;
                        string q2 = housingquery;

                        q1 += "AND HeatSinkData.Operation_ID = '" + op + "' AND Main.Type = ' " + Convert.ToString(tipus_combobox.Text) + "'";
                        q2 += "AND HousingData.Operation_ID = '" + op + "' AND Main.Type = ' " + Convert.ToString(tipus_combobox.Text) + "'";

                        q1 = q1.Replace("WHEREAND", " WHERE");
                        q2 = q2.Replace("WHEREAND", " WHERE");
                        string unionquery = q1 + " UNION " + q2;

                        string query = "Select HeatSinkData.HeatSink_ID as ID, HeatSinkData.Workstation, HeatSinkData.Operation_ID, Operations.Operation_Msg, HeatSinkData.Result, HeatSinkData.Value, HeatSinkData.Limit_Min, HeatSinkData.Limit_Max, HeatSinkData.Date, HeatSinkData.Operator, HeatSinkData.Note  from [dbo].[HeatSinkData] JOIN [dbo].[Operations] ON HeatSinkData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HeatSinkData.HeatSink_ID=main.HeatSink_ID WHERE HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HeatSinkData.Operation_ID='" + op + "' UNION Select HousingData.Housing_ID as ID, HousingData.Workstation, HousingData.Operation_ID, Operations.Operation_Msg, HousingData.Result, HousingData.Value, HousingData.Limit_Min, HousingData.Limit_Max, HousingData.Date, HousingData.Operator, HousingData.Note  from [dbo].[HousingData] JOIN [dbo].[Operations] ON HousingData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HousingData.Housing_ID=main.Housing_ID WHERE HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HousingData.Operation_ID = '" + op + "'";

                        eredmenyek_lekerdezese1(query, result_dgv);
                        ismetlodeskiszurese();
                        fpyellenorzese(op);
                        tableswitch = true;
                        ismetlodeskiszurese();
                        fpyellenorzese(op);


                    }
                }
                MessageBox.Show(@"Az eredmények mentve lettek a C:\temp\ mappába a következő fájlnévvel: " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ismetlodeskiszurese()
        {
            if (tableswitch == true) result_dgv.Sort(this.result_dgv.Columns["Date"], ListSortDirection.Descending);
            final_dgv.Rows.Clear();
            try
            {
                for (int i = 0; i < result_dgv.RowCount - 1; i++)
                {
                    string cell1 = result_dgv.Rows[i].Cells["ID"].Value.ToString();
                    string cell2 = result_dgv.Rows[i].Cells["Operation_Msg"].Value.ToString();

                    if (find(cell1, cell2))
                    {
                    }
                    else
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = (DataGridViewRow)result_dgv.Rows[i].Clone();
                        int intColIndex = 0;
                        foreach (DataGridViewCell cell in result_dgv.Rows[i].Cells)
                        {
                            row.Cells[intColIndex].Value = cell.Value;
                            intColIndex++;
                        }
                        final_dgv.Rows.Add(row);
                    }
                }
            } // try blokk vége
            catch (Exception ex) { MessageBox.Show(ex.Message); }



        }
        private bool find(string cell1, string cell2)
        {
            bool eredmeny = false;
            try
            {
                if (final_dgv.RowCount > 1)
                {
                    for (int i = 0; i < final_dgv.RowCount - 1; i++)
                    {
                        if ((final_dgv.Rows[i].Cells["ID"].Value.ToString() == cell1) && (final_dgv.Rows[i].Cells["Operation_Msg"].Value.ToString() == cell2))
                        {
                            eredmeny = true;
                            break;
                        }
                        else
                        {
                            eredmeny = false;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return eredmeny;


        }

        private void fpyellenorzese(string muvelet)
        {

            int sum = 0;
            double indtlr = 0;
            double indfpy = 0;
            int indokcount = 0;
            int indnokcount = 0;
            int osszdarab = 0;
            
                try
                {

                    string root = @"C:\temp";

                    System.IO.FileInfo file = new System.IO.FileInfo(root);
                    file.Directory.Create();

                    if (!System.IO.Directory.Exists(root))
                    {
                        System.IO.Directory.CreateDirectory(root);
                    }

                    for (int i = 0; i < result_dgv.RowCount - 1; i++)
                    {
                        if (result_dgv.Rows[i].Cells["Operation_ID"].Value.ToString() == muvelet)
                        {
                            osszdarab++;
                        }

                    }

                    for (int i = 0; i < final_dgv.RowCount - 1; i++)
                    {
                        if (final_dgv.Rows[i].Cells["Operation_ID"].Value.ToString() == muvelet)
                        {
                            // osszdarab++;
                            if (final_dgv.Rows[i].Cells["Result"].Value.ToString() == "OK")
                            {
                                indokcount++;
                            }
                            else if (final_dgv.Rows[i].Cells["Result"].Value.ToString() == "NOK")
                            {
                                indnokcount++;
                            }
                        }
                    }


                    sum = indokcount + indnokcount;
                    indtlr = (double)Math.Round(((double)(indnokcount * 100) / sum), 2);
                    indfpy = 100 - indtlr;

                    //MessageBox.Show("Operation_ID:" + Convert.ToString(muvelet) + "\nÖsszes teszt (újratesztel együtt): " + Convert.ToString(osszdarab) + "\nLetesztelt termékek száma: " + Convert.ToString(sum) + "\nNOK termékek száma: " + Convert.ToString(indnokcount) + "\nOk termékek száma: " + Convert.ToString(indokcount) + "\nFPY: " + Convert.ToString(indfpy) + "%\nTLR: " + Convert.ToString(indtlr) + "%");

                    /*********************CSV tartalmának beolvasása, új adatok hozzáfűzése***********************/

                    date = Convert.ToString(datepicker_from.Text).Substring(0, 10).Replace("/", "_");
                    tipus = tipus_combobox.Text.ToString();

                    List<string> newColumnData = new List<string>() { "D" };
                    List<string> lines = File.ReadAllLines(@"C:\temp\" + filename).ToList();
                    if (tableswitch == false)
                    {
                        lines[2] += Convert.ToString(muvelet) + ";";
                        lines[3] += Convert.ToString(sum) + ";";
                        lines[4] += Convert.ToString(indnokcount) + ";";
                        lines[5] += Convert.ToString(indfpy) + "%;";
                        lines[6] += Convert.ToString(indtlr) + "%;";
                    }
                    else if (tableswitch == true)
                    {

                        lines[7] += Convert.ToString(indnokcount) + ";";
                        lines[8] += Convert.ToString(indtlr) + "%;";
                    }
                    File.WriteAllLines(@"C:\temp\" + filename, lines);

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void eredmenyek_lekerdezese1(string lekerdezes1, DataGridView datagridview)
        {
            //pictureBox1.Visible = true;
            try
            {
                string query1 = lekerdezes1;

                SqlConnection cnn = new SqlConnection(connetionstringgen2);
                cnn.Open();
                SqlCommand objcmdm = new SqlCommand(query1, cnn);
                objcmdm.ExecuteNonQuery();
                SqlDataAdapter adpm = new SqlDataAdapter(objcmdm);
                DataTable dtm = new DataTable();
                adpm.Fill(dtm);
                datagridview.DataSource = dtm;
                datagridview.Sort(datagridview.Columns["Date"], ListSortDirection.Ascending);
                datagridview.Refresh();

                cnn.Close();
            }
            catch (Exception ex) { MessageBox.Show("Nem várt hiba a hiba oka, a következő:\n" + ex.Message); }

        }

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPY_lekerdezo
{
    public partial class fpy_lekerdezo : Form
    {
        string connetionstringgen2 = "Data Source=10.207.40.200;Initial Catalog=Gen2;Persist Security Info=True;User ID=GEN2;Password=1234";
        bool tableswitch = false;
        string filename = "";
        string date = "";
        string tipus = "";

        public fpy_lekerdezo()
        {
            InitializeComponent();
        }

        private void fpy_lekerdezo_Load(object sender, EventArgs e)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(@"C:\temp\");
            file.Directory.Create();

            datepicker_from.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1 , 6, 00, 00);
            datepicker_to.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 59, 59);
            
            //datepicker_to.Value = DateTime.Today;

            CultureInfo USlanguage = CultureInfo.CreateSpecificCulture("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = USlanguage;
            InputLanguage l = InputLanguage.FromCulture(USlanguage);
            InputLanguage.CurrentInputLanguage = l;

            tipus_combobox.Items.Add("VOLVO");
            tipus_combobox.Items.Add("BMW");

            datepicker_from.Format = DateTimePickerFormat.Custom;
            datepicker_from.CustomFormat = "MM/dd/yyyy HH:mm:ss";

            datepicker_to.Format = DateTimePickerFormat.Custom;
            datepicker_to.CustomFormat = "MM/dd/yyyy HH:mm:ss";

            tipus_combobox.Text = "VOLVO";

            try
            {
                string query1 = "Select * from [dbo].[Operations]";

                SqlConnection cnn = new SqlConnection(connetionstringgen2);
                cnn.Open();
                SqlCommand objcmdm = new SqlCommand(query1, cnn);
                objcmdm.ExecuteNonQuery();
                SqlDataAdapter adpm = new SqlDataAdapter(objcmdm);
                DataTable dtm = new DataTable();
                adpm.Fill(dtm);
                eredmenyek_seged_datagridview.DataSource = dtm;
                eredmenyek_seged_datagridview.Refresh();
                // pictureBox1.Visible = false;
                cnn.Close();


                if (eredmenyek_seged_datagridview.RowCount > 1)
                {
                    for (int i = 0; i < eredmenyek_seged_datagridview.RowCount - 1; i++)
                    {

                        muvelet_checklist.Items.Add(eredmenyek_seged_datagridview.Rows[i].Cells["Operation_ID"].Value.ToString() + " - " + eredmenyek_seged_datagridview.Rows[i].Cells["Operation_Msg"].Value.ToString());
                        //muvelet_comboBox1.Items.Add(eredmenyek_seged_datagridview.Rows[i].Cells["Operation_ID"].Value.ToString() + " - " + eredmenyek_seged_datagridview.Rows[i].Cells["Operation_Msg"].Value.ToString());

                    }


                }

            }
            catch (Exception ex) { MessageBox.Show("Nem várt hiba a hiba oka:\n" + ex.Message); }
        }

        private void lekerdez_but_Click(object sender, EventArgs e)
        {
            tableswitch = false;
            try
            {

                date = Convert.ToString(datepicker_from.Text).Substring(0, 10).Replace("/", "_");
                tipus = tipus_combobox.Text.ToString();
                filename = date + "_" + tipus + "_C.csv";
                /*****************Create CSV******************/
                if (!File.Exists(@"C:\temp\" + filename))
                {
                    string csvbase = tipus_combobox.Text.ToString() + "\r\n"
                      + Convert.ToString(datepicker_from.Text).Substring(0, 10) + "\r\n"
                      + "Operation_ID: ; \r\n"
                      + "Tesztelt darabszam: ; \r\n"
                      + "Elsore kiesett: ;\r\n"
                      + "FPY: ;\r\n"
                      + "TLR: ;\r\n"
                      + "Valos kieses: ;\r\n"
                      + "Valos kieses [%] : ;\r\n\r\n";
                    File.WriteAllText(@"C:\temp\" + filename, csvbase);
                }
                string heatsinkquery = "Select HeatSinkData.HeatSink_ID as ID "
                                        + ", HeatSinkData.Workstation"
                                        + ", HeatSinkData.Operation_ID"
                                        + ", Operations.Operation_Msg"
                                        + ", HeatSinkData.Result"
                                        + ", HeatSinkData.Value"
                                        + ", HeatSinkData.Limit_Min"
                                        + ", HeatSinkData.Limit_Max"
                                        + ", HeatSinkData.Date"
                                        + ", HeatSinkData.Operator"
                                        + ", HeatSinkData.Note"
                                        + " from [dbo].[HeatSinkData] JOIN [dbo].[Operations]"
                                        + " ON HeatSinkData.Operation_ID=Operations.Operation_ID"
                                        + " INNER JOIN Gen2.dbo.Main ON HeatSinkData.HeatSink_ID=main.HeatSink_ID"
                                        + " WHERE";
                string housingquery = "Select HousingData.Housing_ID as ID "
                                        + ", HousingData.Workstation"
                                        + ", HousingData.Operation_ID"
                                        + ", Operations.Operation_Msg"
                                        + ", HousingData.Result"
                                        + ", HousingData.Value"
                                        + ", HousingData.Limit_Min"
                                        + ", HousingData.Limit_Max"
                                        + ", HousingData.Date"
                                        + ", HousingData.Operator"
                                        + ", HousingData.Note"
                                        + " from [dbo].[HousingData] JOIN [dbo].[Operations]"
                                        + " ON HousingData.Operation_ID=Operations.Operation_ID"
                                        + " INNER JOIN Gen2.dbo.Main ON HousingData.Housing_ID=main.Housing_ID"
                                        + " WHERE";

                heatsinkquery += "AND HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "'";
                housingquery += "AND HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "'";



                for (int i = 0; i < muvelet_checklist.Items.Count; i++)
                {
                    tableswitch = false;
                    if (muvelet_checklist.GetItemChecked(i))
                    {
                        string[] splitted_value = muvelet_checklist.Items[i].ToString().Split(' ', '-');
                        string op = splitted_value[0];
                        string q1 = heatsinkquery;
                        string q2 = housingquery;

                        q1 += "AND HeatSinkData.Operation_ID = '" + op + "' AND Main.Type = ' " + Convert.ToString(tipus_combobox.Text) + "'";
                        q2 += "AND HousingData.Operation_ID = '" + op + "' AND Main.Type = ' " + Convert.ToString(tipus_combobox.Text) + "'";

                        q1 = q1.Replace("WHEREAND", " WHERE");
                        q2 = q2.Replace("WHEREAND", " WHERE");
                        string unionquery = q1 + " UNION " + q2;

                        string query = "Select HeatSinkData.HeatSink_ID as ID, HeatSinkData.Workstation, HeatSinkData.Operation_ID, Operations.Operation_Msg, HeatSinkData.Result, HeatSinkData.Value, HeatSinkData.Limit_Min, HeatSinkData.Limit_Max, HeatSinkData.Date, HeatSinkData.Operator, HeatSinkData.Note  from [dbo].[HeatSinkData] JOIN [dbo].[Operations] ON HeatSinkData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HeatSinkData.HeatSink_ID=main.HeatSink_ID WHERE HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HeatSinkData.Operation_ID='" + op + "' UNION Select HousingData.Housing_ID as ID, HousingData.Workstation, HousingData.Operation_ID, Operations.Operation_Msg, HousingData.Result, HousingData.Value, HousingData.Limit_Min, HousingData.Limit_Max, HousingData.Date, HousingData.Operator, HousingData.Note  from [dbo].[HousingData] JOIN [dbo].[Operations] ON HousingData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HousingData.Housing_ID=main.Housing_ID WHERE HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HousingData.Operation_ID = '" + op + "'";

                        eredmenyek_lekerdezese1(query, result_dgv);
                        ismetlodeskiszurese();
                        fpyellenorzese(op);
                        tableswitch = true;
                        ismetlodeskiszurese();
                        fpyellenorzese(op);


                    }
                }
                MessageBox.Show(@"Az eredmények mentve lettek a C:\temp\ mappába a következő fájlnévvel: " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ismetlodeskiszurese()
        {
            if (tableswitch == true) result_dgv.Sort(this.result_dgv.Columns["Date"], ListSortDirection.Descending);
            final_dgv.Rows.Clear();
            try
            {
                for (int i = 0; i < result_dgv.RowCount - 1; i++)
                {
                    string cell1 = result_dgv.Rows[i].Cells["ID"].Value.ToString();
                    string cell2 = result_dgv.Rows[i].Cells["Operation_Msg"].Value.ToString();

                    if (find(cell1, cell2))
                    {
                    }
                    else
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = (DataGridViewRow)result_dgv.Rows[i].Clone();
                        int intColIndex = 0;
                        foreach (DataGridViewCell cell in result_dgv.Rows[i].Cells)
                        {
                            row.Cells[intColIndex].Value = cell.Value;
                            intColIndex++;
                        }
                        final_dgv.Rows.Add(row);
                    }
                }
            } // try blokk vége
            catch (Exception ex) { MessageBox.Show(ex.Message); }



        }
        private bool find(string cell1, string cell2)
        {
            bool eredmeny = false;
            try
            {
                if (final_dgv.RowCount > 1)
                {
                    for (int i = 0; i < final_dgv.RowCount - 1; i++)
                    {
                        if ((final_dgv.Rows[i].Cells["ID"].Value.ToString() == cell1) && (final_dgv.Rows[i].Cells["Operation_Msg"].Value.ToString() == cell2))
                        {
                            eredmeny = true;
                            break;
                        }
                        else
                        {
                            eredmeny = false;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return eredmeny;


        }

        private void fpyellenorzese(string muvelet)
        {

            int sum = 0;
            double indtlr = 0;
            double indfpy = 0;
            int indokcount = 0;
            int indnokcount = 0;
            int osszdarab = 0;
            
                try
                {

                    string root = @"C:\temp";

                    System.IO.FileInfo file = new System.IO.FileInfo(root);
                    file.Directory.Create();

                    if (!System.IO.Directory.Exists(root))
                    {
                        System.IO.Directory.CreateDirectory(root);
                    }

                    for (int i = 0; i < result_dgv.RowCount - 1; i++)
                    {
                        if (result_dgv.Rows[i].Cells["Operation_ID"].Value.ToString() == muvelet)
                        {
                            osszdarab++;
                        }

                    }

                    for (int i = 0; i < final_dgv.RowCount - 1; i++)
                    {
                        if (final_dgv.Rows[i].Cells["Operation_ID"].Value.ToString() == muvelet)
                        {
                            // osszdarab++;
                            if (final_dgv.Rows[i].Cells["Result"].Value.ToString() == "OK")
                            {
                                indokcount++;
                            }
                            else if (final_dgv.Rows[i].Cells["Result"].Value.ToString() == "NOK")
                            {
                                indnokcount++;
                            }
                        }
                    }


                    sum = indokcount + indnokcount;
                    indtlr = (double)Math.Round(((double)(indnokcount * 100) / sum), 2);
                    indfpy = 100 - indtlr;

                    //MessageBox.Show("Operation_ID:" + Convert.ToString(muvelet) + "\nÖsszes teszt (újratesztel együtt): " + Convert.ToString(osszdarab) + "\nLetesztelt termékek száma: " + Convert.ToString(sum) + "\nNOK termékek száma: " + Convert.ToString(indnokcount) + "\nOk termékek száma: " + Convert.ToString(indokcount) + "\nFPY: " + Convert.ToString(indfpy) + "%\nTLR: " + Convert.ToString(indtlr) + "%");

                    /*********************CSV tartalmának beolvasása, új adatok hozzáfűzése***********************/

                    date = Convert.ToString(datepicker_from.Text).Substring(0, 10).Replace("/", "_");
                    tipus = tipus_combobox.Text.ToString();

                    List<string> newColumnData = new List<string>() { "D" };
                    List<string> lines = File.ReadAllLines(@"C:\temp\" + filename).ToList();
                    if (tableswitch == false)
                    {
                        lines[2] += Convert.ToString(muvelet) + ";";
                        lines[3] += Convert.ToString(sum) + ";";
                        lines[4] += Convert.ToString(indnokcount) + ";";
                        lines[5] += Convert.ToString(indfpy) + "%;";
                        lines[6] += Convert.ToString(indtlr) + "%;";
                    }
                    else if (tableswitch == true)
                    {

                        lines[7] += Convert.ToString(indnokcount) + ";";
                        lines[8] += Convert.ToString(indtlr) + "%;";
                    }
                    File.WriteAllLines(@"C:\temp\" + filename, lines);

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void eredmenyek_lekerdezese1(string lekerdezes1, DataGridView datagridview)
        {
            //pictureBox1.Visible = true;
            try
            {
                string query1 = lekerdezes1;

                SqlConnection cnn = new SqlConnection(connetionstringgen2);
                cnn.Open();
                SqlCommand objcmdm = new SqlCommand(query1, cnn);
                objcmdm.ExecuteNonQuery();
                SqlDataAdapter adpm = new SqlDataAdapter(objcmdm);
                DataTable dtm = new DataTable();
                adpm.Fill(dtm);
                datagridview.DataSource = dtm;
                datagridview.Sort(datagridview.Columns["Date"], ListSortDirection.Ascending);
                datagridview.Refresh();

                cnn.Close();
            }
            catch (Exception ex) { MessageBox.Show("Nem várt hiba a hiba oka, a következő:\n" + ex.Message); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableswitch = false;
            try
            {
                date = Convert.ToString(datepicker_from.Text).Substring(0, 10).Replace("/", "_");
                tipus = tipus_combobox.Text.ToString();
                filename = date + "_" + tipus + "_M.csv";
                /*****************Create CSV******************/
                if (!File.Exists(@"C:\temp\" + filename))
                {
                    string csvbase = tipus_combobox.Text.ToString() + "\r\n"
                      + Convert.ToString(datepicker_from.Text).Substring(0, 10) + ";Product OK on Labeling; Connector Screwing; Grounding Cable Screwing; Leakage Test; Product OK on Main board placement; Cable lug Screwing; Heatsink Screwing; Board screwing; Cover Screwing; Housing Leakage test; Soldering AOI; Gluing\r\n"
                      + "Operation_ID: ; \r\n"
                      + "Tesztelt darabszam: ; \r\n"
                      + "Elsore kiesett: ;\r\n"
                      + "FPY: ;\r\n"
                      + "TLR: ;\r\n"
                      + "Valos kieses: ;\r\n"
                      + "Valos kieses [%] : ;\r\n\r\n";
                    File.WriteAllText(@"C:\temp\" + filename, csvbase);
                }

                string[] house_assy = { "10401", "4010", "4020", "4040" };
                string[] assembly = { "10900", "9015", "9020", "9030", "9050", "9060" };
                string[] soldering = { "6080", "10614" };
                
                
                for (int j = 0; j < house_assy.Length; j++)
                {
                    tableswitch = false;
                    string query = "Select HeatSinkData.HeatSink_ID as ID, HeatSinkData.Workstation, HeatSinkData.Operation_ID, Operations.Operation_Msg, HeatSinkData.Result, HeatSinkData.Value, HeatSinkData.Limit_Min, HeatSinkData.Limit_Max, HeatSinkData.Date, HeatSinkData.Operator, HeatSinkData.Note  from [dbo].[HeatSinkData] JOIN [dbo].[Operations] ON HeatSinkData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HeatSinkData.HeatSink_ID=main.HeatSink_ID WHERE HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HeatSinkData.Operation_ID='" + house_assy[j] + "' UNION Select HousingData.Housing_ID as ID, HousingData.Workstation, HousingData.Operation_ID, Operations.Operation_Msg, HousingData.Result, HousingData.Value, HousingData.Limit_Min, HousingData.Limit_Max, HousingData.Date, HousingData.Operator, HousingData.Note  from [dbo].[HousingData] JOIN [dbo].[Operations] ON HousingData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HousingData.Housing_ID=main.Housing_ID WHERE HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HousingData.Operation_ID = '" + house_assy[j] + "'";
                    eredmenyek_lekerdezese1(query, result_dgv);
                    ismetlodeskiszurese();
                    fpyellenorzese(house_assy[j]);
                    tableswitch = true;
                    ismetlodeskiszurese();
                    fpyellenorzese(house_assy[j]);
                }
                for (int k = 0; k < assembly.Length; k++)
                {
                    tableswitch = false;
                    string query = "Select HeatSinkData.HeatSink_ID as ID, HeatSinkData.Workstation, HeatSinkData.Operation_ID, Operations.Operation_Msg, HeatSinkData.Result, HeatSinkData.Value, HeatSinkData.Limit_Min, HeatSinkData.Limit_Max, HeatSinkData.Date, HeatSinkData.Operator, HeatSinkData.Note  from [dbo].[HeatSinkData] JOIN [dbo].[Operations] ON HeatSinkData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HeatSinkData.HeatSink_ID=main.HeatSink_ID WHERE HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HeatSinkData.Operation_ID='" + assembly[k] + "' UNION Select HousingData.Housing_ID as ID, HousingData.Workstation, HousingData.Operation_ID, Operations.Operation_Msg, HousingData.Result, HousingData.Value, HousingData.Limit_Min, HousingData.Limit_Max, HousingData.Date, HousingData.Operator, HousingData.Note  from [dbo].[HousingData] JOIN [dbo].[Operations] ON HousingData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HousingData.Housing_ID=main.Housing_ID WHERE HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HousingData.Operation_ID = '" + assembly[k] + "'";
                    eredmenyek_lekerdezese1(query, result_dgv);
                    ismetlodeskiszurese();
                    fpyellenorzese(assembly[k]);
                    tableswitch = true;
                    ismetlodeskiszurese();
                    fpyellenorzese(assembly[k]);
                }
                for (int l = 0; l < soldering.Length; l++)
                {
                    tableswitch = false;
                    string query = "Select HeatSinkData.HeatSink_ID as ID, HeatSinkData.Workstation, HeatSinkData.Operation_ID, Operations.Operation_Msg, HeatSinkData.Result, HeatSinkData.Value, HeatSinkData.Limit_Min, HeatSinkData.Limit_Max, HeatSinkData.Date, HeatSinkData.Operator, HeatSinkData.Note  from [dbo].[HeatSinkData] JOIN [dbo].[Operations] ON HeatSinkData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HeatSinkData.HeatSink_ID=main.HeatSink_ID WHERE HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HeatSinkData.Operation_ID='" + soldering[l] + "' UNION Select HousingData.Housing_ID as ID, HousingData.Workstation, HousingData.Operation_ID, Operations.Operation_Msg, HousingData.Result, HousingData.Value, HousingData.Limit_Min, HousingData.Limit_Max, HousingData.Date, HousingData.Operator, HousingData.Note  from [dbo].[HousingData] JOIN [dbo].[Operations] ON HousingData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HousingData.Housing_ID=main.Housing_ID WHERE HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HousingData.Operation_ID = '" + soldering[l] + "'";
                    eredmenyek_lekerdezese1(query, result_dgv);
                    ismetlodeskiszurese();
                    fpyellenorzese(soldering[l]);
                    tableswitch = true;
                    ismetlodeskiszurese();
                    fpyellenorzese(soldering[l]);
                }
                MessageBox.Show(@"Az eredmények mentve lettek a C:\temp\ mappába a következő fájlnévvel: " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableswitch = false;
            try
            {
                date = Convert.ToString(datepicker_from.Text).Substring(0, 10).Replace("/", "_");
                tipus = tipus_combobox.Text.ToString();
                filename = date + "_" + tipus + "_T.csv";
                /*****************Create CSV******************/
                if (!File.Exists(@"C:\temp\" + filename))
                {
                    string csvbase = tipus_combobox.Text.ToString() + "\r\n"
                      + Convert.ToString(datepicker_from.Text).Substring(0, 10) + "\r\n"
                      + "Operation_ID: ; \r\n"
                      + "Tesztelt darabszam: ; \r\n"
                      + "Elsore kiesett: ;\r\n"
                      + "FPY: ;\r\n"
                      + "TLR: ;\r\n"
                      + "Valos kieses: ;\r\n"
                      + "Valos kieses [%] : ;\r\n\r\n";
                    File.WriteAllText(@"C:\temp\" + filename, csvbase);
                }

                string[] ops = { "12030", "12060", "10615", "10980", "10990", "11000" };

                for (int i = 0; i < ops.Length; i++)
                {
                    tableswitch = false;
                    //MessageBox.Show(ops[i]);

                    string query = "Select HeatSinkData.HeatSink_ID as ID, HeatSinkData.Workstation, HeatSinkData.Operation_ID, Operations.Operation_Msg, HeatSinkData.Result, HeatSinkData.Value, HeatSinkData.Limit_Min, HeatSinkData.Limit_Max, HeatSinkData.Date, HeatSinkData.Operator, HeatSinkData.Note  from [dbo].[HeatSinkData] JOIN [dbo].[Operations] ON HeatSinkData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HeatSinkData.HeatSink_ID=main.HeatSink_ID WHERE HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HeatSinkData.Operation_ID='" + ops[i] + "' UNION Select HousingData.Housing_ID as ID, HousingData.Workstation, HousingData.Operation_ID, Operations.Operation_Msg, HousingData.Result, HousingData.Value, HousingData.Limit_Min, HousingData.Limit_Max, HousingData.Date, HousingData.Operator, HousingData.Note  from [dbo].[HousingData] JOIN [dbo].[Operations] ON HousingData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HousingData.Housing_ID=main.Housing_ID WHERE HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HousingData.Operation_ID = '" + ops[i] + "'";
                    if (i == 0)
                    {
                        List<string> newColumnData = new List<string>() { "D" };
                        List<string> lines = File.ReadAllLines(@"C:\temp\" + filename).ToList();
                        lines[1] += ";Komponens AOI;Forrasztas AOI;PCBA Teszter;HiPOT Teszter;Burn-in Teszter;EOL Teszter";
                        File.WriteAllLines(@"C:\temp\" + filename, lines);
                    }
                    eredmenyek_lekerdezese1(query, result_dgv);
                    ismetlodeskiszurese();
                    fpyellenorzese(ops[i]);
                    tableswitch = true;
                    ismetlodeskiszurese();
                    fpyellenorzese(ops[i]);
                }
                MessageBox.Show(@"Az eredmények mentve lettek a C:\temp\ mappába a következő fájlnévvel: " + filename);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
    }
    }
}

                MessageBox.Show(@"Az eredmények mentve lettek a C:\temp\ mappába a következő fájlnévvel: " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableswitch = false;
            try
            {
                date = Convert.ToString(datepicker_from.Text).Substring(0, 10).Replace("/", "_");
                tipus = tipus_combobox.Text.ToString();
                filename = date + "_" + tipus + "_T.csv";
                /*****************Create CSV******************/
                if (!File.Exists(@"C:\temp\" + filename))
                {
                    string csvbase = tipus_combobox.Text.ToString() + "\r\n"
                      + Convert.ToString(datepicker_from.Text).Substring(0, 10) + "\r\n"
                      + "Operation_ID: ; \r\n"
                      + "Tesztelt darabszam: ; \r\n"
                      + "Elsore kiesett: ;\r\n"
                      + "FPY: ;\r\n"
                      + "TLR: ;\r\n"
                      + "Valos kieses: ;\r\n"
                      + "Valos kieses [%] : ;\r\n\r\n";
                    File.WriteAllText(@"C:\temp\" + filename, csvbase);
                }

                string[] ops = { "12030", "12060", "10615", "10980", "10990", "11000" };

                for (int i = 0; i < ops.Length; i++)
                {
                    tableswitch = false;
                    //MessageBox.Show(ops[i]);

                    string query = "Select HeatSinkData.HeatSink_ID as ID, HeatSinkData.Workstation, HeatSinkData.Operation_ID, Operations.Operation_Msg, HeatSinkData.Result, HeatSinkData.Value, HeatSinkData.Limit_Min, HeatSinkData.Limit_Max, HeatSinkData.Date, HeatSinkData.Operator, HeatSinkData.Note  from [dbo].[HeatSinkData] JOIN [dbo].[Operations] ON HeatSinkData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HeatSinkData.HeatSink_ID=main.HeatSink_ID WHERE HeatSinkData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HeatSinkData.Operation_ID='" + ops[i] + "' UNION Select HousingData.Housing_ID as ID, HousingData.Workstation, HousingData.Operation_ID, Operations.Operation_Msg, HousingData.Result, HousingData.Value, HousingData.Limit_Min, HousingData.Limit_Max, HousingData.Date, HousingData.Operator, HousingData.Note  from [dbo].[HousingData] JOIN [dbo].[Operations] ON HousingData.Operation_ID=Operations.Operation_ID INNER JOIN [dbo].[Main] ON HousingData.Housing_ID=main.Housing_ID WHERE HousingData.Date BETWEEN '" + datepicker_from.Text + "' AND '" + datepicker_to.Text + "' AND Main.Type = '" + Convert.ToString(tipus_combobox.Text) + "' AND HousingData.Operation_ID = '" + ops[i] + "'";
                    if (i == 0)
                    {
                        List<string> newColumnData = new List<string>() { "D" };
                        List<string> lines = File.ReadAllLines(@"C:\temp\" + filename).ToList();
                        lines[1] += ";Komponens AOI;Forrasztas AOI;PCBA Teszter;HiPOT Teszter;Burn-in Teszter;EOL Teszter";
                        File.WriteAllLines(@"C:\temp\" + filename, lines);
                    }
                    eredmenyek_lekerdezese1(query, result_dgv);
                    ismetlodeskiszurese();
                    fpyellenorzese(ops[i]);
                    tableswitch = true;
                    ismetlodeskiszurese();
                    fpyellenorzese(ops[i]);
                }
                MessageBox.Show(@"Az eredmények mentve lettek a C:\temp\ mappába a következő fájlnévvel: " + filename);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
    }
    }
}
