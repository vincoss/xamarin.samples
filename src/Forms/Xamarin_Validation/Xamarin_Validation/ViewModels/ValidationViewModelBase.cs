using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin_Validation.Validation;

namespace Xamarin_Validation.ViewModels
{
    public abstract class ValidationViewModelBase : BaseViewModel
    {
        private readonly ModelStateDictionary _modelState = new ModelStateDictionary();

        public ModelStateDictionary ModelState
        {
            get { return _modelState; }
        }

        [IndexerName("Item")]
        public string this[string propertyName]
        {
            get
            {
                return _modelState.GetValue(propertyName);
            }
        }

        public bool IsValid
        {
            get { return _modelState.IsValid == false; }
        }
    }
}
