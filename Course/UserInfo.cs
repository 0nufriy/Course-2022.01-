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
using System.IO;
using Xceed.Words.NET;
using Word = Microsoft.Office.Interop.Word;
using Xceed.Document.NET;

namespace Course
{
    public partial class UserInfo : Form
    {
        public string user { get; set; }
        public bool admin { get; set; }
        public UserInfo(string login)
        {
            user = login;
            
            if (user == "admin") admin = true;
            else admin = false;
            InitializeComponent();
            label1.Text = user;
            dataGridView1.AutoGenerateColumns = true;
            if(user == "admin")
            {
                button5.Enabled = true;
                button5.Visible = true;
                button9.Visible = true;
                button9.Enabled = true;

                button6.Enabled = false;
                button6.Visible = false;
                button8.Enabled = false;
                button8.Visible = false;
                textBox2.Enabled = false;
                textBox2.Visible = false;
                textBox3.Enabled = false;
                textBox3.Visible = false;
                textBox4.Enabled = false;
                textBox4.Visible = false;
                textBox5.Enabled = false;
                textBox5.Visible = false;
                textBox6.Enabled = false;
                textBox6.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
            }
            
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d;
            if(admin)
                d = new SqlDataAdapter("SELECT receipt_id as 'Номер чека', date_time as 'Дата и Время',login as 'Логин' FROM [Receipt]", sqlconn);
            else
            {
                d = new SqlDataAdapter("SELECT receipt_id as 'Номер чека', date_time as 'Дата и Время' FROM [Receipt] WHERE login = '" + user +"'", sqlconn);
            }
            SqlDataAdapter d1 = new SqlDataAdapter("SELECT * FROM [User] WHERE login = '" + user +"'", sqlconn);
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable userinfo = new System.Data.DataTable();
            d.Fill(dt);
            d1.Fill(userinfo);
            dataGridView1.DataSource = dt;
            sqlconn.Close();

            textBox2.Text = userinfo.Rows[0][1].ToString();
            textBox3.Text = userinfo.Rows[0][2].ToString();
            textBox6.Text = userinfo.Rows[0][3].ToString();
            textBox5.Text = userinfo.Rows[0][4].ToString();
            textBox4.Text = userinfo.Rows[0][5].ToString();
        }

 

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new MainMenu(user);
            this.Close();
            Program.Context.MainForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button3.Visible = true;
            button2.Enabled = false;
            button2.Visible = false;
            button4.Visible = false;
            button4.Enabled = false;

            button7.Visible = false;
            button7.Enabled = false;
            button10.Visible = false;
            button10.Enabled = false;

            var a = (int)dataGridView1.SelectedRows[0].Cells[0].Value; ;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d= new SqlDataAdapter("SELECT Product.product_name as 'Название товара', Sales.count as 'Колличество товара'FROM [Sales],[Product] WHERE Sales.product_id = Product.product_id AND receipt_id =" + a, sqlconn);

            System.Data.DataTable dt = new System.Data.DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button3.Visible = false;
            button2.Enabled = true;
            button2.Visible = true;
            button4.Visible = true;
            button4.Enabled = true;

            button7.Visible = true;
            button7.Enabled = true;

            button10.Visible = true;
            button10.Enabled = true;


            button11.Enabled = false;
            button11.Visible = false;

            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d;
            if (admin)
                d = new SqlDataAdapter("SELECT receipt_id as 'Номер чека', date_time as 'Дата и Время',login as 'Логин' FROM [Receipt]", sqlconn);
            else
            {
                d = new SqlDataAdapter("SELECT receipt_id as 'Номер чека', date_time as 'Дата и Время' FROM [Receipt] WHERE login = '" + user + "'", sqlconn);
            }
            System.Data.DataTable dt = new System.Data.DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var a = (int)dataGridView1.SelectedRows[0].Cells[0].Value; ;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter("SELECT SUM(storage*Product.count*Sales.count) FROM [Sales],[Product] WHERE Sales.product_id = Product.product_id AND receipt_id =" + a, sqlconn);

            System.Data.DataTable dt = new System.Data.DataTable();
            d.Fill(dt);
            string sum = dt.Rows[0][0].ToString();
            if (sum == "") sum = "0";
            MessageBox.Show("Общий объём: " + sum + "GB","Ответ", MessageBoxButtons.OK,MessageBoxIcon.Information);
            sqlconn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button3.Visible = true;
            button2.Enabled = false;
            button2.Visible = false;
            button4.Visible = false;
            button4.Enabled = false;

            button7.Visible = false;
            button7.Enabled = false;

            button10.Visible = false;
            button10.Enabled = false;

            string selecter =
                @"SELECT ID1 as 'Код товара', Product_name as 'Название товара', status as 'Статус', stock as 'На складе', disk_type as 'Тип диска', count as 'Страна-производитель', price as 'Цена', DAYS/SELE * stock AS 'Дней до полной продажи' FROM(    SELECT Product_id AS ID1, (YEAR(GETDATE()) - YEAR(MIN(date_time))) * 365 + (MONTH(GETDATE()) - MONTH(MIN(date_time))) * 30 + (DAY(GETDATE()) - DAY(MIN(date_time)) + 1) AS DAYS    FROM Receipt, Sales    WHERE Receipt.receipt_id = Sales.receipt_id    Group by product_id) AS V1,(	SELECT product_id AS ID2, sum(count) AS SELE    FROM Sales    Group by product_id) AS V2, [Product] WHERE ID1 = ID2 AND ID1 = Product_id AND DAYS/ SELE * stock <= 30  AND DAYS/ SELE * stock > 0 order by [Дней до полной продажи] ASC";
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
            System.Data.DataTable dt = new System.Data.DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();


            
            
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.Preorder". При необходимости она может быть перемещена или удалена.
            this.preorderTableAdapter.Fill(this.diskCourseDataSet.Preorder);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.POU". При необходимости она может быть перемещена или удалена.
            this.pOUTableAdapter.Fill(this.diskCourseDataSet.POU);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diskCourseDataSet.User". При необходимости она может быть перемещена или удалена.
            this.userTableAdapter.Fill(this.diskCourseDataSet.User);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Точно хотиите обновить данные?", "Проверка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes) userTableAdapter.UpdateQuery(textBox2.Text, textBox3.Text, textBox6.Text, textBox5.Text, textBox4.Text, label1.Text);
            else return;

            MessageBox.Show("Данные обновлены","Уведомление",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var a = (int)dataGridView1.SelectedRows[0].Cells[0].Value; ;
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter("SELECT Receipt.receipt_id, Receipt.date_time, Product.product_name, Sales.count, price FROM [Product], [Sales],[Receipt] WHERE Sales.product_id = Product.product_id AND Receipt.receipt_id = Sales.receipt_id AND Receipt.receipt_id = " + a, sqlconn);

            System.Data.DataTable dt = new System.Data.DataTable();
            d.Fill(dt);

            string date = dt.Rows[0][1].ToString();
            string id = dt.Rows[0][0].ToString();

            int sum = 0;

            

            string writePath = "check" + id + ".doc";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "doc files (*.doc)|*.doc";
            saveFileDialog.FileName = "check" + id + ".doc";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;

            writePath = saveFileDialog.FileName;

            Word.Document DocWord = new Word.Document();
            DocWord.SaveAs2(writePath);
            DocWord.Close();



           DocX doc = DocX.Create(writePath);

            doc.InsertParagraph("Номер чека: " + id).Font("Times New Roman").FontSize(14).Alignment = Xceed.Document.NET.Alignment.center;
            doc.InsertParagraph("Дата и время: " + date).Font("Times New Roman").FontSize(14).Alignment = Xceed.Document.NET.Alignment.right;
            doc.InsertParagraph("Состав чека:").Font("Times New Roman").FontSize(14).Alignment = Xceed.Document.NET.Alignment.right;

            Table table = doc.AddTable(dt.Rows.Count + 2,3);
            table.Alignment = Alignment.center;
            table.Design = TableDesign.TableGrid;

            int i = 1;
            table.Rows[0].Cells[0].Paragraphs[0].Append("Именование товара").Font("Times New Roman").FontSize(14);
            table.Rows[0].Cells[1].Paragraphs[0].Append("Цена").Font("Times New Roman").FontSize(14);
            table.Rows[0].Cells[2].Paragraphs[0].Append("Количество").Font("Times New Roman").FontSize(14);
            foreach (DataRow dr in dt.Rows)
            {
                table.Rows[i].Cells[0].Paragraphs[0].Append(dr[2].ToString()).Font("Times New Roman").FontSize(14);
                table.Rows[i].Cells[1].Paragraphs[0].Append(dr[4].ToString() + "грн.").Font("Times New Roman").FontSize(14);
                table.Rows[i].Cells[2].Paragraphs[0].Append(dr[3].ToString()).Font("Times New Roman").FontSize(14);
             
                sum += Convert.ToInt32(dr[3]) * Convert.ToInt32(dr[4]);
                i++;
            }

            table.Rows[i].MergeCells(0, 1);

            table.Rows[i].Cells[1].Paragraphs[0].Append(sum.ToString() + "грн.").Font("Times New Roman").FontSize(14);
            table.Rows[i].Cells[0].Paragraphs[0].Append("Общая сумма заказа:").Font("Times New Roman").FontSize(14);

            doc.InsertParagraph().InsertTableAfterSelf(table);

            doc.Save();

            sqlconn.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Точно хотиите удалить акаунт?", "Проверка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(d == DialogResult.Yes) d = MessageBox.Show("Вы прям ТОЧНО уверены, что хотиите удалить акаунт?", "Проверка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes) userTableAdapter.DeleteQuery(user);

            Program.Context.MainForm = new Form1();
            this.Close();
            Program.Context.MainForm.Show();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SellTime g = new SellTime();
            g.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button3.Visible = true;
            button2.Enabled = false;
            button2.Visible = false;
            button4.Visible = false;
            button4.Enabled = false;

            button7.Visible = false;
            button7.Enabled = false;
            button10.Visible = false;
            button10.Enabled = false;


            if (!admin)
            {
                button11.Enabled = true;
                button11.Visible = true;
            }
           
            

            string selecter = "";
            if (admin)
            {
                selecter = "SELECT POU.preorder_id as 'Код предзаказа',preorder_name as 'Название', SUM(P_count) as 'колличество'  FROM POU, Preorder WHERE POU.preorder_id = Preorder.preorder_id GROUP BY POU.preorder_id, preorder_name ORDER BY SUM(P_count) desc";
            }
            else
            {
                selecter = "SELECT POU.preorder_id as 'Код предзаказа',preorder_name as 'Название', SUM(P_count) as 'колличество'  FROM POU, Preorder WHERE POU.preorder_id = Preorder.preorder_id AND login = '" + user + "' GROUP BY POU.preorder_id, preorder_name ORDER BY SUM(P_count) desc";
            }
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
            System.Data.DataTable dt = new System.Data.DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            pOUTableAdapter.UpdateQuery1(id, user);
            preorderTableAdapter.UpdateQuery3(id);
            string selecter = "";
            if (admin)
            {
                selecter = "SELECT POU.preorder_id as 'Код предзаказа',preorder_name as 'Название', SUM(P_count) as 'колличество'  FROM POU, Preorder WHERE POU.preorder_id = Preorder.preorder_id GROUP BY POU.preorder_id, preorder_name ORDER BY SUM(P_count) desc";
            }
            else
            {
                selecter = "SELECT POU.preorder_id as 'Код предзаказа',preorder_name as 'Название', SUM(P_count) as 'колличество'  FROM POU, Preorder WHERE POU.preorder_id = Preorder.preorder_id AND login = '" + user + "' GROUP BY POU.preorder_id, preorder_name ORDER BY SUM(P_count) desc";
            }
            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);
            System.Data.DataTable dt = new System.Data.DataTable();
            d.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
        }

    }
}
