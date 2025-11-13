using System.Windows.Forms;

namespace ManageSpacesOfInstitute
{
    // Простой модальный диалог для показа полного описания
    public class DetailForm : Form
    {
        private readonly TextBox txtDescription;
        private readonly Label lblTitle;
        private readonly Button btnClose;

        public DetailForm(string title, string description)
        {
            Text = "Полное описание";
            Width = 600;
            Height = 400;
            StartPosition = FormStartPosition.CenterParent;

            lblTitle = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                AutoSize = false,
                Height = 30,
                Padding = new Padding(8),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold)
            };

            txtDescription = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Text = description,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };

            btnClose = new Button
            {
                Text = "Закрыть",
                Dock = DockStyle.Bottom,
                Height = 36
            };
            btnClose.Click += (s, e) => Close();

            Controls.Add(txtDescription);
            Controls.Add(lblTitle);
            Controls.Add(btnClose);
        }
    }
}