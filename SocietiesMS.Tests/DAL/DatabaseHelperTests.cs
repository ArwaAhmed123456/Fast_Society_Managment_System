using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocietiesMS.DAL;
using SocietiesMS.Tests.FaultInjection;

namespace SocietiesMS.Tests.DAL
{
    [TestClass]
    public class DatabaseHelperTests
    {
        [TestInitialize]
        public void Setup()
        {
            FaultInjector.ResetAllFaults();
        }

        #region TestConnection Tests

        [TestMethod]
        public void TestConnection_NoFault_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("TestConnection_DatabaseFault", false);

            // Act
            bool result = DatabaseHelper.TestConnection();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestConnection_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("TestConnection_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => DatabaseHelper.TestConnection());
        }

        #endregion

        #region ExecuteNonQuery Tests

        [TestMethod]
        public void ExecuteNonQuery_WithParameters_ReturnsPositive()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteNonQuery_DatabaseFault", false);
            FaultInjector.SetFaultState("ExecuteNonQuery_DataCorruptionFault", false);
            string sql = "UPDATE Users SET IsActive = 1 WHERE UserID = 1";
            SqlParameter[] parameters = { new SqlParameter("@UserID", 1) };

            // Act
            int result = DatabaseHelper.ExecuteNonQuery(sql, parameters);

            // Assert
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void ExecuteNonQuery_WithoutParameters_ReturnsPositive()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteNonQuery_DatabaseFault", false);
            string sql = "SELECT 1";

            // Act
            int result = DatabaseHelper.ExecuteNonQuery(sql);

            // Assert
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void ExecuteNonQuery_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteNonQuery_DatabaseFault", true);
            string sql = "SELECT 1";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => DatabaseHelper.ExecuteNonQuery(sql));
        }

        [TestMethod]
        public void ExecuteNonQuery_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteNonQuery_DataCorruptionFault", true);
            string sql = "SELECT 1";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => DatabaseHelper.ExecuteNonQuery(sql));
        }

        #endregion

        #region ExecuteQuery Tests

        [TestMethod]
        public void ExecuteQuery_WithParameters_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteQuery_DatabaseFault", false);
            FaultInjector.SetFaultState("ExecuteQuery_DataCorruptionFault", false);
            string sql = "SELECT * FROM Users WHERE UserID = @UserID";
            SqlParameter[] parameters = { new SqlParameter("@UserID", 1) };

            // Act
            DataTable result = DatabaseHelper.ExecuteQuery(sql, parameters);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExecuteQuery_WithoutParameters_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteQuery_DatabaseFault", false);
            string sql = "SELECT 1 AS TestColumn";

            // Act
            DataTable result = DatabaseHelper.ExecuteQuery(sql);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Rows.Count > 0);
        }

        [TestMethod]
        public void ExecuteQuery_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteQuery_DatabaseFault", true);
            string sql = "SELECT 1";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => DatabaseHelper.ExecuteQuery(sql));
        }

        [TestMethod]
        public void ExecuteQuery_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteQuery_DataCorruptionFault", true);
            string sql = "SELECT 1";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => DatabaseHelper.ExecuteQuery(sql));
        }

        #endregion

        #region ExecuteScalar Tests

        [TestMethod]
        public void ExecuteScalar_WithParameters_ReturnsValue()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteScalar_DatabaseFault", false);
            FaultInjector.SetFaultState("ExecuteScalar_DataCorruptionFault", false);
            string sql = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID";
            SqlParameter[] parameters = { new SqlParameter("@UserID", 1) };

            // Act
            object result = DatabaseHelper.ExecuteScalar(sql, parameters);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExecuteScalar_WithoutParameters_ReturnsValue()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteScalar_DatabaseFault", false);
            string sql = "SELECT 1";

            // Act
            object result = DatabaseHelper.ExecuteScalar(sql);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, Convert.ToInt32(result));
        }

        [TestMethod]
        public void ExecuteScalar_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteScalar_DatabaseFault", true);
            string sql = "SELECT 1";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => DatabaseHelper.ExecuteScalar(sql));
        }

        [TestMethod]
        public void ExecuteScalar_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteScalar_DataCorruptionFault", true);
            string sql = "SELECT 1";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => DatabaseHelper.ExecuteScalar(sql));
        }

        #endregion

        #region Integration Tests

        [TestMethod]
        public void Integration_ExecuteNonQueryThenExecuteQuery_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteNonQuery_DatabaseFault", false);
            FaultInjector.SetFaultState("ExecuteQuery_DatabaseFault", false);

            // Act
            int updated = DatabaseHelper.ExecuteNonQuery("SELECT 1");
            DataTable result = DatabaseHelper.ExecuteQuery("SELECT 1 AS TestColumn");

            // Assert
            Assert.IsTrue(updated >= 0);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Integration_ExecuteQueryThenExecuteScalar_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("ExecuteQuery_DatabaseFault", false);
            FaultInjector.SetFaultState("ExecuteScalar_DatabaseFault", false);

            // Act
            DataTable result = DatabaseHelper.ExecuteQuery("SELECT 1 AS TestColumn");
            object scalar = DatabaseHelper.ExecuteScalar("SELECT 1");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(scalar);
        }

        [TestMethod]
        public void Integration_AllMethodsNoFault_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("TestConnection_DatabaseFault", false);
            FaultInjector.SetFaultState("ExecuteNonQuery_DatabaseFault", false);
            FaultInjector.SetFaultState("ExecuteQuery_DatabaseFault", false);
            FaultInjector.SetFaultState("ExecuteScalar_DatabaseFault", false);

            // Act
            bool connected = DatabaseHelper.TestConnection();
            int updated = DatabaseHelper.ExecuteNonQuery("SELECT 1");
            DataTable query = DatabaseHelper.ExecuteQuery("SELECT 1 AS TestColumn");
            object scalar = DatabaseHelper.ExecuteScalar("SELECT 1");

            // Assert
            Assert.IsTrue(connected);
            Assert.IsTrue(updated >= 0);
            Assert.IsNotNull(query);
            Assert.IsNotNull(scalar);
        }

        #endregion
    }
}
