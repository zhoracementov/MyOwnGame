using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
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
                if (Set(ref questionItem, value))
                {
                    IsActive = true;
                }
            }
        }

        private int? cost;
        public int? Cost
        {
            get => cost;
            set => Set(ref cost, value);
        }

        private bool isActive;
        public bool IsActive
        {
            get => isActive;
            //set => Set(ref isActive, value);
            set
            {
                if (Set(ref isActive, value))
                {
                    Cost = value ? questionItem.Cost : (int?)null;
                }
            }
        }

        public ICommand TapToAnswerCommand { get; }

        public QuestionItemViewModel(INavigationService navigationService, MainWindowViewModel mainWindowViewModel,
            QuestionsTableViewModel questionsTableViewModel, NewGameViewModel newGameViewModel, PlayersViewModel playersViewModel)
        {
            TapToAnswerCommand = new RelayCommand(async x =>
            {
                IsActive = false;
                QuestionItem.IsClosed = true;

                var skip = await mainWindowViewModel.OpenWaitAsnwerWindow(QuestionItem);

                if (await mainWindowViewModel.OpenMessageChooseWindow(questionItem.Answer))
                    playersViewModel.SuccessfullyAnswered(QuestionItem);

                    mainWindowViewModel.CloseMessageWindow();

                if (questionsTableViewModel.QuestionsTable.IsCompleted())
                {
                    //MessageBox.Show(
                    //    string.Format("Game Over!\n\rScore Table:\n\r{0}",
                    //    string.Join(Environment.NewLine, playerRouletteService
                    //    .GetPlayers()
                    //    .OrderByDescending(x => x.Score)
                    //    .Select(x => $"{x.Name}:\t{x.Score}"))));

                    newGameViewModel.UpdateTable();
                    playersViewModel.ResetPlayers();
                    navigationService.NavigateTo<MainMenuViewModel>();
                }
                else
                {
                    //playerRouletteService.Move();
                    //questionsTableViewModel.Update();
                }
            });
        }
    }
}
