// File: DAL/SocietyDAL.cs
using System;
using System.Data;
using System.Data.SqlClient;

namespace SocietiesMS.DAL
{
    /// <summary>
    /// Data Access Layer for Societies and Memberships.
    /// </summary>
    public class SocietyDAL
    {
        // ----------------------------------------------------------------
        // GetAllActiveSocieties – student browsing
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetAllActiveSocieties()
        {
            string sql = @"SELECT s.SocietyID, s.Name, s.Description, s.Status,
                                  u.FullName AS HeadName
                           FROM Societies s
                           LEFT JOIN Users u ON s.HeadUserID = u.UserID
                           WHERE s.Status = 'Active'
                           ORDER BY s.Name";
            return DatabaseHelper.ExecuteQuery(sql);
        }

        // ----------------------------------------------------------------
        // GetAllSocieties – admin view (all statuses)
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetAllSocieties()
        {
            string sql = @"SELECT s.SocietyID, s.Name, s.Description, s.Status,
                                  u.FullName AS HeadName, s.CreatedAt
                           FROM Societies s
                           LEFT JOIN Users u ON s.HeadUserID = u.UserID
                           ORDER BY s.CreatedAt DESC";
            return DatabaseHelper.ExecuteQuery(sql);
        }

        // ----------------------------------------------------------------
        // GetSocietyIDByHead – find society ID for a given head user
        // CC = 2
        // ----------------------------------------------------------------
        public static int GetSocietyIDByHead(int userID)
        {
            string sql = "SELECT SocietyID FROM Societies WHERE HeadUserID=@UID AND Status='Active'";
            var prms = new SqlParameter[] { new SqlParameter("@UID", userID) };
            object result = DatabaseHelper.ExecuteScalar(sql, prms);
            return (result == null || result == DBNull.Value) ? -1 : Convert.ToInt32(result);
        }

        // ----------------------------------------------------------------
        // CreateSociety – admin creates society
        // CC = 1
        // ----------------------------------------------------------------
        public static bool CreateSociety(string name, string description, int headUserID)
        {
            string sql = @"INSERT INTO Societies (Name, Description, HeadUserID, Status)
                           VALUES (@Name, @Description, @HeadUserID, 'Pending')";
            SqlParameter[] prms = {
                new SqlParameter("@Name",        name),
                new SqlParameter("@Description", description),
                new SqlParameter("@HeadUserID",  headUserID)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // UpdateSocietyStatus – admin approve/suspend/delete
        // CC = 2
        // ----------------------------------------------------------------
        public static bool UpdateSocietyStatus(int societyID, string status)
        {
            string[] allowed = { "Active", "Suspended", "Deleted", "Pending" };
            if (System.Array.IndexOf(allowed, status) < 0) return false;

            string sql = "UPDATE Societies SET Status=@Status WHERE SocietyID=@SocietyID";
            SqlParameter[] prms = {
                new SqlParameter("@Status",    status),
                new SqlParameter("@SocietyID", societyID)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // ApplyForMembership – student applies
        // CC = 2
        // ----------------------------------------------------------------
        public static bool ApplyForMembership(int userID, int societyID)
        {
            string sql = @"IF NOT EXISTS (SELECT 1 FROM Memberships WHERE UserID=@UserID AND SocietyID=@SocietyID)
                           BEGIN
                               INSERT INTO Memberships (UserID, SocietyID, Status)
                               VALUES (@UserID, @SocietyID, 'Pending')
                           END";
            SqlParameter[] prms = {
                new SqlParameter("@UserID",    userID),
                new SqlParameter("@SocietyID", societyID)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // GetMembershipRequestsForSociety – society head views requests
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetMembershipRequestsForSociety(int societyID)
        {
            string sql = @"SELECT m.MembershipID, u.FullName, u.Email,
                                  m.Status, m.RequestedAt
                           FROM Memberships m
                           JOIN Users u ON m.UserID = u.UserID
                           WHERE m.SocietyID = @SocietyID
                           ORDER BY m.RequestedAt DESC";
            SqlParameter[] prms = { new SqlParameter("@SocietyID", societyID) };
            return DatabaseHelper.ExecuteQuery(sql, prms);
        }

        // ----------------------------------------------------------------
        // RespondToMembership – society head approve/reject
        // CC = 2 (valid response check)
        // ----------------------------------------------------------------
        public static bool RespondToMembership(int membershipID, string decision)
        {
            if (decision != "Approved" && decision != "Rejected") return false;

            string sql = @"UPDATE Memberships
                           SET Status=@Status, RespondedAt=GETDATE()
                           WHERE MembershipID=@MembershipID";
            SqlParameter[] prms = {
                new SqlParameter("@Status",       decision),
                new SqlParameter("@MembershipID", membershipID)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // GetMembersBySociety – member list
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetMembersBySociety(int societyID)
        {
            string sql = @"SELECT u.UserID, u.FullName, u.Email, m.Status, m.RequestedAt
                           FROM Memberships m
                           JOIN Users u ON m.UserID = u.UserID
                           WHERE m.SocietyID = @SocietyID AND m.Status = 'Approved'
                           ORDER BY u.FullName";
            SqlParameter[] prms = { new SqlParameter("@SocietyID", societyID) };
            return DatabaseHelper.ExecuteQuery(sql, prms);
        }

        // ----------------------------------------------------------------
        // GetStudentMemberships – student sees their memberships
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetStudentMemberships(int userID)
        {
            string sql = @"SELECT s.Name AS SocietyName, m.Status, m.RequestedAt
                           FROM Memberships m
                           JOIN Societies s ON m.SocietyID = s.SocietyID
                           WHERE m.UserID = @UserID
                           ORDER BY m.RequestedAt DESC";
            SqlParameter[] prms = { new SqlParameter("@UserID", userID) };
            return DatabaseHelper.ExecuteQuery(sql, prms);
        }
    }
}
