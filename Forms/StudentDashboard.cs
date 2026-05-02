// File: Forms/StudentDashboard.cs
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
    /// Student dashboard – browse societies, events, tickets, memberships.
    /// </summary>
    public class StudentDashboard : Form
    {
        private readonly User _user;

        private Panel pnlHeader;
        private PictureBox picLogo;
        private Label lblHeaderTitle, lblWelcome;
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

        public StudentDashboard(User user)
        {
            _user = user;
            InitializeComponents();
            LoadAllData();
        }

        private void InitializeComponents()
        {
            this.Text = $"Student Dashboard – {_user.FullName}";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UIStyle.LightGray;

            // Header
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = UIStyle.Navy };
            
            picLogo = new PictureBox { Size = new Size(60, 60), Location = new Point(15, 10), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Transparent };
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fast_Logo.png");
            if (!File.Exists(logoPath)) logoPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "fast_Logo.png");
            if (File.Exists(logoPath)) picLogo.Image = Image.FromFile(logoPath);

            lblHeaderTitle = new Label { Text = "FAST UNIVERSITY", Font = UIStyle.TitleFont, ForeColor = UIStyle.White, Location = new Point(85, 12), Size = new Size(300, 35), TextAlign = ContentAlignment.MiddleLeft };
            lblWelcome = new Label { Text = $"Welcome, {_user.FullName} (Student)", Font = UIStyle.SmallFont, ForeColor = UIStyle.LightBlue, Location = new Point(85, 47), Size = new Size(500, 25), TextAlign = ContentAlignment.MiddleLeft };
            
            pnlHeader.Controls.AddRange(new Control[] { picLogo, lblHeaderTitle, lblWelcome });

            tabControl = new TabControl { 
                Dock = DockStyle.Fill, 
                Font = UIStyle.TextFont,
                Padding = new Point(20, 10)
            };

            // ---- Societies Tab ----
            tabSocieties  = new TabPage("Browse Societies") { BackColor = UIStyle.White };
            dgvSocieties  = MakeGrid();
            btnApply            = CreateStyledButton("Apply for Membership", new Point(20, 15), 180);
            btnRefreshSocieties = CreateStyledButton("Refresh", new Point(210, 15), 80);
            dgvSocieties.Location = new Point(20, 60);
            dgvSocieties.Size     = new Size(940, 480);
            btnApply.Click            += BtnApply_Click;
            btnRefreshSocieties.Click += (s, e) => LoadSocieties();
            tabSocieties.Controls.AddRange(new Control[] { btnApply, btnRefreshSocieties, dgvSocieties });

            // ---- Events Tab ----
            tabEvents     = new TabPage("Upcoming Events") { BackColor = UIStyle.White };
            dgvEvents     = MakeGrid();
            btnRegisterEvent  = CreateStyledButton("Register for Event", new Point(20, 15), 160);
            btnRefreshEvents  = CreateStyledButton("Refresh", new Point(190, 15), 80);
            dgvEvents.Location    = new Point(20, 60);
            dgvEvents.Size        = new Size(940, 480);
            btnRegisterEvent.Click += BtnRegisterEvent_Click;
            btnRefreshEvents.Click += (s, e) => LoadEvents();
            tabEvents.Controls.AddRange(new Control[] { btnRegisterEvent, btnRefreshEvents, dgvEvents });

            // ---- Memberships Tab ----
            tabMemberships    = new TabPage("My Memberships") { BackColor = UIStyle.White };
            dgvMemberships    = MakeGrid();
            btnRefreshMemberships = CreateStyledButton("Refresh", new Point(20, 15), 80);
            dgvMemberships.Location = new Point(20, 60);
            dgvMemberships.Size     = new Size(940, 480);
            btnRefreshMemberships.Click += (s, e) => LoadMemberships();
            tabMemberships.Controls.AddRange(new Control[] { btnRefreshMemberships, dgvMemberships });

            // ---- Tickets Tab ----
            tabTickets    = new TabPage("My Tickets") { BackColor = UIStyle.White };
            dgvTickets    = MakeGrid();
            btnRefreshTickets = CreateStyledButton("Refresh", new Point(20, 15), 80);
            dgvTickets.Location = new Point(20, 60);
            dgvTickets.Size     = new Size(940, 480);
            btnRefreshTickets.Click += (s, e) => LoadTickets();
            tabTickets.Controls.AddRange(new Control[] { btnRefreshTickets, dgvTickets });

            tabControl.TabPages.AddRange(new TabPage[] { tabSocieties, tabEvents, tabMemberships, tabTickets });

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
