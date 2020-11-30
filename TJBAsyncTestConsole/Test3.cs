using System;
using System.Threading;
using System.Threading.Tasks;

namespace TJBAsyncTestConsole
{
    public class Test3
    {
        /*
         * This test shows what happens if there are two awaits in MethodB
         * Remember: once a wait (or await) is reached in MethodA, all processing must complete in MethodB
         * before any further processing is done in MethodA
         * So the second await in MethodB below doesn't enable MethodA to do any more processing because
         * MethodA has already reached a wait statement which means code is now running synchronously
         * 
         * If the 5000 in MethodA before the wait is changed to 20000, parallel processing continues for longer
         * but the effect is the same - once the wait is reached, all code in MethodB must complete before
         * MethodA continues
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
            Console.WriteLine($"{counter}. Method B ({elapsed.Seconds} seconds) await Task.Delay(10000);");

            await Task.Delay(10000);

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method B ({elapsed.Seconds} seconds) Finished MethodB");
        }
    }
}
