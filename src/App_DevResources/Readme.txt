
## Tasks to learn

see eshop cont data review code
review all xamarin-forms-samples	
name abbreviation, see JIRA and TeamCity

listview check box
picker large search
picker add new|edit|import

embed metatata into img
light csv import
read docker networking
font awsome icons samples
canon uwp

read about tags
https://stackoverflow.blog/2016/12/15/you-can-now-play-with-stack-overflow-data-on-googles-bigquery/
https://stackoverflow.blog/2019/07/24/making-sense-of-the-metadata-clustering-4000-stack-overflow-tags-with-bigquery-k-means/
https://meta.stackexchange.com/questions/22624/what-symbols-characters-are-not-allowed-in-tags
https://meta.stackexchange.com/questions/103492/what-characters-does-stack-overflow-allow-in-tags?noredirect=1&lq=1
https://meta.stackoverflow.com/questions/272625/what-is-the-length-limit-on-tag-names
https://stackoverflow.com/help/tagging
https://zapier.com/blog/how-to-use-tags-and-labels/
https://zapier.com/blog/organize-files-folders/
https://www.google.com/search?rlz=1C1GCEA_enAU847AU847&ei=8kPOXeT2KMKe9QOvk5SgCg&q=gmail+labels&oq=gmail+labels&gs_l=psy-ab.3..0i273j0l9.3005.3285..3526...0.3..0.175.475.0j3......0....1..gws-wiz.......0i71j0i67.idD9X0zVspU&ved=0ahUKEwjkt7biyevlAhVCT30KHa8JBaQQ4dUDCAs&uact=5
https://evernote.com/
https://medium.com/@thomashoneyman/using-evernote-the-right-way-ef61f530d1ad#.vehf4zh6o

localization
	see xamarin-fomms-samples TodoLocalized
	see xamarin-fomms-samples TodoLocalizedRTL
	see xamarin-fomms-samples UsingResxLocalization
	see xamarin-fomms-samples UsingResxLocalization
	https://www.romaniancoder.com/set-culture-for-all-threads-in-c-apps/

telemetry
	See https://docs.microsoft.com/en-us/appcenter/
	dbsite, navigation, last used,location,errors and many more

## Next
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/

review
https://github.com/Daddoon/BlazorMobile
https://github.com/bramborman/UWPHelper 

## Other

test black and white colors
have base color in app resources to apply black and white only 

IsBusy = true;

start pae into Xamarin.Essentials.preferences
Hanselman.Forms to review

add global search
will search tags and list items

tag each item with project, location, and other

## Stackoverflow technology
https://meta.stackexchange.com/questions/10369/which-tools-and-technologies-are-used-to-build-the-stack-exchange-network?rq=1
https://stackexchange.com/performance


async Task ExecuteLoadCommand(bool forceRefresh)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
#if DEBUG
                await Task.Delay(1000);
#endif
                var items = await DataService.GetBlogItemsAsync(forceRefresh);
                if(items == null)
                {
                    await DisplayAlert("Error", "Unable to load blog.", "OK");
                }
                else
                {
                    FeedItems.ReplaceRange(items);
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Unable to load blog.", "OK");
            }
            finally
            {
                IsBusy = false;
            }

            LoadCommand.ChangeCanExecute();
        }