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
            var littleTurtle = new LittleTurtle(gameSettings.StartingPoint.Position, gameSettings.Direction);

            /// This is the game loop.
            /// Each line in the moves file represents a sequence of moves.
            /// Empty line are skipped.
            using (var file = File.OpenText($"{args[1]}.csv"))
            {
                int seqCount = 0;
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

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

                    if (CheckForStillInDanger(littleTurtle))
                    {
                        Console.WriteLine($"Sequence {seqCount}: Still in danger!");
                    }

                    littleTurtle.Reset();
                }
            }
        }

        /// <summary>
        /// Method to read the game settings file and deserialize it into an object.
        /// </summary>
        /// <param name="fileName">The game settings file name.</param>
        /// <returns>The game settings object <see cref="GameSettings"/></returns>
        private static GameSettings ReadGameSettingsFile(string fileName)
        {
            using (var file = File.OpenText($"{fileName}.json"))
            {
                JsonSerializer serializer = new JsonSerializer();

                return (GameSettings)serializer.Deserialize(file, typeof(GameSettings));
            }
        }

        /// <summary>
        /// Method to check if the little turtle has moved outside the bounds of the game board
        /// and updates the turtle's status.
        /// </summary>
        /// <param name="littleTurtle">Our brave little turtle <see cref="LittleTurtle"/>.</param>
        /// <param name="boardSize">The board size.</param>
        /// <returns></returns>
        private static bool CheckForOutOfBounds(LittleTurtle littleTurtle, BoardSize boardSize)
        {
            if(littleTurtle.CurrentPosition.X < 0
                || littleTurtle.CurrentPosition.Y < 0
                || littleTurtle.CurrentPosition.X >= boardSize.Length
                || littleTurtle.CurrentPosition.Y >= boardSize.Width)
            {
                littleTurtle.Status = TurtleStatus.Lost;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to check if the turtle's status has changed to a final state.
        /// </summary>
        /// <param name="littleTurtle">Our brave little turtle <see cref="LittleTurtle"/>.</param>
        /// <returns></returns>
        private static bool CheckForStillInDanger(LittleTurtle littleTurtle)
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

        /// <summary>
        /// Method to check if the current position of the turtle matches the exit position.
        /// </summary>
        /// <param name="littleTurtle">Our brave little turtle <see cref="LittleTurtle"/></param>
        /// <param name="exitPoint">The exit point.</param>
        /// <returns></returns>
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
        /// Method to check if the position of the turtle matches a mine's position.
        /// </summary>
        /// <param name="littleTurtle">Our brave little turtle <see cref="LittleTurtle"/></param>
        /// <param name="mines">The collection of mines <see cref="Mine"/></param>
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
