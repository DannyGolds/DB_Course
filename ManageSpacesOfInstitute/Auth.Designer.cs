namespace ManageSpacesOfInstitute
{
    partial class Auth
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
            btnAuth = new Button();
            txtLogin = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtPass = new TextBox();
            SuspendLayout();
            // 
            // btnAuth
            // 
            btnAuth.Location = new Point(64, 112);
            btnAuth.Name = "btnAuth";
            btnAuth.Size = new Size(102, 29);
            btnAuth.TabIndex = 1;
            btnAuth.Text = "Вход";
            btnAuth.UseVisualStyleBackColor = true;
            btnAuth.Click += button2_Click;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(34, 30);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(169, 23);
            txtLogin.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 12);
            label1.Name = "label1";
            label1.Size = new Size(115, 15);
            label1.TabIndex = 3;
            label1.Text = "Введите свой логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 65);
            label2.Name = "label2";
            label2.Size = new Size(122, 15);
            label2.TabIndex = 5;
            label2.Text = "Введите свой пароль";
            // 
            // txtPass
            // 
            txtPass.Location = new Point(34, 83);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(169, 23);
            txtPass.TabIndex = 4;
            // 
            // Auth
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(235, 147);
            Controls.Add(label2);
            Controls.Add(txtPass);
            Controls.Add(label1);
            Controls.Add(txtLogin);
            Controls.Add(btnAuth);
            Name = "Auth";
            Text = "Auth";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAuth;
        private TextBox txtLogin;
        private Label label1;
        private Label label2;
        private TextBox txtPass;
    }
}