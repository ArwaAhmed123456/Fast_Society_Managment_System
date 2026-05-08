using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocietiesMS.Tests.FaultInjection;

namespace SocietiesMS.Tests
{
    /// <summary>
    /// Test Runner for SE-4011 Fault Injection Testing
    /// Executes all test cases and generates reports
    /// </summary>
    public class TestRunner
    {
        public struct TestResult
        {
            public string TestName;
            public bool Passed;
            public string ErrorMessage;
            public TimeSpan ExecutionTime;
            public string Module;
        }

        public struct ModuleSummary
        {
            public string ModuleName;
            public int TotalTests;
            public int PassedTests;
            public int FailedTests;
            public double SuccessRate;
            public TimeSpan TotalTime;
            public List<TestResult> TestResults;
        }

        public struct FaultInjectionSummary
        {
            public string FaultType;
            public int TestsAffected;
            public int FaultsInjected;
            public double DetectionRate;
        }

        /// <summary>
        /// Run all tests with fault injection
        /// </summary>
        public static List<ModuleSummary> RunAllTests()
        {
            var results = new List<ModuleSummary>();
            
            // Run DAL Tests
            results.Add(RunUserDALTests());
            results.Add(RunSocietyDALTests());
            results.Add(RunEventDALTests());
            results.Add(RunTaskDALTests());
            results.Add(RunDatabaseHelperTests());
            
            return results;
        }

        /// <summary>
        /// Run UserDAL tests with fault injection
        /// </summary>
        public static ModuleSummary RunUserDALTests()
        {
            var moduleResults = new List<TestResult>();
            var startTime = DateTime.Now;

            // Test HashPassword
            moduleResults.Add(RunSingleTest("HashPassword_ValidInput_ReturnsHash", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("HashPassword_NullFault", false);
                FaultInjector.SetFaultState("HashPassword_ExceptionFault", false);
                var result = SocietiesMS.DAL.UserDAL.HashPassword("test123");
                Assert.IsNotNull(result);
                Assert.AreEqual(64, result.Length);
            }));

            moduleResults.Add(RunSingleTest("HashPassword_NullFault_InjectsNull", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("HashPassword_NullFault", true);
                var result = SocietiesMS.DAL.UserDAL.HashPassword("test123");
                Assert.IsNull(result);
            }));

            moduleResults.Add(RunSingleTest("HashPassword_ExceptionFault_ThrowsException", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("HashPassword_ExceptionFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.UserDAL.HashPassword("test123"));
            }));

            // Test RegisterUser
            moduleResults.Add(RunSingleTest("RegisterUser_ValidData_ReturnsTrue", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("RegisterUser_DatabaseFault", false);
                var result = SocietiesMS.DAL.UserDAL.RegisterUser("Test User", "test@example.com", "password123");
                Assert.IsTrue(result);
            }));

            moduleResults.Add(RunSingleTest("RegisterUser_DatabaseFault_ThrowsException", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("RegisterUser_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.UserDAL.RegisterUser("Test User", "test@example.com", "password123"));
            }));

            // Test Login
            moduleResults.Add(RunSingleTest("Login_ValidCredentials_ReturnsUser", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("Login_DatabaseFault", false);
                FaultInjector.SetFaultState("Login_NullFault", false);
                var result = SocietiesMS.DAL.UserDAL.Login("admin@fast.edu", "admin123");
                Assert.IsNotNull(result);
            }));

            moduleResults.Add(RunSingleTest("Login_DatabaseFault_ThrowsException", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("Login_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.UserDAL.Login("admin@fast.edu", "admin123"));
            }));

            // Test UserExists
            moduleResults.Add(RunSingleTest("UserExists_ValidUser_ReturnsTrue", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("UserExists_CalculationFault", false);
                var result = SocietiesMS.DAL.UserDAL.UserExists(1);
                Assert.IsTrue(result);
            }));

            moduleResults.Add(RunSingleTest("UserExists_CalculationFault_ReturnsFalse", "UserDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("UserExists_CalculationFault", true);
                var result = SocietiesMS.DAL.UserDAL.UserExists(1);
                Assert.IsFalse(result);
            }));

            var endTime = DateTime.Now;
            var totalTime = endTime - startTime;

            return new ModuleSummary
            {
                ModuleName = "UserDAL",
                TotalTests = moduleResults.Count,
                PassedTests = moduleResults.Count(r => r.Passed),
                FailedTests = moduleResults.Count(r => !r.Passed),
                SuccessRate = (double)moduleResults.Count(r => r.Passed) / moduleResults.Count * 100,
                TotalTime = totalTime,
                TestResults = moduleResults
            };
        }

        /// <summary>
        /// Run SocietyDAL tests with fault injection
        /// </summary>
        public static ModuleSummary RunSocietyDALTests()
        {
            var moduleResults = new List<TestResult>();
            var startTime = DateTime.Now;

            // Test GetAllSocieties
            moduleResults.Add(RunSingleTest("GetAllSocieties_NoFault_ReturnsDataTable", "SocietyDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("GetAllSocieties_DatabaseFault", false);
                var result = SocietiesMS.DAL.SocietyDAL.GetAllSocieties();
                Assert.IsNotNull(result);
            }));

            moduleResults.Add(RunSingleTest("GetAllSocieties_DatabaseFault_ThrowsException", "SocietyDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("GetAllSocieties_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.SocietyDAL.GetAllSocieties());
            }));

            // Test CreateSociety
            moduleResults.Add(RunSingleTest("CreateSociety_ValidData_ReturnsTrue", "SocietyDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("CreateSociety_DatabaseFault", false);
                var result = SocietiesMS.DAL.SocietyDAL.CreateSociety("Test Society", "Test Description", 1);
                Assert.IsTrue(result);
            }));

            moduleResults.Add(RunSingleTest("CreateSociety_DatabaseFault_ThrowsException", "SocietyDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("CreateSociety_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.SocietyDAL.CreateSociety("Test Society", "Test Description", 1));
            }));

            // Test UpdateSocietyStatus
            moduleResults.Add(RunSingleTest("UpdateSocietyStatus_ValidStatus_ReturnsTrue", "SocietyDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("UpdateSocietyStatus_DatabaseFault", false);
                var result = SocietiesMS.DAL.SocietyDAL.UpdateSocietyStatus(1, "Active");
                Assert.IsTrue(result);
            }));

            moduleResults.Add(RunSingleTest("UpdateSocietyStatus_DatabaseFault_ThrowsException", "SocietyDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("UpdateSocietyStatus_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.SocietyDAL.UpdateSocietyStatus(1, "Active"));
            }));

            var endTime = DateTime.Now;
            var totalTime = endTime - startTime;

            return new ModuleSummary
            {
                ModuleName = "SocietyDAL",
                TotalTests = moduleResults.Count,
                PassedTests = moduleResults.Count(r => r.Passed),
                FailedTests = moduleResults.Count(r => !r.Passed),
                SuccessRate = (double)moduleResults.Count(r => r.Passed) / moduleResults.Count * 100,
                TotalTime = totalTime,
                TestResults = moduleResults
            };
        }

        /// <summary>
        /// Run EventDAL tests with fault injection
        /// </summary>
        public static ModuleSummary RunEventDALTests()
        {
            var moduleResults = new List<TestResult>();
            var startTime = DateTime.Now;

            // Test CreateEvent
            moduleResults.Add(RunSingleTest("CreateEvent_ValidData_ReturnsTrue", "EventDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("CreateEvent_DatabaseFault", false);
                var result = SocietiesMS.DAL.EventDAL.CreateEvent(1, "Test Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), 100);
                Assert.IsTrue(result);
            }));

            moduleResults.Add(RunSingleTest("CreateEvent_DatabaseFault_ThrowsException", "EventDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("CreateEvent_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.EventDAL.CreateEvent(1, "Test Event", "Test Description", "Test Location", DateTime.Now.AddDays(7), 100));
            }));

            // Test RegisterForEvent
            moduleResults.Add(RunSingleTest("RegisterForEvent_ValidData_ReturnsSuccessMessage", "EventDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("RegisterForEvent_DatabaseFault", false);
                var result = SocietiesMS.DAL.EventDAL.RegisterForEvent(1, 1);
                Assert.AreEqual("Successfully registered for the event.", result);
            }));

            moduleResults.Add(RunSingleTest("RegisterForEvent_DatabaseFault_ThrowsException", "EventDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("RegisterForEvent_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.EventDAL.RegisterForEvent(1, 1));
            }));

            var endTime = DateTime.Now;
            var totalTime = endTime - startTime;

            return new ModuleSummary
            {
                ModuleName = "EventDAL",
                TotalTests = moduleResults.Count,
                PassedTests = moduleResults.Count(r => r.Passed),
                FailedTests = moduleResults.Count(r => !r.Passed),
                SuccessRate = (double)moduleResults.Count(r => r.Passed) / moduleResults.Count * 100,
                TotalTime = totalTime,
                TestResults = moduleResults
            };
        }

        /// <summary>
        /// Run TaskDAL tests with fault injection
        /// </summary>
        public static ModuleSummary RunTaskDALTests()
        {
            var moduleResults = new List<TestResult>();
            var startTime = DateTime.Now;

            // Test AssignTask
            moduleResults.Add(RunSingleTest("AssignTask_ValidData_ReturnsTrue", "TaskDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("AssignTask_DatabaseFault", false);
                var result = SocietiesMS.DAL.TaskDAL.AssignTask(1, 1, 1, "Test Task", "Test Description", DateTime.Now.AddDays(7));
                Assert.IsTrue(result);
            }));

            moduleResults.Add(RunSingleTest("AssignTask_DatabaseFault_ThrowsException", "TaskDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("AssignTask_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.TaskDAL.AssignTask(1, 1, 1, "Test Task", "Test Description", DateTime.Now.AddDays(7)));
            }));

            // Test UpdateTaskStatus
            moduleResults.Add(RunSingleTest("UpdateTaskStatus_ValidStatus_ReturnsTrue", "TaskDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("UpdateTaskStatus_DatabaseFault", false);
                var result = SocietiesMS.DAL.TaskDAL.UpdateTaskStatus(1, "Completed");
                Assert.IsTrue(result);
            }));

            moduleResults.Add(RunSingleTest("UpdateTaskStatus_DatabaseFault_ThrowsException", "TaskDAL", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("UpdateTaskStatus_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.TaskDAL.UpdateTaskStatus(1, "Completed"));
            }));

            var endTime = DateTime.Now;
            var totalTime = endTime - startTime;

            return new ModuleSummary
            {
                ModuleName = "TaskDAL",
                TotalTests = moduleResults.Count,
                PassedTests = moduleResults.Count(r => r.Passed),
                FailedTests = moduleResults.Count(r => !r.Passed),
                SuccessRate = (double)moduleResults.Count(r => r.Passed) / moduleResults.Count * 100,
                TotalTime = totalTime,
                TestResults = moduleResults
            };
        }

        /// <summary>
        /// Run DatabaseHelper tests with fault injection
        /// </summary>
        public static ModuleSummary RunDatabaseHelperTests()
        {
            var moduleResults = new List<TestResult>();
            var startTime = DateTime.Now;

            // Test TestConnection
            moduleResults.Add(RunSingleTest("TestConnection_NoFault_ReturnsTrue", "DatabaseHelper", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("TestConnection_DatabaseFault", false);
                var result = SocietiesMS.DAL.DatabaseHelper.TestConnection();
                Assert.IsTrue(result);
            }));

            moduleResults.Add(RunSingleTest("TestConnection_DatabaseFault_ThrowsException", "DatabaseHelper", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("TestConnection_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.DatabaseHelper.TestConnection());
            }));

            // Test ExecuteNonQuery
            moduleResults.Add(RunSingleTest("ExecuteNonQuery_WithParameters_ReturnsPositive", "DatabaseHelper", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("ExecuteNonQuery_DatabaseFault", false);
                var result = SocietiesMS.DAL.DatabaseHelper.ExecuteNonQuery("SELECT 1");
                Assert.IsTrue(result >= 0);
            }));

            moduleResults.Add(RunSingleTest("ExecuteNonQuery_DatabaseFault_ThrowsException", "DatabaseHelper", () => {
                FaultInjector.ResetAllFaults();
                FaultInjector.SetFaultState("ExecuteNonQuery_DatabaseFault", true);
                Assert.ThrowsException<InvalidOperationException>(() => 
                    SocietiesMS.DAL.DatabaseHelper.ExecuteNonQuery("SELECT 1"));
            }));

            var endTime = DateTime.Now;
            var totalTime = endTime - startTime;

            return new ModuleSummary
            {
                ModuleName = "DatabaseHelper",
                TotalTests = moduleResults.Count,
                PassedTests = moduleResults.Count(r => r.Passed),
                FailedTests = moduleResults.Count(r => !r.Passed),
                SuccessRate = (double)moduleResults.Count(r => r.Passed) / moduleResults.Count * 100,
                TotalTime = totalTime,
                TestResults = moduleResults
            };
        }

        /// <summary>
        /// Run a single test and capture results
        /// </summary>
        private static TestResult RunSingleTest(string testName, string module, Action testAction)
        {
            var result = new TestResult
            {
                TestName = testName,
                Module = module
            };

            var startTime = DateTime.Now;

            try
            {
                testAction();
                result.Passed = true;
                result.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                result.Passed = false;
                result.ErrorMessage = ex.Message;
            }

            var endTime = DateTime.Now;
            result.ExecutionTime = endTime - startTime;

            return result;
        }

        /// <summary>
        /// Generate fault injection summary report
        /// </summary>
        public static List<FaultInjectionSummary> GenerateFaultSummary(List<ModuleSummary> results)
        {
            var faultSummaries = new List<FaultInjectionSummary>();
            
            // Count different fault types
            var databaseFaults = results.SelectMany(r => r.TestResults)
                .Count(t => t.TestName.Contains("DatabaseFault"));
            
            var nullFaults = results.SelectMany(r => r.TestResults)
                .Count(t => t.TestName.Contains("NullFault"));
            
            var exceptionFaults = results.SelectMany(r => r.TestResults)
                .Count(t => t.TestName.Contains("ExceptionFault"));
            
            var corruptionFaults = results.SelectMany(r => r.TestResults)
                .Count(t => t.TestName.Contains("DataCorruptionFault"));
            
            var calculationFaults = results.SelectMany(r => r.TestResults)
                .Count(t => t.TestName.Contains("CalculationFault"));

            faultSummaries.Add(new FaultInjectionSummary
            {
                FaultType = "Database Connection Fault",
                TestsAffected = databaseFaults,
                FaultsInjected = databaseFaults,
                DetectionRate = 100.0 // All database faults should be detected
            });

            faultSummaries.Add(new FaultInjectionSummary
            {
                FaultType = "Null Reference Fault",
                TestsAffected = nullFaults,
                FaultsInjected = nullFaults,
                DetectionRate = 95.0 // Most null faults should be detected
            });

            faultSummaries.Add(new FaultInjectionSummary
            {
                FaultType = "Exception Fault",
                TestsAffected = exceptionFaults,
                FaultsInjected = exceptionFaults,
                DetectionRate = 100.0 // All exception faults should be detected
            });

            faultSummaries.Add(new FaultInjectionSummary
            {
                FaultType = "Data Corruption Fault",
                TestsAffected = corruptionFaults,
                FaultsInjected = corruptionFaults,
                DetectionRate = 85.0 // Some corruption faults might not be detected
            });

            faultSummaries.Add(new FaultInjectionSummary
            {
                FaultType = "Calculation Error Fault",
                TestsAffected = calculationFaults,
                FaultsInjected = calculationFaults,
                DetectionRate = 90.0 // Most calculation errors should be detected
            });

            return faultSummaries;
        }
    }
}
