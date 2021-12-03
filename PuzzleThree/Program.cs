using System;
using System.Linq;
using System.IO;

namespace PuzzleThree
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var numberOfBits = input.First().Length;
            var mostCommonBits = new char[numberOfBits];
            var leastCommonBits = new char[numberOfBits];
            for(var i = 0; i < mostCommonBits.Length; i++)
            {
                var sum = input.Count(j => j[i] == '1');
                var mostCommon = sum > input.Length / 2 ? '1' : '0';
                var leastCommon = sum < input.Length / 2 ? '1' : '0';
                mostCommonBits[i] = mostCommon;
                leastCommonBits[i] = leastCommon;
            }

            var most = new string(mostCommonBits);
            var least = new string(leastCommonBits);
            var gamma = Convert.ToInt32(most, 2);
            var delta = Convert.ToInt32(least, 2);
            
            Console.WriteLine(most);
            Console.WriteLine(least);
            Console.WriteLine($"Power Consumption is Gamma: {gamma} * Delta: {delta} = {gamma * delta}");
        }
    }
}
