# Create the logs and database data folders
cd "C:\Program Files\MongoDB 2.6 Standard\"
md logs -F
md data\db -F
cd bin

# Create the YAML config file
$config = "systemLog:"
$config += "`n    destination: file"
$config += "`n    path: `"/Program Files/MongoDB 2.6 Standard/logs/mongo.log`""
$config += "`n    quiet: true"
$config += "`n    logAppend: true"

$config | Out-File mongod.cfg

# Install Mongo as a service
.\mongod --config "C:\Program Files\MongoDB 2.6 Standard\bin\mongod.cfg" --install --smallfiles --dbpath="C:\Program Files\MongoDB 2.6 Standard\data\db"
sc.exe start "MongoDB"