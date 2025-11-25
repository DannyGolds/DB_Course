namespace ManageSpacesOfInstitute
{
    partial class MainFrame
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
            tabPage2 = new TabPage();
            groupBox1 = new GroupBox();
            gridview_foundroomsinfo = new DataGridView();
            gr_filtering = new GroupBox();
            fpl_chlength = new ComboBox();
            fpl_chwidth = new ComboBox();
            label8 = new Label();
            label7 = new Label();
            btn_applyfilter = new Button();
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
            tabPage1 = new TabPage();
            label9 = new Label();
            tab_struct = new TabControl();
            tabPage3 = new TabPage();
            btn_auth = new Button();
            label_username = new Label();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridview_foundroomsinfo).BeginInit();
            gr_filtering.SuspendLayout();
            tabPage1.SuspendLayout();
            tab_struct.SuspendLayout();
            SuspendLayout();
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Controls.Add(gr_filtering);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1009, 405);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Поиск помещений";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(gridview_foundroomsinfo);
            groupBox1.Location = new Point(354, 39);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(654, 362);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Сведения о найденных помещениях";
            // 
            // gridview_foundroomsinfo
            // 
            gridview_foundroomsinfo.AllowUserToDeleteRows = false;
            gridview_foundroomsinfo.AllowUserToResizeColumns = false;
            gridview_foundroomsinfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridview_foundroomsinfo.Location = new Point(6, 23);
            gridview_foundroomsinfo.Name = "gridview_foundroomsinfo";
            gridview_foundroomsinfo.Size = new Size(642, 329);
            gridview_foundroomsinfo.TabIndex = 20;
            gridview_foundroomsinfo.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // gr_filtering
            // 
            gr_filtering.Controls.Add(fpl_chlength);
            gr_filtering.Controls.Add(fpl_chwidth);
            gr_filtering.Controls.Add(label8);
            gr_filtering.Controls.Add(label7);
            gr_filtering.Controls.Add(btn_applyfilter);
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
            gr_filtering.Location = new Point(8, 39);
            gr_filtering.Name = "gr_filtering";
            gr_filtering.Size = new Size(341, 362);
            gr_filtering.TabIndex = 19;
            gr_filtering.TabStop = false;
            gr_filtering.Text = "Фильтрация";
            gr_filtering.UseCompatibleTextRendering = true;
            // 
            // fpl_chlength
            // 
            fpl_chlength.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chlength.Location = new Point(142, 281);
            fpl_chlength.Name = "fpl_chlength";
            fpl_chlength.Size = new Size(186, 25);
            fpl_chlength.TabIndex = 22;
            fpl_chlength.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // fpl_chwidth
            // 
            fpl_chwidth.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chwidth.Location = new Point(148, 240);
            fpl_chwidth.Name = "fpl_chwidth";
            fpl_chwidth.Size = new Size(186, 25);
            fpl_chwidth.TabIndex = 21;
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
            // btn_applyfilter
            // 
            btn_applyfilter.Location = new Point(158, 314);
            btn_applyfilter.Name = "btn_applyfilter";
            btn_applyfilter.Size = new Size(178, 42);
            btn_applyfilter.TabIndex = 18;
            btn_applyfilter.Text = "Применить фильтрацию";
            btn_applyfilter.UseVisualStyleBackColor = true;
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
            fpl_chnumberroom.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chnumberroom.Location = new Point(127, 199);
            fpl_chnumberroom.Name = "fpl_chnumberroom";
            fpl_chnumberroom.Size = new Size(150, 25);
            fpl_chnumberroom.TabIndex = 17;
            fpl_chnumberroom.SelectedIndexChanged += fpl_chnumberroom_SelectedIndexChanged;
            // 
            // fpl_chbuild
            // 
            fpl_chbuild.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chbuild.Location = new Point(158, 23);
            fpl_chbuild.Name = "fpl_chbuild";
            fpl_chbuild.Size = new Size(127, 25);
            fpl_chbuild.TabIndex = 9;
            fpl_chbuild.SelectedIndexChanged += fpl_chbuild_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 202);
            label6.Name = "label6";
            label6.Size = new Size(111, 17);
            label6.TabIndex = 16;
            label6.Text = "Номер аудитории";
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
            fpl_chtyperoom.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chtyperoom.Location = new Point(114, 155);
            fpl_chtyperoom.Name = "fpl_chtyperoom";
            fpl_chtyperoom.Size = new Size(171, 25);
            fpl_chtyperoom.TabIndex = 15;
            // 
            // fpl_cheq
            // 
            fpl_cheq.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_cheq.Location = new Point(150, 67);
            fpl_cheq.Name = "fpl_cheq";
            fpl_cheq.Size = new Size(162, 25);
            fpl_cheq.TabIndex = 11;
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
            fpl_chtypebuild.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chtypebuild.Location = new Point(97, 112);
            fpl_chtypebuild.Name = "fpl_chtypebuild";
            fpl_chtypebuild.Size = new Size(161, 25);
            fpl_chtypebuild.TabIndex = 13;
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
            // tabPage1
            // 
            tabPage1.Controls.Add(label9);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1009, 405);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Редактирование";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label9.ForeColor = Color.DarkCyan;
            label9.Location = new Point(3, 3);
            label9.Name = "label9";
            label9.Size = new Size(469, 33);
            label9.TabIndex = 8;
            label9.Text = "Редактирование информационной базы";
            // 
            // tab_struct
            // 
            tab_struct.Controls.Add(tabPage1);
            tab_struct.Controls.Add(tabPage2);
            tab_struct.Controls.Add(tabPage3);
            tab_struct.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tab_struct.Location = new Point(4, 3);
            tab_struct.Name = "tab_struct";
            tab_struct.SelectedIndex = 0;
            tab_struct.Size = new Size(1017, 435);
            tab_struct.TabIndex = 2;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1009, 405);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Структура помещений";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_auth
            // 
            btn_auth.Location = new Point(895, 439);
            btn_auth.Name = "btn_auth";
            btn_auth.Size = new Size(126, 26);
            btn_auth.TabIndex = 3;
            btn_auth.Text = "Авторизоваться";
            btn_auth.UseVisualStyleBackColor = true;
            // 
            // label_username
            // 
            label_username.AutoSize = true;
            label_username.Font = new Font("Comic Sans MS", 12F);
            label_username.Location = new Point(4, 439);
            label_username.Name = "label_username";
            label_username.Size = new Size(53, 23);
            label_username.TabIndex = 4;
            label_username.Text = "Гость";
            // 
            // MainFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 468);
            Controls.Add(label_username);
            Controls.Add(btn_auth);
            Controls.Add(tab_struct);
            Name = "MainFrame";
            Text = "Manage Spaces";
            Load += Form1_Load;
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridview_foundroomsinfo).EndInit();
            gr_filtering.ResumeLayout(false);
            gr_filtering.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tab_struct.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabPage tabPage2;
        private GroupBox groupBox1;
        private DataGridView gridview_foundroomsinfo;
        private GroupBox gr_filtering;
        private ComboBox fpl_chlength;
        private ComboBox fpl_chwidth;
        private Label label8;
        private Label label7;
        private Button btn_applyfilter;
        private Label label2;
        private ComboBox fpl_chnumberroom;
        private ComboBox fpl_chbuild;
        private Label label6;
        private Label label3;
        private ComboBox fpl_chtyperoom;
        private ComboBox fpl_cheq;
        private Label label5;
        private Label label4;
        private ComboBox fpl_chtypebuild;
        private Label label1;
        private TabPage tabPage1;
        private TabControl tab_struct;
        private Button btn_auth;
        private Label label_username;
        private TabPage tabPage3;
        private Label label9;
    }
}
