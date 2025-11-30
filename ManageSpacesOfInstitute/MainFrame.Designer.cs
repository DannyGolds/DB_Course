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
            page_struct = new TabPage();
            tabPage2 = new TabPage();
            groupBox1 = new GroupBox();
            gridview_foundroomsinfo = new DataGridView();
            gr_filtering = new GroupBox();
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
            label9 = new Label();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridview_foundroomsinfo).BeginInit();
            gr_filtering.SuspendLayout();
            tabs.SuspendLayout();
            page_edit.SuspendLayout();
            SuspendLayout();
            // 
            // btn_auth
            // 
            btn_auth.Location = new Point(666, 414);
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
            label_username.Location = new Point(4, 417);
            label_username.Name = "label_username";
            label_username.Size = new Size(53, 23);
            label_username.TabIndex = 4;
            label_username.Text = "Гость";
            // 
            // page_struct
            // 
            page_struct.Location = new Point(4, 26);
            page_struct.Name = "page_struct";
            page_struct.Padding = new Padding(3);
            page_struct.Size = new Size(784, 375);
            page_struct.TabIndex = 2;
            page_struct.Text = "Структура помещений";
            page_struct.UseVisualStyleBackColor = true;
            page_struct.Click += page_struct_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Controls.Add(gr_filtering);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(784, 375);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Поиск помещений";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(gridview_foundroomsinfo);
            groupBox1.Location = new Point(195, 39);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(583, 331);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Сведения о найденных помещениях";
            // 
            // gridview_foundroomsinfo
            // 
            gridview_foundroomsinfo.AllowUserToAddRows = false;
            gridview_foundroomsinfo.AllowUserToDeleteRows = false;
            gridview_foundroomsinfo.AllowUserToResizeColumns = false;
            gridview_foundroomsinfo.AllowUserToResizeRows = false;
            gridview_foundroomsinfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridview_foundroomsinfo.Location = new Point(6, 22);
            gridview_foundroomsinfo.Name = "gridview_foundroomsinfo";
            gridview_foundroomsinfo.Size = new Size(571, 299);
            gridview_foundroomsinfo.TabIndex = 20;
            gridview_foundroomsinfo.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // gr_filtering
            // 
            gr_filtering.Controls.Add(lbl_chtyperoom);
            gr_filtering.Controls.Add(label2);
            gr_filtering.Controls.Add(lbl_cheq);
            gr_filtering.Controls.Add(lbl_chbuild);
            gr_filtering.Controls.Add(fpl_chbuild);
            gr_filtering.Controls.Add(label3);
            gr_filtering.Controls.Add(fpl_chtyperoom);
            gr_filtering.Controls.Add(fpl_cheq);
            gr_filtering.Controls.Add(fpl_chtypebuild);
            gr_filtering.Location = new Point(8, 39);
            gr_filtering.Name = "gr_filtering";
            gr_filtering.Size = new Size(181, 234);
            gr_filtering.TabIndex = 19;
            gr_filtering.TabStop = false;
            gr_filtering.Text = "Фильтрация";
            gr_filtering.UseCompatibleTextRendering = true;
            // 
            // lbl_chtyperoom
            // 
            lbl_chtyperoom.AutoSize = true;
            lbl_chtyperoom.Location = new Point(8, 176);
            lbl_chtyperoom.Name = "lbl_chtyperoom";
            lbl_chtyperoom.Size = new Size(155, 17);
            lbl_chtyperoom.TabIndex = 26;
            lbl_chtyperoom.Text = "Выберите тип аудитории";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 124);
            label2.Name = "label2";
            label2.Size = new Size(138, 17);
            label2.TabIndex = 25;
            label2.Text = "Выберите тип корпуса";
            // 
            // lbl_cheq
            // 
            lbl_cheq.AutoSize = true;
            lbl_cheq.Location = new Point(8, 73);
            lbl_cheq.Name = "lbl_cheq";
            lbl_cheq.Size = new Size(146, 17);
            lbl_cheq.TabIndex = 24;
            lbl_cheq.Text = "Выберите оборудование";
            // 
            // lbl_chbuild
            // 
            lbl_chbuild.AutoSize = true;
            lbl_chbuild.Location = new Point(8, 22);
            lbl_chbuild.Name = "lbl_chbuild";
            lbl_chbuild.Size = new Size(106, 17);
            lbl_chbuild.TabIndex = 23;
            lbl_chbuild.Text = "Выберите корпус";
            // 
            // fpl_chbuild
            // 
            fpl_chbuild.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chbuild.Location = new Point(10, 43);
            fpl_chbuild.Name = "fpl_chbuild";
            fpl_chbuild.Size = new Size(160, 25);
            fpl_chbuild.TabIndex = 9;
            fpl_chbuild.SelectedIndexChanged += fpl_chbuild_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 70);
            label3.Name = "label3";
            label3.Size = new Size(0, 17);
            label3.TabIndex = 10;
            label3.Click += label3_Click;
            // 
            // fpl_chtyperoom
            // 
            fpl_chtyperoom.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chtyperoom.Location = new Point(10, 198);
            fpl_chtyperoom.Name = "fpl_chtyperoom";
            fpl_chtyperoom.Size = new Size(160, 25);
            fpl_chtyperoom.TabIndex = 15;
            // 
            // fpl_cheq
            // 
            fpl_cheq.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_cheq.Location = new Point(10, 94);
            fpl_cheq.Name = "fpl_cheq";
            fpl_cheq.Size = new Size(160, 25);
            fpl_cheq.TabIndex = 11;
            fpl_cheq.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // fpl_chtypebuild
            // 
            fpl_chtypebuild.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chtypebuild.Location = new Point(10, 146);
            fpl_chtypebuild.Name = "fpl_chtypebuild";
            fpl_chtypebuild.Size = new Size(160, 25);
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
            // tabs
            // 
            tabs.Controls.Add(tabPage2);
            tabs.Controls.Add(page_struct);
            tabs.Controls.Add(page_edit);
            tabs.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tabs.Location = new Point(4, 3);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new Size(792, 405);
            tabs.TabIndex = 2;
            // 
            // page_edit
            // 
            page_edit.Controls.Add(label9);
            page_edit.Location = new Point(4, 26);
            page_edit.Name = "page_edit";
            page_edit.Padding = new Padding(3);
            page_edit.Size = new Size(784, 375);
            page_edit.TabIndex = 3;
            page_edit.Text = "Редактирование";
            page_edit.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label9.ForeColor = Color.DarkCyan;
            label9.Location = new Point(6, 6);
            label9.Name = "label9";
            label9.Size = new Size(469, 33);
            label9.TabIndex = 8;
            label9.Text = "Редактирование информационной базы";
            // 
            // MainFrame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 449);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_auth;
        private Label label_username;
        private TabPage page_struct;
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
    }
}
