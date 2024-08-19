using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CanonCameraExternal_Sample_WPF.Xaml
{
    public abstract class Command : ICommand
    {
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        public abstract void Execute(object parameter);

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        #endregion

        protected void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }

    public class DelegateCommand : Command
    {
        private readonly Action _excecute;
        private readonly Func<bool> _canExecute;

        public DelegateCommand(Action execute) : this(execute, () => true)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExcute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            this._excecute = execute;
            this._canExecute = canExcute;
        }

        public override void Execute(object parameter)
        {
            _excecute();
        }

        public override bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute();
            }
            return true;
        }
    }

    public class DelegateCommand<T> : Command
    {
        private readonly Action<T> _excecute;
        private readonly Func<T, bool> _canExecute;

        public DelegateCommand(Action<T> execute) : this(execute, (arg) => true)
        {
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExcute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            this._excecute = execute;
            this._canExecute = canExcute;
        }

        public override void Execute(object parameter)
        {
            _excecute((T)parameter);
        }

        public override bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute((T)parameter);
            }
            return true;
        }
    }
}
