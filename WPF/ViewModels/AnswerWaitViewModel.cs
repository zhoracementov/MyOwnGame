using Microsoft.Extensions.Options;
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
        private const string placeholderPicture = "/Styles/placeholder.png";

        private readonly IOptions<GameSettings> gameOptions;
        private readonly PlayersViewModel playersViewModel;

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

        private string animationDataTrigger; // "Start", "Stop"
        public string AnimationDataTrigger
        {
            get => animationDataTrigger;
            set => Set(ref animationDataTrigger, value);
        }

        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get => currentPlayer;
            set => Set(ref currentPlayer, value);
        }

        public ICommand AnswerGivenCommand { get; }
        public AsyncTimer Timer { get; set; }

        public AnswerWaitViewModel(IOptions<GameSettings> gameOptions, PlayersViewModel playersViewModel)
        {
            //todo: make something with it
            CurrentPicturePath = placeholderPicture;
            AnimationDataTrigger = "Stop";

            var delayTime = AsyncTimer.DefaultDelay;
            var waitTime = gameOptions.Value.AnswerWaitingTimeSpan;

            Timer = new AsyncTimer(() =>
            {
                TimeBefore -= delayTime;
            });

            AnswerGivenCommand = new RelayCommand(x =>
            {
                Timer.Cancel(bool.TryParse((string)x, out var res) && res);
                AnimationDataTrigger = "Stop";
            });

            this.gameOptions = gameOptions;
            this.playersViewModel = playersViewModel;
        }

        public async Task<bool> WaitAnswerAsync(QuestionItem questionItem)
        {
            Timer = new AsyncTimer(() => TimeBefore -= AsyncTimer.DefaultDelay);

            CurrentPlayer = playersViewModel.CurrentPlayer;
            TimeBefore = gameOptions.Value.AnswerWaitingTimeSpan;
            Cost = questionItem.Cost;

            IsPictureInQuestion = false;
            CurrentPicturePath = placeholderPicture;

            var wait = TimeSpan.FromSeconds(2);

            CurrentQuestionText = questionItem.Row.RowTitle;
            await Task.Delay(wait);
            CurrentQuestionText = questionItem.Cost.ToString();
            await Task.Delay(wait);

            AnimationDataTrigger = "Start";

            if (string.IsNullOrWhiteSpace(questionItem.PicturePath))
            {
                IsPictureInQuestion = false;
                CurrentQuestionText = questionItem.Description;
            }
            else
            {
                var picPath = Path.Combine(App.SavesDataDirectory, questionItem.PicturePath);
                IsPictureInQuestion = !string.IsNullOrEmpty(questionItem.PicturePath) && File.Exists(picPath);

                if (IsPictureInQuestion)
                {
                    CurrentPicturePath = picPath;
                    CurrentQuestionText = string.Empty;
                }
                else
                {
                    CurrentQuestionText = questionItem.Description;
                }
            }

            var result = await Timer.Start();

            AnimationDataTrigger = "Stop";

            return result;
        }
    }
}
