using System;
using System.Collections.Generic;
using System.Linq;

namespace SocietiesMS.Tests.FaultInjection
{
    /// <summary>
    /// Fault Injection Framework for SE-4011 Project
    /// Injects actual faults into code for testing reliability
    /// </summary>
    public static class FaultInjector
    {
        private static readonly Dictionary<string, bool> _faultStates = new Dictionary<string, bool>();
        private static readonly Random _random = new Random();

        /// <summary>
        /// Enable/disable specific fault for testing
        /// </summary>
        public static void SetFaultState(string faultName, bool enabled)
        {
            _faultStates[faultName] = enabled;
        }

        /// <summary>
        /// Check if fault is enabled
        /// </summary>
        public static bool IsFaultEnabled(string faultName)
        {
            return _faultStates.ContainsKey(faultName) && _faultStates[faultName];
        }

        /// <summary>
        /// Inject null reference fault
        /// </summary>
        public static T InjectNullFault<T>(string faultName, T normalValue)
        {
            if (IsFaultEnabled(faultName))
            {
                return default(T); // Returns null for reference types
            }
            return normalValue;
        }

        /// <summary>
        /// Inject exception fault
        /// </summary>
        public static void InjectExceptionFault(string faultName, string exceptionMessage = "Injected fault for testing")
        {
            if (IsFaultEnabled(faultName))
            {
                throw new InvalidOperationException(exceptionMessage);
            }
        }

        /// <summary>
        /// Inject data corruption fault
        /// </summary>
        public static string InjectDataCorruptionFault(string faultName, string originalValue)
        {
            if (IsFaultEnabled(faultName))
            {
                // Corrupt the data by changing characters
                char[] chars = originalValue.ToCharArray();
                if (chars.Length > 0)
                {
                    int index = _random.Next(chars.Length);
                    chars[index] = (char)('A' + _random.Next(26));
                }
                return new string(chars);
            }
            return originalValue;
        }

        /// <summary>
        /// Inject calculation error fault
        /// </summary>
        public static int InjectCalculationFault(string faultName, int correctValue)
        {
            if (IsFaultEnabled(faultName))
            {
                // Return incorrect calculation result
                return correctValue + _random.Next(-10, 11);
            }
            return correctValue;
        }

        /// <summary>
        /// Inject database connection fault
        /// </summary>
        public static void InjectDatabaseFault(string faultName)
        {
            if (IsFaultEnabled(faultName))
            {
                throw new InvalidOperationException("Database connection failed - Injected fault");
            }
        }

        /// <summary>
        /// Reset all fault states
        /// </summary>
        public static void ResetAllFaults()
        {
            _faultStates.Clear();
        }

        /// <summary>
        /// Get list of all available fault names
        /// </summary>
        public static List<string> GetAllFaultNames()
        {
            return _faultStates.Keys.ToList();
        }
    }
}
