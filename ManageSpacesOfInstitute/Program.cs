using FirebirdSql.Data.FirebirdClient;
using Microsoft.VisualBasic.Devices;

namespace ManageSpacesOfInstitute
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrame());

        }

    }
}