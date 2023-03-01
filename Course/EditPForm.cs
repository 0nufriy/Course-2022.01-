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

namespace Course
{
    public partial class EditPForm : Form
    {
        bool edit;
        public EditPForm()
        {
            edit = true;
            InitializeComponent();
        }

        string pf;
        string dit;
        string dat;
        string ide;

        public EditPForm(string id)
        {
            InitializeComponent();
            edit = false;
            ide = id;

            int idint = Convert.ToInt32(id);

            string selecter = "SELECT * FROM [Product] WHERE product_id = " + id;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);

            object[] date = dt.Rows[0].ItemArray;


            textBox2.Text = date[1].ToString();
            textBox4.Text = date[3].ToString();
            textBox3.Text = date[5].ToString();
            pf = date[4].ToString();
            dat = date[6].ToString();
            dit = date[7].ToString();
            textBox8.Text = date[8].ToString();
            textBox9.Text = date[9].ToString();
            textBox10.Text = date[10].ToString();
          

            sqlconn.Close();
        }

        private void EditPForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.disk_info". При необходимости она может быть перемещена или удалена.
            this.disk_infoTableAdapter.Fill(this.diskCourseDataSet.disk_info);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.Product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.diskCourseDataSet.Product);

            string selecter = "SELECT DISTINCT platform FROM [Product]";
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }

            selecter = "SELECT DISTINCT date_type FROM [Product]";
            d = new SqlDataAdapter(selecter, sqlconn);

            dt = new DataTable();
            d.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }
            selecter = "SELECT DISTINCT disk_type FROM [Product]";
            d = new SqlDataAdapter(selecter, sqlconn);

            dt = new DataTable();
            d.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox3.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }
            if(ide != null)
            {
                selecter = "SELECT * FROM [disk_info] WHERE product_id = " + ide;
                d = new SqlDataAdapter(selecter, sqlconn);

                dt = new DataTable();
                d.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listBox1.Items.Add(dt.Rows[i][2].ToString());
                }
            }
            

            sqlconn.Close();

            comboBox1.Text = pf;
            comboBox2.Text = dat;
            comboBox3.Text = dit;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string storage = textBox3.Text;
            try
            {

                panel1.BackColor = BackColor;
                panel2.BackColor = BackColor;
                panel3.BackColor = BackColor;
                panel5.BackColor = BackColor;


                panel1.BackColor = Color.Red;
                Convert.ToInt32(textBox4.Text);
                panel1.BackColor = BackColor;

                panel2.BackColor = Color.Red;
                if (storage != "") Convert.ToDecimal(storage.Replace('.', ','));
                panel2.BackColor = BackColor;

                panel3.BackColor = Color.Red;
                Convert.ToInt32(textBox9.Text);
                panel3.BackColor = BackColor;


                panel5.BackColor = Color.Red;
                Convert.ToInt32(textBox10.Text);
                panel5.BackColor = BackColor;

                Convert.ToInt32(ide);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Неверно введены данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string status = "В наличии";
            if(Convert.ToInt32(textBox4.Text)  == 0) status = "Нет в наличии";
            if (!edit)
            {
                if(storage == "")
                    productTableAdapter.UpdateQuery(textBox2.Text, status, Convert.ToInt32(textBox4.Text),comboBox1.Text, null, comboBox2.Text, comboBox3.Text, textBox8.Text, Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text), Convert.ToInt32(ide));
                else
                    productTableAdapter.UpdateQuery(textBox2.Text, status, Convert.ToInt32(textBox4.Text), comboBox1.Text, Convert.ToDecimal(storage.Replace('.', ',')), comboBox2.Text, comboBox3.Text, textBox8.Text, Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text), Convert.ToInt32(ide));
                disk_infoTableAdapter.DeleteQuery(Convert.ToInt32(ide));
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    disk_infoTableAdapter.Insert(Convert.ToInt32(ide), i +1, listBox1.Items[i].ToString());
                }
            }
            else
            {
                string selecter = "SELECT product_id FROM [Product] ORDER BY product_id DESC";
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

                DataTable dt = new DataTable();
                d.Fill(dt);

                int id = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString()) +1;

                sqlconn.Close();

                if (storage != "")
                    productTableAdapter.InsertQuery(id, textBox2.Text, status, Convert.ToInt32(textBox4.Text), comboBox1.Text, Convert.ToDecimal(storage), comboBox2.Text, comboBox3.Text, textBox8.Text, Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text));
                else
                    productTableAdapter.InsertQuery(id, textBox2.Text, status, Convert.ToInt32(textBox4.Text), comboBox1.Text, null, comboBox2.Text, comboBox3.Text, textBox8.Text, Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox10.Text));

                for(int i = 0; i<listBox1.Items.Count; i++)
                {
                    disk_infoTableAdapter.Insert(id, i+1, listBox1.Items[i].ToString());
                }

            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text == "Балванка")
            {
                listBox1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                listBox1.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var forma = new AddInfo();
            forma.ShowDialog();
            listBox1.Items.Add(forma.result);
        }
    }
}
