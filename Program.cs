// File: Program.cs
using System;
using System.Windows.Forms;
using SocietiesMS.DAL;
using SocietiesMS.Forms;

namespace SocietiesMS
{
    static class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Verify DB connectivity before launching UI
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show(
                    "Cannot connect to SQL Server.\n" +
                    "Please ensure SQL Server is running and the connection string in\n" +
                    "DAL/DatabaseHelper.cs is correct.",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new LoginForm());
        }
    }
}
