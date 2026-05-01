// File: Forms/SocietyDashboard.cs
using System;
using System.Data;
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
        private int _societyID = -1;

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

        private Label lblWelcome;

        public SocietyDashboard(User user)
        {
            _user = user;
            InitializeComponents();
            LoadSocietyID();
        }

        private void InitializeComponents()
        {
            this.Text = $"Society Head Dashboard – {_user.FullName}";
            this.Size = new System.Drawing.Size(900, 620);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblWelcome = new Label
            {
                Text = $"Welcome, {_user.FullName}  |  Society Head",
                Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(500, 28)
            };

            tabControl = new TabControl { Location = new System.Drawing.Point(10, 45),
                                          Size = new System.Drawing.Size(865, 540) };

            // ---- Members Tab ----
            tabMembers  = new TabPage("Membership Requests");
            dgvRequests = MakeGrid(); dgvRequests.Location = new System.Drawing.Point(5, 40); dgvRequests.Size = new System.Drawing.Size(840, 200);
            dgvMembers  = MakeGrid(); dgvMembers.Location  = new System.Drawing.Point(5, 255); dgvMembers.Size  = new System.Drawing.Size(840, 235);
            btnApprove  = new Button { Text = "Approve", Location = new System.Drawing.Point(5,   5), Size = new System.Drawing.Size(90, 28) };
            btnReject   = new Button { Text = "Reject",  Location = new System.Drawing.Point(105, 5), Size = new System.Drawing.Size(90, 28) };
            Label lblReq = new Label { Text = "Pending Requests:", Location = new System.Drawing.Point(5, 245), Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold) };
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click  += BtnReject_Click;
            tabMembers.Controls.AddRange(new Control[] { btnApprove, btnReject, dgvRequests, lblReq, dgvMembers });

            // ---- Events Tab ----
            tabEvents       = new TabPage("Events");
            dgvEvents       = MakeGrid(); dgvEvents.Location = new System.Drawing.Point(5, 40); dgvEvents.Size = new System.Drawing.Size(840, 450);
            btnCreateEvent  = new Button { Text = "Create Event",  Location = new System.Drawing.Point(5,   5), Size = new System.Drawing.Size(120, 28) };
            btnCancelEvent  = new Button { Text = "Cancel Event",  Location = new System.Drawing.Point(135, 5), Size = new System.Drawing.Size(120, 28) };
            btnCreateEvent.Click += BtnCreateEvent_Click;
            btnCancelEvent.Click += BtnCancelEvent_Click;
            tabEvents.Controls.AddRange(new Control[] { btnCreateEvent, btnCancelEvent, dgvEvents });

            // ---- Tasks Tab ----
            tabTasks    = new TabPage("Tasks");
            dgvTasks    = MakeGrid(); dgvTasks.Location = new System.Drawing.Point(5, 40); dgvTasks.Size = new System.Drawing.Size(840, 450);
            btnAssignTask = new Button { Text = "Assign Task",   Location = new System.Drawing.Point(5,   5), Size = new System.Drawing.Size(110, 28) };
            btnMarkDone   = new Button { Text = "Mark Complete", Location = new System.Drawing.Point(125, 5), Size = new System.Drawing.Size(120, 28) };
            btnAssignTask.Click += BtnAssignTask_Click;
            btnMarkDone.Click   += BtnMarkDone_Click;
            tabTasks.Controls.AddRange(new Control[] { btnAssignTask, btnMarkDone, dgvTasks });

            // ---- Reports Tab ----
            tabReports  = new TabPage("Reports");
            dgvReport   = MakeGrid(); dgvReport.Location = new System.Drawing.Point(5, 40); dgvReport.Size = new System.Drawing.Size(840, 450);
            btnGenReport = new Button { Text = "Generate Report", Location = new System.Drawing.Point(5, 5), Size = new System.Drawing.Size(140, 28) };
            btnGenReport.Click += (s, e) => { dgvReport.DataSource = TaskDAL.GenerateSocietyReport(_societyID); };
            tabReports.Controls.AddRange(new Control[] { btnGenReport, dgvReport });

            tabControl.TabPages.AddRange(new TabPage[] { tabMembers, tabEvents, tabTasks, tabReports });
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

        // ----------------------------------------------------------------
        // LoadSocietyID – find society where current user is head
        // CC = 2 (no society found check)
        // ----------------------------------------------------------------
        private void LoadSocietyID()
        {
            string sql = "SELECT SocietyID FROM Societies WHERE HeadUserID=@UID AND Status='Active'";
            var prms = new System.Data.SqlClient.SqlParameter[]
                { new System.Data.SqlClient.SqlParameter("@UID", _user.UserID) };
            object result = DatabaseHelper.ExecuteScalar(sql, prms);

            if (result == null || result == DBNull.Value)
            {
                MessageBox.Show("No active society found for your account. Contact admin.");
                return;
            }
            _societyID = Convert.ToInt32(result);
            LoadAllData();
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
