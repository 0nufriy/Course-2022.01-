using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course
{
    public partial class Reg : Form
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Поля Логин и пароль - обязательные", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (textBox3.Text == "") textBox3.Text = null;
            if (textBox4.Text == "") textBox4.Text = null;
            if (textBox5.Text == "") textBox5.Text = null;
            if (textBox6.Text == "") textBox6.Text = null;

            userTableAdapter.Insert(textBox1.Text, textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text);

            this.Close();
        }

        private void Reg_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.User". При необходимости она может быть перемещена или удалена.
            this.userTableAdapter.Fill(this.diskCourseDataSet.User);

        }
    }
}
