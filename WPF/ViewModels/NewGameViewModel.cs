using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
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
                    UpdateTable();
            }
        }

        public ICommand MoveToGameCommand { get; }

        public NewGameViewModel(INavigationService navigationService, QuestionsTableViewModel questionsTableViewModel)
        {
            this.questionsTableViewModel = questionsTableViewModel;

            MoveToGameCommand = new RelayCommand(x =>
            {
                if (questionsTableViewModel.QuestionsTable is null || questionsTableViewModel.QuestionsTable.TableLines.Count == 0)
                    throw new ArgumentException();

                navigationService.NavigateTo<GameViewModel>();
            });

            Saves = new ObservableCollection<Save>(Save.GetSaves(App.SavesDataDirectory, new JsonObjectSerializer()));

            if (Saves.Count == 0)
            {
                MessageBox.Show("Go to editor!)");
            }
            else
            {
                SelectedSave = Saves.First();
            }
        }

        public async void UpdateTable()
        {
            questionsTableViewModel.QuestionsTable = await selectedSave.GetQuestionsTableAsync(new JsonObjectSerializer());
        }
    }
}
