using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WPF.Models;
using WPF.Services;
using WPF.Services.Navigation;
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
            .SetBasePath(App.UserDataDirectory)
            .AddJsonFile(App.SettingsFile, optional: true, reloadOnChange: true);

        public static void ConfigurateServices(HostBuilderContext host, IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<MainMenuViewModel>()
            .AddSingleton<NewGameViewModel>()
            .AddSingleton<GameEditorViewModel>()
            .AddSingleton<GameViewModel>()
            .AddSingleton<QuestionsTableViewModel>()
            .AddSingleton<AnswerWaitViewModel>()
            .AddSingleton<MessageChooseViewModel>()
            .AddTransient<QuestionItemViewModel>()
            .AddSingleton<BrushesRouletteService>()
            .AddSingleton<PlayersViewModel>()
            .AddSingleton<CancelWaitViewModel>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<Func<Type, ViewModel>>(sp => type => (ViewModel)sp.GetRequiredService(type))
            .Configure<GameSettings>(host.Configuration)
            .Configure<GameSettings>(opt =>
            {
                if (opt.AnswerWaitingTimeSpan <= TimeSpan.Zero)
                {
                    opt.AnswerWaitingTimeSpan = TimeSpan.FromMinutes(1);
                }
            });
    }
}
