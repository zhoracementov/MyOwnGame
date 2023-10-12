using Microsoft.Extensions.DependencyInjection;

namespace WPF.ViewModels
{
    internal class ViewModelsLocator
    {
        public MainWindowViewModel MainWindowViewModel => GetViewModel<MainWindowViewModel>();
        public MainMenuViewModel MainMenuViewModel => GetViewModel<MainMenuViewModel>();
        public NewGameViewModel NewGameViewModel => GetViewModel<NewGameViewModel>();
        public GameEditorViewModel GameEditorViewModel => GetViewModel<GameEditorViewModel>();

        private TViewModel GetViewModel<TViewModel>() where TViewModel : ViewModel
        {
            return App.Host.Services.GetRequiredService<TViewModel>();
        }
    }
}