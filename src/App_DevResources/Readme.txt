
## Tasks

get latest validation result model
validation
	https://chaseflorell.github.io/xamarin/2017/10/04/realtime-validation-in-xamarin-forms-with-fluentvalidation/

Add sample for splash screen
icons binding from view model StaticResource
prefix UI samples with UI_
read about tags
database encryption
dev dock for API
https://zapier.com/blog/how-to-use-tags-and-labels/
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/images?tabs=windows
clone & xamarin samples
localization
telemetry
	See https://docs.microsoft.com/en-us/appcenter/
	dbsite, navigation, last used,location,errors and many more
https://developer.okta.com/blog/2018/02/01/secure-aspnetcore-webapi-token-auth


## Fea
date time settings and formats if used for imput

## Next
https://docs.microsoft.com/en-us/xamarin/get-started/tutorials/


review
https://github.com/bramborman/UWPHelper 


## Other

device_id can be used with apikey as a custom attribute

0	Connected
1	No Internet Access
2	Invalid or expired token

Task<string> CanConnect() // returns resource_key
{
	successful
	no internet
	invalid_grant token expired or invalid
}
Enum
	No connection
	Has connection & invalid_grant
	Success

guid validator
	guid
	propertyName

on change validate
	if (validate.ToModel(model) == true will add into model and do binding
		save then

calc hash for each file
create etag for each file wheter it changed
data stored should have etag and check before send otherwise just send not changed

## other, server api should have build api guid to ensure that api is not fake, client must be able to verify that

Etag for api
https://docs.microsoft.com/en-us/powerapps/developer/common-data-service/webapi/perform-conditional-operations-using-web-api
	
## Throoting
	should be able throot requests per user
	
## API Tokens
	See Google API key
	shall be able generate token for the use to be used for example 2 weeks
		valid from 60 minutes expire 24 hours
	https://blog.timekit.io/google-oauth-invalid-grant-nightmare-and-how-to-fix-it-9f4efaf1da35
	https://docs.apigee.com/api-platform/antipatterns/oauth-long-expiration
	https://medium.com/shoutem/keeping-your-api-tokens-fresh-72059a7b0586
	https://www.oauth.com/oauth2-servers/access-tokens/access-token-lifetime/
	https://dzone.com/articles/security-best-practices-for-managing-api-access-to
	Rhino licencing use for API key that will have pairs what can sync get|post. The APi key is from licence ID
