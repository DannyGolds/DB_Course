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
            lblRoomNumber = new Label();
            lblBuilding = new Label();
            lblRoomType = new Label();
            lblWidth = new Label();
            lblArea = new Label();
            BuildingImage = new PictureBox();
            groupBox1 = new GroupBox();
            lblAdress = new Label();
            lbl2 = new Label();
            lblDep = new Label();
            label7 = new Label();
            lblBuildingType = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            lbl_2 = new Label();
            groupBox2 = new GroupBox();
            flp = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)BuildingImage).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = SystemColors.InfoText;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(276, 19);
            label1.TabIndex = 0;
            label1.Text = "Полная информация аудитории";
            // 
            // lbl_id_input
            // 
            lbl_id_input.AutoSize = true;
            lbl_id_input.Location = new Point(54, 76);
            lbl_id_input.Name = "lbl_id_input";
            lbl_id_input.Size = new Size(0, 15);
            lbl_id_input.TabIndex = 2;
            // 
            // lblRoomNumber
            // 
            lblRoomNumber.AutoSize = true;
            lblRoomNumber.Location = new Point(153, 29);
            lblRoomNumber.Name = "lblRoomNumber";
            lblRoomNumber.Size = new Size(38, 15);
            lblRoomNumber.TabIndex = 3;
            lblRoomNumber.Text = "label2";
            lblRoomNumber.Click += lblRoomNumber_Click;
            // 
            // lblBuilding
            // 
            lblBuilding.AutoSize = true;
            lblBuilding.Location = new Point(139, 55);
            lblBuilding.Name = "lblBuilding";
            lblBuilding.Size = new Size(38, 15);
            lblBuilding.TabIndex = 4;
            lblBuilding.Text = "label2";
            // 
            // lblRoomType
            // 
            lblRoomType.AutoSize = true;
            lblRoomType.Location = new Point(127, 80);
            lblRoomType.Name = "lblRoomType";
            lblRoomType.Size = new Size(38, 15);
            lblRoomType.TabIndex = 5;
            lblRoomType.Text = "label2";
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(105, 128);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(38, 15);
            lblWidth.TabIndex = 6;
            lblWidth.Text = "label2";
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Location = new Point(164, 150);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(38, 15);
            lblArea.TabIndex = 7;
            lblArea.Text = "label2";
            lblArea.Click += lblArea_Click;
            // 
            // BuildingImage
            // 
            BuildingImage.Location = new Point(12, 41);
            BuildingImage.Name = "BuildingImage";
            BuildingImage.Size = new Size(513, 300);
            BuildingImage.TabIndex = 10;
            BuildingImage.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblAdress);
            groupBox1.Controls.Add(lbl2);
            groupBox1.Controls.Add(lblDep);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(lblBuildingType);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(lbl_2);
            groupBox1.Controls.Add(lblRoomNumber);
            groupBox1.Controls.Add(lblRoomType);
            groupBox1.Controls.Add(lblWidth);
            groupBox1.Controls.Add(lblArea);
            groupBox1.Controls.Add(lblBuilding);
            groupBox1.Location = new Point(531, 41);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(436, 300);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Сведения";
            // 
            // lblAdress
            // 
            lblAdress.AutoSize = true;
            lblAdress.Location = new Point(138, 202);
            lblAdress.Name = "lblAdress";
            lblAdress.Size = new Size(38, 15);
            lblAdress.TabIndex = 20;
            lblAdress.Text = "label8";
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.Location = new Point(49, 202);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(91, 15);
            lbl2.TabIndex = 19;
            lbl2.Text = "Адрес корпуса:";
            // 
            // lblDep
            // 
            lblDep.AutoSize = true;
            lblDep.Location = new Point(84, 177);
            lblDep.Name = "lblDep";
            lblDep.Size = new Size(38, 15);
            lblDep.TabIndex = 18;
            lblDep.Text = "label8";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(43, 177);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 17;
            label7.Text = "Отдел:";
            label7.Click += label7_Click;
            // 
            // lblBuildingType
            // 
            lblBuildingType.AutoSize = true;
            lblBuildingType.Location = new Point(122, 105);
            lblBuildingType.Name = "lblBuildingType";
            lblBuildingType.Size = new Size(38, 15);
            lblBuildingType.TabIndex = 16;
            lblBuildingType.Text = "label7";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(43, 150);
            label6.Name = "label6";
            label6.Size = new Size(123, 15);
            label6.TabIndex = 15;
            label6.Text = "Площадь аудитории:";
            label6.Click += label6_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(44, 128);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 14;
            label5.Text = "Габариты:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 105);
            label4.Name = "label4";
            label4.Size = new Size(79, 15);
            label4.TabIndex = 13;
            label4.Text = "Тип корпуса:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 80);
            label3.Name = "label3";
            label3.Size = new Size(84, 15);
            label3.TabIndex = 12;
            label3.Text = "Тип кабинета:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 55);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 11;
            label2.Text = "Номер корпуса:";
            label2.Click += label2_Click_1;
            // 
            // lbl_2
            // 
            lbl_2.AutoSize = true;
            lbl_2.Location = new Point(45, 29);
            lbl_2.Name = "lbl_2";
            lbl_2.Size = new Size(109, 15);
            lbl_2.TabIndex = 10;
            lbl_2.Text = "Номер аудитории:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(flp);
            groupBox2.Location = new Point(12, 347);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(955, 310);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Оборудование в аудитории";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // flp
            // 
            flp.BackColor = SystemColors.ControlDark;
            flp.Location = new Point(6, 22);
            flp.Name = "flp";
            flp.Size = new Size(943, 281);
            flp.TabIndex = 0;
            flp.Paint += flp_Paint;
            // 
            // RoomDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(987, 668);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(BuildingImage);
            Controls.Add(lbl_id_input);
            Controls.Add(label1);
            Name = "RoomDetails";
            Text = "RoomDetails";
            ((System.ComponentModel.ISupportInitialize)BuildingImage).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lbl_id_input;
        private Label lblRoomNumber;
        private Label lblBuilding;
        private Label lblRoomType;
        private Label lblWidth;
        private Label lblArea;
        private PictureBox BuildingImage;
        private GroupBox groupBox1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label lbl_2;
        private Label lblBuildingType;
        private GroupBox groupBox2;
        private Label lblDep;
        private Label label7;
        private Label lblAdress;
        private Label lbl2;
        private FlowLayoutPanel flp;
    }
}