using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyStarter.Data
{
    class ProgramExecutor
    {
        private readonly Queue<IProgram> Programs = new Queue<IProgram>();

        public ProgramExecutor()
        {
        }

        public void Execute(IProgram prog, int delay)
        {
            Execute(new List<IProgram>() { prog }, delay);
        }

        public void Execute(IEnumerable<IProgram> progs, int delay)
        {
            Programs.Clear();
            progs.ForEach(p => Programs.Enqueue(p));

            new Task(__Execute, delay).Start();
        }

        private void __Execute(object state)
        {
            var resetEvent = new ManualResetEvent(false);
            if ((int)state >= 0) resetEvent.WaitOne((int)state);

            lock (Programs)
            {
                while (Programs.Any())
                {
                    var p = Programs.Dequeue();
                    try
                    {
                        if (p.Exist && !resetEvent.WaitOne(1000)) 
                        {
                            Process.Start(p.Path, p.Arguments);
                            resetEvent.Reset();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
