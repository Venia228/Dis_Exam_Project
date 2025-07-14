using ExamApi;
using System.Net.Http.Headers;
using System.Windows.Threading;

namespace DIS_Exams
{
    class ExamTimer
    {
        public Action<string> OnTimerUpdate;

        private int secondsLeft = 0;
        private int wastedSeconds = 0;
        private DispatcherTimer timer;
        private Exam exam;

        public ExamTimer(Exam exam)
        {
            this.exam = exam;

            secondsLeft = exam.timeToSolve;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Elapsed;
        }

        public void Start() => timer.Start();
        public void Stop() => timer.Stop();
        public void Reset()
        {
            secondsLeft = exam.timeToSolve;
            wastedSeconds = 0;

            OnTimerUpdate.Invoke($"Осталось: 00:00:00");
        }
        public string GetWastedTime()
        {
            return $"{TimeSpan.FromSeconds(wastedSeconds):hh\\:mm\\:ss}";
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            wastedSeconds++;
            secondsLeft--;

            TimeSpan timeLeft = TimeSpan.FromSeconds(secondsLeft);

            OnTimerUpdate.Invoke($"Осталось: {timeLeft:hh\\:mm\\:ss}");
        }
    }
}
