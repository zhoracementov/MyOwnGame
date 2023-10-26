using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Navigation.Services;
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
                    questionsTableViewModel.QuestionsTable = QuestionsTable.LoadFromFile(value.FilePath, new JsonObjectSerializer());
            }
        }
        public ICommand MoveToGameCommand { get; }

        public NewGameViewModel(INavigationService navigationService, QuestionsTableViewModel questionsTableViewModel)
        {
            this.questionsTableViewModel = questionsTableViewModel;

            MoveToGameCommand = new RelayCommand(x =>
            {
                if (questionsTableViewModel.QuestionsTable is null)
                    throw new NullReferenceException();

                navigationService.NavigateTo<GameViewModel>();
            });

            Saves = new ObservableCollection<Save>(GetSaves(new JsonObjectSerializer()));
            SelectedSave = Saves.First();
        }

        private IEnumerable<Save> GetSaves(IObjectSerializer objectSerializer)
        {
            return Directory
                .GetFiles(App.SavesDataDirectory, $"*{objectSerializer.FileFormat}", SearchOption.TopDirectoryOnly)
                .Select(file => new Save { FilePath = file });
        }

        private void DeleteSeletedSave(object parameter)
        {
            if (parameter is null)
                return;

            var selected = (Save)parameter;

            File.Delete(selected.FilePath);

            if (Saves.Count != 0 && !Saves.Remove(selected))
                throw new InvalidOperationException();

            if (Saves.Count == 0)
            {
                //ReloadFillword(Size);
            }
            else
            {
                //SelectedSave = SavedFillwords.First();
            }
        }
    }
}
