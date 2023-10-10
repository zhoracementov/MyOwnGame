using Microsoft.Extensions.DependencyInjection;

namespace WPF.ViewModels
{
    internal class ViewModelsLocator
    {
        public MainWindowViewModel MainWindowViewModel => GetViewModel<MainWindowViewModel>();

        private TViewModel GetViewModel<TViewModel>() where TViewModel : ViewModel
        {
            return App.Host.Services.GetRequiredService<TViewModel>();
        }
    }
}