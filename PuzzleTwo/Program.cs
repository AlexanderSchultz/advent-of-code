using System;
using System.IO;
using System.Linq;

namespace PuzzleTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int aim = 0;
            int forward = 0;
            int vertical = 0;
            foreach (var line in input)
            {
                string[] vector = line.Split(" ");
                string direction = vector[0];
                int value = int.Parse(vector[1]);
                switch (direction)
                {
                    case "forward":
                        forward += value;
                        vertical += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }

            Console.WriteLine($"Forward Movement is {forward}");
            Console.WriteLine($"Vertical Movement is {vertical}");
            Console.WriteLine($"Aim is {aim}");
            Console.WriteLine($"Multiplication of Movements is {forward * vertical}");
        }

        static void Simple(string[] input)
        {
            var forward = input
                .Where(i => i.Contains("forward "))
                .Select(i => int.Parse(i.Remove(0, 8)))
                .Sum();

            var down = input
                .Where(i => i.Contains("down "))
                .Select(i => int.Parse(i.Remove(0, 5)))
                .Sum();
            var up = input
                .Where(i => i.Contains("up "))
                .Select(i => int.Parse(i.Remove(0, 3)))
                .Sum();
            var vertical = down - up;

            Console.WriteLine($"Forward Movement is {forward}");
            Console.WriteLine($"Vertical Movement is {vertical}");
            Console.WriteLine($"Multiplication of Movements is {forward * vertical}");
        }
    }
}
