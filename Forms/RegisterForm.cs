// File: Forms/RegisterForm.cs
using System;
using System.Windows.Forms;
using SocietiesMS.DAL;

namespace SocietiesMS.Forms
{
    /// <summary>
    /// New student registration form.
    /// </summary>
    public class RegisterForm : Form
    {
        private Label   lblTitle, lblName, lblEmail, lblPass, lblConfirm, lblStatus;
        private TextBox txtName, txtEmail, txtPass, txtConfirm;
        private Button  btnRegister, btnCancel;

        public RegisterForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text  = "Register New Account";
            this.Size  = new System.Drawing.Size(420, 360);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            lblTitle   = new Label  { Text = "Student Registration", Font = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold),
                                      Location = new System.Drawing.Point(100, 15), Size = new System.Drawing.Size(220, 30) };

            lblName    = new Label  { Text = "Full Name:", Location = new System.Drawing.Point(40,  65), Size = new System.Drawing.Size(90, 25) };
            txtName    = new TextBox { Location = new System.Drawing.Point(140, 63), Size = new System.Drawing.Size(230, 25) };

            lblEmail   = new Label  { Text = "Email:",     Location = new System.Drawing.Point(40, 105), Size = new System.Drawing.Size(90, 25) };
            txtEmail   = new TextBox { Location = new System.Drawing.Point(140, 103), Size = new System.Drawing.Size(230, 25) };

            lblPass    = new Label  { Text = "Password:",  Location = new System.Drawing.Point(40, 145), Size = new System.Drawing.Size(90, 25) };
            txtPass    = new TextBox { Location = new System.Drawing.Point(140, 143), Size = new System.Drawing.Size(230, 25), UseSystemPasswordChar = true };

            lblConfirm = new Label  { Text = "Confirm:",   Location = new System.Drawing.Point(40, 185), Size = new System.Drawing.Size(90, 25) };
            txtConfirm = new TextBox { Location = new System.Drawing.Point(140, 183), Size = new System.Drawing.Size(230, 25), UseSystemPasswordChar = true };

            btnRegister = new Button { Text = "Register", Location = new System.Drawing.Point(140, 225), Size = new System.Drawing.Size(100, 30) };
            btnCancel   = new Button { Text = "Cancel",   Location = new System.Drawing.Point(260, 225), Size = new System.Drawing.Size(100, 30) };

            lblStatus = new Label { Location = new System.Drawing.Point(40, 265), Size = new System.Drawing.Size(340, 50),
                                    ForeColor = System.Drawing.Color.Red };

            btnRegister.Click += BtnRegister_Click;
            btnCancel.Click   += (s, e) => this.Close();

            this.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblTitle, lblName, txtName, lblEmail, txtEmail, lblPass, txtPass,
                  lblConfirm, txtConfirm, btnRegister, btnCancel, lblStatus });
        }

        // ----------------------------------------------------------------
        // BtnRegister_Click – validate and create account
        // CC = 5 (4 validation branches + success/fail)
        // ----------------------------------------------------------------
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            if (string.IsNullOrWhiteSpace(txtName.Text)  ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text))
            {
                lblStatus.Text = "All fields are required.";
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                lblStatus.Text = "Enter a valid email address.";
                return;
            }

            if (txtPass.Text.Length < 6)
            {
                lblStatus.Text = "Password must be at least 6 characters.";
                return;
            }

            if (txtPass.Text != txtConfirm.Text)
            {
                lblStatus.Text = "Passwords do not match.";
                return;
            }

            bool success = UserDAL.RegisterUser(txtName.Text.Trim(),
                                                txtEmail.Text.Trim(),
                                                txtPass.Text);
            if (success)
            {
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "Account created! You can now log in.";
            }
            else
            {
                lblStatus.Text = "Email already registered.";
            }
        }
    }
}
