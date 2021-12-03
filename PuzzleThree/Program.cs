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
            
            // var input = new string[]
            // {
            //     "00100",
            //     "11110",
            //     "10110",
            //     "10111",
            //     "10101",
            //     "01111",
            //     "00111",
            //     "11100",
            //     "10000",
            //     "11001",
            //     "00010",
            //     "01010",
            // };

            FindLifeSupportRating(input);
        }

        private static void FindLifeSupportRating(string[] input)
        {
            var oxygenGenerator = FindRatingByMostCommon(input, 0);
            var co2Scrubber = FindRatingByLeastCommon(input, 0);
            var oxyRating = Convert.ToInt32(new string(oxygenGenerator[0]), 2);
            var co2Rating = Convert.ToInt32(new string(co2Scrubber[0]), 2);
            Console.WriteLine(oxyRating);
            Console.WriteLine(co2Rating);
            Console.WriteLine($"Life Support is Oxy: {oxyRating} * co2: {co2Rating} = {oxyRating * co2Rating}");
        }

        private static string[] FindRatingByMostCommon(string[] input, int bitPosition)
        {
            if(input.Length == 1)
            {
                return input;
            }

            var numberOfOnes = input.Count(j => j[bitPosition] == '1');
            var numberOfZeros = input.Count(j => j[bitPosition] == '0');
            var mostCommon = numberOfOnes >= numberOfZeros ? '1' : '0';
            var matchingMostCommon = input.Where(i => i[bitPosition] == mostCommon).ToArray();
            var newBitPosition = bitPosition + 1;
            
            return FindRatingByMostCommon(matchingMostCommon, newBitPosition);
        }

        private static string[] FindRatingByLeastCommon(string[] input, int bitPosition)
        {
            if(input.Length == 1)
            {
                return input;
            }

            var numberOfOnes = input.Count(j => j[bitPosition] == '1');
            var numberOfZeros = input.Count(j => j[bitPosition] == '0');
            var leastCommon = numberOfOnes >= numberOfZeros ? '0' : '1';
            var matchingLeastCommon = input.Where(i => i[bitPosition] == leastCommon).ToArray();
            var newBitPosition = bitPosition + 1;
            
            return FindRatingByLeastCommon(matchingLeastCommon, newBitPosition);
        }

        private void FindPowerConsumption(string[] input)
        {
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
