using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PuzzleSix
{
    class Program
    {
        static void Main(string[] args)
        {
            var fishAges = File.ReadAllLines("input.txt").First().Split(",").Select(int.Parse);
            
            Dictionary<int, long> numberOfFishPerGeneration = new Dictionary<int, long>()
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0},
                { 3, 0},
                { 4, 0},
                { 5, 0},
                { 6, 0},
                { 7, 0},
                { 8, 0},
            };
            
            foreach (var fish in fishAges)
            {
                numberOfFishPerGeneration[fish]++;
            }
            int daysToGenerate = 256;

            for (int days = 0; days < daysToGenerate; days++)
            {
                var creatingNewFish = numberOfFishPerGeneration[0];
                var newDay0 = numberOfFishPerGeneration[1];
                var newDay1 = numberOfFishPerGeneration[2];
                var newDay2 = numberOfFishPerGeneration[3];
                var newDay3 = numberOfFishPerGeneration[4];
                var newDay4 = numberOfFishPerGeneration[5];
                var newDay5 = numberOfFishPerGeneration[6];
                var newDay6 = numberOfFishPerGeneration[7];
                var newDay7 = numberOfFishPerGeneration[8];
                
                numberOfFishPerGeneration[0] = newDay0;
                numberOfFishPerGeneration[1] = newDay1;
                numberOfFishPerGeneration[2] = newDay2;
                numberOfFishPerGeneration[3] = newDay3;
                numberOfFishPerGeneration[4] = newDay4;
                numberOfFishPerGeneration[5] = newDay5;
                numberOfFishPerGeneration[6] = newDay6 + creatingNewFish;
                numberOfFishPerGeneration[7] = newDay7;
                numberOfFishPerGeneration[8] = creatingNewFish;
            }
            var totalFish = numberOfFishPerGeneration.Select(f => f.Value).Sum();
            Console.WriteLine($"After {daysToGenerate} days, there are {totalFish}");
        }
    }
}
