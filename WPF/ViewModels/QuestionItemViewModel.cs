using System.Windows;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Navigation.Services;

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

        public QuestionItemViewModel(INavigationService navigationService, GameViewModel gameViewModel,
            AnswerWindowViewModel answerWindowViewModel, QuestionsTableViewModel questionsTableViewModel)
        {
            TapToAnswerCommand = new RelayCommand(async x =>
            {
                if (navigationService.CurrentViewModel == gameViewModel)
                {
                    IsActive = false;
                    QuestionItem.IsClosed = true;

                    var answer = await answerWindowViewModel.OpenTiming(QuestionItem);

                    if (QuestionItem.CheckAnswer(answer))
                    {
                        gameViewModel.CostSum += QuestionItem.Cost;
                    }

                    gameViewModel.LastAnswer = answer;

                    if (questionsTableViewModel.QuestionsTable.IsCompleted())
                    {
                        MessageBox.Show("Game Over!");
                        navigationService.NavigateTo<MainMenuViewModel>();
                    }
                    else
                    {
                        //questionsTableViewModel.Update();
                    }
                }
            });
        }
    }
}
