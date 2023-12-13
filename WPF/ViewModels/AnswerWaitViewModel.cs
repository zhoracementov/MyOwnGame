﻿using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.Services;

namespace WPF.ViewModels
{
    public class AnswerWaitViewModel : ViewModel
    {
        private AsyncTimer timer;
        private readonly IOptions<GameSettings> gameOptions;

        private TimeSpan timeBefore;
        public TimeSpan TimeBefore
        {
            get => timeBefore;
            set => Set(ref timeBefore, value);
        }

        private int cost;
        public int Cost
        {
            get => cost;
            set => Set(ref cost, value);
        }

        private string currentQuestionText;
        public string CurrentQuestionText
        {
            get => currentQuestionText;
            set => Set(ref currentQuestionText, value);
        }

        private bool isPictureInQuestion;
        public bool IsPictureInQuestion
        {
            get => isPictureInQuestion;
            set => Set(ref isPictureInQuestion, value);
        }

        private string currentPicturePath;
        public string CurrentPicturePath
        {
            get => currentPicturePath;
            set => Set(ref currentPicturePath, value);
        }

        public ICommand AnswerGivenCommand { get; }

        public AnswerWaitViewModel(IOptions<GameSettings> gameOptions)
        {
            //todo: make something with it
            CurrentPicturePath = "/Styles/placeholder.png";

            AnswerGivenCommand = new RelayCommand(x => timer.Cancel());
            this.gameOptions = gameOptions;
        }

        public async Task<bool> WaitAnswerAsync(QuestionItem questionItem)
        {
            var delayTime = AsyncTimer.DefaultDelay;
            var waitTime = gameOptions.Value.AnswerWaitingTimeSpan;

            TimeBefore = waitTime;
            Cost = questionItem.Cost;

            if (string.IsNullOrEmpty(questionItem.PicturePath))
            {
                IsPictureInQuestion = false;
                CurrentQuestionText = questionItem.Description;
            }
            else
            {
                var picPath = Path.Combine(App.SavesDataDirectory, questionItem.PicturePath);

                IsPictureInQuestion = questionItem.PicturePath != null && File.Exists(picPath);

                if (IsPictureInQuestion)
                    CurrentPicturePath = picPath;

                CurrentQuestionText = string.Empty;
            }

            timer ??= new AsyncTimer(delayTime, waitTime, () => TimeBefore -= delayTime);
            return await timer.Start();
        }
    }
}
