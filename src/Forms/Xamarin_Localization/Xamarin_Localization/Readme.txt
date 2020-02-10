## Based on
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization/text?tabs=windows
https://www.c-sharpcorner.com/article/implemented-localization-in-a-xamarin-forms-app/
https://medium.com/p/5e6bc15c758/responses/show

## Lookup
At runtime, the application attempts to resolve a resource request in order of specificity. 
For example, if the device culture is en-US the application looks for resource files in this order:

AppResources.en-US.resx
AppResources.en.resx
AppResources.resx (default)