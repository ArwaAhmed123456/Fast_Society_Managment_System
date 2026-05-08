using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocietiesMS.DAL;
using SocietiesMS.Tests.FaultInjection;
using Moq;

namespace SocietiesMS.Tests.DAL
{
    [TestClass]
    public class UserDALTests
    {
        [TestInitialize]
        public void Setup()
        {
            FaultInjector.ResetAllFaults();
        }

        #region HashPassword Tests

        [TestMethod]
        public void HashPassword_ValidInput_ReturnsHash()
        {
            // Arrange
            string password = "test123";
            FaultInjector.SetFaultState("HashPassword_NullFault", false);
            FaultInjector.SetFaultState("HashPassword_ExceptionFault", false);

            // Act
            string result = UserDAL.HashPassword(password);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(password, result);
            Assert.AreEqual(64, result.Length); // SHA256 hash length
        }

        [TestMethod]
        public void HashPassword_DifferentInputs_DifferentHashes()
        {
            // Arrange
            string password1 = "password123";
            string password2 = "password456";

            // Act
            string hash1 = UserDAL.HashPassword(password1);
            string hash2 = UserDAL.HashPassword(password2);

            // Assert
            Assert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void HashPassword_SameInput_SameHash()
        {
            // Arrange
            string password = "password123";

            // Act
            string hash1 = UserDAL.HashPassword(password);
            string hash2 = UserDAL.HashPassword(password);

            // Assert
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void HashPassword_NullFault_InjectsNull()
        {
            // Arrange
            string password = "test123";
            FaultInjector.SetFaultState("HashPassword_NullFault", true);

            // Act
            string result = UserDAL.HashPassword(password);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void HashPassword_ExceptionFault_ThrowsException()
        {
            // Arrange
            string password = "test123";
            FaultInjector.SetFaultState("HashPassword_ExceptionFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => UserDAL.HashPassword(password));
        }

        #endregion

        #region RegisterUser Tests

        [TestMethod]
        public void RegisterUser_ValidData_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterUser_DatabaseFault", false);
            FaultInjector.SetFaultState("RegisterUser_DataCorruptionFault", false);

            // Act
            bool result = UserDAL.RegisterUser("Test User", "test@example.com", "password123");

            // Assert
            // Note: This will fail in test environment without actual database
            // In real implementation, you'd use mocking or test database
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegisterUser_EmptyName_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterUser_DatabaseFault", false);

            // Act
            bool result = UserDAL.RegisterUser("", "test@example.com", "password123");

            // Assert
            // Should return false due to validation or database constraints
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterUser_DatabaseFault", false);

            // Act
            bool result = UserDAL.RegisterUser("Test User", "invalid-email", "password123");

            // Assert
            // Should return false due to validation or database constraints
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterUser_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                UserDAL.RegisterUser("Test User", "test@example.com", "password123"));
        }

        [TestMethod]
        public void RegisterUser_DataCorruptionFault_CorruptsData()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterUser_DataCorruptionFault", true);

            // Act & Assert
            // This would corrupt the data in the database
            Assert.ThrowsException<InvalidOperationException>(() => 
                UserDAL.RegisterUser("Test User", "test@example.com", "password123"));
        }

        #endregion

        #region Login Tests

        [TestMethod]
        public void Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            FaultInjector.SetFaultState("Login_DatabaseFault", false);
            FaultInjector.SetFaultState("Login_NullFault", false);

            // Act
            var result = UserDAL.Login("admin@fast.edu", "admin123");

            // Assert
            // Note: This will fail in test environment without actual database
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            FaultInjector.SetFaultState("Login_DatabaseFault", false);
            FaultInjector.SetFaultState("Login_NullFault", false);

            // Act
            var result = UserDAL.Login("invalid@email.com", "wrongpassword");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Login_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("Login_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                UserDAL.Login("admin@fast.edu", "admin123"));
        }

        [TestMethod]
        public void Login_NullFault_ReturnsNull()
        {
            // Arrange
            FaultInjector.SetFaultState("Login_NullFault", true);

            // Act
            var result = UserDAL.Login("admin@fast.edu", "admin123");

            // Assert
            Assert.IsNull(result);
        }

        #endregion

        #region UserExists Tests

        [TestMethod]
        public void UserExists_ValidUser_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UserExists_CalculationFault", false);

            // Act
            bool result = UserDAL.UserExists(1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserExists_InvalidUser_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("UserExists_CalculationFault", false);

            // Act
            bool result = UserDAL.UserExists(99999);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UserExists_CalculationFault_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("UserExists_CalculationFault", true);

            // Act
            bool result = UserDAL.UserExists(1);

            // Assert
            // Fault injection should corrupt the calculation
            Assert.IsFalse(result);
        }

        #endregion

        #region GetAllUsers Tests

        [TestMethod]
        public void GetAllUsers_NoFault_ReturnsDataTable()
        {
            // Arrange
            FaultInjector.SetFaultState("GetAllUsers_DatabaseFault", false);

            // Act
            DataTable result = UserDAL.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllUsers_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("GetAllUsers_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => UserDAL.GetAllUsers());
        }

        #endregion

        #region SetUserActiveStatus Tests

        [TestMethod]
        public void SetUserActiveStatus_ValidUser_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("SetUserActiveStatus_DatabaseFault", false);

            // Act
            bool result = UserDAL.SetUserActiveStatus(1, true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetUserActiveStatus_InvalidUser_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("SetUserActiveStatus_DatabaseFault", false);

            // Act
            bool result = UserDAL.SetUserActiveStatus(99999, true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SetUserActiveStatus_DatabaseFault_ThrowsException()
        {
            // Arrange
            FaultInjector.SetFaultState("SetUserActiveStatus_DatabaseFault", true);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => 
                UserDAL.SetUserActiveStatus(1, true));
        }

        #endregion

        #region UpdateUserRole Tests

        [TestMethod]
        public void UpdateUserRole_ValidRole_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateUserRole_DatabaseFault", false);
            FaultInjector.SetFaultState("UpdateUserRole_DataCorruptionFault", false);

            // Act
            bool result = UserDAL.UpdateUserRole(1, "Admin");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateUserRole_InvalidRole_ReturnsFalse()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateUserRole_DatabaseFault", false);

            // Act
            bool result = UserDAL.UpdateUserRole(1, "InvalidRole");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateUserRole_AllValidRoles_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateUserRole_DatabaseFault", false);
            string[] validRoles = { "Student", "SocietyHead", "Member", "Admin" };

            // Act & Assert
            foreach (string role in validRoles)
            {
                bool result = UserDAL.UpdateUserRole(1, role);
                Assert.IsTrue(result, $"Role '{role}' should be valid");
            }
        }

        [TestMethod]
        public void UpdateUserRole_DataCorruptionFault_CorruptsRole()
        {
            // Arrange
            FaultInjector.SetFaultState("UpdateUserRole_DataCorruptionFault", true);

            // Act & Assert
            // This would corrupt the role data in the database
            Assert.ThrowsException<InvalidOperationException>(() => 
                UserDAL.UpdateUserRole(1, "Admin"));
        }

        #endregion

        #region Integration Tests

        [TestMethod]
        public void Integration_RegisterUserThenLogin_Success()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterUser_DatabaseFault", false);
            FaultInjector.SetFaultState("Login_DatabaseFault", false);
            FaultInjector.SetFaultState("Login_NullFault", false);

            string email = "integration@test.com";
            string password = "test123";

            // Act
            bool registered = UserDAL.RegisterUser("Integration Test", email, password);
            var user = UserDAL.Login(email, password);

            // Assert
            Assert.IsTrue(registered);
            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        public void Integration_UserExistsAfterRegister_ReturnsTrue()
        {
            // Arrange
            FaultInjector.SetFaultState("RegisterUser_DatabaseFault", false);
            FaultInjector.SetFaultState("UserExists_CalculationFault", false);

            string email = "exists@test.com";
            string password = "test123";

            // Act
            bool registered = UserDAL.RegisterUser("Exists Test", email, password);
            bool exists = UserDAL.UserExists(1); // Assuming user ID 1

            // Assert
            Assert.IsTrue(registered);
            Assert.IsTrue(exists);
        }

        #endregion
    }
}
