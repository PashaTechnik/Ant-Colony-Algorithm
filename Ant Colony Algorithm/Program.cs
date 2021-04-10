using System;
using System.Diagnostics;
using System.IO;

namespace Ant_Colony_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            
            SolveSalesmanProblem("berlin52", 52);
            
            
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
        static void SolveSalesmanProblem(string Path, int N)
        {
            Cities Berlin = new Cities(N, $"/Users/admin/Desktop/Ant Colony Algorithm/{Path}.txt");
            
            Path path = new Path(Berlin);
            path.CreateAntGeneration(500);

            Console.WriteLine($"Best: {path.BestDistance}");

            // Console.WriteLine();
            // foreach (var city in path.path)
            // {
            //     Console.Write(city);
            //     Console.Write("->");
            // }

            //Console.WriteLine(path.CurrentDistance);
            
            
        }
    }
}