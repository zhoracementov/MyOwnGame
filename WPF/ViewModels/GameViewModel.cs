using System;
using System.Threading.Tasks;

namespace WPF.ViewModels
{
    public class GameViewModel : ViewModel
    {
        public static readonly TimeSpan DefaultTiming = new TimeSpan(0, 0, 60);
        public static readonly TimeSpan DefaultStep = TimeSpan.FromSeconds(1);

        private bool isGetAnswerWindowAvailable;
        public bool IsGetAnswerWindowAvailable
        {
            get => isGetAnswerWindowAvailable;
            set => Set(ref isGetAnswerWindowAvailable, value);
        }

        private TimeSpan timeBefore;
        public TimeSpan TimeBefore
        {
            get => timeBefore;
            set => Set(ref timeBefore, value);
        }

        public GameViewModel()
        {

        }

        public async Task OpenTiming(TimeSpan timer)
        {
            IsGetAnswerWindowAvailable = true;
            TimeBefore = timer;

            while (TimeBefore >= DefaultStep)
            {
                await Task.Delay(DefaultStep);
                TimeBefore -= DefaultStep;
            }
        }
    }
}
