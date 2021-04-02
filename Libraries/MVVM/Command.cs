using System;
using System.Windows.Input;

namespace MVVM
{
    public class Command : ICommand
    {
        readonly Func<bool> _canExecute;
        readonly Action _execute;

        public Command(Action execute)
        {
            _execute = execute;
            _canExecute = () => true;
        }

        public Command(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute() : true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (_canExecute == null)
                _execute?.Invoke();
            else if (_canExecute())
                _execute?.Invoke();
        }

        public static void UpDataCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
