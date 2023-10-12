using WPF.Services;

namespace WPF.ViewModels
{
    class MainMenuViewModel : ViewModel
    {
        public string Info => GetType().Name;
        public MainMenuViewModel(INavigationService navigationService)
        {
        }
    }
}
