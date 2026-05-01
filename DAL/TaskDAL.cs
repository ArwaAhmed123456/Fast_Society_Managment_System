// File: DAL/TaskDAL.cs
using System;
using System.Data;
using System.Data.SqlClient;

namespace SocietiesMS.DAL
{
    /// <summary>
    /// Data Access Layer for Task assignment and Reports.
    /// </summary>
    public class TaskDAL
    {
        // ----------------------------------------------------------------
        // AssignTask – society head assigns task to member
        // CC = 2 (due date validation)
        // ----------------------------------------------------------------
        public static bool AssignTask(int societyID, int assignedToUserID, int assignedByUserID,
                                      string title, string description, DateTime? dueDate)
        {
            if (dueDate.HasValue && dueDate.Value < DateTime.Today) return false;

            string sql = @"INSERT INTO Tasks (SocietyID, AssignedToUserID, AssignedByUserID, Title, Description, DueDate, Status)
                           VALUES (@SocietyID, @AssignedTo, @AssignedBy, @Title, @Desc, @DueDate, 'Pending')";
            SqlParameter[] prms = {
                new SqlParameter("@SocietyID",  societyID),
                new SqlParameter("@AssignedTo", assignedToUserID),
                new SqlParameter("@AssignedBy", assignedByUserID),
                new SqlParameter("@Title",      title),
                new SqlParameter("@Desc",       (object)description ?? DBNull.Value),
                new SqlParameter("@DueDate",    (object)dueDate ?? DBNull.Value)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // GetTasksForSociety – all tasks in a society
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetTasksForSociety(int societyID)
        {
            string sql = @"SELECT t.TaskID, u.FullName AS AssignedTo, t.Title,
                                  t.Description, t.DueDate, t.Status, t.CreatedAt
                           FROM Tasks t
                           JOIN Users u ON t.AssignedToUserID = u.UserID
                           WHERE t.SocietyID = @SocietyID
                           ORDER BY t.CreatedAt DESC";
            SqlParameter[] prms = { new SqlParameter("@SocietyID", societyID) };
            return DatabaseHelper.ExecuteQuery(sql, prms);
        }

        // ----------------------------------------------------------------
        // UpdateTaskStatus – member or head updates task progress
        // CC = 2 (status validation)
        // ----------------------------------------------------------------
        public static bool UpdateTaskStatus(int taskID, string status)
        {
            string[] allowed = { "Pending", "InProgress", "Completed" };
            if (Array.IndexOf(allowed, status) < 0) return false;

            string sql = "UPDATE Tasks SET Status=@Status WHERE TaskID=@TaskID";
            SqlParameter[] prms = {
                new SqlParameter("@Status", status),
                new SqlParameter("@TaskID", taskID)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // GenerateSocietyReport – members + events summary
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GenerateSocietyReport(int societyID)
        {
            string sql = @"SELECT 'Members' AS Category, COUNT(*) AS Total
                           FROM Memberships WHERE SocietyID=@SocietyID AND Status='Approved'
                           UNION ALL
                           SELECT 'Events', COUNT(*) FROM Events
                           WHERE SocietyID=@SocietyID AND Status='Approved'
                           UNION ALL
                           SELECT 'Pending Tasks', COUNT(*) FROM Tasks
                           WHERE SocietyID=@SocietyID AND Status='Pending'";
            SqlParameter[] prms = { new SqlParameter("@SocietyID", societyID) };
            return DatabaseHelper.ExecuteQuery(sql, prms);
        }

        // ----------------------------------------------------------------
        // GenerateUniversityReport – admin-level summary
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GenerateUniversityReport()
        {
            string sql = @"SELECT s.Name AS SocietyName,
                                  COUNT(DISTINCT m.UserID)  AS TotalMembers,
                                  COUNT(DISTINCT e.EventID) AS TotalEvents
                           FROM Societies s
                           LEFT JOIN Memberships m ON s.SocietyID=m.SocietyID AND m.Status='Approved'
                           LEFT JOIN Events      e ON s.SocietyID=e.SocietyID AND e.Status='Approved'
                           WHERE s.Status = 'Active'
                           GROUP BY s.Name
                           ORDER BY TotalMembers DESC";
            return DatabaseHelper.ExecuteQuery(sql);
        }
    }
}
