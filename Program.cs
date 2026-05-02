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
            // Set up WinForms environment
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Verify Database connectivity (Audit requirement)
            if (!DAL.DatabaseHelper.TestConnection())
            {
                MessageBox.Show(
                    "Database Connection Failed!\nPlease check SQL Server settings in DatabaseHelper.cs",
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Start with the professional WinForms Login
            Application.Run(new Forms.LoginForm());
        }
    }
}
