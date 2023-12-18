﻿using System;
using System.Linq;
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

                var skip = await mainWindowViewModel.OpenWaitAnswerWindow(QuestionItem);

                if (await mainWindowViewModel.OpenMessageChooseWindow(questionItem.Answer))
                    playersViewModel.SuccessfullyAnswered(QuestionItem);

                if (questionsTableViewModel.QuestionsTable.IsCompleted())
                {
                    var message = string.Format("Game Over!\n\r{0}",
                        string.Join(Environment.NewLine, playersViewModel.Players
                        .OrderByDescending(x => x.Score)
                        .Select(x => string.Format("{0}\t: {1, 4}", x.Name, x.Score))));

                    await mainWindowViewModel.OpenCancelWaitWindow(message);

                    newGameViewModel.UpdateTable();
                    playersViewModel.ResetPlayers();
                    navigationService.NavigateTo<MainMenuViewModel>();
                }
                else
                {
                    //playerRouletteService.Move();
                    //questionsTableViewModel.Update();
                }

                mainWindowViewModel.CloseMessageWindow();
            });
        }
    }
}
