-- ============================================================
-- FAST Societies Management System
-- SQL Server Database Schema
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
-- TABLE: Users (Base table for all user types)
-- ============================================================
CREATE TABLE Users (
    UserID      INT IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(150) NOT NULL UNIQUE,
    Password    NVARCHAR(255) NOT NULL,   -- stored as SHA-256 hash
    Role        NVARCHAR(20)  NOT NULL CHECK (Role IN ('Student','SocietyHead','Member','Admin')),
    IsActive    BIT           NOT NULL DEFAULT 1,
    CreatedAt   DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- TABLE: Societies
-- ============================================================
CREATE TABLE Societies (
    SocietyID       INT IDENTITY(1,1) PRIMARY KEY,
    Name            NVARCHAR(100) NOT NULL UNIQUE,
    Description     NVARCHAR(500),
    HeadUserID      INT REFERENCES Users(UserID),
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Pending'
                        CHECK (Status IN ('Pending','Active','Suspended','Deleted')),
    CreatedAt       DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- TABLE: Memberships
-- ============================================================
CREATE TABLE Memberships (
    MembershipID    INT IDENTITY(1,1) PRIMARY KEY,
    UserID          INT NOT NULL REFERENCES Users(UserID),
    SocietyID       INT NOT NULL REFERENCES Societies(SocietyID),
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Pending'
                        CHECK (Status IN ('Pending','Approved','Rejected')),
    RequestedAt     DATETIME NOT NULL DEFAULT GETDATE(),
    RespondedAt     DATETIME,
    CONSTRAINT UQ_Membership UNIQUE (UserID, SocietyID)
);
GO

-- ============================================================
-- TABLE: Events
-- ============================================================
CREATE TABLE Events (
    EventID         INT IDENTITY(1,1) PRIMARY KEY,
    SocietyID       INT NOT NULL REFERENCES Societies(SocietyID),
    Title           NVARCHAR(150) NOT NULL,
    Description     NVARCHAR(1000),
    Location        NVARCHAR(200),
    EventDate       DATETIME NOT NULL,
    Capacity        INT NOT NULL DEFAULT 100,
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Pending'
                        CHECK (Status IN ('Pending','Approved','Cancelled')),
    CreatedAt       DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- TABLE: EventRegistrations
-- ============================================================
CREATE TABLE EventRegistrations (
    RegistrationID  INT IDENTITY(1,1) PRIMARY KEY,
    EventID         INT NOT NULL REFERENCES Events(EventID),
    UserID          INT NOT NULL REFERENCES Users(UserID),
    RegisteredAt    DATETIME NOT NULL DEFAULT GETDATE(),
    TicketCode      NVARCHAR(50) NOT NULL DEFAULT NEWID(),
    CONSTRAINT UQ_EventReg UNIQUE (EventID, UserID)
);
GO

-- ============================================================
-- TABLE: Tasks
-- ============================================================
CREATE TABLE Tasks (
    TaskID          INT IDENTITY(1,1) PRIMARY KEY,
    SocietyID       INT NOT NULL REFERENCES Societies(SocietyID),
    AssignedToUserID INT NOT NULL REFERENCES Users(UserID),
    AssignedByUserID INT NOT NULL REFERENCES Users(UserID),
    Title           NVARCHAR(200) NOT NULL,
    Description     NVARCHAR(500),
    DueDate         DATETIME,
    Status          NVARCHAR(20) NOT NULL DEFAULT 'Pending'
                        CHECK (Status IN ('Pending','InProgress','Completed')),
    CreatedAt       DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- TABLE: Announcements
-- ============================================================
CREATE TABLE Announcements (
    AnnouncementID  INT IDENTITY(1,1) PRIMARY KEY,
    SocietyID       INT REFERENCES Societies(SocietyID),  -- NULL = university-wide
    PostedByUserID  INT NOT NULL REFERENCES Users(UserID),
    Title           NVARCHAR(200) NOT NULL,
    Body            NVARCHAR(2000) NOT NULL,
    PostedAt        DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- SEED DATA
-- ============================================================

-- Admin user (password: admin123 -> SHA256)
INSERT INTO Users (FullName, Email, Password, Role)
VALUES ('System Admin', 'admin@fast.edu.pk',
        '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Admin');

-- Sample students
INSERT INTO Users (FullName, Email, Password, Role) VALUES
('Ali Hassan',    'ali@fast.edu.pk',    '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Student'),
('Sara Malik',   'sara@fast.edu.pk',   '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Student'),
('Usman Khan',   'usman@fast.edu.pk',  '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Student');

-- Societies
INSERT INTO Societies (Name, Description, HeadUserID, Status) VALUES
('Gaming Society',    'For gaming enthusiasts.',      2, 'Active'),
('Developers Club',   'Code, build, innovate.',       3, 'Active'),
('Literary Society',  'Books, poetry, debate.',       4, 'Active');
GO
