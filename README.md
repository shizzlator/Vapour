Vapour
======

Web application testing controller

- Upload test dll
- Administer environmental variables
- Runs tests in headless browser
- Uses chosen set of saved variables to execute DEV/UAT/PROD URLs
- Returns result(s)
- Logs runs
 
Tech

- Katana app
- WebAPI service layer
- Coupled with headless browesr
- Snazzy font end
- MongoDB
- Makes you tea

Dev install
====
TODO

Installing MongoDB on windows
====

Run this .ps1 script for an easier 2.6 windows service install:

		# Create the logs and database data folders
		cd "C:\Program Files\MongoDB 2.6 Standard\"
		md logs
		md data\db
		cd bin

		# Create the YAML config file
		$config = "systemLog:"
		$config += "`n    destination: file"
		$config += "`n    path: `"/Program Files/MongoDB 2.6 Standard/logs/mongo.log`""
		$config += "`n    quiet: true"
		$config += "`n    logAppend: true"

		$config | Out-File mongod.cfg

		# Install Mongo as a service
		.\mongod --config "C:\Program Files\MongoDB 2.6 Standard\bin\mongod.cfg" --install --dbpath="C:\Program Files\MongoDB 2.6 Standard\data\db"
		sc.exe start "MongoDB"