## Based on
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization/text?tabs=windows
https://www.c-sharpcorner.com/article/implemented-localization-in-a-xamarin-forms-app/
https://medium.com/p/5e6bc15c758/responses/show
https://www.romaniancoder.com/set-culture-for-all-threads-in-c-apps/
https://docs.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo.defaultthreadcurrentculture?redirectedfrom=MSDN&view=netframework-4.8#System_Globalization_CultureInfo_DefaultThreadCurrentCulture
https://docs.microsoft.com/en-us/xamarin/cross-platform/app-fundamentals/localization

## Lookup
At runtime, the application attempts to resolve a resource request in order of specificity. 
For example, if the device culture is en-US the application looks for resource files in this order:

AppResources.en-US.resx
AppResources.en.resx
AppResources.resx (default)

## Ensure default assembly culture
[assembly: NeutralResourcesLanguage("en")]