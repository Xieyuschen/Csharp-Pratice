using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Advance_Csharp
{
    class Deadlock
    {
        public class StateObject
        {
            //记得要new一下。
            object s_lock = new object();
            private int _state = 5;
            public void ChangeState(int loop)
            {
                lock (s_lock)
                {
                    if (_state == 5)
                    {
                        ++_state;
                        if (_state == 7)
                        {
                            Console.WriteLine("helloworld");
                        }
                        //Trace.Assert(_state == 6, $"Race condition occurred after {loop} loops");
                    }
                    _state = 5;

                }
            }
        }
        public class SampleTask
        {
            private StateObject _s1;
            private StateObject _s2;
            public SampleTask(StateObject s1, StateObject s2)
            {
                _s1 = s1;
                _s2 = s2;
            }
            public void DeadLock1()
            {
                int i = 0;
                while (true)
                {
                    lock (_s1)
                    {
                        lock (_s2)
                        {
                            _s1.ChangeState(i);
                            _s2.ChangeState(i++);
                            Console.WriteLine($"Still running {i}");
                        }
                    }
                }

            }
            public void DeadLock2()
            {
                int i = 0;
                while (true)
                {
                    lock (_s2)
                    {
                        lock (_s1)
                        {
                            _s2.ChangeState(i);
                            _s1.ChangeState(i++);
                            Console.WriteLine($"Still running {i}");
                        }

                    }
                }

            }
        }
        public static void Main()
        {
            var st1 = new StateObject();
            var st2 = new StateObject();
            new Task(new SampleTask(st1, st2).DeadLock1).Start();
            new Task(new SampleTask(st1, st2).DeadLock2).Start();
            Console.WriteLine("Helloworld");
        }
    }
}
