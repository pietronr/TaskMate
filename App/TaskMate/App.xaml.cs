using System.Windows;
using TaskMate.ViewModels;

namespace TaskMate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainViewModel MainViewModel { get { return (MainViewModel)Current.Resources["MainViewModel"]; } }
    }
}
