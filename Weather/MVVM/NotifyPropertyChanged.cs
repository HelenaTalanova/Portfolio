using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MVVM
{
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEvent(propertyName);
        }

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
                PropertyChangedEvent(propertyName);
        }

        protected bool Set<T>(ref T sender, T value, [CallerMemberName]string propertyName = default)
        {
            if (sender == null || (!sender.Equals(value)))
            {
                sender = value;

                PropertyChangedEvent(propertyName);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Получает обьект, и имя редактируемого свойства
        /// </summary>
        /// <typeparam name="T">Тип обьекта</typeparam>
        /// <typeparam name="V">Тип задаваемого значения</typeparam>
        /// <param name="sender">Редактируемый обьект</param>
        /// <param name="propertyName">Имя редактируемого свойства</param>
        /// <param name="value">Новое значение свойства</param>
        public bool Set<T, V>(T sender, string propertyNameAccess, V value, [CallerMemberName] string propertyName = default)
        {
            if ((sender == null || (!sender.Equals(value))) && propertyNameAccess != null)
            {
                sender.GetType().GetProperty(propertyNameAccess).SetValue(sender, value);

                PropertyChangedEvent(propertyName);

                return true;
            }

            return false;
        }

        public T Set<T>(T value, [CallerMemberName] string propertyName = default)
        {
            PropertyChangedEvent(propertyName);
            return value;
        }

        private void PropertyChangedEvent(string propertyName)
        {
            VerifyPropertyName(propertyName);

            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new Exception("Invalid property name: " + propertyName);
            }
        }
    }
}
