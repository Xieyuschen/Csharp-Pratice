using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using System.Threading;
using System.Diagnostics;

namespace Advance_Csharp
{

    public class Net
    {
        public class StateObject
        {
            object s_lock=new object();
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
            public void RaceCondition(object o)
            {
                Trace.Assert(o is StateObject, "o must be a StateObkect");
                StateObject stateObject = o as StateObject;
                int i = 0;
                while (true)
                {
                    stateObject.ChangeState(i++);
                }
            }
            public void RaceConditions()
            {
                var state = new StateObject();
                for (int i = 0; i < 100; i++)
                {
                    Task.Run(() => new SampleTask().RaceCondition(state));
                }
            }
        }
        public static void Main()
        {
            var s = new SampleTask();
            s.RaceConditions();
            Console.ReadLine();
        }
    }
}
