﻿using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode { get; set; } = true;

        private static IHost host;
        public static IHost Host => host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override async void OnStartup(StartupEventArgs e)
        {            
            IsDesignMode = false;
            CreateSubDirectories();

            base.OnStartup(e);

            await Host.StartAsync().ConfigureAwait(false);
        }

        protected void CreateSubDirectories()
        {
            var dirToCreate = new string[]
            {
                UserDataDirectory,
                SavesDataDirectory,
            };

            foreach (var path in dirToCreate)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
        }

        public static string UserDataDirectory =>
                Path.Combine(CurrentDirectory, ConfigurationManager.AppSettings["userdata"]);

        public static string SavesDataDirectory =>
                Path.Combine(UserDataDirectory, ConfigurationManager.AppSettings["savesdata"]);

        public static string SettingsFile =>
                Path.Combine(UserDataDirectory, ConfigurationManager.AppSettings["settings"]);

        public static string BrushesFile =>
                Path.Combine(UserDataDirectory, ConfigurationManager.AppSettings["brushes"]);

        public static string CurrentDirectory => IsDesignMode
                ? Path.GetDirectoryName(GetSourceCodePath())
                : Environment.CurrentDirectory;

        public static string GitHubLink =>
                ConfigurationManager.AppSettings["githublink"];

        public static string GetSourceCodePath([CallerFilePath] string path = null) => path;
    }
}
