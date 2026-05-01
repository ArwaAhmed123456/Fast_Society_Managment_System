// File: DAL/DatabaseHelper.cs
using System;
using System.Data;
using System.Data.SqlClient;

namespace SocietiesMS.DAL
{
    /// <summary>
    /// Central database helper. All SQL operations go through here.
    /// </summary>
    public static class DatabaseHelper
    {
        // Change connection string to match your SQL Server instance
        private static readonly string ConnectionString =
            @"Server=localhost\SQLEXPRESS;Database=SocietiesMS;Trusted_Connection=True;";

        // ----------------------------------------------------------------
        // ExecuteNonQuery – INSERT / UPDATE / DELETE
        // ----------------------------------------------------------------
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // ----------------------------------------------------------------
        // ExecuteScalar – returns single value
        // ----------------------------------------------------------------
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        // ----------------------------------------------------------------
        // ExecuteQuery – returns DataTable
        // ----------------------------------------------------------------
        public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // ----------------------------------------------------------------
        // TestConnection – used at startup
        // ----------------------------------------------------------------
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
