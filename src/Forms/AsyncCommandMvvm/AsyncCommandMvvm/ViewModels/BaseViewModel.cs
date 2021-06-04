using AsyncAwaitBestPractices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AsyncCommandMvvm.ViewModels
{
    //public class BaseViewModel : INotifyPropertyChanged
    //{
    //    public virtual Task Initialize()
    //    {
    //        return Task.CompletedTask;
    //    }

    //    bool isBusy = false;
    //    public bool IsBusy
    //    {
    //        get { return isBusy; }
    //        set { SetProperty(ref isBusy, value); }
    //    }

    //    string title = string.Empty;
    //    public string Title
    //    {
    //        get { return title; }
    //        set { SetProperty(ref title, value); }
    //    }

    //    protected bool SetProperty<T>(ref T backingStore, T value,
    //        [CallerMemberName]string propertyName = "",
    //        Action onChanged = null)
    //    {
    //        if (EqualityComparer<T>.Default.Equals(backingStore, value))
    //            return false;

    //        backingStore = value;
    //        onChanged?.Invoke();
    //        OnPropertyChanged(propertyName);
    //        return true;
    //    }

    //    #region INotifyPropertyChanged
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    public void OnPropertyChanged([CallerMemberName] string propertyName = "")
    //    {
    //        var changed = PropertyChanged;
    //        if (changed == null)
    //            return;

    //        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //    #endregion
    //}

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly WeakEventManager _propertyChangedEventManager = new WeakEventManager();

        public virtual Task Initialize()
        {
            return Task.CompletedTask;
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => _propertyChangedEventManager.AddEventHandler(value);
            remove => _propertyChangedEventManager.RemoveEventHandler(value);
        }

        protected void SetProperty<T>(ref T backingStore, in T value, in Action? onChanged = null, [CallerMemberName] in string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
        }

        void OnPropertyChanged([CallerMemberName] in string propertyName = "") =>
            _propertyChangedEventManager.RaiseEvent(this, new PropertyChangedEventArgs(propertyName), nameof(INotifyPropertyChanged.PropertyChanged));
    }
}