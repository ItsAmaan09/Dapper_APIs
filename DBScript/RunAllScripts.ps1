# Function to log messages
function Log-Message {
    param (
        [string]$message
    )
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $logMessage = "$timestamp - $message"
    Write-Host $logMessage
    Add-Content -Path $logFilePath -Value $logMessage
}

# Enable script debugging
$DebugPreference = "Continue"

# Read configuration
try {
    $configPath = "../DBScript/config.json"
    $config = Get-Content $configPath | ConvertFrom-Json
    Log-Message "Configuration file loaded successfully."
} catch {
    Write-Host "Error reading configuration file:" $_.Exception.Message
    exit 1
}

$connectionString = $config.connectionString
$logFilePath = $config.logFilePath

# Parse connection string
try {
    $connectionStringParams = $connectionString -split ";"
    $serverName = ($connectionStringParams | Where-Object { $_ -like "Data Source*" }) -replace "Data Source=", ""
    $databaseName = ($connectionStringParams | Where-Object { $_ -like "Initial Catalog*" }) -replace "Initial Catalog=", ""
    $userName = ($connectionStringParams | Where-Object { $_ -like "User ID*" }) -replace "User ID=", ""
    $password = ($connectionStringParams | Where-Object { $_ -like "Password*" }) -replace "Password=", ""
    Log-Message "Connection string parsed successfully."
} catch {
    Write-Host "Error parsing connection string:" $_.Exception.Message
    exit 1
}

# Initialize log file
try {
    if (Test-Path $logFilePath) {
        Remove-Item $logFilePath
    }
    New-Item -Path $logFilePath -ItemType File | Out-Null
    Log-Message "Log file initialized."
} catch {
    Write-Host "Error initializing log file:" $_.Exception.Message
    exit 1
}

# Get all .sql files in the current directory
try {
    $scriptFiles = Get-ChildItem -Path (Get-Location) -Filter "*.sql" | Sort-Object Name
    Log-Message "SQL script files retrieved successfully."
} catch {
    Write-Host "Error retrieving SQL script files:" $_.Exception.Message
    exit 1
}

foreach ($script in $scriptFiles) {
    Log-Message "Running script: $($script.Name)"
    try {
        & sqlcmd -S $serverName -U $userName -P $password -d $databaseName -i $script.FullName -b 2>&1 | Tee-Object -Variable sqlOutput
        $sqlError = $?
        if ($sqlError) {
            Log-Message "Error executing script: $($script.Name)"
            Log-Message "Error details: $sqlOutput"
        } else {
            Log-Message "Successfully executed script: $($script.Name)"
        }
    } catch {
        Log-Message "Exception occurred executing script: $($script.Name)"
        Log-Message "Exception details: $_"
    }
}

Log-Message "All scripts execution completed."
