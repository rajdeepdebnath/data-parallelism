using System;
using System.Threading.Tasks;

namespace data_parallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parallel.For(0,2,Method1);
            // Parallel.For(0, 3, delegate(int i){
            //     Console.WriteLine($"delegate:{i}");
            // });
            Parallel.For(0,4, i => {
                Console.WriteLine($"Lambda: {i}");
            });
            Console.WriteLine("Finished");
        }

        static void Method1(int i)
        {
            Console.WriteLine($"{i}");
        }
    }
}
