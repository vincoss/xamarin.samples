using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_EnterpriseApp_Authentication.Models;
using XamarinForms_EnterpriseApp_Authentication.Services;


namespace XamarinForms_EnterpriseApp_Authentication.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IMicrosoftAuthService _microsoftAuthService;
        private bool _isLoading;
        private User _user;
        private string _errorMessage;

        public HomeViewModel(IMicrosoftAuthService microsoftAuthService)
        {
            this._microsoftAuthService = microsoftAuthService;
            SignInCommand = new Command(async () => await SignInAsync());
            SignOutCommand = new Command(async () => await SignOutAsync());
        }

        public override void Initialize()
        {
            _microsoftAuthService.Initialize();
        }

        public ICommand SignInCommand { get; }
        public ICommand SignOutCommand { get; }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public async Task SignInAsync()
        {
            try
            {
                this.IsLoading = true;
                this.User = await this._microsoftAuthService.OnSignInAsync();
            }
            catch (Exception ex)
            {
                // Manage the exception as you need, you can display an error message using a popup.
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                
                ErrorMessage = ex.ToString();
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        public async Task SignOutAsync()
        {
            try
            {
                this.IsLoading = true;
                await this._microsoftAuthService.OnSignOutAsync();
            }
            catch (Exception ex)
            {
                // Manage the exception as you need, you can display an error message using a popup.
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                ErrorMessage = ex.ToString();
            }
            finally
            {
                // Remove the user on the view just for demo purpose
                this.User = null;
                this.IsLoading = false;
            }
        }
    }
}
