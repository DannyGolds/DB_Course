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
            gr_filtering = new GroupBox();
            button1 = new Button();
            label2 = new Label();
            comboBox4 = new ComboBox();
            fpl_chbuild = new ComboBox();
            label6 = new Label();
            label3 = new Label();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            comboBox1 = new ComboBox();
            label1 = new Label();
            tabPage3 = new TabPage();
            pictureBox1 = new PictureBox();
            tabPage4 = new TabPage();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            gr_filtering.SuspendLayout();
            tabPage3.SuspendLayout();
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
            tabControl1.Size = new Size(800, 449);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 419);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Редактирование";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(gr_filtering);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 419);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Поиск помещений";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // gr_filtering
            // 
            gr_filtering.Controls.Add(button1);
            gr_filtering.Controls.Add(label2);
            gr_filtering.Controls.Add(comboBox4);
            gr_filtering.Controls.Add(fpl_chbuild);
            gr_filtering.Controls.Add(label6);
            gr_filtering.Controls.Add(label3);
            gr_filtering.Controls.Add(comboBox3);
            gr_filtering.Controls.Add(comboBox2);
            gr_filtering.Controls.Add(label5);
            gr_filtering.Controls.Add(label4);
            gr_filtering.Controls.Add(comboBox1);
            gr_filtering.Location = new Point(8, 50);
            gr_filtering.Name = "gr_filtering";
            gr_filtering.Size = new Size(443, 362);
            gr_filtering.TabIndex = 19;
            gr_filtering.TabStop = false;
            gr_filtering.Text = "Фильтрация";
            gr_filtering.UseCompatibleTextRendering = true;
            // 
            // button1
            // 
            button1.Location = new Point(253, 310);
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
            label2.UseWaitCursor = true;
            label2.Click += label2_Click;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(245, 199);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(186, 25);
            comboBox4.TabIndex = 17;
            comboBox4.Text = "Выберите номер аудитории";
            // 
            // fpl_chbuild
            // 
            fpl_chbuild.Location = new Point(158, 23);
            fpl_chbuild.Name = "fpl_chbuild";
            fpl_chbuild.Size = new Size(127, 25);
            fpl_chbuild.TabIndex = 9;
            fpl_chbuild.Text = "Выберите корпус";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 202);
            label6.Name = "label6";
            label6.Size = new Size(229, 17);
            label6.TabIndex = 16;
            label6.Text = "Номер аудитории (обязателен корпус)";
            label6.UseWaitCursor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 70);
            label3.Name = "label3";
            label3.Size = new Size(134, 17);
            label3.TabIndex = 10;
            label3.Text = "Нужное оборудование";
            label3.UseWaitCursor = true;
            label3.Click += label3_Click;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(114, 155);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(171, 25);
            comboBox3.TabIndex = 15;
            comboBox3.Text = "Выберите тип аудитории";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(150, 67);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(162, 25);
            comboBox2.TabIndex = 11;
            comboBox2.Text = "Выберите оборудование";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 158);
            label5.Name = "label5";
            label5.Size = new Size(98, 17);
            label5.TabIndex = 14;
            label5.Text = "Тип аудитории";
            label5.UseWaitCursor = true;
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
            label4.UseWaitCursor = true;
            label4.Click += label4_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(97, 112);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(161, 25);
            comboBox1.TabIndex = 13;
            comboBox1.Text = "Выберите тип корпуса";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
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
            tabPage3.Controls.Add(pictureBox1);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(792, 419);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Поиск оборудования";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(217, 172);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(792, 419);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Поиск по ответственному лицу";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Manage Spaces";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            gr_filtering.ResumeLayout(false);
            gr_filtering.PerformLayout();
            tabPage3.ResumeLayout(false);
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
        private ComboBox comboBox2;
        private Label label4;
        private ComboBox comboBox1;
        private ComboBox comboBox3;
        private Label label5;
        private GroupBox gr_filtering;
        private ComboBox comboBox4;
        private Label label6;
        private Button button1;
    }
}
