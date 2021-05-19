using System;
using System.Collections.Generic;
using System.Text;
using TinyIoC_Sample.Services;


namespace TinyIoC_Sample.ViewModels
{
    public class SampleViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;

        public SampleViewModel(IDatabaseService service)
        {
            _databaseService = service;
        }

        public override void Initialize()
        {
            DateTimeString = _databaseService.Get();
        }


        private string _dateTimeString;

        public string DateTimeString
        {
            get { return _dateTimeString; }
            set { SetProperty(ref _dateTimeString, value); }
        }
    }
}
