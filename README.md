# TurtleChallenge

A turtle must walk through a minefield, given a set of move operations the turtle must reach the exit point.

![](TurtleChallenge/Screenshot1.png)

## Project overview

This is a .NET Core Console Application that takes two file names as input arguments. The game settings file and the moves file.
The program will reach each line in the moves file and preform the moves operations on the turtle.
At the end of each sequence of moves the result will be printed to the screen. 
### The Game Settings 
JSON format was chosen for its readability and ease of use when deserializing in C#.
```json
{
  "board-size": {
    "length": 5,
    "width": 4
  },
  "starting-point": {
    "position": {
      "x": 0,
      "y": 1
    }
  },
  "direction": "North",
  "exit-point": {
    "position": {
      "x": 4,
      "y": 2
    }
  },
  "mines": [
    {
      "position": {
        "x": 1,
        "y": 1
      }
    },
    {
      "position": {
        "x": 3,
        "y": 1
      }
    },
    {
      "position": {
        "x": 3,
        "y": 3
      }
    }
  ]
}
```

### The Moves
The moves file is a csv file with each line representing a sequence of moves. 
Valid moves 'm'(Move forward one step) and 'r'(Rotate 90 degrees). 
Invalid move characters will be ignored.
Empty lines will be ignored.
```csv
m,r,m,m,m,m,r,m,m
r,m
```
