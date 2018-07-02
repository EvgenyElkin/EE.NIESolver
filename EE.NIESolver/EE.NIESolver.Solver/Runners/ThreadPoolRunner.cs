using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using EE.NIESolver.MathNet;
using EE.NIESolver.Solver.Methods;

namespace EE.NIESolver.Solver.Runners
{
    public class ThreadPoolRunner : I2Runner
    {
        private readonly I2Method _method;

        public ThreadPoolRunner(I2Method method)
        {
            _method = method;
        }

        public void Run(MathNet2 net)
        {
            for (var iteration = 1; iteration <= net.Width + net.Height - 1; iteration++)
            {
                var row = iteration <= net.Width ? 1 : iteration - net.Width + 1;
                var column = iteration <= net.Width ? iteration : net.Width;

                var count = Math.Min(column, net.Height - row + 1);
                var @event = new CountdownEvent(count);
                var workers = new List<ThreadWorker>();

                while (row <= net.Height && column >= 1)
                {
                    var worker = new ThreadWorker(@event, net, _method, column, row);
                    workers.Add(worker);
                    ThreadPool.QueueUserWorkItem(worker.Calculate);
                    column--;
                    row++;
                }

                @event.Wait();
                foreach (var worker in workers)
                {
                    net.Set(worker.Column, worker.Row, worker.Result);
                }
            }
        }

        private class ThreadWorker
        {
            private readonly CountdownEvent _event;
            private readonly MathNet2 _net;
            private readonly I2Method _method;
            public readonly int Column;
            public readonly int Row;
            public double Result;

            public ThreadWorker(CountdownEvent @event, MathNet2 net, I2Method method, int column, int row)
            {
                _net = net;
                _method = method;
                _event = @event;
                Column = column;
                Row = row;
            }

            public void Calculate(object context)
            {
                var p = new MathNet2Pointer(_net);
                p.Set(Column, Row);
                Result = _method.Calculate(p);
                _event.Signal();
            }
        }

        
    }
}