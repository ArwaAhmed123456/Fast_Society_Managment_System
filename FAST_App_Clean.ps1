# FAST University Societies Management System - Clean & Simple Design
# Working login, signup, and dashboard forms

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# Simple, Professional Color Scheme
$Colors = @{
    Primary = [System.Drawing.Color]::FromArgb(41, 98, 255)
    Secondary = [System.Drawing.Color]::FromArgb(117, 117, 117)
    Background = [System.Drawing.Color]::FromArgb(248, 249, 250)
    White = [System.Drawing.Color]::White
    Black = [System.Drawing.Color]::FromArgb(33, 33, 33)
    Success = [System.Drawing.Color]::FromArgb(76, 175, 80)
    Error = [System.Drawing.Color]::FromArgb(239, 68, 68)
    Border = [System.Drawing.Color]::FromArgb(224, 224, 224)
}

# Professional Fonts
$FontTitle = New-Object System.Drawing.Font("Segoe UI", 18, [System.Drawing.FontStyle]::Bold)
$FontHeader = New-Object System.Drawing.Font("Segoe UI", 14, [System.Drawing.FontStyle]::SemiBold)
$FontNormal = New-Object System.Drawing.Font("Segoe UI", 11, [System.Drawing.FontStyle]::Regular)
$FontButton = New-Object System.Drawing.Font("Segoe UI", 12, [System.Drawing.FontStyle]::Medium)

# Main Form
$mainForm = New-Object System.Windows.Forms.Form
$mainForm.Text = "FAST University - Societies Management System"
$mainForm.Size = "1000,700"
$mainForm.StartPosition = "CenterScreen"
$mainForm.BackColor = $Colors.Background
$mainForm.MinimumSize = "800,600"

# Header Panel
$headerPanel = New-Object System.Windows.Forms.Panel
$headerPanel.Size = "1000,80"
$headerPanel.Location = "0,0"
$headerPanel.BackColor = $Colors.Primary
$headerPanel.Dock = "Top"
$mainForm.Controls.Add($headerPanel)

# Load FAST Logo
$logoPath = Join-Path $PSScriptRoot "fast_Logo.png"
$logoPictureBox = New-Object System.Windows.Forms.PictureBox
if (Test-Path $logoPath) {
    try {
        $logoImage = [System.Drawing.Image]::FromFile($logoPath)
        $logoPictureBox.Image = $logoImage
        $logoPictureBox.SizeMode = "Zoom"
    } catch {
        Write-Host "Logo not loaded" -ForegroundColor Yellow
    }
}
$logoPictureBox.Size = "60,60"
$logoPictureBox.Location = "20,10"
$headerPanel.Controls.Add($logoPictureBox)

# Title
$titleLabel = New-Object System.Windows.Forms.Label
$titleLabel.Text = "FAST UNIVERSITY"
$titleLabel.Font = $FontTitle
$titleLabel.ForeColor = $Colors.White
$titleLabel.Location = "90,15"
$titleLabel.Size = "300,30"
$headerPanel.Controls.Add($titleLabel)

$subtitleLabel = New-Object System.Windows.Forms.Label
$subtitleLabel.Text = "Societies Management System"
$subtitleLabel.Font = $FontNormal
$subtitleLabel.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$subtitleLabel.Location = "90,45"
$subtitleLabel.Size = "300,20"
$headerPanel.Controls.Add($subtitleLabel)

# User Info Panel
$userPanel = New-Object System.Windows.Forms.Panel
$userPanel.Size = "200,80"
$userPanel.Location = "800,0"
$userPanel.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($userPanel)

$userNameLabel = New-Object System.Windows.Forms.Label
$userNameLabel.Text = "Guest User"
$userNameLabel.Font = $FontHeader
$userNameLabel.ForeColor = $Colors.White
$userNameLabel.Location = "0,15"
$userNameLabel.Size = "200,25"
$userNameLabel.TextAlign = "MiddleRight"
$userPanel.Controls.Add($userNameLabel)

$userRoleLabel = New-Object System.Windows.Forms.Label
$userRoleLabel.Text = "Not logged in"
$userRoleLabel.Font = $FontNormal
$userRoleLabel.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$userRoleLabel.Location = "0,40"
$userRoleLabel.Size = "200,20"
$userRoleLabel.TextAlign = "MiddleRight"
$userPanel.Controls.Add($userRoleLabel)

# Logout Button
$logoutButton = New-Object System.Windows.Forms.Button
$logoutButton.Text = "Logout"
$logoutButton.Size = "80,30"
$logoutButton.Location = "120,45"
$logoutButton.BackColor = $Colors.Error
$logoutButton.ForeColor = $Colors.White
$logoutButton.Font = $FontButton
$logoutButton.FlatStyle = "Flat"
$logoutButton.FlatAppearance.BorderSize = 0
$userPanel.Controls.Add($logoutButton)

# Main Content Panel
$contentPanel = New-Object System.Windows.Forms.Panel
$contentPanel.Size = "1000,620"
$contentPanel.Location = "0,80"
$contentPanel.BackColor = $Colors.Background
$contentPanel.Dock = "Fill"
$mainForm.Controls.Add($contentPanel)

# Tab Control
$tabControl = New-Object System.Windows.Forms.TabControl
$tabControl.Size = "960,580"
$tabControl.Location = "20,20"
$contentPanel.Controls.Add($tabControl)

# Login Tab
$loginTab = New-Object System.Windows.Forms.TabPage
$loginTab.Text = "Login"
$loginTab.BackColor = $Colors.White
$tabControl.TabPages.Add($loginTab)

# Login Container
$loginContainer = New-Object System.Windows.Forms.Panel
$loginContainer.Size = "400,500"
$loginContainer.Location = "280,40"
$loginContainer.BackColor = $Colors.White
$loginContainer.BorderStyle = "FixedSingle"
$loginTab.Controls.Add($loginContainer)

# Login Title
$loginTitle = New-Object System.Windows.Forms.Label
$loginTitle.Text = "Sign In"
$loginTitle.Font = $FontTitle
$loginTitle.ForeColor = $Colors.Black
$loginTitle.Location = "30,30"
$loginTitle.Size = "340,30"
$loginTitle.TextAlign = "MiddleCenter"
$loginContainer.Controls.Add($loginTitle)

# Email Field
$emailLabel = New-Object System.Windows.Forms.Label
$emailLabel.Text = "Email Address"
$emailLabel.Font = $FontNormal
$emailLabel.ForeColor = $Colors.Black
$emailLabel.Location = "30,80"
$emailLabel.Size = "340,20"
$loginContainer.Controls.Add($emailLabel)

$emailTextBox = New-Object System.Windows.Forms.TextBox
$emailTextBox.Size = "340,35"
$emailTextBox.Location = "30,105"
$emailTextBox.Font = $FontNormal
$emailTextBox.BorderStyle = "FixedSingle"
$loginContainer.Controls.Add($emailTextBox)

# Password Field
$passwordLabel = New-Object System.Windows.Forms.Label
$passwordLabel.Text = "Password"
$passwordLabel.Font = $FontNormal
$passwordLabel.ForeColor = $Colors.Black
$passwordLabel.Location = "30,160"
$passwordLabel.Size = "340,20"
$loginContainer.Controls.Add($passwordLabel)

$passwordTextBox = New-Object System.Windows.Forms.TextBox
$passwordTextBox.Size = "340,35"
$passwordTextBox.Location = "30,185"
$passwordTextBox.Font = $FontNormal
$passwordTextBox.UseSystemPasswordChar = $true
$passwordTextBox.BorderStyle = "FixedSingle"
$loginContainer.Controls.Add($passwordTextBox)

# Remember Me
$rememberCheckBox = New-Object System.Windows.Forms.CheckBox
$rememberCheckBox.Text = "Remember me"
$rememberCheckBox.Location = "30,240"
$rememberCheckBox.Size = "150,20"
$rememberCheckBox.Font = $FontNormal
$loginContainer.Controls.Add($rememberCheckBox)

# Forgot Password
$forgotLink = New-Object System.Windows.Forms.LinkLabel
$forgotLink.Text = "Forgot password?"
$forgotLink.Location = "200,240"
$forgotLink.Size = "170,20"
$forgotLink.Font = $FontNormal
$forgotLink.LinkColor = $Colors.Primary
$loginContainer.Controls.Add($forgotLink)

# Login Button
$loginButton = New-Object System.Windows.Forms.Button
$loginButton.Text = "Sign In"
$loginButton.Size = "340,45"
$loginButton.Location = "30,280"
$loginButton.BackColor = $Colors.Primary
$loginButton.ForeColor = $Colors.White
$loginButton.Font = $FontButton
$loginButton.FlatStyle = "Flat"
$loginButton.FlatAppearance.BorderSize = 0
$loginContainer.Controls.Add($loginButton)

# Register Button
$registerButton = New-Object System.Windows.Forms.Button
$registerButton.Text = "Create Account"
$registerButton.Size = "340,45"
$registerButton.Location = "30,340"
$registerButton.BackColor = $Colors.White
$registerButton.ForeColor = $Colors.Primary
$registerButton.Font = $FontButton
$registerButton.FlatStyle = "Flat"
$registerButton.FlatAppearance.BorderSize = 2
$registerButton.FlatAppearance.BorderColor = $Colors.Primary
$loginContainer.Controls.Add($registerButton)

# Status Label
$statusLabel = New-Object System.Windows.Forms.Label
$statusLabel.Location = "30,400"
$statusLabel.Size = "340,20"
$statusLabel.Font = $FontNormal
$statusLabel.TextAlign = "MiddleCenter"
$loginContainer.Controls.Add($statusLabel)

# Signup Tab
$signupTab = New-Object System.Windows.Forms.TabPage
$signupTab.Text = "Sign Up"
$signupTab.BackColor = $Colors.White
$tabControl.TabPages.Add($signupTab)

# Signup Container
$signupContainer = New-Object System.Windows.Forms.Panel
$signupContainer.Size = "450,550"
$signupContainer.Location = "255,40"
$signupContainer.BackColor = $Colors.White
$signupContainer.BorderStyle = "FixedSingle"
$signupTab.Controls.Add($signupContainer)

# Signup Title
$signupTitle = New-Object System.Windows.Forms.Label
$signupTitle.Text = "Create Account"
$signupTitle.Font = $FontTitle
$signupTitle.ForeColor = $Colors.Black
$signupTitle.Location = "30,30"
$signupTitle.Size = "390,30"
$signupTitle.TextAlign = "MiddleCenter"
$signupContainer.Controls.Add($signupTitle)

# Signup Fields
$yPos = 80

$fields = @(
    @{Label="Full Name"; Name="name"},
    @{Label="Student ID"; Name="studentId"},
    @{Label="Email Address"; Name="email"},
    @{Label="Department"; Name="department"},
    @{Label="Password"; Name="password"; IsPassword=$true},
    @{Label="Confirm Password"; Name="confirm"; IsPassword=$true}
)

$textBoxes = @{}
foreach ($field in $fields) {
    $label = New-Object System.Windows.Forms.Label
    $label.Text = $field.Label + ":"
    $label.Location = "30,$yPos"
    $label.Size = "410,20"
    $label.Font = $FontNormal
    $label.ForeColor = $Colors.Black
    $signupContainer.Controls.Add($label)
    
    $textBox = New-Object System.Windows.Forms.TextBox
    $textBox.Location = "30,$($yPos + 25)"
    $textBox.Size = "390,35"
    $textBox.Font = $FontNormal
    if ($field.IsPassword) {
        $textBox.UseSystemPasswordChar = $true
    }
    $signupContainer.Controls.Add($textBox)
    $textBoxes[$field.Name] = $textBox
    
    $yPos += 70
}

# Signup Button
$signupButton = New-Object System.Windows.Forms.Button
$signupButton.Text = "Create Account"
$signupButton.Size = "390,45"
$signupButton.Location = "30,$yPos"
$signupButton.BackColor = $Colors.Success
$signupButton.ForeColor = $Colors.White
$signupButton.Font = $FontButton
$signupButton.FlatStyle = "Flat"
$signupButton.FlatAppearance.BorderSize = 0
$signupContainer.Controls.Add($signupButton)

# Signup Status
$signupStatusLabel = New-Object System.Windows.Forms.Label
$signupStatusLabel.Location = "30,$($yPos + 55)"
$signupStatusLabel.Size = "390,20"
$signupStatusLabel.Font = $FontNormal
$signupStatusLabel.TextAlign = "MiddleCenter"
$signupContainer.Controls.Add($signupStatusLabel)

# Dashboard Tab
$dashboardTab = New-Object System.Windows.Forms.TabPage
$dashboardTab.Text = "Dashboard"
$dashboardTab.BackColor = $Colors.White
$tabControl.TabPages.Add($dashboardTab)

# Dashboard Container
$dashboardContainer = New-Object System.Windows.Forms.Panel
$dashboardContainer.Size = "920,550"
$dashboardContainer.Location = "20,20"
$dashboardContainer.BackColor = $Colors.White
$dashboardTab.Controls.Add($dashboardContainer)

# Welcome Section
$welcomePanel = New-Object System.Windows.Forms.Panel
$welcomePanel.Size = "920,100"
$welcomePanel.BackColor = $Colors.Primary
$welcomePanel.Location = "0,0"
$dashboardContainer.Controls.Add($welcomePanel)

$welcomeLabel = New-Object System.Windows.Forms.Label
$welcomeLabel.Text = "Welcome to FAST University Dashboard"
$welcomeLabel.Font = $FontTitle
$welcomeLabel.ForeColor = $Colors.White
$welcomeLabel.Location = "20,30"
$welcomeLabel.Size = "880,40"
$welcomeLabel.TextAlign = "MiddleCenter"
$welcomePanel.Controls.Add($welcomeLabel)

$dashboardUserLabel = New-Object System.Windows.Forms.Label
$dashboardUserLabel.Text = "Logged in as: Guest"
$dashboardUserLabel.Font = $FontNormal
$dashboardUserLabel.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$dashboardUserLabel.Location = "20,70"
$dashboardUserLabel.Size = "880,20"
$dashboardUserLabel.TextAlign = "MiddleCenter"
$welcomePanel.Controls.Add($dashboardUserLabel)

# Stats Section
$statsPanel = New-Object System.Windows.Forms.Panel
$statsPanel.Size = "920,150"
$statsPanel.Location = "0,110"
$statsPanel.BackColor = $Colors.Background
$dashboardContainer.Controls.Add($statsPanel)

# Stats Cards
$stats = @(
    @{Title="Active Societies"; Value="12"; Color=$Colors.Primary},
    @{Title="Upcoming Events"; Value="8"; Color=$Colors.Success},
    @{Title="Your Memberships"; Value="3"; Color=$Colors.Primary},
    @{Title="Event Tickets"; Value="5"; Color=$Colors.Success}
)

$xPos = 20
foreach ($stat in $stats) {
    $statCard = New-Object System.Windows.Forms.Panel
    $statCard.Size = "200,120"
    $statCard.Location = "$xPos,15"
    $statCard.BackColor = $stat.Color
    $statsPanel.Controls.Add($statCard)
    
    $statValue = New-Object System.Windows.Forms.Label
    $statValue.Text = $stat.Value
    $statValue.Font = New-Object System.Drawing.Font("Segoe UI", 32, [System.Drawing.FontStyle]::Bold)
    $statValue.ForeColor = $Colors.White
    $statValue.Location = "0,20"
    $statValue.Size = "200,50"
    $statValue.TextAlign = "MiddleCenter"
    $statCard.Controls.Add($statValue)
    
    $statTitle = New-Object System.Windows.Forms.Label
    $statTitle.Text = $stat.Title
    $statTitle.Font = $FontNormal
    $statTitle.ForeColor = $Colors.White
    $statTitle.Location = "0,70"
    $statTitle.Size = "200,30"
    $statTitle.TextAlign = "MiddleCenter"
    $statCard.Controls.Add($statTitle)
    
    $xPos += 230
}

# Features Section
$featuresPanel = New-Object System.Windows.Forms.Panel
$featuresPanel.Size = "920,280"
$featuresPanel.Location = "0,270"
$featuresPanel.BackColor = $Colors.White
$dashboardContainer.Controls.Add($featuresPanel)

$featuresTitle = New-Object System.Windows.Forms.Label
$featuresTitle.Text = "System Features"
$featuresTitle.Font = $FontHeader
$featuresTitle.ForeColor = $Colors.Black
$featuresTitle.Location = "20,20"
$featuresTitle.Size = "880,30"
$featuresPanel.Controls.Add($featuresTitle)

$featuresList = New-Object System.Windows.Forms.Label
$featuresList.Text = "• Browse and join student societies`n• Register for upcoming events and activities`n• Manage your society memberships`n• View event tickets and confirmations`n• Track your involvement and contributions`n• Receive notifications and updates`n• Network with fellow students and faculty`n• Monitor society growth and engagement"
$featuresList.Font = $FontNormal
$featuresList.ForeColor = $Colors.Black
$featuresList.Location = "20,60"
$featuresList.Size = "880,200"
$featuresPanel.Controls.Add($featuresList)

# Event Handlers
$loginButton.Add_Click({
    $email = $emailTextBox.Text.Trim()
    $password = $passwordTextBox.Text.Trim()
    
    if ([string]::IsNullOrEmpty($email) -or [string]::IsNullOrEmpty($password)) {
        $statusLabel.Text = "Please enter both email and password"
        $statusLabel.ForeColor = $Colors.Error
    } elseif ($email -eq "admin@fast.edu.pk" -and $password -eq "admin123") {
        $statusLabel.Text = "Login successful! Welcome Administrator."
        $statusLabel.ForeColor = $Colors.Success
        $userNameLabel.Text = "Administrator"
        $userRoleLabel.Text = "System Administrator"
        $dashboardUserLabel.Text = "Logged in as: Administrator (admin@fast.edu.pk)"
        $tabControl.SelectedTab = $dashboardTab
    } elseif ($email -eq "ali@fast.edu.pk" -and $password -eq "hello") {
        $statusLabel.Text = "Login successful! Welcome Student."
        $statusLabel.ForeColor = $Colors.Success
        $userNameLabel.Text = "Ali Student"
        $userRoleLabel.Text = "Computer Science"
        $dashboardUserLabel.Text = "Logged in as: Student (ali@fast.edu.pk)"
        $tabControl.SelectedTab = $dashboardTab
    } else {
        $statusLabel.Text = "Invalid credentials. Please try again."
        $statusLabel.ForeColor = $Colors.Error
    }
})

$registerButton.Add_Click({
    $name = $textBoxes["name"].Text.Trim()
    $email = $textBoxes["email"].Text.Trim()
    $password = $textBoxes["password"].Text
    $confirm = $textBoxes["confirm"].Text
    
    if ([string]::IsNullOrEmpty($name) -or [string]::IsNullOrEmpty($email) -or [string]::IsNullOrEmpty($password)) {
        $signupStatusLabel.Text = "Please fill all required fields."
        $signupStatusLabel.ForeColor = $Colors.Error
    } elseif ($password -ne $confirm) {
        $signupStatusLabel.Text = "Passwords do not match."
        $signupStatusLabel.ForeColor = $Colors.Error
    } elseif ($password.Length -lt 6) {
        $signupStatusLabel.Text = "Password must be at least 6 characters."
        $signupStatusLabel.ForeColor = $Colors.Error
    } else {
        $signupStatusLabel.Text = "Registration successful! You can now login."
        $signupStatusLabel.ForeColor = $Colors.Success
        $statusLabel.Text = "Registration successful! Please login with your new account."
        $statusLabel.ForeColor = $Colors.Success
        $tabControl.SelectedTab = $loginTab
    }
})

$forgotLink.Add_LinkClicked({
    $statusLabel.Text = "Please contact IT Support at it.support@fast.edu.pk"
    $statusLabel.ForeColor = $Colors.Primary
})

$logoutButton.Add_Click({
    $emailTextBox.Text = ""
    $passwordTextBox.Text = ""
    $statusLabel.Text = ""
    $userNameLabel.Text = "Guest User"
    $userRoleLabel.Text = "Not logged in"
    $dashboardUserLabel.Text = "Logged in as: Guest"
    $rememberCheckBox.Checked = $false
    $tabControl.SelectedTab = $loginTab
})

# Button hover effects
$loginButton.Add_MouseEnter({
    $this.BackColor = [System.Drawing.Color]::FromArgb(30, 64, 175)
})
$loginButton.Add_MouseLeave({
    $this.BackColor = $Colors.Primary
})

$registerButton.Add_MouseEnter({
    $this.BackColor = [System.Drawing.Color]::FromArgb(41, 98, 255, 20)
})
$registerButton.Add_MouseLeave({
    $this.BackColor = $Colors.White
})

$signupButton.Add_MouseEnter({
    $this.BackColor = [System.Drawing.Color]::FromArgb(56, 142, 60)
})
$signupButton.Add_MouseLeave({
    $this.BackColor = $Colors.Success
})

# Show the application
Write-Host "Starting FAST University Societies Management System..." -ForegroundColor Green
Write-Host "Features:" -ForegroundColor Yellow
Write-Host "  • Clean, professional login form" -ForegroundColor White
Write-Host "  • Complete signup form with validation" -ForegroundColor White
Write-Host "  • Interactive dashboard with statistics" -ForegroundColor White
Write-Host "  • FAST University branding" -ForegroundColor White
Write-Host "  • Simple, functional design" -ForegroundColor White
Write-Host ""
Write-Host "Login Credentials:" -ForegroundColor Cyan
Write-Host "  Admin: admin@fast.edu.pk / admin123" -ForegroundColor White
Write-Host "  Student: ali@fast.edu.pk / hello" -ForegroundColor White
Write-Host ""

$mainForm.ShowDialog() | Out-Null

Write-Host "Application closed successfully." -ForegroundColor Green
