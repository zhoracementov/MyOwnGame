using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
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
                .UseContentRoot(App.DataDirectory);
        //        .ConfigureAppConfiguration(ConfigureAppConfiguration)
        //        .ConfigureServices(ConfigurateServices);

        //public static void ConfigureAppConfiguration(HostBuilderContext host, IConfigurationBuilder cfg) => cfg
        //        .SetBasePath(App.DataDirectory);
        //.AddJsonFile(App.SettingsFileName, optional: false, reloadOnChange: true);

        //public static void ConfigurateServices(HostBuilderContext host, IServiceCollection services) => services
        //        .AddSingleton<Func<Type, ViewModel>>(sp => vmt => (ViewModel)sp.GetRequiredService(vmt));
        //.ConfigureWritable<GameSettings>(host.Configuration.GetSection(nameof(GameSettings)), App.SettingsFileName);
    }
}
