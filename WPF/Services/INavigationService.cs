using WPF.ViewModels;

namespace WPF.Services
{
    public interface INavigationService
    {
        public ViewModel CurrentViewModel { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModel;
    }
}
