using System;
using System.IO;
using System.Linq;

namespace PuzzleSeven
{
    class Program
    {
        static void Main(string[] args)
        {
            // var input = "16,1,2,0,4,2,7,1,2,14"; 
            var input = File.ReadAllLines("input.txt").First();
            var horizontalPositions = input.Split(",").Select(int.Parse);
            int min = horizontalPositions.Min();
            int max = horizontalPositions.Max();
            int leastFuelCost = int.MaxValue;
            for(var i = min; i <= max; i++)
            {
                int fuelCost = horizontalPositions.Select(h => {
                    var numberOfSteps = Math.Abs(h - i);
                    int cost = 0;
                    for (int step = 0; step <= numberOfSteps; step++)
                    {
                        cost += step;
                    }
                    return cost;
                }).Sum();
                
                if(fuelCost < leastFuelCost)
                {
                    leastFuelCost = fuelCost;
                }
            }
            Console.WriteLine(leastFuelCost);
        }
    }
}
