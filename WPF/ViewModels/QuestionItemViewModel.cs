using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Services.Navigation;
using WPF.Services;

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

        public QuestionItemViewModel(INavigationService navigationService,
            GameViewModel gameViewModel, AnswerWaitViewModel answerWindowViewModel, AnswerGivenViewModel answerGivenViewModel,
            QuestionsTableViewModel questionsTableViewModel, PlayerRouletteService playerRouletteService)
        {
            TapToAnswerCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel != gameViewModel)
                    return;

                IsActive = false;
                QuestionItem.IsClosed = true;

                //gameViewModel.AnswerViewModel = answerWindowViewModel;
                await answerWindowViewModel.WaitAnswerAsync(QuestionItem);

                //gameViewModel.AnswerViewModel = answerGivenViewModel;
                if (await answerGivenViewModel.GetResult(questionItem))
                {
                    playerRouletteService.AddScore(QuestionItem.Cost);
                }

                gameViewModel.AnswerViewModel = null;

                if (questionsTableViewModel.QuestionsTable.IsCompleted())
                {
                    MessageBox.Show(
                        string.Format("Game Over!\n\rScore Table:\n\r{0}",
                        string.Join(Environment.NewLine, playerRouletteService.GetPlayersSortedByScore().Select(x => $"{x.Name}:\t{x.Score}"))));

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
