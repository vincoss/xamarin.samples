﻿using FluentValidation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Validation.Model;
using Xamarin_Validation.Validation;
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
            private readonly IValidatorRegistry _validatorFactory;
            private readonly ModelStateDictionary _modelState = new ModelStateDictionary();

            public SampleOneViewModel(IValidatorRegistry validatorFactory)
            {
                _validatorFactory = validatorFactory;

                OkCommand = new Command(OnOkCommand);
            }

            public void OnOkCommand(object args)
            {
                var userObj = new UserInfo
                {
                    UserName = UserName
                };

                _validatorFactory.GetValidator<UserValidator>().ValidateToModel(userObj, _modelState);

                OnPropertyChanged("Item");
            }

            public ICommand OkCommand { get; private set; }

            [IndexerName("Item")]
            public string this[string propertyName]
            {
                get
                {
                    string error = null;
                    if (_modelState.ContainsKey(propertyName))
                    {
                        var items = _modelState[propertyName];
                        error = items.FirstOrDefault();
                    }
                    return error;
                }
            }

            private string _userName;

            public string UserName
            {
                get { return _userName; }
                set { SetProperty(ref _userName, value); }
            }
        }
    }
}