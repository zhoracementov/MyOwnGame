﻿using Microsoft.Extensions.DependencyInjection;

namespace WPF.ViewModels
{
    public class ViewModelsLocator
    {
        public MainWindowViewModel MainWindowViewModel => GetViewModel<MainWindowViewModel>();
        public MainMenuViewModel MainMenuViewModel => GetViewModel<MainMenuViewModel>();
        public NewGameViewModel NewGameViewModel => GetViewModel<NewGameViewModel>();
        public GameEditorViewModel GameEditorViewModel => GetViewModel<GameEditorViewModel>();
        public GameViewModel GameViewModel => GetViewModel<GameViewModel>();
        public QuestionsTableViewModel QuestionsTableViewModel => GetViewModel<QuestionsTableViewModel>();
        public AnswerWaitViewModel AnswerWindowViewModel => GetViewModel<AnswerWaitViewModel>();
        public AnswerGivenViewModel AnswerGivenViewModel => GetViewModel<AnswerGivenViewModel>();

        private TViewModel GetViewModel<TViewModel>() where TViewModel : ViewModel
        {
            return App.Host.Services.GetRequiredService<TViewModel>();
        }
    }
}