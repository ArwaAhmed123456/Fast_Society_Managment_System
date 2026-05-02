// File: Forms/AdminDashboard.cs
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SocietiesMS.DAL;
using SocietiesMS.Models;

namespace SocietiesMS.Forms
{
    /// <summary>
    /// Admin dashboard – full control over users, societies, events, reports.
    /// </summary>
    public class AdminDashboard : Form
    {
        private readonly User _user;

        private Panel pnlHeader, pnlSidebar, pnlContent;
        private PictureBox picLogo;
        private Label lblHeaderTitle, lblWelcome;
        private TabControl tabControl;
        private TabPage    tabUsers, tabSocieties, tabEvents, tabReports;

        // Users
        private DataGridView dgvUsers;
        private Button       btnActivate, btnDeactivate, btnChangeRole;

        // Societies
        private DataGridView dgvSocieties;
        private Button       btnCreateSociety, btnApproveSociety, btnSuspendSociety;

        // Events
        private DataGridView dgvPendingEvents;
        private Button       btnApproveEvent, btnRejectEvent;

        // Reports
        private DataGridView dgvReport;
        private Button       btnGenReport;

        public AdminDashboard(User user)
        {
            _user = user;
            InitializeComponents();
            LoadAllData();
        }

        private void InitializeComponents()
        {
            this.Text = $"Admin Dashboard – {_user.FullName}";
            this.Size = new Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UIStyle.LightGray;

            // Header
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = UIStyle.Navy };
            
            picLogo = new PictureBox { Size = new Size(60, 60), Location = new Point(15, 10), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Transparent };
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fast_Logo.png");
            if (!File.Exists(logoPath)) logoPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "fast_Logo.png");
            if (File.Exists(logoPath)) picLogo.Image = Image.FromFile(logoPath);

            lblHeaderTitle = new Label { Text = "FAST UNIVERSITY", Font = UIStyle.TitleFont, ForeColor = UIStyle.White, Location = new Point(85, 12), Size = new Size(300, 35), TextAlign = ContentAlignment.MiddleLeft };
            lblWelcome = new Label { Text = $"Welcome, {_user.FullName} (Administrator)", Font = UIStyle.SmallFont, ForeColor = UIStyle.LightBlue, Location = new Point(85, 47), Size = new Size(500, 25), TextAlign = ContentAlignment.MiddleLeft };
            
            pnlHeader.Controls.AddRange(new Control[] { picLogo, lblHeaderTitle, lblWelcome });

            // Sidebar (Optional, but let's use TabControl for simplicity with better styling)
            tabControl = new TabControl { 
                Dock = DockStyle.Fill, 
                Font = UIStyle.TextFont,
                Padding = new Point(20, 10)
            };

            // Apply style to TabControl (custom painting would be better but simple first)

            // ---- Users ----
            tabUsers = new TabPage("Manage Users") { BackColor = UIStyle.White };
            dgvUsers = MakeGrid(); dgvUsers.Location = new Point(20, 60); dgvUsers.Size = new Size(1020, 500);
            
            btnActivate = CreateStyledButton("Activate", new Point(20, 15));
            btnDeactivate = CreateStyledButton("Deactivate", new Point(130, 15));
            btnChangeRole = CreateStyledButton("Change Role", new Point(240, 15));
            
            btnActivate.Click += BtnActivate_Click;
            btnDeactivate.Click += BtnDeactivate_Click;
            btnChangeRole.Click += BtnChangeRole_Click;
            tabUsers.Controls.AddRange(new Control[] { btnActivate, btnDeactivate, btnChangeRole, dgvUsers });

            // ---- Societies ----
            tabSocieties = new TabPage("Societies") { BackColor = UIStyle.White };
            dgvSocieties = MakeGrid(); dgvSocieties.Location = new Point(20, 60); dgvSocieties.Size = new Size(1020, 500);
            
            btnCreateSociety = CreateStyledButton("Create Society", new Point(20, 15), 140);
            btnApproveSociety = CreateStyledButton("Approve", new Point(170, 15));
            btnSuspendSociety = CreateStyledButton("Suspend", new Point(280, 15));
            
            btnCreateSociety.Click += BtnCreateSociety_Click;
            btnApproveSociety.Click += BtnApproveSociety_Click;
            btnSuspendSociety.Click += BtnSuspendSociety_Click;
            tabSocieties.Controls.AddRange(new Control[] { btnCreateSociety, btnApproveSociety, btnSuspendSociety, dgvSocieties });

            // ---- Events ----
            tabEvents = new TabPage("Pending Events") { BackColor = UIStyle.White };
            dgvPendingEvents = MakeGrid(); dgvPendingEvents.Location = new Point(20, 60); dgvPendingEvents.Size = new Size(1020, 500);
            
            btnApproveEvent = CreateStyledButton("Approve Event", new Point(20, 15), 130);
            btnRejectEvent = CreateStyledButton("Reject Event", new Point(160, 15), 130);
            
            btnApproveEvent.Click += BtnApproveEvent_Click;
            btnRejectEvent.Click += BtnRejectEvent_Click;
            tabEvents.Controls.AddRange(new Control[] { btnApproveEvent, btnRejectEvent, dgvPendingEvents });

            // ---- Reports ----
            tabReports = new TabPage("Reports") { BackColor = UIStyle.White };
            dgvReport = MakeGrid(); dgvReport.Location = new Point(20, 60); dgvReport.Size = new Size(1020, 500);
            
            btnGenReport = CreateStyledButton("Generate University Report", new Point(20, 15), 250);
            btnGenReport.Click += (s, e) => { dgvReport.DataSource = TaskDAL.GenerateUniversityReport(); };
            tabReports.Controls.AddRange(new Control[] { btnGenReport, dgvReport });

            tabControl.TabPages.AddRange(new TabPage[] { tabUsers, tabSocieties, tabEvents, tabReports });

            this.Controls.Add(tabControl);
            this.Controls.Add(pnlHeader);
        }

        private Button CreateStyledButton(string text, Point location, int width = 100)
        {
            var btn = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(width, 35),
                BackColor = UIStyle.Blue,
                ForeColor = UIStyle.White,
                FlatStyle = FlatStyle.Flat,
                Font = UIStyle.SmallFont,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private DataGridView MakeGrid()
        {
            var grid = new DataGridView
            {
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = UIStyle.White,
                BorderStyle = BorderStyle.Fixed3D,
                GridColor = UIStyle.Border,
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 35,
                RowTemplate = { Height = 30 }
            };
            
            // Fix header alignment
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.BackColor = UIStyle.Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = UIStyle.White;
            grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            
            // Fix cell alignment
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            
            return grid;
        }

        private void LoadAllData()
        {
            dgvUsers.DataSource         = UserDAL.GetAllUsers();
            dgvSocieties.DataSource     = SocietyDAL.GetAllSocieties();
            dgvPendingEvents.DataSource = EventDAL.GetAllPendingEvents();
        }

        // ----------------------------------------------------------------
        // User management
        // CC = 2 each
        // ----------------------------------------------------------------
        private void BtnActivate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) { MessageBox.Show("Select a user."); return; }
            int uid = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
            UserDAL.SetUserActiveStatus(uid, true);
            dgvUsers.DataSource = UserDAL.GetAllUsers();
        }

        private void BtnDeactivate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) { MessageBox.Show("Select a user."); return; }
            int uid = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
            UserDAL.SetUserActiveStatus(uid, false);
            dgvUsers.DataSource = UserDAL.GetAllUsers();
        }

        // ----------------------------------------------------------------
        // BtnChangeRole_Click – prompt for new role
        // CC = 3
        // ----------------------------------------------------------------
        private void BtnChangeRole_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) { MessageBox.Show("Select a user."); return; }
            int uid = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
            string role = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter role (Student/SocietyHead/Member/Admin):", "Change Role", "Student");
            if (string.IsNullOrWhiteSpace(role)) return;
            bool ok = UserDAL.UpdateUserRole(uid, role);
            MessageBox.Show(ok ? "Role updated." : "Invalid role entered.");
            dgvUsers.DataSource = UserDAL.GetAllUsers();
        }

        // ----------------------------------------------------------------
        // Society management
        // CC = 2 each
        // ----------------------------------------------------------------
        private void BtnCreateSociety_Click(object sender, EventArgs e)
        {
            new CreateSocietyForm().ShowDialog();
            dgvSocieties.DataSource = SocietyDAL.GetAllSocieties();
        }

        private void BtnApproveSociety_Click(object sender, EventArgs e)
        {
            if (dgvSocieties.SelectedRows.Count == 0) { MessageBox.Show("Select a society."); return; }
            int sid = Convert.ToInt32(dgvSocieties.SelectedRows[0].Cells["SocietyID"].Value);
            SocietyDAL.UpdateSocietyStatus(sid, "Active");
            dgvSocieties.DataSource = SocietyDAL.GetAllSocieties();
        }

        private void BtnSuspendSociety_Click(object sender, EventArgs e)
        {
            if (dgvSocieties.SelectedRows.Count == 0) { MessageBox.Show("Select a society."); return; }
            int sid = Convert.ToInt32(dgvSocieties.SelectedRows[0].Cells["SocietyID"].Value);
            SocietyDAL.UpdateSocietyStatus(sid, "Suspended");
            dgvSocieties.DataSource = SocietyDAL.GetAllSocieties();
        }

        // ----------------------------------------------------------------
        // Event approval
        // CC = 2 each
        // ----------------------------------------------------------------
        private void BtnApproveEvent_Click(object sender, EventArgs e)
        {
            if (dgvPendingEvents.SelectedRows.Count == 0) { MessageBox.Show("Select an event."); return; }
            int eid = Convert.ToInt32(dgvPendingEvents.SelectedRows[0].Cells["EventID"].Value);
            EventDAL.UpdateEventStatus(eid, "Approved");
            dgvPendingEvents.DataSource = EventDAL.GetAllPendingEvents();
        }

        private void BtnRejectEvent_Click(object sender, EventArgs e)
        {
            if (dgvPendingEvents.SelectedRows.Count == 0) { MessageBox.Show("Select an event."); return; }
            int eid = Convert.ToInt32(dgvPendingEvents.SelectedRows[0].Cells["EventID"].Value);
            EventDAL.UpdateEventStatus(eid, "Cancelled");
            dgvPendingEvents.DataSource = EventDAL.GetAllPendingEvents();
        }
    }
}
