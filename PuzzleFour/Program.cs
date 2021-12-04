using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PuzzleFour
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").ToList();
            var numbersToDraw = lines[0].Split(",");

            List<BingoBoard> boards = new List<BingoBoard>();
            int numberOfBoards = (lines.Count() - 2) / 6;
            for (int i = 0; i < numberOfBoards; i++)
            {
                var boardLines = lines.GetRange((i * 6) + 2, 5).ToArray();
                boards.Add(new BingoBoard(boardLines));
            }

            List<int> boardWinningIndexes = new List<int>();

            foreach (var number in numbersToDraw)
            {
                foreach (var board in boards)
                {
                    var boardIndex = boards.IndexOf(board);
                    board.DrawNumber(number);
                    if(board.HasWon && !boardWinningIndexes.Contains(boardIndex))
                    {
                        boardWinningIndexes.Add(boardIndex);
                    }
                }
            }

            Console.WriteLine(string.Join(",", boardWinningIndexes));

            var lastWinningBoard = boards[boardWinningIndexes.Last()];
            lastWinningBoard.PrintBoard();
            Console.WriteLine($"Last Winning Board has Score {lastWinningBoard.FinalScore}");
            
        }
    }

    class BingoBoard 
    {
        private string[,] Board { get; set; }
        private string[] Rows { get; }
        private List<string> DrawnNumbers { get; set; }

        public int FinalScore { get; private set; }
        
        public bool HasWon { get; private set; }

        public BingoBoard(string[] rows)
        {
            DrawnNumbers = new List<string>();
            Board = new string[5,5];
            Rows = rows;
            for (int i = 0; i < 5; i++)
            {
                var row = rows[i];
                var values = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < 5; j++)
                {
                    Board[i, j] = values[j];     
                }
            }
        }

        public void DrawNumber(string number)
        {
            DrawnNumbers.Add(number);
            UpdateHasWon();
            UpdateFinalScore();
        }

        private void UpdateHasWon()
        {
            if(DrawnNumbers.Count < 5 || HasWon)
            {
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                if(HasWon)
                {
                    continue;
                }

                HasWon = RowAllDrawn(i) || ColumnAllDrawn(i);
            }
        }

        private void UpdateFinalScore()
        {
            if(!HasWon || FinalScore > 0)
                return;

            List<string> undrawnNumbers = new List<string>();

            for (int row = 0; row < 5; row++)
            {   
                for(int column = 0; column < 5; column++)
                {
                    if(!DrawnNumbers.Contains(Board[row, column]))
                    {
                        undrawnNumbers.Add(Board[row, column]);
                    }
                }   
            }
            
            FinalScore = undrawnNumbers.Select(n => int.Parse(n)).Sum() * int.Parse(DrawnNumbers.Last());    
        }

        private bool RowAllDrawn(int rowIndex)
        {
            return DrawnNumbers.Contains(Board[rowIndex, 0]) 
                    && DrawnNumbers.Contains(Board[rowIndex, 1]) 
                    && DrawnNumbers.Contains(Board[rowIndex, 2]) 
                    && DrawnNumbers.Contains(Board[rowIndex, 3]) 
                    && DrawnNumbers.Contains(Board[rowIndex, 4]);
        }

        private bool ColumnAllDrawn(int columnIndex)
        {
            return DrawnNumbers.Contains(Board[0, columnIndex]) 
                    && DrawnNumbers.Contains(Board[1, columnIndex]) 
                    && DrawnNumbers.Contains(Board[2, columnIndex]) 
                    && DrawnNumbers.Contains(Board[3, columnIndex]) 
                    && DrawnNumbers.Contains(Board[4, columnIndex]);
        }

        internal void PrintBoard()
        {
            Console.WriteLine("Drawn Numbers");
            Console.WriteLine(string.Join(",", DrawnNumbers));
            Console.WriteLine();
            Console.WriteLine("Board");
            foreach (var row in Rows)
            {
                Console.WriteLine(row);
            }
        }
    }
}
