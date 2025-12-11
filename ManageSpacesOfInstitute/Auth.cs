using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSpacesOfInstitute
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
        public string loggedUser;
        public string accessLevel;
        private async void button2_Click(object sender, EventArgs e)
        {
            string username = txtLogin.Text.Trim();
            string password = txtPass.Text;
            

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await using var db = new DBOperations();

                // Получаем хэш из БД
                var dt = await db.CallProcedureAsync(
                    "GET_USER_BY_USERNAME",
                    new List<string> { "USER_ID", "PASSWORD_HASH", "ACCESSLEVEL"},
                    new FbParameter("P_USERNAME", username)
                );

                if (dt.Rows.Count == 0)
                {
                    Shared.ShowNotify("Действия с аккаунтом", "Пользователь не найден");
                    return;
                }

                string storedHash = dt.Rows[0]["PASSWORD_HASH"].ToString();

                // Верификация пароля
                if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                {
                    loggedUser = username;
                    accessLevel = dt.Rows[0]["ACCESSLEVEL"].ToString();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    Shared.ShowNotify("Действия с аккаунтом", "Неверный пароль!");
                }
            }
            catch (Exception ex)
            {
                Shared.ShowNotify("Действия с аккаунтом", "Неверный логин или пароль");
            }
        }
    }
}
