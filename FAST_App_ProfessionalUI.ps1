# FAST University Societies Management System - Professional Modern UI
# Following proper HCI principles and modern design patterns

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# Professional Color Palette
$Colors = @{
    Primary = [System.Drawing.Color]::FromArgb(41, 98, 255)      # Modern Blue
    PrimaryDark = [System.Drawing.Color]::FromArgb(30, 64, 175)   # Dark Blue
    PrimaryLight = [System.Drawing.Color]::FromArgb(66, 133, 244) # Light Blue
    Accent = [System.Drawing.Color]::FromArgb(255, 171, 0)        # Amber
    Secondary = [System.Drawing.Color]::FromArgb(117, 117, 117)   # Grey
    Background = [System.Drawing.Color]::FromArgb(248, 249, 250)  # Light Grey
    Surface = [System.Drawing.Color]::FromArgb(255, 255, 255)     # White
    OnPrimary = [System.Drawing.Color]::FromArgb(255, 255, 255)   # White
    OnSurface = [System.Drawing.Color]::FromArgb(33, 33, 33)      # Dark Grey
    Error = [System.Drawing.Color]::FromArgb(239, 68, 68)         # Red
    Success = [System.Drawing.Color]::FromArgb(76, 175, 80)        # Green
    Warning = [System.Drawing.Color]::FromArgb(255, 152, 0)       # Orange
    Shadow = [System.Drawing.Color]::FromArgb(0, 0, 0, 30)        # Subtle shadow
    Border = [System.Drawing.Color]::FromArgb(224, 224, 224)       # Light Border
}

# Professional Typography
$Fonts = @{
    H1 = New-Object System.Drawing.Font("Segoe UI", 28, [System.Drawing.FontStyle]::Bold)
    H2 = New-Object System.Drawing.Font("Segoe UI", 22, [System.Drawing.FontStyle]::Bold)
    H3 = New-Object System.Drawing.Font("Segoe UI", 18, [System.Drawing.FontStyle]::SemiBold)
    H4 = New-Object System.Drawing.Font("Segoe UI", 16, [System.Drawing.FontStyle]::SemiBold)
    H5 = New-Object System.Drawing.Font("Segoe UI", 14, [System.Drawing.FontStyle]::Medium)
    Body1 = New-Object System.Drawing.Font("Segoe UI", 13, [System.Drawing.FontStyle]::Regular)
    Body2 = New-Object System.Drawing.Font("Segoe UI", 11, [System.Drawing.FontStyle]::Regular)
    Caption = New-Object System.Drawing.Font("Segoe UI", 9, [System.Drawing.FontStyle]::Regular)
    Button = New-Object System.Drawing.Font("Segoe UI", 12, [System.Drawing.FontStyle]::Medium)
    Input = New-Object System.Drawing.Font("Segoe UI", 13, [System.Drawing.FontStyle]::Regular)
}

# Modern Main Form
$mainForm = New-Object System.Windows.Forms.Form
$mainForm.Text = "FAST University - Societies Management System"
$mainForm.Size = "1400,900"
$mainForm.StartPosition = "CenterScreen"
$mainForm.BackColor = $Colors.Background
$mainForm.MinimumSize = "1200,800"
$mainForm.Font = $Fonts.Body1

# Professional Header with Gradient Effect
$headerPanel = New-Object System.Windows.Forms.Panel
$headerPanel.Size = "1400,140"
$headerPanel.Location = "0,0"
$headerPanel.BackColor = $Colors.Primary
$headerPanel.Dock = "Top"
$mainForm.Controls.Add($headerPanel)

# Add gradient effect to header
$headerPanel.Add_Paint({
    $graphics = $_.Graphics
    $rect = $_.ClipRectangle
    $gradient = [System.Drawing.Drawing2D.LinearGradientBrush]::new(
        [System.Drawing.Point]::new(0, 0), 
        [System.Drawing.Point]::new(0, $rect.Height), 
        $Colors.PrimaryDark, 
        $Colors.Primary)
    $graphics.FillRectangle($gradient, $rect)
})

# Load FAST Logo
$logoPath = Join-Path $PSScriptRoot "fast_Logo.png"
$logoPictureBox = New-Object System.Windows.Forms.PictureBox
if (Test-Path $logoPath) {
    try {
        $logoImage = [System.Drawing.Image]::FromFile($logoPath)
        $logoPictureBox.Image = $logoImage
        $logoPictureBox.SizeMode = "Zoom"
        Write-Host "FAST logo loaded successfully" -ForegroundColor Green
    } catch {
        Write-Host "Could not load logo file" -ForegroundColor Yellow
    }
}
$logoPictureBox.Size = "120,120"
$logoPictureBox.Location = "40,10"
$logoPictureBox.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($logoPictureBox)

# Professional Title Section
$titleContainer = New-Object System.Windows.Forms.Panel
$titleContainer.Size = "600,140"
$titleContainer.Location = "180,0"
$titleContainer.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($titleContainer)

$mainTitle = New-Object System.Windows.Forms.Label
$mainTitle.Text = "FAST UNIVERSITY"
$mainTitle.Font = $Fonts.H1
$mainTitle.ForeColor = $Colors.OnPrimary
$mainTitle.Location = "0,20"
$mainTitle.Size = "600,45"
$titleContainer.Controls.Add($mainTitle)

$subTitle = New-Object System.Windows.Forms.Label
$subTitle.Text = "Societies Management System"
$subTitle.Font = $Fonts.H4
$subTitle.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$subTitle.Location = "0,65"
$subTitle.Size = "600,30"
$titleContainer.Controls.Add($subTitle)

$tagline = New-Object System.Windows.Forms.Label
$tagline.Text = "Connect • Collaborate • Create"
$tagline.Font = $Fonts.Body2
$tagline.ForeColor = [System.Drawing.Color]::FromArgb(129, 212, 250)
$tagline.Location = "0,95"
$tagline.Size = "600,25"
$titleContainer.Controls.Add($tagline)

# Modern User Panel
$userPanel = New-Object System.Windows.Forms.Panel
$userPanel.Size = "300,140"
$userPanel.Location = "1100,0"
$userPanel.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($userPanel)

# User Avatar with Circle Effect
$avatarPanel = New-Object System.Windows.Forms.Panel
$avatarPanel.Size = "64,64"
$avatarPanel.Location = "236,18"
$avatarPanel.BackColor = $Colors.PrimaryLight
$userPanel.Controls.Add($avatarPanel)

# Add circular effect
$avatarPanel.Add_Paint({
    $graphics = $_.Graphics
    $graphics.SmoothingMode = "AntiAlias"
    $rect = $_.ClientRectangle
    $graphics.FillEllipse([System.Drawing.SolidBrush]::new($Colors.PrimaryLight), $rect)
    $graphics.DrawEllipse([System.Drawing.Pen]::new($Colors.OnPrimary, 2), $rect)
})

# User Initial
$avatarLabel = New-Object System.Windows.Forms.Label
$avatarLabel.Text = "G"
$avatarLabel.Font = $Fonts.H2
$avatarLabel.ForeColor = $Colors.OnPrimary
$avatarLabel.Location = "0,0"
$avatarLabel.Size = "64,64"
$avatarLabel.TextAlign = "MiddleCenter"
$avatarPanel.Controls.Add($avatarLabel)

# User Information
$userName = New-Object System.Windows.Forms.Label
$userName.Text = "Guest User"
$userName.Font = $Fonts.H5
$userName.ForeColor = $Colors.OnPrimary
$userName.Location = "0,85"
$userName.Size = "300,25"
$userName.TextAlign = "MiddleRight"
$userPanel.Controls.Add($userName)

$userRole = New-Object System.Windows.Forms.Label
$userRole.Text = "Not logged in"
$userRole.Font = $Fonts.Body2
$userRole.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$userRole.Location = "0,110"
$userRole.Size = "300,20"
$userRole.TextAlign = "MiddleRight"
$userPanel.Controls.Add($userRole)

# Modern Logout Button
$logoutButton = New-Object System.Windows.Forms.Button
$logoutButton.Text = "Logout"
$logoutButton.Size = "100,36"
$logoutButton.Location = "120,94"
$logoutButton.BackColor = $Colors.Error
$logoutButton.ForeColor = $Colors.OnPrimary
$logoutButton.Font = $Fonts.Button
$logoutButton.FlatStyle = "Flat"
$logoutButton.FlatAppearance.BorderSize = 0
$logoutButton.Cursor = "Hand"
$userPanel.Controls.Add($logoutButton)

# Hover effects
$logoutButton.Add_MouseEnter({
    $this.BackColor = [System.Drawing.Color]::FromArgb(229, 57, 53)
})
$logoutButton.Add_MouseLeave({
    $this.BackColor = $Colors.Error
})

# Main Content Area
$contentPanel = New-Object System.Windows.Forms.Panel
$contentPanel.Size = "1400,760"
$contentPanel.Location = "0,140"
$contentPanel.BackColor = $Colors.Background
$contentPanel.Dock = "Fill"
$mainForm.Controls.Add($contentPanel)

# Modern Sidebar
$sidebarPanel = New-Object System.Windows.Forms.Panel
$sidebarPanel.Size = "280,760"
$sidebarPanel.Location = "0,0"
$sidebarPanel.BackColor = $Colors.Surface
$sidebarPanel.Dock = "Left"
$contentPanel.Controls.Add($sidebarPanel)

# Add shadow to sidebar
$sidebarPanel.Add_Paint({
    $graphics = $_.Graphics
    $rect = $_.ClipRectangle
    $shadowRect = [System.Drawing.Rectangle]::new($rect.Width - 2, 0, 2, $rect.Height)
    $shadowBrush = [System.Drawing.Drawing2D.LinearGradientBrush]::new(
        [System.Drawing.Point]::new($rect.Width - 2, 0), 
        [System.Drawing.Point]::new($rect.Width, 0), 
        [System.Drawing.Color]::FromArgb(20, 0, 0, 0), 
        [System.Drawing.Color]::FromArgb(0, 0, 0, 0))
    $graphics.FillRectangle($shadowBrush, $shadowRect)
})

# Sidebar Header
$sidebarHeader = New-Object System.Windows.Forms.Label
$sidebarHeader.Text = "MAIN MENU"
$sidebarHeader.Font = $Fonts.Body1
$sidebarHeader.ForeColor = $Colors.OnSurface
$sidebarHeader.Location = "24,32"
$sidebarHeader.Size = "232,24"
$sidebarPanel.Controls.Add($sidebarHeader)

# Navigation Menu Items
$menuItems = @(
    @{Name="Dashboard"; Description="Overview & Statistics"},
    @{Name="Societies"; Description="Browse & Join"},
    @{Name="Events"; Description="Upcoming Activities"},
    @{Name="Memberships"; Description="Your Memberships"},
    @{Name="Profile"; Description="Account Settings"},
    @{Name="Login"; Description="Sign In"}
)

$yPos = 80
foreach ($item in $menuItems) {
    # Menu Item Container
    $menuItem = New-Object System.Windows.Forms.Panel
    $menuItem.Size = "280,56"
    $menuItem.Location = "0,$yPos"
    $menuItem.BackColor = [System.Drawing.Color]::Transparent
    $menuItem.Cursor = "Hand"
    $sidebarPanel.Controls.Add($menuItem)
    
    # Hover effect
    $menuItem.Add_MouseEnter({
        $this.BackColor = [System.Drawing.Color]::FromArgb(0, 0, 0, 4)
    })
    $menuItem.Add_MouseLeave({
        $this.BackColor = [System.Drawing.Color]::Transparent
    })
    
    # Menu Text
    $menuText = New-Object System.Windows.Forms.Label
    $menuText.Text = $item.Name
    $menuText.Font = $Fonts.Body1
    $menuText.ForeColor = $Colors.OnSurface
    $menuText.Location = "24,16"
    $menuText.Size = "200,24"
    $menuItem.Controls.Add($menuText)
    
    # Description
    $menuDesc = New-Object System.Windows.Forms.Label
    $menuDesc.Text = $item.Description
    $menuDesc.Font = $Fonts.Body2
    $menuDesc.ForeColor = $Colors.Secondary
    $menuDesc.Location = "24,36"
    $menuDesc.Size = "200,16"
    $menuItem.Controls.Add($menuDesc)
    
    # Click handler
    $menuItem.Add_Click({
        Write-Host "Navigating to $($item.Name)" -ForegroundColor Green
    })
    
    $yPos += 56
}

# Main Content Area
$mainContentPanel = New-Object System.Windows.Forms.Panel
$mainContentPanel.Size = "1120,760"
$mainContentPanel.Location = "280,0"
$mainContentPanel.BackColor = $Colors.Background
$mainContentPanel.Dock = "Fill"
$contentPanel.Controls.Add($mainContentPanel)

# Modern Login Card
$loginCard = New-Object System.Windows.Forms.Panel
$loginCard.Size = "520,680"
$loginCard.Location = "300,40"
$loginCard.BackColor = $Colors.Surface
$mainContentPanel.Controls.Add($loginCard)

# Add shadow to card
$loginCard.Add_Paint({
    $graphics = $_.Graphics
    $rect = $_.ClipRectangle
    $shadowRect = [System.Drawing.Rectangle]::new(4, 4, $rect.Width, $rect.Height)
    $graphics.FillRectangle([System.Drawing.SolidBrush]::new([System.Drawing.Color]::FromArgb(20, 0, 0, 0)), $shadowRect)
    $graphics.FillRectangle([System.Drawing.SolidBrush]::new($Colors.Surface), $rect)
})

# Card Header
$cardHeader = New-Object System.Windows.Forms.Panel
$cardHeader.Size = "520,120"
$cardHeader.Location = "0,0"
$cardHeader.BackColor = $Colors.Primary
$loginCard.Controls.Add($cardHeader)

# Welcome Text
$welcomeText = New-Object System.Windows.Forms.Label
$welcomeText.Text = "Welcome Back"
$welcomeText.Font = $Fonts.H2
$welcomeText.ForeColor = $Colors.OnPrimary
$welcomeText.Location = "24,32"
$welcomeText.Size = "472,40"
$welcomeText.TextAlign = "MiddleCenter"
$loginCard.Controls.Add($welcomeText)

$subtitleText = New-Object System.Windows.Forms.Label
$subtitleText.Text = "Sign in to continue to your account"
$subtitleText.Font = $Fonts.Body2
$subtitleText.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$subtitleText.Location = "24,72"
$subtitleText.Size = "472,24"
$subtitleText.TextAlign = "MiddleCenter"
$loginCard.Controls.Add($subtitleText)

# Form Fields
$yPos = 160

# Email Field
$emailLabel = New-Object System.Windows.Forms.Label
$emailLabel.Text = "Email Address"
$emailLabel.Font = $Fonts.Body2
$emailLabel.ForeColor = $Colors.OnSurface
$emailLabel.Location = "24,$yPos"
$emailLabel.Size = "472,20"
$loginCard.Controls.Add($emailLabel)
$yPos += 28

$emailTextBox = New-Object System.Windows.Forms.TextBox
$emailTextBox.Size = "472,48"
$emailTextBox.Location = "24,$yPos"
$emailTextBox.Font = $Fonts.Input
$emailTextBox.BorderStyle = "FixedSingle"
$emailTextBox.BackColor = $Colors.Surface
$emailTextBox.ForeColor = $Colors.OnSurface
$loginCard.Controls.Add($emailTextBox)

# Modern text box styling
$emailTextBox.Add_Enter({
    $this.BackColor = $Colors.Background
    $this.ForeColor = $Colors.Primary
})
$emailTextBox.Add_Leave({
    $this.BackColor = $Colors.Surface
    $this.ForeColor = $Colors.OnSurface
})

$yPos += 72

# Password Field
$passwordLabel = New-Object System.Windows.Forms.Label
$passwordLabel.Text = "Password"
$passwordLabel.Font = $Fonts.Body2
$passwordLabel.ForeColor = $Colors.OnSurface
$passwordLabel.Location = "24,$yPos"
$passwordLabel.Size = "472,20"
$loginCard.Controls.Add($passwordLabel)
$yPos += 28

$passwordTextBox = New-Object System.Windows.Forms.TextBox
$passwordTextBox.Size = "472,48"
$passwordTextBox.Location = "24,$yPos"
$passwordTextBox.Font = $Fonts.Input
$passwordTextBox.UseSystemPasswordChar = $true
$passwordTextBox.BorderStyle = "FixedSingle"
$passwordTextBox.BackColor = $Colors.Surface
$passwordTextBox.ForeColor = $Colors.OnSurface
$loginCard.Controls.Add($passwordTextBox)

$passwordTextBox.Add_Enter({
    $this.BackColor = $Colors.Background
    $this.ForeColor = $Colors.Primary
})
$passwordTextBox.Add_Leave({
    $this.BackColor = $Colors.Surface
    $this.ForeColor = $Colors.OnSurface
})

$yPos += 72

# Remember Me and Forgot Password
$checkboxPanel = New-Object System.Windows.Forms.Panel
$checkboxPanel.Size = "472,40"
$checkboxPanel.Location = "24,$yPos"
$checkboxPanel.BackColor = [System.Drawing.Color]::Transparent
$loginCard.Controls.Add($checkboxPanel)

$rememberCheckBox = New-Object System.Windows.Forms.CheckBox
$rememberCheckBox.Text = "Remember me"
$rememberCheckBox.Font = $Fonts.Body2
$rememberCheckBox.ForeColor = $Colors.OnSurface
$rememberCheckBox.Location = "0,8"
$rememberCheckBox.Size = "150,24"
$rememberCheckBox.BackColor = [System.Drawing.Color]::Transparent
$checkboxPanel.Controls.Add($rememberCheckBox)

$forgotLink = New-Object System.Windows.Forms.LinkLabel
$forgotLink.Text = "Forgot password?"
$forgotLink.Font = $Fonts.Body2
$forgotLink.LinkColor = $Colors.Primary
$forgotLink.Location = "322,8"
$forgotLink.Size = "150,24"
$forgotLink.TextAlign = "MiddleRight"
$checkboxPanel.Controls.Add($forgotLink)

$yPos += 56

# Login Button
$loginButton = New-Object System.Windows.Forms.Button
$loginButton.Text = "Sign In"
$loginButton.Size = "472,56"
$loginButton.Location = "24,$yPos"
$loginButton.BackColor = $Colors.Primary
$loginButton.ForeColor = $Colors.OnPrimary
$loginButton.Font = $Fonts.Button
$loginButton.FlatStyle = "Flat"
$loginButton.FlatAppearance.BorderSize = 0
$loginButton.Cursor = "Hand"
$loginCard.Controls.Add($loginButton)

# Button hover effects
$loginButton.Add_MouseEnter({
    $this.BackColor = $Colors.PrimaryDark
})
$loginButton.Add_MouseLeave({
    $this.BackColor = $Colors.Primary
})

$yPos += 72

# Register Button (Outlined)
$registerButton = New-Object System.Windows.Forms.Button
$registerButton.Text = "Create Account"
$registerButton.Size = "472,56"
$registerButton.Location = "24,$yPos"
$registerButton.BackColor = [System.Drawing.Color]::Transparent
$registerButton.ForeColor = $Colors.Primary
$registerButton.Font = $Fonts.Button
$registerButton.FlatStyle = "Flat"
$registerButton.FlatAppearance.BorderSize = 2
$registerButton.FlatAppearance.BorderColor = $Colors.Primary
$registerButton.Cursor = "Hand"
$loginCard.Controls.Add($registerButton)

$registerButton.Add_MouseEnter({
    $this.BackColor = [System.Drawing.Color]::FromArgb(41, 98, 255, 20)
})
$registerButton.Add_MouseLeave({
    $this.BackColor = [System.Drawing.Color]::Transparent
})

$yPos += 72

# Status Message
$statusLabel = New-Object System.Windows.Forms.Label
$statusLabel.Location = "24,$yPos"
$statusLabel.Size = "472,24"
$statusLabel.Font = $Fonts.Body2
$statusLabel.TextAlign = "MiddleCenter"
$loginCard.Controls.Add($statusLabel)

# Event Handlers with Modern Feedback
$loginButton.Add_Click({
    $email = $emailTextBox.Text.Trim()
    $password = $passwordTextBox.Text.Trim()
    
    if ([string]::IsNullOrEmpty($email) -or [string]::IsNullOrEmpty($password)) {
        $statusLabel.Text = "Please enter both email and password"
        $statusLabel.ForeColor = $Colors.Error
    } elseif ($email -eq "admin@fast.edu.pk" -and $password -eq "admin123") {
        $statusLabel.Text = "Login successful! Welcome Administrator."
        $statusLabel.ForeColor = $Colors.Success
        $userName.Text = "Administrator"
        $userRole.Text = "System Administrator"
    } elseif ($email -eq "ali@fast.edu.pk" -and $password -eq "hello") {
        $statusLabel.Text = "Login successful! Welcome Student."
        $statusLabel.ForeColor = $Colors.Success
        $userName.Text = "Ali Student"
        $userRole.Text = "Computer Science Student"
    } else {
        $statusLabel.Text = "Invalid credentials. Please try again."
        $statusLabel.ForeColor = $Colors.Error
    }
})

$registerButton.Add_Click({
    $statusLabel.Text = "Opening registration form..."
    $statusLabel.ForeColor = $Colors.Primary
})

$forgotLink.Add_LinkClicked({
    $statusLabel.Text = "Password reset link sent to your email"
    $statusLabel.ForeColor = $Colors.Primary
})

$logoutButton.Add_Click({
    $emailTextBox.Text = ""
    $passwordTextBox.Text = ""
    $statusLabel.Text = ""
    $userName.Text = "Guest User"
    $userRole.Text = "Not logged in"
    $rememberCheckBox.Checked = $false
})

# Fade-in animation
$mainForm.Add_Load({
    $mainForm.Opacity = 0
    $timer = New-Object System.Windows.Forms.Timer
    $timer.Interval = 50
    $timer.Add_Tick({
        $mainForm.Opacity += 0.05
        if ($mainForm.Opacity -ge 1) {
            $mainForm.Opacity = 1
            $timer.Stop()
        }
    })
    $timer.Start()
})

# Show the professional application
Write-Host "Starting Professional FAST University Societies Management System..." -ForegroundColor Green
Write-Host "Modern UI/UX Features:" -ForegroundColor Yellow
Write-Host "  • Professional color palette and typography" -ForegroundColor White
Write-Host "  • Material design principles" -ForegroundColor White
Write-Host "  • Proper visual hierarchy and spacing" -ForegroundColor White
Write-Host "  • Modern card-based layout" -ForegroundColor White
Write-Host "  • Smooth hover effects and transitions" -ForegroundColor White
Write-Host "  • Professional form controls" -ForegroundColor White
Write-Host "  • Integrated FAST logo" -ForegroundColor White
Write-Host ""
Write-Host "Login Credentials:" -ForegroundColor Cyan
Write-Host "  Admin: admin@fast.edu.pk / admin123" -ForegroundColor White
Write-Host "  Student: ali@fast.edu.pk / hello" -ForegroundColor White
Write-Host ""

$mainForm.ShowDialog() | Out-Null

Write-Host "Professional application closed successfully." -ForegroundColor Green
