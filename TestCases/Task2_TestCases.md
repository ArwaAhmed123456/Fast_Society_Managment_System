# Task 2: Test Cases for FAST Societies Management System

This document contains the detailed test cases for all core functions within the project, mapped to their respective modules and Cyclomatic Complexity (CC).

## 1. User Authentication (UserDAL)

| Function Name | CC | Test Case ID | Input | Expected Outcome |
| :--- | :---: | :--- | :--- | :--- |
| `HashPassword` | 1 | TC-AUTH-01 | `"admin123"` | SHA-256 Hex string |
| `HashPassword` | 1 | TC-AUTH-02 | `""` (Empty) | SHA-256 Hex string for empty input |
| `Login` | 3 | TC-AUTH-03 | `("admin@fast.edu.pk", "admin123")` | Valid Admin User object returned |
| `Login` | 3 | TC-AUTH-04 | `("wrong@email.com", "any")` | `null` (Invalid email) |
| `Login` | 3 | TC-AUTH-05 | `("admin@fast.edu.pk", "wrong")` | `null` (Invalid password) |
| `RegisterUser` | 2 | TC-AUTH-06 | `("Ali", "ali@nu.edu", "pass")` | `true` (New user created) |
| `RegisterUser` | 2 | TC-AUTH-07 | `("Ali", "ali@nu.edu", "pass")` | `false` (Duplicate email) |

## 2. Society Management (SocietyDAL)

| Function Name | CC | Test Case ID | Input | Expected Outcome |
| :--- | :---: | :--- | :--- | :--- |
| `GetAllActiveSocieties` | 1 | TC-SOC-01 | Execute Query | DataTable with active societies |
| `UpdateSocietyStatus` | 2 | TC-SOC-02 | `(1, "Active")` | `true` (Valid status update) |
| `UpdateSocietyStatus` | 2 | TC-SOC-03 | `(1, "Invalid")` | `false` (Validation failed) |
| `ApplyForMembership` | 2 | TC-SOC-04 | `(UserID:10, SocietyID:1)` | `true` (New application) |
| `ApplyForMembership` | 2 | TC-SOC-05 | `(UserID:10, SocietyID:1)` | `false` (Already applied) |

## 3. Event Management (EventDAL)

| Function Name | CC | Test Case ID | Input | Expected Outcome |
| :--- | :---: | :--- | :--- | :--- |
| `CreateEvent` | 2 | TC-EVT-01 | `(Capacity: 50)` | `true` (Valid capacity) |
| `CreateEvent` | 2 | TC-EVT-02 | `(Capacity: -5)` | `false` (Capacity validation failed) |
| `RegisterForEvent` | 3 | TC-EVT-03 | `(New Student)` | `"TicketCode"` (Success) |
| `RegisterForEvent` | 3 | TC-EVT-04 | `(Duplicate Student)` | `"Already registered"` |
| `RegisterForEvent` | 3 | TC-EVT-05 | `(Event Full)` | `"Event is full"` |

## 4. Task & Reports (TaskDAL)

| Function Name | CC | Test Case ID | Input | Expected Outcome |
| :--- | :---: | :--- | :--- | :--- |
| `AssignTask` | 2 | TC-TSK-01 | `(Date: Tomorrow)` | `true` (Valid due date) |
| `AssignTask` | 2 | TC-TSK-02 | `(Date: Yesterday)` | `false` (Date validation failed) |
| `UpdateTaskStatus` | 2 | TC-TSK-03 | `("Completed")` | `true` (Valid status) |
| `UpdateTaskStatus` | 2 | TC-TSK-04 | `("Random")` | `false` (Status validation failed) |
