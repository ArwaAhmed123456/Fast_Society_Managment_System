using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocietiesMS.DAL;
using SocietiesMS.Tests.FaultInjection;

namespace SocietiesMS.Tests.DAL
{
    [TestClass]
    public class SocietyDALTests
    {
        [TestInitialize]
        public void Setup()
        {
            FaultInjector.ResetAllFaults();
        }

        #region GetAllSocieties Tests

        [TestMethod]
        public void GetAllSocieties_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetAllSocieties_DatabaseFault", false);

            // Act
            DataTable result = SocietyDAL.GetAllSocieties();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllSocieties_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetAllSocieties_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => SocietyDAL.GetAllSocieties());
        }

        #endregion

        #region GetAllActiveSocieties Tests

        [TestMethod]
        public void GetAllActiveSocieties_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetAllActiveSocieties_DatabaseFault", false);

            // Act
            DataTable result = SocietyDAL.GetAllActiveSocieties();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllActiveSocieties_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetAllActiveSocieties_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => SocietyDAL.GetAllActiveSocieties());
        }

        #endregion

        #region GetSocietyIDByHead Tests

        [TestMethod]
        public void GetSocietyIDByHead_ValidHead_ReturnsSocietyID()
        {
            // Arrange
            FaultInjector.SetFaultState("GetSocietyIDByHead_DatabaseFault", false);
            FaultInjector.SetFaultState("GetSocietyIDByHead_NullFault", false);

            // Act
            int result = SocietyDAL.GetSocietyIDByHead(1);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetSocietyIDByHead_InvalidHead_ReturnsMinusOne()
        {
            // Arrange
            FaultInjector.SetFaultState("GetSocietyIDByHead_DatabaseFault", false);
            FaultInjector.SetFaultState("GetSocietyIDByHead_NullFault", false);

            // Act
            int result = SocietyDAL.GetSocietyIDByHead(99999);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void GetSocietyIDByHead_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetSocietyIDByHead_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => SocietyDAL.GetSocietyIDByHead(1));
        }

        [TestMethod]
        public void GetSocietyIDByHead_NullFault_ReturnsMinusOne()
        {
            // Arrange
            FaultInjector.SetFaultState("GetSocietyIDByHead_DatabaseFault", false);
            FaultInjector.SetFaultState("GetSocietyIDByHead_NullFault", true);

            // Act
            int result = SocietyDAL.GetSocietyIDByHead(1);

            // Assert
            Assert.AreEqual(-1, result);
        }

        #endregion

        #region CreateSociety Tests

        [TestMethod]
        public void CreateSociety_ValidData_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateSociety_DatabaseFault", false);
            FaultInjector.SetFaultState("CreateSociety_DataCorruptionFault", false);

            // Act
            bool result = SocietyDAL.CreateSociety("Test Society", "Test Description", 1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateSociety_EmptyName_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateSociety_DatabaseFault", false);

            // Act
            bool result = SocietyDAL.CreateSociety("", "Test Description", 1);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateSociety_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateSociety_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.CreateSociety("Test Society", "Test Description", 1));
        }

        [TestMethod]
        public void CreateSociety_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateSociety_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.CreateSociety("Test Society", "Test Description", 1));
        }

        #endregion

        #region UpdateSocietyStatus Tests

        [TestMethod]
        public void UpdateSocietyStatus_ValidStatus_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateSocietyStatus_DatabaseFault", false);
            FaultInjector.SetFaultState("UpdateSocietyStatus_DataCorruptionFault", false);

            // Act
            bool result = SocietyDAL.UpdateSocietyStatus(1, "Active");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateSocietyStatus_InvalidStatus_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateSocietyStatus_DatabaseFault", false);

            // Act
            bool result = SocietyDAL.UpdateSocietyStatus(1, "InvalidStatus");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateSocietyStatus_AllValidStatuses_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateSocietyStatus_DatabaseFault", false);
            string[] validStatuses = { "Active", "Suspended", "Deleted", "Pending" };

            // Act & Assert
            foreach (string status in validStatuses)
            {
                bool result = SocietyDAL.UpdateSocietyStatus(1, status);
                Assert.IsTrue(result, $"Status '{status}' should be valid");
            }
        }

        [TestMethod]
        public void UpdateSocietyStatus_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateSocietyStatus_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.UpdateSocietyStatus(1, "Active"));
        }

        [TestMethod]
        public void UpdateSocietyStatus_DataCorruptionFault_CorruptsStatus()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateSocietyStatus_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.UpdateSocietyStatus(1, "Active"));
        }

        #endregion

        #region ApplyForMembership Tests

        [TestMethod]
        public void ApplyForMembership_ValidData_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("ApplyForMembership_DatabaseFault", false);
            FaultInjector.SetFaultState("ApplyForMembership_DataCorruptionFault", false);

            // Act
            bool result = SocietyDAL.ApplyForMembership(1, 1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ApplyForMembership_AlreadyApplied_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("ApplyForMembership_DatabaseFault", false);

            // Act
            bool result = SocietyDAL.ApplyForMembership(1, 1);

            // Assert
            // Should return false if already applied
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ApplyForMembership_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("ApplyForMembership_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.ApplyForMembership(1, 1));
        }

        [TestMethod]
        public void ApplyForMembership_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("ApplyForMembership_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.ApplyForMembership(1, 1));
        }

        #endregion

        #region GetMembershipRequestsForSociety Tests

        [TestMethod]
        public void GetMembershipRequestsForSociety_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetMembershipRequestsForSociety_DatabaseFault", false);

            // Act
            DataTable result = SocietyDAL.GetMembershipRequestsForSociety(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetMembershipRequestsForSociety_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetMembershipRequestsForSociety_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.GetMembershipRequestsForSociety(1));
        }

        #endregion

        #region RespondToMembership Tests

        [TestMethod]
        public void RespondToMembership_ValidDecision_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("RespondToMembership_DatabaseFault", false);
            FaultInjector.SetFaultState("RespondToMembership_DataCorruptionFault", false);

            // Act
            bool result = SocietyDAL.RespondToMembership(1, "Approved");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RespondToMembership_InvalidDecision_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("RespondToMembership_DatabaseFault", false);

            // Act
            bool result = SocietyDAL.RespondToMembership(1, "InvalidDecision");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RespondToMembership_AllValidDecisions_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("RespondToMembership_DatabaseFault", false);
            string[] validDecisions = { "Approved", "Rejected" };

            // Act & Assert
            foreach (string decision in validDecisions)
            {
                bool result = SocietyDAL.RespondToMembership(1, decision);
                Assert.IsTrue(result, $"Decision '{decision}' should be valid");
            }
        }

        [TestMethod]
        public void RespondToMembership_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("RespondToMembership_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.RespondToMembership(1, "Approved"));
        }

        [TestMethod]
        public void RespondToMembership_DataCorruptionFault_CorruptsDecision()
        {
            // Arrange
            FaultInjector.SetFaultState("RespondToMembership_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.RespondToMembership(1, "Approved"));
        }

        #endregion

        #region GetStudentMemberships Tests

        [TestMethod]
        public void GetStudentMemberships_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetStudentMemberships_DatabaseFault", false);

            // Act
            DataTable result = SocietyDAL.GetStudentMemberships(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetStudentMemberships_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetStudentMemberships_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.GetStudentMemberships(1));
        }

        #endregion

        #region GetMembersBySociety Tests

        [TestMethod]
        public void GetMembersBySociety_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetMembersBySociety_DatabaseFault", false);

            // Act
            DataTable result = SocietyDAL.GetMembersBySociety(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetMembersBySociety_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetMembersBySociety_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                SocietyDAL.GetMembersBySociety(1));
        }

        #endregion

        #region Integration Tests

        [TestMethod]
        public void Integration_CreateSocietyThenGetID_ReturnsValidID()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateSociety_DatabaseFault", false);
            FaultInjector.SetFaultState("GetSocietyIDByHead_DatabaseFault", false);
            FaultInjector.SetFaultState("GetSocietyIDByHead_NullFault", false);

            // Act
            bool created = SocietyDAL.CreateSociety("Integration Society", "Test Description", 1);
            int societyID = SocietyDAL.GetSocietyIDByHead(1);

            // Assert
            Assert.IsTrue(created);
            Assert.IsTrue(societyID > 0);
        }

        [TestMethod]
        public void Integration_ApplyMembershipThenRespond_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("ApplyForMembership_DatabaseFault", false);
            FaultInjector.SetFaultState("RespondToMembership_DatabaseFault", false);

            // Act
            bool applied = SocietyDAL.ApplyForMembership(1, 1);
            bool responded = SocietyDAL.RespondToMembership(1, "Approved");

            // Assert
            Assert.IsTrue(applied);
            Assert.IsTrue(responded);
        }

        #endregion
    }
}
