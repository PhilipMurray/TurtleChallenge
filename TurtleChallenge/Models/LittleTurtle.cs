using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Models
{
    public class LittleTurtle
    {
        private Position _initialPosition;
        private Direction _initialDirection;
        private Position _currentPosition;
        private Direction _currentDirection;
        private TurtleStatus _status;

        public LittleTurtle(Position initialPosition, Direction initialDirection)
        {
            _initialPosition = initialPosition;
            _initialDirection = initialDirection;
            _currentPosition = initialPosition;
            _currentDirection = initialDirection;
            _status = TurtleStatus.Alive;
        }

        public Position CurrentPosition { get => _currentPosition; }
        public Direction CurrentDirection { get => _currentDirection; }
        public TurtleStatus Status { get => _status; set => _status = value; }

        /// <summary>
        /// Performs the move operation on the little turtle.
        /// </summary>
        /// <param name="operation">The operation</param>
        public void PerformOperation(string operation)
        {
            switch (operation)
            {
                case ("m"):
                    Move();
                    break;
                case ("r"):
                    Rotate();
                    break;
                default:
                    break;
            }
        }

        public void Reset()
        {
            _currentPosition = _initialPosition;
            _currentDirection = _initialDirection;
        }
        private void Rotate()
        {
            switch (CurrentDirection)
            {
                case Direction.North:
                    _currentDirection = Direction.East;
                    break;
                case Direction.East:
                    _currentDirection = Direction.South;
                    break;
                case Direction.South:
                    _currentDirection = Direction.West;
                    break;
                case Direction.West:
                    _currentDirection = Direction.North;
                    break;
                default:
                    break;
            }
        }

        private void Move()
        {
            switch (CurrentDirection)
            {
                case Direction.North:
                    _currentPosition.Y -= 1;
                    break;
                case Direction.East:
                    _currentPosition.X += 1;
                    break;
                case Direction.South:
                    _currentPosition.Y += 1;
                    break;
                case Direction.West:
                    _currentPosition.X -= 1;
                    break;
                default:
                    break;
            }
        }

        
    }
}
