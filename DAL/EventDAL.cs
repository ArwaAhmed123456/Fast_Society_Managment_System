// File: DAL/EventDAL.cs
using System;
using System.Data;
using System.Data.SqlClient;

namespace SocietiesMS.DAL
{
    /// <summary>
    /// Data Access Layer for Events and Event Registrations.
    /// </summary>
    public class EventDAL
    {
        // ----------------------------------------------------------------
        // GetUpcomingEvents – student views all approved upcoming events
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetUpcomingEvents()
        {
            string sql = @"SELECT e.EventID, s.Name AS SocietyName, e.Title,
                                  e.Description, e.Location, e.EventDate, e.Capacity, e.Status
                           FROM Events e
                           JOIN Societies s ON e.SocietyID = s.SocietyID
                           WHERE e.Status = 'Approved' AND e.EventDate >= GETDATE()
                           ORDER BY e.EventDate";
            return DatabaseHelper.ExecuteQuery(sql);
        }

        // ----------------------------------------------------------------
        // GetEventsBySociety – society head manages their events
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetEventsBySociety(int societyID)
        {
            string sql = @"SELECT EventID, Title, Description, Location,
                                  EventDate, Capacity, Status, CreatedAt
                           FROM Events
                           WHERE SocietyID = @SocietyID
                           ORDER BY EventDate DESC";
            SqlParameter[] prms = { new SqlParameter("@SocietyID", societyID) };
            return DatabaseHelper.ExecuteQuery(sql, prms);
        }

        // ----------------------------------------------------------------
        // CreateEvent – society head creates an event
        // CC = 2 (capacity validation)
        // ----------------------------------------------------------------
        public static bool CreateEvent(int societyID, string title, string description,
                                       string location, DateTime eventDate, int capacity)
        {
            if (capacity <= 0) return false;

            string sql = @"INSERT INTO Events (SocietyID, Title, Description, Location, EventDate, Capacity, Status)
                           VALUES (@SocietyID, @Title, @Description, @Location, @EventDate, @Capacity, 'Pending')";
            SqlParameter[] prms = {
                new SqlParameter("@SocietyID",   societyID),
                new SqlParameter("@Title",        title),
                new SqlParameter("@Description",  description),
                new SqlParameter("@Location",     location),
                new SqlParameter("@EventDate",    eventDate),
                new SqlParameter("@Capacity",     capacity)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // UpdateEventStatus – admin approves/cancels; society head cancels
        // CC = 2 (status validation)
        // ----------------------------------------------------------------
        public static bool UpdateEventStatus(int eventID, string status)
        {
            string[] allowed = { "Approved", "Cancelled", "Pending" };
            if (Array.IndexOf(allowed, status) < 0) return false;

            string sql = "UPDATE Events SET Status=@Status WHERE EventID=@EventID";
            SqlParameter[] prms = {
                new SqlParameter("@Status",  status),
                new SqlParameter("@EventID", eventID)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // RegisterForEvent – student registers
        // CC = 3 (already registered + capacity check)
        // ----------------------------------------------------------------
        public static string RegisterForEvent(int userID, int eventID)
        {
            // Check already registered
            string checkSql = "SELECT COUNT(*) FROM EventRegistrations WHERE UserID=@UserID AND EventID=@EventID";
            SqlParameter[] checkPrms = {
                new SqlParameter("@UserID",  userID),
                new SqlParameter("@EventID", eventID)
            };
            int exists = (int)DatabaseHelper.ExecuteScalar(checkSql, checkPrms);
            if (exists > 0) return "Already registered.";

            // Check capacity
            string capSql = @"SELECT e.Capacity - COUNT(r.RegistrationID) AS Remaining
                              FROM Events e
                              LEFT JOIN EventRegistrations r ON e.EventID = r.EventID
                              WHERE e.EventID = @EventID
                              GROUP BY e.Capacity";
            int remaining = Convert.ToInt32(DatabaseHelper.ExecuteScalar(capSql,
                new SqlParameter[] { new SqlParameter("@EventID", eventID) }));
            if (remaining <= 0) return "Event is full.";

            string sql = @"INSERT INTO EventRegistrations (EventID, UserID)
                           VALUES (@EventID, @UserID)";
            SqlParameter[] prms = {
                new SqlParameter("@EventID", eventID),
                new SqlParameter("@UserID",  userID)
            };
            DatabaseHelper.ExecuteNonQuery(sql, prms);
            return "Registered successfully.";
        }

        // ----------------------------------------------------------------
        // GetStudentTickets – student views their event passes
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetStudentTickets(int userID)
        {
            string sql = @"SELECT e.Title AS EventTitle, s.Name AS SocietyName,
                                  e.Location, e.EventDate, r.TicketCode, r.RegisteredAt
                           FROM EventRegistrations r
                           JOIN Events e ON r.EventID = e.EventID
                           JOIN Societies s ON e.SocietyID = s.SocietyID
                           WHERE r.UserID = @UserID
                           ORDER BY e.EventDate";
            SqlParameter[] prms = { new SqlParameter("@UserID", userID) };
            return DatabaseHelper.ExecuteQuery(sql, prms);
        }

        // ----------------------------------------------------------------
        // GetAllPendingEvents – admin approves events
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetAllPendingEvents()
        {
            string sql = @"SELECT e.EventID, s.Name AS SocietyName, e.Title,
                                  e.Location, e.EventDate, e.Capacity, e.Status, e.CreatedAt
                           FROM Events e
                           JOIN Societies s ON e.SocietyID = s.SocietyID
                           WHERE e.Status = 'Pending'
                           ORDER BY e.CreatedAt";
            return DatabaseHelper.ExecuteQuery(sql);
        }
    }
}
