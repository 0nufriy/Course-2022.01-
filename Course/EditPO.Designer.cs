namespace Course
{
    partial class EditPO
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPO));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.diskCourseDataSet = new Course.DiskCourseDataSet();
            this.preorderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.preorderTableAdapter = new Course.DiskCourseDataSetTableAdapters.PreorderTableAdapter();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.diskCourseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preorderBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(149, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 50);
            this.button2.TabIndex = 41;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 50);
            this.button1.TabIndex = 40;
            this.button1.Text = "Готово";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(69, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Цена";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Название";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(108, 55);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(114, 20);
            this.textBox10.TabIndex = 29;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(108, 29);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(114, 20);
            this.textBox2.TabIndex = 24;
            // 
            // diskCourseDataSet
            // 
            this.diskCourseDataSet.DataSetName = "DiskCourseDataSet";
            this.diskCourseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // preorderBindingSource
            // 
            this.preorderBindingSource.DataMember = "Preorder";
            this.preorderBindingSource.DataSource = this.diskCourseDataSet;
            // 
            // preorderTableAdapter
            // 
            this.preorderTableAdapter.ClearBeforeFill = true;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(105, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 27);
            this.panel3.TabIndex = 42;
            // 
            // EditPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 169);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(291, 208);
            this.MinimumSize = new System.Drawing.Size(291, 208);
            this.Name = "EditPO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditPO";
            this.Load += new System.EventHandler(this.EditPO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.diskCourseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preorderBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox2;
        private DiskCourseDataSet diskCourseDataSet;
        private System.Windows.Forms.BindingSource preorderBindingSource;
        private DiskCourseDataSetTableAdapters.PreorderTableAdapter preorderTableAdapter;
        private System.Windows.Forms.Panel panel3;
    }
}