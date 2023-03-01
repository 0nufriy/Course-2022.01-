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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Login()
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter("SELECT * FROM [User] WHERE login = '" + login + "' AND password = '" + password + "'", sqlconn);
            DataTable dt = new DataTable();
            d.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel1.BackColor = Color.Red;
                panel2.BackColor = Color.Red;
            }
            else
            {

                Program.Context.MainForm = new MainMenu(login.ToLower());
                this.Close();
                Program.Context.MainForm.Show();

            }
            sqlconn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var reg = new Reg();
            reg.ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                textBox2.Select();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
    }
}
