using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Xceed.Words.NET;
using Word = Microsoft.Office.Interop.Word;
using Xceed.Document.NET;


namespace Course
{
    public partial class SellTime : Form
    {
        public SellTime()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string d1 = dateTimePicker1.Value.ToString();
            string d2 = dateTimePicker2.Value.ToString();
            d1 = d1.Substring(0, 10);
            d2 = d2.Substring(0,10);
            d1 += " 00:00:00.000";
            d2 += " 00:00:00.000";

            string selecter = "SELECT product_name, sum(Sales.count), price FROM [Product],[Sales], [Receipt]where Product.product_id = Sales.product_id AND  Sales.receipt_id = Receipt.receipt_id AND date_time BETWEEN CAST(N'" + d1 +"' AS DateTime) AND CAST(N'"+ d2 + "' AS DateTime) Group by Product.product_id, product_name, price ";

            SqlConnection sqlconn = new SqlConnection(CoSt.GetString());
            sqlconn.Open();
            SqlDataAdapter d = new SqlDataAdapter(selecter, sqlconn);

            DataTable dt = new DataTable();
            d.Fill(dt);



            int sum = 0;



            string writePath = "Sales.doc";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "doc files (*.doc)|*.doc";
            saveFileDialog.FileName = "Sales.doc";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;

            writePath = saveFileDialog.FileName;

            Word.Document DocWord = new Word.Document();
            DocWord.SaveAs2(writePath);
            DocWord.Close();



            DocX doc = DocX.Create(writePath);

            doc.InsertParagraph("Отчёт по продажам").Font("Times New Roman").FontSize(14).Alignment = Xceed.Document.NET.Alignment.center;
            doc.InsertParagraph("C " + d1.Substring(0, 10) + " По " + d2.Substring(0, 10)).Font("Times New Roman").FontSize(14).Alignment = Xceed.Document.NET.Alignment.center;


            Table table = doc.AddTable(dt.Rows.Count + 2, 3);
            table.Alignment = Alignment.center;
            table.Design = TableDesign.TableGrid;

            int i = 1;
            table.Rows[0].Cells[0].Paragraphs[0].Append("Именование товара").Font("Times New Roman").FontSize(14);
            table.Rows[0].Cells[1].Paragraphs[0].Append("Цена").Font("Times New Roman").FontSize(14);
            table.Rows[0].Cells[2].Paragraphs[0].Append("Количество").Font("Times New Roman").FontSize(14);
            foreach (DataRow dr in dt.Rows)
            {
                table.Rows[i].Cells[0].Paragraphs[0].Append(dr[0].ToString()).Font("Times New Roman").FontSize(14);
                table.Rows[i].Cells[1].Paragraphs[0].Append(dr[2].ToString() + "грн.").Font("Times New Roman").FontSize(14);
                table.Rows[i].Cells[2].Paragraphs[0].Append(dr[1].ToString()).Font("Times New Roman").FontSize(14);
             
                sum += Convert.ToInt32(dr[2]) * Convert.ToInt32(dr[1]);
                i++;
            }

            table.Rows[i].MergeCells(0, 1);

            table.Rows[i].Cells[1].Paragraphs[0].Append(sum.ToString() + "грн.").Font("Times New Roman").FontSize(14);
            table.Rows[i].Cells[0].Paragraphs[0].Append("Общие продажи: ").Font("Times New Roman").FontSize(14);

            doc.InsertParagraph().InsertTableAfterSelf(table);

            doc.Save();




            sqlconn.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
