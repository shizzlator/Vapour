$ErrorActionPreference = "Stop"

# From http://rkeithhill.wordpress.com/2013/04/05/powershell-script-that-relaunches-as-admin/
function IsAdministrator
{
    $Identity = [System.Security.Principal.WindowsIdentity]::GetCurrent()
    $Principal = New-Object System.Security.Principal.WindowsPrincipal($Identity)
    $Principal.IsInRole([System.Security.Principal.WindowsBuiltInRole]::Administrator)
}


function IsUacEnabled
{
    (Get-ItemProperty HKLM:\Software\Microsoft\Windows\CurrentVersion\Policies\System).EnableLua -ne 0
}

# Check the script is run as an administrator
if (!(IsAdministrator))
{
    if (IsUacEnabled -eq $true)
    {
    	Write-Host "This script needs to be run by an administrator/elevated privileges." -ForegroundColor red
    	exit 1
    }
}
# The current location should be the master/trunk root of Vapour
$pwd = pwd

# Switch to the IIS7 snapin (built into IIS 7.5+)
import-module WebAdministration
Set-Location IIS:\

# Delete the sites/app pools
# Lazy deleting - ignore errors if the sites don't exist

try
{
	Remove-Website Vapour
	Write-Host "- Removed existing Vapour site" -ForegroundColor Green
}
catch
{
	Write-Host "Deleting the Vapour site failed (it may not exist)"  -ForegroundColor DarkGray
}

try
{
	Remove-Website Vapour.Api
	Write-Host "- Removed existing Vapour.Api site" -ForegroundColor Green
}
catch
{
 	Write-Host "Deleting the Vapour.Api site failed (it may not exist)"  -ForegroundColor DarkGray
}

try
{
	Remove-WebAppPool Vapour
	Write-Host "- Removed existing Vapour app pool" -ForegroundColor Green
}
catch
{
	Write-Host "Deleting the Vapour app pool failed (it may not exist)"  -ForegroundColor DarkGray
}

try
{
	Remove-WebAppPool Vapour.Api
	Write-Host "- Removed existing Vapour.Api app pool" -ForegroundColor Green
}
catch
{
	Write-Host "Deleting the Vapour.Api app pool failed (it may not exist)"  -ForegroundColor DarkGray
}

# Create the sites
New-Item iis:\Sites\Vapour -bindings @{protocol="http";bindingInformation=":8040:*"} -physicalPath $pwd\Vapour.Web -force
New-Item iis:\Sites\Vapour.Api -bindings @{protocol="http";bindingInformation=":8041:*"} -physicalPath $pwd\Vapour.Api -force
Write-Host "Vapour sites created on ports 8040 and 8041" -ForegroundColor Green

# Create the app pools
New-Item IIS:\AppPools\Vapour -force
New-Item IIS:\AppPools\Vapour.Api -force
Write-Host "Vapour App pools created" -ForegroundColor Green

# Set the app pools to use LocalService
Set-ItemProperty iis:\apppools\Vapour -name processModel -value @{userName="LocalService";identitytype=1}
Set-ItemProperty iis:\apppools\Vapour.Api -name processModel -value @{userName="LocalService";identitytype=1}
Write-Host "Assigned app pools to LocalService" -ForegroundColor Green

# Assign the app pools to the sites
Set-ItemProperty IIS:\sites\Vapour -name applicationPool -value Vapour -force
Set-ItemProperty IIS:\sites\Vapour.Api -name applicationPool -value Vapour.Api -force
Write-Host "Assigned app pools to sites" -ForegroundColor Green



Write-Host "...Done!" -ForegroundColor Green

Set-Location $pwd