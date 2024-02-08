using System;
using System.IO;
using System.Text;

namespace Rubik_sCubeV31LLLIndex
{
    internal class Program
    {
        static Piece[] cube = new Piece[20];

        static char[] centres = new char[6];

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

        static int GetRotationCorner(Piece piece)
        {
            int rotation = -1;
            char dominantColour = GetDominantColourCorner(piece);
            if (piece.colour1 == dominantColour) rotation = 0;
            else if (piece.colour2 == dominantColour) rotation = 1;
            else if (piece.colour3 == dominantColour) rotation = 2;

            return rotation;
        }

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
             * Input will be a string[6, 3, 3] for each of the colours on the cube. The first number will be the face of the cube
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

        static void UpdatePositions()
        {
            for (int i = 0; i < 20; i++)
            {
                cube[i].position = i;
            }
        }

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

        static void MoveSequenceInverse(string sequence)
        {
            string[] moves = sequence.Split(' ');
            for (int i = moves.Length; i > 0; i--)
            {
                string move = moves[i - 1];
                if (move.Length > 1)
                {
                    if (move[1] == '\'')
                        move = Char.ToString(move[0]);
                }
                else
                {
                    move = move + '\'';
                }

                Move(move);
            }
        }

        static int GetOLLEdges()
        {
            int state = 0;
            
            state += cube[0].rotation * 1000;
            state += cube[2].rotation * 100;
            state += cube[4].rotation * 10;
            state += cube[6].rotation * 1;

            return state;
        }
        
        static int getOLLCorners()
        {
            int state = 0;

            state += cube[1].rotation * 1000;
            state += cube[3].rotation * 100;
            state += cube[5].rotation * 10;
            state += cube[7].rotation * 1;

            return state;
        }

        static int getPLLEdges()
        {
            int state = 0;
            char colour;

            for (int i = 0; i < 8; i+= 2)
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

        static int getPLLCorners()
        {
            int state = 0;
            char colour;

            for (int i = 1; i < 9; i+= 2)
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

        static void Main(string[] args)
        {
            char[,,] faces = new char[6, 3, 3];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        //Console.Write("[{0}, {1}. {2}]: ", i, k, j);
                        //faces[i, k, j] = Console.ReadLine()[0];
                        if (i == 0)
                            faces[i, j, k] = 'w';
                        else if (i == 1)
                            faces[i, j, k] = 'y';
                        else if (i == 2)
                            faces[i, j, k] = 'r';
                        else if (i == 3)
                            faces[i, j, k] = 'o';
                        else if (i == 4)
                            faces[i, j, k] = 'b';
                        else if (i == 5)
                            faces[i, j, k] = 'g';
                    }
                }
            }

            CreateCube(faces);

            string[] algorithms = File.ReadAllLines("1LLL Algorithms.txt", Encoding.UTF8);


            //MoveSequenceInverse("l U' R2 D R2 U R D' R2 U F2 U' x");
            //MoveSequence("l U' R2 D R2 U R D' R2 U F2 U' x");

            for (int i = 0; i < 20; i++)
            {
                //PrintPiece(cube[i]);
            }

            //*
            int x = 0;
            string indexedAlgs = "";
            foreach (string algorithm in algorithms)
            {
                MoveSequenceInverse(algorithm);
                int ollEdge = GetOLLEdges();
                int ollCorner = getOLLCorners();
                int pllEdge = getPLLEdges();
                int pllCorner = getPLLCorners();

                Console.WriteLine("{0} {1} {2} {3} {4}", ollEdge, ollCorner, pllEdge, pllCorner, algorithm);
                MoveSequence(algorithm);


                string indexedAlg = String.Join(' ', ollEdge, ollCorner, pllEdge, pllCorner, algorithm);
                string indexedAlgs1 = indexedAlgs;
                indexedAlgs = String.Join('\n', indexedAlgs1, indexedAlg);
                File.WriteAllText("1LLL Indexed Algorithms.txt", indexedAlgs);
                x++;
            }
            //*/
            /*
            string algorithm = "L U2 r' F' r U' L2 U2 L U L' U L";
            MoveSequenceInverse(algorithm);
            int ollEdge = GetOLLEdges();
            int ollCorner = getOLLCorners();
            int pllEdge = getPLLEdges();
            int pllCorner = getPLLCorners();

            Console.WriteLine("{0} {1} {2} {3} {4}", ollEdge, ollCorner, pllEdge, pllCorner, algorithm);
            MoveSequence(algorithm);

            for (int i = 0; i < 20; i++)
            {
                PrintPiece(cube[i]);
            }
            */
        }
    }
}
