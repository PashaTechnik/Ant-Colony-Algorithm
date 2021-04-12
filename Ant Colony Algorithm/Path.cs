using System;
using System.Collections.Generic;
using System.Linq;

namespace Ant_Colony_Algorithm
{
    public class Path
    {
        
        double[,] distance;
        double[,] pheromones;

        public int[] path;
        

        public double BestDistance = int.MaxValue;

        private readonly int alpha = 3;
        private readonly int beta = 5;
        private readonly double P = 0.1;
        private readonly double Q = 100;


        public Path(Cities map)
        {

            Random rnd = new Random();
            distance = new double[map.Coordinate.Length, map.Coordinate.Length];
            pheromones = new double[map.Coordinate.Length, map.Coordinate.Length];

            for (int j = 0; j < map.Coordinate.Length; j++)
            {
                distance[j, j] = 0;

                for (int i = 0; i < map.Coordinate.Length; i++)
                {
                    double value = Math.Sqrt(Math.Pow(map.Coordinate[i].X - map.Coordinate[j].X, 2) +
                                             Math.Pow(map.Coordinate[i].Y - map.Coordinate[j].Y, 2));
                    distance[i, j] = distance[j, i] = value;
                    
                    //pheromones[i, j] = pheromones[j, i] = rnd.Next(1,10);
                    pheromones[i, j] = pheromones[j, i] = 5;
                    pheromones[i, i] = 0;
                }
            }

            path = new int[map.Coordinate.Length];
            for (int i = 0; i < map.Coordinate.Length; i++)
            {
                path[i] = i;
            }


        }

        public void CreateAntGeneration(int numberOfGeneration)
        {
            for (int i = 0; i < numberOfGeneration; i++)
            {
                Console.WriteLine($"Number of generation:{i + 1}");
                FindPath();
            }
        }

        public void FindPath()
        {
            Random rnd = new Random();
            var mainPath = new List<int>(path);
            var tempPath = new List<int>();
            var numberOfAnts = mainPath.Count;
            for (int z = 0; z < numberOfAnts; z++)
            {
                //mainPath = new List<int>(path);
                Dictionary<int, double> probabilities = new Dictionary<int, double>();
                int start = 0;
                while (mainPath.Count != 0)
                {
                    ChooseCity(probabilities, mainPath, ref start, tempPath);
                }
            
                UpdatePheromones(PathLength(tempPath.ToArray()),tempPath);
                Console.WriteLine("===================");
                DisplayPheromones();
                Console.WriteLine("===================");

                Console.WriteLine($"Length: {PathLength(tempPath.ToArray())}");
                if (PathLength(tempPath.ToArray()) < BestDistance)
                {
                    BestDistance = PathLength(tempPath.ToArray());
                }
                Console.WriteLine($"Number of ant:{z + 1}");
            }
            
        }

        public void ChooseCity(Dictionary<int, double> probabilities, List<int> mainPath, ref int start, List<int> tempPath)
        {
            probabilities.Clear();
            mainPath.Remove(start);
            foreach (var i in mainPath)
            {
                double sum = 0;

                foreach (var j in mainPath)
                {
                    var t = Math.Pow((1 / distance[start, j]), beta);
                    var d = Math.Pow(pheromones[start, j], alpha);
                    sum += t * d;
                }

                double Probability =
                    (Math.Pow((1 / distance[start, i]), beta) * Math.Pow(pheromones[start, i], alpha)) / sum;
                Probability = 100 * Probability;
                probabilities.Add(i, Probability);
            }

            probabilities = probabilities.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                
            start = getNextCity(probabilities);
            tempPath.Add(start);
        }

        public void DisplayPheromones()
        {
            for (int i = 0; i < path.Length; i++)
            {
                for (int j = 0; j < path.Length; j++)
                {
                    Console.Write(Math.Round(pheromones[i,j], 3) + " ");
                }
                Console.WriteLine();
            }
        }

        public void UpdatePheromones(double length, List<int> tPath)
        {
            List<List<int>> pairs = new List<List<int>>();
            for (int i = 0; i < tPath.Count - 1; i++)
            {
                List<int> temp = new List<int>(){ tPath[i], tPath[i + 1] };
                pairs.Add(temp);
            }
            
            for (int i = 0; i < path.Length; i++)
            {
                for (int j = 0; j < path.Length; j++)
                {
                    if (pairs.Contains(new List<int> {i, j}) || pairs.Contains(new List<int> {j, i}))
                    {
                        pheromones[i, j] =  (1 - P) * pheromones[i, j] + Q / length;
                    }
                    else
                    {
                        pheromones[i, j] = (1 - P) * pheromones[i, j];
                    }
                }
            }
        }

        public int getNextCity(Dictionary<int, double> dict)
        {
            Random rnd = new Random();
            var moveProb = rnd.Next(1, 100);
            
            foreach (KeyValuePair<int, double> keyValue in dict)
            {
                if (moveProb < keyValue.Value)
                {
                    return keyValue.Key;
                }
            }
            return 0;
        }



        public double PathLength(int[] Path)
        {
            double pathSum = 0;
            for (int i = 0; i < Path.Length - 1; i++)
            {
                pathSum += distance[Path[i], Path[i + 1]];
            }
            return pathSum;
        }

        public double CalcPathLength(int[] path)
        {
            

            var k = path.ToList();
            k.Insert(0,0);
            k.Add(0);
            path = k.ToArray();
            
            double pathSum = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                pathSum += distance[path[i], path[i + 1]];
            }
            return pathSum;
        }
        
    }
}
