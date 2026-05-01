// File: Forms/AdminDashboard.cs
using System;
using System.Data;
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

        private Label lblWelcome;

        public AdminDashboard(User user)
        {
            _user = user;
            InitializeComponents();
            LoadAllData();
        }

        private void InitializeComponents()
        {
            this.Text = $"Admin Dashboard – {_user.FullName}";
            this.Size = new System.Drawing.Size(950, 640);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblWelcome = new Label
            {
                Text = $"Administrator: {_user.FullName}",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(400, 28)
            };

            tabControl = new TabControl { Location = new System.Drawing.Point(10, 45),
                                          Size = new System.Drawing.Size(920, 560) };

            // ---- Users ----
            tabUsers    = new TabPage("Users");
            dgvUsers    = MakeGrid(); dgvUsers.Location = new System.Drawing.Point(5, 40); dgvUsers.Size = new System.Drawing.Size(895, 475);
            btnActivate   = new Button { Text = "Activate",     Location = new System.Drawing.Point(5,   5), Size = new System.Drawing.Size(90,  28) };
            btnDeactivate = new Button { Text = "Deactivate",   Location = new System.Drawing.Point(105, 5), Size = new System.Drawing.Size(90,  28) };
            btnChangeRole = new Button { Text = "Change Role",  Location = new System.Drawing.Point(205, 5), Size = new System.Drawing.Size(100, 28) };
            btnActivate.Click   += BtnActivate_Click;
            btnDeactivate.Click += BtnDeactivate_Click;
            btnChangeRole.Click += BtnChangeRole_Click;
            tabUsers.Controls.AddRange(new Control[] { btnActivate, btnDeactivate, btnChangeRole, dgvUsers });

            // ---- Societies ----
            tabSocieties     = new TabPage("Societies");
            dgvSocieties     = MakeGrid(); dgvSocieties.Location = new System.Drawing.Point(5, 40); dgvSocieties.Size = new System.Drawing.Size(895, 475);
            btnCreateSociety  = new Button { Text = "Create Society",  Location = new System.Drawing.Point(5,   5), Size = new System.Drawing.Size(130, 28) };
            btnApproveSociety = new Button { Text = "Approve",         Location = new System.Drawing.Point(145, 5), Size = new System.Drawing.Size(90,  28) };
            btnSuspendSociety = new Button { Text = "Suspend",         Location = new System.Drawing.Point(245, 5), Size = new System.Drawing.Size(90,  28) };
            btnCreateSociety.Click  += BtnCreateSociety_Click;
            btnApproveSociety.Click += BtnApproveSociety_Click;
            btnSuspendSociety.Click += BtnSuspendSociety_Click;
            tabSocieties.Controls.AddRange(new Control[] { btnCreateSociety, btnApproveSociety, btnSuspendSociety, dgvSocieties });

            // ---- Events ----
            tabEvents        = new TabPage("Pending Events");
            dgvPendingEvents = MakeGrid(); dgvPendingEvents.Location = new System.Drawing.Point(5, 40); dgvPendingEvents.Size = new System.Drawing.Size(895, 475);
            btnApproveEvent  = new Button { Text = "Approve Event", Location = new System.Drawing.Point(5,   5), Size = new System.Drawing.Size(120, 28) };
            btnRejectEvent   = new Button { Text = "Reject Event",  Location = new System.Drawing.Point(135, 5), Size = new System.Drawing.Size(120, 28) };
            btnApproveEvent.Click += BtnApproveEvent_Click;
            btnRejectEvent.Click  += BtnRejectEvent_Click;
            tabEvents.Controls.AddRange(new Control[] { btnApproveEvent, btnRejectEvent, dgvPendingEvents });

            // ---- Reports ----
            tabReports  = new TabPage("University Report");
            dgvReport   = MakeGrid(); dgvReport.Location = new System.Drawing.Point(5, 40); dgvReport.Size = new System.Drawing.Size(895, 475);
            btnGenReport = new Button { Text = "Generate University Report", Location = new System.Drawing.Point(5, 5), Size = new System.Drawing.Size(200, 28) };
            btnGenReport.Click += (s, e) => { dgvReport.DataSource = TaskDAL.GenerateUniversityReport(); };
            tabReports.Controls.AddRange(new Control[] { btnGenReport, dgvReport });

            tabControl.TabPages.AddRange(new TabPage[] { tabUsers, tabSocieties, tabEvents, tabReports });
            this.Controls.AddRange(new Control[] { lblWelcome, tabControl });
        }

        private DataGridView MakeGrid()
        {
            return new DataGridView
            {
                ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                BackgroundColor = System.Drawing.Color.White
            };
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
