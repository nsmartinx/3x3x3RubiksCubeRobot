using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RubiksCubeV3
{
    internal class Program
    {
        
        static List<string> solution = new List<string>();
        static char[,,] faces = new char[6, 3, 3];
        static Piece[] cubeCopy1 = new Piece[20];

        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                cubeCopy1[i] = new Piece(0, 0, 'z', 'z', 'z');
            }
            
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Console.Write("[{0}, {1}. {2}]: ", i, k, j);
                        faces[i, k, j] = Console.ReadLine()[0];
                    }
                }
            }
           
            /*
            faces[0, 0, 0] = 'r';
            faces[0, 1, 0] = 'w';
            faces[0, 2, 0] = 'o';
            faces[0, 0, 1] = 'w';
            faces[0, 1, 1] = 'w';
            faces[0, 2, 1] = 'y';
            faces[0, 0, 2] = 'o';
            faces[0, 1, 2] = 'g';
            faces[0, 2, 2] = 'b';
                             
            faces[1, 0, 0] = 'r';
            faces[1, 1, 0] = 'r';
            faces[1, 2, 0] = 'r';
            faces[1, 0, 1] = 'w';
            faces[1, 1, 1] = 'y';
            faces[1, 2, 1] = 'r';
            faces[1, 0, 2] = 'g';
            faces[1, 1, 2] = 'g';
            faces[1, 2, 2] = 'o';
                             
            faces[2, 0, 0] = 'w';
            faces[2, 1, 0] = 'y';
            faces[2, 2, 0] = 'y';
            faces[2, 0, 1] = 'o';
            faces[2, 1, 1] = 'r';
            faces[2, 2, 1] = 'y';
            faces[2, 0, 2] = 'w';
            faces[2, 1, 2] = 'o';
            faces[2, 2, 2] = 'w';
                             
            faces[3, 0, 0] = 'g';
            faces[3, 1, 0] = 'g';
            faces[3, 2, 0] = 'y';
            faces[3, 0, 1] = 'b';
            faces[3, 1, 1] = 'o';
            faces[3, 2, 1] = 'o';
            faces[3, 0, 2] = 'b';
            faces[3, 1, 2] = 'g';
            faces[3, 2, 2] = 'y';
                             
            faces[4, 0, 0] = 'o';
            faces[4, 1, 0] = 'r';
            faces[4, 2, 0] = 'y';
            faces[4, 0, 1] = 'o';
            faces[4, 1, 1] = 'b';
            faces[4, 2, 1] = 'y';
            faces[4, 0, 2] = 'r';
            faces[4, 1, 2] = 'b';
            faces[4, 2, 2] = 'w';
                             
            faces[5, 0, 0] = 'b';
            faces[5, 1, 0] = 'r';
            faces[5, 2, 0] = 'g';
            faces[5, 0, 1] = 'w';
            faces[5, 1, 1] = 'g';
            faces[5, 2, 1] = 'b';
            faces[5, 0, 2] = 'g';
            faces[5, 1, 2] = 'b';
            faces[5, 2, 2] = 'b';
            */
            
            CreateCube(faces);

            //MoveSequence("R D2 B U2 R' D2 F D");

            //solution.Clear();

            /*
            for (int i = 0; i < 20; i++)
            {
                cubeCopy1[i].position = cube[i].position;
                cubeCopy1[i].rotation = cube[i].rotation;
                cubeCopy1[i].colour1 = cube[i].colour1;
                cubeCopy1[i].colour2 = cube[i].colour2;
                cubeCopy1[i].colour3 = cube[i].colour3;
            }
            int[] moveSequence = { 1, 0, 0, 0, 0, 0 };
            TurnSequence(moveSequence);
            for (int i = 0; i < 20; i++)
            {
                PrintPiece(cube[i]);
            }
            Console.WriteLine("\n");
            */
            Cross();
            F2L();
            LL();
            AUF();
            ShortenSolution();

            
            foreach (string move in solution)
                Console.Write(move + " ");
        }

        static void AUF()
        {
            Move("z2");
            while (!F2LSolved())
            {
                Move("D");
            }
        }

        static void ShortenSolution()
        {
            bool changesMade = true;
            while (changesMade)
            {
                changesMade = false;
                if (solution.Count != 0)
                {
                    List<string> solutionCopy = new List<string>();
                    foreach (string move in solution)
                    {
                        solutionCopy.Add(move);
                    }

                    solution.Clear();

                    int moveCount = 0;
                    char side = solutionCopy[0][0], side2 = 'z';
                    foreach (string move in solutionCopy)
                    {
                        side2 = move[0];
                        if (side2 == side)
                        {
                            if (move.Length == 1)
                                moveCount++;
                            else if (move[1] == '2')
                                moveCount += 2;
                            else if (move[1] == '\'')
                                moveCount += 3;
                        }
                        else
                        {
                            moveCount %= 4;

                            if (moveCount == 1)
                                solution.Add(side.ToString());
                            else if (moveCount == 2)
                                solution.Add(side + "2");
                            else if (moveCount == 3)
                                solution.Add(side + "\'");

                            moveCount = 0;
                            side = side2;
                            if (move.Length == 1)
                                moveCount++;
                            else if (move[1] == '2')
                                moveCount += 2;
                            else if (move[1] == '\'')
                                moveCount += 3;
                        }

                    }
                    if (moveCount == 1)
                        solution.Add(side.ToString());
                    else if (moveCount == 2)
                        solution.Add(side + "2");
                    else if (moveCount == 3)
                        solution.Add(side + "\'");

                    for (int i = 0; i < solution.Count; i++)
                    {
                        if (solution[i] != solutionCopy[i])
                            changesMade = true;
                    }
                }
            }
        }

        static void Cross()
        {
            for (int i = 0; i < 6; i++)
            {
                centresCopy[i] = centres[i];
            }
            for (int i = 0; i < 20; i++)
            {
                cubeCopy1[i].position = cube[i].position;
                cubeCopy1[i].rotation = cube[i].rotation;
                cubeCopy1[i].colour1 = cube[i].colour1;
                cubeCopy1[i].colour2 = cube[i].colour2;
                cubeCopy1[i].colour3 = cube[i].colour3;
            }

            int[] crossMoves = new int[6];

            int completedPieces = 0;
            int countedPieces = 0;

            if (cubeCopy1[0].colour1 == 'w' && cubeCopy1[0].colour2 == 'o')
                completedPieces++;
            if (cubeCopy1[2].colour1 == 'w' && cubeCopy1[2].colour2 == 'b')
                completedPieces++;
            if (cubeCopy1[4].colour1 == 'w' && cubeCopy1[4].colour2 == 'r')
                completedPieces++;
            if (cubeCopy1[6].colour1 == 'w' && cubeCopy1[6].colour2 == 'g')
                completedPieces++;
            while (completedPieces < 4)
            {
                for (int i = 0; i < 20; i++)
                {
                    cubeCopy1[i].position = cube[i].position;
                    cubeCopy1[i].rotation = cube[i].rotation;
                    cubeCopy1[i].colour1 = cube[i].colour1;
                    cubeCopy1[i].colour2 = cube[i].colour2;
                    cubeCopy1[i].colour3 = cube[i].colour3;
                }
             
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
                                    if (crossMoves[5] > 18)
                                    {
                                        crossMoves[5] = 1;
                                        crossMoves[6]++;
                                    }
                                }
                            }
                        }
                    }
                }
                /*
                
                Console.WriteLine();
                for (int i = 0; i < 20; i++)
                {
                    PrintPiece(cube[i]);
                }
                Console.WriteLine("\n");
                */
                TurnSequence(crossMoves);
                /*
                for (int i = 0; i < 6; i++)
                {
                    Console.Write(crossMoves[i] + " ");
                }
                Console.WriteLine();
                for (int i = 0; i < 20; i++)
                {
                    PrintPiece(cubeCopy1[i]);
                }
                */
               
                //Console.WriteLine("\n\n");


                completedPieces = 0;
                if (cubeCopy1[0].colour1 == 'w' && cubeCopy1[0].colour2 == 'o')
                    completedPieces++;
                if (cubeCopy1[2].colour1 == 'w' && cubeCopy1[2].colour2 == 'b')
                    completedPieces++;
                if (cubeCopy1[4].colour1 == 'w' && cubeCopy1[4].colour2 == 'r')
                    completedPieces++;
                if (cubeCopy1[6].colour1 == 'w' && cubeCopy1[6].colour2 == 'g')
                    completedPieces++;

                if (completedPieces > countedPieces)
                {
                    TurnSequenceCube(crossMoves);
                    crossMoves = new int[6];
                    countedPieces = completedPieces;
                }
            }
        }
        static void TurnSequence(int[] moves)
        {
            foreach (int move in moves)
            {
                if (move == 1)
                    MoveCopy("U");
                else if (move == 2)
                    MoveCopy("U'");
                else if (move == 3)
                    MoveCopy("U2");
                else if (move == 4)
                    MoveCopy("D");
                else if (move == 5)
                    MoveCopy("D'");
                else if (move == 6)
                    MoveCopy("D2");
                else if (move == 7)
                    MoveCopy("F");
                else if (move == 8)
                    MoveCopy("F'");
                else if (move == 9)
                    MoveCopy("F2");
                else if (move == 10)
                    MoveCopy("B");
                else if (move == 11)
                    MoveCopy("B'");
                else if (move == 12)
                    MoveCopy("B2");
                else if (move == 13)
                    MoveCopy("R");
                else if (move == 14)
                    MoveCopy("R'");
                else if (move == 15)
                    MoveCopy("R2");
                else if (move == 16)
                    MoveCopy("L");
                else if (move == 17)
                    MoveCopy("L'");
                else if (move == 18)
                    MoveCopy("L2");

            }
        }
        
        static void TurnSequenceCube(int[] moves)
        {
            foreach (int move in moves)
            {
                if (move == 1)
                    Move("U");
                else if (move == 2)
                    Move("U'");
                else if (move == 3)
                    Move("U2");
                else if (move == 4)
                    Move("D");
                else if (move == 5)
                    Move("D'");
                else if (move == 6)
                    Move("D2");
                else if (move == 7)
                    Move("F");
                else if (move == 8)
                    Move("F'");
                else if (move == 9)
                    Move("F2");
                else if (move == 10)
                    Move("B");
                else if (move == 11)
                    Move("B'");
                else if (move == 12)
                    Move("B2");
                else if (move == 13)
                    Move("R");
                else if (move == 14)
                    Move("R'");
                else if (move == 15)
                    Move("R2");
                else if (move == 16)
                    Move("L");
                else if (move == 17)
                    Move("L'");
                else if (move == 18)
                    Move("L2");

            }
        }
        
        static void F2L()
        {
            Move("z2");
            Dictionary<int, Dictionary<int, string>> algorithms = new Dictionary<int, Dictionary<int, string>>();
            string[] fileInput = File.ReadAllLines("F2L Algorithms.txt", Encoding.UTF8);

            for (int i = 1; i < 7; i++)
            {
                algorithms.Add(i, new Dictionary<int, string>());
            }
            
            foreach (string line in fileInput)
            {
                string[] moves = line.Split(' ');

                algorithms[int.Parse(moves[0])].Add(int.Parse(moves[1]), line);

            }

            int case1 = F2Lcase1(), case2 = F2Lcase2();

            bool pieceSolved = false;
            while (!F2LSolved())
            {
                pieceSolved = false;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        case1 = F2Lcase1();
                        case2 = F2Lcase2();
                        if (!(case1 == -1 || case2 == -1 || (case1 == 4 && case2 == 9) || (case1 == 4 && case2 == 1) || (case1 == 4 && case2 == 2) || (case1 == 4 && case2 == 4 || (case1 == 4 && case2 == 5) || (case1 == 4 && case2 == 7) || (case1 == 4 && case2 == 8) || (case1 == 5 && case2 == 1) || (case1 == 5 && case2 == 2) || (case1 == 5 && case2 == 4 || (case1 == 5 && case2 == 5) || (case1 == 5 && case2 == 7) || (case1 == 5 && case2 == 8) || (case1 == 6 && case2 == 1) || (case1 == 6 && case2 == 2) || (case1 == 6 && case2 == 4 || (case1 == 6 && case2 == 5) || (case1 == 6 && case2 == 7) || (case1 == 6 && case2 == 8))))))
                        {
                            MoveSequence(algorithms[case1][case2]);
                            pieceSolved = true;
                            break;
                        }
                        Move("U");
                    }
                    Move("y");
                }
                if (!pieceSolved)
                {
                    int num = 0;
                    while (slotSolved())
                    {
                        num++;
                        Move("y");
                        if (num >= 4)
                            break;
                    }
                    Move("R");
                    Move("U");
                    Move("R'");
                }
            }

        }

        static bool slotSolved()
        {
            if (cube[9].colour1 == centres[2] && cube[9].colour2 == centres[4] && cube[15].colour1 == centres[1] && cube[15].colour2 == centres[2])
                return true;
            else if ((cube[9].colour1 != centres[0] && cube[9].colour2 != centres[0]) || (cube[15].colour1 == centres[0] && cube[15].colour2 == centres[0] && cube[15].colour1 == centres[0]))
                return false;
            else
                return true;
        }
        static bool F2LSolved()
        {
            if (cube[8].colour1 == centres[3] && cube[8].colour2 == centres[4] && cube[9].colour1 == centres[2] && cube[9].colour2 == centres[4] && cube[10].colour1 == centres[2] && cube[10].colour2 == centres[5] && cube[11].colour1 == centres[3] && cube[11].colour2 == centres[5] && cube[13].colour1 == centres[1] && cube[15].colour1 == centres[1] && cube[17].colour1 == centres[1] && cube[19].colour1 == centres[1] && cube[13].colour2 == centres[4] && cube[15].colour2 == centres[2] && cube[17].colour2 == centres[5] && cube[19].colour2 == centres[3])
                return true;
            else
                return false;
        }
        static int F2Lcase1()
        {
            int case1 = -1;
            char colour1 = centres[1];
            char colour2 = centres[2];
            char colour3 = centres[4];

            if (cube[3].colour1 == colour1 && cube[3].colour2 == colour2 && cube[3].colour3 == colour3)
                case1 = 1;
            else if (cube[3].colour2 == colour1 && cube[3].colour3 == colour2 && cube[3].colour1 == colour3)
                case1 = 2;
            else if (cube[3].colour3 == colour1 && cube[3].colour1 == colour2 && cube[3].colour2 == colour3)
                case1 = 3;
            else if (cube[15].colour1 == colour1 && cube[15].colour2 == colour2 && cube[15].colour3 == colour3)
                case1 = 4;
            else if (cube[15].colour2 == colour1 && cube[15].colour3 == colour2 && cube[15].colour1 == colour3)
                case1 = 5;
            else if (cube[15].colour3 == colour1 && cube[15].colour1 == colour2 && cube[15].colour2 == colour3)
                case1 = 6;


            return case1;
        }

        static int F2Lcase2()
        {
            int case2 = -1;
            char colour1 = centres[4];
            char colour2 = centres[2];

            if (cube[0].colour1 == colour1 && cube[0].colour2 == colour2)
                case2 = 1;
            else if (cube[2].colour1 == colour1 && cube[2].colour2 == colour2)
                case2 = 2;
            else if (cube[4].colour1 == colour1 && cube[4].colour2 == colour2)
                case2 = 3;
            else if (cube[6].colour1 == colour1 && cube[6].colour2 == colour2)
                case2 = 4;
            else if (cube[0].colour2 == colour1 && cube[0].colour1 == colour2)
                case2 = 5;
            else if (cube[2].colour2 == colour1 && cube[2].colour1 == colour2)
                case2 = 6;
            else if (cube[4].colour2 == colour1 && cube[4].colour1 == colour2)
                case2 = 7;
            else if (cube[6].colour2 == colour1 && cube[6].colour1 == colour2)
                case2 = 8;
            else if (cube[9].colour2 == colour1 && cube[9].colour1 == colour2)
                case2 = 9;
            else if (cube[9].colour1 == colour1 && cube[9].colour2 == colour2)
                case2 = 10;

            return case2;
        }


        static void LL()
        {
            bool solved = false;
            string[] fileInput = File.ReadAllLines("1LLL Indexed Algorithms.txt", Encoding.UTF8);

            int llnum1 = LLnum1(), llnum2 = LLnum2(), llnum3 = LLnum3(), llnum4 = LLnum4();
            int num = 0;
            while (!solved)
            {
                llnum1 = LLnum1();
                llnum2 = LLnum2();
                llnum3 = LLnum3();
                llnum4 = LLnum4();
                foreach (string line in fileInput)
                {
                    
                    string[] moves = line.Split(' ');
                    
                    if (int.Parse(moves[0]) == llnum1 && int.Parse(moves[1]) == llnum2 && int.Parse(moves[2]) == llnum3 && int.Parse(moves[3]) == llnum4)
                    {
                        MoveSequence(line);
                        solved = true;
                    }
                }
                num++;
                if (num % 4 == 0)
                {
                    Move("U");
                }
                else
                    Move("y");


            }
            
        }

        static int LLnum1()
        {
            int LLnum1 = 0;

            LLnum1 += cube[0].rotation * 1000;
            LLnum1 += cube[2].rotation * 100;
            LLnum1 += cube[4].rotation * 10;
            LLnum1 += cube[6].rotation * 1;

            return LLnum1;
        }
        static int LLnum2()
        {
            int LLnum2 = 0;

            LLnum2 += cube[1].rotation * 1000;
            LLnum2 += cube[3].rotation * 100;
            LLnum2 += cube[5].rotation * 10;
            LLnum2 += cube[7].rotation * 1;

            return LLnum2;
        }
        static int LLnum3()
        {
            int state = 0;

            char colour;

            for (int i = 0; i < 8; i += 2)
            {
                int power = 1000;
                if (cube[i].rotation == 1)
                    colour = cube[i].colour1;
                else
                    colour = cube[i].colour2;

                for (int j = 0; j < i / 2; j++)
                {
                    power /= 10;
                }
                if (colour == centres[3])
                    state += 0 * power;
                else if (colour == centres[4])
                    state += 1 * power;
                else if (colour == centres[2])
                    state += 2 * power;
                else if (colour == centres[5])
                    state += 3 * power;
            }
            return state;
        }
        static int LLnum4()
        {
            int state = 0;
            char colour;

            for (int i = 1; i < 9; i += 2)
            {
                int power = 1000;
                if (cube[i].rotation == 1)
                    colour = cube[i].colour3;
                else if (cube[i].rotation == 2)
                    colour = cube[i].colour1;
                else
                    colour = cube[i].colour2;


                for (int j = 0; j < i / 2; j++)
                {
                    power /= 10;
                }
                if (colour == centres[3])
                    state += 0 * power;
                else if (colour == centres[4])
                    state += 1 * power;
                else if (colour == centres[2])
                    state += 2 * power;
                else if (colour == centres[5])
                    state += 3 * power;

            }


            return state;
        }

        /*
         * See the comment of class Piece for how the cube works
         */
        static Piece[] cube = new Piece[20];

        /*
         * 0 is the top, 1 is bottom, 2 is front, 3 is back, 4 is right, 5 is left
         * colour of the centre on that side
         */
        static char[] centres = new char[6];
        static char[] centresCopy = new char[6];

        class Piece
        {
            /*
             * primary colour will be top/bottom of cube, secondary colour will be front/back of cube. If a piece has the primary colour
             * that is the colour that will be used for determing rotation. If a piece doesn't the secondary colour will be.
             * For corners, if the primary colour is facing the top/bottom, that is rotation 0, if it has been rotated clockwise one time
             * that is rotation 1, if it has been rotated clockwise twice, that is rotation 2
             * For edges, if the piece is in the top/bottom layer. The primary colour facing the top/bottom is rotation one, facing the sides is
             * rotation 1. If the piece is in the middle layer, facing the front/back is 0, left/right is 2
             */
            public int rotation;//0-2 for corner, 0-1 for edge
            /*
             * The position will be an integer from 0-19
             * Starting from the top middle of the top face will be 0, going clowise around will be 1-7. The middle face will be 8-11, starting from
             * top middle. Bottom face will be the same (looking from top "through the cube") will be 12-19
             */
            public int position; //0-19
            /*
             * colour 1 is the colour that is where the primary colour is in rotation 0, colour 2 is rotation 1, colour 3 is rotation 2
             */
            public char colour1;
            public char colour2;
            public char colour3; //only if cube is a corner

            public Piece(int rotation, int position, char colour1, char colour2, char colour3)
            {
                this.rotation = rotation;
                this.position = position;
                this.colour1 = colour1;
                this.colour2 = colour2;
                this.colour3 = colour3;
            }
        }

        /*
         * The dominant colour is the colour that is on the top/bottom 
         * (e.g. if white is on the top and yellow is on the bottom, whichever of the two that corner has will be the dominant colour)
         * The dominant colour is used for determining the rotation of the piece
         */
        static char GetDominantColourCorner(Piece piece)
        {
            char dominantColour = 'z';
            if (piece.colour1 == centres[0] || piece.colour1 == centres[1])
                dominantColour = piece.colour1;
            else if (piece.colour2 == centres[0] || piece.colour2 == centres[1])
                dominantColour = piece.colour2;
            else if (piece.colour3 == centres[0] || piece.colour3 == centres[1])
                dominantColour = piece.colour3;

            return dominantColour;
        }
        /*
         * If the corner belongs on the top/bottom layer, the dominant colour is the colour that is on the top/bottom, if the corner belongs
         * in the equator, the dominant colour is the colour that is on the front/back
         */
        static char GetDominantColourEdge(Piece piece)
        {
            char dominantColour = 'z';

            if (piece.colour1 == centres[0] || piece.colour1 == centres[1])
                dominantColour = piece.colour1;
            else if (piece.colour2 == centres[0] || piece.colour2 == centres[1])
                dominantColour = piece.colour2;
            else if (piece.colour1 == centres[2] || piece.colour1 == centres[3])
                dominantColour = piece.colour1;
            else if (piece.colour2 == centres[2] || piece.colour2 == centres[3])
                dominantColour = piece.colour2;

            return dominantColour;
        }

        /*
         * Will return the rotation of the corner (int from 0-2 inclusive)
         * 0 means that the dominant colour is on the top/bottom, 1 means it has been rotated clockwise from that position and 2 is counter-clockwise
         */
        static int GetRotationCorner(Piece piece)
        {
            int rotation = -1;
            char dominantColour = GetDominantColourCorner(piece);
            if (piece.colour1 == dominantColour) rotation = 0;
            else if (piece.colour2 == dominantColour) rotation = 1;
            else if (piece.colour3 == dominantColour) rotation = 2;

            return rotation;
        }

        /*
         * Will return the rotation of the corner (int from 0-1 inclusive)
         * For pieces on the top/bottom 0 means the dominant colour is on the top.bottom, 1 means its not
         * For pieces in the equator, 0 means the domiannt colour is on the front/back, 1 means it is on the left/right
         */
        static int GetRotationEdge(Piece piece)
        {
            int rotation = -1;
            char dominantColour = GetDominantColourEdge(piece);
            if (piece.colour1 == dominantColour) rotation = 0;
            else if (piece.colour2 == dominantColour) rotation = 1;

            return rotation;
        }

        static Piece CreateCorner(int position, char colour1, char colour2, char colour3)
        {
            Piece corner = new Piece(0, position, colour1, colour2, colour3);
            corner.rotation = GetRotationCorner(corner);
            return corner;
        }

        static Piece CreateEdge(int position, char colour1, char colour2)
        {
            Piece edge = new Piece(0, position, colour1, colour2, 'z');
            edge.rotation = GetRotationEdge(edge);
            return edge;
        }

        /*
         * Input will be a char[6, 3, 3] for each of the colours on the cube. The first number will be the face of the cube
         * 0 = white, 1 = yellow, 2 = red, 3 = orange, 4 = blue, 5 = green.
         * Second will be the row (0 = left, 2 = right)
         * Third will be the column (0 = top, 2 = bottom)
         * 
         * The function will then populate the Piece[] Cube
         */
        static void CreateCube(char[,,] faces)
        {
            centres[0] = 'w';
            centres[1] = 'y';
            centres[2] = 'r';
            centres[3] = 'o';
            centres[4] = 'b';
            centres[5] = 'g';

            cube[1] = CreateCorner(1, faces[0, 2, 0], faces[3, 0, 0], faces[4, 2, 0]);
            cube[3] = CreateCorner(3, faces[0, 2, 2], faces[4, 0, 0], faces[2, 2, 0]);
            cube[5] = CreateCorner(5, faces[0, 0, 2], faces[2, 0, 0], faces[5, 2, 0]);
            cube[7] = CreateCorner(7, faces[0, 0, 0], faces[5, 0, 0], faces[3, 2, 0]);
            cube[13] = CreateCorner(13, faces[1, 0, 0], faces[4, 2, 2], faces[3, 0, 2]);
            cube[15] = CreateCorner(15, faces[1, 0, 2], faces[2, 2, 2], faces[4, 0, 2]);
            cube[17] = CreateCorner(17, faces[1, 2, 2], faces[5, 2, 2], faces[2, 0, 2]);
            cube[19] = CreateCorner(19, faces[1, 2, 0], faces[3, 2, 2], faces[5, 0, 2]);

            cube[0] = CreateEdge(0, faces[0, 1, 0], faces[3, 1, 0]);
            cube[2] = CreateEdge(2, faces[0, 2, 1], faces[4, 1, 0]);
            cube[4] = CreateEdge(4, faces[0, 1, 2], faces[2, 1, 0]);
            cube[6] = CreateEdge(6, faces[0, 0, 1], faces[5, 1, 0]);
            cube[8] = CreateEdge(8, faces[3, 0, 1], faces[4, 2, 1]);
            cube[9] = CreateEdge(9, faces[2, 2, 1], faces[4, 0, 1]);
            cube[10] = CreateEdge(10, faces[2, 0, 1], faces[5, 2, 1]);
            cube[11] = CreateEdge(11, faces[3, 2, 1], faces[5, 0, 1]);
            cube[12] = CreateEdge(12, faces[1, 1, 0], faces[3, 1, 2]);
            cube[14] = CreateEdge(14, faces[1, 0, 1], faces[4, 1, 2]);
            cube[16] = CreateEdge(16, faces[1, 1, 2], faces[2, 1, 2]);
            cube[18] = CreateEdge(18, faces[1, 2, 1], faces[5, 1, 2]);


        }

        /*
         * Will rotate piece by rotation times.
         * For edges, 1 will flip it
         * For corners, 1 will rotate clockwise once and 2 will rotate clockwise twice (or counter-clockwise once)
         */
        static Piece Rotate(Piece piece, int rotation)
        {
            if (piece.colour3 == 'z')//if piece is an edge
            {
                char colour = piece.colour1;
                piece.colour1 = piece.colour2;
                piece.colour2 = colour;
                piece.rotation = rotation;
            }
            else//piece is a corner
            {
                while (rotation >= 1)
                {
                    char colour = piece.colour1;
                    piece.colour1 = piece.colour3;
                    piece.colour3 = piece.colour2;
                    piece.colour2 = colour;
                    piece.rotation++;
                    if (piece.rotation >= 3)
                    {
                        piece.rotation = 0;
                    }
                    rotation--;
                }
            }
            return piece;
        }

        /*
         * Called after pieces have been moved.
         * The Move() function moves the pieces positions in the Piece[] cube, and this will update the pieces internal storage of their position
         */
        static void UpdatePositions()
        {
            for (int i = 0; i < 20; i++)
            {
                cube[i].position = i;
            }
        }

        /*
         * Same as above but for rotations
         */
        static void UpdateRotations()
        {
            cube[1].rotation = GetRotationCorner(cube[1]);
            cube[3].rotation = GetRotationCorner(cube[3]);
            cube[5].rotation = GetRotationCorner(cube[5]);
            cube[7].rotation = GetRotationCorner(cube[7]);
            cube[13].rotation = GetRotationCorner(cube[13]);
            cube[15].rotation = GetRotationCorner(cube[15]);
            cube[17].rotation = GetRotationCorner(cube[17]);
            cube[19].rotation = GetRotationCorner(cube[19]);

            cube[0].rotation = GetRotationEdge(cube[0]);
            cube[2].rotation = GetRotationEdge(cube[2]);
            cube[4].rotation = GetRotationEdge(cube[4]);
            cube[6].rotation = GetRotationEdge(cube[6]);
            cube[8].rotation = GetRotationEdge(cube[8]);
            cube[9].rotation = GetRotationEdge(cube[9]);
            cube[10].rotation = GetRotationEdge(cube[10]);
            cube[11].rotation = GetRotationEdge(cube[11]);
            cube[12].rotation = GetRotationEdge(cube[12]);
            cube[14].rotation = GetRotationEdge(cube[14]);
            cube[16].rotation = GetRotationEdge(cube[16]);
            cube[18].rotation = GetRotationEdge(cube[18]);
        }


        static void UpdatePositionsCopy()
        {
            for (int i = 0; i < 20; i++)
            {
                cubeCopy1[i].position = i;
            }
        }
        

        /*
         * Same as above but for rotations
         */
        
        static void UpdateRotationsCopy()
        {
            cubeCopy1[1].rotation = GetRotationCorner(cubeCopy1[1]);
            cubeCopy1[3].rotation = GetRotationCorner(cubeCopy1[3]);
            cubeCopy1[5].rotation = GetRotationCorner(cubeCopy1[5]);
            cubeCopy1[7].rotation = GetRotationCorner(cubeCopy1[7]);
            cubeCopy1[13].rotation = GetRotationCorner(cubeCopy1[13]);
            cubeCopy1[15].rotation = GetRotationCorner(cubeCopy1[15]);
            cubeCopy1[17].rotation = GetRotationCorner(cubeCopy1[17]);
            cubeCopy1[19].rotation = GetRotationCorner(cubeCopy1[19]);
                                                          
            cubeCopy1[0].rotation = GetRotationEdge(cubeCopy1[0]);
            cubeCopy1[2].rotation = GetRotationEdge(cubeCopy1[2]);
            cubeCopy1[4].rotation = GetRotationEdge(cubeCopy1[4]);
            cubeCopy1[6].rotation = GetRotationEdge(cubeCopy1[6]);
            cubeCopy1[8].rotation = GetRotationEdge(cubeCopy1[8]);
            cubeCopy1[9].rotation = GetRotationEdge(cubeCopy1[9]);
            cubeCopy1[10].rotation = GetRotationEdge(cubeCopy1[10]);
            cubeCopy1[11].rotation = GetRotationEdge(cubeCopy1[11]);
            cubeCopy1[12].rotation = GetRotationEdge(cubeCopy1[12]);
            cubeCopy1[14].rotation = GetRotationEdge(cubeCopy1[14]);
            cubeCopy1[16].rotation = GetRotationEdge(cubeCopy1[16]);
            cubeCopy1[18].rotation = GetRotationEdge(cubeCopy1[18]);
        }
        

        /*
         * Input will be a "move" to make to the cube, possible inputs are
         * U U' U2 U2' u u' u2 u2' - (for all of U, D, F, B, R, L)
         * x, x2, x' - (for all of x, y, z)
         * 
         * Will then update the Piece[] Cube accordingly
         */

        
        static void Move(string move)
        {
            Piece[] cubeCopy = new Piece[20];
            for (int i = 0; i < 20; i++)
            {
                cubeCopy[i] = cube[i];
            }
            if (move == "U")
            {
                cube[0] = cubeCopy[6];
                cube[1] = cubeCopy[7];
                cube[2] = cubeCopy[0];
                cube[3] = cubeCopy[1];
                cube[4] = cubeCopy[2];
                cube[5] = cubeCopy[3];
                cube[6] = cubeCopy[4];
                cube[7] = cubeCopy[5];

                solution.Add(centres[0].ToString());
            }
            else if (move == "U2" || move == "U2'")
            {
                Move("U");
                Move("U");
            }
            else if (move == "U'")
            {
                Move("U");
                Move("U");
                Move("U");
            }

            else if (move == "D")
            {
                Move("z2");
                Move("U");
                Move("z2");
            }
            else if (move == "D2" || move == "D2'")
            {
                Move("z2");
                Move("U2");
                Move("z2");
            }
            else if (move == "D'")
            {
                Move("z2");
                Move("U'");
                Move("z2");
            }

            else if (move == "L")
            {
                Move("z");
                Move("U");
                Move("z'");
            }
            else if (move == "L2" || move == "L2'")
            {
                Move("z");
                Move("U2");
                Move("z'");
            }
            else if (move == "L'")
            {
                Move("z");
                Move("U'");
                Move("z'");
            }

            else if (move == "R")
            {
                Move("z'");
                Move("U");
                Move("z");
            }
            else if (move == "R2" || move == "R2'")
            {
                Move("z'");
                Move("U2");
                Move("z");
            }
            else if (move == "R'")
            {
                Move("z'");
                Move("U'");
                Move("z");
            }

            else if (move == "F")
            {
                Move("x");
                Move("U");
                Move("x'");
            }
            else if (move == "F2" || move == "F2'")
            {
                Move("x");
                Move("U2");
                Move("x'");
            }
            else if (move == "F'")
            {
                Move("x");
                Move("U'");
                Move("x'");
            }

            else if (move == "B")
            {
                Move("x'");
                Move("U");
                Move("x");
            }
            else if (move == "B2" || move == "B2'")
            {
                Move("x'");
                Move("U2");
                Move("x");
            }
            else if (move == "B'")
            {
                Move("x'");
                Move("U'");
                Move("x");
            }

            else if (move == "u")
            {
                Move("D");
                Move("y");
            }
            else if (move == "u2" || move == "u2'")
            {
                Move("D2");
                Move("y2");
            }
            else if (move == "u'")
            {
                Move("D'");
                Move("y'");
            }

            else if (move == "d")
            {
                Move("U");
                Move("y'");
            }
            else if (move == "d2" || move == "d2'")
            {
                Move("U2");
                Move("y2");
            }
            else if (move == "d'")
            {
                Move("U'");
                Move("y");
            }

            else if (move == "l")
            {
                Move("R");
                Move("x'");
            }
            else if (move == "l2" || move == "l2'")
            {
                Move("R2");
                Move("x2");
            }
            else if (move == "l'")
            {
                Move("R'");
                Move("x");
            }

            else if (move == "r")
            {
                Move("L");
                Move("x");
            }
            else if (move == "r2" || move == "r2'")
            {
                Move("L2");
                Move("x2");
            }
            else if (move == "r'")
            {
                Move("L'");
                Move("x'");
            }

            else if (move == "f")
            {
                Move("B");
                Move("z");
            }
            else if (move == "f2" || move == "f2'")
            {
                Move("B2");
                Move("z2");
            }
            else if (move == "f'")
            {
                Move("B'");
                Move("z'");
            }

            else if (move == "b")
            {
                Move("F");
                Move("z'");
            }
            else if (move == "b2" || move == "b2'")
            {
                Move("F2");
                Move("z2");
            }
            else if (move == "b'")
            {
                Move("F'");
                Move("z");
            }

            else if (move == "x")
            {
                char colour = centres[0];
                centres[0] = centres[2];
                centres[2] = centres[1];
                centres[1] = centres[3];
                centres[3] = colour;

                cube[0] = cubeCopy[4];
                cube[1] = cubeCopy[3];
                cube[2] = cubeCopy[9];
                cube[3] = cubeCopy[15];
                cube[4] = cubeCopy[16];
                cube[5] = cubeCopy[17];
                cube[6] = cubeCopy[10];
                cube[7] = cubeCopy[5];
                cube[8] = cubeCopy[2];
                cube[9] = cubeCopy[14];
                cube[10] = cubeCopy[18];
                cube[11] = cubeCopy[6];
                cube[12] = cubeCopy[0];
                cube[13] = cubeCopy[1];
                cube[14] = cubeCopy[8];
                cube[15] = cubeCopy[13];
                cube[16] = cubeCopy[12];
                cube[17] = cubeCopy[19];
                cube[18] = cubeCopy[11];
                cube[19] = cubeCopy[7];

                cube[0] = Rotate(cube[0], 1);
                cube[1] = Rotate(cube[1], 1);

                cube[3] = Rotate(cube[3], 2);
                cube[4] = Rotate(cube[4], 1);
                cube[5] = Rotate(cube[5], 1);

                cube[7] = Rotate(cube[7], 2);

                cube[12] = Rotate(cube[12], 1);
                cube[13] = Rotate(cube[13], 2);

                cube[15] = Rotate(cube[15], 1);
                cube[16] = Rotate(cube[16], 1);
                cube[17] = Rotate(cube[17], 2);

                cube[19] = Rotate(cube[19], 1);


            }
            else if (move == "x'")
            {
                Move("x");
                Move("x");
                Move("x");
            }
            else if (move == "x2")
            {
                Move("x");
                Move("x");
            }

            else if (move == "y")
            {
                char colour = centres[2];
                centres[2] = centres[4];
                centres[4] = centres[3];
                centres[3] = centres[5];
                centres[5] = colour;

                cube[0] = cubeCopy[6];
                cube[1] = cubeCopy[7];
                cube[2] = cubeCopy[0];
                cube[3] = cubeCopy[1];
                cube[4] = cubeCopy[2];
                cube[5] = cubeCopy[3];
                cube[6] = cubeCopy[4];
                cube[7] = cubeCopy[5];
                cube[8] = cubeCopy[11];
                cube[9] = cubeCopy[8];
                cube[10] = cubeCopy[9];
                cube[11] = cubeCopy[10];
                cube[12] = cubeCopy[18];
                cube[13] = cubeCopy[19];
                cube[14] = cubeCopy[12];
                cube[15] = cubeCopy[13];
                cube[16] = cubeCopy[14];
                cube[17] = cubeCopy[15];
                cube[18] = cubeCopy[16];
                cube[19] = cubeCopy[17];

                cube[8] = Rotate(cube[8], 1);
                cube[9] = Rotate(cube[9], 1);
                cube[10] = Rotate(cube[10], 1);
                cube[11] = Rotate(cube[11], 1);
            }
            else if (move == "y'")
            {
                Move("y");
                Move("y");
                Move("y");
            }
            else if (move == "y2")
            {
                Move("y");
                Move("y");
            }

            else if (move == "z")
            {
                char colour = centres[0];
                centres[0] = centres[5];
                centres[5] = centres[1];
                centres[1] = centres[4];
                centres[4] = colour;

                cube[0] = cubeCopy[11];
                cube[1] = cubeCopy[7];
                cube[2] = cubeCopy[6];
                cube[3] = cubeCopy[5];
                cube[4] = cubeCopy[10];
                cube[5] = cubeCopy[17];
                cube[6] = cubeCopy[18];
                cube[7] = cubeCopy[19];
                cube[8] = cubeCopy[0];
                cube[9] = cubeCopy[4];
                cube[10] = cubeCopy[16];
                cube[11] = cubeCopy[12];
                cube[12] = cubeCopy[8];
                cube[13] = cubeCopy[1];
                cube[14] = cubeCopy[2];
                cube[15] = cubeCopy[3];
                cube[16] = cubeCopy[9];
                cube[17] = cubeCopy[15];
                cube[18] = cubeCopy[14];
                cube[19] = cubeCopy[13];

                cube[0] = Rotate(cube[0], 1);
                cube[1] = Rotate(cube[1], 2);
                cube[2] = Rotate(cube[2], 1);
                cube[3] = Rotate(cube[3], 1);
                cube[4] = Rotate(cube[4], 1);
                cube[5] = Rotate(cube[5], 2);
                cube[6] = Rotate(cube[6], 1);
                cube[7] = Rotate(cube[7], 1);
                cube[8] = Rotate(cube[8], 1);
                cube[9] = Rotate(cube[9], 1);
                cube[10] = Rotate(cube[10], 1);
                cube[11] = Rotate(cube[11], 1);
                cube[12] = Rotate(cube[12], 1);
                cube[13] = Rotate(cube[13], 1);
                cube[14] = Rotate(cube[14], 1);
                cube[15] = Rotate(cube[15], 2);
                cube[16] = Rotate(cube[16], 1);
                cube[17] = Rotate(cube[17], 1);
                cube[18] = Rotate(cube[18], 1);
                cube[19] = Rotate(cube[19], 2);
            }
            else if (move == "z'")
            {
                Move("z");
                Move("z");
                Move("z");
            }
            else if (move == "z2")
            {
                Move("z");
                Move("z");
            }

            UpdatePositions();
            UpdateRotations();
        }
        
        static void MoveCopy(string move)
        {
            Piece[] cubeCopy = new Piece[20];
            for (int i = 0; i < 20; i++)
            {
                cubeCopy[i] = cubeCopy1[i];
            }
            if (move == "U")
            {
                cubeCopy1[0] = cubeCopy[6];
                cubeCopy1[1] = cubeCopy[7];
                cubeCopy1[2] = cubeCopy[0];
                cubeCopy1[3] = cubeCopy[1];
                cubeCopy1[4] = cubeCopy[2];
                cubeCopy1[5] = cubeCopy[3];
                cubeCopy1[6] = cubeCopy[4];
                cubeCopy1[7] = cubeCopy[5];

            }
            else if (move == "U2" || move == "U2'")
            {
                MoveCopy("U");
                MoveCopy("U");
            }
            else if (move == "U'")
            {
                MoveCopy("U");
                MoveCopy("U");
                MoveCopy("U");
            }

            else if (move == "D")
            {
                MoveCopy("z2");
                MoveCopy("U");
                MoveCopy("z2");
            }
            else if (move == "D2" || move == "D2'")
            {
                MoveCopy("z2");
                MoveCopy("U2");
                MoveCopy("z2");
            }
            else if (move == "D'")
            {
                MoveCopy("z2");
                MoveCopy("U'");
                MoveCopy("z2");
            }

            else if (move == "L")
            {
                MoveCopy("z");
                MoveCopy("U");
                MoveCopy("z'");
            }
            else if (move == "L2" || move == "L2'")
            {
                MoveCopy("z");
                MoveCopy("U2");
                MoveCopy("z'");
            }
            else if (move == "L'")
            {
                MoveCopy("z");
                MoveCopy("U'");
                MoveCopy("z'");
            }

            else if (move == "R")
            {
                MoveCopy("z'");
                MoveCopy("U");
                MoveCopy("z");
            }
            else if (move == "R2" || move == "R2'")
            {
                MoveCopy("z'");
                MoveCopy("U2");
                MoveCopy("z");
            }
            else if (move == "R'")
            {
                MoveCopy("z'");
                MoveCopy("U'");
                MoveCopy("z");
            }

            else if (move == "F")
            {
                MoveCopy("x");
                MoveCopy("U");
                MoveCopy("x'");
            }
            else if (move == "F2" || move == "F2'")
            {
                MoveCopy("x");
                MoveCopy("U2");
                MoveCopy("x'");
            }
            else if (move == "F'")
            {
                MoveCopy("x");
                MoveCopy("U'");
                MoveCopy("x'");
            }

            else if (move == "B")
            {
                MoveCopy("x'");
                MoveCopy("U");
                MoveCopy("x");
            }
            else if (move == "B2" || move == "B2'")
            {
                MoveCopy("x'");
                MoveCopy("U2");
                MoveCopy("x");
            }
            else if (move == "B'")
            {
                MoveCopy("x'");
                MoveCopy("U'");
                MoveCopy("x");
            }

            else if (move == "u")
            {
                MoveCopy("D");
                MoveCopy("y");
            }
            else if (move == "u2" || move == "u2'")
            {
                MoveCopy("D2");
                MoveCopy("y2");
            }
            else if (move == "u'")
            {
                MoveCopy("D'");
                MoveCopy("y'");
            }

            else if (move == "d")
            {
                MoveCopy("U");
                MoveCopy("y'");
            }
            else if (move == "d2" || move == "d2'")
            {
                MoveCopy("U2");
                MoveCopy("y2");
            }
            else if (move == "d'")
            {
                MoveCopy("U'");
                MoveCopy("y");
            }

            else if (move == "l")
            {
                MoveCopy("R");
                MoveCopy("x'");
            }
            else if (move == "l2" || move == "l2'")
            {
                MoveCopy("R2");
                MoveCopy("x2");
            }
            else if (move == "l'")
            {
                MoveCopy("R'");
                MoveCopy("x");
            }

            else if (move == "r")
            {
                MoveCopy("L");
                MoveCopy("x");
            }
            else if (move == "r2" || move == "r2'")
            {
                MoveCopy("L2");
                MoveCopy("x2");
            }
            else if (move == "r'")
            {
                MoveCopy("L'");
                MoveCopy("x'");
            }

            else if (move == "f")
            {
                MoveCopy("B");
                MoveCopy("z");
            }
            else if (move == "f2" || move == "f2'")
            {
                MoveCopy("B2");
                MoveCopy("z2");
            }
            else if (move == "f'")
            {
                MoveCopy("B'");
                MoveCopy("z'");
            }

            else if (move == "b")
            {
                MoveCopy("F");
                MoveCopy("z'");
            }
            else if (move == "b2" || move == "b2'")
            {
                MoveCopy("F2");
                MoveCopy("z2");
            }
            else if (move == "b'")
            {
                MoveCopy("F'");
                MoveCopy("z");
            }

            else if (move == "x")
            {
                char colour = centresCopy[0];
                centresCopy[0] = centresCopy[2];
                centresCopy[2] = centresCopy[1];
                centresCopy[1] = centresCopy[3];
                centresCopy[3] = colour;

                cubeCopy1[0] = cubeCopy[4];
                cubeCopy1[1] = cubeCopy[3];
                cubeCopy1[2] = cubeCopy[9];
                cubeCopy1[3] = cubeCopy[15];
                cubeCopy1[4] = cubeCopy[16];
                cubeCopy1[5] = cubeCopy[17];
                cubeCopy1[6] = cubeCopy[10];
                cubeCopy1[7] = cubeCopy[5];
                cubeCopy1[8] = cubeCopy[2];
                cubeCopy1[9] = cubeCopy[14];
                cubeCopy1[10] = cubeCopy[18];
                cubeCopy1[11] = cubeCopy[6];
                cubeCopy1[12] = cubeCopy[0];
                cubeCopy1[13] = cubeCopy[1];
                cubeCopy1[14] = cubeCopy[8];
                cubeCopy1[15] = cubeCopy[13];
                cubeCopy1[16] = cubeCopy[12];
                cubeCopy1[17] = cubeCopy[19];
                cubeCopy1[18] = cubeCopy[11];
                cubeCopy1[19] = cubeCopy[7];

                cubeCopy1[0] = Rotate(cubeCopy1[0], 1);
                cubeCopy1[1] = Rotate(cubeCopy1[1], 1);

                cubeCopy1[3] = Rotate(cubeCopy1[3], 2);
                cubeCopy1[4] = Rotate(cubeCopy1[4], 1);
                cubeCopy1[5] = Rotate(cubeCopy1[5], 1);

                cubeCopy1[7] = Rotate(cubeCopy1[7], 2);

                cubeCopy1[12] = Rotate(cubeCopy1[12], 1);
                cubeCopy1[13] = Rotate(cubeCopy1[13], 2);

                cubeCopy1[15] = Rotate(cubeCopy1[15], 1);
                cubeCopy1[16] = Rotate(cubeCopy1[16], 1);
                cubeCopy1[17] = Rotate(cubeCopy1[17], 2);

                cubeCopy1[19] = Rotate(cubeCopy1[19], 1);


            }
            else if (move == "x'")
            {
                MoveCopy("x");
                MoveCopy("x");
                MoveCopy("x");
            }
            else if (move == "x2")
            {
                MoveCopy("x");
                MoveCopy("x");
            }

            else if (move == "y")
            {
                char colour = centresCopy[2];
                centresCopy[2] = centresCopy[4];
                centresCopy[4] = centresCopy[3];
                centresCopy[3] = centresCopy[5];
                centresCopy[5] = colour;

                cubeCopy1[0] = cubeCopy[6];
                cubeCopy1[1] = cubeCopy[7];
                cubeCopy1[2] = cubeCopy[0];
                cubeCopy1[3] = cubeCopy[1];
                cubeCopy1[4] = cubeCopy[2];
                cubeCopy1[5] = cubeCopy[3];
                cubeCopy1[6] = cubeCopy[4];
                cubeCopy1[7] = cubeCopy[5];
                cubeCopy1[8] = cubeCopy[11];
                cubeCopy1[9] = cubeCopy[8];
                cubeCopy1[10] = cubeCopy[9];
                cubeCopy1[11] = cubeCopy[10];
                cubeCopy1[12] = cubeCopy[18];
                cubeCopy1[13] = cubeCopy[19];
                cubeCopy1[14] = cubeCopy[12];
                cubeCopy1[15] = cubeCopy[13];
                cubeCopy1[16] = cubeCopy[14];
                cubeCopy1[17] = cubeCopy[15];
                cubeCopy1[18] = cubeCopy[16];
                cubeCopy1[19] = cubeCopy[17];

                cubeCopy1[8] = Rotate(cubeCopy1[8], 1);
                cubeCopy1[9] = Rotate(cubeCopy1[9], 1);
                cubeCopy1[10] = Rotate(cubeCopy1[10], 1);
                cubeCopy1[11] = Rotate(cubeCopy1[11], 1);
            }
            else if (move == "y'")
            {
                MoveCopy("y");
                MoveCopy("y");
                MoveCopy("y");
            }
            else if (move == "y2")
            {
                MoveCopy("y");
                MoveCopy("y");
            }

            else if (move == "z")
            {
                char colour = centresCopy[0];
                centresCopy[0] = centresCopy[5];
                centresCopy[5] = centresCopy[1];
                centresCopy[1] = centresCopy[4];
                centresCopy[4] = colour;

                cubeCopy1[0] = cubeCopy[11];
                cubeCopy1[1] = cubeCopy[7];
                cubeCopy1[2] = cubeCopy[6];
                cubeCopy1[3] = cubeCopy[5];
                cubeCopy1[4] = cubeCopy[10];
                cubeCopy1[5] = cubeCopy[17];
                cubeCopy1[6] = cubeCopy[18];
                cubeCopy1[7] = cubeCopy[19];
                cubeCopy1[8] = cubeCopy[0];
                cubeCopy1[9] = cubeCopy[4];
                cubeCopy1[10] = cubeCopy[16];
                cubeCopy1[11] = cubeCopy[12];
                cubeCopy1[12] = cubeCopy[8];
                cubeCopy1[13] = cubeCopy[1];
                cubeCopy1[14] = cubeCopy[2];
                cubeCopy1[15] = cubeCopy[3];
                cubeCopy1[16] = cubeCopy[9];
                cubeCopy1[17] = cubeCopy[15];
                cubeCopy1[18] = cubeCopy[14];
                cubeCopy1[19] = cubeCopy[13];

                cubeCopy1[0] = Rotate(cubeCopy1[0], 1);
                cubeCopy1[1] = Rotate(cubeCopy1[1], 2);
                cubeCopy1[2] = Rotate(cubeCopy1[2], 1);
                cubeCopy1[3] = Rotate(cubeCopy1[3], 1);
                cubeCopy1[4] = Rotate(cubeCopy1[4], 1);
                cubeCopy1[5] = Rotate(cubeCopy1[5], 2);
                cubeCopy1[6] = Rotate(cubeCopy1[6], 1);
                cubeCopy1[7] = Rotate(cubeCopy1[7], 1);
                cubeCopy1[8] = Rotate(cubeCopy1[8], 1);
                cubeCopy1[9] = Rotate(cubeCopy1[9], 1);
                cubeCopy1[10] = Rotate(cubeCopy1[10], 1);
                cubeCopy1[11] = Rotate(cubeCopy1[11], 1);
                cubeCopy1[12] = Rotate(cubeCopy1[12], 1);
                cubeCopy1[13] = Rotate(cubeCopy1[13], 1);
                cubeCopy1[14] = Rotate(cubeCopy1[14], 1);
                cubeCopy1[15] = Rotate(cubeCopy1[15], 2);
                cubeCopy1[16] = Rotate(cubeCopy1[16], 1);
                cubeCopy1[17] = Rotate(cubeCopy1[17], 1);
                cubeCopy1[18] = Rotate(cubeCopy1[18], 1);
                cubeCopy1[19] = Rotate(cubeCopy1[19], 2);
            }
            else if (move == "z'")
            {
                MoveCopy("z");
                MoveCopy("z");
                MoveCopy("z");
            }
            else if (move == "z2")
            {
                MoveCopy("z");
                MoveCopy("z");
            }

            UpdatePositionsCopy();
            UpdateRotationsCopy();
        }

        /*
         * Used for debugging, will print all the information about a specific piece
         */
        static void PrintPiece(Piece piece)
        {
            Console.WriteLine("{0} {1} {2} {3} {4}", piece.position, piece.rotation, piece.colour1, piece.colour2, piece.colour3);
        }

        static void MoveSequence(string sequence)
        {
            string[] moves = sequence.Split(' ');
            foreach (string move in moves)
            {
                Move(move);
            }
        }
    }
}
