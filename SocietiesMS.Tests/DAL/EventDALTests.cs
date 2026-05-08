using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocietiesMS.DAL;
using SocietiesMS.Tests.FaultInjection;

namespace SocietiesMS.Tests.DAL
{
    [TestClass]
    public class EventDALTests
    {
        [TestInitialize]
        public void Setup()
        {
            FaultInjector.ResetAllFaults();
        }

        #region GetUpcomingEvents Tests

        [TestMethod]
        public void GetUpcomingEvents_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetUpcomingEvents_DatabaseFault", false);

            // Act
            DataTable result = EventDAL.GetUpcomingEvents();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUpcomingEvents_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetUpcomingEvents_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => EventDAL.GetUpcomingEvents());
        }

        #endregion

        #region GetEventsBySociety Tests

        [TestMethod]
        public void GetEventsBySociety_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetEventsBySociety_DatabaseFault", false);

            // Act
            DataTable result = EventDAL.GetEventsBySociety(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetEventsBySociety_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetEventsBySociety_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => EventDAL.GetEventsBySociety(1));
        }

        #endregion

        #region CreateEvent Tests

        [TestMethod]
        public void CreateEvent_ValidData_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateEvent_DatabaseFault", false);
            FaultInjector.SetFaultState("CreateEvent_DataCorruptionFault", false);

            // Act
            bool result = EventDAL.CreateEvent(1, "Test Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), 100);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateEvent_InvalidCapacity_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateEvent_DatabaseFault", false);

            // Act
            bool result = EventDAL.CreateEvent(1, "Test Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateEvent_NegativeCapacity_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateEvent_DatabaseFault", false);

            // Act
            bool result = EventDAL.CreateEvent(1, "Test Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), -10);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateEvent_EmptyTitle_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateEvent_DatabaseFault", false);

            // Act
            bool result = EventDAL.CreateEvent(1, "", "Test Description", "Test Location", DateTime.Now.AddDays(7), 100);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateEvent_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateEvent_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                EventDAL.CreateEvent(1, "Test Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), 100));
        }

        [TestMethod]
        public void CreateEvent_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateEvent_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                EventDAL.CreateEvent(1, "Test Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), 100));
        }

        #endregion

        #region UpdateEventStatus Tests

        [TestMethod]
        public void UpdateEventStatus_ValidStatus_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateEventStatus_DatabaseFault", false);
            FaultInjector.SetFaultState("UpdateEventStatus_DataCorruptionFault", false);

            // Act
            bool result = EventDAL.UpdateEventStatus(1, "Approved");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateEventStatus_InvalidStatus_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateEventStatus_DatabaseFault", false);

            // Act
            bool result = EventDAL.UpdateEventStatus(1, "InvalidStatus");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateEventStatus_AllValidStatuses_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateEventStatus_DatabaseFault", false);
            string[] validStatuses = { "Approved", "Cancelled", "Pending" };

            // Act & Assert
            foreach (string status in validStatuses)
            {
                bool result = EventDAL.UpdateEventStatus(1, status);
                Assert.IsTrue(result, $"Status '{status}' should be valid");
            }
        }

        [TestMethod]
        public void UpdateEventStatus_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateEventStatus_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                EventDAL.UpdateEventStatus(1, "Approved"));
        }

        [TestMethod]
        public void UpdateEventStatus_DataCorruptionFault_CorruptsStatus()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateEventStatus_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                EventDAL.UpdateEventStatus(1, "Approved"));
        }

        #endregion

        #region RegisterForEvent Tests

        [TestMethod]
        public void RegisterForEvent_ValidData_ReturnsSuccessMessage()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterForEvent_DatabaseFault", false);
            FaultInjector.SetFaultState("RegisterForEvent_DataCorruptionFault", false);

            // Act
            string result = EventDAL.RegisterForEvent(1, 1);

            // Assert
            Assert.AreEqual("Successfully registered for the event.", result);
        }

        [TestMethod]
        public void RegisterForEvent_AlreadyRegistered_ReturnsAlreadyRegisteredMessage()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterForEvent_DatabaseFault", false);

            // Act
            string result = EventDAL.RegisterForEvent(1, 1);

            // Assert
            Assert.AreEqual("Already registered.", result);
        }

        [TestMethod]
        public void RegisterForEvent_EventFull_ReturnsEventFullMessage()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterForEvent_DatabaseFault", false);

            // Act
            string result = EventDAL.RegisterForEvent(1, 1);

            // Assert
            Assert.AreEqual("Event is full.", result);
        }

        [TestMethod]
        public void RegisterForEvent_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterForEvent_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                EventDAL.RegisterForEvent(1, 1));
        }

        [TestMethod]
        public void RegisterForEvent_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterForEvent_DataCorruptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                EventDAL.RegisterForEvent(1, 1));
        }

        #endregion

        #region GetStudentTickets Tests

        [TestMethod]
        public void GetStudentTickets_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetStudentTickets_DatabaseFault", false);

            // Act
            DataTable result = EventDAL.GetStudentTickets(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetStudentTickets_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetStudentTickets_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => EventDAL.GetStudentTickets(1));
        }

        #endregion

        #region GetAllPendingEvents Tests

        [TestMethod]
        public void GetAllPendingEvents_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetAllPendingEvents_DatabaseFault", false);

            // Act
            DataTable result = EventDAL.GetAllPendingEvents();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllPendingEvents_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetAllPendingEvents_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => EventDAL.GetAllPendingEvents());
        }

        #endregion

        #region Integration Tests

        [TestMethod]
        public void Integration_CreateEventThenUpdateStatus_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateEvent_DatabaseFault", false);
            FaultInjector.SetFaultState("UpdateEventStatus_DatabaseFault", false);

            // Act
            bool created = EventDAL.CreateEvent(1, "Integration Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), 100);
            bool updated = EventDAL.UpdateEventStatus(1, "Approved");

            // Assert
            Assert.IsTrue(created);
            Assert.IsTrue(updated);
        }

        [TestMethod]
        public void Integration_CreateEventThenRegister_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("CreateEvent_DatabaseFault", false);
            FaultInjector.SetFaultState("RegisterForEvent_DatabaseFault", false);

            // Act
            bool created = EventDAL.CreateEvent(1, "Integration Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), 100);
            string registered = EventDAL.RegisterForEvent(1, 1);

            // Assert
            Assert.IsTrue(created);
            Assert.AreEqual("Successfully registered for the event.", registered);
        }

        #endregion
    }
}
