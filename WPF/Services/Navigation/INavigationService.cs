using WPF.ViewModels;

namespace WPF.Services.Navigation
{
    public interface INavigationService
    {
        ViewModel CurrentViewModel { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModel;
    }
}
