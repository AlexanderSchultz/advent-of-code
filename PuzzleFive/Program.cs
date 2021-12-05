using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PuzzleFive
{
    class Program
    {
        static int FloorSize = 1000;
        static void Main(string[] args)
        {
            
            var input = File.ReadAllLines("input.txt");
            var ventLines = input.Select(i => new Vent(i));
            var floorMap = new int[FloorSize, FloorSize];
            
            foreach (var line in ventLines)
            {
                line.AddToMap(floorMap);
            } 
            
            int overlap = 0;
            for (int x = 0; x < FloorSize; x++)
            {
                for (int y = 0; y < FloorSize; y++)
                {
                    var value = floorMap[x, y];
                    if(value > 1)
                    {
                        overlap++;
                    }
                }                
            }
            Console.WriteLine($"Overlapping lines: {overlap}");
        }

        static void PrintFloorMap(int[,] floorMap)
        {
            for (int x = 0; x < FloorSize; x++)
            {
                StringBuilder row = new StringBuilder();
                for (int y = 0; y < FloorSize; y++)
                {
                    var value = floorMap[x, y];
                    if(value == 0){
                        row.Append(".");
                    }
                    else
                    {
                        row.Append(value);
                    }
                }
                Console.WriteLine(row.ToString());
            }
        }
    }



    class Vent
    {
        public int X1 { get; private set; }
        public int X2 { get; private set; }
        public int Y1 { get; private set; }
        public int Y2 { get; private set; }
        public bool IsHorizonal => Y1 == Y2;
        public bool IsVertical => X1 == X2;
        public Vent(string line)
        {
            var points = line.Split(" -> ");
            var startPoints = points[0].Split(",");
            var endPoints = points[1].Split(",");
            X1 = int.Parse(startPoints[0]);
            Y1 = int.Parse(startPoints[1]);
            X2 = int.Parse(endPoints[0]);
            Y2 = int.Parse(endPoints[1]);
        }

        public void AddToMap(int[,] floorMap)
        {
            if(IsVertical)
            {
                var diff = Math.Abs(Y2 - Y1);
                int start = Y1 <= Y2 ? Y1 : Y2;
                for (int i = start; i <= start + diff; i++)
                {   
                    floorMap[i, X1]++;
                }
            } else if (IsHorizonal) {
                
                var diff = Math.Abs(X2 - X1);
                int start = X1 <= X2 ? X1 : X2;
                for (int i = start; i <= start + diff; i++)
                {
                    floorMap[Y1, i]++;
                }
            } 
            else 
            {
                var diff = Math.Abs(X2 - X1);
                for (int i = 0; i <= diff; i++)
                {
                    int xPosition = X1 < X2 ? X1 + i : X1 - i;
                    int yPosition = Y1 < Y2 ? Y1 + i : Y1 - i;
                    floorMap[yPosition, xPosition]++;
                }
            }
        }
    }
}
