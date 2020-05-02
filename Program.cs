using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace data_parallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();

            try
            {
                Parallel.For(0, 10, (i, state) => {
                    try
                    {
                        if (i == 2 || i==7)
                        {
                            throw new Exception($"exception in index {i}.");
                        }
                    }
                    catch(Exception ex)
                    {
                        exceptions.Enqueue(ex);
                    }

                    Console.WriteLine(i);
                });

                if (exceptions.Count>0)
                {
                    throw new AggregateException(exceptions);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex is AggregateException);
                
                if (ex is AggregateException)
                {
                    foreach (var item in (ex as AggregateException).InnerExceptions)
                    {
                        Console.WriteLine(item.Message);
                    }
                }
            }
            // Parallel.For(0, 10, new ParallelOptions{ MaxDegreeOfParallelism=3 }, (i, state) => {
            //     Task.Delay(1000).GetAwaiter().GetResult();

            //     if(state.ShouldExitCurrentIteration)
            //     {
            //         Console.WriteLine($"Current:{i}, LowestBreakIteration:{state.LowestBreakIteration}, IsExceptional:{state.IsExceptional}");
            //         return;
            //     }

            //     Console.WriteLine(i);

            //     if(i == 3)
            //     {
            //         //state.Break();
            //         //state.Stop();
            //         throw new Exception("new exception");
            //     }
            // });
            //Parallel.For(0,2,Method1);
            // Parallel.For(0, 3, delegate(int i){
            //     Console.WriteLine($"delegate:{i}");
            // });
            // Parallel.For(0,4, i => {
            //     Console.WriteLine($"Lambda: {i}");
            // });
            Console.WriteLine("Finished");
        }

        static void Method1(int i)
        {
            Console.WriteLine($"{i}");
        }
    }
}
