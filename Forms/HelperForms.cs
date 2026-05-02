// File: Forms/CreateEventForm.cs
using System;
using System.Windows.Forms;
using SocietiesMS.DAL;

namespace SocietiesMS.Forms
{
    /// <summary>
    /// Dialog for creating a new event (used by Society Head).
    /// </summary>
    public class CreateEventForm : Form
    {
        private readonly int _societyID;

        private Label   lblTitle, lblDesc, lblLocation, lblDate, lblCapacity, lblStatus;
        private TextBox txtTitle, txtDesc, txtLocation, txtCapacity;
        private DateTimePicker dtpDate;
        private Button  btnCreate, btnCancel;

        public CreateEventForm(int societyID)
        {
            _societyID = societyID;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Create New Event";
            this.Size = new System.Drawing.Size(420, 390);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int lx = 20, tx = 130, w = 250, lw = 100;

            lblTitle    = new Label  { Text = "Title:",    Location = new System.Drawing.Point(lx, 20),  Size = new System.Drawing.Size(lw, 25) };
            txtTitle    = new TextBox { Location = new System.Drawing.Point(tx, 18),  Size = new System.Drawing.Size(w, 25) };

            lblDesc     = new Label  { Text = "Description:", Location = new System.Drawing.Point(lx, 55),  Size = new System.Drawing.Size(lw, 25) };
            txtDesc     = new TextBox { Location = new System.Drawing.Point(tx, 53),  Size = new System.Drawing.Size(w, 25) };

            lblLocation = new Label  { Text = "Location:", Location = new System.Drawing.Point(lx, 90),  Size = new System.Drawing.Size(lw, 25) };
            txtLocation = new TextBox { Location = new System.Drawing.Point(tx, 88),  Size = new System.Drawing.Size(w, 25) };

            lblDate     = new Label  { Text = "Date:", Location = new System.Drawing.Point(lx, 125), Size = new System.Drawing.Size(lw, 25) };
            dtpDate     = new DateTimePicker { Location = new System.Drawing.Point(tx, 123), Size = new System.Drawing.Size(w, 25),
                                               MinDate = DateTime.Today.AddDays(1) };

            lblCapacity = new Label  { Text = "Capacity:", Location = new System.Drawing.Point(lx, 160), Size = new System.Drawing.Size(lw, 25) };
            txtCapacity = new TextBox { Location = new System.Drawing.Point(tx, 158), Size = new System.Drawing.Size(80, 25), Text = "100" };

            btnCreate = new Button { Text = "Create Event", Location = new System.Drawing.Point(tx, 200), Size = new System.Drawing.Size(110, 30) };
            btnCancel = new Button { Text = "Cancel",       Location = new System.Drawing.Point(tx + 120, 200), Size = new System.Drawing.Size(90, 30) };

            lblStatus = new Label { Location = new System.Drawing.Point(lx, 245), Size = new System.Drawing.Size(360, 50),
                                    ForeColor = System.Drawing.Color.Red };

            btnCreate.Click += BtnCreate_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblTitle, txtTitle, lblDesc, txtDesc, lblLocation, txtLocation,
                  lblDate, dtpDate, lblCapacity, txtCapacity, btnCreate, btnCancel, lblStatus });
        }

        // ----------------------------------------------------------------
        // BtnCreate_Click – validate and create event
        // CC = 4 (3 validation branches + success/fail)
        // ----------------------------------------------------------------
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                lblStatus.Text = "Title is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                lblStatus.Text = "Location is required.";
                return;
            }

            int capacity;
            if (!int.TryParse(txtCapacity.Text, out capacity) || capacity <= 0)
            {
                lblStatus.Text = "Enter a valid capacity (positive number).";
                return;
            }

            bool ok = EventDAL.CreateEvent(_societyID, txtTitle.Text.Trim(),
                                           txtDesc.Text.Trim(), txtLocation.Text.Trim(),
                                           dtpDate.Value, capacity);
            if (ok)
            {
                MessageBox.Show("Event created and pending admin approval.");
                this.Close();
            }
            else
            {
                lblStatus.Text = "Failed to create event. Please try again.";
            }
        }
    }
}

// ============================================================
// File: Forms/CreateSocietyForm.cs
// ============================================================
namespace SocietiesMS.Forms
{
    using System;
    using System.Windows.Forms;
    using SocietiesMS.DAL;

    /// <summary>
    /// Dialog for admin to create a new society.
    /// </summary>
    public class CreateSocietyForm : Form
    {
        private Label   lblName, lblDesc, lblHeadID, lblStatus;
        private TextBox txtName, txtDesc, txtHeadID;
        private Button  btnCreate, btnCancel;

        public CreateSocietyForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Create Society";
            this.Size = new System.Drawing.Size(400, 310);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int lx = 20, tx = 140, lw = 110, tw = 220;

            lblName  = new Label  { Text = "Society Name:", Location = new System.Drawing.Point(lx, 20),  Size = new System.Drawing.Size(lw, 25) };
            txtName  = new TextBox { Location = new System.Drawing.Point(tx, 18),  Size = new System.Drawing.Size(tw, 25) };

            lblDesc  = new Label  { Text = "Description:",  Location = new System.Drawing.Point(lx, 60),  Size = new System.Drawing.Size(lw, 25) };
            txtDesc  = new TextBox { Location = new System.Drawing.Point(tx, 58),  Size = new System.Drawing.Size(tw, 25) };

            lblHeadID = new Label  { Text = "Head UserID:", Location = new System.Drawing.Point(lx, 100), Size = new System.Drawing.Size(lw, 25) };
            txtHeadID = new TextBox { Location = new System.Drawing.Point(tx, 98),  Size = new System.Drawing.Size(80, 25) };

            btnCreate = new Button { Text = "Create", Location = new System.Drawing.Point(tx, 140), Size = new System.Drawing.Size(90, 30) };
            btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(tx + 100, 140), Size = new System.Drawing.Size(90, 30) };

            lblStatus = new Label { Location = new System.Drawing.Point(lx, 185), Size = new System.Drawing.Size(350, 50),
                                    ForeColor = System.Drawing.Color.Red };

            btnCreate.Click += BtnCreate_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblName, txtName, lblDesc, txtDesc, lblHeadID, txtHeadID,
                  btnCreate, btnCancel, lblStatus });
        }

        // ----------------------------------------------------------------
        // BtnCreate_Click
        // CC = 5 (UserID check + 2 validation branches + success/fail)
        // ----------------------------------------------------------------
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblStatus.Text = "Society name is required.";
                return;
            }
            int headID;
            if (!int.TryParse(txtHeadID.Text, out headID))
            {
                lblStatus.Text = "Enter a valid numeric User ID.";
                return;
            }

            // Audit Check: Verify if UserID exists before attempting insert (Avoid FK Exception)
            if (!UserDAL.UserExists(headID))
            {
                lblStatus.Text = "User ID does not exist. Please check the Users table.";
                return;
            }

            try 
            {
                bool ok = SocietyDAL.CreateSociety(txtName.Text.Trim(), txtDesc.Text.Trim(), headID);
                if (ok) { MessageBox.Show("Society created (Pending approval)."); this.Close(); }
                else     lblStatus.Text = "Society name already exists.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Database Error: " + ex.Message;
            }
        }
    }
}

// ============================================================
// File: Forms/AssignTaskForm.cs
// ============================================================
namespace SocietiesMS.Forms
{
    using System;
    using System.Windows.Forms;
    using SocietiesMS.DAL;

    /// <summary>
    /// Dialog for assigning a task to a society member.
    /// </summary>
    public class AssignTaskForm : Form
    {
        private readonly int _societyID;
        private readonly int _assignedByUserID;

        private Label        lblTo, lblTitle, lblDesc, lblDue, lblStatus;
        private TextBox      txtTo, txtTitle, txtDesc;
        private DateTimePicker dtpDue;
        private CheckBox     chkNoDue;
        private Button       btnAssign, btnCancel;

        public AssignTaskForm(int societyID, int assignedByUserID)
        {
            _societyID       = societyID;
            _assignedByUserID = assignedByUserID;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Assign Task";
            this.Size = new System.Drawing.Size(420, 360);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int lx = 20, tx = 140, lw = 110, tw = 240;

            lblTo    = new Label  { Text = "Assign To (UserID):", Location = new System.Drawing.Point(lx, 20),  Size = new System.Drawing.Size(lw, 25) };
            txtTo    = new TextBox { Location = new System.Drawing.Point(tx, 18),  Size = new System.Drawing.Size(80, 25) };

            lblTitle = new Label  { Text = "Task Title:",         Location = new System.Drawing.Point(lx, 60),  Size = new System.Drawing.Size(lw, 25) };
            txtTitle = new TextBox { Location = new System.Drawing.Point(tx, 58),  Size = new System.Drawing.Size(tw, 25) };

            lblDesc  = new Label  { Text = "Description:",        Location = new System.Drawing.Point(lx, 100), Size = new System.Drawing.Size(lw, 25) };
            txtDesc  = new TextBox { Location = new System.Drawing.Point(tx, 98),  Size = new System.Drawing.Size(tw, 25) };

            lblDue   = new Label  { Text = "Due Date:",           Location = new System.Drawing.Point(lx, 140), Size = new System.Drawing.Size(lw, 25) };
            dtpDue   = new DateTimePicker { Location = new System.Drawing.Point(tx, 138), Size = new System.Drawing.Size(tw, 25) };
            chkNoDue = new CheckBox { Text = "No due date", Location = new System.Drawing.Point(tx, 170), Size = new System.Drawing.Size(120, 25) };
            chkNoDue.CheckedChanged += (s, ev) => dtpDue.Enabled = !chkNoDue.Checked;

            btnAssign = new Button { Text = "Assign Task", Location = new System.Drawing.Point(tx, 205), Size = new System.Drawing.Size(110, 30) };
            btnCancel = new Button { Text = "Cancel",      Location = new System.Drawing.Point(tx + 120, 205), Size = new System.Drawing.Size(90, 30) };

            lblStatus = new Label { Location = new System.Drawing.Point(lx, 245), Size = new System.Drawing.Size(360, 50),
                                    ForeColor = System.Drawing.Color.Red };

            btnAssign.Click += BtnAssign_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblTo, txtTo, lblTitle, txtTitle, lblDesc, txtDesc,
                  lblDue, dtpDue, chkNoDue, btnAssign, btnCancel, lblStatus });
        }

        // ----------------------------------------------------------------
        // BtnAssign_Click – validate and assign
        // CC = 6 (UserID check + logic)
        // ----------------------------------------------------------------
        private void BtnAssign_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            int toUID;
            if (!int.TryParse(txtTo.Text, out toUID))
            {
                lblStatus.Text = "Enter a valid numeric User ID.";
                return;
            }

            // Audit Check: Prevent FK crash
            if (!UserDAL.UserExists(toUID))
            {
                lblStatus.Text = "Assigned User ID does not exist.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                lblStatus.Text = "Task title is required.";
                return;
            }
            DateTime? due = chkNoDue.Checked ? (DateTime?)null : dtpDue.Value;
            
            try 
            {
                bool ok = TaskDAL.AssignTask(_societyID, toUID, _assignedByUserID,
                                             txtTitle.Text.Trim(), txtDesc.Text.Trim(), due);
                if (ok) { MessageBox.Show("Task assigned successfully."); this.Close(); }
                else     lblStatus.Text = "Failed. Check that due date is not in the past.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Database Error: " + ex.Message;
            }
        }
    }
}
