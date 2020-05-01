using System;
using System.Threading.Tasks;

namespace data_parallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 10, new ParallelOptions{ MaxDegreeOfParallelism=3 }, (i, state) => {
                Task.Delay(1000).GetAwaiter().GetResult();

                if(state.ShouldExitCurrentIteration)
                {
                    Console.WriteLine($"Current:{i}, LowestBreakIteration:{state.LowestBreakIteration}, IsExceptional:{state.IsExceptional}");
                    return;
                }

                Console.WriteLine(i);

                if(i == 3)
                {
                    //state.Stop();
                    throw new Exception("new exception");
                }
            });
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
