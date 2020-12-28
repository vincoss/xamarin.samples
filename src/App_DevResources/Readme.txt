
## Tasks to learn


## UWP sign package example
https://docs.microsoft.com/en-us/windows-hardware/drivers/dashboard/get-a-code-signing-certificate
https://stackoverflow.com/questions/329396/what-happens-when-a-code-signing-certificate-expires
http://www.cacert.org/
https://comodosslstore.com/code-signing
https://docs.microsoft.com/en-us/windows-hardware/drivers/install/authenticode

## Xamarin to read, samples to read & research
skia sharp touch not working on iOS
items list-item design template create
	put those into DesignUi item not runnable
	one design must be self contained all colors and layout items
	see outlook item or hotmail item for ideas

## Xamarin Commands permission (Role-Profile-Readme.txt) samples to read & research
At first list functionality
	pages, buttons and other
commands|Menu items hide if no rights
	project hide create, edit, delete actions (with edit rule),import,export
		ItemAddUpdateDelete
		ItemActivateDeactivate
	Enable|Disable
	ADD|Edit
	each action
	Delete (need soft delete)
	all project or site (read only)
	possible disable whole pages
		Import
		Export
show|hide buttons
show|hide menu items
show|hide grid actions
show|hide create button
Read only
 can't edit Items, site, tags, jobs
 can't data import
 cant share (what?)
 Server
	can read  Items, site, tags, jobs
	can create workspace items
enable edit|delete|deactivate and other action
	have profiles for that, controlled from server side
	this might not be visisble for the user if server integration
control app from serverside what is available for the UI
	actions
	pages
	navigation
server side settings (key-value) JSON

sample with table view
	keydata, name, path, createdDate, modifiedDate, activityDate, dataSize, otherSize, version
	actions, backup, update, share, clear
MenuItem from class instead of xaml
viss git logo
font awsome icons samples
image zoon and pan, see gestures
Master view
RefreshView 
Image - IsAnimationPlaying

##localization
	see xamarin-fomms-samples TodoLocalized
	see xamarin-fomms-samples TodoLocalizedRTL
	see xamarin-fomms-samples UsingResxLocalization
	see xamarin-fomms-samples UsingResxLocalization
	C:\_Data\GitHub\Xamarin\docs-archive\Samples\TodoLocalized
	C:\_Data\GitHub\Xamarin\docs-archive\Samples\UsingResxLocalization
	https://www.romaniancoder.com/set-culture-for-all-threads-in-c-apps/
	https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization/

telemetry
	See https://docs.microsoft.com/en-us/appcenter/
	dbsite, navigation, last used,location,errors and many more


## Android keytool

+Create Android Sign Key
+	C:\Program Files (x86)\Java\jre1.8.0_261\bin
+	keytool -genkey -keyalg RSA -keysize 4096 -validity 36000 -storepass Pass@word1 -v -keystore dev-key.jks -alias dev-key

## Next
ListView
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/
https://github.com/dotnet-architecture/News
https://dotnet.microsoft.com/learn/dotnet/architecture-guides

review
https://github.com/Daddoon/BlazorMobile
https://github.com/bramborman/UWPHelper 
https://github.com/XLabs/Xamarin-Forms-Labs
