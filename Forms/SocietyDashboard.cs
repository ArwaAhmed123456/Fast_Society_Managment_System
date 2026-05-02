// File: Forms/SocietyDashboard.cs
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
    /// Society Head dashboard – manage members, events, tasks, reports.
    /// </summary>
    public class SocietyDashboard : Form
    {
        private readonly User _user;
        private readonly int _societyID;

        private Panel pnlHeader;
        private PictureBox picLogo;
        private Label lblHeaderTitle, lblWelcome;
        private TabControl tabControl;
        private TabPage    tabMembers, tabEvents, tabTasks, tabReports;

        // Members
        private DataGridView dgvRequests, dgvMembers;
        private Button       btnApprove, btnReject;

        // Events
        private DataGridView dgvEvents;
        private Button       btnCreateEvent, btnCancelEvent;

        // Tasks
        private DataGridView dgvTasks;
        private Button       btnAssignTask, btnMarkDone;

        // Reports
        private DataGridView dgvReport;
        private Button       btnGenReport;

        public SocietyDashboard(User user)
        {
            _user = user;
            _societyID = SocietyDAL.GetSocietyIDByHead(_user.UserID);
            InitializeComponents();
            
            if (_societyID == -1)
            {
                MessageBox.Show("You are not assigned to any society!");
                this.Load += (s, e) => this.Close();
            }
            else
            {
                LoadAllData();
            }
        }

        private void InitializeComponents()
        {
            this.Text = $"Society Head Dashboard – {_user.FullName}";
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
            lblWelcome = new Label { Text = $"Welcome, {_user.FullName} (Society Head)", Font = UIStyle.SmallFont, ForeColor = UIStyle.LightBlue, Location = new Point(85, 47), Size = new Size(500, 25), TextAlign = ContentAlignment.MiddleLeft };
            
            pnlHeader.Controls.AddRange(new Control[] { picLogo, lblHeaderTitle, lblWelcome });

            tabControl = new TabControl { 
                Dock = DockStyle.Fill, 
                Font = UIStyle.TextFont,
                Padding = new Point(20, 10)
            };

            // ---- Members Tab ----
            tabMembers  = new TabPage("Members") { BackColor = UIStyle.White };
            dgvRequests = MakeGrid(); dgvRequests.Location = new Point(20, 60); dgvRequests.Size = new Size(940, 200);
            dgvMembers  = MakeGrid(); dgvMembers.Location  = new Point(20, 300); dgvMembers.Size  = new Size(940, 240);
            btnApprove  = CreateStyledButton("Approve", new Point(20, 15));
            btnReject   = CreateStyledButton("Reject", new Point(130, 15));
            Label lblReq = new Label { Text = "Pending Membership Requests:", Location = new Point(20, 40), Font = UIStyle.HeaderFont, Size = new Size(300, 20) };
            Label lblMem = new Label { Text = "Active Society Members:", Location = new Point(20, 275), Font = UIStyle.HeaderFont, Size = new Size(300, 20) };
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click  += BtnReject_Click;
            tabMembers.Controls.AddRange(new Control[] { btnApprove, btnReject, lblReq, dgvRequests, lblMem, dgvMembers });

            // ---- Events Tab ----
            tabEvents       = new TabPage("Events") { BackColor = UIStyle.White };
            dgvEvents       = MakeGrid(); dgvEvents.Location = new Point(20, 60); dgvEvents.Size = new Size(940, 480);
            btnCreateEvent  = CreateStyledButton("Create Event", new Point(20, 15), 130);
            btnCancelEvent  = CreateStyledButton("Cancel Event", new Point(160, 15), 130);
            btnCreateEvent.Click += BtnCreateEvent_Click;
            btnCancelEvent.Click += BtnCancelEvent_Click;
            tabEvents.Controls.AddRange(new Control[] { btnCreateEvent, btnCancelEvent, dgvEvents });

            // ---- Tasks Tab ----
            tabTasks    = new TabPage("Tasks") { BackColor = UIStyle.White };
            dgvTasks    = MakeGrid(); dgvTasks.Location = new Point(20, 60); dgvTasks.Size = new Size(940, 480);
            btnAssignTask = CreateStyledButton("Assign Task", new Point(20, 15), 130);
            btnMarkDone   = CreateStyledButton("Mark Complete", new Point(160, 15), 150);
            btnAssignTask.Click += BtnAssignTask_Click;
            btnMarkDone.Click   += BtnMarkDone_Click;
            tabTasks.Controls.AddRange(new Control[] { btnAssignTask, btnMarkDone, dgvTasks });

            // ---- Reports Tab ----
            tabReports  = new TabPage("Reports") { BackColor = UIStyle.White };
            dgvReport   = MakeGrid(); dgvReport.Location = new Point(20, 60); dgvReport.Size = new Size(940, 480);
            btnGenReport = CreateStyledButton("Generate Report", new Point(20, 15), 150);
            btnGenReport.Click += (s, e) => { dgvReport.DataSource = TaskDAL.GenerateSocietyReport(_societyID); };
            tabReports.Controls.AddRange(new Control[] { btnGenReport, dgvReport });

            tabControl.TabPages.AddRange(new TabPage[] { tabMembers, tabEvents, tabTasks, tabReports });
            
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
            dgvRequests.DataSource = SocietyDAL.GetMembershipRequestsForSociety(_societyID);
            dgvMembers.DataSource  = SocietyDAL.GetMembersBySociety(_societyID);
            dgvEvents.DataSource   = EventDAL.GetEventsBySociety(_societyID);
            dgvTasks.DataSource    = TaskDAL.GetTasksForSociety(_societyID);
        }

        // ----------------------------------------------------------------
        // BtnApprove_Click / BtnReject_Click
        // CC = 2 each
        // ----------------------------------------------------------------
        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0) { MessageBox.Show("Select a request."); return; }
            int mid = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells["MembershipID"].Value);
            SocietyDAL.RespondToMembership(mid, "Approved");
            LoadAllData();
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0) { MessageBox.Show("Select a request."); return; }
            int mid = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells["MembershipID"].Value);
            SocietyDAL.RespondToMembership(mid, "Rejected");
            LoadAllData();
        }

        // ----------------------------------------------------------------
        // BtnCreateEvent_Click – show event creation dialog
        // CC = 2
        // ----------------------------------------------------------------
        private void BtnCreateEvent_Click(object sender, EventArgs e)
        {
            if (_societyID < 0) { MessageBox.Show("Society not loaded."); return; }
            new CreateEventForm(_societyID).ShowDialog();
            dgvEvents.DataSource = EventDAL.GetEventsBySociety(_societyID);
        }

        // ----------------------------------------------------------------
        // BtnCancelEvent_Click
        // CC = 2
        // ----------------------------------------------------------------
        private void BtnCancelEvent_Click(object sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0) { MessageBox.Show("Select an event."); return; }
            int eid = Convert.ToInt32(dgvEvents.SelectedRows[0].Cells["EventID"].Value);
            EventDAL.UpdateEventStatus(eid, "Cancelled");
            dgvEvents.DataSource = EventDAL.GetEventsBySociety(_societyID);
        }

        // ----------------------------------------------------------------
        // BtnAssignTask_Click
        // CC = 2
        // ----------------------------------------------------------------
        private void BtnAssignTask_Click(object sender, EventArgs e)
        {
            if (_societyID < 0) { MessageBox.Show("Society not loaded."); return; }
            new AssignTaskForm(_societyID, _user.UserID).ShowDialog();
            dgvTasks.DataSource = TaskDAL.GetTasksForSociety(_societyID);
        }

        // ----------------------------------------------------------------
        // BtnMarkDone_Click
        // CC = 2
        // ----------------------------------------------------------------
        private void BtnMarkDone_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) { MessageBox.Show("Select a task."); return; }
            int tid = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["TaskID"].Value);
            TaskDAL.UpdateTaskStatus(tid, "Completed");
            dgvTasks.DataSource = TaskDAL.GetTasksForSociety(_societyID);
        }
    }
}
