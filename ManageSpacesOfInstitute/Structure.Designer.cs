namespace ManageSpacesOfInstitute
{
    partial class Structure
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
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.ButtonFace;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Enabled = false;
            richTextBox1.Font = new Font("Zero Cool", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            richTextBox1.Location = new Point(12, 43);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(375, 109);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.DarkCyan;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(375, 31);
            label1.TabIndex = 1;
            label1.Text = "Структура по аудитории";
            // 
            // Structure
            // 
            AutoScaleDimensions = new SizeF(18F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 161);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Font = new Font("Zero Cool", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Margin = new Padding(8, 6, 8, 6);
            Name = "Structure";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Structure";
            Load += Structure_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Label label1;
    }
}