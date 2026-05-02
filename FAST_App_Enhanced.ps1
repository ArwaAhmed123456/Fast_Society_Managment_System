# FAST University Societies Management System - Enhanced with Logo & Better Flow
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# FAST University Official Colors from Logo
$FAST_Navy = [System.Drawing.Color]::FromArgb(0, 32, 96)        # Deep Navy Blue
$FAST_Blue = [System.Drawing.Color]::FromArgb(0, 102, 204)       # Primary Blue
$FAST_LightBlue = [System.Drawing.Color]::FromArgb(173, 216, 230) # Light Blue
$FAST_Gray = [System.Drawing.Color]::FromArgb(128, 128, 128)      # Medium Gray
$FAST_LightGray = [System.Drawing.Color]::FromArgb(245, 245, 245) # Light Gray
$FAST_White = [System.Drawing.Color]::White
$FAST_Green = [System.Drawing.Color]::FromArgb(0, 128, 0)         # Success Green
$FAST_Red = [System.Drawing.Color]::FromArgb(220, 53, 69)         # Error Red

# Main Application Form
$appForm = New-Object System.Windows.Forms.Form
$appForm.Text = "FAST University - Societies Management System"
$appForm.Size = "1200,800"
$appForm.StartPosition = "CenterScreen"
$appForm.BackColor = $FAST_LightGray
$appForm.MinimumSize = "1000,700"
$appForm.Font = New-Object System.Drawing.Font("Segoe UI", 10)

# Load FAST Logo
$logoPath = Join-Path $PSScriptRoot "fast_Logo.png"
$logoImage = $null
if (Test-Path $logoPath) {
    try {
        $logoImage = [System.Drawing.Image]::FromFile($logoPath)
    } catch {
        Write-Host "Could not load logo, using placeholder" -ForegroundColor Yellow
    }
}

# Header Panel with Gradient Effect
$headerPanel = New-Object System.Windows.Forms.Panel
$headerPanel.Size = "1200,100"
$headerPanel.BackColor = $FAST_Navy
$headerPanel.Dock = "Top"
$appForm.Controls.Add($headerPanel)

# Logo PictureBox
$logoPictureBox = New-Object System.Windows.Forms.PictureBox
if ($logoImage) {
    $logoPictureBox.Image = $logoImage
    $logoPictureBox.SizeMode = "Zoom"
} else {
    # Fallback colored panel
    $logoPictureBox.BackColor = $FAST_Blue
}
$logoPictureBox.Size = "80,80"
$logoPictureBox.Location = "20,10"
$logoPictureBox.BorderStyle = "None"
$headerPanel.Controls.Add($logoPictureBox)

# Title Section
$titleContainer = New-Object System.Windows.Forms.Panel
$titleContainer.Size = "400,80"
$titleContainer.Location = "110,10"
$titleContainer.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($titleContainer)

# Main Title
$title = New-Object System.Windows.Forms.Label
$title.Text = "FAST UNIVERSITY"
$title.Font = New-Object System.Drawing.Font("Segoe UI", 22, [System.Drawing.FontStyle]::Bold)
$title.ForeColor = $FAST_White
$title.Location = "0,5"
$title.Size = "400,35"
$titleContainer.Controls.Add($title)

# Subtitle
$subtitle = New-Object System.Windows.Forms.Label
$subtitle.Text = "Societies Management System"
$subtitle.Font = New-Object System.Drawing.Font("Segoe UI", 12)
$subtitle.ForeColor = $FAST_LightBlue
$subtitle.Location = "0,35"
$subtitle.Size = "400,25"
$titleContainer.Controls.Add($subtitle)

# User Info Panel
$userInfoPanel = New-Object System.Windows.Forms.Panel
$userInfoPanel.Size = "250,80"
$userInfoPanel.Location = "930,10"
$userInfoPanel.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($userInfoPanel)

# Current User Label
$currentUserLabel = New-Object System.Windows.Forms.Label
$currentUserLabel.Text = "Welcome, Guest"
$currentUserLabel.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$currentUserLabel.ForeColor = $FAST_LightBlue
$currentUserLabel.Location = "0,15"
$currentUserLabel.Size = "250,20"
$currentUserLabel.TextAlign = "MiddleRight"
$userInfoPanel.Controls.Add($currentUserLabel)

# Current Time Label
$timeLabel = New-Object System.Windows.Forms.Label
$timeLabel.Text = Get-Date -Format "yyyy-MM-dd HH:mm"
$timeLabel.Font = New-Object System.Drawing.Font("Segoe UI", 9)
$timeLabel.ForeColor = $FAST_White
$timeLabel.Location = "0,40"
$timeLabel.Size = "250,20"
$timeLabel.TextAlign = "MiddleRight"
$userInfoPanel.Controls.Add($timeLabel)

# Logout Button
$logoutButton = New-Object System.Windows.Forms.Button
$logoutButton.Text = "Logout"
$logoutButton.Size = "100,35"
$logoutButton.Location = "150,45"
$logoutButton.BackColor = $FAST_Red
$logoutButton.ForeColor = $FAST_White
$logoutButton.Font = New-Object System.Drawing.Font("Segoe UI", 10, [System.Drawing.FontStyle]::Bold)
$logoutButton.FlatStyle = "Flat"
$logoutButton.FlatAppearance.BorderSize = 0
$logoutButton.Cursor = "Hand"
$userInfoPanel.Controls.Add($logoutButton)

# Main Content Area with Sidebar
$mainContainer = New-Object System.Windows.Forms.Panel
$mainContainer.Size = "1200,700"
$mainContainer.Location = "0,100"
$mainContainer.BackColor = $FAST_White
$mainContainer.Dock = "Fill"
$appForm.Controls.Add($mainContainer)

# Sidebar Navigation
$sidebarPanel = New-Object System.Windows.Forms.Panel
$sidebarPanel.Size = "250,700"
$sidebarPanel.Location = "0,0"
$sidebarPanel.BackColor = $FAST_Navy
$sidebarPanel.Dock = "Left"
$mainContainer.Controls.Add($sidebarPanel)

# Sidebar Title
$sidebarTitle = New-Object System.Windows.Forms.Label
$sidebarTitle.Text = "NAVIGATION"
$sidebarTitle.Font = New-Object System.Drawing.Font("Segoe UI", 14, [System.Drawing.FontStyle]::Bold)
$sidebarTitle.ForeColor = $FAST_White
$sidebarTitle.Location = "20,20"
$sidebarTitle.Size = "210,30"
$sidebarPanel.Controls.Add($sidebarTitle)

# Navigation Buttons
$navButtons = @()
$navItems = @(
    @{Name="Login"; Icon=">"; Tab="Login"},
    @{Name="Dashboard"; Icon=">"; Tab="Dashboard"},
    @{Name="Societies"; Icon=">"; Tab="Societies"},
    @{Name="Events"; Icon=">"; Tab="Events"},
    @{Name="Memberships"; Icon=">"; Tab="Memberships"},
    @{Name="Profile"; Icon=">"; Tab="Profile"}
)

$yPos = 70
foreach ($item in $navItems) {
    $navButton = New-Object System.Windows.Forms.Button
    $navButton.Text = "$($item.Icon) $($item.Name)"
    $navButton.Size = "230,45"
    $navButton.Location = "10,$yPos"
    $navButton.BackColor = [System.Drawing.Color]::Transparent
    $navButton.ForeColor = $FAST_White
    $navButton.Font = New-Object System.Drawing.Font("Segoe UI", 11)
    $navButton.FlatStyle = "Flat"
    $navButton.TextAlign = "MiddleLeft"
    $navButton.Cursor = "Hand"
    $navButton.Tag = $item.Tab
    $sidebarPanel.Controls.Add($navButton)
    $navButtons += $navButton
    $yPos += 55
}

# Content Panel
$contentPanel = New-Object System.Windows.Forms.Panel
$contentPanel.Size = "950,700"
$contentPanel.Location = "250,0"
$contentPanel.BackColor = $FAST_White
$contentPanel.Dock = "Fill"
$mainContainer.Controls.Add($contentPanel)

# Tab Control for Content
$tabControl = New-Object System.Windows.Forms.TabControl
$tabControl.Size = "930,680"
$tabControl.Location = "10,10"
$tabControl.Font = New-Object System.Drawing.Font("Segoe UI", 11)
$tabControl.BackColor = $FAST_White
$contentPanel.Controls.Add($tabControl)

# Login Tab
$loginTab = New-Object System.Windows.Forms.TabPage
$loginTab.Text = "Login"
$loginTab.BackColor = $FAST_White
$loginTab.ForeColor = $FAST_Navy
$tabControl.TabPages.Add($loginTab)

# Login Container with Centering
$loginContainer = New-Object System.Windows.Forms.Panel
$loginContainer.Size = "930,680"
$loginContainer.BackColor = $FAST_White
$loginTab.Controls.Add($loginContainer)

# Login Card with Shadow Effect
$loginCard = New-Object System.Windows.Forms.Panel
$loginCard.Size = "450,550"
$loginCard.Location = "240,65"
$loginCard.BackColor = $FAST_White
$loginCard.BorderStyle = "None"
$loginContainer.Controls.Add($loginCard)

# Add shadow effect to card
$loginCard.Add_Paint({
    param($sender, $e)
    $graphics = $e.Graphics
    $rectangle = $sender.ClientRectangle
    $graphics.FillRectangle([System.Drawing.Drawing2D.LinearGradientBrush]::new(
        [System.Drawing.Point]::new(0, 0), 
        [System.Drawing.Point]::new(0, $rectangle.Height), 
        $FAST_LightGray, 
        $FAST_White), $rectangle)
})

# Login Header
$loginHeader = New-Object System.Windows.Forms.Panel
$loginHeader.Size = "450,80"
$loginHeader.BackColor = $FAST_Blue
$loginHeader.Location = "0,0"
$loginCard.Controls.Add($loginHeader)

$loginTitle = New-Object System.Windows.Forms.Label
$loginTitle.Text = "Welcome Back"
$loginTitle.Font = New-Object System.Drawing.Font("Segoe UI", 24, [System.Drawing.FontStyle]::Bold)
$loginTitle.ForeColor = $FAST_White
$loginTitle.Location = "30,20"
$loginTitle.Size = "390,40"
$loginTitle.TextAlign = "MiddleCenter"
$loginHeader.Controls.Add($loginTitle)

# Login Form Fields
$yPos = 100

# Email Field
$emailLabel = New-Object System.Windows.Forms.Label
$emailLabel.Text = "Email Address"
$emailLabel.Font = New-Object System.Drawing.Font("Segoe UI", 11, [System.Drawing.FontStyle]::Bold)
$emailLabel.ForeColor = $FAST_Navy
$emailLabel.Location = "30,$yPos"
$emailLabel.Size = "390,25"
$loginCard.Controls.Add($emailLabel)
$yPos += 30

$emailTextBox = New-Object System.Windows.Forms.TextBox
$emailTextBox.Size = "390,40"
$emailTextBox.Location = "30,$yPos"
$emailTextBox.Font = New-Object System.Drawing.Font("Segoe UI", 11)
$emailTextBox.BorderStyle = "FixedSingle"
$emailTextBox.BackColor = $FAST_White
$loginCard.Controls.Add($emailTextBox)
$yPos += 60

# Password Field
$passwordLabel = New-Object System.Windows.Forms.Label
$passwordLabel.Text = "Password"
$passwordLabel.Font = New-Object System.Drawing.Font("Segoe UI", 11, [System.Drawing.FontStyle]::Bold)
$passwordLabel.ForeColor = $FAST_Navy
$passwordLabel.Location = "30,$yPos"
$passwordLabel.Size = "390,25"
$loginCard.Controls.Add($passwordLabel)
$yPos += 30

$passwordTextBox = New-Object System.Windows.Forms.TextBox
$passwordTextBox.Size = "390,40"
$passwordTextBox.Location = "30,$yPos"
$passwordTextBox.Font = New-Object System.Drawing.Font("Segoe UI", 11)
$passwordTextBox.UseSystemPasswordChar = $true
$passwordTextBox.BorderStyle = "FixedSingle"
$passwordTextBox.BackColor = $FAST_White
$loginCard.Controls.Add($passwordTextBox)
$yPos += 60

# Remember Me Checkbox
$rememberCheckBox = New-Object System.Windows.Forms.CheckBox
$rememberCheckBox.Text = "Remember me"
$rememberCheckBox.Location = "30,$yPos"
$rememberCheckBox.Size = "150,25"
$rememberCheckBox.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$rememberCheckBox.ForeColor = $FAST_Navy
$loginCard.Controls.Add($rememberCheckBox)
$yPos += 35

# Forgot Password Link
$forgotLabel = New-Object System.Windows.Forms.Label
$forgotLabel.Text = "Forgot your password?"
$forgotLabel.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$forgotLabel.ForeColor = $FAST_Blue
$forgotLabel.Location = "30,$yPos"
$forgotLabel.Size = "200,25"
$forgotLabel.Cursor = "Hand"
$loginCard.Controls.Add($forgotLabel)
$yPos += 45

# Login Button
$loginButton = New-Object System.Windows.Forms.Button
$loginButton.Text = "Sign In"
$loginButton.Size = "390,50"
$loginButton.Location = "30,$yPos"
$loginButton.BackColor = $FAST_Blue
$loginButton.ForeColor = $FAST_White
$loginButton.Font = New-Object System.Drawing.Font("Segoe UI", 14, [System.Drawing.FontStyle]::Bold)
$loginButton.FlatStyle = "Flat"
$loginButton.FlatAppearance.BorderSize = 0
$loginButton.Cursor = "Hand"
$loginCard.Controls.Add($loginButton)
$yPos += 65

# Register Button
$registerButton = New-Object System.Windows.Forms.Button
$registerButton.Text = "Create New Account"
$registerButton.Size = "390,50"
$registerButton.Location = "30,$yPos"
$registerButton.BackColor = $FAST_White
$registerButton.ForeColor = $FAST_Blue
$registerButton.Font = New-Object System.Drawing.Font("Segoe UI", 14)
$registerButton.FlatStyle = "Flat"
$registerButton.FlatAppearance.BorderSize = 2
$registerButton.FlatAppearance.BorderColor = $FAST_Blue
$registerButton.Cursor = "Hand"
$loginCard.Controls.Add($registerButton)
$yPos += 65

# Status Label
$statusLabel = New-Object System.Windows.Forms.Label
$statusLabel.Location = "30,$yPos"
$statusLabel.Size = "390,30"
$statusLabel.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$statusLabel.TextAlign = "MiddleCenter"
$loginCard.Controls.Add($statusLabel)

# Dashboard Tab
$dashboardTab = New-Object System.Windows.Forms.TabPage
$dashboardTab.Text = "Dashboard"
$dashboardTab.BackColor = $FAST_White
$dashboardTab.ForeColor = $FAST_Navy
$tabControl.TabPages.Add($dashboardTab)

# Dashboard Content
$dashboardContainer = New-Object System.Windows.Forms.Panel
$dashboardContainer.Size = "930,680"
$dashboardContainer.BackColor = $FAST_White
$dashboardTab.Controls.Add($dashboardContainer)

# Welcome Section
$welcomePanel = New-Object System.Windows.Forms.Panel
$welcomePanel.Size = "930,120"
$welcomePanel.BackColor = $FAST_Blue
$welcomePanel.Location = "0,0"
$dashboardContainer.Controls.Add($welcomePanel)

$welcomeLabel = New-Object System.Windows.Forms.Label
$welcomeLabel.Text = "Welcome to FAST University Societies Management System!"
$welcomeLabel.Font = New-Object System.Drawing.Font("Segoe UI", 20, [System.Drawing.FontStyle]::Bold)
$welcomeLabel.ForeColor = $FAST_White
$welcomeLabel.Location = "30,30"
$welcomeLabel.Size = "870,60"
$welcomeLabel.TextAlign = "MiddleCenter"
$welcomePanel.Controls.Add($welcomeLabel)

$dashboardUserLabel = New-Object System.Windows.Forms.Label
$dashboardUserLabel.Text = "Logged in as: Guest"
$dashboardUserLabel.Font = New-Object System.Drawing.Font("Segoe UI", 12)
$dashboardUserLabel.ForeColor = $FAST_LightBlue
$dashboardUserLabel.Location = "30,80"
$dashboardUserLabel.Size = "870,30"
$dashboardUserLabel.TextAlign = "MiddleCenter"
$welcomePanel.Controls.Add($dashboardUserLabel)

# Quick Stats Panel
$statsPanel = New-Object System.Windows.Forms.Panel
$statsPanel.Size = "930,200"
$statsPanel.Location = "0,130"
$statsPanel.BackColor = $FAST_White
$dashboardContainer.Controls.Add($statsPanel)

# Stats Cards
$statsInfo = @(
    @{Title="Active Societies"; Value="12"; Color=$FAST_Blue},
    @{Title="Upcoming Events"; Value="8"; Color=$FAST_Green},
    @{Title="Your Memberships"; Value="3"; Color=$FAST_Blue},
    @{Title="Event Tickets"; Value="5"; Color=$FAST_Green}
)

$xPos = 30
foreach ($stat in $statsInfo) {
    $statCard = New-Object System.Windows.Forms.Panel
    $statCard.Size = "200,150"
    $statCard.Location = "$xPos,25"
    $statCard.BackColor = $stat.Color
    $statCard.BorderStyle = "None"
    $statsPanel.Controls.Add($statCard)
    
    $statValue = New-Object System.Windows.Forms.Label
    $statValue.Text = $stat.Value
    $statValue.Font = New-Object System.Drawing.Font("Segoe UI", 36, [System.Drawing.FontStyle]::Bold)
    $statValue.ForeColor = $FAST_White
    $statValue.Location = "0,40"
    $statValue.Size = "200,50"
    $statValue.TextAlign = "MiddleCenter"
    $statCard.Controls.Add($statValue)
    
    $statTitle = New-Object System.Windows.Forms.Label
    $statTitle.Text = $stat.Title
    $statTitle.Font = New-Object System.Drawing.Font("Segoe UI", 12)
    $statTitle.ForeColor = $FAST_White
    $statTitle.Location = "0,90"
    $statTitle.Size = "200,30"
    $statTitle.TextAlign = "MiddleCenter"
    $statCard.Controls.Add($statTitle)
    
    $xPos += 230
}

# Features Section
$featuresPanel = New-Object System.Windows.Forms.Panel
$featuresPanel.Size = "930,300"
$featuresPanel.Location = "0,340"
$featuresPanel.BackColor = $FAST_LightGray
$dashboardContainer.Controls.Add($featuresPanel)

$featuresTitle = New-Object System.Windows.Forms.Label
$featuresTitle.Text = "System Features"
$featuresTitle.Font = New-Object System.Drawing.Font("Segoe UI", 16, [System.Drawing.FontStyle]::Bold)
$featuresTitle.ForeColor = $FAST_Navy
$featuresTitle.Location = "30,20"
$featuresTitle.Size = "870,30"
$featuresPanel.Controls.Add($featuresTitle)

$featuresLabel = New-Object System.Windows.Forms.Label
$featuresLabel.Text = "• Browse and join student societies`n• Register for upcoming events and activities`n• Manage your society memberships`n• View and download event tickets`n• Administrative tools for society management`n• Real-time notifications and updates`n• Professional networking opportunities"
$featuresLabel.Font = New-Object System.Drawing.Font("Segoe UI", 12)
$featuresLabel.ForeColor = $FAST_Navy
$featuresLabel.Location = "30,60"
$featuresLabel.Size = "870,200"
$featuresPanel.Controls.Add($featuresLabel)

# Additional Tabs (placeholder)
$additionalTabs = @("Societies", "Events", "Memberships", "Profile")
foreach ($tabName in $additionalTabs) {
    $tab = New-Object System.Windows.Forms.TabPage
    $tab.Text = $tabName
    $tab.BackColor = $FAST_White
    $tab.ForeColor = $FAST_Navy
    $tabControl.TabPages.Add($tab)
    
    $placeholder = New-Object System.Windows.Forms.Label
    $placeholder.Text = "$tabName functionality coming soon...`n`nThis section will provide detailed $tabName.ToLower() management features."
    $placeholder.Font = New-Object System.Drawing.Font("Segoe UI", 14)
    $placeholder.ForeColor = $FAST_Navy
    $placeholder.Location = "50,50"
    $placeholder.Size = "800,200"
    $placeholder.TextAlign = "MiddleCenter"
    $tab.Controls.Add($placeholder)
}

# Event Handlers
$loginButton_Click = {
    $email = $emailTextBox.Text.Trim()
    $password = $passwordTextBox.Text.Trim()
    
    if ([string]::IsNullOrEmpty($email) -or [string]::IsNullOrEmpty($password)) {
        $statusLabel.Text = "Please enter both email and password."
        $statusLabel.ForeColor = $FAST_Red
    } elseif ($email -eq "admin@fast.edu.pk" -and $password -eq "admin123") {
        $statusLabel.Text = "Login successful! Welcome Administrator."
        $statusLabel.ForeColor = $FAST_Green
        $currentUserLabel.Text = "Administrator"
        $dashboardUserLabel.Text = "Logged in as: Administrator (admin@fast.edu.pk)"
        $tabControl.SelectedTab = $dashboardTab
    } elseif ($email -eq "ali@fast.edu.pk" -and $password -eq "hello") {
        $statusLabel.Text = "Login successful! Welcome Student."
        $statusLabel.ForeColor = $FAST_Green
        $currentUserLabel.Text = "Student"
        $dashboardUserLabel.Text = "Logged in as: Student (ali@fast.edu.pk)"
        $tabControl.SelectedTab = $dashboardTab
    } else {
        $statusLabel.Text = "Invalid credentials. Please try again."
        $statusLabel.ForeColor = $FAST_Red
    }
}

$registerButton_Click = {
    # Enhanced Registration Form
    $regForm = New-Object System.Windows.Forms.Form
    $regForm.Text = "Create New Account - FAST University"
    $regForm.Size = "500,600"
    $regForm.StartPosition = "CenterScreen"
    $regForm.BackColor = $FAST_White
    $regForm.Font = New-Object System.Drawing.Font("Segoe UI", 10)
    
    # Registration Header
    $regHeader = New-Object System.Windows.Forms.Panel
    $regHeader.Size = "500,80"
    $regHeader.BackColor = $FAST_Navy
    $regHeader.Dock = "Top"
    $regForm.Controls.Add($regHeader)
    
    $regTitle = New-Object System.Windows.Forms.Label
    $regTitle.Text = "Create Your Student Account"
    $regTitle.Font = New-Object System.Drawing.Font("Segoe UI", 18, [System.Drawing.FontStyle]::Bold)
    $regTitle.ForeColor = $FAST_White
    $regTitle.Location = "20,25"
    $regTitle.Size = "460,30"
    $regTitle.TextAlign = "MiddleCenter"
    $regHeader.Controls.Add($regTitle)
    
    # Registration Form Fields
    $regYPos = 100
    
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
        $label.Location = "30,$regYPos"
        $label.Size = "150,25"
        $label.Font = New-Object System.Drawing.Font("Segoe UI", 10, [System.Drawing.FontStyle]::Bold)
        $label.ForeColor = $FAST_Navy
        $regForm.Controls.Add($label)
        
        $textBox = New-Object System.Windows.Forms.TextBox
        $textBox.Location = "180,$regYPos"
        $textBox.Size = "280,25"
        $textBox.Font = New-Object System.Drawing.Font("Segoe UI", 10)
        if ($field.IsPassword) {
            $textBox.UseSystemPasswordChar = $true
        }
        $regForm.Controls.Add($textBox)
        $textBoxes[$field.Name] = $textBox
        
        $regYPos += 40
    }
    
    # Register Button
    $regButton = New-Object System.Windows.Forms.Button
    $regButton.Text = "Create Account"
    $regButton.Location = "180,$regYPos"
    $regButton.Size = "280,45"
    $regButton.BackColor = $FAST_Blue
    $regButton.ForeColor = $FAST_White
    $regButton.Font = New-Object System.Drawing.Font("Segoe UI", 12, [System.Drawing.FontStyle]::Bold)
    $regButton.FlatStyle = "Flat"
    $regButton.FlatAppearance.BorderSize = 0
    $regButton.Cursor = "Hand"
    $regForm.Controls.Add($regButton)
    $regYPos += 55
    
    # Status Label
    $regStatusLabel = New-Object System.Windows.Forms.Label
    $regStatusLabel.Location = "30,$regYPos"
    $regStatusLabel.Size = "440,30"
    $regStatusLabel.Font = New-Object System.Drawing.Font("Segoe UI", 9)
    $regStatusLabel.TextAlign = "MiddleCenter"
    $regForm.Controls.Add($regStatusLabel)
    
    # Register button click
    $regButton.Add_Click({
        $name = $textBoxes["name"].Text.Trim()
        $email = $textBoxes["email"].Text.Trim()
        $password = $textBoxes["password"].Text
        $confirm = $textBoxes["confirm"].Text
        
        if ([string]::IsNullOrEmpty($name) -or [string]::IsNullOrEmpty($email) -or [string]::IsNullOrEmpty($password)) {
            $regStatusLabel.Text = "Please fill all required fields."
            $regStatusLabel.ForeColor = $FAST_Red
        } elseif ($password -ne $confirm) {
            $regStatusLabel.Text = "Passwords do not match."
            $regStatusLabel.ForeColor = $FAST_Red
        } elseif ($password.Length -lt 6) {
            $regStatusLabel.Text = "Password must be at least 6 characters."
            $regStatusLabel.ForeColor = $FAST_Red
        } elseif (-not ($email -match "@.*\.")) {
            $regStatusLabel.Text = "Please enter a valid email address."
            $regStatusLabel.ForeColor = $FAST_Red
        } else {
            $regStatusLabel.Text = "Registration successful! You can now login."
            $regStatusLabel.ForeColor = $FAST_Green
            $statusLabel.Text = "Registration successful! Please login with your new account."
            $statusLabel.ForeColor = $FAST_Green
            Start-Sleep -Seconds 2
            $regForm.Close()
        }
    })
    
    $regForm.ShowDialog()
}

$forgotLabel_Click = {
    $statusLabel.Text = "Please contact IT Support at it.support@fast.edu.pk to reset your password."
    $statusLabel.ForeColor = $FAST_Blue
}

$logoutButton_Click = {
    $tabControl.SelectedTab = $loginTab
    $emailTextBox.Text = ""
    $passwordTextBox.Text = ""
    $statusLabel.Text = ""
    $currentUserLabel.Text = "Guest"
    $dashboardUserLabel.Text = "Logged in as: Guest"
    $rememberCheckBox.Checked = $false
}

# Navigation button handlers
foreach ($navButton in $navButtons) {
    $navButton.Add_Click({
        $targetTab = $this.Tag
        foreach ($tab in $tabControl.TabPages) {
            if ($tab.Text -eq $targetTab) {
                $tabControl.SelectedTab = $tab
                break
            }
        }
    })
}

# Add event handlers
$loginButton.Add_Click($loginButton_Click)
$registerButton.Add_Click($registerButton_Click)
$forgotLabel.Add_Click($forgotLabel_Click)
$logoutButton.Add_Click($logoutButton_Click)

# Update time every second
$timeUpdate = {
    $timeLabel.Text = Get-Date -Format "yyyy-MM-dd HH:mm"
}
$timer = New-Object System.Windows.Forms.Timer
$timer.Interval = 1000
$timer.Add_Tick($timeUpdate)
$timer.Start()

# Button hover effects
$loginButton.Add_MouseEnter({
    $this.BackColor = [System.Drawing.Color]::FromArgb(0, 82, 164)
})
$loginButton.Add_MouseLeave({
    $this.BackColor = $FAST_Blue
})

$registerButton.Add_MouseEnter({
    $this.BackColor = [System.Drawing.Color]::FromArgb(230, 240, 250)
})
$registerButton.Add_MouseLeave({
    $this.BackColor = $FAST_White
})

foreach ($navButton in $navButtons) {
    $navButton.Add_MouseEnter({
        $this.BackColor = [System.Drawing.Color]::FromArgb(0, 41, 82)
    })
    $navButton.Add_MouseLeave({
        $this.BackColor = [System.Drawing.Color]::Transparent
    })
}

# Show the application
Write-Host "Starting Enhanced FAST University Societies Management System..." -ForegroundColor Green
Write-Host "Features:" -ForegroundColor Yellow
Write-Host "  • Official FAST University branding and colors" -ForegroundColor White
Write-Host "  • Integrated FAST logo" -ForegroundColor White
Write-Host "  • Modern sidebar navigation" -ForegroundColor White
Write-Host "  • Enhanced visual design" -ForegroundColor White
Write-Host "  • Improved user flow and experience" -ForegroundColor White
Write-Host ""
Write-Host "Login Credentials:" -ForegroundColor Yellow
Write-Host "  Admin: admin@fast.edu.pk / admin123" -ForegroundColor White
Write-Host "  Student: ali@fast.edu.pk / hello" -ForegroundColor White
Write-Host ""

$appForm.ShowDialog() | Out-Null

Write-Host "Application closed." -ForegroundColor Green
