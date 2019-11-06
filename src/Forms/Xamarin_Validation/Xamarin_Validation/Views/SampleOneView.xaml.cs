using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Validation.Model;
using Xamarin_Validation.Validation;
using Xamarin_Validation.Validators;
using Xamarin_Validation.ViewModels;

namespace Xamarin_Validation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SampleOneView : ContentPage
    {
        public SampleOneView()
        {
            InitializeComponent();

            BindingContext = new SampleOneViewModel(new ValidatorRegistry());
        }

        public class SampleOneViewModel : BaseViewModel
        {
            private readonly IValidatorFactory _validatorFactory;
            private readonly ModelStateDictionary _modelState = new ModelStateDictionary();

            public SampleOneViewModel(IValidatorFactory validatorFactory)
            {
                _validatorFactory = validatorFactory;

                modelState.AddError("UserName", "Value is required");
                OkCommand = new Command(OnOkCommand);
            }

            private ModelStateDictionary modelState = new ModelStateDictionary();

            public void OnOkCommand(object args)
            {
                var userObj = new UserInfo
                {
                    UserName = UserName
                };

                _validatorFactory.GetValidator<UserValidator>().ToModel(_modelState);

                var validationResults = _validatorFactory.GetValidator<UserValidator>().Validate(userObj);

                if (validationResults.IsValid)
                {
                    App.Current.MainPage.DisplayAlert("FluentValidation", "Validation Success..!!", "Ok");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("FluentValidation", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }

            public ICommand OkCommand { get; private set; }

            public string this[string propertyName]
            {
                get
                {
                    string error = null;
                    if (modelState.ContainsKey(propertyName))
                    {
                        var items = modelState[propertyName];
                        error = items.FirstOrDefault();
                    }
                    return error;
                }
            }

            string _userName;
            public string UserName
            {
                get { return _userName; }
                set { SetProperty(ref _userName, value); }
            }
        }
    }
}