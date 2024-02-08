using System;
using System.Collections.Generic;
using System.Linq;

namespace Rubik_s_Cube_Test1
{
    class Program
    {
        static List<String> moves = new List<string>();
        static String[,,] faces = new String[6, 3, 3];
        static string[,,] facesCopy = new string[6, 3, 3];
        static List<string> scramble = new List<string>();

        static void Main()
        {
            /*
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        Console.Write("[{0}, {1}, {2}]: ", x, y, z);
                        faces[x, y, z] = Console.ReadLine();
                    }
                }
            }
            */
            Scramble();
            Test();

            WhiteCross();
            F2L();
            OLLEdges();
            OLLCorners();
            PLLCorners();
            PLLEdges();
            TurnLastLayer();
            


            shortenMoveSequence();

            Console.Write("Moves: ");
            foreach (var move in moves)
            {
                Console.Write(move + " ");

            }
            
            Console.Write("\n\n");
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        Console.Write(faces[x, y, z]);
                        if (z == 2)
                            Console.Write("\n");
                        if (y == 2 && z == 2)
                            Console.Write("\n");
                        //Console.Write("[{0}, {1}, {2}]: {3}\n", x, y, z, faces[x, y, z]);
                    }
                }
            }


            Console.Write("Scramble: ");
            foreach (var move in scramble)
            {
                Console.Write(move + " ");

            }
        }
        static void shortenMoveSequence()
        {
            Console.Write("\n\n\n");
            int moveCount = 0;
            List<int> movesToRemove = new List<int>();
            List<int> movesToChange = new List<int>();
            bool wasModified = false;
            string lastMove1 = null, lastMove2 = null;
            do
            {
                wasModified = false;
                moveCount = 0;
                lastMove1 = null;
                lastMove2 = null;
                foreach (string move in moves)
                {
                    if (move == lastMove1 + "'" || lastMove1 == move + "'")//if there was x and x' next to each other
                    {
                        moves.RemoveAt(moveCount);
                        moves.RemoveAt(moveCount - 1);
                        wasModified = true;
                        if (moveCount >= 2)
                            lastMove2 = lastMove1;
                        if (moveCount >= 1)
                            lastMove1 = move;
                        break;
                    }
                    if(move == lastMove1 && move == lastMove2)
                    {
                        if (moves[moveCount] == "w")
                            moves[moveCount] = "w'";
                        else if (moves[moveCount] == "y")
                            moves[moveCount] = "y'";
                        else  if (moves[moveCount] == "r")
                            moves[moveCount] = "r'";
                        else if (moves[moveCount] == "o")
                            moves[moveCount] = "o'";
                        else if (moves[moveCount] == "b")
                            moves[moveCount] = "b'";
                        else if (moves[moveCount] == "g")
                            moves[moveCount] = "g'";
                        else if (moves[moveCount] == "w'")
                            moves[moveCount] = "w";
                        else if (moves[moveCount] == "y'")
                            moves[moveCount] = "y";
                        else if (moves[moveCount] == "r'")
                            moves[moveCount] = "r";
                        else if (moves[moveCount] == "o'")
                            moves[moveCount] = "o";
                        else if (moves[moveCount] == "b'")
                            moves[moveCount] = "b";
                        else if (moves[moveCount] == "g'")
                            moves[moveCount] = "g";

                        moves.RemoveAt(moveCount - 1);
                        moves.RemoveAt(moveCount - 2);
                        wasModified = true;
                        break;
                    }

                    if (moveCount >= 1)
                        lastMove2 = lastMove1;
                    if (moveCount >= 0)
                        lastMove1 = move;

                    moveCount++;
                }
            } while (wasModified);

            foreach (var moveToRemove in movesToRemove)
            {
                moves[moveToRemove] = " ";
            }
            foreach (var moveToChange in movesToRemove)
            {
                moves[moveToChange - 1] = " ";
                moves[moveToChange - 2] = " ";
                if (moves[moveToChange].Length == 1)
                {
                    moves[moveToChange] = moves[moveToChange] + "'";
                }
                else
                {
                    if (moves[moveToChange] == "w'")
                        moves[moveToChange] = "w";
                    if (moves[moveToChange] == "y'")
                        moves[moveToChange] = "y";
                    if (moves[moveToChange] == "r'")
                        moves[moveToChange] = "r";
                    if (moves[moveToChange] == "o'")
                        moves[moveToChange] = "o";
                    if (moves[moveToChange] == "b'")
                        moves[moveToChange] = "b";
                    if (moves[moveToChange] == "g'")
                        moves[moveToChange] = "g";
                }
            }
        }
        static void Scramble()
        {
            Random rand = new Random();

            faces[0, 0, 0] = "w";
            faces[0, 0, 1] = "w";
            faces[0, 0, 2] = "w";
            faces[0, 1, 0] = "w";
            faces[0, 1, 1] = "w";
            faces[0, 1, 2] = "w";
            faces[0, 2, 0] = "w";
            faces[0, 2, 1] = "w";
            faces[0, 2, 2] = "w";

            faces[1, 0, 0] = "y";
            faces[1, 0, 1] = "y";
            faces[1, 0, 2] = "y";
            faces[1, 1, 0] = "y";
            faces[1, 1, 1] = "y";
            faces[1, 1, 2] = "y";
            faces[1, 2, 0] = "y";
            faces[1, 2, 1] = "y";
            faces[1, 2, 2] = "y";

            faces[2, 0, 0] = "r";
            faces[2, 0, 1] = "r";
            faces[2, 0, 2] = "r";
            faces[2, 1, 0] = "r";
            faces[2, 1, 1] = "r";
            faces[2, 1, 2] = "r";
            faces[2, 2, 0] = "r";
            faces[2, 2, 1] = "r";
            faces[2, 2, 2] = "r";

            faces[3, 0, 0] = "o";
            faces[3, 0, 1] = "o";
            faces[3, 0, 2] = "o";
            faces[3, 1, 0] = "o";
            faces[3, 1, 1] = "o";
            faces[3, 1, 2] = "o";
            faces[3, 2, 0] = "o";
            faces[3, 2, 1] = "o";
            faces[3, 2, 2] = "o";

            faces[4, 0, 0] = "b";
            faces[4, 0, 1] = "b";
            faces[4, 0, 2] = "b";
            faces[4, 1, 0] = "b";
            faces[4, 1, 1] = "b";
            faces[4, 1, 2] = "b";
            faces[4, 2, 0] = "b";
            faces[4, 2, 1] = "b";
            faces[4, 2, 2] = "b";

            faces[5, 0, 0] = "g";
            faces[5, 0, 1] = "g";
            faces[5, 0, 2] = "g";
            faces[5, 1, 0] = "g";
            faces[5, 1, 1] = "g";
            faces[5, 1, 2] = "g";
            faces[5, 2, 0] = "g";
            faces[5, 2, 1] = "g";
            faces[5, 2, 2] = "g";


            

            for (int i = 0; i < 20; i++)
            {
                int random = rand.Next(12);

                if (random == 0)
                {
                    Turn("w");
                    scramble.Add("w");
                }
                else if (random == 1)
                {
                    Turn("w'");
                    scramble.Add("w'");
                }
                else if (random == 2)
                {
                    Turn("y");
                    scramble.Add("y");
                }
                else if (random == 3)
                {
                    Turn("y'");
                    scramble.Add("y'");
                }
                else if (random == 4)
                {
                    Turn("r");
                    scramble.Add("r");
                }
                else if (random == 5)
                {
                    Turn("r'");
                    scramble.Add("r'");
                }
                else if (random == 6)
                {
                    Turn("o");
                    scramble.Add("o");
                }
                else if (random == 7)
                {
                    Turn("o'");
                    scramble.Add("o'");
                }
                else if (random == 8)
                {
                    Turn("b");
                    scramble.Add("b");
                }
                else if (random == 9)
                {
                    Turn("b'");
                    scramble.Add("b'");
                }
                else if (random == 10)
                {
                    Turn("g");
                    scramble.Add("g");
                }
                else if (random == 11)
                {
                    Turn("g'");
                    scramble.Add("g'");
                }
                moves = new List<string>();
            }
        }
        static void Test()
        {/*
            faces[0, 0, 0] = "o";
            faces[0, 0, 1] = "w";
            faces[0, 0, 2] = "b";
            faces[0, 1, 0] = "w";
            faces[0, 1, 1] = "w";
            faces[0, 1, 2] = "w";
            faces[0, 2, 0] = "o";
            faces[0, 2, 1] = "b";
            faces[0, 2, 2] = "r";

            faces[1, 0, 0] = "y";
            faces[1, 0, 1] = "b";
            faces[1, 0, 2] = "b";
            faces[1, 1, 0] = "y";
            faces[1, 1, 1] = "y";
            faces[1, 1, 2] = "o";
            faces[1, 2, 0] = "g";
            faces[1, 2, 1] = "g";
            faces[1, 2, 2] = "o";

            faces[2, 0, 0] = "r";
            faces[2, 0, 1] = "b";
            faces[2, 0, 2] = "g";
            faces[2, 1, 0] = "g";
            faces[2, 1, 1] = "r";
            faces[2, 1, 2] = "g";
            faces[2, 2, 0] = "g";
            faces[2, 2, 1] = "o";
            faces[2, 2, 2] = "y";

            faces[3, 0, 0] = "b";
            faces[3, 0, 1] = "r";
            faces[3, 0, 2] = "g";
            faces[3, 1, 0] = "b";
            faces[3, 1, 1] = "o";
            faces[3, 1, 2] = "g";
            faces[3, 2, 0] = "b";
            faces[3, 2, 1] = "r";
            faces[3, 2, 2] = "w";

            faces[4, 0, 0] = "w";
            faces[4, 0, 1] = "r";
            faces[4, 0, 2] = "w";
            faces[4, 1, 0] = "y";
            faces[4, 1, 1] = "b";
            faces[4, 1, 2] = "y";
            faces[4, 2, 0] = "r";
            faces[4, 2, 1] = "y";
            faces[4, 2, 2] = "y";

            faces[5, 0, 0] = "y";
            faces[5, 0, 1] = "o";
            faces[5, 0, 2] = "w";
            faces[5, 1, 0] = "w";
            faces[5, 1, 1] = "g";
            faces[5, 1, 2] = "o";
            faces[5, 2, 0] = "r";
            faces[5, 2, 1] = "r";
            faces[5, 2, 2] = "o";
            */
            /*
            faces[0, 0, 0] = "r";
            faces[0, 0, 1] = "b";
            faces[0, 0, 2] = "r";
            faces[0, 1, 0] = "w";
            faces[0, 1, 1] = "w";
            faces[0, 1, 2] = "w";
            faces[0, 2, 0] = "w";
            faces[0, 2, 1] = "w";
            faces[0, 2, 2] = "w";

            faces[1, 0, 0] = "w";
            faces[1, 0, 1] = "o";
            faces[1, 0, 2] = "o";
            faces[1, 1, 0] = "y";
            faces[1, 1, 1] = "y";
            faces[1, 1, 2] = "o";
            faces[1, 2, 0] = "y";
            faces[1, 2, 1] = "y";
            faces[1, 2, 2] = "y";

            faces[2, 0, 0] = "w";
            faces[2, 0, 1] = "r";
            faces[2, 0, 2] = "g";
            faces[2, 1, 0] = "r";
            faces[2, 1, 1] = "r";
            faces[2, 1, 2] = "g";
            faces[2, 2, 0] = "g";
            faces[2, 2, 1] = "g";
            faces[2, 2, 2] = "y";

            faces[3, 0, 0] = "o";
            faces[3, 0, 1] = "o";
            faces[3, 0, 2] = "o";
            faces[3, 1, 0] = "o";
            faces[3, 1, 1] = "o";
            faces[3, 1, 2] = "r";
            faces[3, 2, 0] = "g";
            faces[3, 2, 1] = "g";
            faces[3, 2, 2] = "o";

            faces[4, 0, 0] = "y";
            faces[4, 0, 1] = "b";
            faces[4, 0, 2] = "b";
            faces[4, 1, 0] = "y";
            faces[4, 1, 1] = "b";
            faces[4, 1, 2] = "y";
            faces[4, 2, 0] = "b";
            faces[4, 2, 1] = "b";
            faces[4, 2, 2] = "r";

            faces[5, 0, 0] = "g";
            faces[5, 0, 1] = "g";
            faces[5, 0, 2] = "b";
            faces[5, 1, 0] = "g";
            faces[5, 1, 1] = "g";
            faces[5, 1, 2] = "w";
            faces[5, 2, 0] = "g";
            faces[5, 2, 1] = "r";
            faces[5, 2, 2] = "r";
            */
            /*
            //Scramble: w o y r' g o g' y' w w y' b
            faces[0, 0, 0] = "y";
            faces[0, 0, 1] = "y";
            faces[0, 0, 2] = "r";
            faces[0, 1, 0] = "g";
            faces[0, 1, 1] = "w";
            faces[0, 1, 2] = "w";
            faces[0, 2, 0] = "g";
            faces[0, 2, 1] = "b";
            faces[0, 2, 2] = "r";

            faces[1, 0, 0] = "w";
            faces[1, 0, 1] = "o";
            faces[1, 0, 2] = "o";
            faces[1, 1, 0] = "y";
            faces[1, 1, 1] = "y";
            faces[1, 1, 2] = "b";
            faces[1, 2, 0] = "g";
            faces[1, 2, 1] = "g";
            faces[1, 2, 2] = "b";

            faces[2, 0, 0] = "b";
            faces[2, 0, 1] = "o";
            faces[2, 0, 2] = "o";
            faces[2, 1, 0] = "b";
            faces[2, 1, 1] = "r";
            faces[2, 1, 2] = "o";
            faces[2, 2, 0] = "r";
            faces[2, 2, 1] = "g";
            faces[2, 2, 2] = "g";

            faces[3, 0, 0] = "o";
            faces[3, 0, 1] = "r";
            faces[3, 0, 2] = "g";
            faces[3, 1, 0] = "w";
            faces[3, 1, 1] = "o";
            faces[3, 1, 2] = "g";
            faces[3, 2, 0] = "o";
            faces[3, 2, 1] = "r";
            faces[3, 2, 2] = "w";

            faces[4, 0, 0] = "b";
            faces[4, 0, 1] = "y";
            faces[4, 0, 2] = "w";
            faces[4, 1, 0] = "b";
            faces[4, 1, 1] = "b";
            faces[4, 1, 2] = "r";
            faces[4, 2, 0] = "y";
            faces[4, 2, 1] = "y";
            faces[4, 2, 2] = "w";

            faces[5, 0, 0] = "y";
            faces[5, 0, 1] = "o";
            faces[5, 0, 2] = "y";
            faces[5, 1, 0] = "w";
            faces[5, 1, 1] = "g";
            faces[5, 1, 2] = "w";
            faces[5, 2, 0] = "r";
            faces[5, 2, 1] = "r";
            faces[5, 2, 2] = "b";
            */
            /*
            string[,,] facesCopy = new string[6,3,3];
            Array.Copy(faces, facesCopy, 54);
            */
            /*
            faces[0, 0, 0] = "b";
            faces[0, 0, 1] = "b";
            faces[0, 0, 2] = "r";
            faces[0, 1, 0] = "w";
            faces[0, 1, 1] = "w";
            faces[0, 1, 2] = "g";
            faces[0, 2, 0] = "y";
            faces[0, 2, 1] = "w";
            faces[0, 2, 2] = "b";

            faces[1, 0, 0] = "o";
            faces[1, 0, 1] = "b";
            faces[1, 0, 2] = "o";
            faces[1, 1, 0] = "g";
            faces[1, 1, 1] = "y";
            faces[1, 1, 2] = "o";
            faces[1, 2, 0] = "r";
            faces[1, 2, 1] = "g";
            faces[1, 2, 2] = "r";

            faces[2, 0, 0] = "b";
            faces[2, 0, 1] = "y";
            faces[2, 0, 2] = "w";
            faces[2, 1, 0] = "y";
            faces[2, 1, 1] = "r";
            faces[2, 1, 2] = "b";
            faces[2, 2, 0] = "w";
            faces[2, 2, 1] = "o";
            faces[2, 2, 2] = "w";

            faces[3, 0, 0] = "o";
            faces[3, 0, 1] = "r";
            faces[3, 0, 2] = "y";
            faces[3, 1, 0] = "g";
            faces[3, 1, 1] = "o";
            faces[3, 1, 2] = "r";
            faces[3, 2, 0] = "g";
            faces[3, 2, 1] = "o";
            faces[3, 2, 2] = "g";

            faces[4, 0, 0] = "r";
            faces[4, 0, 1] = "o";
            faces[4, 0, 2] = "g";
            faces[4, 1, 0] = "w";
            faces[4, 1, 1] = "b";
            faces[4, 1, 2] = "w";
            faces[4, 2, 0] = "g";
            faces[4, 2, 1] = "y";
            faces[4, 2, 2] = "y";

            faces[5, 0, 0] = "o";
            faces[5, 0, 1] = "y";
            faces[5, 0, 2] = "y";
            faces[5, 1, 0] = "b";
            faces[5, 1, 1] = "g";
            faces[5, 1, 2] = "r";
            faces[5, 2, 0] = "w";
            faces[5, 2, 1] = "r";
            faces[5, 2, 2] = "b";
            */
            /*
            faces[0, 0, 0] = "r";
            faces[0, 0, 1] = "w";
            faces[0, 0, 2] = "g";
            faces[0, 1, 0] = "o";
            faces[0, 1, 1] = "w";
            faces[0, 1, 2] = "o";
            faces[0, 2, 0] = "b";
            faces[0, 2, 1] = "r";
            faces[0, 2, 2] = "r";

            faces[1, 0, 0] = "r";
            faces[1, 0, 1] = "w";
            faces[1, 0, 2] = "o";
            faces[1, 1, 0] = "o";
            faces[1, 1, 1] = "y";
            faces[1, 1, 2] = "y";
            faces[1, 2, 0] = "y";
            faces[1, 2, 1] = "w";
            faces[1, 2, 2] = "g";

            faces[2, 0, 0] = "w";
            faces[2, 0, 1] = "b";
            faces[2, 0, 2] = "y";
            faces[2, 1, 0] = "g";
            faces[2, 1, 1] = "r";
            faces[2, 1, 2] = "y";
            faces[2, 2, 0] = "g";
            faces[2, 2, 1] = "r";
            faces[2, 2, 2] = "y";

            faces[3, 0, 0] = "w";
            faces[3, 0, 1] = "b";
            faces[3, 0, 2] = "b";
            faces[3, 1, 0] = "r";
            faces[3, 1, 1] = "o";
            faces[3, 1, 2] = "g";
            faces[3, 2, 0] = "w";
            faces[3, 2, 1] = "g";
            faces[3, 2, 2] = "o";

            faces[4, 0, 0] = "b";
            faces[4, 0, 1] = "g";
            faces[4, 0, 2] = "o";
            faces[4, 1, 0] = "b";
            faces[4, 1, 1] = "b";
            faces[4, 1, 2] = "y";
            faces[4, 2, 0] = "b";
            faces[4, 2, 1] = "o";
            faces[4, 2, 2] = "o";

            faces[5, 0, 0] = "w";
            faces[5, 0, 1] = "b";
            faces[5, 0, 2] = "r";
            faces[5, 1, 0] = "r";
            faces[5, 1, 1] = "g";
            faces[5, 1, 2] = "y";
            faces[5, 2, 0] = "g";
            faces[5, 2, 1] = "w";
            faces[5, 2, 2] = "y";
            */

            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        Console.Write(faces[x, y, z]);
                        if (z == 2)
                            Console.Write("\n");
                        if (y == 2 && z == 2)
                            Console.Write("\n");
                        //Console.Write("[{0}, {1}, {2}]: {3}\n", x, y, z, faces[x, y, z]);
                    }
                }
            }


            /*
            if (faces.OfType<String>().SequenceEqual(facesCopy.OfType<String>()))
            {
                Console.WriteLine("\nSuccess");
            }
            else
            {
                Console.WriteLine("\nFailure");
            }
            */
        }
        static void WhiteCross()
        {
            Array.Copy(faces, facesCopy, 54);

            
            int[] crossMoves = new int[6];

            int completedWhiteSides = 0;
            int countedWhiteSides = 0;

            if (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r")
                completedWhiteSides++;
            if (facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b")
                completedWhiteSides++;
            if (facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g")
                completedWhiteSides++;
            if (facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o")
                completedWhiteSides++;
            //facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"
            while (completedWhiteSides < 4)
            {  
                Array.Copy(faces, facesCopy, 54);

                crossMoves[0]++;

                if (crossMoves[0] > 18)
                {
                    crossMoves[0] = 1;
                    crossMoves[1]++;
                    if (crossMoves[1] > 18)
                    {
                        crossMoves[1] = 1;
                        crossMoves[2]++;
                        if (crossMoves[2] > 18)
                        {
                            crossMoves[2] = 1;
                            crossMoves[3]++;
                            if (crossMoves[3] > 18)
                            {
                                crossMoves[3] = 1;
                                crossMoves[4]++;
                                if (crossMoves[4] > 18)
                                {
                                    crossMoves[4] = 1;
                                    crossMoves[5]++;
                                }
                            }
                        }
                    }
                }
                TurnSequence(crossMoves, true);//apply moves to copy of cubes

                completedWhiteSides = 0;//number of white edges in place
                if (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r")
                {
                    completedWhiteSides++;
                }
                if (facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b")
                {
                    completedWhiteSides++;
                }
                if (facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g")
                {
                    completedWhiteSides++;
                }
                if (facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o")
                {
                    completedWhiteSides++;
                }

                if (completedWhiteSides > countedWhiteSides)
                {
                    TurnSequence(crossMoves, false);//apply moves to actual cube
                    foreach (var move in crossMoves)
                    {
                        Console.Write(move + " ");
                    }
                    Console.Write("\n");
                    crossMoves = new int[6];//clears move array
                    countedWhiteSides = completedWhiteSides;

                    
                }
            }
        }
        static void F2L()
        {
            bool allowedSpecialCase = false;

            int[] f2LMoves = new int[7];

            Array.Copy(faces, facesCopy, 54);

            int pairsCounted = 0;

            int pairsInserted = 0;
            if (facesCopy[0, 0, 0] == "w" && facesCopy[2, 0, 2] == "r" && facesCopy[2, 1, 2] == "r" && facesCopy[4, 0, 0] == "b" && facesCopy[4, 1, 0] == "b" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "r" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "r" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "r"))
            {
                pairsInserted++;
            }
            if (facesCopy[0, 0, 2] == "w" && facesCopy[5, 0, 2] == "g" && facesCopy[5, 1, 2] == "g" && facesCopy[2, 0, 0] == "r" && facesCopy[2, 1, 0] == "r" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "r" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "r" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "r"))
            {
                pairsInserted++;
            }
            if (facesCopy[0, 2, 2] == "w" && facesCopy[3, 0, 2] == "o" && facesCopy[3, 1, 2] == "o" && facesCopy[5, 0, 0] == "g" && facesCopy[5, 1, 0] == "g" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "r" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "r" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "r"))
            {
                pairsInserted++;
            }
            if (facesCopy[0, 2, 0] == "w" && facesCopy[4, 0, 2] == "b" && facesCopy[4, 1, 2] == "b" && facesCopy[3, 0, 0] == "o" && facesCopy[3, 1, 0] == "o" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "r" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "r" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "r"))
            {
                pairsInserted++;
            }

            pairsCounted = pairsInserted;

            f2LMoves[0]++;

            while (pairsCounted < 4)
            {
                List<string> f2LAdditionalMoves = new List<string>();
                Array.Copy(faces, facesCopy, 54);
                
                f2LMoves[0]++;

                if (f2LMoves[0] > 11)
                {
                    f2LMoves[0] = 1;
                    f2LMoves[1]++;
                    if (f2LMoves[1] > 11)
                    {
                        f2LMoves[1] = 1;
                        f2LMoves[2]++;
                        if (f2LMoves[2] > 11)
                        {
                            f2LMoves[2] = 1;
                            f2LMoves[3]++;
                            if (f2LMoves[3] > 11)
                            {
                                f2LMoves[3] = 1;
                                f2LMoves[4]++;
                                if (f2LMoves[4] > 11)
                                {
                                    f2LMoves[4] = 1;
                                    f2LMoves[5]++;
                                    if (f2LMoves[5] > 11)
                                    {
                                        f2LMoves[5] = 1;
                                        f2LMoves[6]++;
                                    }
                                }
                            }
                        }
                    }
                }
                

                foreach (var item in f2LMoves)
                {
                    Console.Write(item + " ");
                }
                
                if(f2LMoves[5] > 0)
                {
                    allowedSpecialCase = true;
                    f2LMoves = new int[7];//clears move array
                    
                }
                Console.Write(allowedSpecialCase);
                
                TurnSequence2(f2LMoves, true);//apply moves to copy of cubes

                pairsInserted = 0;
                if (facesCopy[0, 0, 0] == "w" && facesCopy[2, 0, 2] == "r" && facesCopy[2, 1, 2] == "r" && facesCopy[4, 0, 0] == "b" && facesCopy[4, 1, 0] == "b" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 0, 2] == "w" && facesCopy[5, 0, 2] == "g" && facesCopy[5, 1, 2] == "g" && facesCopy[2, 0, 0] == "r" && facesCopy[2, 1, 0] == "r" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 2, 2] == "w" && facesCopy[3, 0, 2] == "o" && facesCopy[3, 1, 2] == "o" && facesCopy[5, 0, 0] == "g" && facesCopy[5, 1, 0] == "g" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 2, 0] == "w" && facesCopy[4, 0, 2] == "b" && facesCopy[4, 1, 2] == "b" && facesCopy[3, 0, 0] == "o" && facesCopy[3, 1, 0] == "o" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }

                if (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o")
                {
                    if (pairsInserted >= pairsCounted)
                    {
                        if (facesCopy[3, 2, 0] == "w" && facesCopy[4, 2, 1] == facesCopy[4, 2, 2] && facesCopy[1, 1, 2] == facesCopy[1, 2, 2])//corner and edge are paired next to each other, edge to right of corner, white on orange
                        {
                            if (facesCopy[4, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("r");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r'");
                            }
                            if (facesCopy[4, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g'");
                            }
                            if (facesCopy[4, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o'");
                            }
                            if (facesCopy[4, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b'");
                            }
                        }
                        if (facesCopy[5, 2, 0] == "w" && facesCopy[3, 2, 1] == facesCopy[3, 2, 2] && facesCopy[1, 2, 0] == facesCopy[1, 2, 1])//white on green
                        {
                            if (facesCopy[3, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r'");
                            }
                            if (facesCopy[3, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g'");
                            }
                            if (facesCopy[3, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o'");
                            }
                            if (facesCopy[3, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("b");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b'");
                            }
                        }
                        if (facesCopy[2, 2, 0] == "w" && facesCopy[5, 2, 1] == facesCopy[5, 2, 2] && facesCopy[1, 0, 0] == facesCopy[1, 1, 0])//white on red
                        {
                            if (facesCopy[5, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r'");
                            }
                            if (facesCopy[5, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g'");
                            }
                            if (facesCopy[5, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("o");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o'");
                            }
                            if (facesCopy[5, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b'");
                            }
                        }
                        if (facesCopy[4, 2, 0] == "w" && facesCopy[2, 2, 1] == facesCopy[2, 2, 2] && facesCopy[1, 0, 1] == facesCopy[1, 0, 2])//white on blue
                        {
                            if (facesCopy[2, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r'");
                            }
                            if (facesCopy[2, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("g");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g'");
                            }
                            if (facesCopy[2, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o'");
                            }
                            if (facesCopy[2, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b'");
                            }
                        }

                        if (facesCopy[4, 2, 2] == "w" && facesCopy[5, 2, 1] == facesCopy[5, 2, 0] && facesCopy[1, 1, 0] == facesCopy[1, 2, 0])//corner and edge are paired next to each other, edge to left of corner, white on orange
                        {
                            if (facesCopy[5, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("r'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r");
                            }
                            if (facesCopy[5, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g");
                            }
                            if (facesCopy[5, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o");
                            }
                            if (facesCopy[5, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                            }
                        }
                        if (facesCopy[5, 2, 2] == "w" && facesCopy[2, 2, 1] == facesCopy[2, 2, 0] && facesCopy[1, 0, 0] == facesCopy[1, 0, 1])//white on green
                        {
                            if (facesCopy[2, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r");
                            }
                            if (facesCopy[2, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g");
                            }
                            if (facesCopy[2, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o");
                            }
                            if (facesCopy[2, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("b'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                            }
                        }
                        if (facesCopy[2, 2, 2] == "w" && facesCopy[4, 2, 1] == facesCopy[4, 2, 0] && facesCopy[1, 0, 2] == facesCopy[1, 1, 2])//white on red
                        {
                            if (facesCopy[4, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r");
                            }
                            if (facesCopy[4, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g");
                            }
                            if (facesCopy[4, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("o'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o");
                            }
                            if (facesCopy[4, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                            }
                        }
                        if (facesCopy[4, 2, 2] == "w" && facesCopy[3, 2, 1] == facesCopy[3, 2, 0] && facesCopy[1, 1, 2] == facesCopy[1, 2, 2])//white on blue
                        {
                            if (facesCopy[3, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r");
                            }
                            if (facesCopy[3, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("g'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g");
                            }
                            if (facesCopy[3, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o");
                            }
                            if (facesCopy[3, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                            }
                        }

                        if (facesCopy[3, 2, 0] == "w" && facesCopy[5, 2, 1] == facesCopy[1, 2, 2] && facesCopy[1, 1, 0] == facesCopy[4, 2, 2])//corner and edge are seperate from each other, edge to left of corner, white on orange
                        {
                            if (facesCopy[5, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r");
                            }
                            if (facesCopy[5, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g");
                            }
                            if (facesCopy[5, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("o'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o");
                            }
                            if (facesCopy[5, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b");
                            }
                        }
                        if (facesCopy[4, 2, 0] == "w" && facesCopy[2, 2, 1] == facesCopy[1, 0, 2] && facesCopy[1, 0, 1] == facesCopy[3, 2, 2])//white on green
                        {
                            if (facesCopy[2, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r");
                            }
                            if (facesCopy[2, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("g'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g");
                            }
                            if (facesCopy[2, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o");
                            }
                            if (facesCopy[2, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b");
                            }
                        }
                        if (facesCopy[2, 2, 0] == "w" && facesCopy[4, 2, 1] == facesCopy[1, 0, 0] && facesCopy[1, 1, 2] == facesCopy[5, 2, 2])//white on red
                        {
                            if (facesCopy[4, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("r'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r");
                            }
                            if (facesCopy[4, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g");
                            }
                            if (facesCopy[4, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o");
                            }
                            if (facesCopy[4, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b");
                            }
                        }
                        if (facesCopy[4, 2, 0] == "w" && facesCopy[3, 2, 1] == facesCopy[2, 0, 2] && facesCopy[1, 2, 1] == facesCopy[2, 2, 2])//white on blue
                        {
                            if (facesCopy[3, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r");
                            }
                            if (facesCopy[3, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g");
                            }
                            if (facesCopy[3, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o");
                            }
                            if (facesCopy[3, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b");
                            }
                        }

                        if (facesCopy[3, 2, 2] == "w" && facesCopy[4, 2, 1] == facesCopy[1, 2, 0] && facesCopy[1, 1, 2] == facesCopy[5, 0, 2])//corner and edge are seperate from each other, edge to right of corner, white on orange
                        {
                            if (facesCopy[4, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r'");
                            }
                            if (facesCopy[4, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g");
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g'");
                            }
                            if (facesCopy[4, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("o");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o'");
                            }
                            if (facesCopy[4, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("b");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                            }
                        }
                        if (facesCopy[4, 2, 2] == "w" && facesCopy[3, 2, 1] == facesCopy[1, 0, 0] && facesCopy[1, 2, 1] == facesCopy[2, 0, 2])//white on green
                        {
                            if (facesCopy[3, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r'");
                            }
                            if (facesCopy[3, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("g");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g'");
                            }
                            if (facesCopy[3, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("o");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o'");
                            }
                            if (facesCopy[3, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                            }
                        }
                        if (facesCopy[2, 2, 2] == "w" && facesCopy[5, 2, 1] == facesCopy[1, 0, 2] && facesCopy[1, 1, 0] == facesCopy[4, 0, 2])//white on red
                        {
                            if (facesCopy[5, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("r");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r'");
                            }
                            if (facesCopy[5, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("g");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g'");
                            }
                            if (facesCopy[5, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o'");
                            }
                            if (facesCopy[5, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                            }
                        }
                        if (facesCopy[4, 2, 2] == "w" && facesCopy[2, 2, 1] == facesCopy[2, 2, 2] && facesCopy[1, 0, 1] == facesCopy[3, 0, 2])//white on blue
                        {
                            if (facesCopy[2, 2, 1] == "r")
                            {
                                f2LAdditionalMoves.Add("y'");
                                f2LAdditionalMoves.Add("r");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("r'");
                            }
                            if (facesCopy[2, 2, 1] == "g")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                TurnCopy("g");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("g'");
                            }
                            if (facesCopy[2, 2, 1] == "o")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("o'");
                            }
                            if (facesCopy[2, 2, 1] == "b")
                            {
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b");
                                f2LAdditionalMoves.Add("y");
                                f2LAdditionalMoves.Add("b'");
                            }
                        }
                    }
                }

                pairsInserted = 0;
                if (facesCopy[0, 0, 0] == "w" && facesCopy[2, 0, 2] == "r" && facesCopy[2, 1, 2] == "r" && facesCopy[4, 0, 0] == "b" && facesCopy[4, 1, 0] == "b" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 0, 2] == "w" && facesCopy[5, 0, 2] == "g" && facesCopy[5, 1, 2] == "g" && facesCopy[2, 0, 0] == "r" && facesCopy[2, 1, 0] == "r" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 2, 2] == "w" && facesCopy[3, 0, 2] == "o" && facesCopy[3, 1, 2] == "o" && facesCopy[5, 0, 0] == "g" && facesCopy[5, 1, 0] == "g" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 2, 0] == "w" && facesCopy[4, 0, 2] == "b" && facesCopy[4, 1, 2] == "b" && facesCopy[3, 0, 0] == "o" && facesCopy[3, 1, 0] == "o" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }

                if (allowedSpecialCase && pairsInserted >= pairsCounted)
                {
                    if ((facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))//special cases below. If it reaches them it changes it to a normal state
                    {//red/blue piece, these are the posibilities with blue up.
                        
                        if ((facesCopy[3, 2, 2] == "w" && facesCopy[3, 2, 1] == facesCopy[1, 2, 0] && facesCopy[1, 2, 1] == facesCopy[5, 2, 0]) || (facesCopy[5, 2, 2] == "w" && facesCopy[5, 2, 1] == facesCopy[1, 0, 0] && facesCopy[1, 1, 0] == facesCopy[2, 2, 0]) || (facesCopy[2, 2, 2] == "w" && facesCopy[2, 2, 1] == facesCopy[1, 0, 2] && facesCopy[1, 0, 1] == facesCopy[4, 2, 0]) || (facesCopy[4, 2, 2] == "w" && facesCopy[4, 2, 1] == facesCopy[1, 2, 2] && facesCopy[1, 1, 2] == facesCopy[3, 2, 0]))
                        {
                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int [7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[5, 2, 2] == "w" && facesCopy[5, 2, 1] == facesCopy[1, 0, 0] && facesCopy[1, 1, 0] == facesCopy[2, 2, 0])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 5;//r
                                f2LMoves[1] = 4;//y'
                                f2LMoves[2] = 6;//r'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[3, 2, 2] == "w" && facesCopy[3, 2, 1] == facesCopy[1, 2, 0] && facesCopy[1, 2, 1] == facesCopy[5, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 11;//g
                                f2LMoves[1] = 4;//y'
                                f2LMoves[2] = 1;//g'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[2, 2, 2] == "w" && facesCopy[2, 2, 1] == facesCopy[1, 0, 2] && facesCopy[1, 0, 1] == facesCopy[4, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 9;//b
                                f2LMoves[1] = 4;//y'
                                f2LMoves[2] = 10;//b'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[4, 2, 2] == "w" && facesCopy[4, 2, 1] == facesCopy[1, 2, 2] && facesCopy[1, 1, 2] == facesCopy[3, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 7;//o
                                f2LMoves[1] = 4;//y'
                                f2LMoves[2] = 8;//o'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }

                        if ((facesCopy[3, 2, 0] == "w" && facesCopy[3, 2, 1] == facesCopy[4, 2, 2] && facesCopy[1, 2, 1] == facesCopy[1, 2, 2]) || (facesCopy[5, 2, 0] == "w" && facesCopy[5, 2, 1] == facesCopy[3, 2, 2] && facesCopy[1, 1, 0] == facesCopy[1, 2, 0]) || (facesCopy[2, 2, 0] == "w" && facesCopy[2, 2, 1] == facesCopy[5, 2, 2] && facesCopy[1, 0, 1] == facesCopy[1, 0, 0]) || (facesCopy[4, 2, 0] == "w" && facesCopy[4, 2, 1] == facesCopy[2, 2, 2] && facesCopy[1, 1, 2] == facesCopy[1, 0, 2]))
                        {
                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int [7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[4, 2, 0] == "w" && facesCopy[4, 2, 1] == facesCopy[2, 2, 2] && facesCopy[1, 1, 2] == facesCopy[1, 0, 2])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 5;//r
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 6;//r'
                                f2LMoves[3] = 3;//y
                                f2LMoves[4] = 3;//r
                                f2LMoves[5] = 3;//y
                                f2LMoves[6] = 3;//r'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[2, 2, 0] == "w" && facesCopy[2, 2, 1] == facesCopy[5, 2, 2] && facesCopy[1, 0, 1] == facesCopy[1, 0, 0])))/////////////////////////////
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 11;//g
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 1;//g'
                                f2LMoves[3] = 3;//y
                                f2LMoves[4] = 11;//g
                                f2LMoves[5] = 3;//y
                                f2LMoves[6] = 1;//g'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[3, 2, 0] == "w" && facesCopy[3, 2, 1] == facesCopy[4, 2, 2] && facesCopy[1, 2, 1] == facesCopy[1, 2, 2])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 9;//b
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 10;//b'
                                f2LMoves[3] = 3;//y
                                f2LMoves[4] = 9;//b
                                f2LMoves[5] = 3;//y
                                f2LMoves[6] = 10;//b'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[5, 2, 0] == "w" && facesCopy[5, 2, 1] == facesCopy[3, 2, 2] && facesCopy[1, 1, 0] == facesCopy[1, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 7;//o
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 8;//o'
                                f2LMoves[3] = 3;//y
                                f2LMoves[4] = 7;//o
                                f2LMoves[5] = 3;//y
                                f2LMoves[6] = 8;//bo'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }

                        if ((facesCopy[3, 2, 2] == "w" && facesCopy[5, 2, 0] == facesCopy[1, 1, 0] && facesCopy[5, 2, 1] == facesCopy[1, 2, 0]) || (facesCopy[5, 2, 2] == "w" && facesCopy[2, 2, 0] == facesCopy[1, 0, 1] && facesCopy[2, 2, 1] == facesCopy[1, 0, 0]) || (facesCopy[2, 2, 2] == "w" && facesCopy[4, 2, 0] == facesCopy[1, 1, 2] && facesCopy[4, 2, 1] == facesCopy[1, 0, 2]) || (facesCopy[4, 2, 2] == "w" && facesCopy[3, 2, 0] == facesCopy[1, 2, 1] && facesCopy[3, 2, 1] == facesCopy[1, 2, 2]))
                        {
                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int [7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[4, 2, 2] == "w" && facesCopy[3, 2, 0] == facesCopy[1, 2, 1] && facesCopy[3, 2, 1] == facesCopy[1, 2, 2])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 10;//b'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 9;//b
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[2, 2, 2] == "w" && facesCopy[4, 2, 0] == facesCopy[1, 1, 2] && facesCopy[4, 2, 1] == facesCopy[1, 0, 2])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 6;//r'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 5;//r
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[3, 2, 2] == "w" && facesCopy[5, 2, 0] == facesCopy[1, 1, 0] && facesCopy[5, 2, 1] == facesCopy[1, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 8;//o'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 7;//o
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[5, 2, 2] == "w" && facesCopy[2, 2, 0] == facesCopy[1, 0, 1] && facesCopy[2, 2, 1] == facesCopy[1, 0, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 1;//g'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 11;//g
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }

                        if ((facesCopy[1, 2, 0] == "w" && facesCopy[1, 1, 0] == facesCopy[3, 2, 2] && facesCopy[5, 2, 0] == facesCopy[5, 2, 1]) || (facesCopy[1, 0, 0] == "w" && facesCopy[1, 0, 1] == facesCopy[5, 2, 2] && facesCopy[2, 2, 0] == facesCopy[2, 2, 1]) || (facesCopy[1, 0, 2] == "w" && facesCopy[1, 1, 2] == facesCopy[2, 2, 2] && facesCopy[4, 2, 0] == facesCopy[4, 2, 1]) || (facesCopy[1, 2, 2] == "w" && facesCopy[1, 2, 1] == facesCopy[4, 2, 2] && facesCopy[3, 2, 0] == facesCopy[3, 2, 1]))
                        {
                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int [7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[1, 0, 2] == "w" && facesCopy[1, 1, 2] == facesCopy[2, 2, 2] && facesCopy[4, 2, 0] == facesCopy[4, 2, 1])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 5;//r
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 6;//r'
                                f2LMoves[3] = 10;//b'
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 9;//b
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[1, 0, 0] == "w" && facesCopy[1, 0, 1] == facesCopy[5, 2, 2] && facesCopy[2, 2, 0] == facesCopy[2, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 11;//g
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 1;//g'
                                f2LMoves[3] = 6;//r'
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 5;//r
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[1, 2, 2] == "w" && facesCopy[1, 2, 1] == facesCopy[4, 2, 2] && facesCopy[3, 2, 0] == facesCopy[3, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 9;//b
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 10;//b'
                                f2LMoves[3] = 8;//o'
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 7;//o
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[1, 2, 0] == "w" && facesCopy[1, 1, 0] == facesCopy[3, 2, 2] && facesCopy[5, 2, 0] == facesCopy[5, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 7;//o
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 8;//o'
                                f2LMoves[3] = 1;//g'
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 11;//g
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }

                        if ((facesCopy[1, 2, 0] == "w" && facesCopy[3, 2, 2] == facesCopy[1, 2, 1] && facesCopy[5, 2, 0] == facesCopy[3, 2, 1]) || (facesCopy[1, 0, 0] == "w" && facesCopy[5, 2, 2] == facesCopy[1, 1, 0] && facesCopy[2, 2, 0] == facesCopy[5, 2, 1]) || (facesCopy[1, 0, 2] == "w" && facesCopy[2, 2, 2] == facesCopy[1, 0, 1] && facesCopy[4, 2, 0] == facesCopy[2, 2, 1]) || (facesCopy[1, 2, 2] == "w" && facesCopy[4, 2, 2] == facesCopy[1, 1, 2] && facesCopy[3, 2, 0] == facesCopy[4, 2, 1]))
                        {
                            Console.Write(facesCopy[1, 0, 2]);
                            Console.Write(facesCopy[2, 2, 2]);
                            Console.Write(facesCopy[1, 0, 1]);
                            Console.Write(facesCopy[4, 2, 0]);
                            Console.Write(facesCopy[2, 2, 1]);

                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int [7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[1, 0, 2] == "w" && facesCopy[2, 2, 2] == facesCopy[1, 0, 1] && facesCopy[4, 2, 0] == facesCopy[2, 2, 1])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 10;//b'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 9;//b
                                f2LMoves[3] = 5;//r
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 6;//r'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[1, 0, 0] == "w" && facesCopy[5, 2, 2] == facesCopy[1, 1, 0] && facesCopy[2, 2, 0] == facesCopy[5, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 6;//r'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 5;//r
                                f2LMoves[3] = 11;//g
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 1;//g'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[1, 2, 2] == "w" && facesCopy[4, 2, 2] == facesCopy[1, 1, 2] && facesCopy[3, 2, 0] == facesCopy[4, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 8;//o'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 7;//o
                                f2LMoves[3] = 9;//b
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 10;//b'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[1, 2, 0] == "w" && facesCopy[3, 2, 2] == facesCopy[1, 2, 1] && facesCopy[5, 2, 0] == facesCopy[3, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    PrintCubeState();
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int [7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 1;//g'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 11;//g
                                f2LMoves[3] = 7;//o
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 8;//o'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int [7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }
                    }
                }
                

                TurnSequence(f2LAdditionalMoves, true);
                

                pairsInserted = 0;
                if (facesCopy[0, 0, 0] == "w" && facesCopy[2, 0, 2] == "r" && facesCopy[2, 1, 2] == "r" && facesCopy[4, 0, 0] == "b" && facesCopy[4, 1, 0] == "b" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 0, 2] == "w" && facesCopy[5, 0, 2] == "g" && facesCopy[5, 1, 2] == "g" && facesCopy[2, 0, 0] == "r" && facesCopy[2, 1, 0] == "r" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 2, 2] == "w" && facesCopy[3, 0, 2] == "o" && facesCopy[3, 1, 2] == "o" && facesCopy[5, 0, 0] == "g" && facesCopy[5, 1, 0] == "g" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }
                if (facesCopy[0, 2, 0] == "w" && facesCopy[4, 0, 2] == "b" && facesCopy[4, 1, 2] == "b" && facesCopy[3, 0, 0] == "o" && facesCopy[3, 1, 0] == "o" && (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))
                {
                    pairsInserted++;
                }

                if(pairsInserted > pairsCounted)
                {
                    TurnSequence2(f2LMoves, false);
                    TurnSequence(f2LAdditionalMoves, false);
                    pairsCounted = pairsInserted;
                    f2LMoves = new int [7];//clears move array
                    allowedSpecialCase = false;
                }
                Console.Write("  " + pairsInserted + pairsCounted);
                Console.Write("\n");

                Console.Write("\n\n");
                /*
                for (int x = 0; x < 6; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            Console.Write(facesCopy[x, y, z]);
                            if (z == 2)
                                Console.Write("\n");
                            if (y == 2 && z == 2)
                                Console.Write("\n");
                            //Console.Write("[{0}, {1}, {2}]: {3}\n", x, y, z, faces[x, y, z]);
                        }
                    }
                }
                */
            }
        }
        static void TurnSequence(int[] moves, bool isCopy)
        {
            if (isCopy)
            {
                foreach (var move in moves)
                {
                    if (move == 1)
                        TurnCopy("w");
                    else if (move == 2)
                        TurnCopy("w'");
                    else if (move == 3)
                        TurnCopy("y");
                    else if (move == 4)
                        TurnCopy("y'");
                    else if (move == 5)
                        TurnCopy("r");
                    else if (move == 6)
                        TurnCopy("r'");
                    else if (move == 7)
                        TurnCopy("o");
                    else if (move == 8)
                        TurnCopy("o'");
                    else if (move == 9)
                        TurnCopy("b");
                    else if (move == 10)
                        TurnCopy("b'");
                    else if (move == 11)
                        TurnCopy("g");
                    else if (move == 12)
                        TurnCopy("g'");
                    else if (move == 13)
                    {
                        TurnCopy("w");
                        TurnCopy("w");
                    }
                    else if (move == 14)
                    {
                        TurnCopy("y");
                        TurnCopy("y");
                    }
                    else if (move == 15)
                    {
                        TurnCopy("r");
                        TurnCopy("r");
                    }
                    else if (move == 16)
                    {
                        TurnCopy("o");
                        TurnCopy("o");
                    }
                    else if (move == 17)
                    {
                        TurnCopy("b");
                        TurnCopy("b");
                    }
                    else if (move == 18)
                    {
                        TurnCopy("g");
                        TurnCopy("g");
                    }
                }
            }
            else
            {
                foreach (var move in moves)
                {
                    if (move == 1)
                        Turn("w");
                    else if (move == 2)
                        Turn("w'");
                    else if (move == 3)
                        Turn("y");
                    else if (move == 4)
                        Turn("y'");
                    else if (move == 5)
                        Turn("r");
                    else if (move == 6)
                        Turn("r'");
                    else if (move == 7)
                        Turn("o");
                    else if (move == 8)
                        Turn("o'");
                    else if (move == 9)
                        Turn("b");
                    else if (move == 10)
                        Turn("b'");
                    else if (move == 11)
                        Turn("g");
                    else if (move == 12)
                        Turn("g'");
                    else if (move == 13)
                    {
                        Turn("w");
                        Turn("w");
                    }
                    else if (move == 14)
                    {
                        Turn("y");
                        Turn("y");
                    }
                    else if (move == 15)
                    {
                        Turn("r");
                        Turn("r");
                    }
                    else if (move == 16)
                    {
                        Turn("o");
                        Turn("o");
                    }
                    else if (move == 17)
                    {
                        Turn("b");
                        Turn("b");
                    }
                    else if (move == 18)
                    {
                        Turn("g");
                        Turn("g");
                    }
                }
            }
        }
        static void TurnSequence2(int[] moves, bool isCopy)
        {
            if (isCopy)
            {
                foreach (var move in moves)
                {
                    if (move == 1)
                        TurnCopy("g'");
                    else if (move == 2)
                    {
                        TurnCopy("y");
                        TurnCopy("y");
                    }
                    else if (move == 3)
                        TurnCopy("y");
                    else if (move == 4)
                        TurnCopy("y'");
                    else if (move == 5)
                        TurnCopy("r");
                    else if (move == 6)
                        TurnCopy("r'");
                    else if (move == 7)
                        TurnCopy("o");
                    else if (move == 8)
                        TurnCopy("o'");
                    else if (move == 9)
                        TurnCopy("b");
                    else if (move == 10)
                        TurnCopy("b'");
                    else if (move == 11)
                        TurnCopy("g");
                }
            }
            else
            {
                foreach (var move in moves)
                {
                    if (move == 1)
                        Turn("g'");
                    else if (move == 2)
                    {
                        Turn("y");
                        Turn("y");
                    }
                    else if (move == 3)
                        Turn("y");
                    else if (move == 4)
                        Turn("y'");
                    else if (move == 5)
                        Turn("r");
                    else if (move == 6)
                        Turn("r'");
                    else if (move == 7)
                        Turn("o");
                    else if (move == 8)
                        Turn("o'");
                    else if (move == 9)
                        Turn("b");
                    else if (move == 10)
                        Turn("b'");
                    else if (move == 11)
                        Turn("g");
                }
            }
        }
        static void TurnSequence(List<string> moves, bool isCopy)
        {
            if (isCopy)
            {
                foreach (var move in moves)
                {
                    TurnCopy(move);
                }
            }
            else
            {
                foreach (var move in moves)
                {
                    Turn(move);
                }
            }
        }
        static void PrintCubeState()
        {
            Console.Write("\n\n");

            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        Console.Write(faces[x, y, z]);
                        if (z == 2)
                            Console.Write("\n");
                        if (y == 2 && z == 2)
                            Console.Write("\n");
                        //Console.Write("[{0}, {1}, {2}]: {3}\n", x, y, z, faces[x, y, z]);
                    }
                }
            }
            Console.Write("Moves: ");
            foreach (var move in moves)
            {
                Console.Write(move + " ");

            }
        }
        static void OLLEdges()
        {
            if (faces[1, 0, 1] != "y" && faces[1, 1, 2] == "y" && faces[1, 2, 1] != "y" && faces[1, 1, 0] == "y")//horizontal yellow line
            {
                Turn("o");
                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y'");
                Turn("o'");
            }
            else if (faces[1, 0, 1] == "y" && faces[1, 1, 2] != "y" && faces[1, 2, 1] == "y" && faces[1, 1, 0] != "y")//vertical yellow line
            {
                Turn("y");
                Turn("o");
                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y'");
                Turn("o'");
            }
            else if ((faces[1, 0, 1] != "y" && faces[1, 1, 2] == "y" && faces[1, 2, 1] == "y" && faces[1, 1, 0] != "y") || (faces[1, 0, 1] != "y" && faces[1, 1, 2] != "y" && faces[1, 2, 1] == "y" && faces[1, 1, 0] == "y") || (faces[1, 0, 1] == "y" && faces[1, 1, 2] != "y" && faces[1, 2, 1] != "y" && faces[1, 1, 0] == "y") || (faces[1, 0, 1] == "y" && faces[1, 1, 2] == "y" && faces[1, 2, 1] != "y" && faces[1, 1, 0] != "y"))//vertical yellow line
            {
                while (!(faces[1, 0, 1] != "y" && faces[1, 1, 2] == "y" && faces[1, 2, 1] == "y" && faces[1, 1, 0] != "y"))
                {
                    Turn("y");
                }
                Turn("r");
                Turn("y");
                Turn("g");
                Turn("y'");
                Turn("g'");
                Turn("r'");
            }
            else if (faces[1, 0, 1] != "y" && faces[1, 1, 2] != "y" && faces[1, 2, 1] != "y" && faces[1, 1, 0] != "y")//yellow dot (no edges)
            {
                Turn("o");
                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y'");
                Turn("o'");

                Turn("r");
                Turn("y");
                Turn("g");
                Turn("y'");
                Turn("g'");
                Turn("r'");
            }
        }
        static void OLLCorners()
        {
            if ((faces[1, 2, 0] == "y" && faces[3, 2, 0] == "y" && faces[4, 2, 0] == "y" && faces[2, 2, 0] == "y") || (faces[1, 0, 0] == "y" && faces[3, 2, 0] == "y" && faces[5, 2, 0] == "y" && faces[4, 2, 0] == "y") || (faces[1, 0, 2] == "y" && faces[3, 2, 0] == "y" && faces[2, 2, 0] == "y" && faces[5, 2, 0] == "y") || (faces[1, 2, 2] == "y" && faces[4, 2, 0] == "y" && faces[5, 2, 0] == "y" && faces[2, 2, 0] == "y"))//Sune
            {
                while (!(faces[1, 2, 0] == "y" && faces[3, 2, 0] == "y" && faces[4, 2, 0] == "y" && faces[2, 2, 0] == "y"))
                {
                    Turn("y");
                }
                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y");
                Turn("b");
                Turn("y");
                Turn("y");
                Turn("b'");
            }
            else if ((faces[1, 2, 2] == "y" && faces[3, 2, 2] == "y" && faces[5, 2, 2] == "y" && faces[2, 2, 2] == "y") || (faces[1, 2, 0] == "y" && faces[2, 2, 2] == "y" && faces[5, 2, 2] == "y" && faces[4, 2, 2] == "y") || (faces[1, 0, 0] == "y" && faces[3, 2, 2] == "y" && faces[2, 2, 2] == "y" && faces[4, 2, 2] == "y") || (faces[1, 0, 2] == "y" && faces[4, 2, 2] == "y" && faces[5, 2, 2] == "y" && faces[3, 2, 2] == "y"))//Antisune
            {
                while (!(faces[1, 2, 2] == "y" && faces[3, 2, 2] == "y" && faces[5, 2, 2] == "y" && faces[2, 2, 2] == "y"))
                {
                    Turn("y");
                }
                Turn("g'");
                Turn("y'");
                Turn("g");
                Turn("y'");
                Turn("g'");
                Turn("y");
                Turn("y");
                Turn("g");
            }
            else if ((faces[3, 2, 2] == "y" && faces[3, 2, 0] == "y" && faces[2, 2, 2] == "y" && faces[2, 2, 0] == "y") || (faces[4, 2, 2] == "y" && faces[4, 2, 0] == "y" && faces[5, 2, 2] == "y" && faces[5, 2, 0] == "y"))//"H"
            {
                while (!(faces[3, 2, 2] == "y" && faces[3, 2, 0] == "y" && faces[2, 2, 2] == "y" && faces[2, 2, 0] == "y"))
                {
                    Turn("y");
                }
                Turn("o");

                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y'");

                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y'");

                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y'");

                Turn("o'");
            }
            else if ((faces[5, 2, 2] == "y" && faces[5, 2, 0] == "y" && faces[2, 2, 2] == "y" && faces[3, 2, 0] == "y") || (faces[2, 2, 2] == "y" && faces[2, 2, 0] == "y" && faces[4, 2, 2] == "y" && faces[5, 2, 0] == "y") || (faces[4, 2, 2] == "y" && faces[4, 2, 0] == "y" && faces[3, 2, 2] == "y" && faces[2, 2, 0] == "y") || (faces[3, 2, 2] == "y" && faces[3, 2, 0] == "y" && faces[5, 2, 2] == "y" && faces[4, 2, 0] == "y"))//"Pi"
            {
                while (!(faces[5, 2, 2] == "y" && faces[5, 2, 0] == "y" && faces[2, 2, 2] == "y" && faces[3, 2, 0] == "y"))
                {
                    Turn("y");
                }
                Turn("b");
                Turn("y");
                Turn("y");
                Turn("b");
                Turn("b");
                Turn("y'");
                Turn("b");
                Turn("b");
                Turn("y'");
                Turn("b");
                Turn("b");
                Turn("y");
                Turn("y");
                Turn("b");
            }
            else if ((faces[1, 0, 0] == "y" && faces[1, 2, 2] == "y" && faces[3, 2, 2] == "y" && faces[4, 2, 0] == "y") || (faces[1, 0, 2] == "y" && faces[1, 2, 0] == "y" && faces[5, 2, 2] == "y" && faces[3, 2, 0] == "y") || (faces[1, 2, 2] == "y" && faces[1, 0, 0] == "y" && faces[2, 2, 2] == "y" && faces[5, 2, 0] == "y") || (faces[1, 0, 2] == "y" && faces[1, 2, 0] == "y" && faces[4, 2, 2] == "y" && faces[2, 2, 0] == "y"))//"L"
            {
                ;
                while (!(faces[1, 0, 0] == "y" && faces[1, 2, 2] == "y" && faces[3, 2, 2] == "y" && faces[4, 2, 0] == "y"))
                {
                    Turn("y");
                }
                Turn("b'");
                Turn("o");
                Turn("b");
                Turn("r'");
                Turn("b'");
                Turn("o'");
                Turn("b");
                Turn("r");
            }
            else if ((faces[1, 0, 2] == "y" && faces[1, 2, 2] == "y" && faces[3, 2, 2] == "y" && faces[2, 2, 0] == "y") || (faces[1, 2, 2] == "y" && faces[1, 2, 0] == "y" && faces[5, 2, 2] == "y" && faces[4, 2, 0] == "y") || (faces[1, 2, 0] == "y" && faces[1, 0, 0] == "y" && faces[2, 2, 2] == "y" && faces[3, 2, 0] == "y") || (faces[1, 0, 2] == "y" && faces[1, 0, 0] == "y" && faces[4, 2, 2] == "y" && faces[5, 2, 0] == "y"))//"T"
            {
                while (!(faces[1, 0, 2] == "y" && faces[1, 2, 2] == "y" && faces[3, 2, 2] == "y" && faces[2, 2, 0] == "y"))
                {
                    Turn("y");
                }
                Turn("g");
                Turn("o");
                Turn("b'");
                Turn("o'");
                Turn("g'");
                Turn("o");
                Turn("b");
                Turn("o'");
            }
            else if ((faces[1, 0, 0] == "y" && faces[1, 0, 2] == "y" && faces[3, 2, 2] == "y" && faces[3, 2, 0] == "y") || (faces[1, 2, 2] == "y" && faces[1, 0, 2] == "y" && faces[5, 2, 2] == "y" && faces[5, 2, 0] == "y") || (faces[1, 2, 0] == "y" && faces[1, 2, 2] == "y" && faces[2, 2, 2] == "y" && faces[2, 2, 0] == "y") || (faces[1, 0, 0] == "y" && faces[1, 2, 0] == "y" && faces[4, 2, 2] == "y" && faces[4, 2, 0] == "y"))//"U"
            {
                while (!(faces[1, 0, 0] == "y" && faces[1, 0, 2] == "y" && faces[3, 2, 2] == "y" && faces[3, 2, 0] == "y"))
                {
                    Turn("y");
                }
                Turn("b");
                Turn("b");
                Turn("w");
                Turn("b'");
                Turn("y");
                Turn("y");
                Turn("b");
                Turn("w'");
                Turn("b'");
                Turn("y");
                Turn("y");
                Turn("b'");
            }

        }
        static void PLLCorners()
        {
            if (faces[2, 2, 2] != faces[2, 2, 0] && faces[3, 2, 2] != faces[3, 2, 0] && faces[4, 2, 2] != faces[4, 2, 0] && faces[5, 2, 2] != faces[5, 2, 0])//swap diagnal corners
            {
                Turn("o");
                Turn("b");
                Turn("y'");
                Turn("b'");
                Turn("y'");
                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("o'");

                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y'");
                Turn("b'");
                Turn("o");
                Turn("b");
                Turn("o'");
            }
            if ((faces[2, 2, 2] == faces[2, 2, 0] && faces[3, 2, 2] != faces[3, 2, 0] && faces[4, 2, 2] != faces[4, 2, 0] && faces[5, 2, 2] != faces[5, 2, 0]) || (faces[2, 2, 2] != faces[2, 2, 0] && faces[3, 2, 2] == faces[3, 2, 0] && faces[4, 2, 2] != faces[4, 2, 0] && faces[5, 2, 2] != faces[5, 2, 0]) || (faces[2, 2, 2] != faces[2, 2, 0] && faces[3, 2, 2] != faces[3, 2, 0] && faces[4, 2, 2] == faces[4, 2, 0] && faces[5, 2, 2] != faces[5, 2, 0]) || (faces[2, 2, 2] != faces[2, 2, 0] && faces[3, 2, 2] != faces[3, 2, 0] && faces[4, 2, 2] != faces[4, 2, 0] && faces[5, 2, 2] == faces[5, 2, 0]))//swap adjacent corners
            {
                while (faces[5, 2, 2] != faces[5, 2, 0])
                {
                    Turn("y");
                }

                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("y'");
                Turn("b'");
                Turn("o");
                Turn("b");

                Turn("b");
                Turn("y'");
                Turn("b'");
                Turn("y'");
                Turn("b");
                Turn("y");
                Turn("b'");
                Turn("o'");

            }
        }
        static void PLLEdges()
        {
            bool isSolved = false;
            while (!isSolved)
            {
                if (faces[3, 2, 1] == faces[4, 2, 2] && faces[4, 2, 1] == faces[5, 2, 2] && faces[5, 2, 1] == faces[3, 2, 2])//3 edge cycle right
                {
                    Turn("b");
                    Turn("y'");
                    Turn("b");
                    Turn("y");
                    Turn("b");
                    Turn("y");
                    Turn("b");
                    Turn("y'");
                    Turn("b'");
                    Turn("y'");
                    Turn("b");
                    Turn("b");
                }
                else if (faces[3, 2, 1] == faces[5, 2, 2] && faces[4, 2, 1] == faces[3, 2, 2] && faces[5, 2, 1] == faces[4, 2, 2])//3 edge cycle left
                {
                    Turn("b");
                    Turn("b");
                    Turn("y");
                    Turn("b");
                    Turn("y");
                    Turn("b'");
                    Turn("y'");
                    Turn("b'");
                    Turn("y'");
                    Turn("b'");
                    Turn("y");
                    Turn("b'");
                }
                else if (faces[3, 2, 1] == faces[2, 2, 2] && faces[2, 2, 1] == faces[3, 2, 2])//Opposite edge swap
                {
                    Turn("b");
                    Turn("b");
                    Turn("g");
                    Turn("g");

                    Turn("w'");

                    Turn("b");
                    Turn("b");
                    Turn("g");
                    Turn("g");

                    Turn("y");
                    Turn("y");

                    Turn("b");
                    Turn("b");
                    Turn("g");
                    Turn("g");

                    Turn("w'");

                    Turn("b");
                    Turn("b");
                    Turn("g");
                    Turn("g");
                }
                else if (faces[3, 2, 1] == faces[4, 2, 2] && faces[4, 2, 1] == faces[3, 2, 2])//adjacent edge swap
                {
                    Turn("b'");
                    Turn("g");

                    Turn("o'");

                    Turn("b");
                    Turn("b");
                    Turn("g");
                    Turn("g");

                    Turn("r'");

                    Turn("b");
                    Turn("b");
                    Turn("g");
                    Turn("g");

                    Turn("o'");

                    Turn("b'");
                    Turn("g");

                    Turn("w");
                    Turn("w");

                    Turn("b");
                    Turn("b");
                    Turn("g");
                    Turn("g");
                }
                if (faces[3, 2, 1] == faces[3, 2, 2] && faces[2, 2, 2] == faces[2, 2, 1])
                {
                    isSolved = true;
                }
                if (!isSolved)
                {
                    Turn("y");
                }
            }
        }
        static void TurnLastLayer()
        {
            while (faces[3, 2, 1] != "o")
            {
                Turn("y");
            }
        }
        static void Turn(string side)
        {

            if (side == "w")
            {
                ChangeFaces(0, 2, 4, 3, 5, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, false);
            }
            else if (side == "w'")
            {
                ChangeFaces(0, 5, 3, 4, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, true);
            }
            else if (side == "y")
            {
                ChangeFaces(1, 5, 3, 4, 2, 2, 0, 2, 0, 2, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 2, 1, false);
            }
            else if (side == "y'")
            {
                ChangeFaces(1, 2, 4, 3, 5, 2, 0, 2, 0, 2, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 2, 1, true);
            }
            else if (side == "r")
            {
                ChangeFaces(2, 0, 5, 1, 4, 0, 2, 2, 2, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 0, 1, 1, 2, 0, 1, 1, 0, false);
            }
            else if (side == "r'")
            {
                ChangeFaces(2, 4, 1, 5, 0, 0, 0, 0, 2, 2, 2, 0, 2, 2, 0, 0, 0, 0, 2, 0, 0, 1, 0, 0, 1, 1, 2, 0, 1, true);
            }
            else if (side == "o")
            {
                ChangeFaces(3, 4, 1, 5, 0, 2, 2, 2, 0, 0, 0, 2, 0, 0, 2, 2, 2, 2, 0, 2, 2, 1, 2, 2, 1, 1, 0, 2, 1, false);
            }
            else if (side == "o'")
            {
                ChangeFaces(3, 0, 5, 1, 4, 2, 0, 0, 0, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 0, 2, 2, 1, 1, 0, 2, 1, 1, 2, true);
            }
            else if (side == "b")
            {
                ChangeFaces(4, 0, 2, 1, 3, 0, 0, 2, 2, 2, 2, 0, 0, 2, 0, 0, 2, 0, 2, 2, 0, 1, 0, 1, 2, 1, 2, 1, 0, false);
            }
            else if (side == "b'")
            {
                ChangeFaces(4, 3, 1, 2, 0, 0, 0, 2, 2, 2, 2, 0, 0, 2, 0, 0, 2, 0, 2, 2, 0, 1, 0, 1, 2, 1, 2, 1, 0, true);
            }
            else if (side == "g")
            {
                ChangeFaces(5, 3, 1, 2, 0, 2, 2, 0, 0, 0, 0, 2, 2, 0, 2, 2, 0, 2, 0, 0, 2, 1, 2, 1, 0, 1, 0, 1, 2, false);
            }
            else if (side == "g'")
            {
                ChangeFaces(5, 0, 2, 1, 3, 2, 2, 0, 0, 0, 0, 2, 2, 0, 2, 2, 0, 2, 0, 0, 2, 1, 2, 1, 0, 1, 0, 1, 2, true);
            }
            moves.Add(side);
        }
        static void ChangeFaces(int faceNum, int num1, int num2, int num3, int num4, int num5, int num6, int num7, int num8, int num9, int num10, int num11, int num12, int num13, int num14, int num15, int num16, int num17, int num18, int num19, int num20, int num21, int num22, int num23, int num24, int num25, int num26, int num27, int num28, bool isPrime)
        {
            string face = faces[faceNum, 0, 0]; //Moving corners on the face
            if (!isPrime)
            {
                faces[faceNum, 0, 0] = faces[faceNum, 2, 0];
                faces[faceNum, 2, 0] = faces[faceNum, 2, 2];
                faces[faceNum, 2, 2] = faces[faceNum, 0, 2];
                faces[faceNum, 0, 2] = face;

                face = faces[faceNum, 0, 1];//Moving edges of the face
                faces[faceNum, 0, 1] = faces[faceNum, 1, 0];
                faces[faceNum, 1, 0] = faces[faceNum, 2, 1];
                faces[faceNum, 2, 1] = faces[faceNum, 1, 2];
                faces[faceNum, 1, 2] = face;
            }
            else
            {
                faces[faceNum, 0, 0] = faces[faceNum, 0, 2];
                faces[faceNum, 0, 2] = faces[faceNum, 2, 2];
                faces[faceNum, 2, 2] = faces[faceNum, 2, 0];
                faces[faceNum, 2, 0] = face;

                face = faces[faceNum, 0, 1];//Moving edges of the face
                faces[faceNum, 0, 1] = faces[faceNum, 1, 2];
                faces[faceNum, 1, 2] = faces[faceNum, 2, 1];
                faces[faceNum, 2, 1] = faces[faceNum, 1, 0];
                faces[faceNum, 1, 0] = face;
            }

            face = faces[num1, num5, num6];//Moving right corners of other faces
            faces[num1, num5, num6] = faces[num2, num7, num8];
            faces[num2, num7, num8] = faces[num3, num9, num10];
            faces[num3, num9, num10] = faces[num4, num11, num12];
            faces[num4, num11, num12] = face;

            face = faces[num1, num13, num14];//Moving left corners of other faces
            faces[num1, num13, num14] = faces[num2, num15, num16];
            faces[num2, num15, num16] = faces[num3, num17, num18];
            faces[num3, num17, num18] = faces[num4, num19, num20];
            faces[num4, num19, num20] = face;

            face = faces[num1, num21, num22];//Moving edges of other faces
            faces[num1, num21, num22] = faces[num2, num23, num24];
            faces[num2, num23, num24] = faces[num3, num25, num26];
            faces[num3, num25, num26] = faces[num4, num27, num28];
            faces[num4, num27, num28] = face;
        }
        static void TurnCopy(string side)
        {
            if (side == "w")
            {
                ChangeFacesCopy(0, 2, 4, 3, 5, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, false);
            }
            else if (side == "w'")
            {
                ChangeFacesCopy(0, 5, 3, 4, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, true);
            }
            else if (side == "y")
            {
                ChangeFacesCopy(1, 5, 3, 4, 2, 2, 0, 2, 0, 2, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 2, 1, false);
            }
            else if (side == "y'")
            {
                ChangeFacesCopy(1, 2, 4, 3, 5, 2, 0, 2, 0, 2, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 2, 1, true);
            }
            else if (side == "r")
            {
                ChangeFacesCopy(2, 0, 5, 1, 4, 0, 2, 2, 2, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 0, 1, 1, 2, 0, 1, 1, 0, false);
            }
            else if (side == "r'")
            {
                ChangeFacesCopy(2, 4, 1, 5, 0, 0, 0, 0, 2, 2, 2, 0, 2, 2, 0, 0, 0, 0, 2, 0, 0, 1, 0, 0, 1, 1, 2, 0, 1, true);
            }
            else if (side == "o")
            {
                ChangeFacesCopy(3, 4, 1, 5, 0, 2, 2, 2, 0, 0, 0, 2, 0, 0, 2, 2, 2, 2, 0, 2, 2, 1, 2, 2, 1, 1, 0, 2, 1, false);
            }
            else if (side == "o'")
            {
                ChangeFacesCopy(3, 0, 5, 1, 4, 2, 0, 0, 0, 2, 0, 2, 2, 2, 2, 2, 0, 2, 2, 0, 2, 2, 1, 1, 0, 2, 1, 1, 2, true);
            }
            else if (side == "b")
            {
                ChangeFacesCopy(4, 0, 2, 1, 3, 0, 0, 2, 2, 2, 2, 0, 0, 2, 0, 0, 2, 0, 2, 2, 0, 1, 0, 1, 2, 1, 2, 1, 0, false);
            }
            else if (side == "b'")
            {
                ChangeFacesCopy(4, 3, 1, 2, 0, 0, 0, 2, 2, 2, 2, 0, 0, 2, 0, 0, 2, 0, 2, 2, 0, 1, 0, 1, 2, 1, 2, 1, 0, true);
            }
            else if (side == "g")
            {
                ChangeFacesCopy(5, 3, 1, 2, 0, 2, 2, 0, 0, 0, 0, 2, 2, 0, 2, 2, 0, 2, 0, 0, 2, 1, 2, 1, 0, 1, 0, 1, 2, false);
            }
            else if (side == "g'")
            {
                ChangeFacesCopy(5, 0, 2, 1, 3, 2, 2, 0, 0, 0, 0, 2, 2, 0, 2, 2, 0, 2, 0, 0, 2, 1, 2, 1, 0, 1, 0, 1, 2, true);
            }
        }
        static void ChangeFacesCopy(int faceNum, int num1, int num2, int num3, int num4, int num5, int num6, int num7, int num8, int num9, int num10, int num11, int num12, int num13, int num14, int num15, int num16, int num17, int num18, int num19, int num20, int num21, int num22, int num23, int num24, int num25, int num26, int num27, int num28, bool isPrime)
        {
            string face = facesCopy[faceNum, 0, 0]; //Moving corners on the face
            if (!isPrime)
            {
                facesCopy[faceNum, 0, 0] = facesCopy[faceNum, 2, 0];
                facesCopy[faceNum, 2, 0] = facesCopy[faceNum, 2, 2];
                facesCopy[faceNum, 2, 2] = facesCopy[faceNum, 0, 2];
                facesCopy[faceNum, 0, 2] = face;

                face = facesCopy[faceNum, 0, 1];//Moving edges of the face
                facesCopy[faceNum, 0, 1] = facesCopy[faceNum, 1, 0];
                facesCopy[faceNum, 1, 0] = facesCopy[faceNum, 2, 1];
                facesCopy[faceNum, 2, 1] = facesCopy[faceNum, 1, 2];
                facesCopy[faceNum, 1, 2] = face;
            }
            else
            {
                facesCopy[faceNum, 0, 0] = facesCopy[faceNum, 0, 2];
                facesCopy[faceNum, 0, 2] = facesCopy[faceNum, 2, 2];
                facesCopy[faceNum, 2, 2] = facesCopy[faceNum, 2, 0];
                facesCopy[faceNum, 2, 0] = face;

                face = facesCopy[faceNum, 0, 1];//Moving edges of the face
                facesCopy[faceNum, 0, 1] = facesCopy[faceNum, 1, 2];
                facesCopy[faceNum, 1, 2] = facesCopy[faceNum, 2, 1];
                facesCopy[faceNum, 2, 1] = facesCopy[faceNum, 1, 0];
                facesCopy[faceNum, 1, 0] = face;
            }

            face = facesCopy[num1, num5, num6];//Moving right corners of other faces
            facesCopy[num1, num5, num6] = facesCopy[num2, num7, num8];
            facesCopy[num2, num7, num8] = facesCopy[num3, num9, num10];
            facesCopy[num3, num9, num10] = facesCopy[num4, num11, num12];
            facesCopy[num4, num11, num12] = face;

            face = facesCopy[num1, num13, num14];//Moving left corners of other faces
            facesCopy[num1, num13, num14] = facesCopy[num2, num15, num16];
            facesCopy[num2, num15, num16] = facesCopy[num3, num17, num18];
            facesCopy[num3, num17, num18] = facesCopy[num4, num19, num20];
            facesCopy[num4, num19, num20] = face;

            face = facesCopy[num1, num21, num22];//Moving edges of other faces
            facesCopy[num1, num21, num22] = facesCopy[num2, num23, num24];
            facesCopy[num2, num23, num24] = facesCopy[num3, num25, num26];
            facesCopy[num3, num25, num26] = facesCopy[num4, num27, num28];
            facesCopy[num4, num27, num28] = face;
        }
    }
}
