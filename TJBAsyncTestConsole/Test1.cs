using System;
using System.Threading;
using System.Threading.Tasks;

namespace TJBAsyncTestConsole
{
    public class Test1
    {
        /*
         * This test shows how control moves between two methods, A and B, when there are
         * no await or wait statements in MethodA
         * Remember: it is the await in MethodB that passes control back to MethodA
         * As soon as the await in MethodB is reached, MethodA's processing resumes in parallel with MethodB's
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
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) MethodB();");

            MethodB();

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) Thread.Sleep(20000);");

            Thread.Sleep(20000);

            counter++;
            elapsed = DateTime.Now - start;
            Console.WriteLine($"{counter}. Method A ({elapsed.Seconds} seconds) Finished MethodA");
        }

        public async void MethodB()
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
