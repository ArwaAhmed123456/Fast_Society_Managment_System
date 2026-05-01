// File: DAL/UserDAL.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using SocietiesMS.Models;

namespace SocietiesMS.DAL
{
    /// <summary>
    /// Data Access Layer for Users.
    /// Cyclomatic Complexity targets kept low (1-4 per method).
    /// </summary>
    public class UserDAL
    {
        // ----------------------------------------------------------------
        // HashPassword – SHA-256 one-way hash
        // CC = 1 (no branches)
        // ----------------------------------------------------------------
        public static string HashPassword(string plainText)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // ----------------------------------------------------------------
        // RegisterUser – creates a new student account
        // CC = 2 (one branch: duplicate email)
        // ----------------------------------------------------------------
        public static bool RegisterUser(string fullName, string email, string password)
        {
            string hashed = HashPassword(password);
            string sql = @"IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
                           BEGIN
                               INSERT INTO Users (FullName, Email, Password, Role)
                               VALUES (@FullName, @Email, @Password, 'Student')
                           END";
            SqlParameter[] prms = {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Email",    email),
                new SqlParameter("@Password", hashed)
            };
            int rows = DatabaseHelper.ExecuteNonQuery(sql, prms);
            return rows > 0;
        }

        // ----------------------------------------------------------------
        // Login – validates credentials and returns User object
        // CC = 3 (null check + role check)
        // ----------------------------------------------------------------
        public static User Login(string email, string password)
        {
            string hashed = HashPassword(password);
            string sql = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password AND IsActive=1";
            SqlParameter[] prms = {
                new SqlParameter("@Email",    email),
                new SqlParameter("@Password", hashed)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, prms);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new User
            {
                UserID    = (int)row["UserID"],
                FullName  = row["FullName"].ToString(),
                Email     = row["Email"].ToString(),
                Role      = row["Role"].ToString(),
                IsActive  = (bool)row["IsActive"],
                CreatedAt = (DateTime)row["CreatedAt"]
            };
        }

        // ----------------------------------------------------------------
        // GetAllUsers – admin function
        // CC = 1
        // ----------------------------------------------------------------
        public static DataTable GetAllUsers()
        {
            string sql = "SELECT UserID, FullName, Email, Role, IsActive, CreatedAt FROM Users ORDER BY CreatedAt DESC";
            return DatabaseHelper.ExecuteQuery(sql);
        }

        // ----------------------------------------------------------------
        // SetUserActiveStatus – admin activate/deactivate
        // CC = 1
        // ----------------------------------------------------------------
        public static bool SetUserActiveStatus(int userID, bool isActive)
        {
            string sql = "UPDATE Users SET IsActive=@IsActive WHERE UserID=@UserID";
            SqlParameter[] prms = {
                new SqlParameter("@IsActive", isActive),
                new SqlParameter("@UserID",   userID)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }

        // ----------------------------------------------------------------
        // UpdateUserRole – admin assigns roles
        // CC = 2
        // ----------------------------------------------------------------
        public static bool UpdateUserRole(int userID, string newRole)
        {
            string[] validRoles = { "Student", "SocietyHead", "Member", "Admin" };
            bool valid = Array.IndexOf(validRoles, newRole) >= 0;
            if (!valid) return false;

            string sql = "UPDATE Users SET Role=@Role WHERE UserID=@UserID";
            SqlParameter[] prms = {
                new SqlParameter("@Role",   newRole),
                new SqlParameter("@UserID", userID)
            };
            return DatabaseHelper.ExecuteNonQuery(sql, prms) > 0;
        }
    }
}
