// File: Forms/LoginForm.cs
using System;
using System.Windows.Forms;
using SocietiesMS.DAL;
using SocietiesMS.Models;

namespace SocietiesMS.Forms
{
    /// <summary>
    /// Login screen. Routes to correct dashboard based on user role.
    /// </summary>
    public class LoginForm : Form
    {
        private Label      lblTitle, lblEmail, lblPassword, lblStatus;
        private TextBox    txtEmail, txtPassword;
        private Button     btnLogin, btnRegister;

        // ----------------------------------------------------------------
        // Constructor
        // ----------------------------------------------------------------
        public LoginForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text            = "Societies Management System – Login";
            this.Size            = new System.Drawing.Size(400, 350);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;

            lblTitle = new Label  { Text = "FAST Societies MS", Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                                    Location = new System.Drawing.Point(80, 20), Size = new System.Drawing.Size(240, 35) };

            lblEmail    = new Label  { Text = "Email:",    Location = new System.Drawing.Point(40, 80),  Size = new System.Drawing.Size(80, 25) };
            txtEmail    = new TextBox { Location = new System.Drawing.Point(130, 78), Size = new System.Drawing.Size(210, 25) };

            lblPassword = new Label  { Text = "Password:", Location = new System.Drawing.Point(40, 120), Size = new System.Drawing.Size(80, 25) };
            txtPassword = new TextBox { Location = new System.Drawing.Point(130, 118), Size = new System.Drawing.Size(210, 25), UseSystemPasswordChar = true };

            btnLogin    = new Button { Text = "Login",    Location = new System.Drawing.Point(130, 165), Size = new System.Drawing.Size(90, 30) };
            btnRegister = new Button { Text = "Register", Location = new System.Drawing.Point(250, 165), Size = new System.Drawing.Size(90, 30) };

            lblStatus   = new Label  { Location = new System.Drawing.Point(40, 210), Size = new System.Drawing.Size(310, 50),
                                       ForeColor = System.Drawing.Color.Red };

            btnLogin.Click    += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;

            this.Controls.AddRange(new Control[]
                { lblTitle, lblEmail, txtEmail, lblPassword, txtPassword,
                  btnLogin, btnRegister, lblStatus });
        }

        // ----------------------------------------------------------------
        // BtnLogin_Click – authenticate and route by role
        // CC = 4 (null check + 3 role branches)
        // ----------------------------------------------------------------
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblStatus.Text = "Please enter email and password.";
                return;
            }

            User user = UserDAL.Login(txtEmail.Text.Trim(), txtPassword.Text);
            if (user == null)
            {
                lblStatus.Text = "Invalid credentials. Please try again.";
                return;
            }

            // Route to correct dashboard
            Form dashboard;
            if      (user.Role == "Admin")        dashboard = new AdminDashboard(user);
            else if (user.Role == "SocietyHead")  dashboard = new SocietyDashboard(user);
            else                                   dashboard = new StudentDashboard(user);

            this.Hide();
            dashboard.ShowDialog();
            this.Show();
        }

        // ----------------------------------------------------------------
        // BtnRegister_Click – open registration form
        // CC = 1
        // ----------------------------------------------------------------
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            rf.ShowDialog();
        }
    }
}
