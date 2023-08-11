using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskMate.ViewModels
{
    public class BindableBase : INotifyPropertyChanged
    {
        private bool _isNewEntry;
        public bool IsNewEntry
        {
            get => _isNewEntry;
            set => Set(ref _isNewEntry, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool Set<T>(ref T storage, T value,
            [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
