using System;
using System.Threading;
using System.Threading.Tasks;

namespace TJBAsyncTestConsole
{
    public class Test2
    {
        /*
         * This test shows how control moves between two methods, A and B, when
         * a Task in MethodB is waited on in MethodA
         * Remember: Wait (or Await if passing control to a higher level) signals the end of asynchronous processing
         * Processing won't continue after methodBTask.Wait() below until all of MethodB has finished - not just the
         * task portion of MethodB
         */

        int counter;
        readonly DateTime start = DateTime.Now;
        TimeSpan elapsed;

        public void MethodA()
        {
            counter = 0;

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) Thread.Sleep(5000);");

            Thread.Sleep(5000);

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) Task methodBTask = MethodB();");

            Task methodBTask = MethodB();

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) Thread.Sleep(5000);");

            Thread.Sleep(5000);

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) methodBTask.Wait();");

            methodBTask.Wait();

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) Thread.Sleep(10000);");

            Thread.Sleep(10000);

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) Finished MethodA");
        }

        public async Task MethodB()
        {
            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method B ({elapsed.Seconds} seconds) Thread.Sleep(10000);");

            Thread.Sleep(10000);

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method B ({elapsed.Seconds} seconds) await Task.Delay(10000);");

            await Task.Delay(10000);

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method B ({elapsed.Seconds} seconds) Thread.Sleep(5000);");

            Thread.Sleep(5000);

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method B ({elapsed.Seconds} seconds) Finished MethodB");
        }
    }
}
