using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TurtleChallenge.Enums;
using TurtleChallenge.Models;

namespace TurtleChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSettings gameSettings = ReadGameSettingsFile(args[0]);
            Moves moves = ReadMovesFile(args[1]);

            var littleTurtle = new LittleTurtle(gameSettings.StartingPoint.Position, gameSettings.Direction);

            using (var file = File.OpenText($"{args[1]}.csv"))
            {
                int seqCount = 0;
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    seqCount++;
                    var operations = line.Split(',');

                    foreach (var operation in operations)
                    {
                        littleTurtle.PerformOperation(operation);

                        if (CheckForMine(littleTurtle, gameSettings.Mines))
                        {
                            Console.WriteLine($"Sequence {seqCount}: Mine Hit!");
                            break;
                        }
                        else if (CheckForEscape(littleTurtle, gameSettings.ExitPoint))
                        {
                            Console.WriteLine($"Sequence {seqCount}: Success!");
                            break;
                        }
                        else if (CheckForOutOfBounds(littleTurtle, gameSettings.BoardSize))
                        {
                            Console.WriteLine($"Sequence {seqCount}: Turtle has moved off the board and is now lost forever!");
                            break;
                        }
                    }

                    if (CheckForLost(littleTurtle))
                    {
                        Console.WriteLine($"Sequence {seqCount}: Still in danger!");
                    }

                    littleTurtle.Reset();
                }
            }

            foreach (var seq in moves.Sequences)
            {
                foreach (var operation in seq.MoveOperations)
                {
                    littleTurtle.PerformOperation(operation);

                    if (CheckForMine(littleTurtle, gameSettings.Mines))
                    {
                        Console.WriteLine($"Sequence {seq.Id}: Mine Hit!");
                        break;
                    }
                    else if (CheckForEscape(littleTurtle, gameSettings.ExitPoint))
                    {
                        Console.WriteLine($"Sequence {seq.Id}: Success!");
                        break;
                    }
                    else if (CheckForOutOfBounds(littleTurtle, gameSettings.BoardSize))
                    {
                        Console.WriteLine($"Sequence {seq.Id}: Turtle has moved off the board and is now lost forever!");
                        break;
                    }
                }

                if (CheckForLost(littleTurtle))
                {
                    Console.WriteLine($"Sequence {seq.Id}: Still in danger!");
                }

                littleTurtle.Reset();
            }

        }

        private static Moves ReadMovesFile(string fileName)
        {
            using (var file = File.OpenText($"{fileName}.json"))
            {
                JsonSerializer serializer = new JsonSerializer();

                return (Moves)serializer.Deserialize(file, typeof(Moves));
            }
        }

        private static GameSettings ReadGameSettingsFile(string fileName)
        {
            using (var file = File.OpenText($"{fileName}.json"))
            {
                JsonSerializer serializer = new JsonSerializer();

                return (GameSettings)serializer.Deserialize(file, typeof(GameSettings));
            }
        }

        private static bool CheckForOutOfBounds(LittleTurtle littleTurtle, BoardSize boardSize)
        {
            if(littleTurtle.CurrentPosition.X < 0
                || littleTurtle.CurrentPosition.Y < 0
                || littleTurtle.CurrentPosition.X >= boardSize.Length
                || littleTurtle.CurrentPosition.Y >= boardSize.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckForLost(LittleTurtle littleTurtle)
        {
            if(littleTurtle.Status == TurtleStatus.Alive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckForEscape(LittleTurtle littleTurtle, ExitPoint exitPoint)
        {
            if(exitPoint.Position.Equals(littleTurtle.CurrentPosition))
            {
                littleTurtle.Status = TurtleStatus.Escaped;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="littleTurtle"></param>
        /// <param name="mines"></param>
        /// <returns></returns>
        private static bool CheckForMine(LittleTurtle littleTurtle, List<Mine> mines)
        {
            if(mines.Select(x => x.Position).Contains(littleTurtle.CurrentPosition))
            {
                littleTurtle.Status = TurtleStatus.Dead;
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
