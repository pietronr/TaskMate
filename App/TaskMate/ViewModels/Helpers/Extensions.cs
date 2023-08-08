using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace TaskMate.ViewModels.Helpers
{
    public static class Extensions
    {
        public static void RefreshCollection<T>(this ObservableCollection<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            CollectionViewSource.GetDefaultView(nameof(collection)).Refresh();
        }
    }
}
