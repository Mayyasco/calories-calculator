using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;int f;
        public Form1()
        {
            InitializeComponent();
            f = 0;
            comboBox1.Text = "«·›ÿÊ—";
            for (int i = 0; i < 4; i++)
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < 7; i++)
                dataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
            monthCalendar1.Focus();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox8.Text = monthCalendar1.SelectionRange.Start.Year.ToString();
            textBox9.Text = monthCalendar1.SelectionRange.Start.Day.ToString();
            textBox10.Text = monthCalendar1.SelectionRange.Start.Month.ToString();
            textBox1.Text = monthCalendar1.SelectionRange.Start.DayOfWeek.ToString();
            monthCalendar1.Visible = false;
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }
        private void refresh_all()
        {
          
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "select * from list_food";
            MySqlCommand cmd;
            cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //copy data to lists
            int columns = 2;
            List<string>[] list = new List<string>[columns];
            for (int i = 0; i < columns; i++)
                list[i] = new List<string>();
            while (dataReader.Read())
            {
                for (int j = 0; j < columns; j++)
                {
                    list[j].Add(dataReader[j] + "");
                }
            }
            dataReader.Close();
            //close connection
            connection.Close();
            comboBox3.Items.Clear(); comboBox2.Items.Clear();
            comboBox5.Items.Clear(); comboBox4.Items.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Clear();
            textBox2.Text = ""; textBox3.Text = "";
            textBox6.Text = ""; textBox7.Text = "";
            button2.Enabled = false;
            DataGridViewComboBoxColumn cmbCol = (DataGridViewComboBoxColumn) dataGridView1.Columns[0];
            cmbCol.Items.Clear();
            for (int k = 0; k < list[0].Count; k++)
            {
                cmbCol.Items.Add(list[0][k]);
                comboBox3.Items.Add(list[0][k]);
                comboBox2.Items.Add(list[1][k]);
                comboBox5.Items.Add(list[0][k]);
                comboBox4.Items.Add(list[1][k]);
            }
            comboBox3.Text = list[0][0];
            comboBox2.Text = list[1][0];
            comboBox5.Text = list[0][0];
            comboBox4.Text = list[1][0];
            textBox12.Text = list[0][0];
            textBox11.Text = list[1][0];
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
            float o;
            if (textBox4.Text.Trim().Length < 3) { MessageBox.Show("ÌÃ» «‰ ÌﬂÊ‰ «”„ «·’‰› «ﬂÀ— „‰ Õ—›Ì‰"); return; }
            if (textBox5.Text.Trim().Length < 1) { MessageBox.Show("«ﬂ » «·ﬁÌ„… ›Ì Œ«‰… ”.·.€"); return; }
            if (!float.TryParse(textBox5.Text.Trim(), out o)) { MessageBox.Show("«ﬂ » «·ﬁÌ„… ›Ì Œ«‰… ”.·.€ »«·‘ﬂ· «·’ÕÌÕ"); return; }
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "";
            MySqlCommand cmd;
            query = "INSERT INTO list_food VALUES ('" + textBox4.Text + "','" + textBox5.Text + "')";
            //excute query
            cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();            
            connection.Close();            
            textBox4.Text = ""; textBox5.Text = "";
            refresh_all();
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refresh_all();
            textBox8.Text = DateTime.Now.Year.ToString();
            textBox9.Text = DateTime.Now.Day.ToString();
            textBox10.Text = DateTime.Now.Month.ToString();
            textBox1.Text = DateTime.Now.DayOfWeek.ToString();
            //------------------
            textBox16.Text = DateTime.Now.Year.ToString();
            textBox15.Text = DateTime.Now.Day.ToString();
            textBox14.Text = DateTime.Now.Month.ToString();
            textBox13.Text = DateTime.Now.DayOfWeek.ToString();
            //------------------
            textBox20.Text = DateTime.Now.Year.ToString();
            textBox19.Text = DateTime.Now.Day.ToString();
            textBox18.Text = DateTime.Now.Month.ToString();
            textBox17.Text = DateTime.Now.DayOfWeek.ToString();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex==0)
            {
                if (f == 0)
                {
                    ComboBox cmbprocess = e.Control as ComboBox;
                    cmbprocess.TextChanged += new EventHandler(grvcmbProcess_SelectedIndexChanged);
                    f = 0;
                    
                }
            }
           
        }
        private void grvcmbProcess_SelectedIndexChanged(object sender, EventArgs e)
        {  try{
            int ii=dataGridView1.CurrentCell.RowIndex;
            ComboBox cmb = (ComboBox)sender;
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "select cpg from list_food where food='" + cmb.Text + "'";
            MySqlCommand cmd;
            cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //copy data to lists
            int columns = 1;
            List<string>[] list = new List<string>[columns];
            for (int i = 0; i < columns; i++)
                list[i] = new List<string>();
            while (dataReader.Read())
            {
                for (int j = 0; j < columns; j++)
                {
                    list[j].Add(dataReader[j] + "");
                }
            }
            dataReader.Close();
            //close connection
            connection.Close();
            //string x = list[0][0];
            if (list[0].Count > 0)
            {
                dataGridView1.Rows[ii].Cells[1].Value = list[0][0];
                cellend(ii, 2);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }

        private void cellend(int r,int c)
        {try{
            if (c == 2)
            {
                int i = r;
                string[,] dg = new string[dataGridView1.Rows.Count , 4];
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)//row
                    for (int k = 0; k < 4; k++)//column
                    {
                        if (!string.IsNullOrEmpty(dataGridView1.Rows[j].Cells[k].Value + ""))
                            dg[j, k] = dataGridView1.Rows[j].Cells[k].Value.ToString();
                        else dg[j, k] = "";
                    }

                float s1 = 0, s2 = 0, ff = 0;
                if (!string.IsNullOrEmpty(dg[i, 2]))
                    if (float.TryParse(dg[i, 2], out ff))
                        s1 = float.Parse(dg[i, 2]);
                if (!string.IsNullOrEmpty(dg[i, 1]))
                    if (float.TryParse(dg[i, 1], out ff))
                        s2 = float.Parse(dg[i, 1]);
                dataGridView1.Rows[i].Cells[3].Value = s1 * s2;
                dg[i, 3] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                float s3 = 0, s4 = 0, sum1 = 0, sum2 = 0;
                for (int jj = 0; jj < dataGridView1.Rows.Count - 1; jj++)
                {
                    if (!string.IsNullOrEmpty(dg[jj, 2]))
                        if (float.TryParse(dg[jj, 2], out ff))
                            s3 = float.Parse(dg[jj, 2]);
                    if (!string.IsNullOrEmpty(dg[jj, 3]))
                        if (float.TryParse(dg[jj, 3], out ff))
                            s4 = float.Parse(dg[jj, 3]);
                    sum1 = sum1 + s3;
                    sum2 = sum2 + s4;
                    textBox2.Text = sum1.ToString();
                    textBox3.Text = sum2.ToString();
                }

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {try{
            cellend(e.RowIndex, e.ColumnIndex);
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            //validation
                //if (dataGridView1.Rows.Count < 2) { MessageBox.Show("·« ÌÊÃœ „⁄·Ê„« "); return; }
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                    if (string.IsNullOrEmpty(dataGridView1.Rows[j].Cells[0].Value + "") || string.IsNullOrEmpty(dataGridView1.Rows[j].Cells[2].Value + ""))
                    {
                        int h = j + 1;
                        MessageBox.Show("·œÌﬂ „⁄·Ê„… ‰«ﬁ’… ›Ì «·”ÿ—"+" : "+h.ToString()); return;
                    }
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "";
            string d = textBox6.Text;
            MySqlCommand cmd;
            query = "DELETE FROM meal WHERE meal='" + textBox7.Text + "' and date='" + d + "'";
            //excute query
            cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            //-----------------------------
            connection.Open();
            string s1="", s2="", s3="", s4="";
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                s1 = d;
                s2 = textBox7.Text;
                s3 = dataGridView1.Rows[i].Cells[0].Value.ToString();
                s4 = dataGridView1.Rows[i].Cells[2].Value.ToString();
                query = "INSERT INTO meal VALUES ('" + s1 + "', '" + s2 + "', '" + s3 + "', '" + s4 + "' )";
                cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            //-----------------------------
            connection.Close();
            dataGridView1.Rows.Clear();
            textBox2.Text = ""; textBox3.Text = "";
            textBox6.Text = ""; textBox7.Text = "";
            button2.Enabled = false;
            dataGridView1.Enabled = false;
            MessageBox.Show(" „ «·Õ›Ÿ »‰Ã«Õ");
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }

        private void button1_Click(object sender, EventArgs e)
        {try{
            dataGridView1.Rows.Clear();
            textBox6.Text = textBox9.Text + "/" + textBox10.Text + "/" + textBox8.Text;
            textBox7.Text = comboBox1.Text;
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "select food,w from meal where date='" + textBox6.Text + "' and meal ='" + textBox7.Text + "'";
            MySqlCommand cmd;
            cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //copy data to lists
            int columns = 2;
            List<string>[] list = new List<string>[columns];
            for (int i = 0; i < columns; i++)
                list[i] = new List<string>();
            while (dataReader.Read())
            {
                for (int j = 0; j < columns; j++)
                {
                    list[j].Add(dataReader[j] + "");
                }
            }
            dataReader.Close();
            
            for (int i = 0; i < list[0].Count; i++)
            {
                dataGridView1.Rows.Add(list[0][i]);
                dataGridView1.Rows[i].Cells[2].Value = list[1][i];
                //--------------------------------------------
                query = "select cpg from list_food where food='" + list[0][i] + "'";
               cmd = new MySqlCommand(query, connection);
               dataReader = cmd.ExecuteReader();
                columns = 1;
               List<string>[] list1 = new List<string>[columns];
               for (int ii = 0; ii < columns; ii++)
                   list1[ii] = new List<string>();
               while (dataReader.Read())
               {
                   for (int j = 0; j < columns; j++)
                   {
                       list1[j].Add(dataReader[j] + "");
                   }
               }
               dataReader.Close();
               if (list1[0].Count > 0)
               {
                   dataGridView1.Rows[i].Cells[1].Value = list1[0][0];
               }
               //--------------------------------------------
               cellend(i, 2);
            }
            
            button2.Enabled= true;
            dataGridView1.Enabled = true;
            connection.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i=comboBox3.SelectedIndex;
            comboBox2.Text=comboBox2.Items[i].ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try{string s=check(comboBox3.Text);
            if (s != "no") { MessageBox.Show(s); return; }
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "";
            MySqlCommand cmd;
            query = "DELETE FROM list_food WHERE food='" + comboBox3.Text + "'";
            //excute query
            cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            //-----------------------------
            refresh_all();
            //-----------------------------
            MessageBox.Show(" „ «·Õ–› »‰Ã«Õ");
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }

        

        string check(string food)
        {
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "select meal,date from meal where food='"+food+"'";
            MySqlCommand cmd;
            cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //copy data to lists
            int columns = 2;
            List<string>[] list = new List<string>[columns];
            for (int i = 0; i < columns; i++)
                list[i] = new List<string>();
            int j = 0;
            while (dataReader.Read())
            {
                for ( j = 0; j < columns; j++)
                {
                    list[j].Add(dataReader[j] + "");
                }
            }
            dataReader.Close();
            if (list[0].Count == 0) return "no";
            string s = "·« Ì„ﬂ‰ «·Õ–› ·«‰ Â–« «·’‰› „ÊÃÊœ ›Ì :" + Environment.NewLine;
            for (j = 0; j < list[0].Count; j++)
            {
                s = s + list[0][j] + " : " + list[1][j] + Environment.NewLine;
            }
            return s;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox5.SelectedIndex;
            comboBox4.Text = comboBox4.Items[i].ToString();
            textBox12.Text = comboBox5.Text;
            textBox11.Text = comboBox4.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {try{
            float o;
            if (textBox12.Text.Trim().Length < 3) { MessageBox.Show("ÌÃ» «‰ ÌﬂÊ‰ «”„ «·’‰› «ﬂÀ— „‰ Õ—›Ì‰"); return; }
            if (textBox11.Text.Trim().Length < 1) { MessageBox.Show("«ﬂ » «·ﬁÌ„… ›Ì Œ«‰… ”.·.€"); return; }
            if (!float.TryParse(textBox11.Text.Trim(), out o)) { MessageBox.Show("«ﬂ » «·ﬁÌ„… ›Ì Œ«‰… ”.·.€ »«·‘ﬂ· «·’ÕÌÕ"); return; }
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "";
            MySqlCommand cmd;
            query = "UPDATE list_food SET food = '" + textBox12.Text + "' , cpg = '" + textBox11.Text + "' WHERE food='" + comboBox5.Text+"'";
            //excute query
            cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            //-----------------------------
            connection.Open();
            query = "UPDATE meal SET food = '" + textBox12.Text +"' WHERE food='" + comboBox5.Text + "'";
            //excute query
            cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            //-----------------------------
            refresh_all();
            //-----------------------------
            MessageBox.Show(" „ «· ⁄œÌ· »‰Ã«Õ");
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comparedate(/*s*/"29/12/2013",/*e*/ "3/1/2014",/*n*/ "1/1/2014").ToString());
            try{
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "select * from meal";
            MySqlCommand cmd;
            cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //copy data to lists
            int columns = 4;
            List<string>[] list = new List<string>[columns];
            for (int i = 0; i < columns; i++)
                list[i] = new List<string>();
            int j = 0;
            while (dataReader.Read())
            {
                for (j = 0; j < columns; j++)
                {
                    list[j].Add(dataReader[j] + "");
                }
            }
            dataReader.Close();
            connection.Close();
            dataGridView2.Rows.Clear();
            float sum1 = 0, sum2 = 0;
            string s = "", end = "";
            s = textBox15.Text + "/" + textBox14.Text + "/" + textBox16.Text;
            end = textBox19.Text + "/" + textBox18.Text + "/" + textBox20.Text;
            int y = 0;
            for ( j = 0; j < list[0].Count; j++)
            {
                if (comparedate(s, end, list[0][j]))
                {
                    //MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
                    dataGridView2.Rows.Add(list[0][j]);//«· «—ÌŒ
                    string[] date1 = new string[3];
                    date1 = list[0][j].Split('/');
                    DateTime dateValue = new DateTime(int.Parse(date1[2]), int.Parse(date1[1]), int.Parse(date1[0]));
                    dataGridView2.Rows[y].Cells[1].Value = dateValue.ToString("dddd"); //«·ÌÊ„
                    dataGridView2.Rows[y].Cells[2].Value = list[1][j];//«·ÊÃ»…
                    dataGridView2.Rows[y].Cells[3].Value = list[2][j];//«·’‰›
                    string h = find_cpg(list[2][j]);
                    dataGridView2.Rows[y].Cells[4].Value = h;//”.·.€
                    dataGridView2.Rows[y].Cells[5].Value = list[3][j];//«·ﬂ„Ì…
                    dataGridView2.Rows[y].Cells[6].Value = float.Parse(list[3][j]) * float.Parse(h);//«·„Ã„Ê⁄
                    sum1 = sum1 + float.Parse(list[3][j]);
                    sum2 = sum2 + float.Parse(list[3][j]) * float.Parse(h);
                    y++;
                }
            }
            textBox22.Text = sum1.ToString();
            textBox21.Text = sum2.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show("ÌÊÃœ ·œÌﬂ „‘ﬂ·…");
        }
        }
        private bool comparedate(string s, string e,string n)
        {
            string[] date1 = new string[3];
            string[] date2 = new string[3];
            string[] date3 = new string[3];

            date1 = s.Split('/');
            date2 = e.Split('/');
            date3 = n.Split('/');
            if (sub(date3, date1) == "+" && sub(date2, date3) == "+") return true; else return false;
            
        }

        private string sub(string[] n1, string[] n2)
        {
            if ((int.Parse(n1[2]) - int.Parse(n2[2])) < 0) return "-";
            if ((int.Parse(n1[2]) - int.Parse(n2[2])) > 0) return "+";

            if ((int.Parse(n1[1]) - int.Parse(n2[1])) < 0) return "-";
            if ((int.Parse(n1[1]) - int.Parse(n2[1])) > 0) return "+";

            if ((int.Parse(n1[0]) - int.Parse(n2[0])) < 0) return "-";
            if ((int.Parse(n1[0]) - int.Parse(n2[0])) > 0) return "+";
            return "+";
        }
        private string find_cpg(string p)
        {
            //connect to database
            connection = new MySqlConnection("SERVER=" + "" + ";DATABASE=" + "cal" + ";UID=root;PASSWORD=100;");
            connection.Open();
            string query = "select cpg from list_food where food='"+p+"'";
            MySqlCommand cmd;
            cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            //copy data to lists
            int columns = 1;
            List<string>[] list = new List<string>[columns];
            for (int i = 0; i < columns; i++)
                list[i] = new List<string>();
            int j = 0;
            while (dataReader.Read())
            {
                for (j = 0; j < columns; j++)
                {
                    list[j].Add(dataReader[j] + "");
                }
            }
            dataReader.Close();
            connection.Close();
            return list[0][0];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            monthCalendar2.Visible = true;
            monthCalendar2.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            monthCalendar3.Visible = true;
            monthCalendar3.Focus();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            textBox16.Text = monthCalendar2.SelectionRange.Start.Year.ToString();
            textBox15.Text = monthCalendar2.SelectionRange.Start.Day.ToString();
            textBox14.Text = monthCalendar2.SelectionRange.Start.Month.ToString();
            textBox13.Text = monthCalendar2.SelectionRange.Start.DayOfWeek.ToString();
            monthCalendar2.Visible = false;
        }

        private void monthCalendar2_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar2.Visible = false;
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            textBox20.Text = monthCalendar3.SelectionRange.Start.Year.ToString();
            textBox19.Text = monthCalendar3.SelectionRange.Start.Day.ToString();
            textBox18.Text = monthCalendar3.SelectionRange.Start.Month.ToString();
            textBox17.Text = monthCalendar3.SelectionRange.Start.DayOfWeek.ToString();
            monthCalendar3.Visible = false;
        }

        private void monthCalendar3_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar3.Visible = false;
        }

    }
}