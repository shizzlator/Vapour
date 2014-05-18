Vapour
======

Web application testing controller

- Upload unit test dlls
- Administer environmental variables
- Runs tests in headless browser
- Uses chosen set of saved variables to execute DEV/UAT/PROD URLs
- Returns result(s)
- Logs runs
 
Tech
====

- Katana app
- WebAPI service layer
- Coupled with headless browser
- Snazzy font end
- MongoDB
- Makes you tea

Dev install
====
Run the setup.ps1 powershell script.

Installing MongoDB on windows
====
To install MongoDB, choose the MSI package and install it to the default location.
Once it's installed, run the install-mongodb-service.ps1 powershell script to install
MongoDB as a service.