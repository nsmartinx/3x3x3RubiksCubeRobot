# 3x3x3RubiksCubeRobot
*Fully Autonomous Rubik's Cube solving robot*

## Algorithm
Version 3 of the rubik's cube solving algorithm can be found in RubiksCubeV3/RubiksCubeV3/Program.cs. 
Warning: The code was not developed using proper software engineering principles and as such is very challenging to read. (This is due to it being developed years ago)

The algorithm used begins very similar to the Petrus method (a relatively move effecient method that has fallen out of favour in speedcubing due to the difficulty for human's to excecute it quickly), and draws significant inspiration from many FMC techniques.
### 1x2x2 Block
The algorithm brute forces moves to find the most optimal ways to build a 1x2x2 block. It will usually identify ~8 to use in the next step
### 2x2x2 Block
All identified 1x2x2 blocks will be extended to a 2x2x2 block, and the most move effecient ones will be kept
### 2x2x3 Block
The 2x2x2 block is expanded to 2x2x3 by attaching an aditional 1x2x2 block. Again, the most move effecient solutions are kept
### F2L - 1
One more 1x2x2 block is attached to form the first two layers, minus one slot. Only the most move effecient solution is kept
### F2L
The program contains a database of the most optimal algorithm for each of the 42 possible cases for the last two pieces of F2L (Ignoring beginning U moves), the algorithms identifies the case and applies it.
### Last Layer
At this point, the cube is solved except the last layer. The program identifies the case from the 15552 possible cases (ignoring AUF), and applies the most effecient known algorithm.

The method can consitently find a <40 move solution (HTM).
