# FAST University Societies Management System - Modern UI/UX Design
# Following proper HCI principles and modern design patterns

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Drawing.Drawing2D

# Modern Color Palette - Material Design inspired
$Colors = @{
    Primary = [System.Drawing.Color]::FromArgb(25, 118, 210)      # Material Blue 600
    PrimaryDark = [System.Drawing.Color]::FromArgb(21, 101, 192)  # Material Blue 700
    PrimaryLight = [System.Drawing.Color]::FromArgb(66, 165, 245) # Material Blue 400
    Accent = [System.Drawing.Color]::FromArgb(255, 193, 7)        # Material Amber A400
    Secondary = [System.Drawing.Color]::FromArgb(96, 125, 139)    # Material Blue Grey 500
    Background = [System.Drawing.Color]::FromArgb(250, 250, 250)  # Material Grey 50
    Surface = [System.Drawing.Color]::FromArgb(255, 255, 255)     # Material Grey 50
    OnPrimary = [System.Drawing.Color]::FromArgb(255, 255, 255)   # White
    OnSurface = [System.Drawing.Color]::FromArgb(33, 33, 33)      # Material Grey 900
    Error = [System.Drawing.Color]::FromArgb(244, 67, 54)         # Material Red 500
    Success = [System.Drawing.Color]::FromArgb(76, 175, 80)        # Material Green 500
    Warning = [System.Drawing.Color]::FromArgb(255, 152, 0)       # Material Orange 500
    Shadow = [System.Drawing.Color]::FromArgb(0, 0, 0, 26)        # Subtle shadow
    Border = [System.Drawing.Color]::FromArgb(224, 224, 224)       # Material Grey 200
}

# Typography Scale
$Fonts = @{
    H1 = New-Object System.Drawing.Font("Segoe UI", 32, [System.Drawing.FontStyle]::Bold)
    H2 = New-Object System.Drawing.Font("Segoe UI", 24, [System.Drawing.FontStyle]::Bold)
    H3 = New-Object System.Drawing.Font("Segoe UI", 20, [System.Drawing.FontStyle]::SemiBold)
    H4 = New-Object System.Drawing.Font("Segoe UI", 18, [System.Drawing.FontStyle]::SemiBold)
    H5 = New-Object System.Drawing.Font("Segoe UI", 16, [System.Drawing.FontStyle]::Medium)
    Body1 = New-Object System.Drawing.Font("Segoe UI", 14, [System.Drawing.FontStyle]::Regular)
    Body2 = New-Object System.Drawing.Font("Segoe UI", 12, [System.Drawing.FontStyle]::Regular)
    Caption = New-Object System.Drawing.Font("Segoe UI", 10, [System.Drawing.FontStyle]::Regular)
    Button = New-Object System.Drawing.Font("Segoe UI", 12, [System.Drawing.FontStyle]::Medium)
    Input = New-Object System.Drawing.Font("Segoe UI", 14, [System.Drawing.FontStyle]::Regular)
}

# Modern Form with Material Design Principles
$mainForm = New-Object System.Windows.Forms.Form
$mainForm.Text = "FAST University - Societies Management System"
$mainForm.Size = "1400,900"
$mainForm.StartPosition = "CenterScreen"
$mainForm.BackColor = $Colors.Background
$mainForm.MinimumSize = "1200,800"
$mainForm.Font = $Fonts.Body1
$mainForm.FormBorderStyle = "Sizable"

# Custom painting for rounded corners and shadows
$mainForm.Add_Paint({
    $graphics = $_.Graphics
    $graphics.SmoothingMode = "AntiAlias"
})

# Modern Header with Elevation
$headerPanel = New-Object System.Windows.Forms.Panel
$headerPanel.Size = "1400,140"
$headerPanel.Location = "0,0"
$headerPanel.BackColor = $Colors.Primary
$headerPanel.Dock = "Top"
$mainForm.Controls.Add($headerPanel)

# Add shadow effect to header
$headerPanel.Add_Paint({
    $graphics = $_.Graphics
    $rect = $_.ClipRectangle
    $shadowRect = [System.Drawing.Rectangle]::new(0, 2, $rect.Width, 8)
    $shadowBrush = [System.Drawing.Drawing2D.LinearGradientBrush]::new(
        [System.Drawing.Point]::new(0, 2), 
        [System.Drawing.Point]::new(0, 10), 
        [System.Drawing.Color]::FromArgb(50, 0, 0, 0), 
        [System.Drawing.Color]::FromArgb(0, 0, 0, 0))
    $graphics.FillRectangle($shadowBrush, $shadowRect)
})

# Load and display FAST Logo
$logoPath = Join-Path $PSScriptRoot "fast_Logo.png"
$logoPictureBox = New-Object System.Windows.Forms.PictureBox
if (Test-Path $logoPath) {
    $logoImage = [System.Drawing.Image]::FromFile($logoPath)
    $logoPictureBox.Image = $logoImage
    $logoPictureBox.SizeMode = "Zoom"
}
$logoPictureBox.Size = "120,120"
$logoPictureBox.Location = "40,10"
$logoPictureBox.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($logoPictureBox)

# Modern Title Section with Typography Hierarchy
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
$mainTitle.Size = "600,50"
$titleContainer.Controls.Add($mainTitle)

$subTitle = New-Object System.Windows.Forms.Label
$subTitle.Text = "Societies Management System"
$subTitle.Font = $Fonts.H4
$subTitle.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220) # Material Blue 100
$subTitle.Location = "0,70"
$subTitle.Size = "600,30"
$titleContainer.Controls.Add($subTitle)

$tagline = New-Object System.Windows.Forms.Label
$tagline.Text = "Connect • Collaborate • Create"
$tagline.Font = $Fonts.Body2
$tagline.ForeColor = [System.Drawing.Color]::FromArgb(129, 212, 250) # Material Blue 100
$tagline.Location = "0,100"
$tagline.Size = "600,25"
$titleContainer.Controls.Add($tagline)

# Modern User Panel with Avatar
$userPanel = New-Object System.Windows.Forms.Panel
$userPanel.Size = "300,140"
$userPanel.Location = "1100,0"
$userPanel.BackColor = [System.Drawing.Color]::Transparent
$headerPanel.Controls.Add($userPanel)

# Avatar Circle
$avatarPanel = New-Object System.Windows.Forms.Panel
$avatarPanel.Size = "60,60"
$avatarPanel.Location = "240,20"
$avatarPanel.BackColor = $Colors.PrimaryLight
$userPanel.Controls.Add($avatarPanel)

# Add circular avatar effect
$avatarPanel.Add_Paint({
    $graphics = $_.Graphics
    $graphics.SmoothingMode = "AntiAlias"
    $rect = $_.ClientRectangle
    $path = [System.Drawing.Drawing2D.GraphicsPath]::new()
    $path.AddEllipse($rect)
    $graphics.FillPath([System.Drawing.SolidBrush]::new($Colors.PrimaryLight), $path)
    $graphics.DrawPath([System.Drawing.Pen]::new($Colors.OnPrimary, 2), $path)
})

# User Initial
$avatarLabel = New-Object System.Windows.Forms.Label
$avatarLabel.Text = "G"
$avatarLabel.Font = $Fonts.H2
$avatarLabel.ForeColor = $Colors.OnPrimary
$avatarLabel.Location = "0,0"
$avatarLabel.Size = "60,60"
$avatarLabel.TextAlign = "MiddleCenter"
$avatarPanel.Controls.Add($avatarLabel)

# User Info
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

# Modern Floating Action Button for Logout
$logoutButton = New-Object System.Windows.Forms.Button
$logoutButton.Text = "Logout"
$logoutButton.Size = "100,40"
$logoutButton.Location = "120,90"
$logoutButton.BackColor = $Colors.Error
$logoutButton.ForeColor = $Colors.OnPrimary
$logoutButton.Font = $Fonts.Button
$logoutButton.FlatStyle = "Flat"
$logoutButton.FlatAppearance.BorderSize = 0
$logoutButton.Cursor = "Hand"
$userPanel.Controls.Add($logoutButton)

# Add hover effects to logout button
$logoutButton.Add_MouseEnter({
    $this.BackColor = [System.Drawing.Color]::FromArgb(229, 57, 53)
})
$logoutButton.Add_MouseLeave({
    $this.BackColor = $Colors.Error
})

# Main Content Area with Cards
$contentPanel = New-Object System.Windows.Forms.Panel
$contentPanel.Size = "1400,760"
$contentPanel.Location = "0,140"
$contentPanel.BackColor = $Colors.Background
$contentPanel.Dock = "Fill"
$mainForm.Controls.Add($contentPanel)

# Modern Sidebar with Navigation
$sidebarPanel = New-Object System.Windows.Forms.Panel
$sidebarPanel.Size = "320,760"
$sidebarPanel.Location = "0,0"
$sidebarPanel.BackColor = $Colors.Surface
$sidebarPanel.Dock = "Left"
$contentPanel.Controls.Add($sidebarPanel)

# Add shadow to sidebar
$sidebarPanel.Add_Paint({
    $graphics = $_.Graphics
    $rect = $_.ClipRectangle
    $shadowRect = [System.Drawing.Rectangle]::new($rect.Width - 4, 0, 4, $rect.Height)
    $shadowBrush = [System.Drawing.Drawing2D.LinearGradientBrush]::new(
        [System.Drawing.Point]::new($rect.Width - 4, 0), 
        [System.Drawing.Point]::new($rect.Width, 0), 
        [System.Drawing.Color]::FromArgb(20, 0, 0, 0), 
        [System.Drawing.Color]::FromArgb(0, 0, 0, 0))
    $graphics.FillRectangle($shadowBrush, $shadowRect)
})

# Sidebar Header
$sidebarHeader = New-Object System.Windows.Forms.Label
$sidebarHeader.Text = "NAVIGATION"
$sidebarHeader.Font = $Fonts.Body1
$sidebarHeader.ForeColor = $Colors.OnSurface
$sidebarHeader.Location = "24,32"
$sidebarHeader.Size = "272,24"
$sidebarPanel.Controls.Add($sidebarHeader)

# Modern Navigation Menu Items
$menuItems = @(
    @{Name="Dashboard"; Icon=""; Description="Overview & Statistics"},
    @{Name="Societies"; Icon=""; Description="Browse & Join"},
    @{Name="Events"; Icon=""; Description="Upcoming Activities"},
    @{Name="Memberships"; Icon=""; Description="Your Memberships"},
    @{Name="Profile"; Icon=""; Description="Account Settings"},
    @{Name="Login"; Icon=""; Description="Sign In"}
)

$yPos = 80
foreach ($item in $menuItems) {
    # Menu Item Container
    $menuItem = New-Object System.Windows.Forms.Panel
    $menuItem.Size = "320,56"
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
    
    # Ripple effect click handler
    $menuItem.Add_Click({
        # Add ripple animation here
        Write-Host "Navigating to $($item.Name)" -ForegroundColor Green
    })
    
    $yPos += 56
}

# Main Content Area
$mainContentPanel = New-Object System.Windows.Forms.Panel
$mainContentPanel.Size = "1080,760"
$mainContentPanel.Location = "320,0"
$mainContentPanel.BackColor = $Colors.Background
$mainContentPanel.Dock = "Fill"
$contentPanel.Controls.Add($mainContentPanel)

# Modern Login Card with Material Design
$loginCard = New-Object System.Windows.Forms.Panel
$loginCard.Size = "480,640"
$loginCard.Location = "300,60"
$loginCard.BackColor = $Colors.Surface
$mainContentPanel.Controls.Add($loginCard)

# Add shadow and rounded corners to card
$loginCard.Add_Paint({
    $graphics = $_.Graphics
    $graphics.SmoothingMode = "AntiAlias"
    $rect = $_.ClientRectangle
    
    # Shadow
    $shadowRect = [System.Drawing.Rectangle]::new(4, 4, $rect.Width, $rect.Height)
    $shadowPath = [System.Drawing.Drawing2D.GraphicsPath]::new()
    $shadowPath.AddRectangle($shadowRect)
    $graphics.FillPath([System.Drawing.SolidBrush]::new([System.Drawing.Color]::FromArgb(30, 0, 0, 0)), $shadowPath)
    
    # Card with rounded corners
    $cardPath = [System.Drawing.Drawing2D.GraphicsPath]::new()
    $cardRadius = 8
    $cardPath.AddArc($rect.X, $rect.Y, $cardRadius * 2, $cardRadius * 2, 180, 90)
    $cardPath.AddArc($rect.Right - $cardRadius * 2, $rect.Y, $cardRadius * 2, $cardRadius * 2, 270, 90)
    $cardPath.AddArc($rect.Right - $cardRadius * 2, $rect.Bottom - $cardRadius * 2, $cardRadius * 2, $cardRadius * 2, 0, 90)
    $cardPath.AddArc($rect.X, $rect.Bottom - $cardRadius * 2, $cardRadius * 2, $cardRadius * 2, 90, 90)
    $cardPath.CloseFigure()
    $graphics.FillPath([System.Drawing.SolidBrush]::new($Colors.Surface), $cardPath)
})

# Card Header
$cardHeader = New-Object System.Windows.Forms.Panel
$cardHeader.Size = "480,120"
$cardHeader.Location = "0,0"
$cardHeader.BackColor = $Colors.Primary
$loginCard.Controls.Add($cardHeader)

# Rounded header
$cardHeader.Add_Paint({
    $graphics = $_.Graphics
    $graphics.SmoothingMode = "AntiAlias"
    $rect = $_.ClientRectangle
    $path = [System.Drawing.Drawing2D.GraphicsPath]::new()
    $path.AddArc($rect.X, $rect.Y, 16, 16, 180, 90)
    $path.AddArc($rect.Right - 16, $rect.Y, 16, 16, 270, 90)
    $path.AddLine($rect.Right, $rect.Height, $rect.Right, $rect.Height)
    $path.AddLine($rect.X, $rect.Height, $rect.X, $rect.Height)
    $path.CloseFigure()
    $graphics.FillPath([System.Drawing.SolidBrush]::new($Colors.Primary), $path)
})

# Welcome Text
$welcomeText = New-Object System.Windows.Forms.Label
$welcomeText.Text = "Welcome Back"
$welcomeText.Font = $Fonts.H2
$welcomeText.ForeColor = $Colors.OnPrimary
$welcomeText.Location = "24,32"
$welcomeText.Size = "432,40"
$loginCard.Controls.Add($welcomeText)

$subtitleText = New-Object System.Windows.Forms.Label
$subtitleText.Text = "Sign in to continue to your account"
$subtitleText.Font = $Fonts.Body2
$subtitleText.ForeColor = [System.Drawing.Color]::FromArgb(207, 216, 220)
$subtitleText.Location = "24,72"
$subtitleText.Size = "432,24"
$loginCard.Controls.Add($subtitleText)

# Form Fields with Material Design
$yPos = 160

# Email Field
$emailLabel = New-Object System.Windows.Forms.Label
$emailLabel.Text = "Email Address"
$emailLabel.Font = $Fonts.Body2
$emailLabel.ForeColor = $Colors.OnSurface
$emailLabel.Location = "24,$yPos"
$emailLabel.Size = "432,20"
$loginCard.Controls.Add($emailLabel)
$yPos += 24

$emailTextBox = New-Object System.Windows.Forms.TextBox
$emailTextBox.Size = "432,48"
$emailTextBox.Location = "24,$yPos"
$emailTextBox.Font = $Fonts.Input
$emailTextBox.BorderStyle = "FixedSingle"
$emailTextBox.BackColor = $Colors.Surface
$emailTextBox.ForeColor = $Colors.OnSurface
$emailTextBox.Text = ""
$loginCard.Controls.Add($emailTextBox)

# Modern text box styling
$emailTextBox.Add_Enter({
    $this.BackColor = $Colors.Background
    $this.BorderStyle = "FixedSingle"
})
$emailTextBox.Add_Leave({
    $this.BackColor = $Colors.Surface
})

$yPos += 64

# Password Field
$passwordLabel = New-Object System.Windows.Forms.Label
$passwordLabel.Text = "Password"
$passwordLabel.Font = $Fonts.Body2
$passwordLabel.ForeColor = $Colors.OnSurface
$passwordLabel.Location = "24,$yPos"
$passwordLabel.Size = "432,20"
$loginCard.Controls.Add($passwordLabel)
$yPos += 24

$passwordTextBox = New-Object System.Windows.Forms.TextBox
$passwordTextBox.Size = "432,48"
$passwordTextBox.Location = "24,$yPos"
$passwordTextBox.Font = $Fonts.Input
$passwordTextBox.UseSystemPasswordChar = $true
$passwordTextBox.BorderStyle = "FixedSingle"
$passwordTextBox.BackColor = $Colors.Surface
$passwordTextBox.ForeColor = $Colors.OnSurface
$loginCard.Controls.Add($passwordTextBox)

$passwordTextBox.Add_Enter({
    $this.BackColor = $Colors.Background
})
$passwordTextBox.Add_Leave({
    $this.BackColor = $Colors.Surface
})

$yPos += 64

# Remember Me Checkbox with Material Design
$checkboxPanel = New-Object System.Windows.Forms.Panel
$checkboxPanel.Size = "432,40"
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
$forgotLink.Location = "280,8"
$forgotLink.Size = "152,24"
$forgotLink.TextAlign = "MiddleRight"
$checkboxPanel.Controls.Add($forgotLink)

$yPos += 56

# Login Button with Material Design
$loginButton = New-Object System.Windows.Forms.Button
$loginButton.Text = "Sign In"
$loginButton.Size = "432,56"
$loginButton.Location = "24,$yPos"
$loginButton.BackColor = $Colors.Primary
$loginButton.ForeColor = $Colors.OnPrimary
$loginButton.Font = $Fonts.Button
$loginButton.FlatStyle = "Flat"
$loginButton.FlatAppearance.BorderSize = 0
$loginButton.Cursor = "Hand"
$loginCard.Controls.Add($loginButton)

# Rounded button effect
$loginButton.Add_Paint({
    $graphics = $_.Graphics
    $graphics.SmoothingMode = "AntiAlias"
    $rect = $_.ClientRectangle
    $path = [System.Drawing.Drawing2D.GraphicsPath]::new()
    $path.AddRectangle($rect)
    $graphics.FillPath([System.Drawing.SolidBrush]::new($Colors.Primary), $path)
})

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
$registerButton.Size = "432,56"
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
    $this.BackColor = [System.Drawing.Color]::FromArgb(25, 118, 210, 20)
})
$registerButton.Add_MouseLeave({
    $this.BackColor = [System.Drawing.Color]::Transparent
})

$yPos += 72

# Status Message
$statusLabel = New-Object System.Windows.Forms.Label
$statusLabel.Location = "24,$yPos"
$statusLabel.Size = "432,24"
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
    # Modern registration dialog
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

# Add smooth transitions and animations
$mainForm.Add_Load({
    # Fade in effect
    $mainForm.Opacity = 0
    $timer = New-Object System.Windows.Forms.Timer
    $timer.Interval = 50
    $fadeStep = 0.05
    $timer.Add_Tick({
        $mainForm.Opacity += $fadeStep
        if ($mainForm.Opacity -ge 1) {
            $mainForm.Opacity = 1
            $timer.Stop()
        }
    })
    $timer.Start()
})

# Show the modern application
Write-Host "Starting Modern FAST University Societies Management System..." -ForegroundColor Green
Write-Host "UI/UX Features:" -ForegroundColor Yellow
Write-Host "  • Material Design principles" -ForegroundColor White
Write-Host "  • Modern color palette and typography" -ForegroundColor White
Write-Host "  • Proper visual hierarchy and spacing" -ForegroundColor White
Write-Host "  • Rounded corners and shadows" -ForegroundColor White
Write-Host "  • Hover effects and transitions" -ForegroundColor White
Write-Host "  • Responsive form controls" -ForegroundColor White
Write-Host "  • Professional card-based layout" -ForegroundColor White
Write-Host ""

$mainForm.ShowDialog() | Out-Null

Write-Host "Modern application closed successfully." -ForegroundColor Green
