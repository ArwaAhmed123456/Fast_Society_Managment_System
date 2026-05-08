using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocietiesMS.DAL;
using SocietiesMS.Tests.FaultInjection;

namespace SocietiesMS.Tests.DAL
{
    [TestClass]
    public class TaskDALTests
    {
        [TestInitialize]
        public void Setup()
        {
            FaultInjector.ResetAllFaults();
        }

        #region AssignTask Tests

        [TestMethod]
        public void AssignTask_ValidData_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("AssignTask_DatabaseFault", false);
            FaultInjector.SetFaultState("AssignTask_DataCorruptionFault", false);

            // Act
            bool result = TaskDAL.AssignTask(1, 1, 1, "Test Task", "Test Description", DateTime.Now.AddDays(7));

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AssignTask_InvalidDueDate_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("AssignTask_DatabaseFault", false);

            // Act
            bool result = TaskDAL.AssignTask(1, 1, 1, "Test Task", "Test Description", DateTime.Now.AddDays(-1));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AssignTask_EmptyTitle_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("AssignTask_DatabaseFault", false);

            // Act
            bool result = TaskDAL.AssignTask(1, 1, 1, "", "Test Description", DateTime.Now.AddDays(7));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AssignTask_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("AssignTask_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                TaskDAL.AssignTask(1, 1, 1, "Test Task", "Test Description", DateTime.Now.AddDays(7)));
        }

        [TestMethod]
        public void AssignTask_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("AssignTask_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                TaskDAL.AssignTask(1, 1, 1, "Test Task", "Test Description", DateTime.Now.AddDays(7)));
        }

        #endregion

        #region UpdateTaskStatus Tests

        [TestMethod]
        public void UpdateTaskStatus_ValidStatus_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateTaskStatus_DatabaseFault", false);
            FaultInjector.SetFaultState("UpdateTaskStatus_DataCorruptionFault", false);

            // Act
            bool result = TaskDAL.UpdateTaskStatus(1, "Completed");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateTaskStatus_InvalidStatus_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateTaskStatus_DatabaseFault", false);

            // Act
            bool result = TaskDAL.UpdateTaskStatus(1, "InvalidStatus");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateTaskStatus_AllValidStatuses_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateTaskStatus_DatabaseFault", false);
            string[] validStatuses = { "Pending", "InProgress", "Completed" };

            // Act & Assert
            foreach (string status in validStatuses)
            {
                bool result = TaskDAL.UpdateTaskStatus(1, status);
                Assert.IsTrue(result, $"Status '{status}' should be valid");
            }
        }

        [TestMethod]
        public void UpdateTaskStatus_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateTaskStatus_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                TaskDAL.UpdateTaskStatus(1, "Completed"));
        }

        [TestMethod]
        public void UpdateTaskStatus_DataCorruptionFault_CorruptsStatus()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateTaskStatus_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                TaskDAL.UpdateTaskStatus(1, "Completed"));
        }

        #endregion

        #region GetTasksForSociety Tests

        [TestMethod]
        public void GetTasksForSociety_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetTasksForSociety_DatabaseFault", false);

            // Act
            DataTable result = TaskDAL.GetTasksForSociety(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTasksForSociety_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetTasksForSociety_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => TaskDAL.GetTasksForSociety(1));
        }

        #endregion

        #region GenerateSocietyReport Tests

        [TestMethod]
        public void GenerateSocietyReport_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GenerateSocietyReport_DatabaseFault", false);

            // Act
            DataTable result = TaskDAL.GenerateSocietyReport(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GenerateSocietyReport_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GenerateSocietyReport_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => TaskDAL.GenerateSocietyReport(1));
        }

        #endregion

        #region GenerateUniversityReport Tests

        [TestMethod]
        public void GenerateUniversityReport_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GenerateUniversityReport_DatabaseFault", false);

            // Act
            DataTable result = TaskDAL.GenerateUniversityReport();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GenerateUniversityReport_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GenerateUniversityReport_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => TaskDAL.GenerateUniversityReport());
        }

        #endregion

        #region Integration Tests

        [TestMethod]
        public void Integration_AssignTaskThenUpdateStatus_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("AssignTask_DatabaseFault", false);
            FaultInjector.SetFaultState("UpdateTaskStatus_DatabaseFault", false);

            // Act
            bool assigned = TaskDAL.AssignTask(1, 1, 1, "Integration Task", "Test Description", DateTime.Now.AddDays(7));
            bool updated = TaskDAL.UpdateTaskStatus(1, "Completed");

            // Assert
            Assert.IsTrue(assigned);
            Assert.IsTrue(updated);
        }

        [TestMethod]
        public void Integration_AssignTaskThenGenerateReport_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("AssignTask_DatabaseFault", false);
            FaultInjector.SetFaultState("GenerateSocietyReport_DatabaseFault", false);

            // Act
            bool assigned = TaskDAL.AssignTask(1, 1, 1, "Integration Task", "Test Description", DateTime.Now.AddDays(7));
            DataTable report = TaskDAL.GenerateSocietyReport(1);

            // Assert
            Assert.IsTrue(assigned);
            Assert.IsNotNull(report);
        }

        #endregion
    }
}
