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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Auth));
            btnAuth = new Button();
            txtLogin = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtPass = new TextBox();
            SuspendLayout();
            // 
            // btnAuth
            // 
            btnAuth.ForeColor = Color.DarkCyan;
            btnAuth.Location = new Point(15, 163);
            btnAuth.Margin = new Padding(6, 5, 6, 5);
            btnAuth.Name = "btnAuth";
            btnAuth.Size = new Size(234, 46);
            btnAuth.TabIndex = 1;
            btnAuth.Text = "Вход";
            btnAuth.UseVisualStyleBackColor = true;
            btnAuth.Click += button2_Click;
            // 
            // txtLogin
            // 
            txtLogin.Font = new Font("Doloto", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtLogin.Location = new Point(15, 41);
            txtLogin.Margin = new Padding(6, 5, 6, 5);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(234, 31);
            txtLogin.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.DarkCyan;
            label1.Location = new Point(15, 12);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(226, 24);
            label1.TabIndex = 3;
            label1.Text = "Введите свой логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.DarkCyan;
            label2.Location = new Point(15, 93);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(234, 24);
            label2.TabIndex = 5;
            label2.Text = "Введите свой пароль";
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Doloto", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtPass.Location = new Point(15, 122);
            txtPass.Margin = new Padding(6, 5, 6, 5);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '●';
            txtPass.Size = new Size(234, 31);
            txtPass.TabIndex = 4;
            // 
            // Auth
            // 
            AutoScaleDimensions = new SizeF(13F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(264, 223);
            Controls.Add(label2);
            Controls.Add(txtPass);
            Controls.Add(label1);
            Controls.Add(txtLogin);
            Controls.Add(btnAuth);
            Font = new Font("Zero Cool", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 5, 6, 5);
            Name = "Auth";
            StartPosition = FormStartPosition.CenterParent;
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