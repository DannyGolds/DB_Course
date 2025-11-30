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
            ((System.ComponentModel.ISupportInitialize)pcEq).BeginInit();
            SuspendLayout();
            // 
            // pcEq
            // 
            pcEq.Location = new Point(3, 44);
            pcEq.Name = "pcEq";
            pcEq.Size = new Size(292, 238);
            pcEq.TabIndex = 0;
            pcEq.TabStop = false;
            // 
            // lblEqNm
            // 
            lblEqNm.AutoSize = true;
            lblEqNm.Location = new Point(65, 16);
            lblEqNm.Name = "lblEqNm";
            lblEqNm.Size = new Size(38, 15);
            lblEqNm.TabIndex = 1;
            lblEqNm.Text = "label1";
            // 
            // EquipmentCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblEqNm);
            Controls.Add(pcEq);
            Name = "EquipmentCard";
            Size = new Size(298, 285);
            ((System.ComponentModel.ISupportInitialize)pcEq).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pcEq;
        private Label lblEqNm;
    }
}
