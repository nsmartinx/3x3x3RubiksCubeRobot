using System;
using System.Collections.Generic;
using System.Linq;

namespace Rubik_s_Cube_Test
{
    class Program
    {
        static List<String> moves = new List<string>();
        static String[,,] faces = new String[6, 3, 3];

        static void Main()
        {
         
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
            

            Test();

            WhiteCross();
            WhiteCorners();
            SecondLayerEdges();
            OLLEdges();
            OLLCorners();
            PLLCorners();
            PLLEdges();
            TurnLastLayer();

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
        }
        static void Test()
        {
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
            for (int colourNum = 0; colourNum < 4; colourNum++)
            {
                string colour = " ";
                if (colourNum == 0)
                    colour = "r";
                else if (colourNum == 1)
                    colour = "b";
                else if (colourNum == 2)
                    colour = "o";
                else if (colourNum == 3)
                    colour = "g";


                if ((faces[0, 0, 1] == "w" && faces[2, 0, 1] == colour) || (faces[0, 1, 0] == "w" && faces[4, 0, 1] == colour) || (faces[0, 2, 1] == "w" && faces[3, 0, 1] == colour) || (faces[0, 1, 2] == "w" && faces[5, 0, 1] == colour))//piece is in white layer, oriented correctly
                {

                    int num = 0;
                    while (!(faces[0, 0, 1] == "w" && faces[2, 0, 1] == colour))
                    {
                        Turn("w");
                        num++;
                    }
                    if (num > 0)
                    {
                        Turn("r");
                        for (int i = 0; i < num; i++)
                        {
                            Turn("w'");
                        }
                        Turn("r'");
                    }
                }
                else if ((faces[0, 0, 1] == colour && faces[2, 0, 1] == "w") || (faces[0, 1, 0] == colour && faces[4, 0, 1] == "w") || (faces[0, 2, 1] == colour && faces[3, 0, 1] == "w") || faces[0, 1, 2] == colour && faces[5, 0, 1] == "w")//piece is in white layer, oriented incorrectly
                {
                    int num = 0;
                    while (!(faces[0, 1, 2] == colour && faces[5, 0, 1] == "w"))//when piece is above green side
                    {
                        Turn("w");
                        num++;
                    }
                    Turn("g");
                    if (num > 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            Turn("w'");
                        }
                    }
                    Turn("r");
                }
                else if (faces[2, 1, 0] == colour && faces[5, 1, 2] == "w")//piece is between red and green, white facing green
                {
                    Turn("r");
                }
                else if (faces[2, 1, 0] == "w" && faces[5, 1, 2] == colour)//piece is between red and green, white facing red
                {
                    Turn("w");
                    Turn("g'");
                    Turn("w'");
                }
                else if (faces[2, 1, 2] == colour && faces[4, 1, 0] == "w")//piece is between red and blue, white facing blue
                {
                    Turn("r'");
                }
                else if (faces[2, 1, 2] == "w" && faces[4, 1, 0] == colour)//piece is between red and blue, white facing red
                {
                    Turn("w'");
                    Turn("b");
                    Turn("w");
                }
                else if (faces[4, 1, 2] == "w" && faces[3, 1, 0] == colour)//piece is between orange and blue, white facing blue
                {
                    Turn("w");
                    Turn("w");
                    Turn("o");
                    Turn("w");
                    Turn("w");
                }
                else if (faces[4, 1, 2] == colour && faces[3, 1, 0] == "w")//piece is between orange and blue, white facing orange
                {
                    Turn("w'");
                    Turn("b'");
                    Turn("w");
                }
                else if (faces[3, 1, 2] == colour && faces[5, 1, 0] == "w")//piece is between orange and green, white facing green
                {
                    Turn("w");
                    Turn("w");
                    Turn("o'");
                    Turn("w");
                    Turn("w");
                }
                else if (faces[3, 1, 2] == "w" && faces[5, 1, 0] == colour)//piece is between orange and green, white facing orange
                {
                    Turn("w");
                    Turn("g");
                    Turn("w'");
                }
                else if ((faces[1, 0, 1] == "w" && faces[2, 2, 1] == colour) || (faces[1, 1, 0] == "w" && faces[5, 2, 1] == colour) || (faces[1, 2, 1] == "w" && faces[3, 2, 1] == colour) || (faces[1, 1, 2] == "w" && faces[4, 2, 1] == colour))//piece is on yellow layer, white facing yellow
                {
                    while (!(faces[1, 0, 1] == "w" && faces[2, 2, 1] == colour))
                    {
                        Turn("y");
                    }
                    Turn("r");
                    Turn("r");
                }
                else if ((faces[1, 0, 1] == colour && faces[2, 2, 1] == "w") || (faces[1, 1, 0] == colour && faces[5, 2, 1] == "w") || (faces[1, 2, 1] == colour && faces[3, 2, 1] == "w") || (faces[1, 1, 2] == colour && faces[4, 2, 1] == "w"))//piece is on yellow layer, white not facing yellow
                {
                    while (!(faces[1, 1, 0] == colour && faces[5, 2, 1] == "w"))
                    {
                        Turn("y");
                    }
                    Turn("g'");
                    Turn("r");
                    Turn("g");
                }
                Turn("w");

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

        static void WhiteCorners()
        {
            for (int colourNum = 0; colourNum < 4; colourNum++)
            {
                string colour1 = " ";
                string colour2 = " ";
                if (colourNum == 0)
                {
                    colour1 = "r";
                    colour2 = "b";
                }
                else if (colourNum == 1)
                {
                    colour1 = "b";
                    colour2 = "o";
                }
                else if (colourNum == 2)
                {
                    colour1 = "o";
                    colour2 = "g";
                }
                else if (colourNum == 3)
                {
                    colour1 = "g";
                    colour2 = "r";
                }

                if ((faces[5, 2, 2] == "w" && faces[1, 0, 0] == colour1 && faces[2, 2, 0] == colour2) || (faces[2, 2, 2] == "w" && faces[1, 0, 2] == colour1 && faces[4, 2, 0] == colour2) || (faces[4, 2, 2] == "w" && faces[1, 2, 2] == colour1 && faces[3, 2, 0] == colour2) || (faces[3, 2, 2] == "w" && faces[1, 2, 0] == colour1 && faces[5, 2, 0] == colour2))//piece is on the yellow layer with white facing left
                {
                    while (!(faces[5, 2, 2] == "w" && faces[1, 0, 0] == colour1 && faces[2, 2, 0] == colour2))
                    {
                        Turn("y");
                    }
                    Turn("b'");
                    Turn("y");
                    Turn("b");
                }
                else if ((faces[5, 2, 2] == colour1 && faces[2, 2, 0] == "w" && faces[1, 0, 0] == colour2) || (faces[2, 2, 2] == colour1 && faces[4, 2, 0] == "w" && faces[1, 0, 2] == colour2) || (faces[4, 2, 2] == colour1 && faces[3, 2, 0] == "w" && faces[1, 2, 2] == colour2) || (faces[3, 2, 2] == colour1 && faces[5, 2, 0] == "w" && faces[1, 2, 0] == colour2))//piece is on the yellow layer with white facing right
                {

                    while (!(faces[4, 2, 2] == colour1 && faces[3, 2, 0] == "w" && faces[1, 2, 2] == colour2))
                    {
                        Turn("y");
                    }
                    Turn("r");
                    Turn("y'");
                    Turn("r'");
                }
                else if ((faces[1, 0, 0] == "w" && faces[2, 2, 0] == colour1 && faces[5, 2, 2] == colour2) || (faces[1, 0, 2] == "w" && faces[4, 2, 0] == colour1 && faces[2, 2, 2] == colour2) || (faces[1, 2, 2] == "w" && faces[3, 2, 0] == colour1 && faces[4, 2, 2] == colour2) || (faces[1, 2, 0] == "w" && faces[5, 2, 0] == colour1 && faces[3, 2, 2] == colour2))//piece is on the yellow layer with white facing bottom
                {
                    while (!(faces[1, 0, 2] == "w" && faces[4, 2, 0] == colour1 && faces[2, 2, 2] == colour2))
                    {
                        Turn("y");
                    }
                    Turn("b'");
                    Turn("y");
                    Turn("b");
                    Turn("y'");
                    Turn("r");
                    Turn("y'");
                    Turn("r'");
                }
                else if ((faces[2, 0, 2] == colour1 && faces[0, 0, 0] == "w" && faces[4, 0, 0] == colour2) || (faces[5, 0, 2] == colour1 && faces[0, 0, 2] == "w" && faces[2, 0, 0] == colour2) || (faces[3, 0, 2] == colour1 && faces[0, 2, 2] == "w" && faces[5, 0, 0] == colour2) || (faces[4, 0, 2] == colour1 && faces[0, 2, 0] == "w" && faces[3, 0, 0] == colour2))//piece is on the white with white facing up
                {
                    int num = 0;
                    while (!(faces[2, 0, 2] == colour1 && faces[0, 0, 0] == "w" && faces[4, 0, 0] == colour2))
                    {
                        num++;
                        Turn("w");
                    }
                    Turn("b'");
                    Turn("y'");
                    Turn("b");
                    if (num > 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            Turn("w'");
                        }
                    }
                    Turn("b'");
                    Turn("y");
                    Turn("b");
                }
                else if ((faces[2, 0, 2] == "w" && faces[4, 0, 0] == colour1 && faces[0, 0, 0] == colour2) || (faces[5, 0, 2] == "w" && faces[2, 0, 0] == colour1 && faces[0, 0, 2] == colour2) || (faces[3, 0, 2] == "w" && faces[5, 0, 0] == colour1 && faces[0, 2, 2] == colour2) || (faces[4, 0, 2] == "w" && faces[3, 0, 0] == colour1) && faces[0, 2, 0] == colour2)//piece is on the white with white facing left
                {
                    int num = 0;
                    while (!(faces[2, 0, 2] == "w" && faces[4, 0, 0] == colour1 && faces[0, 0, 0] == colour2))
                    {
                        num++;
                        Turn("w");
                    }
                    Turn("b'");
                    Turn("y");
                    Turn("b");
                    if (num > 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            Turn("w'");
                        }
                    }
                    Turn("y'");
                    Turn("b'");
                    Turn("y");
                    Turn("b");
                }
                else if ((faces[0, 0, 0] == colour1 && faces[4, 0, 0] == "w" && faces[2, 0, 2] == colour2) || (faces[5, 0, 2] == colour2 && faces[0, 0, 2] == colour1 && faces[2, 0, 0] == "w") || (faces[3, 0, 2] == colour2 && faces[0, 2, 2] == colour1 && faces[5, 0, 0] == "w") || (faces[4, 0, 2] == colour2 && faces[0, 2, 0] == colour1 && faces[3, 0, 0] == "w"))//piece is on the white with white facing right
                {
                    int num = 0;
                    while (!(faces[0, 0, 0] == colour1 && faces[4, 0, 0] == "w" && faces[2, 0, 2] == colour2))
                    {
                        num++;
                        Turn("w");
                    }
                    Turn("b'");
                    Turn("y'");
                    Turn("b");
                    if (num > 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            Turn("w'");
                        }
                    }
                    Turn("y");
                    Turn("y");
                    Turn("r");
                    Turn("y'");
                    Turn("r'");
                }

                Turn("w");
            }
        }
        static void SecondLayerEdges()
        {
            bool again = true;

            bool rbDone = false, boDone = false, ogDone = false, grDone = false;

            while (again)
            {
                bool pieceInserted = false;

                for (int colourNum = 0; colourNum < 4; colourNum++)
                {
                    int sideNum1 = 0;
                    int sideNum2 = 0;
                    int yellowPos1 = 0;
                    int yellowPos2 = 0;
                    int yellowPos3 = 0;
                    int yellowPos4 = 0;
                    string colour1 = " ";
                    string colour2 = " ";
                    bool colourDone = false;
                    if (colourNum == 0)
                    {
                        colourDone = rbDone;
                        colour1 = "r";
                        sideNum1 = 2;
                        colour2 = "b";
                        sideNum2 = 4;
                        yellowPos1 = 1;
                        yellowPos2 = 2;
                        yellowPos3 = 0;
                        yellowPos4 = 1;
                    }
                    else if (colourNum == 1)
                    {
                        colourDone = boDone;
                        colour1 = "b";
                        sideNum1 = 4;
                        colour2 = "o";
                        sideNum2 = 3;
                        yellowPos1 = 2;
                        yellowPos2 = 1;
                        yellowPos3 = 1;
                        yellowPos4 = 2;
                    }
                    else if (colourNum == 2)
                    {
                        colourDone = ogDone;
                        colour1 = "o";
                        sideNum1 = 3;
                        colour2 = "g";
                        sideNum2 = 5;
                        yellowPos1 = 1;
                        yellowPos2 = 0;
                        yellowPos3 = 2;
                        yellowPos4 = 1;
                    }
                    else if (colourNum == 3)
                    {
                        colourDone = grDone;
                        colour1 = "g";
                        sideNum1 = 5;
                        colour2 = "r";
                        sideNum2 = 2;
                        yellowPos1 = 0;
                        yellowPos2 = 1;
                        yellowPos3 = 1;
                        yellowPos4 = 0;
                    }
                    if (!colourDone)
                    {
                        if ((faces[1, 0, 1] == colour1 && faces[2, 2, 1] == colour2) || (faces[1, 1, 2] == colour1 && faces[4, 2, 1] == colour2) || (faces[1, 2, 1] == colour1 && faces[3, 2, 1] == colour2) || (faces[1, 1, 0] == colour1 && faces[5, 2, 1] == colour2))//face is on the bottom layer with colour1 facing down
                        {
                            while (!((faces[sideNum2, 2, 1] == colour2 && faces[1, yellowPos1, yellowPos2] == colour1)))
                            {
                                Turn("y");
                            }
                            Turn(colour2);
                            Turn("y");
                            Turn(colour2);
                            Turn("y");
                            Turn(colour2);
                            Turn("y'");
                            Turn(colour2 + "'");
                            Turn("y'");
                            Turn(colour2 + "'");
                        }
                        else if ((faces[1, 0, 1] == colour2 && faces[2, 2, 1] == colour1) || (faces[1, 1, 2] == colour2 && faces[4, 2, 1] == colour1) || (faces[1, 2, 1] == colour2 && faces[3, 2, 1] == colour1) || (faces[1, 1, 0] == colour2 && faces[5, 2, 1] == colour1))//face is on bottom with colour2 facing down
                        {
                            while (!(faces[sideNum1, 2, 1] == colour1 && faces[1, yellowPos3, yellowPos4] == colour2))
                            {
                                Turn("y");
                            }
                            Turn(colour1 + "'");
                            Turn("y'");
                            Turn(colour1 + "'");
                            Turn("y'");
                            Turn(colour1 + "'");
                            Turn("y");
                            Turn(colour1);
                            Turn("y");
                            Turn(colour1);
                        }
                        
                    }
                }
                if ((!(rbDone && boDone && ogDone && grDone)) && pieceInserted)//if all of the pieces are NOT inserted, and a piece was inserted last time
                    again = true;//run  code again
                if (!(rbDone && boDone && ogDone && grDone))//if all of the pieces are NOT inserted (no piece was inserted last time)
                {
                    for (int colourNum = 0; colourNum < 4; colourNum++)
                    {
                        bool colourDone = false;
                        int sideNum1 = 0;
                        int sideNum2 = 0;
                        int yellowPos1 = 0;
                        int yellowPos2 = 0;
                        int yellowPos3 = 0;
                        int yellowPos4 = 0;
                        string colour1 = " ";
                        string colour2 = " ";
                        if (colourNum == 0)
                        {
                            colourDone = rbDone;
                            colour1 = "r";
                            sideNum1 = 2;
                            colour2 = "b";
                            sideNum2 = 4;
                            yellowPos1 = 1;
                            yellowPos2 = 2;
                            yellowPos3 = 0;
                            yellowPos4 = 1;
                        }
                        else if (colourNum == 1)
                        {
                            colourDone = boDone;
                            colour1 = "b";
                            sideNum1 = 4;
                            colour2 = "o";
                            sideNum2 = 3;
                            yellowPos1 = 2;
                            yellowPos2 = 1;
                            yellowPos3 = 1;
                            yellowPos4 = 2;
                        }
                        else if (colourNum == 2)
                        {
                            colourDone = ogDone;
                            colour1 = "o";
                            sideNum1 = 3;
                            colour2 = "g";
                            sideNum2 = 5;
                            yellowPos1 = 1;
                            yellowPos2 = 0;
                            yellowPos3 = 2;
                            yellowPos4 = 1;
                        }
                        else if (colourNum == 3)
                        {
                            colourDone = grDone;
                            colour1 = "g";
                            sideNum1 = 5;
                            colour2 = "r";
                            sideNum2 = 2;
                            yellowPos1 = 0;
                            yellowPos2 = 1;
                            yellowPos3 = 1;
                            yellowPos4 = 0;
                        }
                        if (!colourDone)
                        {
                            if (faces[2, 1, 2] == colour2 && faces[4, 1, 0] == colour1)//piece between red and blue, colour1 facing blue
                            {
                                Turn("b");
                                Turn("y");
                                Turn("b");
                                Turn("y");
                                Turn("b");
                                Turn("y'");
                                Turn("b'");
                                Turn("y'");
                                Turn("b'");

                                while (!(faces[sideNum1, 2, 1] == colour1 && faces[1, yellowPos3, yellowPos4] == colour2))
                                {
                                    Turn("y");
                                }

                                Turn(colour1 + "'");
                                Turn("y'");
                                Turn(colour1 + "'");
                                Turn("y'");
                                Turn(colour1 + "'");
                                Turn("y");
                                Turn(colour1);
                                Turn("y");
                                Turn(colour1);
                                colourDone = true;
                            }
                            if (faces[4, 1, 2] == colour2 && faces[3, 1, 0] == colour1)//piece between blue and orange, colour1 facing orange
                            {
                                Turn("o");
                                Turn("y");
                                Turn("o");
                                Turn("y");
                                Turn("o");
                                Turn("y'");
                                Turn("o'");
                                Turn("y'");
                                Turn("o'");

                                while (!(faces[sideNum1, 2, 1] == colour1 && faces[1, yellowPos3, yellowPos4] == colour2))
                                {
                                    Turn("y");
                                }

                                Turn(colour1 + "'");
                                Turn("y'");
                                Turn(colour1 + "'");
                                Turn("y'");
                                Turn(colour1 + "'");
                                Turn("y");
                                Turn(colour1);
                                Turn("y");
                                Turn(colour1);
                                colourDone = true;
                            }
                            if (faces[3, 1, 2] == colour2 && faces[5, 1, 0] == colour1)//piece between green and orange, colour1 facing green
                            {
                                Turn("g");
                                Turn("y");
                                Turn("g");
                                Turn("y");
                                Turn("g");
                                Turn("y'");
                                Turn("g'");
                                Turn("y'");
                                Turn("g'");

                                while (!(faces[sideNum1, 2, 1] == colour1 && faces[1, yellowPos3, yellowPos4] == colour2))
                                {
                                    Turn("y");
                                }

                                Turn(colour1 + "'");
                                Turn("y'");
                                Turn(colour1 + "'");
                                Turn("y'");
                                Turn(colour1 + "'");
                                Turn("y");
                                Turn(colour1);
                                Turn("y");
                                Turn(colour1);
                                colourDone = true;
                            }
                            if (faces[5, 1, 2] == colour2 && faces[2, 1, 0] == colour1)//piece between green and red, colour1 facing red
                            {
                                Turn("r");
                                Turn("y");
                                Turn("r");
                                Turn("y");
                                Turn("r");
                                Turn("y'");
                                Turn("r'");
                                Turn("y'");
                                Turn("r'");

                                while (!(faces[sideNum1, 2, 1] == colour1 && faces[1, yellowPos3, yellowPos4] == colour2))
                                {
                                    Turn("y");
                                }

                                Turn(colour1 + "'");
                                Turn("y'");
                                Turn(colour1 + "'");
                                Turn("y'");
                                Turn(colour1 + "'");
                                Turn("y");
                                Turn(colour1);
                                Turn("y");
                                Turn(colour1);
                                colourDone = true;
                            }


                            if (faces[2, 1, 2] == colour1 && faces[4, 1, 0] == colour2)//piece between red and blue, colour1 facing red
                            {
                                Turn("b");
                                Turn("y");
                                Turn("b");
                                Turn("y");
                                Turn("b");
                                Turn("y'");
                                Turn("b'");
                                Turn("y'");
                                Turn("b'");

                                while (!(faces[sideNum2, 2, 1] == colour2 && faces[1, yellowPos1, yellowPos2] == colour1))
                                {
                                    Turn("y");
                                }

                                Turn(colour2);
                                Turn("y");
                                Turn(colour2);
                                Turn("y");
                                Turn(colour2);
                                Turn("y'");
                                Turn(colour2 + "'");
                                Turn("y'");
                                Turn(colour2 + "'");
                                colourDone = true;
                            }
                            if (faces[4, 1, 2] == colour1 && faces[3, 1, 0] == colour2)//piece between blue and orange, colour1 facing blue
                            {
                                Turn("o");
                                Turn("y");
                                Turn("o");
                                Turn("y");
                                Turn("o");
                                Turn("y'");
                                Turn("o'");
                                Turn("y'");
                                Turn("o'");

                                while (!(faces[sideNum2, 2, 1] == colour2 && faces[1, yellowPos1, yellowPos2] == colour1))
                                {
                                    Turn("y");
                                }

                                Turn(colour2);
                                Turn("y");
                                Turn(colour2);
                                Turn("y");
                                Turn(colour2);
                                Turn("y'");
                                Turn(colour2 + "'");
                                Turn("y'");
                                Turn(colour2 + "'");
                                colourDone = true;
                            }
                            if (faces[3, 1, 2] == colour1 && faces[5, 1, 0] == colour2)//piece between green and orange, colour1 facing orange
                            {
                                Turn("g");
                                Turn("y");
                                Turn("g");
                                Turn("y");
                                Turn("g");
                                Turn("y'");
                                Turn("g'");
                                Turn("y'");
                                Turn("g'");

                                while (!(faces[sideNum2, 2, 1] == colour2 && faces[1, yellowPos1, yellowPos2] == colour1))
                                {
                                    Turn("y");
                                }

                                Turn(colour2);
                                Turn("y");
                                Turn(colour2);
                                Turn("y");
                                Turn(colour2);
                                Turn("y'");
                                Turn(colour2 + "'");
                                Turn("y'");
                                Turn(colour2 + "'");
                                colourDone = true;
                            }
                            if (faces[5, 1, 2] == colour1 && faces[2, 1, 0] == colour2)//piece between green and red, colour1 facing green
                            {
                                Turn("r");
                                Turn("y");
                                Turn("r");
                                Turn("y");
                                Turn("r");
                                Turn("y'");
                                Turn("r'");
                                Turn("y'");
                                Turn("r'");

                                while (!(faces[sideNum2, 2, 1] == colour2 && faces[1, yellowPos1, yellowPos2] == colour1))
                                {
                                    Turn("y");
                                }

                                Turn(colour2);
                                Turn("y");
                                Turn(colour2);
                                Turn("y");
                                Turn(colour2);
                                Turn("y'");
                                Turn(colour2 + "'");
                                Turn("y'");
                                Turn(colour2 + "'");
                                colourDone = true;
                            }
                        }
                    }
                }
                if (faces[2, 1, 2] == "r" && faces[4, 1, 0] == "b")
                    rbDone = true;
                if (faces[4, 1, 2] == "b" && faces[3, 1, 0] == "o")
                    boDone = true;
                if (faces[3, 1, 2] == "o" && faces[5, 1, 0] == "g")
                    ogDone = true;
                if (faces[2, 1, 0] == "r" && faces[5, 1, 2] == "g")
                    grDone = true;

                if (rbDone && boDone && ogDone && grDone)
                    again = false;
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
                while(!(faces[1, 0, 1] != "y" && faces[1, 1, 2] == "y" && faces[1, 2, 1] == "y" && faces[1, 1, 0] != "y"))
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
            if ((faces[1, 2, 0] == "y" && faces[3, 2, 0] == "y" && faces[4, 2, 0] == "y" && faces[2, 2, 0] == "y")|| (faces[1, 0, 0] == "y" && faces[3, 2, 0] == "y" && faces[5, 2, 0] == "y" && faces[4, 2, 0] == "y")|| (faces[1, 0, 2] == "y" && faces[3, 2, 0] == "y" && faces[2, 2, 0] == "y" && faces[5, 2, 0] == "y")||(faces[1, 2, 2] == "y" && faces[4, 2, 0] == "y" && faces[5, 2, 0] == "y" && faces[2, 2, 0] == "y"))//Sune
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
            {;
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
            if (faces [2,2,2] != faces[2,2,0] && faces[3, 2, 2] != faces[3, 2, 0] && faces[4, 2, 2] != faces[4, 2, 0] && faces[5, 2, 2] != faces[5, 2, 0])//swap diagnal corners
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
                if (faces[3,2,1] == faces[3,2,2] && faces[2,2,2] == faces[2, 2, 1])
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
    }
}
