namespace ManageSpacesOfInstitute
{
    partial class RoomDetails
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
            label1 = new Label();
            lbl_id_input = new Label();
            BuildingImage = new PictureBox();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Габариты = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            flp = new FlowLayoutPanel();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)BuildingImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Zero Cool", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.DarkCyan;
            label1.Location = new Point(14, 6);
            label1.Name = "label1";
            label1.Size = new Size(272, 18);
            label1.TabIndex = 0;
            label1.Text = "Полная информация аудитории";
            // 
            // lbl_id_input
            // 
            lbl_id_input.AutoSize = true;
            lbl_id_input.Location = new Point(62, 56);
            lbl_id_input.Name = "lbl_id_input";
            lbl_id_input.Size = new Size(0, 11);
            lbl_id_input.TabIndex = 2;
            // 
            // BuildingImage
            // 
            BuildingImage.Location = new Point(14, 30);
            BuildingImage.Margin = new Padding(3, 2, 3, 2);
            BuildingImage.Name = "BuildingImage";
            BuildingImage.Size = new Size(403, 227);
            BuildingImage.TabIndex = 10;
            BuildingImage.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.ButtonFace;
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column5, Габариты, Column4, Column9, Column3, Column2, Column6, Column8 });
            dataGridView1.Location = new Point(14, 262);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.Size = new Size(798, 70);
            dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "Аудитория";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 104;
            // 
            // Column5
            // 
            Column5.HeaderText = "Тип аудитории";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 119;
            // 
            // Габариты
            // 
            Габариты.HeaderText = "Назначение аудитории";
            Габариты.Name = "Габариты";
            Габариты.ReadOnly = true;
            Габариты.Width = 169;
            // 
            // Column4
            // 
            Column4.HeaderText = "Площадь аудитории";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 153;
            // 
            // Column9
            // 
            Column9.HeaderText = "Габариты";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            Column9.Width = 95;
            // 
            // Column3
            // 
            Column3.HeaderText = "Отдел";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 73;
            // 
            // Column2
            // 
            Column2.HeaderText = "Корпус";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 79;
            // 
            // Column6
            // 
            Column6.HeaderText = "Тип корпуса";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 104;
            // 
            // Column8
            // 
            Column8.HeaderText = "Адрес корпуса";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Width = 120;
            // 
            // flp
            // 
            flp.AutoScroll = true;
            flp.BackColor = SystemColors.Menu;
            flp.Location = new Point(420, 30);
            flp.Margin = new Padding(0);
            flp.Name = "flp";
            flp.Size = new Size(392, 227);
            flp.TabIndex = 0;
            flp.Paint += flp_Paint;
            // 
            // button1
            // 
            button1.Font = new Font("Doloto", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(14, 338);
            button1.Name = "button1";
            button1.Size = new Size(213, 40);
            button1.TabIndex = 11;
            button1.Text = "Показать структуру по аудитории";
            button1.UseVisualStyleBackColor = true;
            // 
            // RoomDetails
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(824, 390);
            Controls.Add(button1);
            Controls.Add(flp);
            Controls.Add(dataGridView1);
            Controls.Add(BuildingImage);
            Controls.Add(lbl_id_input);
            Controls.Add(label1);
            Font = new Font("Yuruka Kerning (sherbackoffalex", 8.25F, FontStyle.Bold);
            Margin = new Padding(3, 2, 3, 2);
            Name = "RoomDetails";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RoomDetails";
            ((System.ComponentModel.ISupportInitialize)BuildingImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lbl_id_input;
        private PictureBox BuildingImage;
        private DataGridView dataGridView1;
        private FlowLayoutPanel flp;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Габариты;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column8;
        private Button button1;
    }
}