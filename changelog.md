
#### Change log 0.10.0.0

* Making use of madb over the previous methods of adb communication
* No longer requires root to work.
* No longer depends on busybox toolset
* added ability to connect to wearable devices
* Source code migrated to [Github][desource100]. 
* Builds moved to AppVeyor.
* Releases will continue to be published on [CodePlex site][decodeplex100].

[desource100]: http://github.com/camalot/droidexplorer
[decodeplex100]: http://de.codeplex.com/


#### Change log 0.9.0.4

* Fixed the install apk shortcut to default to install. [workitem:17512]
* Added screen capture functionality. The Plugin will show up if the connected device supports screen capture. [workitem:17417]
* Tweaked the SaveFileDialog, OpenFileDialog, & FileDialog to better support an 'initial directory'.
* Tweaked the way the plugin toolstrips are created to hopefully support reloading when connecting to another device. 
* Fixed launching screenshot from the shortcut or jumplist item. [workitem:17416]
* Replaced the JNLP with a more active developed androidscreencast and launch a jar file directly.
* Changed the window of the ScreenRecorder to be fixed.
* Updated the InstallDialog to make use of the PluginHost which provides device and other information.
* Fixed the regex for checking if an app was installed successfully and fixed the regex for getting the permissions
* Fixed the device backup so it launches correctly from the shortcut.
* Added Sqlite Browser to replace the feature lacking sqlite explorer that was part of this project. See http://sqlitebrowser.org/ for info on Sqlite Browser.
* TransferDialog now usable by plugins. [workitem:17521]
* Consolidation of some string resources in to the Global App Resources
* Default the file name when saving a screenshot to be 'screenshot-{yyyy-MM-dd-hhmmss}.png' [workitem:17516]
* All PluginForms now require IPluginHost to be passed in. 
* Updated image resources for the screenshot plugin
* added toolbar item to screenshot to "edit in default application"
* removed SqliteEditForm since it isn't used any more.
* fixed the saving size of the screenshot. It will save as the original source size [workitem:17517]
* Added ability to rotate screen capture video 90/180/270 degrees when copying to pc. uses ffmpeg to rotate video. [workitem:17519]
* Added option to delete capture file after copy to pc [workitem:17525]
* set a default filename on '/sdcard/' for screen capture [workitem:17523]
* Changed when disconnected from device that the 'connect to device' button is not disabled. [workitem:17528]
* Fixed crash when the monitor service is not running and you disconnect from the device because the ADB daemon shuts down and status cannot be retrieved.
* Added link to WiFi ADB app on the remote connect dialog. This will help people enable wifi adb. [workitem:17533]
* Added method to IPlugin interface to initialize the plugin. This can be used to set up files on the device before the plugin executes.
* Removed androidscreencast, switched to using android screen monitor fork (https://github.com/camalot/android-screen-monitor). The fork allows the device ID to be passed as an argument. This way the user does not have to select the device again.
* Changed the call to launch the jar for screen monitor to call "java.exe", before it was just "java" and that was causing issues. [workitem:17531]
* Added more logging during initialization to help with debugging start up issues. [workitem:17527]
* Set the toolstrip renderer on screenshot plugin to be the same that is used elsewhere.
* Added functions for Forward & Reverse to be used with the port manager
* Added option to restart ADB server on the connection dialog


#### Change log 0.9.0.3 

* on the warning dialog about not being able to run adb as root, added link to go get ADBD Insecure by ChainFire on Google Play.
* updated places that linked to the codeplex site (as the project site) to link to the DE website (http://de.bit13.com/)
* Fixed the second opening of the options dialog so the registry settings are set after the settings reload.
* fixed the device backup shortcut. 
* Added device restore shortcut.
* fixed the restore plug-in to handle the restore process.
* updated the location of the device images to use the program data directory. This is mainly for the service because a normal user, on windows 10, 
	will not have access to read the icon file. [workitem:17502]
* fixed when looking for the build tools versions it will only take directories in to account that match a version string regex. [workitem:17420]
* changed the position of the Ask for Help button on windows 8+ devices to account for the larger buttons (on windows 10)
* (Development Tools) updated to use Visual Studio 2015 and .net 4.5.2
* (Development Tools) updated to use Wix 3.10 - it will try to use the installed version of wix, if it has created the WIX environment variable
* Reworked the 'status-window' to use existing functionality in ADB so a different binary does not have to be distributed with Droid Explorer.
* Updated some of the plugins that launch tools. It now checks for the tools, if it can't find them, they will not show up.
* Updated the application manifest files to support current features in windows.
* Added check to see if user (or user policy) has recent document tracking disabled. If it is, do not attempt to create jump lists.

#### Change log 0.9.0.2

* Fixed bug in build process that did not change the host name from the test service to the production service.

#### Change log 0.9.0.1

* Added shortcuts to the shell in the Start Menu Program Folder
* Fixed the shell console not performing line breaks correctly.
* Cloud Statistics now "throttled" by 7 days for each device.
* catching the exception that is sometimes thrown when existing from logcat so it doesn't crash the application.
* Fixed the issue where the tree will not "reload" after device is disconnected/reconnected.
* Fixed the issue with some commands in the shell console not on a new line when they should be. 
* Added check for a minimum version of adb.
* Added support for adb devices call that doesn't support "-l"
* Added multiple shortcuts for shell, screenshot, screencast, ask for help, DE website

#### Change log 0.9.0.0

* Added support for connecting to devices via TCP/IP
* New web service for getting device icons. 
* Upgraded .NET Framework Requirement to 4.5.1
* Device lookup now uses 'detailed' call. Gets device name, model, and product name. see adb devices -l.
* Redesigned the GenericDeviceSelectionForm
* Updated the [default] image for devices
* Options Windows: added information to show the build tools version that is being used. [workitem:17313]
* Added ActiveMenu to add button to DWM. 
* Moved DDMS launcher to be a plugin
* Moved Hierarchy Viewer to be a plugin
* Removed the "reboot" buttons, instead there is now a reboot plugin that supports all forms of reboot: normal, recovery, bootloader, download. 
* Added a warning dialog when the app launches that will let the user know if droid explorer was unable to gain root access via adb root.
* Some refactoring to some plugins, like the shell, so it can be launched independently. 
* Added links to ask questions on android enthusiasts. 
* Removing some code that is unused.
* removed out of date plugins / tools.
* Some refactoring of the ConsoleControl. Moved to the Core.UI. Added reference to Camalot.Common. 
* Updated LogCat to use ConsoleControl. 
* Updated the Debug Console to use ConsoleControl.
* Updated ConsoleWriter to work with the ConsoleControl.
* Added OutputProcessors to handle different content for logging.
* Adb Root call is now called during initialization, after device selection. 
* if adb root fails, a warning is displayed to the user. Future warnings can be ignored.
* A minor redesign to the splash screen. 
* Plugins are now grouped in to separate toolbars.
* Added SDK Manager plugin
* Added AVD Manager plugin
* updated some icons across the application. 
* Plugins now grouped in the plugin menu. 
* Fixed the cloud image path to support the new site. 
* Fixed the file names when transfering to PC to remove invalid characters.
* Fixed the device monitor service. 
* Added PluginSettings (not yet used really)
* Added AdbScanner (test) to find adb devices on network.
* Added icon handler interface
* Added picture extension handler using the icon handler interface

#### Change log 0.8.8.11

* Fixed issue with not finding aapt.exe - [workitem:16288]

#### Change log 0.8.8.10

* Fixed issue with some people having a folder called "android-4.2.2" in their build-tools path. - [workitem:16223]

#### Change log 0.8.8.9

* fixed issue with new aapt tool being in a "build-tools" directory. [workitem:15816]
* fixed issue with having a device name with a space in it. [workitem:15739]
* Wix tools updated to 3.7

#### Change log 0.8.8.8

* fixed the icon for packages on the desktop
* fixed the install dialog closing right when it starts
* removed the link to "set up the sdk for me" as this is no longer supported.
* fixed bug where the device selection dialog would show, even if there was only one device connected.
* fixed toolbar from having "gap" between other toolbar
* removed main menu items that do not have any menus

#### Change log 0.8.8.7

* Added fallback icon if unable to get the image/icon from the Cloud Service
* Removed some stale plugins that were either out dated or incomplete.
* Added handler for *.ab files for restoring backups
* Added plugin to create device backups
* Backups stored in %USERPROFILE%\Android Backups\%DEVICE_ID%\
* Added custom folder icon for the android backups directory
* better error handling for installing an apk
* bug fixes for the Runner.
* Added windows shell menu to unpack android backups thanks to a modified version of "ABE" (https://github.com/camalot/android-backup-extractor)
* added BasePlugin class that defines properties that may stay the same between plugins. 
* Runner changed to use ID of plugin to load. This defaults to the "type,assembly", but is overridable.
* Implemented the Enhanced Android Backup. - http://de.codeplex.com/wikipage?title=The%20.ABEX%20File%20Format%20Specification
* .ABEX files are tied to a device so backups can be applied to the correct device during a restore.
* new icon for android packages
* From my tests, Vista x64 is now working

#### Change log 0.8.8.6

* Device images are now pulled from DroidExplorer Cloud Service
* refined some issues with the usage statistics
* Added a method to get the first available value from a list of property names
* DroidExplorer.Configuration no longer depends on DroidExplorer.Core.UI (it is actually the other way now)
* fix to the bootstraper to only try to delete the SDK if it is a "local" sdk, not an existing.
* no longer support the "local" sdk, you must now select an existing SDK
* checks for sdk if it was installed by the "official" installer.
* removed option to use the "local sdk" in the bootstrapper during the install process
* removed the download panel from the bootstrapper. It will never need to use that panel with using an existing sdk.
* removed the Android SDK license agreement acceptance panel.
* added another panel to stop the adb service if it is running right before it starts the service.
* added some "cleanup" when the service stops to help ensure that there are no "orphaned" devices. All connected devices and all "known" devices are unregistered from the system.
* static assets removed. They are no longer used.
* added a notification for new versions being released.
* fixed the issue with the colors of the screenshot being off. (may not be the fastest solution, but works for now)

#### Change log 0.8.8.5

* fix for x86 builds

#### Change log 0.8.8.4

* added device icon for anzu (Sony Ericson XperiaArc-LT15i)
* added device icon for blade (ZTE Blade)
* added device icon for SCH-I800 (Verizon Galaxy Tab)
* added device icon for buzz (HTC Wildfire)
* added device icon for c660 (LG Optimus Pro)
* added optional usage statistics. This will records some device information for analytics. 
	This service will eventually allow for the device icon to come from the web service
	Currently, this option is only set during the install (or in the registry). Will add option in the UI soon.

#### Change log 0.8.8.3

 * added check for "ro.product.model" value for the icon name as well as the other properties that are already checked in "Service"
 * added sgh-t849 (Tmobile Samsung Galaxy Tab)

#### Change log 0.8.8.2

* New Device selection dialog
* all assemblies are now signed again
* fixed the "runner" so apk's can now be installed from the machine (along with the other plugins)
* when upgrading, and using an existing SDK, the value will now be pulled from the registry, if it exists.
* fixed bug in uninstall that would remove the sdk, even if you used an existing sdk
* removed anda.pk plugin since the site is dead.
* added a link in the bootstrapper (installer) to download the sdk. 
* refactored some strings to the external resources files in the bootstrapper (installer)
* changed the reboot commands to use the adb commands, not the shell commands
* fixed rm command to now use busybox as it was passing args that are not supported on the "stock" rm command.
* added the sph-p100 device icon

#### Change log 0.8.8.1

* fixed issue that some people experience error durring install about an droidexplorer shell extension.
* added sph-m910, htc desire hd (ace), nexus s, and the c8600 icon support
* use existing android SDK is now the default and recommended.
* added htc vision (Tmobile G2) and the s5830

#### Change log 0.8.8.0

* updated project / solution files to use VS 2010 ( Still using .NET 3.5 for the runtime )
* fixed the issue with latest android-sdk using "platform-tools" directory for adb
* added the LG G2X and Samsung Galaxy S variant icons.
* fixed the issue with Bart plugin not being located because android added "-1" to the file name.
* fixed screen shot capture to now support 32bpp raw image as well as 16bpp

#### Change log 0.8.7.1

* Some refactoring of the settings to keep the Core libraries non-windows dependant.
* APK information is now cached for faster loading
* Attempting a fix for vista 64 bit and xp 64 bit
* fixed the .net 3.5 check
* added icon for the EVO (supersonic) - thanks to sanjsrik for identifying the device
* added some debugging info to the shell console plugin to see if I can get it working for droid/milestone

#### Change log 0.8.7.0

* upgraded project / solution files to Visual Studio 2010 - still targeting 3.5 for now
* upgraded WiX to version 3.5 (beta) because 3.0 is not supported in VS2010
* fixed bug with zip file process being in use during bootstrapper
* did a lot of warning clean up for methods missing the xml docs
* did some code refactoring
* Can now use existing SDK instead of the "trimmed" version
* registry settings now check both the local machine and current user
* can change the sdk path in the options (could be buggy, please report any issues)

#### Change log 0.8.6.0

* fixed missing file in the standalone installer
* added check for minimum .net framework version in to bootstrapper (v3.5sp1)
* increased the service start/stop timeout in the bootstrapper from 30 seconds to 60 seconds
* removed initial strings from download panel labels of bootstrapper
* htc desire / bravo icon added for attached devices - thanks to beadza for identifying the device
* added ability to only install the SDK Tools by running install with /sdk switch
* sdk install mode checks if DE is installed, if not, it switches to full install mode
* bart plugin now also checks the sd-ext mount point for the license.
* added the sd-ext app paths as valid application paths
* added sd-ext app paths to the application backup plugin
* removed anda.pk plugin as the site is dead.
* screencast plugin changed to pull the jnlp file directly from the trunk of the project. If there 
	is an error, it falls back to a "local" file
* fixed issues with spaces in folder names
* motorola backflip icon added for attached devices - thanks to zibiza for identifying the device
* new screenshot app that handles all resolutions. Uses new methods to get the framebuffer data
* adjusted the RGB565 class to better handle other devices for screenshots
* started to implement communicating with adb over TCP like ddms does.
* acer liquid icon added for attached devices - thanks to fyodor0218 for identifying the device
* started working on the ability to use existing sdk (not yet an option, but soon)

#### Change log 0.8.5.1

* Fixed typeo in the final step when uninstalling
* Fixed x64 issues for windows 7 x64 - still broke on Vista x64 & XP x64 (sorry people, i'm still working on it!)
* Default button for bootstrapper changed to the "next" button instead of cancel
* added incredible icon for attached devices
* added nexus one icon for attached devices
* added galaxy S (gt-i9000) icon for attached devices
* added acer liquid icon for attached devices
* updated the samsung moment icon
* added a new "open" version of the moment - rename in the assets directory to "moment.ico" to use
	
#### Change log 0.8.5.0

* android screen cast updated to load the lastest build from the trunk
* changed publish to use skydrive share instead of the api from the msbuild tasks
* added platform tools 2.2
* added sdk tools r6
* updated repository.xml
* added help video link when the device cannot be found
* attempting to fix the registry read issues with XP SP3 x64 and Vista x64
* removed the "fade out" of the expandos from the treeview to remove the flicker
* fixed the issue with opening offscreen
* fixed workitem:10275, changed "Close" to "Finish" in the description of the final step of installer
* fixed the "hanging" of trying to delete a file. added the '-f' switch to never prompt.
	
#### Change log 0.8.4.3

* Additional steps to try to shutdown running adb server
* Publisher should only "tweet" once now. 
	
#### Change log 0.8.4.1

* Fixed the typo in the 2.1 platform tools file name

#### Change log 0.8.4.0

* Fixed issue with cyanogen 4.2.14.x
* fixed issue with bart manager plugin license (sorry for the delay)
* added 2.1 r1 platform tools
* added usb drivers r3 - adds support for nexus one
* fixed issue with install plugin crashing if application is already installed.
	
#### Change log 0.8.3.0

* Fixed bug with device not identifying recovery mode
* Display QR code to purchase bart manager if license not found.
* mount partitions in recovery mode to find bart manager license
* bart manager now attaches to process to output the info
* another attempt to set as root when starting for non-adp devices
* There is a known bug with droid explorer detecting a device already "connected" going in to recovery mode.
* Flash recovery now works in any mode, not just recovery
* bart manager now available for purchase on market.
	
#### Change log 0.8.2.3

* Drivers removed for windows XP until XP issue with the drivers can be resolved.
* added device icon for the nexus one / passion
* Code setup to allow some plugins to be purchased.

#### Change log 0.8.2.1

* Fixed install issues with 0.8.2.0 - should install for x86 and x64

#### Change log 0.8.2.0

* created a WiX custom action library for checking for the android usb drivers
* install logging is now merged in to one file and off by default. use /l[og] to turn on logging
* tools will always be downloaded/extracted during install. This lets the tool update to newer tools if needed
* fixed delete file when the file name has a space in the file name.
* sdk tools upgraded to r4
* fixed bug in uninstall if the service did not exist on the machine
* should now successfully check for the android usb drivers and install them if revision 2 is not installed.
* remember location of "open file dialog"
* added code that should check for .net 3.5 sp1 before "crashing" for not having it installed.
* added check that the user is installing the correct version (x86 or x64) for their system

#### Change log 0.8.1.0

* added a splash screen so the user is aware that the app is running right away
* .NET Framework 3.5 SP1 check added to installer.
* apk shell extension now uses the path stored in the registry for the tools.
* added logging to the install process
* kill all adb processes before attempting to cleanup the sdk path during install.
* apk seems to not be working, still debuging the issues. it will be back.
* defined a platform constant to the project scripts, x86: PLATFORMX86, x64: PLATFORMX64, ia64: PLATFORMIA64
* fixed bug with reading / writing to the registry in x64. now looks in HKLM\Software\WOW6432Node\
* fatal errors are now caught and user given option to restart app, close app, or report bug

#### Change log 0.8.0.1

* fixed issue with bootstrapper if there was not a proxy server used for internet explorer

#### Change log 0.8.0.0

* added the r2 windows usb drivers, which add support for droid and other devices
* added new checks for the drivers. Checks for dream/magic/sholes - these are the devices that 
	google defines in the .inf file.
* images moved to external resources library
* changed the installer images to be more "custom"
* removed need for droid explorer to require "run as administrator" - Yay!
* the bootstrapper now handles starting the service. This makes sure the sdk is setup before it starts.
* bootstrapper is self contained, the msi is an embedded resource and all referenced assemblies are ILMerged
* repository file is hosed on the droid explorer google code site, it is based on the same one that google uses for the android sdk setup
* the tools are also hosted on the google code site, this is so the download size is smaller, since all unused bits are removed.
* A fully standalone version of install is also available. No need for internet access to install.
* boot strapper support uninstall - setup.exe /uninstall
* removed reference to the "common.shared" assembly.
* added a properites dialog for folders/files
* properties dialog shows security settings as well.
* fixed icons in context menu for new folder/file
		
#### Change log 0.7.12.0

* fixed the error that anda.pk plugin logs when it starts because it did not implement "Runnable"
* shell extension for apk files so the apk icon displays in explorer - based on http://code.google.com/p/apkshellext/
* seems there is a bug in some APKs that dont display their icon, nor do they display the default icon.
* registers and unregisters the shell extension on install/uninstall
* driver check now works better in the installer  
* removed some tools menu items that have been replaced by plugins
* Installer now gives "options" on what features to install
* changed default apk icon to be the "android package" icon
* fixed bug with launching ddms and hierarchy viewer
* fixed bug launching the google applications backup plugin
* added methods to the core command runner to make a mount point read/write and readonly
* added icon for the motorola droid
* added icon for the samsung moment
* added icon for the htc droid eris
	
#### Change log 0.7.11.0

* usb drivers now included in the zip and installer
* drivers are installed to the DriverStore and installed when the device is attached the first time
* fixed logcat not starting
* create a new logcat console that colorizes the log entries
* save the logcat output (minus the log level indicator (W/I/D/E/)
* support for android screencast 0.2

#### Change log 0.7.10.1

* fixed issue with crashing when device state went from connected to disconnected

#### Change log 0.7.10.0

* Sign Package plugin (signs zip file with test keys)
* modified IPlugin to now have methods for creating the toolstrip button and menu items
* DroidExplorer.Core.Plugin.PluginHeler added. Contain default static methods for creating the toolstrip button and menu
* and.apk now a plugin instead part of "core"
* started a contacts manager
* started a tool to export facebook contacts from official application to android contacts
* added "recovery" as device state.
* now "attaches" while in recovery mode.
* screenshot now works in recovery mode.

#### Change log 0.7.9.0

* new shell console enhanced
* getprop wrapped to get device properties
* explorer icon now attempts to load an icon of the device
	- Known devices:
		- Bahamas (Tattoo)
		- GT-I7500 (Galaxy)
		- Hero (G2/Hero)
		- Sprint Hero (do not yet know what it identifies itself as in ro.product.device may just show normal hero)
		- cliq (need to verify what ro.product.device returns)
		- Saphire (MyTouch3G/ION/Magic)
		- Dream (G1/ADP1)
		- Pulse (need to verify what ro.product.device returns)
		- Zii Egg (need to verify what ro.product.device returns)
* Device properties viewable in options->environment->known devices->[device-serial]->properties (only when device is connected)
* fixed bug with screenshot image being landscape but window portrait.
* fixed bug with large icons not always showing the right icon
		
#### Change log 0.7.8.0

* Desktop SMS now launched from officially signed jar file
* Plugin tool strip disabled / enabled when device disconnected / connected
* SymLinks and Directories can now also be renamed
* F2 starts file/link/directory rename
* Executable files now run if double clicked (open from context menu)
* Shell Console (could still be buggy so the normal shell window is still available)
* double clicking sh scripts run them
* plugins that are registered as a file type handler will show up in the right click menu for the file

#### Change log 0.7.7.0

* Renaming of files (folders and links coming)
* Google Application Backup (GAB) plugin added
* GAB supports HTC's ADB1 update packages (containing system.img), pulling from the device or from normal update.zip
* Screen shot plugin supports portrait & landscape modes (use button or right click image)
* Screen shot threaded so it doesn't "hang"

#### Change log 0.7.6.0

* Fixed screen shot plugin from opening off screen if droid explorer is maximized or positioned on the right of the screen.
* Check for the USB Driver version and download the tool set based on that. If you select the sdk yourself, you must
	select the correct tools yourself.
* USB Driver Version info available in Options->Environment->Android SDK
* Speed up of navigating to different directory. Reduced the number of LS calls that are made to build the tree and listview
 
#### Change log 0.7.5.0

* Fixed plugins executing when loading within Runner
* Fixed bug with additional plugin getting the same changed values as other plugin.
* Change the SDK path from the Options dialog (requires restart of application)
* Fixed bug with apk's not displaying.
* Fixed Batch Installer not "showing"
* provided a way to manually install, start and stop the droid explorer service - for non-installer users
* added ability to save debug output to a file
* moved debug window filter buttons to the right
* debug window will display below DE if there is room, otherwise, it will display at the top of the screen
* added plugin to launch DesktopSMS (http://desktopsms.googlecode.com/). Requires the APK be installed on 
	the device (http://desktopsms.googlecode.com/files/DesktopSMSServer.apk)
* can now copy symlinks files to clipboard
	
#### Change log 0.7.4.0

* Changed the SdkInstallDialog to use a WebRequest instead of the WebClient. Hopefully this will help some peoples issues...
* Window settings are now saved and reloaded.
* Remembers the folder view state (large icon, details, etc)
* added --color=never to directory listing command. this should fix the issue people with Heros are having
* moved the options dialog tree config to its own file, as it really isnt configured by the user.
* added batch installer plugin (alpha) that can install/uninstall multiple apk's at one time.
* added logging info for droid explorer. (saved in %USERAPPDATA%\DroidExplorer\logs)
* it should also handle "unhandled" errors better
* wired up Tools menu items
* Added property to indicate if a plugin can be ran by the Runner
* Jumplist items added for runnable plugins

#### Change log 0.7.3.0

* app.manifest added to projects. requestedExecutionLevel = requireAdministrator. This means in Vista/Win7 it will prompt w/ UAC. I can not get around this at this time.
	this is because in order for the service to use the same settings as the application, i need to save them in the install directory.
* The sdk tools, if downloaded, is no longer stored in the user directory, it is stored in the install directory, see above for the reason.
* Known devices moved back to HKLM for the same reasons above.
* New Options form added.
* Device manager now part of options dialog
* plugins can now reside in any directory as long as it is added to the plugins settings
* plugins can now be enabled or disabled from the options dialog
* Service is now working because of the requireAdministrator change.
* DroidExplorer.Runner added - a tool that can execute a plugin; usage: DroidExplorer.Runner.exe /type=Full.Plugin.Class.Name,Plugin.Assembly.Name /any /additional /args
	if the type argument is not specified, then it will display a plugin selection dialog.
* Installer plugin. This is launched by the runner when an APK file is opened in Explorer.
* Registry settings to register .apk files to open with DroidExplorer.Runner /Installer
* .apk files can be installed by double clicking them or by right clicking and selecting "Install"
* .apk files can be uninstalled by right clicking and selecting "Uninstall"
* the initial device selection dialog is only required if more then 1 device is connected.
	 
#### Change log 0.7.2.0

* Added the Android SDK Tools package to the installer and binary zip files to fix the issue people were having with downloading
* Added about dialog
* Added Android Screencast plugin: The requirements for this application are still required, Droid Explorer does not fulfill them automatically
* Fixed license in installer to display correct license
* Logger now wraps log4net logging
* Service can be ran as a console app by passing /console when running it
	
#### Change log 0.7.0.0

* Fix for issues with running on Windows 7 (x86 & x64)
* Added Auto setup of sdk tools used by droid explorer
* There is still an issue with windows 7 not showing the device connected in explorer. (next release I will have this fixed)

#### Change log 0.6.0.0

* First version containing an installer
* Windows Service to monitor for connected devices
* Screen Shot Plugin
* Sqlite Editor Plugin
* Debugging Window Plugin
* Copy files from Device to Local Computer
* Copy files from Local Computer to Device
* Install Applications on Device
* Uninstall applications from Device
* x86 & x64 versions. (The x86 Service WILL NOT work on an x64 system)

#### Change log 0.5.0.0

* initial binaries beta release
* Requires .NET 3.5
* Tested on Windows XP, Windows Vista, Windows 7
* Tested with Rooted Android ADP1 (G1) Phone with cyanogen 3.9.x with 1.4 recovery image