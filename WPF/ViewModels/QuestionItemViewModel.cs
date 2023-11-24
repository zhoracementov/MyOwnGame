using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Services;
using WPF.Services.Navigation;

namespace WPF.ViewModels
{
    public class QuestionItemViewModel : ViewModel
    {
        private QuestionItem questionItem;
        public QuestionItem QuestionItem
        {
            get => questionItem;
            set
            {
                if (Set(ref questionItem, value) && questionItem.IsClosed != null)
                    IsActive = questionItem.IsClosed == false;
            }
        }

        private bool isActive = true;
        public bool IsActive
        {
            get => isActive;
            set => Set(ref isActive, value);
        }

        public ICommand TapToAnswerCommand { get; }

        public QuestionItemViewModel(INavigationService navigationService, MainWindowViewModel mainWindowViewModel,
            QuestionsTableViewModel questionsTableViewModel, PlayerRouletteService playerRouletteService)
        {
            TapToAnswerCommand = new RelayCommand(async x =>
            {
                IsActive = false;
                QuestionItem.IsClosed = true;

                var skip = await mainWindowViewModel.OpenWaitAsnwerWindow(QuestionItem);

                if (await mainWindowViewModel.OpenMessageChooseWindow(questionItem.Answer))
                    playerRouletteService.AddScore(QuestionItem);

                mainWindowViewModel.CloseMessageWindow();

                if (questionsTableViewModel.QuestionsTable.IsCompleted())
                {
                    MessageBox.Show(
                        string.Format("Game Over!\n\rScore Table:\n\r{0}",
                        string.Join(Environment.NewLine, playerRouletteService
                        .GetPlayers()
                        .OrderByDescending(x => x.Score)
                        .Select(x => $"{x.Name}:\t{x.Score}"))));

                    navigationService.NavigateTo<MainMenuViewModel>();
                }
                else
                {
                    playerRouletteService.Move();
                    //questionsTableViewModel.Update();
                }
            });
        }
    }
}
