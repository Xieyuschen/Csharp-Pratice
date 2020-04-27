using static System.Console;
namespace SynchronizatonSamples
{
    public class Job
    {
        private SharedState _sharedState;

        public Job(SharedState sharedState)
        {
            _sharedState = sharedState;
        }

        public void DoTheJob(int j)
        {
            for (int i = 0; i < 50000; i++)
            {
                lock (_sharedState)
                {
                    _sharedState.State += 1;
                }
            }
            WriteLine(_sharedState.State);

            WriteLine(j);
        }
    }

}
