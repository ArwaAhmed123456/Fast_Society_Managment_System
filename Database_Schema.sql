-- ============================================================
-- FAST Societies Management System - Detailed Database Schema
-- SE-4011 Software Measurement and Metrics
-- ============================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SocietiesMS')
    DROP DATABASE SocietiesMS;
GO

CREATE DATABASE SocietiesMS;
GO

USE SocietiesMS;
GO

-- ============================================================
-- 1. TABLE: Users
-- Purpose: Stores all registered users including Students, Heads, and Admins.
-- ============================================================
CREATE TABLE Users (
    UserID      INT IDENTITY(1,1) PRIMARY KEY,      -- PK: Unique identifier for each user
    FullName    NVARCHAR(100) NOT NULL,             -- Full name of the user
    Email       NVARCHAR(150) NOT NULL UNIQUE,      -- Unique university email (e.g., k191234@nu.edu.pk)
    Password    NVARCHAR(255) NOT NULL,             -- SHA-256 Hashed password for security
    Role        NVARCHAR(20)  NOT NULL              -- Role: Student, SocietyHead, Admin
                CHECK (Role IN ('Student','SocietyHead','Admin')),
    IsActive    BIT           NOT NULL DEFAULT 1,   -- Status: 1 for Active, 0 for Suspended
    CreatedAt   DATETIME      NOT NULL DEFAULT GETDATE(), -- Timestamp of registration
    
    -- Constraints
    CONSTRAINT CK_User_Email CHECK (Email LIKE '%@%.%') -- Basic email format validation
);
GO

-- ============================================================
-- 2. TABLE: Societies
-- Purpose: Stores details of student societies.
-- ============================================================
CREATE TABLE Societies (
    SocietyID       INT IDENTITY(1,1) PRIMARY KEY,  -- PK: Unique identifier for each society
    Name            NVARCHAR(100) NOT NULL UNIQUE,  -- Unique name of the society
    Description     NVARCHAR(500),                  -- Brief description of the society's aim
    HeadUserID      INT NOT NULL,                   -- FK: Link to the Society Head in Users table
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Pending' -- Status: Pending, Active, Suspended, Deleted
                    CHECK (Status IN ('Pending','Active','Suspended','Deleted')),
    CreatedAt       DATETIME NOT NULL DEFAULT GETDATE(),    -- Timestamp of creation
    
    -- Relationships
    CONSTRAINT FK_Societies_Users FOREIGN KEY (HeadUserID) REFERENCES Users(UserID)
);
GO

-- ============================================================
-- 3. TABLE: Memberships
-- Purpose: Links Students to Societies (Many-to-Many Relationship).
-- ============================================================
CREATE TABLE Memberships (
    MembershipID    INT IDENTITY(1,1) PRIMARY KEY,  -- PK: Unique identifier for membership record
    UserID          INT NOT NULL,                   -- FK: Link to the Student (Users table)
    SocietyID       INT NOT NULL,                   -- FK: Link to the Society (Societies table)
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Pending' -- Status: Pending, Approved, Rejected
                    CHECK (Status IN ('Pending','Approved','Rejected')),
    RequestedAt     DATETIME NOT NULL DEFAULT GETDATE(),    -- Date student applied
    RespondedAt     DATETIME,                       -- Date head approved/rejected
    
    -- Relationships & Unique Constraints
    CONSTRAINT FK_Memberships_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Memberships_Societies FOREIGN KEY (SocietyID) REFERENCES Societies(SocietyID),
    CONSTRAINT UQ_User_Society UNIQUE (UserID, SocietyID) -- Prevents duplicate applications
);
GO

-- ============================================================
-- 4. TABLE: Events
-- Purpose: Stores society events, workshops, and competitions.
-- ============================================================
CREATE TABLE Events (
    EventID         INT IDENTITY(1,1) PRIMARY KEY,  -- PK: Unique identifier for the event
    SocietyID       INT NOT NULL,                   -- FK: Link to the organizing society
    Title           NVARCHAR(150) NOT NULL,         -- Title of the event
    Description     NVARCHAR(1000),                 -- Detailed event description
    Location        NVARCHAR(200),                  -- Venue (e.g., Auditorium, Lab 1)
    EventDate       DATETIME NOT NULL,              -- Scheduled date and time
    Capacity        INT NOT NULL DEFAULT 100,       -- Max number of registrations allowed
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Pending' -- Status: Pending, Approved, Cancelled
                    CHECK (Status IN ('Pending','Approved','Cancelled')),
    CreatedAt       DATETIME NOT NULL DEFAULT GETDATE(),    -- Date event was created
    
    -- Relationships
    CONSTRAINT FK_Events_Societies FOREIGN KEY (SocietyID) REFERENCES Societies(SocietyID)
);
GO

-- ============================================================
-- 5. TABLE: EventRegistrations
-- Purpose: Tracks student registrations for specific events.
-- ============================================================
CREATE TABLE EventRegistrations (
    RegistrationID  INT IDENTITY(1,1) PRIMARY KEY,  -- PK: Unique identifier for registration
    EventID         INT NOT NULL,                   -- FK: Link to the Event
    UserID          INT NOT NULL,                   -- FK: Link to the Student
    RegisteredAt    DATETIME NOT NULL DEFAULT GETDATE(), -- Date of registration
    TicketCode      NVARCHAR(50) NOT NULL DEFAULT NEWID(), -- Unique digital ticket/pass code
    
    -- Relationships & Unique Constraints
    CONSTRAINT FK_Registrations_Events FOREIGN KEY (EventID) REFERENCES Events(EventID),
    CONSTRAINT FK_Registrations_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT UQ_User_Event UNIQUE (UserID, EventID) -- Prevents duplicate registrations
);
GO

-- ============================================================
-- 6. TABLE: Tasks
-- Purpose: Internal task coordination within a society.
-- ============================================================
CREATE TABLE Tasks (
    TaskID          INT IDENTITY(1,1) PRIMARY KEY,  -- PK: Unique identifier for the task
    SocietyID       INT NOT NULL,                   -- FK: Link to the society
    AssignedToUserID INT NOT NULL,                  -- FK: Link to member assigned the task
    AssignedByUserID INT NOT NULL,                  -- FK: Link to the Head/Member who assigned it
    Title           NVARCHAR(200) NOT NULL,         -- Short task title
    Description     NVARCHAR(500),                  -- Task details
    DueDate         DATETIME,                       -- Deadline for the task
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Pending' -- Status: Pending, InProgress, Completed
                    CHECK (Status IN ('Pending','InProgress','Completed')),
    CreatedAt       DATETIME NOT NULL DEFAULT GETDATE(),    -- Date task was created
    
    -- Relationships
    CONSTRAINT FK_Tasks_Societies FOREIGN KEY (SocietyID) REFERENCES Societies(SocietyID),
    CONSTRAINT FK_Tasks_AssignedTo FOREIGN KEY (AssignedToUserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Tasks_AssignedBy FOREIGN KEY (AssignedByUserID) REFERENCES Users(UserID)
);
GO

-- ============================================================
-- 7. TABLE: Announcements
-- Purpose: System-wide or society-specific announcements and updates.
-- ============================================================
CREATE TABLE Announcements (
    AnnouncementID  INT IDENTITY(1,1) PRIMARY KEY,  -- PK: Unique identifier for announcement
    SocietyID       INT NULL,                       -- FK: Link to society (NULL = University-wide)
    PostedByUserID  INT NOT NULL,                   -- FK: Link to the Admin/Head who posted it
    Title           NVARCHAR(200) NOT NULL,         -- Subject of the announcement
    Body            NVARCHAR(MAX) NOT NULL,         -- Detailed content of the announcement
    Priority        NVARCHAR(20) DEFAULT 'Normal'   -- Priority: Normal, High, Urgent
                    CHECK (Priority IN ('Normal','High','Urgent')),
    PostedAt        DATETIME NOT NULL DEFAULT GETDATE(), -- Timestamp of posting
    
    -- Relationships
    CONSTRAINT FK_Announcements_Societies FOREIGN KEY (SocietyID) REFERENCES Societies(SocietyID),
    CONSTRAINT FK_Announcements_Users FOREIGN KEY (PostedByUserID) REFERENCES Users(UserID)
);
GO

-- ============================================================
-- SEED DATA (For Testing)
-- ============================================================

-- Admin: admin@fast.edu.pk | admin123
INSERT INTO Users (FullName, Email, Password, Role)
VALUES ('System Admin', 'admin@fast.edu.pk', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Admin');

-- Student: ali@fast.edu.pk | hello
INSERT INTO Users (FullName, Email, Password, Role)
VALUES ('Ali Hassan', 'ali@fast.edu.pk', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Student');
GO
