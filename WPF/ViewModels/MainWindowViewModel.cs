namespace WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public string Title => App.CurrentDirectory;

        public MainWindowViewModel()
        {

        }
    }
}
