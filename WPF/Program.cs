using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WPF.Navigation.Services;
using WPF.Services;
using WPF.ViewModels;

namespace WPF
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] agrs) => Host
            .CreateDefaultBuilder(agrs)
            .UseContentRoot(App.UserDataDirectory)
            .ConfigureAppConfiguration(ConfigureAppConfiguration)
            .ConfigureServices(ConfigurateServices);

        public static void ConfigureAppConfiguration(HostBuilderContext host, IConfigurationBuilder cfg) => cfg
            .SetBasePath(App.UserDataDirectory);
        //.AddJsonFile(App.SettingsFileName, optional: false, reloadOnChange: true);

        public static void ConfigurateServices(HostBuilderContext host, IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<MainMenuViewModel>()
            .AddSingleton<NewGameViewModel>()
            .AddSingleton<GameEditorViewModel>()
            .AddSingleton<GameViewModel>()
            .AddSingleton<QuestionsTableViewModel>()
            .AddSingleton<AnswerWindowViewModel>()
            .AddTransient<QuestionItemViewModel>()
            .AddSingleton<BrushesRouletteService>()
            .AddSingleton<PlayerRouletteService>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<Func<Type, ViewModel>>(sp => type => (ViewModel)sp.GetRequiredService(type));
        //.ConfigureWritable<GameSettings>(host.Configuration.GetSection(nameof(GameSettings)), App.SettingsFileName);
    }
}
