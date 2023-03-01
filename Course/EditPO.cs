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
    public partial class EditPO : Form
    {
        bool edit;
        int id;
        public EditPO(bool edit, int id)
        {
            this.edit = edit;
            this.id = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(textBox10.Text);
            }catch(Exception ex)
            {
                panel3.BackColor = Color.Red;
                MessageBox.Show("Неверно введена цена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (edit)
            {
                preorderTableAdapter.UpdateQuery1(textBox2.Text, Convert.ToInt32(textBox10.Text), id);
            }
            else
            {
                string selecter = "SELECT MAX(preorder_id) FROM Preorder";
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

                DataTable dt = new DataTable();
                d.Fill(dt);
                int id = Convert.ToInt32(dt.Rows[0][0]) + 1;
                sqlconn.Close();
                preorderTableAdapter.Insert(id, textBox2.Text, Convert.ToInt32(textBox10.Text), 0);
                
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void preorderBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            

        }

        private void EditPO_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.Preorder". При необходимости она может быть перемещена или удалена.
            this.preorderTableAdapter.Fill(this.diskCourseDataSet.Preorder);

            if (edit)
            {
                string selecter = "SELECT * FROM Preorder WHERE preorder_id = " + id;
                SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
                sqlconn.Open();
                SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

                DataTable dt = new DataTable();
                d.Fill(dt);
                sqlconn.Close();
                textBox10.Text = dt.Rows[0][2].ToString();
                textBox2.Text = dt.Rows[0][1].ToString();

            }

        }
    }
}
