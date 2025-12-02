namespace ManageSpacesOfInstitute
{
    partial class EquipmentCard
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            pcEq = new PictureBox();
            richTxt = new RichTextBox();
            lblEqNm = new Label();
            ((System.ComponentModel.ISupportInitialize)pcEq).BeginInit();
            SuspendLayout();
            // 
            // pcEq
            // 
            pcEq.BackgroundImage = Properties.Resources.LoadImage;
            pcEq.Location = new Point(3, 24);
            pcEq.Name = "pcEq";
            pcEq.Size = new Size(302, 171);
            pcEq.TabIndex = 0;
            pcEq.TabStop = false;
            // 
            // richTxt
            // 
            richTxt.Enabled = false;
            richTxt.Location = new Point(3, 201);
            richTxt.Name = "richTxt";
            richTxt.ReadOnly = true;
            richTxt.Size = new Size(302, 71);
            richTxt.TabIndex = 2;
            richTxt.Text = "";
            richTxt.TextChanged += richTextBox1_TextChanged;
            // 
            // lblEqNm
            // 
            lblEqNm.AutoSize = true;
            lblEqNm.Location = new Point(3, 6);
            lblEqNm.Name = "lblEqNm";
            lblEqNm.Size = new Size(38, 15);
            lblEqNm.TabIndex = 3;
            lblEqNm.Text = "label1";
            // 
            // EquipmentCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            Controls.Add(lblEqNm);
            Controls.Add(richTxt);
            Controls.Add(pcEq);
            Name = "EquipmentCard";
            Size = new Size(308, 275);
            ((System.ComponentModel.ISupportInitialize)pcEq).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox pcEq;
        private RichTextBox richTxt;
        private Label lblEqNm;
    }
}
