# 3x3x3RubiksCubeRobot
*Fully Autonomous Rubik's Cube solving robot*
## Overview
TODO
## Algorithm
Version 3 of the rubik's cube solving algorithm can be found in RubiksCubeV3/RubiksCubeV3/Program.cs. 
Warning: The code was not developped using proper software engineering principles and as such is very challenging to read.

The algorithm is loosely based on the CFOP speedcubing algorith (which stands for "Cross", "First 2 layers", "Orientation of last layer", "Permuation of last layer")
### Structrue of Cube/Moves
### Cross
### First 2 Layers (F2L)
### Last layer
This program uses a database containing the most move effecient algoirthm (sequence of moves) to solve every single one of the 15552 possible last layer states (after the first two layers have been solved). After the algorithm has been applied, the last layer will be solved but may require a rotation to solve the cube. This final move (if required), is then applied.

## Potential Optimizations
