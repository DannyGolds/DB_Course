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
            btn_auth = new Button();
            label_username = new Label();
            tabPage2 = new TabPage();
            groupBox1 = new GroupBox();
            gridview_foundroomsinfo = new DataGridView();
            gr_filtering = new GroupBox();
            label23 = new Label();
            fpl_chpurproom = new ComboBox();
            lbl_chtyperoom = new Label();
            label2 = new Label();
            lbl_cheq = new Label();
            lbl_chbuild = new Label();
            fpl_chbuild = new ComboBox();
            label3 = new Label();
            fpl_chtyperoom = new ComboBox();
            fpl_cheq = new ComboBox();
            fpl_chtypebuild = new ComboBox();
            label1 = new Label();
            tabs = new TabControl();
            page_edit = new TabPage();
            btnAdd = new Button();
            btnDel = new Button();
            btnEd = new Button();
            btnRep = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            btnChFile = new Button();
            pictureBox1 = new PictureBox();
            textBox2 = new TextBox();
            label4 = new Label();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            label7 = new Label();
            lblBuildType = new Label();
            lblBuildName = new Label();
            tabPage3 = new TabPage();
            splitContainer2 = new SplitContainer();
            dataGridView2 = new DataGridView();
            comboBox2 = new ComboBox();
            label14 = new Label();
            checkedListBox1 = new CheckedListBox();
            comboBox6 = new ComboBox();
            label13 = new Label();
            comboBox5 = new ComboBox();
            label12 = new Label();
            textBox5 = new TextBox();
            label11 = new Label();
            textBox4 = new TextBox();
            label10 = new Label();
            comboBox4 = new ComboBox();
            label8 = new Label();
            textBox3 = new TextBox();
            label6 = new Label();
            comboBox3 = new ComboBox();
            label5 = new Label();
            tabPage4 = new TabPage();
            splitContainer3 = new SplitContainer();
            comboBox7 = new ComboBox();
            dataGridView3 = new DataGridView();
            pictureBox2 = new PictureBox();
            label19 = new Label();
            richTextBox1 = new RichTextBox();
            label18 = new Label();
            textBox7 = new TextBox();
            label17 = new Label();
            textBox6 = new TextBox();
            label16 = new Label();
            comboBox8 = new ComboBox();
            label15 = new Label();
            tabPage5 = new TabPage();
            textBox10 = new TextBox();
            label22 = new Label();
            textBox9 = new TextBox();
            label21 = new Label();
            textBox8 = new TextBox();
            label20 = new Label();
            dataGridView4 = new DataGridView();
            label9 = new Label();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridview_foundroomsinfo).BeginInit();
            gr_filtering.SuspendLayout();
            tabs.SuspendLayout();
            page_edit.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            SuspendLayout();
            // 
            // btn_auth
            // 
            btn_auth.Font = new Font("Zero Cool", 9.75F);
            btn_auth.ForeColor = Color.DarkCyan;
            btn_auth.Location = new Point(681, 439);
            btn_auth.Name = "btn_auth";
            btn_auth.Size = new Size(189, 35);
            btn_auth.TabIndex = 3;
            btn_auth.Text = "Авторизоваться";
            btn_auth.UseVisualStyleBackColor = true;
            btn_auth.Click += btn_auth_Click;
            // 
            // label_username
            // 
            label_username.AutoSize = true;
            label_username.Font = new Font("Zero Cool", 9.75F);
            label_username.ForeColor = Color.DarkCyan;
            label_username.Location = new Point(4, 453);
            label_username.Name = "label_username";
            label_username.Size = new Size(48, 15);
            label_username.TabIndex = 4;
            label_username.Text = "Гость";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Controls.Add(gr_filtering);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(859, 406);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Поиск информации о помещениях";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(gridview_foundroomsinfo);
            groupBox1.Font = new Font("Doloto", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            groupBox1.ForeColor = Color.Black;
            groupBox1.Location = new Point(195, 39);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(667, 361);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Сведения о найденных помещениях";
            // 
            // gridview_foundroomsinfo
            // 
            gridview_foundroomsinfo.AllowUserToAddRows = false;
            gridview_foundroomsinfo.AllowUserToDeleteRows = false;
            gridview_foundroomsinfo.AllowUserToResizeRows = false;
            gridview_foundroomsinfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridview_foundroomsinfo.BackgroundColor = SystemColors.Menu;
            gridview_foundroomsinfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridview_foundroomsinfo.GridColor = SystemColors.Menu;
            gridview_foundroomsinfo.Location = new Point(6, 20);
            gridview_foundroomsinfo.Name = "gridview_foundroomsinfo";
            gridview_foundroomsinfo.RowHeadersVisible = false;
            gridview_foundroomsinfo.Size = new Size(655, 335);
            gridview_foundroomsinfo.TabIndex = 20;
            gridview_foundroomsinfo.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // gr_filtering
            // 
            gr_filtering.Controls.Add(label23);
            gr_filtering.Controls.Add(fpl_chpurproom);
            gr_filtering.Controls.Add(lbl_chtyperoom);
            gr_filtering.Controls.Add(label2);
            gr_filtering.Controls.Add(lbl_cheq);
            gr_filtering.Controls.Add(lbl_chbuild);
            gr_filtering.Controls.Add(fpl_chbuild);
            gr_filtering.Controls.Add(label3);
            gr_filtering.Controls.Add(fpl_chtyperoom);
            gr_filtering.Controls.Add(fpl_cheq);
            gr_filtering.Controls.Add(fpl_chtypebuild);
            gr_filtering.Font = new Font("Doloto", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gr_filtering.ForeColor = Color.Black;
            gr_filtering.Location = new Point(8, 39);
            gr_filtering.Name = "gr_filtering";
            gr_filtering.Size = new Size(181, 361);
            gr_filtering.TabIndex = 19;
            gr_filtering.TabStop = false;
            gr_filtering.Text = "Фильтрация";
            gr_filtering.UseCompatibleTextRendering = true;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Zero Cool", 9.75F);
            label23.ForeColor = Color.Black;
            label23.Location = new Point(5, 305);
            label23.Name = "label23";
            label23.Size = new Size(159, 15);
            label23.TabIndex = 28;
            label23.Text = "Выберите назначение";
            // 
            // fpl_chpurproom
            // 
            fpl_chpurproom.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chpurproom.Font = new Font("Doloto", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 204);
            fpl_chpurproom.ForeColor = Color.Black;
            fpl_chpurproom.Location = new Point(7, 327);
            fpl_chpurproom.Name = "fpl_chpurproom";
            fpl_chpurproom.Size = new Size(160, 28);
            fpl_chpurproom.TabIndex = 27;
            // 
            // lbl_chtyperoom
            // 
            lbl_chtyperoom.AutoSize = true;
            lbl_chtyperoom.Font = new Font("Zero Cool", 9.75F);
            lbl_chtyperoom.ForeColor = Color.Black;
            lbl_chtyperoom.Location = new Point(5, 237);
            lbl_chtyperoom.Name = "lbl_chtyperoom";
            lbl_chtyperoom.Size = new Size(172, 15);
            lbl_chtyperoom.TabIndex = 26;
            lbl_chtyperoom.Text = "Выберите тип аудитории";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Zero Cool", 9.75F);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(5, 167);
            label2.Name = "label2";
            label2.Size = new Size(156, 15);
            label2.TabIndex = 25;
            label2.Text = "Выберите тип корпуса";
            // 
            // lbl_cheq
            // 
            lbl_cheq.AutoSize = true;
            lbl_cheq.Font = new Font("Zero Cool", 9.75F);
            lbl_cheq.ForeColor = Color.Black;
            lbl_cheq.Location = new Point(7, 99);
            lbl_cheq.Name = "lbl_cheq";
            lbl_cheq.Size = new Size(173, 15);
            lbl_cheq.TabIndex = 24;
            lbl_cheq.Text = "Выберите оборудование";
            // 
            // lbl_chbuild
            // 
            lbl_chbuild.AutoSize = true;
            lbl_chbuild.Font = new Font("Zero Cool", 9.75F);
            lbl_chbuild.ForeColor = Color.Black;
            lbl_chbuild.Location = new Point(7, 29);
            lbl_chbuild.Name = "lbl_chbuild";
            lbl_chbuild.Size = new Size(121, 15);
            lbl_chbuild.TabIndex = 23;
            lbl_chbuild.Text = "Выберите корпус";
            // 
            // fpl_chbuild
            // 
            fpl_chbuild.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chbuild.Font = new Font("Doloto", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 204);
            fpl_chbuild.ForeColor = Color.Black;
            fpl_chbuild.Location = new Point(9, 50);
            fpl_chbuild.Name = "fpl_chbuild";
            fpl_chbuild.Size = new Size(160, 28);
            fpl_chbuild.TabIndex = 9;
            fpl_chbuild.SelectedIndexChanged += fpl_chbuild_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 70);
            label3.Name = "label3";
            label3.Size = new Size(0, 23);
            label3.TabIndex = 10;
            label3.Click += label3_Click;
            // 
            // fpl_chtyperoom
            // 
            fpl_chtyperoom.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chtyperoom.Font = new Font("Doloto", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 204);
            fpl_chtyperoom.ForeColor = Color.Black;
            fpl_chtyperoom.Location = new Point(7, 259);
            fpl_chtyperoom.Name = "fpl_chtyperoom";
            fpl_chtyperoom.Size = new Size(160, 28);
            fpl_chtyperoom.TabIndex = 15;
            // 
            // fpl_cheq
            // 
            fpl_cheq.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_cheq.Font = new Font("Doloto", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 204);
            fpl_cheq.ForeColor = Color.Black;
            fpl_cheq.Location = new Point(9, 120);
            fpl_cheq.Name = "fpl_cheq";
            fpl_cheq.Size = new Size(160, 28);
            fpl_cheq.TabIndex = 11;
            fpl_cheq.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // fpl_chtypebuild
            // 
            fpl_chtypebuild.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chtypebuild.Font = new Font("Doloto", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 204);
            fpl_chtypebuild.ForeColor = Color.Black;
            fpl_chtypebuild.Location = new Point(7, 189);
            fpl_chtypebuild.Name = "fpl_chtypebuild";
            fpl_chtypebuild.Size = new Size(160, 28);
            fpl_chtypebuild.TabIndex = 13;
            fpl_chtypebuild.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Zero Cool", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.DarkCyan;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(239, 18);
            label1.TabIndex = 7;
            label1.Text = "Информация о помещениях";
            label1.Click += label1_Click;
            // 
            // tabs
            // 
            tabs.Controls.Add(tabPage2);
            tabs.Controls.Add(page_edit);
            tabs.Font = new Font("Zero Cool", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabs.Location = new Point(4, 3);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new Size(867, 434);
            tabs.TabIndex = 2;
            // 
            // page_edit
            // 
            page_edit.Controls.Add(btnAdd);
            page_edit.Controls.Add(btnDel);
            page_edit.Controls.Add(btnEd);
            page_edit.Controls.Add(btnRep);
            page_edit.Controls.Add(tabControl1);
            page_edit.Controls.Add(label9);
            page_edit.Location = new Point(4, 24);
            page_edit.Name = "page_edit";
            page_edit.Padding = new Padding(3);
            page_edit.Size = new Size(859, 406);
            page_edit.TabIndex = 3;
            page_edit.Text = "Редактирование базы";
            page_edit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.ForeColor = Color.ForestGreen;
            btnAdd.Location = new Point(446, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(92, 28);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Добавить +";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            btnDel.ForeColor = Color.Firebrick;
            btnDel.Location = new Point(544, 3);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(93, 28);
            btnDel.TabIndex = 1;
            btnDel.Text = "Удалить -";
            btnDel.UseVisualStyleBackColor = true;
            // 
            // btnEd
            // 
            btnEd.ForeColor = Color.SteelBlue;
            btnEd.Location = new Point(643, 3);
            btnEd.Name = "btnEd";
            btnEd.Size = new Size(109, 28);
            btnEd.TabIndex = 2;
            btnEd.Text = "Отменить";
            btnEd.UseVisualStyleBackColor = true;
            // 
            // btnRep
            // 
            btnRep.ForeColor = Color.SlateBlue;
            btnRep.Location = new Point(758, 3);
            btnRep.Name = "btnRep";
            btnRep.Size = new Size(98, 28);
            btnRep.TabIndex = 3;
            btnRep.Text = "Сохранить";
            btnRep.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Location = new Point(0, 51);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(863, 359);
            tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(855, 331);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Здания";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnChFile);
            splitContainer1.Panel2.Controls.Add(pictureBox1);
            splitContainer1.Panel2.Controls.Add(textBox2);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(comboBox1);
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Panel2.Controls.Add(label7);
            splitContainer1.Panel2.Controls.Add(lblBuildType);
            splitContainer1.Panel2.Controls.Add(lblBuildName);
            splitContainer1.Size = new Size(849, 325);
            splitContainer1.SplitterDistance = 282;
            splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(282, 325);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_2;
            // 
            // btnChFile
            // 
            btnChFile.Location = new Point(246, 217);
            btnChFile.Name = "btnChFile";
            btnChFile.Size = new Size(229, 23);
            btnChFile.TabIndex = 10;
            btnChFile.Text = "Выберите изображение...";
            btnChFile.UseVisualStyleBackColor = true;
            btnChFile.Click += btnChFile_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ButtonFace;
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.LoadImage;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(246, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(304, 158);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(14, 216);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(189, 22);
            textBox2.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 196);
            label4.Name = "label4";
            label4.Size = new Size(105, 15);
            label4.TabIndex = 7;
            label4.Text = "Адрес корпуса";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(14, 143);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(189, 23);
            comboBox1.TabIndex = 6;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_2;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(14, 67);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(189, 22);
            textBox1.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(356, 33);
            label7.Name = "label7";
            label7.Size = new Size(97, 15);
            label7.TabIndex = 3;
            label7.Text = "Изображение";
            // 
            // lblBuildType
            // 
            lblBuildType.AutoSize = true;
            lblBuildType.Location = new Point(14, 123);
            lblBuildType.Name = "lblBuildType";
            lblBuildType.Size = new Size(88, 15);
            lblBuildType.TabIndex = 1;
            lblBuildType.Text = "Тип корпуса";
            // 
            // lblBuildName
            // 
            lblBuildName.AutoSize = true;
            lblBuildName.Location = new Point(14, 47);
            lblBuildName.Name = "lblBuildName";
            lblBuildName.Size = new Size(132, 15);
            lblBuildName.TabIndex = 0;
            lblBuildName.Text = "Название корпуса";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(splitContainer2);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(855, 331);
            tabPage3.TabIndex = 1;
            tabPage3.Text = "Аудитории";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(dataGridView2);
            splitContainer2.Panel1.Controls.Add(comboBox2);
            splitContainer2.Panel1.Paint += splitContainer2_Panel1_Paint;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(label14);
            splitContainer2.Panel2.Controls.Add(checkedListBox1);
            splitContainer2.Panel2.Controls.Add(comboBox6);
            splitContainer2.Panel2.Controls.Add(label13);
            splitContainer2.Panel2.Controls.Add(comboBox5);
            splitContainer2.Panel2.Controls.Add(label12);
            splitContainer2.Panel2.Controls.Add(textBox5);
            splitContainer2.Panel2.Controls.Add(label11);
            splitContainer2.Panel2.Controls.Add(textBox4);
            splitContainer2.Panel2.Controls.Add(label10);
            splitContainer2.Panel2.Controls.Add(comboBox4);
            splitContainer2.Panel2.Controls.Add(label8);
            splitContainer2.Panel2.Controls.Add(textBox3);
            splitContainer2.Panel2.Controls.Add(label6);
            splitContainer2.Panel2.Controls.Add(comboBox3);
            splitContainer2.Panel2.Controls.Add(label5);
            splitContainer2.Panel2.Paint += splitContainer2_Panel2_Paint;
            splitContainer2.Size = new Size(849, 325);
            splitContainer2.SplitterDistance = 282;
            splitContainer2.TabIndex = 0;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(3, 34);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(276, 288);
            dataGridView2.TabIndex = 1;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(3, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(276, 23);
            comboBox2.TabIndex = 0;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(305, 182);
            label14.Name = "label14";
            label14.Size = new Size(190, 15);
            label14.TabIndex = 22;
            label14.Text = "Оборудование в аудитории";
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(305, 202);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(206, 72);
            checkedListBox1.TabIndex = 21;
            // 
            // comboBox6
            // 
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.FormattingEnabled = true;
            comboBox6.Location = new Point(63, 249);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(206, 23);
            comboBox6.TabIndex = 20;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(63, 229);
            label13.Name = "label13";
            label13.Size = new Size(206, 15);
            label13.TabIndex = 19;
            label13.Text = "Отвественный за аудиторию";
            // 
            // comboBox5
            // 
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(305, 151);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(206, 23);
            comboBox5.TabIndex = 18;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(305, 131);
            label12.Name = "label12";
            label12.Size = new Size(206, 15);
            label12.TabIndex = 17;
            label12.Text = "Отвественный за аудиторию";
            // 
            // textBox5
            // 
            textBox5.BorderStyle = BorderStyle.FixedSingle;
            textBox5.Location = new Point(305, 94);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(206, 22);
            textBox5.TabIndex = 16;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(305, 74);
            label11.Name = "label11";
            label11.Size = new Size(135, 15);
            label11.TabIndex = 15;
            label11.Text = "Ширина аудитории";
            // 
            // textBox4
            // 
            textBox4.BorderStyle = BorderStyle.FixedSingle;
            textBox4.Location = new Point(305, 34);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(206, 22);
            textBox4.TabIndex = 14;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(305, 14);
            label10.Name = "label10";
            label10.Size = new Size(124, 15);
            label10.TabIndex = 13;
            label10.Text = "Длина аудитории";
            // 
            // comboBox4
            // 
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(63, 179);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(206, 23);
            comboBox4.TabIndex = 12;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(63, 159);
            label8.Name = "label8";
            label8.Size = new Size(104, 15);
            label8.TabIndex = 11;
            label8.Text = "Тип аудитории";
            // 
            // textBox3
            // 
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Location = new Point(63, 107);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(206, 22);
            textBox3.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(63, 87);
            label6.Name = "label6";
            label6.Size = new Size(125, 15);
            label6.TabIndex = 9;
            label6.Text = "Номер аудитории";
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(63, 34);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(206, 23);
            comboBox3.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(63, 14);
            label5.Name = "label5";
            label5.Size = new Size(121, 15);
            label5.TabIndex = 7;
            label5.Text = "Выберите корпус";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(splitContainer3);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(855, 331);
            tabPage4.TabIndex = 2;
            tabPage4.Text = "Оборудование";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(3, 3);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(comboBox7);
            splitContainer3.Panel1.Controls.Add(dataGridView3);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(pictureBox2);
            splitContainer3.Panel2.Controls.Add(label19);
            splitContainer3.Panel2.Controls.Add(richTextBox1);
            splitContainer3.Panel2.Controls.Add(label18);
            splitContainer3.Panel2.Controls.Add(textBox7);
            splitContainer3.Panel2.Controls.Add(label17);
            splitContainer3.Panel2.Controls.Add(textBox6);
            splitContainer3.Panel2.Controls.Add(label16);
            splitContainer3.Panel2.Controls.Add(comboBox8);
            splitContainer3.Panel2.Controls.Add(label15);
            splitContainer3.Size = new Size(849, 325);
            splitContainer3.SplitterDistance = 282;
            splitContainer3.TabIndex = 0;
            // 
            // comboBox7
            // 
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.FormattingEnabled = true;
            comboBox7.Location = new Point(3, 3);
            comboBox7.Name = "comboBox7";
            comboBox7.Size = new Size(276, 23);
            comboBox7.TabIndex = 1;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(3, 34);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.Size = new Size(276, 288);
            dataGridView3.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ButtonFace;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Image = Properties.Resources.LoadImage;
            pictureBox2.Location = new Point(239, 134);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(294, 165);
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(239, 114);
            label19.Name = "label19";
            label19.Size = new Size(97, 15);
            label19.TabIndex = 17;
            label19.Text = "Изображение";
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Location = new Point(239, 40);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(294, 66);
            richTextBox1.TabIndex = 16;
            richTextBox1.Text = "";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(239, 20);
            label18.Name = "label18";
            label18.Size = new Size(74, 15);
            label18.TabIndex = 15;
            label18.Text = "Описание";
            // 
            // textBox7
            // 
            textBox7.BorderStyle = BorderStyle.FixedSingle;
            textBox7.Location = new Point(24, 186);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(162, 22);
            textBox7.TabIndex = 14;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(24, 166);
            label17.Name = "label17";
            label17.Size = new Size(151, 15);
            label17.TabIndex = 13;
            label17.Text = "Инвентарный номер";
            // 
            // textBox6
            // 
            textBox6.BorderStyle = BorderStyle.FixedSingle;
            textBox6.Location = new Point(24, 111);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(162, 22);
            textBox6.TabIndex = 12;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(24, 91);
            label16.Name = "label16";
            label16.Size = new Size(74, 15);
            label16.TabIndex = 11;
            label16.Text = "Название";
            label16.Click += label16_Click;
            // 
            // comboBox8
            // 
            comboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox8.FormattingEnabled = true;
            comboBox8.Location = new Point(24, 40);
            comboBox8.Name = "comboBox8";
            comboBox8.Size = new Size(162, 23);
            comboBox8.TabIndex = 10;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(24, 20);
            label15.Name = "label15";
            label15.Size = new Size(78, 15);
            label15.TabIndex = 9;
            label15.Text = "Категория";
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(textBox10);
            tabPage5.Controls.Add(label22);
            tabPage5.Controls.Add(textBox9);
            tabPage5.Controls.Add(label21);
            tabPage5.Controls.Add(textBox8);
            tabPage5.Controls.Add(label20);
            tabPage5.Controls.Add(dataGridView4);
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(855, 331);
            tabPage5.TabIndex = 3;
            tabPage5.Text = "Ответственные";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // textBox10
            // 
            textBox10.BorderStyle = BorderStyle.FixedSingle;
            textBox10.Location = new Point(487, 209);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(207, 22);
            textBox10.TabIndex = 11;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(489, 189);
            label22.Name = "label22";
            label22.Size = new Size(189, 15);
            label22.TabIndex = 10;
            label22.Text = "Номер телефона(рабочий)";
            // 
            // textBox9
            // 
            textBox9.BorderStyle = BorderStyle.FixedSingle;
            textBox9.Location = new Point(487, 134);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(207, 22);
            textBox9.TabIndex = 9;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(487, 114);
            label21.Name = "label21";
            label21.Size = new Size(207, 15);
            label21.TabIndex = 8;
            label21.Text = "Должность ответственного";
            // 
            // textBox8
            // 
            textBox8.BorderStyle = BorderStyle.FixedSingle;
            textBox8.Location = new Point(487, 69);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(207, 22);
            textBox8.TabIndex = 7;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(489, 51);
            label20.Name = "label20";
            label20.Size = new Size(154, 15);
            label20.TabIndex = 6;
            label20.Text = "ФИО ответственного";
            label20.Click += label20_Click;
            // 
            // dataGridView4
            // 
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Location = new Point(6, 6);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.Size = new Size(389, 319);
            dataGridView4.TabIndex = 0;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Zero Cool", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label9.ForeColor = Color.DarkCyan;
            label9.Location = new Point(3, 3);
            label9.Name = "label9";
            label9.Size = new Size(348, 18);
            label9.TabIndex = 8;
            label9.Text = "Редактирование информационной базы";
            // 
            // MainFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(877, 490);
            Controls.Add(label_username);
            Controls.Add(btn_auth);
            Controls.Add(tabs);
            Name = "MainFrame";
            Text = "Manage Spaces";
            Load += Form1_Load;
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridview_foundroomsinfo).EndInit();
            gr_filtering.ResumeLayout(false);
            gr_filtering.PerformLayout();
            tabs.ResumeLayout(false);
            page_edit.ResumeLayout(false);
            page_edit.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPage3.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabPage4.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_auth;
        private Label label_username;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private DataGridView gridview_foundroomsinfo;
        private GroupBox gr_filtering;
        private ComboBox fpl_chbuild;
        private Label label3;
        private ComboBox fpl_chtyperoom;
        private ComboBox fpl_cheq;
        private ComboBox fpl_chtypebuild;
        private Label label1;
        private TabControl tabs;
        private TabPage page_edit;
        private Label label9;
        private Label lbl_chtyperoom;
        private Label label2;
        private Label lbl_cheq;
        private Label lbl_chbuild;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private Button btnRep;
        private Button btnEd;
        private Button btnDel;
        private Button btnAdd;
        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Label label7;
        private Label lblBuildType;
        private Label lblBuildName;
        private TextBox textBox2;
        private Label label4;
        private PictureBox pictureBox1;
        private Button btnChFile;
        private SplitContainer splitContainer2;
        private ComboBox comboBox2;
        private DataGridView dataGridView2;
        private TextBox textBox5;
        private Label label11;
        private TextBox textBox4;
        private Label label10;
        private ComboBox comboBox4;
        private Label label8;
        private TextBox textBox3;
        private Label label6;
        private ComboBox comboBox3;
        private Label label5;
        private Label label14;
        private CheckedListBox checkedListBox1;
        private ComboBox comboBox6;
        private Label label13;
        private ComboBox comboBox5;
        private Label label12;
        private SplitContainer splitContainer3;
        private DataGridView dataGridView3;
        private ComboBox comboBox7;
        private RichTextBox richTextBox1;
        private Label label18;
        private TextBox textBox7;
        private Label label17;
        private TextBox textBox6;
        private Label label16;
        private ComboBox comboBox8;
        private Label label15;
        private PictureBox pictureBox2;
        private Label label19;
        private DataGridView dataGridView4;
        private TextBox textBox10;
        private Label label22;
        private TextBox textBox9;
        private Label label21;
        private TextBox textBox8;
        private Label label20;
        private Label label23;
        private ComboBox fpl_chpurproom;
    }
}
