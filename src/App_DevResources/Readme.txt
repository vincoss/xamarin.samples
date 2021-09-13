
## Tasks to learn


## UWP sign package example
https://docs.microsoft.com/en-us/windows-hardware/drivers/dashboard/get-a-code-signing-certificate
https://stackoverflow.com/questions/329396/what-happens-when-a-code-signing-certificate-expires
http://www.cacert.org/
https://comodosslstore.com/code-signing
https://docs.microsoft.com/en-us/windows-hardware/drivers/install/authenticode
https://github.com/xamarin/XamarinCommunityToolkit


collection view sample
    create new project
    create a collection view with add funtionality, on same page, must modify the title also
    move that into maui to test

scrollView horizontal sample
    collection view multiple
    auto fit screen
    auto width, see other layouts

Page title sample
    back arrow
    editable
    done button if has changes
    autosave on lost focus or done key

complete image drawing sample
	image drawing render gird over, have option to show on ride, grid with proper scale


create a sample
	Carousel
		swipeview
		List view actions

.dotnet core see custom IOC, TinyIoCContainer wheter is worth


CollectionView items reorder, drag & drop
TableView for forms? see xamarin samples, create sample imput form
	TableView for forms, create sample, review and update existing
update filePicker sample Task PickAndShowFile(string[] fileTypes)
	pass file types for each platform ".db3", ".DB3"
signature update full screen, review other drawing
create address entry form, try to use also GPS picker to get location, this might require WIFI
home screen (splash fickers android emulator) possible shown multiple times. check on default project or google that







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
MenuItem from class instead of xaml

## Done


review and create maui sample, GeolocationService
+create drawer actions view
+	see swipeview and others
+tabbedpage like an kanban board
+	default sample review
+		Flyout template
+		Tabbed
+		Carousel view
+			columns (Tile, Colour, Icon)
+			https://www.ithinkdiff.com/the-tasks-app-app-store-available/
+			https://apps.apple.com/au/app/tasks-to-do-lists-reminders/id1502903102#?platform=ipad
+			https://www.workast.com/help/articles/61000165208/
+create console host sample
+validation sample
+	phone, mobile (use Telephone keyboard)
++	    // https://stackoverflow.com/questions/5066329/regex-for-valid-international-mobile-phone-number
+		RuleFor(customer => customer.Surname).Matches("^\+[1-9]{1}[0-9]{3,14}$");
+	email (Email keyboard)
+		RuleFor(customer => customer.Email).EmailAddress();
+	URL (Url keyboard)
+		RuleFor(x => x.SomeUri).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.SomeUri));
+Finish the Dapper Sqlite sample provider samples, see fluentmigration with dapper
+see JIRA itemId and the move to another project (Move change the ID based on targed project count)
+	ProjectA-3657
+	ProjectB-796
+review Entry_Focus each platform
+Focus & Keyboard On new item set focus and show keyboard, see android contacts
+swipe action to remove items right or left is default? (Left to delete)
+floating label entry
+square button
+url add http prefix and sample
+swipe action to remove items right or left is default? Left
+home screen (splash fickers android emulator) possible shown multiple times. check on default project or google that. Works well on splash sample

## Speech
+speach to text sample (offline) there are not much at a moment

## Android keytool
+Create Android Sign Key
+	C:\Program Files (x86)\Java\jre1.8.0_261\bin
+	keytool -genkey -keyalg RSA -keysize 4096 -validity 36000 -storepass Pass@word1 -v -keystore dev-key.jks -alias dev-key

## Performance
review all layout use just grids and stacklayout
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/deploy-test/performance


## Next
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/
https://github.com/dotnet-architecture/News
https://dotnet.microsoft.com/learn/dotnet/architecture-guides

review
https://github.com/Daddoon/BlazorMobile
https://github.com/XLabs/Xamarin-Forms-Labs


      

  public class ProjectListViewModel : BaseViewModel
    {
        private readonly IProjectDataStore _projectStore;
        private readonly INavigator _navigator;
        private readonly IApplicationContext _actx;
        private readonly IViewModelFactory _viewModelFactory;
        private string _search;
        private SortType _sortColumn = SortType.Popular;
        private readonly static InbuiltLogger Logger = InbuiltLog.For(typeof(ProjectListViewModel));

        public ProjectListViewModel(IApplicationContext actx, IProjectDataStore database, INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _actx = actx ?? throw new ArgumentNullException(nameof(actx));
            _projectStore = database ?? throw new ArgumentNullException(nameof(database));
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            AddCommand = new Command(OnAddCommand);
            EditCommand = new Command<ProjectListItemDto>(OnEditCommand);
            DeleteCommand = new Command<ProjectListItemDto>(OnDeleteCommand);

            SearchCommand = new Command<string>(OnSearchCommand);
            LoadMoreCommand = new Command(OnLoadMoreCommand, OnCanLoadMoreCommand);
            RefreshCommand = new Command(OnRefreshCommand);
            SortCommand = new Command<string>(OnSortCommand);

            ItemsSource = new ObservableRangeCollection<ProjectListItemDto>();
            _actx.TrackEvent(nameof(ProjectListViewModel));
        }

        public override void Initialize()
        {
            ItemsSource.Clear();
            OnSearchDataAsync();
        }

        #region Private methods

        private async void OnSearchDataAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                var args = new SearchDataParameterDto
                {
                    Search = _search,
                    SortColumn = _sortColumn
                };
                args.Skip = (int)((ItemsSource.Count / (double)args.Take) * args.Take);

                var items = await _projectStore.ProjectListGetAsync(args);

                // TODO: temp only remove
                var query = ItemsSource.GroupBy(x => x.ProjectId)
                          .Where(g => g.Count() > 1)
                          .Select(y => y.Key)
                          .ToList();

                ItemsSource.AddRange(items);

                if (query.Any())
                {
                    throw new InvalidOperationException("There are duplicates");
                }
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        #endregion

        #region Command methods

        private async void OnAddCommand()
        {
            await _navigator.PushAsync(typeof(ProjectAddUpdateView));
        }

        private async void OnEditCommand(ProjectListItemDto args)
        {
            if (args == null || IsBusy)
            {
                return;
            }

            var viewModel = _viewModelFactory.Get<ProjectAddUpdateViewModel>();
            viewModel.ProjectId = args.ProjectId;

            await _navigator.PushAsync(typeof(ProjectAddUpdateView), viewModel);
        }

        private async void OnDeleteCommand(ProjectListItemDto args)
        {
            if (args == null || IsBusy)
            {
                return;
            }

            try
            {
                if (await _actx.DisplayAlertAsync(AppResources.Delete, AppResources.DeleteConfirmMessage, AppResources.Ok, AppResources.Cancel) == false)
                {
                    return;
                }

                IsBusy = true;
                var result = await _projectStore.ProjectDeleteAsync(new[] { args.ProjectId }); // TODO: multi delete
                if (result)
                {
                    ItemsSource.Remove(args);
                }
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        private void OnSearchCommand(string search)
        {
            _search = search;
            Initialize();
        }

        private void OnLoadMoreCommand()
        {
            OnSearchDataAsync();
        }

        private bool OnCanLoadMoreCommand()
        {
            return IsBusy == false;
        }

        private void OnRefreshCommand()
        {
            Initialize();
        }

        private void OnSortCommand(string arg)
        {
            _sortColumn = (SortType)Enum.Parse(typeof(SortType), arg);
            Initialize();
        }

        #endregion

        #region Commands

        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ICommand SearchCommand { get; private set; }
        public ICommand LoadMoreCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand SortCommand { get; private set; }

        #endregion

        private ProjectListItemDto _selectedItem;

        public ProjectListItemDto SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ObservableRangeCollection<ProjectListItemDto> ItemsSource { get; private set; }
    }
    


<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             x:Class="Udar.Views.ProjectListView"
             xmlns:xa="clr-namespace:Udar.Xaml;assembly=Udar"
             xmlns:vm="clr-namespace:Udar.ViewModels;assembly=Udar"
             xmlns:sh="clr-namespace:Udar.Views.Shared;assembly=Udar"
             xmlns:do="clr-namespace:Udar.Interface.Dto;assembly=Udar.Interface"
             xmlns:fl="clr-namespace:Udar;assembly=Udar"
             xmlns:co="clr-namespace:Udar.Interface;assembly=Udar.Interface"
             fl:ViewModelLocator.AutoWireViewModel="true"
             BackgroundColor="White"
             x:Name="this"
             Title="{xa:Translate Projects}"
             x:DataType="vm:ProjectListViewModel">

    <StackLayout Margin="5">

        <sh:SeachBarView/>
                
                <RefreshView IsRefreshing="{Binding IsRefreshing}" 
                             Command="{Binding RefreshCommand}"
                             RefreshColor="{StaticResource refreshColor}">

                    <CollectionView x:Name="collectionViewItems"
                            HorizontalOptions="FillAndExpand"
                            EmptyView="{xa:Translate NoItems}"
                            RemainingItemsThreshold="5"
                            RemainingItemsThresholdReachedCommand="{Binding LoadMoreCommand}"
                            Scrolled="collectionViewItems_Scrolled"
                            ItemsSource="{Binding ItemsSource}">
                        <CollectionView.ItemTemplate>
                            <DataTemplate x:DataType="do:ProjectListItemDto">
                                <SwipeView>
                                    <SwipeView.LeftItems>
                                        <SwipeItem Text="{xa:Translate Edit}" BackgroundColor="LightGreen"  
                                           CommandParameter="{Binding .}"
                                           Command="{Binding Path=BindingContext.EditCommand, Source={x:Reference this}}"/>
                                        <SwipeItem Text="{xa:Translate Delete}" IsDestructive="true" BackgroundColor="LightPink" 
                                           CommandParameter="{Binding .}" 
                                           Command="{Binding Path=BindingContext.DeleteCommand, Source={x:Reference this}}"/>
                                    </SwipeView.LeftItems>

                                    <StackLayout>
                                        <Label Text="{Binding Name}" FontSize="Title"/>
                                        <xa:VisibilityLabel Text="{Binding Description}" FontSize="Body" MaxLines="2" LineBreakMode="TailTruncation"/>
                                        <xa:VisibilityLabel Text="{Binding CompanyName}" FontSize="Body"/>
                                        <StackLayout Orientation="Horizontal" IsVisible="{Binding ShowCounts}">
                                            <Label Text="{xa:Translate Jobs}"/>
                                            <Label Text="{Binding JobCount}"/>
                                            <Label Text="{xa:Translate Items}"/>
                                            <Label Text="{Binding ItemCount}"/>
                                        </StackLayout>
                                        <BoxView HeightRequest="1" HorizontalOptions="FillAndExpand" Color="Black"/>
                                    </StackLayout>
                                </SwipeView>
                            </DataTemplate>
                        </CollectionView.ItemTemplate>
                    </CollectionView>
                </RefreshView>

        <Button Text="+"
                FontSize="60"
                FontAttributes="None"
                BackgroundColor="#FBBC05"
                TextColor="White" 
                Padding="0,-15,0,0"
                Margin="5"
                BorderWidth="1"
                BorderColor="Black"
                CornerRadius="30"
                WidthRequest="60"
                HeightRequest="60"
                HorizontalOptions="End" VerticalOptions="End"
                Command="{Binding AddCommand}"/>
                
    </StackLayout>
      
</ContentPage>


## Template sample


    /* 
        board to store original template (this has a version)
        board to store modified template
        TODO: here must define all template,
        each time we modify the default template we increment the version 
        declare in code and then serialize into the board
        store as json
        
        types
        text
        number
        datetime

        attributes
            name
            order

        ##
        Description enabled
        Attachments enabled
        External Link
        Card Type   enabled
        Priority    enabled
        Due Date    enabled
        Assignment  enabled
        External ID this can be Card GUID
        Estimated time
        Task Difficulty int
        Tags    string comma separated
        
        Custom Fields (there are 15)
            label:  (ID) string
        Types: text Field
                    placeholder
                    format
                    allow multiline (can have separated custom type)
                Weblink (just entry control)
                number
                    placeholder
                dropdwon with options
                    options semicolumn separated values
                    allow multi selection
                multiple choice grid
                    options semicolumn separated values
                    allow multi selection
                date this is basic date picker
                User List
                    allow multiple selection
                email address basic entry control 
            
                
                
        

    */

    public class BoardTemplateDto
    {
        public ushort Version
        {
            get { return 1; }
        }


        public string Description { get; set; }
    }

    public class BoardDefaultTemplateDto
    {
        public ushort Version
        {
            get { return 1; }
        }


    }

    public interface IBoardCardTemplate
    {
        string Name { get; set; }
        string UITemplateKey { get; }
        string UITemplateConfigurationKey { get; }
    }

    public class DescriptionTemplate : IBoardCardTemplate
    {
        public string Value { get; set; }
        public string Placeholder { get; set; }
        public string Name { get; set; }

        public string UITemplateKey { get; } = "BoardCardDescriptionKey";

        public string UITemplateConfigurationKey { get; } = "BoardCardConfigurationDescriptionKey";
    }

    public class AttachmentsTemplate
    {

    }

    public class ExternalLink
    {
        public string Value { get; set; }
        public string Placeholder { get; set; }
    }

    public class Sample
    {
        private ISet<IBoardCardTemplate> _availableTemplates = new HashSet<IBoardCardTemplate>();
        private IDictionary<string, ISet<IBoardCardTemplate>> _cardTypeTemplates = new Dictionary<string, HashSet<IBoardCardTemplate>();

        public Sample()
        {
            var description = new DescriptionTemplate
            {
                Name = "Description",
                Placeholder = "Optional"
            };

            _availableTemplates.Add(description);


            _cardTypeTemplates.Add("Notes", description);

        }



        /*
            
            #Default tempalte 
            TemplateName
            Order
            
            #Example
            Description 0
            Assigned    1
            Due Date     2

               NOTE:
               decription
               assigned
               due date

               _cardTypes.Add("Note", defaultTemplate);

           */
    }
