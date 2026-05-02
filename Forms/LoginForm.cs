using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SocietiesMS.DAL;
using SocietiesMS.Models;

namespace SocietiesMS.Forms
{
    /// <summary>
    /// Professional Windows Forms Login for FAST Societies MS.
    /// Meets project audit requirements for SE-4011.
    /// </summary>
    public class LoginForm : Form
    {
        private Panel      pnlLeft, pnlRight, pnlFull;
        private PictureBox picLogo;
        private Label      lblTitle, lblStatus, lblWelcome, lblSubTitle;
        private TextBox    txtEmail, txtPassword;
        private Button     btnLogin, btnRegister, btnToggle;

        public LoginForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Basic Form Settings (Frameless for Modern Look)
            this.Text            = "FAST Societies Management System";
            this.Size            = new Size(1000, 600);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;

            // Full Panel
            pnlFull = new Panel { Dock = DockStyle.Fill };

            // Left Side: Branding (Navy)
            pnlLeft = new Panel
            {
                Dock = DockStyle.Left,
                Width = 450,
                BackColor = UIStyle.Navy
            };

            picLogo = new PictureBox
            {
                Size = new Size(180, 180),
                Location = new Point(135, 120),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fast_Logo.png");
            if (!File.Exists(logoPath)) logoPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "fast_Logo.png");
            if (File.Exists(logoPath)) picLogo.Image = Image.FromFile(logoPath);

            lblTitle = new Label
            {
                Text = "FAST UNIVERSITY",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 320),
                Size = new Size(450, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblSubTitle = new Label
            {
                Text = "Societies Management System",
                Font = new Font("Segoe UI", 12),
                ForeColor = UIStyle.LightBlue,
                Location = new Point(0, 370),
                Size = new Size(450, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlLeft.Controls.AddRange(new Control[] { picLogo, lblTitle, lblSubTitle });

            // Right Side: Login Form (White)
            pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(50)
            };

            lblWelcome = new Label
            {
                Text = "Welcome Back",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = UIStyle.Navy,
                Location = new Point(50, 80),
                Size = new Size(450, 60)
            };

            var lblPrompt = new Label
            {
                Text = "Please sign in to your account",
                Font = new Font("Segoe UI", 10),
                ForeColor = UIStyle.DarkGray,
                Location = new Point(55, 140),
                Size = new Size(400, 30)
            };

            int startY = 220;
            
            var lblEmail = new Label { Text = "University Email", Location = new Point(55, startY), Size = new Size(400, 20), Font = UIStyle.SmallFont, ForeColor = UIStyle.DarkGray };
            txtEmail = UIStyle.CreateModernTextBox("k191234@nu.edu.pk");
            txtEmail.Location = new Point(55, startY + 25);
            txtEmail.Size = new Size(400, 35);

            var lblPass = new Label { Text = "Password", Location = new Point(55, startY + 80), Size = new Size(400, 20), Font = UIStyle.SmallFont, ForeColor = UIStyle.DarkGray };
            txtPassword = UIStyle.CreateModernTextBox("••••••••", true);
            txtPassword.Location = new Point(55, startY + 105);
            txtPassword.Size = new Size(355, 35);

            btnToggle = new Button
            {
                Text = "👁",
                Location = new Point(415, startY + 105),
                Size = new Size(40, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 12)
            };
            btnToggle.FlatAppearance.BorderSize = 0;
            btnToggle.Click += (s, e) => {
                if (txtPassword.Text != (string)txtPassword.Tag) {
                    txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
                    btnToggle.Text = txtPassword.UseSystemPasswordChar ? "👁" : "Ø";
                }
            };

            btnLogin = UIStyle.CreateModernButton("Sign In", UIStyle.Blue, Color.White);
            btnLogin.Location = new Point(55, startY + 170);
            btnLogin.Size = new Size(400, 50);
            btnLogin.Click += BtnLogin_Click;

            btnRegister = UIStyle.CreateModernButton("Create New Account", UIStyle.Blue, Color.White, true);
            btnRegister.Location = new Point(55, startY + 235);
            btnRegister.Size = new Size(400, 50);

            lblStatus = new Label
            {
                Location = new Point(55, startY + 295),
                Size = new Size(400, 30),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlRight.Controls.AddRange(new Control[] { lblWelcome, lblPrompt, lblEmail, txtEmail, lblPass, txtPassword, btnToggle, btnLogin, btnRegister, lblStatus });

            pnlFull.Controls.Add(pnlRight);
            pnlFull.Controls.Add(pnlLeft);
            this.Controls.Add(pnlFull);
        }

        private void BtnLogin_Click(object _, EventArgs __)
        {
            // Audit Metric: Handle empty/placeholder inputs
            string email = txtEmail.Text == (string)txtEmail.Tag ? "" : txtEmail.Text.Trim();
            string pass = txtPassword.Text == (string)txtPassword.Tag ? "" : txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                lblStatus.Text = "Required fields are missing.";
                return;
            }

            // Database Authentication
            User user = UserDAL.Login(email, pass);
            if (user != null)
            {
                Form dashboard;
                if (user.Role == "Admin") dashboard = new AdminDashboard(user);
                else if (user.Role == "SocietyHead") dashboard = new SocietyDashboard(user);
                else dashboard = new StudentDashboard(user);

                this.Hide();
                dashboard.ShowDialog();
                this.Show();
            }
            else
            {
                lblStatus.Text = "Invalid email or password.";
            }
        }
    }
}
