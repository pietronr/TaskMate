using System.Threading.Tasks;
using System.Windows.Input;
using TaskMate.Models.Settings;
using TaskMate.Views.Dialogs;

namespace TaskMate.ViewModels.Settings
{
    public class SettingsShellViewModel : BindableBase
    {
        protected Surfaces.DialogManager? dialogManager;

        public SettingsShellViewModel(bool initialize)
        {
            if (initialize)
            {
                dialogManager = new Surfaces.DialogManager();
                OpenSettingsCommand = new AsyncRelayCommand(OpenSettings);
            }
        }

        public SettingsShellViewModel() : this(true)
        {
        }

        #region Main properties

        public UserSettingsViewModel? CurrentSettings { get; set; }

        #endregion

        #region View properties

        private UserSettingsViewModel? _previousSettings;
        public UserSettingsViewModel? PreviousSettings
        {
            get => _previousSettings;
            set => Set(ref _previousSettings, value);
        }

        #endregion

        #region Commands

        public ICommand? OpenSettingsCommand { get; set; }

        #endregion

        #region Methods

        private async Task OpenSettings()
        {
            if (CurrentSettings?.IsNewEntry == false)
            {
                UserSettings previous = CurrentSettings.GetFullModel().Duplicate();
                PreviousSettings = new UserSettingsViewModel(previous);
            }
            else
            {
                CurrentSettings = new UserSettingsViewModel();
            }
            
            await dialogManager!.ShowDialogAsync(CurrentSettings, new UserSettingsDialog(), false, false);
        }

        #endregion
    }
}
