using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Course.Parse;
using Course.Parse.Ps4Disk;
using System.Threading;

namespace Course
{
    public partial class Productcs : Form
    {
        public string user { get; set; }
        public bool admin { get; set; }
        public Productcs(string login)
        {
            user = login;
            if (user == "admin") admin = true;
            else admin = false;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            string selecter = "SELECT product_id as 'Код товара', product_name as 'Название' , status as 'Статус', stock as 'На складе', platform as 'Платформа', storage as 'Объём памяти', date_type as 'Тип данных', disk_type as 'Тип диска', country as 'Страна-производитель', count as 'Дисков в упаковке', price as 'Цена' FROM Product";
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
            SqlDataAdapter dd = new SqlDataAdapter();
            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
            if (count == null)
            {
                count = new int[dataGridView1.RowCount];
                for (int i = 0; i < count.Length; i++)
                {
                    count[i] = 0;
                }
            }
            if (user == "admin")
            {
                button6.Visible = true;
                button6.Enabled = true;
                button8.Visible = true;
                button8.Enabled = true;
                button16.Enabled = false;
                button16.Visible = false;
                button17.Enabled = false;
                button17.Visible = false;
            }

            parser = new ParserWorker<string[]>(new PS4DiskParser());
            parser.OnCompeted += Parser_Oncoplited;
            parser.OnNewData += Parser_OnNewData;


        }

        private void Parser_Oncoplited(object obj)
        {
            MessageBox.Show("Всё");
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            string selecter = "SELECT MAX(preorder_id) FROM Preorder";
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            int g = (int)dt.Rows[0][0] + 1;
            sqlconn.Close();

            for (int i = 0; i < arg2.Length/2; i++)
            {
                if (arg2[i].Contains("БУ")) continue;
                for(int j = 0; j < arg2[i].Length; j++)
                {
                    if (arg2[i + arg2.Length / 2][j] == ' ')
                    {
                        arg2[i + arg2.Length / 2] = arg2[i + arg2.Length / 2].Substring(0, j);
                        break;
                    }
                }
                arg2[i] = arg2[i].Replace("RU", "");
                arg2[i] = arg2[i].Replace("'", "");

                selecter = "SELECT * FROM Product WHERE product_name = '" + arg2[i] + "'";
                sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                d = new SqlDataAdapter(selecter, sqlconn);

                dt = new DataTable();
                d.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    continue;
                }
                sqlconn.Close();

                selecter = "SELECT * FROM Preorder WHERE Preorder_Name = '" +arg2[i] +"'";
                sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                d = new SqlDataAdapter(selecter, sqlconn);

                dt = new DataTable();
                d.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    preorderTableAdapter.Insert(g, arg2[i], Convert.ToInt32(arg2[i + arg2.Length / 2]) + 100, 0);
                    g++;
                }
                else
                {
                    preorderTableAdapter.UpdateQuery(Convert.ToInt32(arg2[i + arg2.Length / 2]) + 100, arg2[i]);
                }
                sqlconn.Close();

                

                selecter = "SELECT preorder_id as 'код товара', preorder_name as 'Название', price as 'Цена', count as 'Кол-во Предзаказов' FROM Preorder";;
                sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                d = new SqlDataAdapter(selecter, sqlconn);

                dt = new DataTable();
                d.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlconn.Close();

            }
        }

        bool chet = true;

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new MainMenu(user);
            this.Close();
            Program.Context.MainForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(chet)
            {
                
                button10.Enabled = false;
                button9.Enabled = false;
                textBox1.Enabled = false;
                
                textBox1.Text = "";

                dataGridView1.Width = 1090;


                if (user == "admin")
                {
                    button6.Visible = false;
                    button6.Enabled = false;
                    button8.Visible = false;
                    button8.Enabled = false;
                    button7.Enabled = true;
                    button7.Visible = true;
                    button11.Enabled = true;
                    button11.Visible = true;
                    button12.Enabled = true;
                    button12.Visible = true;
                    button13.Enabled = true;
                    button13.Visible = true;
                }

               
                button15.Enabled = false;
                button15.Visible = false;
                button16.Enabled = false;
                button16.Visible = false;
                if(user != "admin")
                {
                    button17.Enabled = true;
                    button17.Visible = true;
                }


                button2.Text = "Товар";
                string selecter = "SELECT preorder_id as 'код товара', preorder_name as 'Название', price as 'Цена', count as 'Кол-во Предзаказов' FROM Preorder";;
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

                DataTable dt = new DataTable();
                d.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlconn.Close();
            }
            else
            {
                
                button10.Enabled = true;
                button9.Enabled = true;

                button3.Enabled = true;
                button3.Visible = true;
                textBox1.Enabled = true;

                if (user == "admin")
                {
                    button6.Visible = true;
                    button6.Enabled = true;
                    button8.Visible = true;
                    button8.Enabled = true;
                    button7.Enabled = false;
                    button7.Visible = false;
                }

                button11.Enabled = false;
                button11.Visible = false;
                button12.Enabled = false;
                button12.Visible = false;
                button13.Enabled = false;
                button13.Visible = false;
                button15.Enabled = true;
                button15.Visible = true;
                if(user != "admin")
                {
                    button16.Enabled = true;
                    button16.Visible = true;
                }
                button17.Enabled = false;
                button17.Visible = false;



                button2.Text = "Предзаказ"; 
                string selecter = "SELECT product_id as 'Код товара', product_name as 'Название' , status as 'Статус', stock as 'На складе', platform as 'Платформа', storage as 'Объём памяти', date_type as 'Тип данных', disk_type as 'Тип диска', country as 'Страна-производитель', count as 'Дисков в упаковке', price as 'Цена' FROM Product";;
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

                DataTable dt = new DataTable();
                d.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlconn.Close();
                
            }
            chet = !chet;
        }

        private void Productcs_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.POU". При необходимости она может быть перемещена или удалена.
            this.pOUTableAdapter.Fill(this.diskCourseDataSet.POU);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.Preorder". При необходимости она может быть перемещена или удалена.
            this.preorderTableAdapter.Fill(this.diskCourseDataSet.Preorder);
            {
                string selecter = "SELECT Platform, COUNT(product_id) FROM Product Group by platform ORDER BY Platform";
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
                SqlDataAdapter dd = new SqlDataAdapter();
                DataTable dt = new DataTable();
                d.Fill(dt);

                checkBox9.Text += "(" + dt.Rows[3].ItemArray[1].ToString() + ")";
                checkBox10.Text += "(" + dt.Rows[1].ItemArray[1].ToString() + ")";
                checkBox11.Text += "(" + dt.Rows[2].ItemArray[1].ToString() + ")";
                checkBox12.Text += "(" + dt.Rows[0].ItemArray[1].ToString() + ")";
                checkBox13.Text += "(" + dt.Rows[4].ItemArray[1].ToString() + ")";
                sqlconn.Close();
                
            }

            {
                string selecter = "SELECT date_type, COUNT(product_id) FROM Product Group by date_type ORDER BY date_type";
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
                SqlDataAdapter dd = new SqlDataAdapter();
                DataTable dt = new DataTable();
                d.Fill(dt);

                checkBox1.Text += "(" + dt.Rows[1].ItemArray[1].ToString() + ")";
                checkBox2.Text += "(" + dt.Rows[0].ItemArray[1].ToString() + ")";
                checkBox3.Text += "(" + dt.Rows[2].ItemArray[1].ToString() + ")";
                checkBox4.Text += "(" + dt.Rows[3].ItemArray[1].ToString() + ")";
                sqlconn.Close();
            }

            {
                string selecter = "SELECT  disk_type, COUNT(product_id) FROM Product Group by  disk_type ORDER BY  disk_type";
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
                SqlDataAdapter dd = new SqlDataAdapter();
                DataTable dt = new DataTable();
                d.Fill(dt);

                checkBox8.Text += "(" + dt.Rows[1].ItemArray[1].ToString() + ")";
                checkBox7.Text += "(" + dt.Rows[2].ItemArray[1].ToString() + ")";
                checkBox6.Text += "(" + dt.Rows[0].ItemArray[1].ToString() + ")";
                checkBox5.Text += "(" + dt.Rows[3].ItemArray[1].ToString() + ")";
                sqlconn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            button7.Visible = false;
            button3.Enabled = false;
            button3.Visible = false;
            button9.Enabled = false;
            textBox1.Enabled = false;
                textBox1.Text = "";
            button10.Enabled = false;
            button15.Enabled = false;
            button15.Visible = false;
            button16.Enabled = false;
            button16.Visible = false;
            
            button17.Enabled = false;
            button17.Visible = false;
            

            dataGridView1.Width = 1090;

            
            chet = false;

            if (user == "admin")
            {
                button6.Visible = false;
                button6.Enabled = false;
                button8.Visible = false;
                button8.Enabled = false;
            }

            button11.Enabled = false;
            button11.Visible = false;
            button12.Enabled = false;
            button12.Visible = false;
            button13.Enabled = false;
            button13.Visible = false;

            button2.Text = "Товар";
            string selecter = "SELECT top 10 product_name as ' Название товара', sum(Sales.count) AS 'Продажи' FROM Product, Sales, Receipt WHERE Product.product_id = Sales.product_id AND Sales.receipt_id = Receipt.receipt_id AND (YEAR(GETDATE()) - YEAR(date_time)) * 365 + (MONTH(GETDATE()) - MONTH(date_time)) * 30 + (DAY(GETDATE()) - DAY(date_time)) <=30 Group by product_name, Product.product_id ORDER BY [Продажи] DESC";
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            string selecter = "SELECT product_id as 'Код товара', product_name as 'Название' , status as 'Статус', stock as 'На складе', platform as 'Платформа', storage as 'Объём памяти', date_type as 'Тип данных', disk_type as 'Тип диска', country as 'Страна-производитель', count as 'Дисков в упаковке', price as 'Цена' FROM Product ";

            selecter += "WHERE 1=1 ";

            if (checkBox14.Checked) selecter += " AND status = 'В наличии'";

            if (textBox2.Text != "0") selecter += " AND storage BETWEEN " + textBox2.Text + " AND " + textBox3.Text + " AND price BETWEEN " + textBox4.Text + " AND " + textBox5.Text;
            else selecter  += " AND (storage <=" + textBox3.Text + " OR storage is NULL) AND price BETWEEN " + textBox4.Text + " AND " + textBox5.Text;




            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked || checkBox13.Checked)
            {
                

                

               
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
                {
                    selecter += " AND ( 1=0 ";

                    if (checkBox1.Checked)
                    {
                        selecter += " OR date_type = 'Игра' ";
                    }
                    if (checkBox2.Checked)
                    {
                        selecter += " OR date_type = 'Балванка' ";
                    }
                    if (checkBox3.Checked)
                    {
                        selecter += " OR date_type = 'Музыка' ";
                    }
                    if (checkBox4.Checked)
                    {
                        selecter += " OR date_type = 'Фильм' ";
                    }

                    selecter += ")  ";
                }





                if (checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked)
                {
                    selecter += " AND ( 1=0 ";
                    if (checkBox5.Checked)
                    {
                        selecter += " OR disk_type = 'Винил' ";
                    }
                    if (checkBox6.Checked)
                    {
                        selecter += " OR disk_type = 'Blu-Ray' ";
                    }
                    if (checkBox7.Checked)
                    {
                        selecter += " OR disk_type = 'DVD' ";
                    }
                    if (checkBox8.Checked)
                    {
                        selecter += " OR disk_type = 'CD' ";
                    }
                    selecter += ") ";
                }

                if (checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked || checkBox13.Checked)
                {
                    selecter += " AND ( 1=0 ";
                    if (checkBox9.Checked)
                    {
                        selecter += " OR platform = 'PS4' ";
                    }
                    if (checkBox10.Checked)
                    {
                        selecter += " OR platform = 'CD-Дисковод' ";
                    }
                    if (checkBox11.Checked)
                    {
                        selecter += " OR platform = 'DVD-Дисковод' ";
                    }
                    if (checkBox12.Checked)
                    {
                        selecter += " OR platform = 'Blu-Ray-Дисковод' ";
                    }
                    if (checkBox13.Checked)
                    {
                        selecter += " OR platform = 'Винил' ";
                    }

                    selecter += ")";
                }

                

            }

            

                if (radioButton1.Checked)
                {
                    if (radioButton3.Checked) selecter += " ORDER BY product_name ASC ";
                    if (radioButton4.Checked) selecter += " ORDER BY storage ASC ";
                    if (radioButton5.Checked) selecter += " ORDER BY price ASC ";
                }
                if (radioButton2.Checked)
                {
                    if (radioButton3.Checked) selecter += " ORDER BY product_name DESC ";
                    if (radioButton4.Checked) selecter += " ORDER BY storage DESC ";
                    if (radioButton5.Checked) selecter += " ORDER BY price DESC ";
                }
            
            
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            textBox1.Text = "";
            radioButton1.Checked = false;  
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            checkBox1.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false; 
            checkBox13.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox14.Checked = false;

            trackBar1.Value = 100;
            trackBar4.Value = 0;
            trackBar2.Value = 3000;
            trackBar3.Value =  0;
            textBox2.Text = "0";
            textBox3.Text = "100";
            textBox4.Text = "0";
            textBox5.Text = "3000";
            string selecter = "SELECT product_id as 'Код товара', product_name as 'Название' , status as 'Статус', stock as 'На складе', platform as 'Платформа', storage as 'Объём памяти', date_type as 'Тип данных', disk_type as 'Тип диска', country as 'Страна-производитель', count as 'Дисков в упаковке', price as 'Цена' FROM Product";;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if(trackBar4.Value > trackBar1.Value) trackBar4.Value = trackBar1.Value;
            textBox2.Text = trackBar4.Value.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar4.Value > trackBar1.Value) trackBar1.Value = trackBar4.Value;
            textBox3.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value < trackBar3.Value) trackBar2.Value = trackBar3.Value;
            textBox5.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value < trackBar3.Value) trackBar3.Value = trackBar2.Value;
            textBox4.Text = trackBar3.Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EditPForm editPForm = new EditPForm();
            editPForm.ShowDialog();
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            string selecter = "SELECT product_id as 'Код товара', product_name as 'Название' , status as 'Статус', stock as 'На складе', platform as 'Платформа', storage as 'Объём памяти', date_type as 'Тип данных', disk_type as 'Тип диска', country as 'Страна-производитель', count as 'Дисков в упаковке', price as 'Цена' FROM Product";;
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
            SqlDataAdapter dd = new SqlDataAdapter();
            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            string st = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();


            EditPForm editPForm = new EditPForm(st);
            editPForm.ShowDialog();

            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            string selecter = "SELECT product_id as 'Код товара', product_name as 'Название' , status as 'Статус', stock as 'На складе', platform as 'Платформа', storage as 'Объём памяти', date_type as 'Тип данных', disk_type as 'Тип диска', country as 'Страна-производитель', count as 'Дисков в упаковке', price as 'Цена' FROM Product";;
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
            SqlDataAdapter dd = new SqlDataAdapter();
            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int c1 = Convert.ToInt32(textBox2.Text);
                int c2 = Convert.ToInt32(textBox3.Text);
                if (c1 > c2) textBox2.Text = textBox3.Text;
                trackBar4.Value = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int c1 = Convert.ToInt32(textBox2.Text);
                int c2 = Convert.ToInt32(textBox3.Text);
                if (c2 > 100)
                {
                    textBox3.Text = "100";
                    c2 = 100;
                }
                if (c1 > c2) textBox3.Text = textBox2.Text;
                trackBar1.Value = Convert.ToInt32(textBox3.Text);
            }
            catch
            {

            }
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int c1 = Convert.ToInt32(textBox4.Text);
                int c2 = Convert.ToInt32(textBox5.Text);
                if (c1 > c2) textBox4.Text = textBox5.Text;
                trackBar3.Value = Convert.ToInt32(textBox4.Text);
            }
            catch
            {

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int c1 = Convert.ToInt32(textBox4.Text);
                int c2 = Convert.ToInt32(textBox5.Text);
                if (c2 > 3000)
                {
                    textBox4.Text = "3000";
                    c2 = 3000;
                }
                if (c1 > c2) textBox5.Text = textBox4.Text;
                trackBar2.Value = Convert.ToInt32(textBox5.Text);
            }
            catch
            {

            }
        }

        ParserWorker<string[]> parser;

        private void button7_Click(object sender, EventArgs e)
        {
            parser.ParserSettings = new PS4DiskSettings(1,1);
            parser.Start();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            parser.Abort();
        }


        int search_id = 0;
        string search_date = "";
        int[] d1;

        private void button9_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                search_date = "";
                return;
            }
            if(search_date != textBox1.Text)
            {
                search_date = textBox1.Text;
                search_id = 0;
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                string selecter = "SELECT product_id FROM Product WHERE product_name LIKE '%" + textBox1.Text + "%'";
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
                SqlDataAdapter dd = new SqlDataAdapter();
                DataTable dt = new DataTable();
                d.Fill(dt);
                sqlconn.Close();

                d1 = new int[dt.Rows.Count];

                int le = 0;
                
                foreach (DataRow dr in dt.Rows)
                {
                    d1[le] = Convert.ToInt32(dr[0].ToString());
                    le++;
                }
            }

            if (d1.Length == 0)
            {
                MessageBox.Show("Ничего не найдено");
                return;
            }

            int l = 0;

            for (int i = 0; i< dataGridView1.Rows.Count ; i++)
            {
                if (  d1.Contains(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value)))
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                    l++;
                    if (d1.Length == 1) return;
                    if (l > search_id)
                    {
                        search_id++;
                        if (search_id == d1.Length) search_id = 0;
                        return;
                    }
                }
                if (i == dataGridView1.Rows.Count - 1) i = -1;
                
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Width == 1090)
            {
                dataGridView1.Width = 670;
                return;
            }
            if(dataGridView1.Width == 670)
            {
                dataGridView1.Width = 1090;
                
                return;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var foem = new EditPO(false,-1);
            foem.ShowDialog();
            string selecter = "SELECT preorder_id as 'код товара', preorder_name as 'Название', price as 'Цена', count as 'Кол-во Предзаказов' FROM Preorder";
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            var foem = new EditPO(true, (int)dataGridView1.SelectedRows[0].Cells[0].Value);
            foem.ShowDialog();
            string selecter = "SELECT preorder_id as 'код товара', preorder_name as 'Название', price as 'Цена', count as 'Кол-во Предзаказов' FROM Preorder";;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult tt = MessageBox.Show("Точно хотиите удалить выбранный предзаказ?", "Проверка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tt == DialogResult.No) return;
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            preorderTableAdapter.DeleteQuery(id);
            string selecter = "SELECT preorder_id as 'код товара', preorder_name as 'Название', price as 'Цена', count as 'Кол-во Предзаказов' FROM Preorder";;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            string selecter = "SELECT number as 'Номер', Info as 'Название' FROM disk_info WHERE product_id = " + id;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            sqlconn.Close();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Для данного товара нет подробной информации", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridView1.DataSource = dt;

            button7.Enabled = false;
            button7.Visible = false;
            button3.Enabled = false;
            button3.Visible = false;
            button9.Enabled = false;
            textBox1.Enabled = false;
            textBox1.Text = "";
            button10.Enabled = false;
            button15.Enabled = false;
            button15.Visible = false;
            button16.Enabled = false;
            button16.Visible = false;
            button17.Enabled = false;
            button17.Visible = false;

            dataGridView1.Width = 1090;


            chet = false;

            if (user == "admin")
            {
                button6.Visible = false;
                button6.Enabled = false;
                button8.Visible = false;
                button8.Enabled = false;
            }

            button11.Enabled = false;
            button11.Visible = false;
            button12.Enabled = false;
            button12.Visible = false;
            button13.Enabled = false;
            button13.Visible = false;
            button2.Text = "Товар";
        }

        List<int> buy = new List<int>();
        int[] count;

        private void button16_Click(object sender, EventArgs e)
        {
           
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            if(count[id] >= (int)dataGridView1.SelectedRows[0].Cells[3].Value)
            {
                MessageBox.Show("На складе недостаточно товара для данного действия","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            count[id]++;
            buy.Add(id);
            button14.Text = "Корзина(" + buy.Count +")";
        }

        public void dellbuy()
        {
            buy.Clear();
            button14.Text = "Корзина(" + 0 + ")";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var buyform = new Buy(this,user,buy);
            buyform.ShowDialog();

            for (int i = 0; i < count.Length; i++)
            {
                count[i] = 0;
            }
            for(int i = 0; i< buy.Count; i++)
            {
                count[buy[i]]++;
            }

            button10.Enabled = true;
            button9.Enabled = true;

            button3.Enabled = true;
            button3.Visible = true;
            textBox1.Enabled = true;

            if (user == "admin")
            {
                button6.Visible = true;
                button6.Enabled = true;
                button8.Visible = true;
                button8.Enabled = true;
                button7.Enabled = false;
                button7.Visible = false;
            }

            button11.Enabled = false;
            button11.Visible = false;
            button12.Enabled = false;
            button12.Visible = false;
            button13.Enabled = false;
            button13.Visible = false;
            button15.Enabled = true;
            button15.Visible = true;
            if(user != "admin")
            {
                button16.Enabled = true;
                button16.Visible = true;
            }
            button17.Enabled = false;
            button17.Visible = false;

            button2.Text = "Предзаказ";
            string selecter = "SELECT product_id as 'Код товара', product_name as 'Название' , status as 'Статус', stock as 'На складе', platform as 'Платформа', storage as 'Объём памяти', date_type as 'Тип данных', disk_type as 'Тип диска', country as 'Страна-производитель', count as 'Дисков в упаковке', price as 'Цена' FROM Product"; ;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
            chet = true;
            button14.Text = "Корзина(" + buy.Count + ")";

        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            string selecter = "SELECT * FROM POU WHERE preorder_id = " + id + " AND login = '" + user + "'"; ;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            if(dt.Rows.Count == 0)
            {
                pOUTableAdapter.Insert(id, user, 1);
            }
            else
            {
                pOUTableAdapter.UpdateQuery(user, id);
            }
            preorderTableAdapter.UpdateQuery2(id);

            sqlconn.Close();
            selecter = "SELECT preorder_id as 'код товара', preorder_name as 'Название', price as 'Цена', count as 'Кол-во Предзаказов' FROM Preorder"; ;
            sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            d = new SqlDataAdapter(selecter, sqlconn);

            dt = new DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }
    }
}

