namespace ManageSpacesOfInstitute
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            gr_filtering = new GroupBox();
            comboBox1 = new ComboBox();
            fpl_chwidth = new ComboBox();
            label8 = new Label();
            label7 = new Label();
            button1 = new Button();
            label2 = new Label();
            fpl_chnumberroom = new ComboBox();
            fpl_chbuild = new ComboBox();
            label6 = new Label();
            label3 = new Label();
            fpl_chtyperoom = new ComboBox();
            fpl_cheq = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            fpl_chtypebuild = new ComboBox();
            label1 = new Label();
            tabPage3 = new TabPage();
            groupBox2 = new GroupBox();
            label9 = new Label();
            comboBox2 = new ComboBox();
            button2 = new Button();
            label10 = new Label();
            label11 = new Label();
            dataGridView2 = new DataGridView();
            pictureBox1 = new PictureBox();
            tabPage4 = new TabPage();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            gr_filtering.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1024, 449);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1016, 419);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Редактирование";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Controls.Add(gr_filtering);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1016, 419);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Поиск помещений";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(417, 50);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(592, 362);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Сведения о найденных помещениях";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 23);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(580, 329);
            dataGridView1.TabIndex = 20;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // gr_filtering
            // 
            gr_filtering.Controls.Add(comboBox1);
            gr_filtering.Controls.Add(fpl_chwidth);
            gr_filtering.Controls.Add(label8);
            gr_filtering.Controls.Add(label7);
            gr_filtering.Controls.Add(button1);
            gr_filtering.Controls.Add(label2);
            gr_filtering.Controls.Add(fpl_chnumberroom);
            gr_filtering.Controls.Add(fpl_chbuild);
            gr_filtering.Controls.Add(label6);
            gr_filtering.Controls.Add(label3);
            gr_filtering.Controls.Add(fpl_chtyperoom);
            gr_filtering.Controls.Add(fpl_cheq);
            gr_filtering.Controls.Add(label5);
            gr_filtering.Controls.Add(label4);
            gr_filtering.Controls.Add(fpl_chtypebuild);
            gr_filtering.Location = new Point(8, 50);
            gr_filtering.Name = "gr_filtering";
            gr_filtering.Size = new Size(403, 362);
            gr_filtering.TabIndex = 19;
            gr_filtering.TabStop = false;
            gr_filtering.Text = "Фильтрация";
            gr_filtering.UseCompatibleTextRendering = true;
            // 
            // comboBox1
            // 
            comboBox1.Location = new Point(158, 280);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(186, 25);
            comboBox1.TabIndex = 22;
            comboBox1.Text = "Выберите номер аудитории";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // fpl_chwidth
            // 
            fpl_chwidth.Location = new Point(158, 240);
            fpl_chwidth.Name = "fpl_chwidth";
            fpl_chwidth.Size = new Size(186, 25);
            fpl_chwidth.TabIndex = 21;
            fpl_chwidth.Text = "Выберите номер аудитории";
            fpl_chwidth.SelectedIndexChanged += fpl_chwidth_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 284);
            label8.Name = "label8";
            label8.Size = new Size(126, 17);
            label8.TabIndex = 20;
            label8.Text = "Необходимая длина";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 243);
            label7.Name = "label7";
            label7.Size = new Size(134, 17);
            label7.TabIndex = 19;
            label7.Text = "Необходимая ширина";
            // 
            // button1
            // 
            button1.Location = new Point(219, 314);
            button1.Name = "button1";
            button1.Size = new Size(178, 42);
            button1.TabIndex = 18;
            button1.Text = "Применить фильтрацию";
            button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 26);
            label2.Name = "label2";
            label2.Size = new Size(142, 17);
            label2.TabIndex = 8;
            label2.Text = "Наименование корпуса";
            label2.Click += label2_Click;
            // 
            // fpl_chnumberroom
            // 
            fpl_chnumberroom.Enabled = false;
            fpl_chnumberroom.Location = new Point(245, 199);
            fpl_chnumberroom.Name = "fpl_chnumberroom";
            fpl_chnumberroom.Size = new Size(150, 25);
            fpl_chnumberroom.TabIndex = 17;
            fpl_chnumberroom.Text = "Выберите аудиторию";
            fpl_chnumberroom.SelectedIndexChanged += fpl_chnumberroom_SelectedIndexChanged;
            // 
            // fpl_chbuild
            // 
            fpl_chbuild.Location = new Point(158, 23);
            fpl_chbuild.Name = "fpl_chbuild";
            fpl_chbuild.Size = new Size(127, 25);
            fpl_chbuild.TabIndex = 9;
            fpl_chbuild.Text = "Выберите корпус";
            fpl_chbuild.SelectedIndexChanged += fpl_chbuild_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 202);
            label6.Name = "label6";
            label6.Size = new Size(229, 17);
            label6.TabIndex = 16;
            label6.Text = "Номер аудитории (обязателен корпус)";
            label6.Click += label6_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 70);
            label3.Name = "label3";
            label3.Size = new Size(134, 17);
            label3.TabIndex = 10;
            label3.Text = "Нужное оборудование";
            label3.Click += label3_Click;
            // 
            // fpl_chtyperoom
            // 
            fpl_chtyperoom.Location = new Point(114, 155);
            fpl_chtyperoom.Name = "fpl_chtyperoom";
            fpl_chtyperoom.Size = new Size(171, 25);
            fpl_chtyperoom.TabIndex = 15;
            fpl_chtyperoom.Text = "Выберите тип аудитории";
            // 
            // fpl_cheq
            // 
            fpl_cheq.Location = new Point(150, 67);
            fpl_cheq.Name = "fpl_cheq";
            fpl_cheq.Size = new Size(162, 25);
            fpl_cheq.TabIndex = 11;
            fpl_cheq.Text = "Выберите оборудование";
            fpl_cheq.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 158);
            label5.Name = "label5";
            label5.Size = new Size(98, 17);
            label5.TabIndex = 14;
            label5.Text = "Тип аудитории";
            label5.Click += label5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 115);
            label4.Name = "label4";
            label4.Size = new Size(81, 17);
            label4.TabIndex = 12;
            label4.Text = "Тип корпуса";
            label4.Click += label4_Click;
            // 
            // fpl_chtypebuild
            // 
            fpl_chtypebuild.Location = new Point(97, 112);
            fpl_chtypebuild.Name = "fpl_chtypebuild";
            fpl_chtypebuild.Size = new Size(161, 25);
            fpl_chtypebuild.TabIndex = 13;
            fpl_chtypebuild.Text = "Выберите тип корпуса";
            fpl_chtypebuild.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.DarkCyan;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(339, 33);
            label1.TabIndex = 7;
            label1.Text = "Информация о помещениях";
            label1.Click += label1_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(groupBox2);
            tabPage3.Controls.Add(label11);
            tabPage3.Controls.Add(dataGridView2);
            tabPage3.Controls.Add(pictureBox1);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1016, 419);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Поиск оборудования";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(comboBox2);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(label10);
            groupBox2.Location = new Point(6, 39);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1002, 138);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "groupBox2";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(19, 35);
            label9.Name = "label9";
            label9.Size = new Size(144, 17);
            label9.TabIndex = 3;
            label9.Text = "Название оборудования";
            label9.Click += label9_Click;
            // 
            // comboBox2
            // 
            comboBox2.Location = new Point(19, 64);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(144, 25);
            comboBox2.TabIndex = 10;
            comboBox2.Text = "Выберите корпус";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged_1;
            // 
            // button2
            // 
            button2.Location = new Point(648, 94);
            button2.Name = "button2";
            button2.Size = new Size(124, 38);
            button2.TabIndex = 2;
            button2.Text = "Найти";
            button2.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(210, 35);
            label10.Name = "label10";
            label10.Size = new Size(46, 17);
            label10.TabIndex = 4;
            label10.Text = "label10";
            label10.Click += label10_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Comic Sans MS", 18F);
            label11.ForeColor = Color.DarkCyan;
            label11.Location = new Point(3, 3);
            label11.Name = "label11";
            label11.Size = new Size(429, 33);
            label11.TabIndex = 5;
            label11.Text = "Поиск оборудования в помещениях";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(6, 183);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(707, 229);
            dataGridView2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(719, 183);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(289, 229);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1016, 419);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Поиск по ответственному лицу";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 450);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Manage Spaces";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            gr_filtering.ResumeLayout(false);
            gr_filtering.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private PictureBox pictureBox1;
        private Label label1;
        private TabPage tabPage4;
        private Label label2;
        private ComboBox fpl_chbuild;
        private Label label3;
        private ComboBox fpl_cheq;
        private Label label4;
        private ComboBox fpl_chtypebuild;
        private ComboBox fpl_chtyperoom;
        private Label label5;
        private GroupBox gr_filtering;
        private ComboBox fpl_chnumberroom;
        private Label label6;
        private Button button1;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Label label8;
        private Label label7;
        private ComboBox comboBox1;
        private ComboBox fpl_chwidth;
        private Label label11;
        private Label label10;
        private Label label9;
        private Button button2;
        private DataGridView dataGridView2;
        private ComboBox comboBox2;
        private GroupBox groupBox2;
    }
}
