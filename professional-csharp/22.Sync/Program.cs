using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace SynchronizatonSamples
{
    class Program
    {
        public static void Main()
        {
            int numTasks = 20;
            var state = new SharedState();
            var tasks = new Task[numTasks];

            for (int i = 0; i < numTasks; i++)
            {
                int j = i;
                tasks[i] = Task.Run(() => new Job(state).DoTheJob(j));
            }

            Task.WaitAll(tasks);

            WriteLine($"summarized {state.State}");

        }
    }
}
