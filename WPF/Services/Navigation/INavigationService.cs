using WPF.ViewModels;

namespace WPF.Navigation.Services
{
    public interface INavigationService
    {
        ViewModel CurrentViewModel { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModel;
    }
}
