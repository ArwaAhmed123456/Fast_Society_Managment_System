# FAST University Societies Management System - Fixed Layout
# Proper sizing and border management

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# Professional Color Scheme
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
$FontTitle = New-Object System.Drawing.Font("Segoe UI", 16, [System.Drawing.FontStyle]::Bold)
$FontHeader = New-Object System.Drawing.Font("Segoe UI", 12, [System.Drawing.FontStyle]::SemiBold)
$FontNormal = New-Object System.Drawing.Font("Segoe UI", 10, [System.Drawing.FontStyle]::Regular)
$FontButton = New-Object System.Drawing.Font("Segoe UI", 11, [System.Drawing.FontStyle]::Medium)

# Main Form - Fixed Size
$mainForm = New-Object System.Windows.Forms.Form
$mainForm.Text = "FAST University - Societies Management System"
$mainForm.Size = "900,650"
$mainForm.StartPosition = "CenterScreen"
$mainForm.BackColor = $Colors.Background
$mainForm.MinimumSize = "800,600"
$mainForm.MaximumSize = "1200,800"
$mainForm.FormBorderStyle = "Sizable"

# Header Panel - Proper sizing
$headerPanel = New-Object System.Windows.Forms.Panel
$headerPanel.Size = "890,70"
$headerPanel.Location = "5,5"
$headerPanel.BackColor = $Colors.Primary
$mainForm.Controls.Add($headerPanel)

# Load FAST Logo - Proper sizing
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
$logoPictureBox.Size = "50,50"
$logoPictureBox.Location = "10,10"
$headerPanel.Controls.Add($logoPictureBox)

# Title - Proper positioning
$titleLabel = New-Object System.Windows.Forms.Label
$titleLabel.Text = "FAST UNIVERSITY"
$titleLabel.Font = $FontTitle
$titleLabel.ForeColor = $Colors.White
$titleLabel.Location = "70,10"
$titleLabel.Size = "300,25"
$headerPanel.Controls.Add($titleLabel)

$subtitleLabel = New-Object System.Windows.Forms.Label
$subtitleLabel.Text = "Societies Management System"
$subtitleLabel.Font = $FontNormal
$subtitleLabel.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$subtitleLabel.Location = "70,35"
$subtitleLabel.Size = "300,20"
$headerPanel.Controls.Add($subtitleLabel)

# User Info Panel - Proper positioning
$userPanel = New-Object System.Windows.Forms.Panel
$userPanel.Size = "180,70"
$userPanel.Location = "700,0"
$userPanel.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($userPanel)

$userNameLabel = New-Object System.Windows.Forms.Label
$userNameLabel.Text = "Guest User"
$userNameLabel.Font = $FontHeader
$userNameLabel.ForeColor = $Colors.White
$userNameLabel.Location = "0,10"
$userNameLabel.Size = "180,25"
$userNameLabel.TextAlign = "MiddleRight"
$userPanel.Controls.Add($userNameLabel)

$userRoleLabel = New-Object System.Windows.Forms.Label
$userRoleLabel.Text = "Not logged in"
$userRoleLabel.Font = $FontNormal
$userRoleLabel.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$userRoleLabel.Location = "0,35"
$userRoleLabel.Size = "180,20"
$userRoleLabel.TextAlign = "MiddleRight"
$userPanel.Controls.Add($userRoleLabel)

# Logout Button - Smaller size
$logoutButton = New-Object System.Windows.Forms.Button
$logoutButton.Text = "Logout"
$logoutButton.Size = "70,25"
$logoutButton.Location = "110,42"
$logoutButton.BackColor = $Colors.Error
$logoutButton.ForeColor = $Colors.White
$logoutButton.Font = $FontButton
$logoutButton.FlatStyle = "Flat"
$logoutButton.FlatAppearance.BorderSize = 0
$userPanel.Controls.Add($logoutButton)

# Main Content Panel - Proper sizing
$contentPanel = New-Object System.Windows.Forms.Panel
$contentPanel.Size = "890,570"
$contentPanel.Location = "5,80"
$contentPanel.BackColor = $Colors.Background
$mainForm.Controls.Add($contentPanel)

# Tab Control - Fixed size
$tabControl = New-Object System.Windows.Forms.TabControl
$tabControl.Size = "880,560"
$tabControl.Location = "5,5"
$contentPanel.Controls.Add($tabControl)

# Login Tab
$loginTab = New-Object System.Windows.Forms.TabPage
$loginTab.Text = "Login"
$loginTab.BackColor = $Colors.White
$tabControl.TabPages.Add($loginTab)

# Login Container - Centered and properly sized
$loginContainer = New-Object System.Windows.Forms.Panel
$loginContainer.Size = "350,450"
$loginContainer.Location = "240,50"
$loginContainer.BackColor = $Colors.White
$loginContainer.BorderStyle = "FixedSingle"
$loginTab.Controls.Add($loginContainer)

# Login Title
$loginTitle = New-Object System.Windows.Forms.Label
$loginTitle.Text = "Sign In"
$loginTitle.Font = $FontTitle
$loginTitle.ForeColor = $Colors.Black
$loginTitle.Location = "20,20"
$loginTitle.Size = "310,30"
$loginTitle.TextAlign = "MiddleCenter"
$loginContainer.Controls.Add($loginTitle)

# Email Field
$emailLabel = New-Object System.Windows.Forms.Label
$emailLabel.Text = "Email Address"
$emailLabel.Font = $FontNormal
$emailLabel.ForeColor = $Colors.Black
$emailLabel.Location = "20,70"
$emailLabel.Size = "310,20"
$loginContainer.Controls.Add($emailLabel)

$emailTextBox = New-Object System.Windows.Forms.TextBox
$emailTextBox.Size = "310,30"
$emailTextBox.Location = "20,95"
$emailTextBox.Font = $FontNormal
$emailTextBox.BorderStyle = "FixedSingle"
$loginContainer.Controls.Add($emailTextBox)

# Password Field
$passwordLabel = New-Object System.Windows.Forms.Label
$passwordLabel.Text = "Password"
$passwordLabel.Font = $FontNormal
$passwordLabel.ForeColor = $Colors.Black
$passwordLabel.Location = "20,140"
$passwordLabel.Size = "310,20"
$loginContainer.Controls.Add($passwordLabel)

$passwordTextBox = New-Object System.Windows.Forms.TextBox
$passwordTextBox.Size = "310,30"
$passwordTextBox.Location = "20,165"
$passwordTextBox.Font = $FontNormal
$passwordTextBox.UseSystemPasswordChar = $true
$passwordTextBox.BorderStyle = "FixedSingle"
$loginContainer.Controls.Add($passwordTextBox)

# Remember Me
$rememberCheckBox = New-Object System.Windows.Forms.CheckBox
$rememberCheckBox.Text = "Remember me"
$rememberCheckBox.Location = "20,210"
$rememberCheckBox.Size = "120,20"
$rememberCheckBox.Font = $FontNormal
$loginContainer.Controls.Add($rememberCheckBox)

# Forgot Password
$forgotLink = New-Object System.Windows.Forms.LinkLabel
$forgotLink.Text = "Forgot password?"
$forgotLink.Location = "150,210"
$forgotLink.Size = "180,20"
$forgotLink.Font = $FontNormal
$forgotLink.LinkColor = $Colors.Primary
$loginContainer.Controls.Add($forgotLink)

# Login Button
$loginButton = New-Object System.Windows.Forms.Button
$loginButton.Text = "Sign In"
$loginButton.Size = "310,40"
$loginButton.Location = "20,250"
$loginButton.BackColor = $Colors.Primary
$loginButton.ForeColor = $Colors.White
$loginButton.Font = $FontButton
$loginButton.FlatStyle = "Flat"
$loginButton.FlatAppearance.BorderSize = 0
$loginContainer.Controls.Add($loginButton)

# Register Button
$registerButton = New-Object System.Windows.Forms.Button
$registerButton.Text = "Create Account"
$registerButton.Size = "310,40"
$registerButton.Location = "20,300"
$registerButton.BackColor = $Colors.White
$registerButton.ForeColor = $Colors.Primary
$registerButton.Font = $FontButton
$registerButton.FlatStyle = "Flat"
$registerButton.FlatAppearance.BorderSize = 2
$registerButton.FlatAppearance.BorderColor = $Colors.Primary
$loginContainer.Controls.Add($registerButton)

# Status Label
$statusLabel = New-Object System.Windows.Forms.Label
$statusLabel.Location = "20,360"
$statusLabel.Size = "310,20"
$statusLabel.Font = $FontNormal
$statusLabel.TextAlign = "MiddleCenter"
$loginContainer.Controls.Add($statusLabel)

# Signup Tab
$signupTab = New-Object System.Windows.Forms.TabPage
$signupTab.Text = "Sign Up"
$signupTab.BackColor = $Colors.White
$tabControl.TabPages.Add($signupTab)

# Signup Container - Properly sized
$signupContainer = New-Object System.Windows.Forms.Panel
$signupContainer.Size = "400,480"
$signupContainer.Location = "220,35"
$signupContainer.BackColor = $Colors.White
$signupContainer.BorderStyle = "FixedSingle"
$signupTab.Controls.Add($signupContainer)

# Signup Title
$signupTitle = New-Object System.Windows.Forms.Label
$signupTitle.Text = "Create Account"
$signupTitle.Font = $FontTitle
$signupTitle.ForeColor = $Colors.Black
$signupTitle.Location = "20,20"
$signupTitle.Size = "360,30"
$signupTitle.TextAlign = "MiddleCenter"
$signupContainer.Controls.Add($signupTitle)

# Signup Fields - Compact layout
$yPos = 70

$fields = @(
    @{Label="Full Name"; Name="name"},
    @{Label="Student ID"; Name="studentId"},
    @{Label="Email"; Name="email"},
    @{Label="Department"; Name="department"},
    @{Label="Password"; Name="password"; IsPassword=$true},
    @{Label="Confirm"; Name="confirm"; IsPassword=$true}
)

$textBoxes = @{}
foreach ($field in $fields) {
    $label = New-Object System.Windows.Forms.Label
    $label.Text = $field.Label + ":"
    $label.Location = "20,$yPos"
    $label.Size = "360,20"
    $label.Font = $FontNormal
    $label.ForeColor = $Colors.Black
    $signupContainer.Controls.Add($label)
    
    $textBox = New-Object System.Windows.Forms.TextBox
    $textBox.Location = "20,$($yPos + 25)"
    $textBox.Size = "360,30"
    $textBox.Font = $FontNormal
    if ($field.IsPassword) {
        $textBox.UseSystemPasswordChar = $true
    }
    $signupContainer.Controls.Add($textBox)
    $textBoxes[$field.Name] = $textBox
    
    $yPos += 65
}

# Signup Button
$signupButton = New-Object System.Windows.Forms.Button
$signupButton.Text = "Create Account"
$signupButton.Size = "360,40"
$signupButton.Location = "20,$yPos"
$signupButton.BackColor = $Colors.Success
$signupButton.ForeColor = $Colors.White
$signupButton.Font = $FontButton
$signupButton.FlatStyle = "Flat"
$signupButton.FlatAppearance.BorderSize = 0
$signupContainer.Controls.Add($signupButton)

# Signup Status
$signupStatusLabel = New-Object System.Windows.Forms.Label
$signupStatusLabel.Location = "20,$($yPos + 50)"
$signupStatusLabel.Size = "360,20"
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
$dashboardContainer.Size = "860,530"
$dashboardContainer.Location = "10,10"
$dashboardContainer.BackColor = $Colors.White
$dashboardTab.Controls.Add($dashboardContainer)

# Welcome Section
$welcomePanel = New-Object System.Windows.Forms.Panel
$welcomePanel.Size = "860,80"
$welcomePanel.BackColor = $Colors.Primary
$welcomePanel.Location = "0,0"
$dashboardContainer.Controls.Add($welcomePanel)

$welcomeLabel = New-Object System.Windows.Forms.Label
$welcomeLabel.Text = "Welcome to FAST University Dashboard"
$welcomeLabel.Font = $FontTitle
$welcomeLabel.ForeColor = $Colors.White
$welcomeLabel.Location = "20,25"
$welcomeLabel.Size = "820,30"
$welcomeLabel.TextAlign = "MiddleCenter"
$welcomePanel.Controls.Add($welcomeLabel)

$dashboardUserLabel = New-Object System.Windows.Forms.Label
$dashboardUserLabel.Text = "Logged in as: Guest"
$dashboardUserLabel.Font = $FontNormal
$dashboardUserLabel.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$dashboardUserLabel.Location = "20,50"
$dashboardUserLabel.Size = "820,20"
$dashboardUserLabel.TextAlign = "MiddleCenter"
$welcomePanel.Controls.Add($dashboardUserLabel)

# Stats Section
$statsPanel = New-Object System.Windows.Forms.Panel
$statsPanel.Size = "860,120"
$statsPanel.Location = "0,90"
$statsPanel.BackColor = $Colors.Background
$dashboardContainer.Controls.Add($statsPanel)

# Stats Cards - Smaller size
$stats = @(
    @{Title="Societies"; Value="12"; Color=$Colors.Primary},
    @{Title="Events"; Value="8"; Color=$Colors.Success},
    @{Title="Memberships"; Value="3"; Color=$Colors.Primary},
    @{Title="Tickets"; Value="5"; Color=$Colors.Success}
)

$xPos = 20
foreach ($stat in $stats) {
    $statCard = New-Object System.Windows.Forms.Panel
    $statCard.Size = "180,90"
    $statCard.Location = "$xPos,15"
    $statCard.BackColor = $stat.Color
    $statsPanel.Controls.Add($statCard)
    
    $statValue = New-Object System.Windows.Forms.Label
    $statValue.Text = $stat.Value
    $statValue.Font = New-Object System.Drawing.Font("Segoe UI", 24, [System.Drawing.FontStyle]::Bold)
    $statValue.ForeColor = $Colors.White
    $statValue.Location = "0,15"
    $statValue.Size = "180,35"
    $statValue.TextAlign = "MiddleCenter"
    $statCard.Controls.Add($statValue)
    
    $statTitle = New-Object System.Windows.Forms.Label
    $statTitle.Text = $stat.Title
    $statTitle.Font = $FontNormal
    $statTitle.ForeColor = $Colors.White
    $statTitle.Location = "0,50"
    $statTitle.Size = "180,25"
    $statTitle.TextAlign = "MiddleCenter"
    $statCard.Controls.Add($statTitle)
    
    $xPos += 210
}

# Features Section
$featuresPanel = New-Object System.Windows.Forms.Panel
$featuresPanel.Size = "860,300"
$featuresPanel.Location = "0,220"
$featuresPanel.BackColor = $Colors.White
$dashboardContainer.Controls.Add($featuresPanel)

$featuresTitle = New-Object System.Windows.Forms.Label
$featuresTitle.Text = "System Features"
$featuresTitle.Font = $FontHeader
$featuresTitle.ForeColor = $Colors.Black
$featuresTitle.Location = "20,20"
$featuresTitle.Size = "820,25"
$featuresPanel.Controls.Add($featuresTitle)

$featuresList = New-Object System.Windows.Forms.Label
$featuresList.Text = "• Browse and join student societies`n• Register for upcoming events and activities`n• Manage your society memberships`n• View event tickets and confirmations`n• Track your involvement and contributions`n• Receive notifications and updates`n• Network with fellow students and faculty"
$featuresList.Font = $FontNormal
$featuresList.ForeColor = $Colors.Black
$featuresList.Location = "20,55"
$featuresList.Size = "820,200"
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
        $userRoleLabel.Text = "System Admin"
        $dashboardUserLabel.Text = "Logged in as: Administrator"
        $tabControl.SelectedTab = $dashboardTab
    } elseif ($email -eq "ali@fast.edu.pk" -and $password -eq "hello") {
        $statusLabel.Text = "Login successful! Welcome Student."
        $statusLabel.ForeColor = $Colors.Success
        $userNameLabel.Text = "Ali Student"
        $userRoleLabel.Text = "Computer Science"
        $dashboardUserLabel.Text = "Logged in as: Student"
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
        $statusLabel.Text = "Registration successful! Please login."
        $statusLabel.ForeColor = $Colors.Success
        $tabControl.SelectedTab = $loginTab
    }
})

$forgotLink.Add_LinkClicked({
    $statusLabel.Text = "Please contact IT Support for password reset"
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
Write-Host "Fixed Layout Features:" -ForegroundColor Yellow
Write-Host "  • Proper form sizing and borders" -ForegroundColor White
Write-Host "  • Elements fit within window" -ForegroundColor White
Write-Host "  • Responsive layout" -ForegroundColor White
Write-Host "  • Clean, professional design" -ForegroundColor White
Write-Host ""
Write-Host "Login Credentials:" -ForegroundColor Cyan
Write-Host "  Admin: admin@fast.edu.pk / admin123" -ForegroundColor White
Write-Host "  Student: ali@fast.edu.pk / hello" -ForegroundColor White
Write-Host ""

$mainForm.ShowDialog() | Out-Null

Write-Host "Application closed successfully." -ForegroundColor Green
