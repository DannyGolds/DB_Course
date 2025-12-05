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
            lblEqNm = new Label();
            richTxt = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pcEq).BeginInit();
            SuspendLayout();
            // 
            // pcEq
            // 
            pcEq.Location = new Point(3, 29);
            pcEq.Margin = new Padding(3, 4, 3, 4);
            pcEq.Name = "pcEq";
            pcEq.Size = new Size(167, 143);
            pcEq.TabIndex = 0;
            pcEq.TabStop = false;
            // 
            // lblEqNm
            // 
            lblEqNm.AutoSize = true;
            lblEqNm.Font = new Font("Zero Cool", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblEqNm.ForeColor = Color.DarkCyan;
            lblEqNm.Location = new Point(5, 3);
            lblEqNm.Name = "lblEqNm";
            lblEqNm.Size = new Size(62, 18);
            lblEqNm.TabIndex = 3;
            lblEqNm.Text = "label1";
            lblEqNm.Click += lblEqNm_Click;
            // 
            // richTxt
            // 
            richTxt.BackColor = Color.Black;
            richTxt.BorderStyle = BorderStyle.None;
            richTxt.Enabled = false;
            richTxt.Font = new Font("Doloto", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 204);
            richTxt.Location = new Point(3, 180);
            richTxt.Margin = new Padding(3, 4, 3, 4);
            richTxt.Name = "richTxt";
            richTxt.ReadOnly = true;
            richTxt.Size = new Size(167, 39);
            richTxt.TabIndex = 2;
            richTxt.Text = "";
            richTxt.TextChanged += richTextBox1_TextChanged;
            // 
            // EquipmentCard
            // 
            AutoScaleDimensions = new SizeF(5F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblEqNm);
            Controls.Add(richTxt);
            Controls.Add(pcEq);
            Font = new Font("Doloto", 14.2499981F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EquipmentCard";
            Size = new Size(176, 223);
            ((System.ComponentModel.ISupportInitialize)pcEq).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox pcEq;
        private Label lblEqNm;
        private RichTextBox richTxt;
    }
}
