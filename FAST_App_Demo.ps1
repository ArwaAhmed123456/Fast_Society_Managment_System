# FAST University Societies Management System - PowerShell Demo
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# Main Application Form
$appForm = New-Object System.Windows.Forms.Form
$appForm.Text = "FAST University - Societies Management System"
$appForm.Size = "1000,700"
$appForm.StartPosition = "CenterScreen"
$appForm.BackColor = [System.Drawing.Color]::FromArgb(230, 240, 250) # LightBlue
$appForm.MinimumSize = "800,600"

# Header Panel
$headerPanel = New-Object System.Windows.Forms.Panel
$headerPanel.Size = "1000,80"
$headerPanel.BackColor = [System.Drawing.Color]::FromArgb(0, 51, 102) # SecondaryBlue
$headerPanel.Dock = "Top"
$appForm.Controls.Add($headerPanel)

# Logo placeholder
$logo = New-Object System.Windows.Forms.Panel
$logo.Size = "50,50"
$logo.Location = "20,15"
$logo.BackColor = [System.Drawing.Color]::FromArgb(255, 204, 0) # AccentYellow
$headerPanel.Controls.Add($logo)

# Title
$title = New-Object System.Windows.Forms.Label
$title.Text = "FAST University"
$title.Font = New-Object System.Drawing.Font("Segoe UI", 18, [System.Drawing.FontStyle]::Bold)
$title.ForeColor = [System.Drawing.Color]::White
$title.Location = "80,10"
$title.Size = "300,35"
$headerPanel.Controls.Add($title)

# Subtitle
$subtitle = New-Object System.Windows.Forms.Label
$subtitle.Text = "Societies Management System"
$subtitle.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$subtitle.ForeColor = [System.Drawing.Color]::FromArgb(230, 240, 250) # LightBlue
$subtitle.Location = "80,45"
$subtitle.Size = "300,20"
$headerPanel.Controls.Add($subtitle)

# Logout button
$logoutButton = New-Object System.Windows.Forms.Button
$logoutButton.Text = "Logout"
$logoutButton.Size = "100,40"
$logoutButton.Location = "880,20"
$logoutButton.BackColor = [System.Drawing.Color]::FromArgb(220, 53, 69) # ErrorRed
$logoutButton.ForeColor = [System.Drawing.Color]::White
$logoutButton.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$logoutButton.FlatStyle = "Flat"
$logoutButton.FlatAppearance.BorderSize = 0
$headerPanel.Controls.Add($logoutButton)

# Main Content Panel
$mainPanel = New-Object System.Windows.Forms.Panel
$mainPanel.Size = "1000,620"
$mainPanel.Location = "0,80"
$mainPanel.BackColor = [System.Drawing.Color]::White
$mainPanel.Dock = "Fill"
$appForm.Controls.Add($mainPanel)

# Tab Control
$tabControl = New-Object System.Windows.Forms.TabControl
$tabControl.Size = "960,580"
$tabControl.Location = "20,20"
$tabControl.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$mainPanel.Controls.Add($tabControl)

# Login Tab
$loginTab = New-Object System.Windows.Forms.TabPage
$loginTab.Text = "Login"
$loginTab.BackColor = [System.Drawing.Color]::White
$loginTab.ForeColor = [System.Drawing.Color]::FromArgb(0, 51, 102)
$tabControl.TabPages.Add($loginTab)

# Login Card Panel
$loginCard = New-Object System.Windows.Forms.Panel
$loginCard.Size = "400,450"
$loginCard.Location = "250,50"
$loginCard.BackColor = [System.Drawing.Color]::White
$loginCard.BorderStyle = "FixedSingle"
$loginTab.Controls.Add($loginCard)

# Login Title
$loginTitle = New-Object System.Windows.Forms.Label
$loginTitle.Text = "Sign In"
$loginTitle.Font = New-Object System.Drawing.Font("Segoe UI", 20, [System.Drawing.FontStyle]::Bold)
$loginTitle.ForeColor = [System.Drawing.Color]::FromArgb(0, 51, 102)
$loginTitle.Location = "30,30"
$loginTitle.Size = "340,40"
$loginCard.Controls.Add($loginTitle)

# Email Label
$emailLabel = New-Object System.Windows.Forms.Label
$emailLabel.Text = "Email Address"
$emailLabel.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$emailLabel.ForeColor = [System.Drawing.Color]::FromArgb(51, 51, 51)
$emailLabel.Location = "30,90"
$emailLabel.Size = "340,20"
$loginCard.Controls.Add($emailLabel)

# Email TextBox
$emailTextBox = New-Object System.Windows.Forms.TextBox
$emailTextBox.Size = "340,35"
$emailTextBox.Location = "30,115"
$emailTextBox.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$loginCard.Controls.Add($emailTextBox)

# Password Label
$passwordLabel = New-Object System.Windows.Forms.Label
$passwordLabel.Text = "Password"
$passwordLabel.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$passwordLabel.ForeColor = [System.Drawing.Color]::FromArgb(51, 51, 51)
$passwordLabel.Location = "30,170"
$passwordLabel.Size = "340,20"
$loginCard.Controls.Add($passwordLabel)

# Password TextBox
$passwordTextBox = New-Object System.Windows.Forms.TextBox
$passwordTextBox.Size = "340,35"
$passwordTextBox.Location = "30,195"
$passwordTextBox.Font = New-Object System.Drawing.Font("Segoe UI", 10)
$passwordTextBox.UseSystemPasswordChar = $true
$loginCard.Controls.Add($passwordTextBox)

# Forgot Password Link
$forgotLabel = New-Object System.Windows.Forms.Label
$forgotLabel.Text = "Forgot your password?"
$forgotLabel.Font = New-Object System.Drawing.Font("Segoe UI", 9)
$forgotLabel.ForeColor = [System.Drawing.Color]::FromArgb(0, 102, 204)
$forgotLabel.Location = "30,240"
$forgotLabel.Size = "340,20"
$forgotLabel.Cursor = "Hand"
$loginCard.Controls.Add($forgotLabel)

# Login Button
$loginButton = New-Object System.Windows.Forms.Button
$loginButton.Text = "Sign In"
$loginButton.Size = "340,45"
$loginButton.Location = "30,280"
$loginButton.BackColor = [System.Drawing.Color]::FromArgb(0, 102, 204)
$loginButton.ForeColor = [System.Drawing.Color]::White
$loginButton.Font = New-Object System.Drawing.Font("Segoe UI", 12, [System.Drawing.FontStyle]::Bold)
$loginButton.FlatStyle = "Flat"
$loginButton.FlatAppearance.BorderSize = 0
$loginCard.Controls.Add($loginButton)

# Register Button
$registerButton = New-Object System.Windows.Forms.Button
$registerButton.Text = "Create New Account"
$registerButton.Size = "340,45"
$registerButton.Location = "30,340"
$registerButton.BackColor = [System.Drawing.Color]::White
$registerButton.ForeColor = [System.Drawing.Color]::FromArgb(0, 102, 204)
$registerButton.Font = New-Object System.Drawing.Font("Segoe UI", 12)
$registerButton.FlatStyle = "Flat"
$registerButton.FlatAppearance.BorderSize = 2
$registerButton.FlatAppearance.BorderColor = [System.Drawing.Color]::FromArgb(0, 102, 204)
$loginCard.Controls.Add($registerButton)

# Status Label
$statusLabel = New-Object System.Windows.Forms.Label
$statusLabel.Location = "30,400"
$statusLabel.Size = "340,30"
$statusLabel.Font = New-Object System.Drawing.Font("Segoe UI", 9)
$statusLabel.ForeColor = [System.Drawing.Color]::FromArgb(220, 53, 69)
$statusLabel.TextAlign = "MiddleCenter"
$loginCard.Controls.Add($statusLabel)

# Dashboard Tab
$dashboardTab = New-Object System.Windows.Forms.TabPage
$dashboardTab.Text = "Dashboard"
$dashboardTab.BackColor = [System.Drawing.Color]::White
$dashboardTab.ForeColor = [System.Drawing.Color]::FromArgb(0, 51, 102)
$tabControl.TabPages.Add($dashboardTab)

# Dashboard Content
$welcomeLabel = New-Object System.Windows.Forms.Label
$welcomeLabel.Text = "Welcome to FAST University Societies Management System!"
$welcomeLabel.Font = New-Object System.Drawing.Font("Segoe UI", 16, [System.Drawing.FontStyle]::Bold)
$welcomeLabel.ForeColor = [System.Drawing.Color]::FromArgb(0, 51, 102)
$welcomeLabel.Location = "50,50"
$welcomeLabel.Size = "800,40"
$dashboardTab.Controls.Add($welcomeLabel)

$featuresLabel = New-Object System.Windows.Forms.Label
$featuresLabel.Text = "Features available:`n• Browse and join societies`n• Register for events`n• Manage memberships`n• View event tickets`n• Administrative tools"
$featuresLabel.Font = New-Object System.Drawing.Font("Segoe UI", 12)
$featuresLabel.ForeColor = [System.Drawing.Color]::FromArgb(51, 51, 51)
$featuresLabel.Location = "50,120"
$featuresLabel.Size = "800,200"
$dashboardTab.Controls.Add($featuresLabel)

# Event Handlers - Fixed with proper script blocks
$loginButton_Click = {
    $email = $emailTextBox.Text.Trim()
    $password = $passwordTextBox.Text.Trim()
    
    if ([string]::IsNullOrEmpty($email) -or [string]::IsNullOrEmpty($password)) {
        $statusLabel.Text = "Please enter both email and password."
        $statusLabel.ForeColor = [System.Drawing.Color]::FromArgb(220, 53, 69)
    } elseif ($email -eq "admin@fast.edu.pk" -and $password -eq "admin123") {
        $statusLabel.Text = "Login successful! Welcome Administrator."
        $statusLabel.ForeColor = [System.Drawing.Color]::FromArgb(40, 167, 69)
        $tabControl.SelectedTab = $dashboardTab
    } elseif ($email -eq "ali@fast.edu.pk" -and $password -eq "hello") {
        $statusLabel.Text = "Login successful! Welcome Student."
        $statusLabel.ForeColor = [System.Drawing.Color]::FromArgb(40, 167, 69)
        $tabControl.SelectedTab = $dashboardTab
    } else {
        $statusLabel.Text = "Invalid credentials. Please try again."
        $statusLabel.ForeColor = [System.Drawing.Color]::FromArgb(220, 53, 69)
    }
}

$registerButton_Click = {
    # Create registration form
    $regForm = New-Object System.Windows.Forms.Form
    $regForm.Text = "Create New Account"
    $regForm.Size = "450,400"
    $regForm.StartPosition = "CenterScreen"
    $regForm.BackColor = [System.Drawing.Color]::White
    
    # Registration form title
    $regTitle = New-Object System.Windows.Forms.Label
    $regTitle.Text = "Create Your Student Account"
    $regTitle.Font = New-Object System.Drawing.Font("Segoe UI", 16, [System.Drawing.FontStyle]::Bold)
    $regTitle.ForeColor = [System.Drawing.Color]::FromArgb(0, 51, 102)
    $regTitle.Location = "20,20"
    $regTitle.Size = "400,30"
    $regForm.Controls.Add($regTitle)
    
    # Name field
    $nameLabel = New-Object System.Windows.Forms.Label
    $nameLabel.Text = "Full Name:"
    $nameLabel.Location = "20,70"
    $nameLabel.Size = "100,25"
    $regForm.Controls.Add($nameLabel)
    
    $nameTextBox = New-Object System.Windows.Forms.TextBox
    $nameTextBox.Location = "120,70"
    $nameTextBox.Size = "300,25"
    $regForm.Controls.Add($nameTextBox)
    
    # Email field
    $regEmailLabel = New-Object System.Windows.Forms.Label
    $regEmailLabel.Text = "Email:"
    $regEmailLabel.Location = "20,110"
    $regEmailLabel.Size = "100,25"
    $regForm.Controls.Add($regEmailLabel)
    
    $regEmailTextBox = New-Object System.Windows.Forms.TextBox
    $regEmailTextBox.Location = "120,110"
    $regEmailTextBox.Size = "300,25"
    $regForm.Controls.Add($regEmailTextBox)
    
    # Password field
    $regPasswordLabel = New-Object System.Windows.Forms.Label
    $regPasswordLabel.Text = "Password:"
    $regPasswordLabel.Location = "20,150"
    $regPasswordLabel.Size = "100,25"
    $regForm.Controls.Add($regPasswordLabel)
    
    $regPasswordTextBox = New-Object System.Windows.Forms.TextBox
    $regPasswordTextBox.Location = "120,150"
    $regPasswordTextBox.Size = "300,25"
    $regPasswordTextBox.UseSystemPasswordChar = $true
    $regForm.Controls.Add($regPasswordTextBox)
    
    # Confirm password field
    $confirmLabel = New-Object System.Windows.Forms.Label
    $confirmLabel.Text = "Confirm:"
    $confirmLabel.Location = "20,190"
    $confirmLabel.Size = "100,25"
    $regForm.Controls.Add($confirmLabel)
    
    $confirmTextBox = New-Object System.Windows.Forms.TextBox
    $confirmTextBox.Location = "120,190"
    $confirmTextBox.Size = "300,25"
    $confirmTextBox.UseSystemPasswordChar = $true
    $regForm.Controls.Add($confirmTextBox)
    
    # Register button
    $regButton = New-Object System.Windows.Forms.Button
    $regButton.Text = "Register"
    $regButton.Location = "120,250"
    $regButton.Size = "140,40"
    $regButton.BackColor = [System.Drawing.Color]::FromArgb(0, 102, 204)
    $regButton.ForeColor = [System.Drawing.Color]::White
    $regButton.Font = New-Object System.Drawing.Font("Segoe UI", 12, [System.Drawing.FontStyle]::Bold)
    $regButton.FlatStyle = "Flat"
    $regButton.FlatAppearance.BorderSize = 0
    $regForm.Controls.Add($regButton)
    
    # Cancel button
    $cancelButton = New-Object System.Windows.Forms.Button
    $cancelButton.Text = "Cancel"
    $cancelButton.Location = "280,250"
    $cancelButton.Size = "140,40"
    $cancelButton.BackColor = [System.Drawing.Color]::White
    $cancelButton.ForeColor = [System.Drawing.Color]::FromArgb(0, 102, 204)
    $cancelButton.Font = New-Object System.Drawing.Font("Segoe UI", 12)
    $cancelButton.FlatStyle = "Flat"
    $cancelButton.FlatAppearance.BorderSize = 2
    $cancelButton.FlatAppearance.BorderColor = [System.Drawing.Color]::FromArgb(0, 102, 204)
    $regForm.Controls.Add($cancelButton)
    
    # Status label
    $regStatusLabel = New-Object System.Windows.Forms.Label
    $regStatusLabel.Location = "20,310"
    $regStatusLabel.Size = "400,30"
    $regStatusLabel.Font = New-Object System.Drawing.Font("Segoe UI", 9)
    $regStatusLabel.TextAlign = "MiddleCenter"
    $regForm.Controls.Add($regStatusLabel)
    
    # Register button click
    $regButton.Add_Click({
        $name = $nameTextBox.Text.Trim()
        $email = $regEmailTextBox.Text.Trim()
        $password = $regPasswordTextBox.Text
        $confirm = $confirmTextBox.Text
        
        if ([string]::IsNullOrEmpty($name) -or [string]::IsNullOrEmpty($email) -or [string]::IsNullOrEmpty($password)) {
            $regStatusLabel.Text = "Please fill all fields."
            $regStatusLabel.ForeColor = [System.Drawing.Color]::FromArgb(220, 53, 69)
        } elseif ($password -ne $confirm) {
            $regStatusLabel.Text = "Passwords do not match."
            $regStatusLabel.ForeColor = [System.Drawing.Color]::FromArgb(220, 53, 69)
        } elseif ($password.Length -lt 6) {
            $regStatusLabel.Text = "Password must be at least 6 characters."
            $regStatusLabel.ForeColor = [System.Drawing.Color]::FromArgb(220, 53, 69)
        } else {
            $regStatusLabel.Text = "Registration successful! You can now login."
            $regStatusLabel.ForeColor = [System.Drawing.Color]::FromArgb(40, 167, 69)
            $statusLabel.Text = "Registration successful! Please login with your new account."
            $statusLabel.ForeColor = [System.Drawing.Color]::FromArgb(40, 167, 69)
            Start-Sleep -Seconds 2
            $regForm.Close()
        }
    })
    
    # Cancel button click
    $cancelButton.Add_Click({ $regForm.Close() })
    
    $regForm.ShowDialog()
}

$forgotLabel_Click = {
    $statusLabel.Text = "Please contact the system administrator to reset your password."
    $statusLabel.ForeColor = [System.Drawing.Color]::FromArgb(0, 102, 204)
}

$logoutButton_Click = {
    $tabControl.SelectedTab = $loginTab
    $emailTextBox.Text = ""
    $passwordTextBox.Text = ""
    $statusLabel.Text = ""
}

# Add event handlers
$loginButton.Add_Click($loginButton_Click)
$registerButton.Add_Click($registerButton_Click)
$forgotLabel.Add_Click($forgotLabel_Click)
$logoutButton.Add_Click($logoutButton_Click)

# Show the application
Write-Host "Starting FAST University Societies Management System..." -ForegroundColor Green
Write-Host "Login Credentials:" -ForegroundColor Yellow
Write-Host "  Admin: admin@fast.edu.pk / admin123" -ForegroundColor White
Write-Host "  Student: ali@fast.edu.pk / hello" -ForegroundColor White
Write-Host ""

$appForm.ShowDialog() | Out-Null

Write-Host "Application closed." -ForegroundColor Green
