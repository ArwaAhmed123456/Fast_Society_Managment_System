// File: Forms/StudentDashboard.cs
using System;
using System.Data;
using System.Windows.Forms;
using SocietiesMS.DAL;
using SocietiesMS.Models;

namespace SocietiesMS.Forms
{
    /// <summary>
    /// Student dashboard – browse societies, events, tickets, memberships.
    /// </summary>
    public class StudentDashboard : Form
    {
        private readonly User _user;

        private TabControl  tabControl;
        private TabPage     tabSocieties, tabEvents, tabMemberships, tabTickets;

        // Societies tab
        private DataGridView dgvSocieties;
        private Button       btnApply, btnRefreshSocieties;

        // Events tab
        private DataGridView dgvEvents;
        private Button       btnRegisterEvent, btnRefreshEvents;

        // Memberships tab
        private DataGridView dgvMemberships;
        private Button       btnRefreshMemberships;

        // Tickets tab
        private DataGridView dgvTickets;
        private Button       btnRefreshTickets;

        private Label lblWelcome;

        public StudentDashboard(User user)
        {
            _user = user;
            InitializeComponents();
            LoadAllData();
        }

        private void InitializeComponents()
        {
            this.Text = $"Student Dashboard – {_user.FullName}";
            this.Size = new System.Drawing.Size(850, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblWelcome = new Label
            {
                Text = $"Welcome, {_user.FullName}  |  Role: {_user.Role}",
                Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(600, 28)
            };

            tabControl = new TabControl { Location = new System.Drawing.Point(10, 45),
                                          Size = new System.Drawing.Size(815, 510) };

            // ---- Societies Tab ----
            tabSocieties  = new TabPage("Browse Societies");
            dgvSocieties  = MakeGrid();
            btnApply            = new Button { Text = "Apply for Membership", Location = new System.Drawing.Point(5, 5),   Size = new System.Drawing.Size(160, 28) };
            btnRefreshSocieties = new Button { Text = "Refresh",              Location = new System.Drawing.Point(175, 5), Size = new System.Drawing.Size(80, 28) };
            dgvSocieties.Location = new System.Drawing.Point(5, 40);
            dgvSocieties.Size     = new System.Drawing.Size(790, 420);
            btnApply.Click            += BtnApply_Click;
            btnRefreshSocieties.Click += (s, e) => LoadSocieties();
            tabSocieties.Controls.AddRange(new Control[] { btnApply, btnRefreshSocieties, dgvSocieties });

            // ---- Events Tab ----
            tabEvents     = new TabPage("Upcoming Events");
            dgvEvents     = MakeGrid();
            btnRegisterEvent  = new Button { Text = "Register for Event", Location = new System.Drawing.Point(5,   5), Size = new System.Drawing.Size(140, 28) };
            btnRefreshEvents  = new Button { Text = "Refresh",            Location = new System.Drawing.Point(155, 5), Size = new System.Drawing.Size(80,  28) };
            dgvEvents.Location    = new System.Drawing.Point(5, 40);
            dgvEvents.Size        = new System.Drawing.Size(790, 420);
            btnRegisterEvent.Click += BtnRegisterEvent_Click;
            btnRefreshEvents.Click += (s, e) => LoadEvents();
            tabEvents.Controls.AddRange(new Control[] { btnRegisterEvent, btnRefreshEvents, dgvEvents });

            // ---- Memberships Tab ----
            tabMemberships    = new TabPage("My Memberships");
            dgvMemberships    = MakeGrid();
            btnRefreshMemberships = new Button { Text = "Refresh", Location = new System.Drawing.Point(5, 5), Size = new System.Drawing.Size(80, 28) };
            dgvMemberships.Location = new System.Drawing.Point(5, 40);
            dgvMemberships.Size     = new System.Drawing.Size(790, 420);
            btnRefreshMemberships.Click += (s, e) => LoadMemberships();
            tabMemberships.Controls.AddRange(new Control[] { btnRefreshMemberships, dgvMemberships });

            // ---- Tickets Tab ----
            tabTickets    = new TabPage("My Tickets");
            dgvTickets    = MakeGrid();
            btnRefreshTickets = new Button { Text = "Refresh", Location = new System.Drawing.Point(5, 5), Size = new System.Drawing.Size(80, 28) };
            dgvTickets.Location = new System.Drawing.Point(5, 40);
            dgvTickets.Size     = new System.Drawing.Size(790, 420);
            btnRefreshTickets.Click += (s, e) => LoadTickets();
            tabTickets.Controls.AddRange(new Control[] { btnRefreshTickets, dgvTickets });

            tabControl.TabPages.AddRange(new TabPage[]
                { tabSocieties, tabEvents, tabMemberships, tabTickets });

            this.Controls.AddRange(new Control[] { lblWelcome, tabControl });
        }

        // ----------------------------------------------------------------
        // Helper – create a standard read-only DataGridView
        // CC = 1
        // ----------------------------------------------------------------
        private DataGridView MakeGrid()
        {
            return new DataGridView
            {
                ReadOnly         = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode    = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows    = false,
                AllowUserToDeleteRows = false,
                BackgroundColor  = System.Drawing.Color.White
            };
        }

        // ----------------------------------------------------------------
        // Load helpers
        // ----------------------------------------------------------------
        private void LoadAllData()        { LoadSocieties(); LoadEvents(); LoadMemberships(); LoadTickets(); }
        private void LoadSocieties()      { dgvSocieties.DataSource   = SocietyDAL.GetAllActiveSocieties(); }
        private void LoadEvents()         { dgvEvents.DataSource       = EventDAL.GetUpcomingEvents();       }
        private void LoadMemberships()    { dgvMemberships.DataSource  = SocietyDAL.GetStudentMemberships(_user.UserID); }
        private void LoadTickets()        { dgvTickets.DataSource      = EventDAL.GetStudentTickets(_user.UserID);       }

        // ----------------------------------------------------------------
        // BtnApply_Click – apply for selected society
        // CC = 3 (no selection + result branches)
        // ----------------------------------------------------------------
        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (dgvSocieties.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a society first.");
                return;
            }
            int societyID = Convert.ToInt32(dgvSocieties.SelectedRows[0].Cells["SocietyID"].Value);
            bool ok = SocietyDAL.ApplyForMembership(_user.UserID, societyID);
            MessageBox.Show(ok ? "Application submitted!" : "You have already applied for this society.");
            LoadMemberships();
        }

        // ----------------------------------------------------------------
        // BtnRegisterEvent_Click – register for selected event
        // CC = 2 (no selection check)
        // ----------------------------------------------------------------
        private void BtnRegisterEvent_Click(object sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an event first.");
                return;
            }
            int eventID = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["EventID"].Value);
            string msg  = EventDAL.RegisterForEvent(_user.UserID, eventID);
            MessageBox.Show(msg);
            LoadTickets();
        }
    }
}
