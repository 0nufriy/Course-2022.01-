using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course
{
    public partial class Buy : Form
    {
        Productcs formb;
        string login;
        List<int> buy;
        bool admin;
        public Buy(Productcs formb, string login, List<int> buy)
        {
            this.formb = formb;
            this.login = login;
            this.buy = buy;
            if (login == "admin") admin = true;
            else admin = false;
            InitializeComponent();
        }

        private void load()
        {
            string selecter = "SELECT product_id as 'Код товара', product_name as 'Название ', price as 'Цена' From Product WHERE 1=0 ";
            for (int i = 0; i < buy.Count; i++)
            {
                selecter += "OR product_id = " + buy[i];
            }
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
            DataTable dt = new DataTable();
            d.Fill(dt);
            dt.Columns.Add("Колличество");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int code = (int)dt.Rows[i][0];
                int c = 0;
                for (int j = 0; j < buy.Count; j++)
                {
                    if (buy[j] == code) c++;
                }
                dt.Rows[i][3] = c;
            }
            int sum = 0;
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                sum += Convert.ToInt32( dt.Rows[i][2]) * Convert.ToInt32(dt.Rows[i][3]);
            }
            dataGridView1.DataSource = dt;
            sqlconn.Close();
            label2.Text = sum.ToString() + " грн.";
        }

        private void Buy_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.Product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.diskCourseDataSet.Product);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.Sales". При необходимости она может быть перемещена или удалена.
            this.salesTableAdapter.Fill(this.diskCourseDataSet.Sales);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.Receipt". При необходимости она может быть перемещена или удалена.
            this.receiptTableAdapter.Fill(this.diskCourseDataSet.Receipt);
            dataGridView1.AutoGenerateColumns = true;
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            for (int j = 0; j < buy.Count; j++)
            {
                if (buy[j] == id)
                {
                    buy.RemoveAt(j);
                    break;
                }
            }
            load();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Корзина пуста", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string selecter = "SELECT MAX(receipt_id) FROM Receipt";
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);
            int id = (int)dt.Rows[0][0] + 1;
            sqlconn.Close();
            receiptTableAdapter.Insert(id, DateTime.Now, login);
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                salesTableAdapter.Insert(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value), id, Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value));
            }
            if (!admin)
            {
                for (int i = 0; i < buy.Count; i++)
                {
                    productTableAdapter.UpdateQuery1(buy[i]);
                }
            }
            MessageBox.Show("Спасибо за покупку!", "Покупка прошла умпешно",MessageBoxButtons.OK, MessageBoxIcon.Information);
            buy.Clear();    
            Close();
        }
    }
}
