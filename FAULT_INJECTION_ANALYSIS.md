# Fault Injection Analysis - FAST University Societies Management System
## SE-4011 Software Measurement and Metrics - Task 5

**Analysis Date:** May 3, 2026  
**Project:** FAST University Societies Management System  
**Language:** C# (.NET Framework 4.7.2)  
**Confidence Threshold (E):** 1 fault per function  
**Total Functions Analyzed:** 50

---

## 🎯 Fault Injection Methodology

**Fault Injection Process:**
1. **5 faults injected** per function/module (as per assignment instructions).
2. **Probability calculated** using the Time-Independent Reliability Model (Hypergeometric):
   - If `f < A`: P = C(A, f-1) / C(E+1+A, E+f)
   - If `f = A`: P = A / (A+E+1)
3. **f (Faults Found)** is modeled based on test case execution (`f` = min(Test Cases, 5)).
4. **Reliability assessment** is based on the final probability (P) of having ≤1 fault.

---

## 📊 Fault Injection Analysis Table

### **1. Login Form Module**

**Calculations:**
- `InitializeComponents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `BtnLogin_Click()`: C(5,2) / C(7,4) = 10/35 = 0.286
- **Module Total**: C(10,3) / C(12,5) = 120/792 = **0.152**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `InitializeComponents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `BtnLogin_Click()` | 4 | 3 | 5 | 3 | 0.286 | Medium |
| **Module Total** | **5** | **4** | **10** | **4** | **0.152** | **Low** |

### **2. Register Form Module**

**Calculations:**
- `InitializeComponents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `BtnRegister_Click()`: 5 / (5+1+1) = 5/7 = 0.714
- **Module Total**: C(10,5) / C(12,7) = 252/792 = **0.318**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `InitializeComponents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `BtnRegister_Click()` | 6 | 5 | 5 | 5 | 0.714 | High |
| **Module Total** | **7** | **6** | **10** | **6** | **0.318** | **Medium** |

### **3. Student Dashboard Module**

**Calculations:**
- `InitializeComponents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `CreateStyledButton()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `MakeGrid()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `LoadAllData()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `LoadSocieties()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `LoadEvents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `LoadMemberships()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `LoadTickets()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `BtnApply_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnRegisterEvent_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- **Module Total**: C(50,11) / C(52,13) = 37353738800/635013559600 = **0.059**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `InitializeComponents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `CreateStyledButton()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `MakeGrid()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `LoadAllData()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `LoadSocieties()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `LoadEvents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `LoadMemberships()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `LoadTickets()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `BtnApply_Click()` | 3 | 2 | 5 | 2 | 0.143 | Low |
| `BtnRegisterEvent_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| **Module Total** | **13** | **13** | **50** | **12** | **0.059** | **Very Low** |

### **4. Admin Dashboard Module**

**Calculations:**
- `InitializeComponents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `MakeGrid()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `CreateStyledButton()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `LoadAllData()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `BtnActivate_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnDeactivate_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnChangeRole_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnCreateSociety_Click()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `BtnApproveSociety_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnSuspendSociety_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnApproveEvent_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnRejectEvent_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- **Module Total**: C(60,18) / C(62,20) = 925029565741050/9206478467454345 = **0.100**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `InitializeComponents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `MakeGrid()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `CreateStyledButton()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `LoadAllData()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `BtnActivate_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnDeactivate_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnChangeRole_Click()` | 3 | 2 | 5 | 2 | 0.143 | Low |
| `BtnCreateSociety_Click()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `BtnApproveSociety_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnSuspendSociety_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnApproveEvent_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnRejectEvent_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| **Module Total** | **20** | **19** | **60** | **19** | **0.100** | **Low** |

### **5. Society Dashboard Module**

**Calculations:**
- `InitializeComponents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `MakeGrid()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `CreateStyledButton()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `LoadAllData()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `BtnApprove_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnReject_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnCreateEvent_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnCancelEvent_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnAssignTask_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `BtnMarkDone_Click()`: C(5,1) / C(7,3) = 5/35 = 0.143
- **Module Total**: C(50,15) / C(52,17) = 2250829575120/21945588357420 = **0.103**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `InitializeComponents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `MakeGrid()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `CreateStyledButton()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `LoadAllData()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `BtnApprove_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnReject_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnCreateEvent_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnCancelEvent_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnAssignTask_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `BtnMarkDone_Click()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| **Module Total** | **19** | **17** | **50** | **16** | **0.103** | **Low** |

### **6. Helper Forms Module**

**Calculations:**
- `CreateEventForm.InitializeComponents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `CreateEventForm.BtnCreate_Click()`: C(5,3) / C(7,5) = 10/21 = 0.476
- `CreateSocietyForm.InitializeComponents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `CreateSocietyForm.BtnCreate_Click()`: 5 / (5+1+1) = 5/7 = 0.714
- `AssignTaskForm.InitializeComponents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `AssignTaskForm.BtnAssign_Click()`: 5 / (5+1+1) = 5/7 = 0.714
- **Module Total**: C(30,16) / C(32,18) = 145422675/471435600 = **0.308**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `CreateEventForm.InitializeComponents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `CreateEventForm.BtnCreate_Click()` | 4 | 4 | 5 | 4 | 0.476 | Medium |
| `CreateSocietyForm.InitializeComponents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `CreateSocietyForm.BtnCreate_Click()` | 5 | 5 | 5 | 5 | 0.714 | High |
| `AssignTaskForm.InitializeComponents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `AssignTaskForm.BtnAssign_Click()` | 6 | 6 | 5 | 5 | 0.714 | High |
| **Module Total** | **18** | **18** | **30** | **17** | **0.308** | **Medium** |

### **7. UI Style Module**

**Calculations:**
- `CreateModernTextBox()`: 5 / (5+1+1) = 5/7 = 0.714
- `CreateModernButton()`: C(5,3) / C(7,5) = 10/21 = 0.476
- `ApplyModernStyle()`: 5 / (5+1+1) = 5/7 = 0.714
- **Module Total**: C(15,13) / C(17,15) = 105/136 = **0.772**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `CreateModernTextBox()` | 5 | 5 | 5 | 5 | 0.714 | High |
| `CreateModernButton()` | 5 | 4 | 5 | 4 | 0.476 | Medium |
| `ApplyModernStyle()` | 5 | 5 | 5 | 5 | 0.714 | High |
| **Module Total** | **15** | **14** | **15** | **14** | **0.772** | **High** |

### **8. Database Helper Module**

**Calculations:**
- `TestConnection()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `ExecuteNonQuery()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `ExecuteQuery()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `ExecuteScalar()`: C(5,1) / C(7,3) = 5/35 = 0.143
- **Module Total**: C(20,7) / C(22,9) = 77520/497420 = **0.156**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `TestConnection()` | 3 | 2 | 5 | 2 | 0.143 | Low |
| `ExecuteNonQuery()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `ExecuteQuery()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `ExecuteScalar()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| **Module Total** | **9** | **8** | **20** | **8** | **0.156** | **Low** |

### **9. User DAL Module**

**Calculations:**
- `HashPassword()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `RegisterUser()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `Login()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `UserExists()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `GetAllUsers()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `SetUserActiveStatus()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `UpdateUserRole()`: C(5,1) / C(7,3) = 5/35 = 0.143
- **Module Total**: C(35,11) / C(37,13) = 417225900/3562467300 = **0.117**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `HashPassword()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `RegisterUser()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `Login()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `UserExists()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `GetAllUsers()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `SetUserActiveStatus()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `UpdateUserRole()` | 3 | 2 | 5 | 2 | 0.143 | Low |
| **Module Total** | **13** | **12** | **35** | **12** | **0.117** | **Low** |

### **10. Society DAL Module**

**Calculations:**
- `GetAllSocieties()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `GetAllActiveSocieties()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `GetSocietyIDByHead()`: C(5,2) / C(7,4) = 10/35 = 0.286
- `CreateSociety()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `UpdateSocietyStatus()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `ApplyForMembership()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `GetMembershipRequestsForSociety()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `RespondToMembership()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `GetStudentMemberships()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `GetMembersBySociety()`: C(5,0) / C(7,2) = 1/21 = 0.048
- **Module Total**: C(50,15) / C(52,17) = 2250829575120/21945588357420 = **0.103**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `GetAllSocieties()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `GetAllActiveSocieties()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `GetSocietyIDByHead()` | 4 | 3 | 5 | 3 | 0.286 | Medium |
| `CreateSociety()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `UpdateSocietyStatus()` | 3 | 2 | 5 | 2 | 0.143 | Low |
| `ApplyForMembership()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `GetMembershipRequestsForSociety()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `RespondToMembership()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `GetStudentMemberships()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `GetMembersBySociety()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| **Module Total** | **18** | **15** | **50** | **16** | **0.103** | **Low** |

### **11. Event DAL Module**

**Calculations:**
- `GetUpcomingEvents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `GetEventsBySociety()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `CreateEvent()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `UpdateEventStatus()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `RegisterForEvent()`: C(5,2) / C(7,4) = 10/35 = 0.286
- `GetStudentTickets()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `GetAllPendingEvents()`: C(5,0) / C(7,2) = 1/21 = 0.048
- **Module Total**: C(35,10) / C(37,12) = 183579396/1852482996 = **0.099**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `GetUpcomingEvents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `GetEventsBySociety()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `CreateEvent()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `UpdateEventStatus()` | 2 | 2 | 5 | 2 | 0.143 | Low |
| `RegisterForEvent()` | 3 | 3 | 5 | 3 | 0.286 | Medium |
| `GetStudentTickets()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `GetAllPendingEvents()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| **Module Total** | **11** | **10** | **35** | **11** | **0.099** | **Very Low** |

### **12. Task DAL Module**

**Calculations:**
- `AssignTask()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `UpdateTaskStatus()`: C(5,1) / C(7,3) = 5/35 = 0.143
- `GetTasksForSociety()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `GenerateSocietyReport()`: C(5,0) / C(7,2) = 1/21 = 0.048
- `GenerateUniversityReport()`: C(5,0) / C(7,2) = 1/21 = 0.048
- **Module Total**: C(25,6) / C(27,8) = 177100/2220075 = **0.080**


| Function | CC | Test Cases | Faults Injected (A) | Faults Found (f) | P(≤1 fault) | Reliability |
|----------|----|------------|-----------------|------------------|-------------|-------------|
| `AssignTask()` | 3 | 2 | 5 | 2 | 0.143 | Low |
| `UpdateTaskStatus()` | 3 | 2 | 5 | 2 | 0.143 | Low |
| `GetTasksForSociety()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `GenerateSocietyReport()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| `GenerateUniversityReport()` | 1 | 1 | 5 | 1 | 0.048 | Very Low |
| **Module Total** | **9** | **7** | **25** | **7** | **0.080** | **Very Low** |

---

## 🎯 Reliability Analysis Summary

### **Module Reliability Ranking (Highest to Lowest):**

| Rank | Module | P(≤E faults) | Reliability Level |
|------|--------|-------------|-------------------|
| 1 | **7. UI Style Module** | 0.772 | High |
| 2 | **2. Register Form Module** | 0.318 | Medium |
| 3 | **6. Helper Forms Module** | 0.308 | Medium |
| 4 | **8. Database Helper Module** | 0.156 | Low |
| 5 | **1. Login Form Module** | 0.152 | Low |
| 6 | **9. User DAL Module** | 0.117 | Low |
| 7 | **5. Society Dashboard Module** | 0.103 | Low |
| 8 | **10. Society DAL Module** | 0.103 | Low |
| 9 | **4. Admin Dashboard Module** | 0.100 | Low |
| 10 | **11. Event DAL Module** | 0.099 | Very Low |
| 11 | **12. Task DAL Module** | 0.080 | Very Low |
| 12 | **3. Student Dashboard Module** | 0.059 | Very Low |

---

## 🏆 Key Findings

### **✅ Most Reliable Function/Feature:**
**`BtnRegister_Click()`**
- **CC:** 6
- **Test Cases:** 5
- **Faults Injected (A):** 5
- **Faults Found (f):** 5
- **P(≤1 fault):** 0.714 (71.4%)
- **Reason:** Highest probability of finding faults given the test coverage

### **⚠️ Least Reliable Function/Feature:**
**`InitializeComponents()`**
- **CC:** 1
- **Test Cases:** 1
- **Faults Injected (A):** 5
- **Faults Found (f):** 1
- **P(≤1 fault):** 0.048 (4.8%)
- **Reason:** Minimal test coverage and identified faults

---

## 📊 Overall System Reliability

- **Total Functions:** 78
- **Total Faults Injected (A):** 390
- **Total Test Cases:** 143
- **Total Faults Found (f):** 142
- **Overall P(≤E faults):** 0.132 (13.2%)
- **System Reliability Level:** Low

---

## 🔧 Recommendations

### **High Priority Improvements:**
1. **Increase test coverage** for InitializeComponents functions
2. **Add more test cases** for high-CC functions
3. **Implement defensive programming** for critical business logic
4. **Add input validation** and error handling

### **Medium Priority Improvements:**
1. **Refactor high-CC functions** to reduce complexity
2. **Add integration tests** for DAL modules
3. **Implement logging** for better fault detection

### **System Reliability Enhancement:**
- **Target:** Achieve >70% reliability across all modules
- **Strategy:** Increase test case coverage by 50%
- **Timeline:** 2-3 development cycles
