using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public NewGameViewModel(INavigationService navigationService, BrushesRouletteService brushesRouletteService,
            MainWindowViewModel mainWindowViewModel, QuestionsTableViewModel questionsTableViewModel, PlayersViewModel playersViewModel)
        {
            this.questionsTableViewModel = questionsTableViewModel;

            MoveToGameCommand = new RelayCommand(x =>
            {
                if (questionsTableViewModel.QuestionsTable is null)
                    throw new ArgumentException();

                if (questionsTableViewModel.QuestionsTable.TableLines.Count == 0)
                    throw new ArgumentException();

                if (SelectedSave is null)
                    return;

                if (playersViewModel.Players.Count < 2)
                    return;

                playersViewModel.GameStarts();

                navigationService.NavigateTo<GameViewModel>();
            });

            AddPlayerCommand = new RelayCommand(async x =>
            {
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

            Saves = new ObservableCollection<Save>(Save.GetSaves(App.SavesDataDirectory, new JsonObjectSerializer()));
            SelectedSave = Saves.FirstOrDefault();
        }

        public async void ResetTable()
        {
            questionsTableViewModel.QuestionsTable = await selectedSave.GetQuestionsTableAsync(new JsonObjectSerializer());
        }
    }
}
