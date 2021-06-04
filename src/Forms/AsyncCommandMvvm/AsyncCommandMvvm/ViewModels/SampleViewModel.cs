using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncCommandMvvm.ViewModels
{
    public class SampleViewModel : BaseViewModel
    {
        private readonly WeakEventManager<string> _errorOccurredEventManager = new WeakEventManager<string>();

        public  SampleViewModel()
        {
            SaveCommand = new AsyncCommand(OnSaveCommand);

            OnOtherCommand().SafeFireAndForget(ex => OnErrorOccurred(ex.ToString()));
        }

        public override Task Initialize()
        {
            this.Title = nameof(Initialize);
            return Task.CompletedTask;
        }

        private async Task OnSaveCommand()
        {
            this.Title = nameof(Initialize);

            var t = Task.Run(() =>
            {
                Title = "From Task";
                throw new Exception("test");
            });

            await t;
        }

        private async Task OnOtherCommand()
        {
            var t = Task.Run(() =>
            {
                throw new Exception("test");
            });

            await t;
        }

        public ICommand SaveCommand { get; set; }

        public event EventHandler<string> ErrorOccurred
        {
            add => _errorOccurredEventManager.AddEventHandler(value);
            remove => _errorOccurredEventManager.RemoveEventHandler(value);
        }
        void OnErrorOccurred(string message) => _errorOccurredEventManager.RaiseEvent(this, message, nameof(ErrorOccurred));
    }
}
