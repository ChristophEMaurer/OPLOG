# OPLOG
OP-LOG - the electronic logbook for surgeons in training (this used to be at www.op.log.de)

This program contains some cool features that you can reuse:
* a framework for database access where you can switch between databases by editing the application settings file.
* a permission framework, where each UI control has a specific permission which can be granted to user groups.
A group contains users and all this (permissions/groups/users) can be edited by the user.

### Running OPLOG from MS Visual Studio
* Run the script '_createFilesAndFolders.cmd'
* Open the solution 'OpLog.sln'
* Set the project 'Operationen' as the startup project
* Run

### Creating a new version from script
* Search for file 'setupBuildEnvironment.cmd' and adapt it to your path.
* Run the file 'CreateVersionFull-all-platforms.cmd'

### Required external licenses
* OPLOG uses the Elegant Ribbon UI from FOSS
* you must purchase a license key and add it to your code

File BusinessLayerPrivate.cs

public void ActivateEleganzUiLicense()

{

/* You must purchase a license for the Elegant UI Runtime Version v2.0.50727, Version 3.3.0.0 and activate it like below. */
    
    string elegantUiKey = "...";

    Elegant.Ui.RibbonLicenser.LicenseKey = elegantUiKey;

}

   
