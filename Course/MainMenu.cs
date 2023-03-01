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
    public partial class MainMenu : Form
    {
        public string user { get; set; } 
        public bool admin { get; set; }
        public MainMenu(string login)
        {
            user = login;
            if(user == "admin") admin = true;
            else admin = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new Productcs(user);
            this.Close();
            Program.Context.MainForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new UserInfo(user);
            this.Close();
            Program.Context.MainForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new Form1();
            this.Close();
            Program.Context.MainForm.Show();
        }
    }
}
