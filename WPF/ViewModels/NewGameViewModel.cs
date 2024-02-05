using Microsoft.Extensions.Options;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Services;
using WPF.Services.Navigation;
using WPF.Services.Serialization;

namespace WPF.ViewModels
{
    public class NewGameViewModel : ViewModel
    {
        private readonly QuestionsTableViewModel questionsTableViewModel;

        private ObservableCollection<Save> saves;
        public ObservableCollection<Save> Saves
        {
            get => saves;
            set => Set(ref saves, value);
        }

        private Save selectedSave;
        public Save SelectedSave
        {
            get => selectedSave;
            set
            {
                if (value != null && Set(ref selectedSave, value))
                    ResetTable();
            }
        }

        public ICommand AddPlayerCommand { get; }
        public ICommand ResetPlayersCommand { get; }
        public ICommand MoveToGameCommand { get; }
        public ICommand CreateEmptyGameCommand { get; }
        public ICommand UpdateSaveDataCommand { get; }
        public ICommand OpenSaveDataFolderCommand { get; }

        public NewGameViewModel(INavigationService navigationService, BrushesRouletteService brushesRouletteService,
            MainWindowViewModel mainWindowViewModel, QuestionsTableViewModel questionsTableViewModel,
            PlayersViewModel playersViewModel, IOptions<GameSettings> options)
        {
            this.questionsTableViewModel = questionsTableViewModel;

            var minPlayersCount = 2;

            MoveToGameCommand = new RelayCommand(async x =>
            {
                if (questionsTableViewModel.QuestionsTable is null)
                    throw new ArgumentException();

                if (questionsTableViewModel.QuestionsTable.TableRows.Count == 0)
                    throw new ArgumentException();

                if (SelectedSave is null)
                    return;

                if (playersViewModel.Players.Count < minPlayersCount)
                {
                    await mainWindowViewModel.OpenCancelWaitWindow($"Players count should be more than {minPlayersCount}!");
                    mainWindowViewModel.CloseMessageWindow();
                    return;
                }

                playersViewModel.GameStarts();

                navigationService.NavigateTo<GameViewModel>();
            });

            AddPlayerCommand = new RelayCommand(async x =>
            {
                if (playersViewModel.Players.Count >= options.Value.MaxPlayersCount)
                {
                    await mainWindowViewModel.OpenCancelWaitWindow("Players Count Limit!");
                    mainWindowViewModel.CloseMessageWindow();
                    return;
                }

                var playerName = await mainWindowViewModel.OpenAddPlayerWindow();
                mainWindowViewModel.CloseMessageWindow();

                if (!string.IsNullOrWhiteSpace(playerName))
                {
                    var playerBrush = brushesRouletteService.Next;
                    var player = new Player(playerName, playerBrush);

                    playersViewModel.Players.Add(player);
                }
            });

            ResetPlayersCommand = new RelayCommand(x =>
            {
                playersViewModel.Players.Clear();
                playersViewModel.CurrentPlayer = null;
            });

            UpdateSaveDataCommand = new RelayCommand(x =>
            {
                UpdateSaves();
            });

            OpenSaveDataFolderCommand = new RelayCommand(x =>
            {
                Process.Start("explorer.exe", App.SavesDataDirectory);
            });

            var baseRowCount = 5;
            var baseRowLength = 6;

            CreateEmptyGameCommand = new RelayCommand(async x =>
            {
                var json = new JsonObjectSerializer();

                var name = $"Empty instance {DateTime.Now:HH.mm.ss dd.mm.yyyy-FFFFFFF}{json.FileFormat}";
                var instance = QuestionsTable.CreateEmpty(name, baseRowCount, baseRowLength);
                var path = Path.Combine(App.SavesDataDirectory, name);

                using var watcher = new FileSystemWatcher();
                watcher.Path = App.SavesDataDirectory;
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.LastAccess;
                watcher.Changed += (s, e) => UpdateEditingSaves();
                watcher.Created += (s, e) => UpdateEditingSaves();
                watcher.EnableRaisingEvents = true;

                await instance.SaveAsync(path, json);

                using var p = new Process { StartInfo = new ProcessStartInfo(path) { UseShellExecute = true } };
                p.Start();
            });

            UpdateSaves();
        }

        public void UpdateEditingSaves()
        {
            Saves = new ObservableCollection<Save>(Save.GetSaves(App.SavesDataDirectory, new JsonObjectSerializer()));
            ResetTable();
        }

        public void UpdateSaves()
        {
            Saves = new ObservableCollection<Save>(Save.GetSaves(App.SavesDataDirectory, new JsonObjectSerializer()));
            SelectedSave = Saves.FirstOrDefault();
        }

        public async void ResetTable()
        {
            questionsTableViewModel.QuestionsTable = await selectedSave.GetQuestionsTableAsync(new JsonObjectSerializer());
        }
    }
}
