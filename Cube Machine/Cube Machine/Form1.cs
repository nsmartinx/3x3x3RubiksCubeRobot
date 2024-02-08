using Pololu.UsbWrapper;
using Pololu.Usc;
using System.Diagnostics;
using System.Threading;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube_Machine
{
    public partial class Form1 : Form
    {
        static List<String> moves = new List<string>();//This is the list of moves that the computer generates
        static String[,,] faces = new String[6, 3, 3];//This is the 3 dimensional array that the cube is storred in
        static string[,,] facesCopy = new string[6, 3, 3];//The computer will sometime copy the cube state to this in order to test moves without changing the actual cube

        int Servo0Pos0 = 1100;
        int Servo0Pos1 = 400;
        int Servo1Pos0 = 1300;
        int Servo1Pos1 = 600;
        int Servo2Pos0 = 1300;
        int Servo2Pos1 = 600;
        int Servo3Pos0 = 1300;
        int Servo3Pos1 = 530;

        int Servo8Pos0 = 590;
        int Servo8Pos1 = 1560;
        int Servo8Pos2 = 2510;
        int Servo9Pos0 = 590;
        int Servo9Pos1 = 1530;
        int Servo9Pos2 = 2510;
        int Servo10Pos0 = 590;
        int Servo10Pos1 = 1560;
        int Servo10Pos2 = 2510;
        int Servo11Pos0 = 590;
        int Servo11Pos1 = 1560;
        int Servo11Pos2 = 2510;

        int orient0 = 2;//colour facing servo0 (0 = white, 1 = yellow, 2 = red, 3 = orange, 4 = blue, 5 = green)
        int orient1 = 4;//"" servo1
        int orient2 = 3;//"" servo2
        int orient3 = 5;//"" servo3
        int orient4 = 0;//"" front
        int orient5 = 1;//"" back

        string[] colour = new string[9];
        int[] xPos = new int[9];
        int[] yPos = new int[9];

        int shortDelay = 300;
        int longDelay = 500;

        //Servos 0-4 are the rack and pinion ones that move the 4 sides in and out, numbers start in bottom right, going counter-clockwise. Pos 0 is retracted, Pos 1 is Extended
        //Servos 8-11 are the claw ones that turn the cube. Numbering is same as above. Pos 1 is neutral.

        //CW stands for clock wise (CW0 is a clockwise turn of side 0), CCW stands for counter clockwise, R stands for Rotate (R02 is a rotation using servos 0 and 2).
        public Form1()
        {
            InitializeComponent();
        }

        private void TurnSide(string side)
        {
            int colour = 0;
            if (side == "w")
                colour = 0;
            if (side == "y")
                colour = 1;
            if (side == "r")
                colour = 2;
            if (side == "o")
                colour = 3;
            if (side == "b")
                colour = 4;
            if (side == "g")
                colour = 5;

            if (side == "w'")
                colour = 0 + 6;
            if (side == "y'")
                colour = 1 + 6;
            if (side == "r'")
                colour = 2 + 6;
            if (side == "o'")
                colour = 3 + 6;
            if (side == "b'")
                colour = 4 + 6;
            if (side == "g'")
                colour = 5 + 6;

            if (orient0 == colour)
                CW0();
            if (orient1 == colour)
                CW1();
            if (orient2 == colour)
                CW2();
            if (orient3 == colour)
                CW3();
            if (orient4 == colour)
            {
                R02();
                CW3();
            }
            if (orient5 == colour)
            {
                R13();
                CW2();
            }

            if (orient0 == colour - 6)
                CCW0();
            if (orient1 == colour - 6)
                CCW1();
            if (orient2 == colour - 6)
                CCW2();
            if (orient3 == colour - 6)
                CCW3();
            if (orient4 == colour - 6)
            {
                R02();
                CCW3();
            }
            if (orient5 == colour - 6)
            {
                R13();
                CCW2();
            }
        }

        private void Solve(object sender, EventArgs e)
        {
            orient0 = 2;
            orient1 = 4;
            orient2 = 3;
            orient3 = 5;
            orient4 = 0;
            orient5 = 1;

            List<string> solution = new List<string>();

            solution = FindSolution();

            foreach (string turn in solution)
            {
                TurnSide(turn);
            }

            Thread.Sleep(longDelay);
            MoveServo(0, 0);
            MoveServo(1, 0);
            MoveServo(2, 0);
            MoveServo(3, 0);
        }

        private void DisableAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i < 4)
                {
                    TrySetTarget(Convert.ToByte(i), 0);
                }
                else
                {
                    TrySetTarget(Convert.ToByte(i + 8), 0);
                }
            }
        }

        private void CW0(object sender = null, EventArgs e = null)
        {
            MoveServo(8, 2, 70);
            Thread.Sleep(longDelay);
            MoveServo(8, 2);
            Thread.Sleep(shortDelay);
            MoveServo(0, 0);
            Thread.Sleep(longDelay);
            MoveServo(8, 1);
            Thread.Sleep(longDelay);
            MoveServo(0, 1);
            Thread.Sleep(longDelay);
        }
        private void CCW0(object sender = null, EventArgs e = null)
        {
            MoveServo(8, 0, -70);
            Thread.Sleep(longDelay);
            MoveServo(8, 0);
            Thread.Sleep(shortDelay);
            MoveServo(0, 0);
            Thread.Sleep(longDelay);
            MoveServo(8, 1);
            Thread.Sleep(longDelay);
            MoveServo(0, 1);
            Thread.Sleep(longDelay);
        }
        private void CW1(object sender = null, EventArgs e = null)
        {
            MoveServo(9, 2, 70);
            Thread.Sleep(longDelay);
            MoveServo(9, 2);
            Thread.Sleep(shortDelay);
            MoveServo(1, 0);
            Thread.Sleep(longDelay);
            MoveServo(9, 1);
            Thread.Sleep(longDelay);
            MoveServo(1, 1);
            Thread.Sleep(longDelay);
        }
        private void CCW1(object sender = null, EventArgs e = null)
        {
            MoveServo(9, 0, -70);
            Thread.Sleep(longDelay);
            MoveServo(9, 0);
            Thread.Sleep(shortDelay);
            MoveServo(1, 0);
            Thread.Sleep(longDelay);
            MoveServo(9, 1);
            Thread.Sleep(longDelay);
            MoveServo(1, 1);
            Thread.Sleep(longDelay);
        }
        private void CW2(object sender = null, EventArgs e = null)
        {
            MoveServo(10, 2, 70);
            Thread.Sleep(longDelay);
            MoveServo(10, 2);
            Thread.Sleep(shortDelay);
            MoveServo(2, 0);
            Thread.Sleep(longDelay);
            MoveServo(10, 1);
            Thread.Sleep(longDelay);
            MoveServo(2, 1);
            Thread.Sleep(longDelay);
        }
        private void CCW2(object sender = null, EventArgs e = null)
        {
            MoveServo(10, 0, -70);
            Thread.Sleep(longDelay);
            MoveServo(10, 0);
            Thread.Sleep(shortDelay);
            MoveServo(2, 0);
            Thread.Sleep(longDelay);
            MoveServo(10, 1);
            Thread.Sleep(longDelay);
            MoveServo(2, 1);
            Thread.Sleep(longDelay);
        }
        private void CW3(object sender = null, EventArgs e = null)
        {
            MoveServo(11, 2, 70);
            Thread.Sleep(longDelay);
            MoveServo(11, 2);
            Thread.Sleep(shortDelay);
            MoveServo(3, 0);
            Thread.Sleep(longDelay);
            MoveServo(11, 1);
            Thread.Sleep(longDelay);
            MoveServo(3, 1);
            Thread.Sleep(longDelay);
        }
        private void CCW3(object sender = null, EventArgs e = null)
        {
            MoveServo(11, 0, -70);
            Thread.Sleep(longDelay);
            MoveServo(11, 0);
            Thread.Sleep(shortDelay);
            MoveServo(3, 0);
            Thread.Sleep(longDelay);
            MoveServo(11, 1);
            Thread.Sleep(longDelay);
            MoveServo(3, 1);
            Thread.Sleep(longDelay);
        }

        private void R02(object sender = null, EventArgs e = null)
        {
            int orient = orient4;
            orient4 = orient1;
            orient1 = orient5;
            orient5 = orient3;
            orient3 = orient;

            MoveServo(1, 0);
            MoveServo(3, 0);
            Thread.Sleep(longDelay);
            MoveServo(8, 0);
            MoveServo(10, 2);
            Thread.Sleep(longDelay);
            MoveServo(1, 1);
            MoveServo(3, 1);
            Thread.Sleep(longDelay);
            MoveServo(0, 0);
            MoveServo(2, 0);
            Thread.Sleep(longDelay);
            MoveServo(8, 1);
            MoveServo(10, 1);
            Thread.Sleep(longDelay);
            MoveServo(0, 1);
            MoveServo(2, 1);
            Thread.Sleep(longDelay);
        }
        private void R13(object sender = null, EventArgs e = null)
        {
            int orient = orient4;
            orient4 = orient2;
            orient2 = orient5;
            orient5 = orient0;
            orient0 = orient;

            MoveServo(0, 0);
            MoveServo(2, 0);
            Thread.Sleep(longDelay);
            MoveServo(9, 0);
            MoveServo(11, 2);
            Thread.Sleep(longDelay);
            MoveServo(0, 1);
            MoveServo(2, 1);
            Thread.Sleep(longDelay);
            MoveServo(1, 0);
            MoveServo(3, 0);
            Thread.Sleep(longDelay);
            MoveServo(9, 1);
            MoveServo(11, 1);
            Thread.Sleep(longDelay);
            MoveServo(1, 1);
            MoveServo(3, 1);
            Thread.Sleep(longDelay);
        }

        private void Initialize(object sender, EventArgs e)
        {
            MoveServo(8, 1);
            MoveServo(9, 1);
            MoveServo(10, 1);
            MoveServo(11, 1);
            Thread.Sleep(3000);
            MoveServo(0, 1);
            MoveServo(3, 1);
            MoveServo(1, 1);
            MoveServo(2, 1);
        }

        //Servo0
        private void Servo0Disable_Click(object sender, EventArgs e)
        {
            TrySetTarget(0, 0);
        }

        private void Servo0Pos0_Click(object sender, EventArgs e)
        {
            MoveServo(0, 0);
        }

        private void Servo0Pos1_Click(object sender, EventArgs e)
        {
            MoveServo(0, 1);
        }

        //Servo1
        private void Servo1Disable_Click(object sender, EventArgs e)
        {
            TrySetTarget(1, 0);
        }

        private void Servo1Pos0_Click(object sender, EventArgs e)
        {
            MoveServo(1, 0);
        }

        private void Servo1Pos1_Click(object sender, EventArgs e)
        {
            MoveServo(1, 1);
        }

        //Servo2
        private void Servo2Disable_Click(object sender, EventArgs e)
        {
            TrySetTarget(2, 0);
        }

        private void Servo2Pos0_Click(object sender, EventArgs e)
        {
            MoveServo(2, 0);
        }

        private void Servo2Pos1_Click(object sender, EventArgs e)
        {
            MoveServo(2, 1);
        }

        //Servo3
        private void Servo3Disable_Click(object sender, EventArgs e)
        {
            TrySetTarget(3, 0);
        }

        private void Servo3Pos0_Click(object sender, EventArgs e)
        {
            MoveServo(3, 0);
        }

        private void Servo3Pos1_Click(object sender, EventArgs e)
        {
            MoveServo(3, 1);
        }

        //Claw Servos

        //Servo8
        private void Servo8Disable_Click(object sender, EventArgs e)
        {
            TrySetTarget(8, 0);
        }

        private void Servo8Pos0_Click(object sender, EventArgs e)
        {
            MoveServo(8, 0);
        }

        private void Servo8Pos1_Click(object sender, EventArgs e)
        {
            MoveServo(8, 1);
        }
        private void Servo8Pos2_Click(object sender, EventArgs e)
        {
            MoveServo(8, 2);
        }

        //Servo9
        private void Servo9Disable_Click(object sender, EventArgs e)
        {
            TrySetTarget(9, 0);
        }

        private void Servo9Pos0_Click(object sender, EventArgs e)
        {
            MoveServo(9, 0);
        }

        private void Servo9Pos1_Click(object sender, EventArgs e)
        {
            MoveServo(9, 1);
        }
        private void Servo9Pos2_Click(object sender, EventArgs e)
        {
            MoveServo(9, 2);
        }

        //Servo10
        private void Servo10Disable_Click(object sender, EventArgs e)
        {
            TrySetTarget(10, 0);
        }

        private void Servo10Pos0_Click(object sender, EventArgs e)
        {
            MoveServo(10, 0);
        }

        private void Servo10Pos1_Click(object sender, EventArgs e)
        {
            MoveServo(10, 1);
        }
        private void Servo10Pos2_Click(object sender, EventArgs e)
        {
            MoveServo(10, 2);
        }

        //Servo11
        private void Servo11Disable_Click(object sender, EventArgs e)
        {
            TrySetTarget(11, 0);
        }

        private void Servo11Pos0_Click(object sender, EventArgs e)
        {
            MoveServo(11, 0);
        }

        private void Servo11Pos1_Click(object sender, EventArgs e)
        {
            MoveServo(11, 1);
        }
        private void Servo11Pos2_Click(object sender, EventArgs e)
        {
            MoveServo(11, 2);
        }

        private void MoveServo(int servo, int pos, int add = 0)
        {
            if (servo == 0)
            {
                if (pos == 0)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo0Pos0 + add) * 4));
                }
                if (pos == 1)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo0Pos1 + add) * 4));
                }
            }
            if (servo == 1)
            {
                if (pos == 0)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo1Pos0 + add) * 4));
                }
                if (pos == 1)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo1Pos1 + add) * 4));
                }
            }
            if (servo == 2)
            {
                if (pos == 0)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo2Pos0 + add) * 4));
                }
                if (pos == 1)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo2Pos1 + add) * 4));
                }
            }
            if (servo == 3)
            {
                if (pos == 0)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo3Pos0 + add) * 4));
                }
                if (pos == 1)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo3Pos1 + add) * 4));
                }
            }
            if (servo == 8)
            {
                if (pos == 0)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo8Pos0 + add) * 4));
                }
                if (pos == 1)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo8Pos1 + add) * 4));
                }
                if (pos == 2)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo8Pos2 + add) * 4));
                }
            }
            if (servo == 9)
            {
                if (pos == 0)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo9Pos0 + add) * 4));
                }
                if (pos == 1)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo9Pos1 + add) * 4));
                }
                if (pos == 2)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo9Pos2 + add) * 4));
                }
            }
            if (servo == 10)
            {
                if (pos == 0)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo10Pos0 + add) * 4));
                }
                if (pos == 1)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo10Pos1 + add) * 4));
                }
                if (pos == 2)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo10Pos2 + add) * 4));
                }
            }
            if (servo == 11)
            {
                if (pos == 0)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo11Pos0 + add) * 4));
                }
                if (pos == 1)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo11Pos1 + add) * 4));
                }
                if (pos == 2)
                {
                    TrySetTarget(Convert.ToByte(servo), Convert.ToUInt16((Servo11Pos2 + add) * 4));
                }
            }
        }

        void TrySetTarget(Byte channel, UInt16 target)
        {
            try
            {
                using (Usc device = connectToDevice())  // Find a device and temporarily connect.
                {
                    device.setSpeed(channel, 500);
                    device.setTarget(channel, target);
                    // device.Dispose() is called automatically when the "using" block ends,
                    // allowing other functions and processes to use the device.
                }
            }
            catch (Exception exception)  // Handle exceptions by displaying them to the user.
            {
                displayException(exception);
            }
        }

        /// <summary>
        /// Connects to a Maestro using native USB and returns the Usc object
        /// representing that connection.  When you are done with the
        /// connection, you should close it using the Dispose() method so that
        /// other processes or functions can connect to the device later.  The
        /// "using" statement can do this automatically for you.
        /// </summary>
        Usc connectToDevice()
        {
            // Get a list of all connected devices of this type.
            List<DeviceListItem> connectedDevices = Usc.getConnectedDevices();

            foreach (DeviceListItem dli in connectedDevices)
            {
                // If you have multiple devices connected and want to select a particular
                // device by serial number, you could simply add a line like this:
                //   if (dli.serialNumber != "00012345"){ continue; }

                Usc device = new Usc(dli); // Connect to the device.
                return device;             // Return the device.
            }
            throw new Exception("Could not find device.  Make sure it is plugged in to USB " +
                "and check your Device Manager (Windows) or run lsusb (Linux).");
        }
        /// <summary>
        /// Displays an exception to the user by popping up a message box.
        /// </summary>
        void displayException(Exception exception)
        {
            StringBuilder stringBuilder = new StringBuilder();
            do
            {
                stringBuilder.Append(exception.Message + "  ");
                if (exception is Win32Exception)
                {
                    stringBuilder.Append("Error code 0x" + ((Win32Exception)exception).NativeErrorCode.ToString("x") + ".  ");
                }
                exception = exception.InnerException;
            }
            while (exception != null);
            MessageBox.Show(stringBuilder.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        List<String> FindSolution()
        {
            /*
            faces[0, 0, 0] = "g";
            faces[0, 0, 1] = "b";
            faces[0, 0, 2] = "g";
            faces[0, 1, 0] = "r";
            faces[0, 1, 1] = "w";
            faces[0, 1, 2] = "r";
            faces[0, 2, 0] = "g";
            faces[0, 2, 1] = "g";
            faces[0, 2, 2] = "r";

            faces[1, 0, 0] = "b";
            faces[1, 0, 1] = "g";
            faces[1, 0, 2] = "o";
            faces[1, 1, 0] = "y";
            faces[1, 1, 1] = "y";
            faces[1, 1, 2] = "w";
            faces[1, 2, 0] = "y";
            faces[1, 2, 1] = "g";
            faces[1, 2, 2] = "y";

            faces[2, 0, 0] = "w";
            faces[2, 0, 1] = "o";
            faces[2, 0, 2] = "o";
            faces[2, 1, 0] = "b";
            faces[2, 1, 1] = "r";
            faces[2, 1, 2] = "y";
            faces[2, 2, 0] = "w";
            faces[2, 2, 1] = "r";
            faces[2, 2, 2] = "b";

            faces[3, 0, 0] = "o";
            faces[3, 0, 1] = "w";
            faces[3, 0, 2] = "g";
            faces[3, 1, 0] = "b";
            faces[3, 1, 1] = "o";
            faces[3, 1, 2] = "r";
            faces[3, 2, 0] = "b";
            faces[3, 2, 1] = "o";
            faces[3, 2, 2] = "b";

            faces[4, 0, 0] = "y";
            faces[4, 0, 1] = "w";
            faces[4, 0, 2] = "w";
            faces[4, 1, 0] = "o";
            faces[4, 1, 1] = "b";
            faces[4, 1, 2] = "y";
            faces[4, 2, 0] = "w";
            faces[4, 2, 1] = "o";
            faces[4, 2, 2] = "r";

            faces[5, 0, 0] = "y";
            faces[5, 0, 1] = "b";
            faces[5, 0, 2] = "r";
            faces[5, 1, 0] = "y";
            faces[5, 1, 1] = "g";
            faces[5, 1, 2] = "w";
            faces[5, 2, 0] = "o";
            faces[5, 2, 1] = "g";
            faces[5, 2, 2] = "r";
            */


            WhiteCross();//Solves the white cross on the cube. Gets all of the white edges into the correct position, orineted correctly
            F2L();//Solves the first two layers of the cube. Gets all of the white corners and remaining, non-yellow edges into the correct place, oriented correctly
            OLLEdges();//Orientation of last layer edges. Orients all of the yellow edges correctly, but does not worry about position
            OLLCorners();//Orientation of last layer corners. Orients all of the yellow corners correctly, but does not worry about position
            PLLCorners();//Position the last layer corners. Puts the yellow corners into the correct position and maintains the orientation
            PLLEdges();//Position the last layer edges. Puts the yellow edges into the correct position and maintains the orientation
            TurnLastLayer();//Turns the final yellow layer untill the cube is solved

            shortenMoveSequence();//goes through the sequence of moves generated by the computer and takes out unnecesary moves (e.g. if there is w then w', it will remove them)

            Debug.Write("Moves: ");//prints the solution that the computer came up with
            foreach (var move in moves)
            {
                Debug.Write(move + " ");

            }

            Debug.Write("\n\n");//prints the cube state (should be solved)
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        Debug.Write(faces[x, y, z]);
                        if (z == 2)
                            Debug.Write("\n");
                        if (y == 2 && z == 2)
                            Debug.Write("\n");
                    }
                }
            }

            return moves;
        }

        static void shortenMoveSequence()
        {
            Debug.Write("\n\n\n");
            int moveCount = 0;
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
                        moves.RemoveAt(moveCount);//removes both the x and the x'
                        moves.RemoveAt(moveCount - 1);
                        wasModified = true;
                        if (moveCount >= 2)
                            lastMove2 = lastMove1;
                        if (moveCount >= 1)
                            lastMove1 = move;
                        break;
                    }
                    if (move == lastMove1 && move == lastMove2)//if there were three of the same moves in a row
                    {
                        //determines which move had 2 duplicates and replaces it with the opposite (clockwise instead of counter-clockwise)
                        if (moves[moveCount] == "w")
                            moves[moveCount] = "w'";
                        else if (moves[moveCount] == "y")
                            moves[moveCount] = "y'";
                        else if (moves[moveCount] == "r")
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

                        moves.RemoveAt(moveCount - 1);//removes the other two duplicate moves
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
            } while (wasModified);//will keep repeating untill it did not modify the move sequence
        }
        static void WhiteCross()
        {
            Array.Copy(faces, facesCopy, 54); //Copys the cube to a seperate 3 dimensional array


            int[] crossMoves = new int[6];//creates an array

            int completedWhiteSides = 0;//variable for how many white cross pieces have been solved
            int countedWhiteSides = 0;//variable for how many of the solved white pieces have been saved

            if (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r")//checks how many of the white pieces are solved
                completedWhiteSides++;//increases the number of solved pieces by one
            if (facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b")
                completedWhiteSides++;
            if (facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g")
                completedWhiteSides++;
            if (facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o")
                completedWhiteSides++;
            while (completedWhiteSides < 4)//while the white cross has not been finished
            {
                Array.Copy(faces, facesCopy, 54);//Resets the copy of the cube

                crossMoves[0]++;//tries every possibility of 1 move sequences, then 2 moves sequences e.t.c up to a max of 6 moves

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
                if (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r")//determines if the white edge is in place
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

                if (completedWhiteSides > countedWhiteSides)//if a new edge was put in place from that sequence
                {
                    TurnSequence(crossMoves, false);//apply moves to actual cube
                    foreach (var move in crossMoves)//prints the moves to solve it
                    {
                        Debug.Write(move + " ");
                    }
                    Debug.Write("\n");
                    crossMoves = new int[6];//clears move array
                    countedWhiteSides = completedWhiteSides;//saves the number of solved white pieces


                }
            }
        }
        static void F2L()
        {
            //In F2L (First 2 Layers), the white corners are paired up with the coresponding middle edge and inserted at the same time


            bool allowedSpecialCase = false;//"special cases" are when the white edge and the corner are next to each other, but not oriented correctly. These take more moves to solve so can not be brute forced by the computer

            int[] f2LMoves = new int[7];//creates an array to store the move sequence

            Array.Copy(faces, facesCopy, 54);//copies the cube

            int pairsCounted = 0;//how many corner/edge rpairs have been inserted and counted

            int pairsInserted = 0;//how many corner/edge piars are in place
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

            f2LMoves[0]++;//goes through every possible sequence of 1 move, then 2, up to 7

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


                foreach (var item in f2LMoves)//prints the move sequence
                {
                    Debug.Write(item + " ");
                }

                if (f2LMoves[4] > 0)//if all possibilities of 5 move sequences have been tried without creating a pair that can be inserted
                {
                    allowedSpecialCase = true;//allows the computer to use algorithms to solve the special case
                    f2LMoves = new int[7];//clears move array

                }
                Debug.Write(allowedSpecialCase);//prints whether or not the computer can use special cases

                TurnSequence2(f2LMoves, true);//apply moves to copy of cubes

                pairsInserted = 0;//determines how many pairs are in place
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

                if (facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o")//if the white cross is still complete (F2L must preserve the white cross)
                {
                    if (pairsInserted >= pairsCounted)
                    {
                        //when the corner and edges are next to each other with edge to right of corner
                        if (facesCopy[3, 2, 0] == "w" && facesCopy[4, 2, 1] == facesCopy[4, 2, 2] && facesCopy[1, 1, 2] == facesCopy[1, 2, 2])//corner and edge are paired next to each other, edge to right of corner, the corner that has white on it is facing orange
                        {
                            if (facesCopy[4, 2, 1] == "r")//determines which pair it is and follows an algorithm to insert it
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
                        if (facesCopy[5, 2, 0] == "w" && facesCopy[3, 2, 1] == facesCopy[3, 2, 2] && facesCopy[1, 2, 0] == facesCopy[1, 2, 1])//white tile facing green
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
                        if (facesCopy[2, 2, 0] == "w" && facesCopy[5, 2, 1] == facesCopy[5, 2, 2] && facesCopy[1, 0, 0] == facesCopy[1, 1, 0])//white tile facing red
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
                        if (facesCopy[4, 2, 0] == "w" && facesCopy[2, 2, 1] == facesCopy[2, 2, 2] && facesCopy[1, 0, 1] == facesCopy[1, 0, 2])//white tile facing blue
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

                        //when the corner and edges are next to each other with edge to left of corner
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

                        //When the corner and the edge are seperate from each other with the edge to the left of corner
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

                        //When the corner and the edge are seperate from each other with the edge to the right of corner
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

                pairsInserted = 0;//determines how many pairs have been inserted
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

                if (allowedSpecialCase && pairsInserted >= pairsCounted)//will only run if it is allowed to use special cases
                {
                    if ((facesCopy[0, 0, 1] == "w" && facesCopy[2, 0, 1] == "r" && facesCopy[0, 1, 0] == "w" && facesCopy[4, 0, 1] == "b" && facesCopy[0, 1, 2] == "w" && facesCopy[5, 0, 1] == "g" && facesCopy[0, 2, 1] == "w" && facesCopy[3, 0, 1] == "o"))//special cases below. If it reaches them it follows a series of moves in order to changes it to a normal state
                    {

                        if ((facesCopy[3, 2, 2] == "w" && facesCopy[3, 2, 1] == facesCopy[1, 2, 0] && facesCopy[1, 2, 1] == facesCopy[5, 2, 0]) || (facesCopy[5, 2, 2] == "w" && facesCopy[5, 2, 1] == facesCopy[1, 0, 0] && facesCopy[1, 1, 0] == facesCopy[2, 2, 0]) || (facesCopy[2, 2, 2] == "w" && facesCopy[2, 2, 1] == facesCopy[1, 0, 2] && facesCopy[1, 0, 1] == facesCopy[4, 2, 0]) || (facesCopy[4, 2, 2] == "w" && facesCopy[4, 2, 1] == facesCopy[1, 2, 2] && facesCopy[1, 1, 2] == facesCopy[3, 2, 0]))
                        {
                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int[7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[5, 2, 2] == "w" && facesCopy[5, 2, 1] == facesCopy[1, 0, 0] && facesCopy[1, 1, 0] == facesCopy[2, 2, 0])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 5;//r
                                f2LMoves[1] = 4;//y'
                                f2LMoves[2] = 6;//r'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[3, 2, 2] == "w" && facesCopy[3, 2, 1] == facesCopy[1, 2, 0] && facesCopy[1, 2, 1] == facesCopy[5, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 11;//g
                                f2LMoves[1] = 4;//y'
                                f2LMoves[2] = 1;//g'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[2, 2, 2] == "w" && facesCopy[2, 2, 1] == facesCopy[1, 0, 2] && facesCopy[1, 0, 1] == facesCopy[4, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 9;//b
                                f2LMoves[1] = 4;//y'
                                f2LMoves[2] = 10;//b'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[4, 2, 2] == "w" && facesCopy[4, 2, 1] == facesCopy[1, 2, 2] && facesCopy[1, 1, 2] == facesCopy[3, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 7;//o
                                f2LMoves[1] = 4;//y'
                                f2LMoves[2] = 8;//o'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }

                        if ((facesCopy[3, 2, 0] == "w" && facesCopy[3, 2, 1] == facesCopy[4, 2, 2] && facesCopy[1, 2, 1] == facesCopy[1, 2, 2]) || (facesCopy[5, 2, 0] == "w" && facesCopy[5, 2, 1] == facesCopy[3, 2, 2] && facesCopy[1, 1, 0] == facesCopy[1, 2, 0]) || (facesCopy[2, 2, 0] == "w" && facesCopy[2, 2, 1] == facesCopy[5, 2, 2] && facesCopy[1, 0, 1] == facesCopy[1, 0, 0]) || (facesCopy[4, 2, 0] == "w" && facesCopy[4, 2, 1] == facesCopy[2, 2, 2] && facesCopy[1, 1, 2] == facesCopy[1, 0, 2]))
                        {
                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int[7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[4, 2, 0] == "w" && facesCopy[4, 2, 1] == facesCopy[2, 2, 2] && facesCopy[1, 1, 2] == facesCopy[1, 0, 2])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
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
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[2, 2, 0] == "w" && facesCopy[2, 2, 1] == facesCopy[5, 2, 2] && facesCopy[1, 0, 1] == facesCopy[1, 0, 0])))/////////////////////////////
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
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
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[3, 2, 0] == "w" && facesCopy[3, 2, 1] == facesCopy[4, 2, 2] && facesCopy[1, 2, 1] == facesCopy[1, 2, 2])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
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
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[5, 2, 0] == "w" && facesCopy[5, 2, 1] == facesCopy[3, 2, 2] && facesCopy[1, 1, 0] == facesCopy[1, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
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
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }

                        if ((facesCopy[3, 2, 2] == "w" && facesCopy[5, 2, 0] == facesCopy[1, 1, 0] && facesCopy[5, 2, 1] == facesCopy[1, 2, 0]) || (facesCopy[5, 2, 2] == "w" && facesCopy[2, 2, 0] == facesCopy[1, 0, 1] && facesCopy[2, 2, 1] == facesCopy[1, 0, 0]) || (facesCopy[2, 2, 2] == "w" && facesCopy[4, 2, 0] == facesCopy[1, 1, 2] && facesCopy[4, 2, 1] == facesCopy[1, 0, 2]) || (facesCopy[4, 2, 2] == "w" && facesCopy[3, 2, 0] == facesCopy[1, 2, 1] && facesCopy[3, 2, 1] == facesCopy[1, 2, 2]))
                        {
                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int[7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[4, 2, 2] == "w" && facesCopy[3, 2, 0] == facesCopy[1, 2, 1] && facesCopy[3, 2, 1] == facesCopy[1, 2, 2])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 10;//b'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 9;//b
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[2, 2, 2] == "w" && facesCopy[4, 2, 0] == facesCopy[1, 1, 2] && facesCopy[4, 2, 1] == facesCopy[1, 0, 2])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 6;//r'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 5;//r
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[3, 2, 2] == "w" && facesCopy[5, 2, 0] == facesCopy[1, 1, 0] && facesCopy[5, 2, 1] == facesCopy[1, 2, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 8;//o'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 7;//o
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[5, 2, 2] == "w" && facesCopy[2, 2, 0] == facesCopy[1, 0, 1] && facesCopy[2, 2, 1] == facesCopy[1, 0, 0])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 1;//g'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 11;//g
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }

                        if ((facesCopy[1, 2, 0] == "w" && facesCopy[1, 1, 0] == facesCopy[3, 2, 2] && facesCopy[5, 2, 0] == facesCopy[5, 2, 1]) || (facesCopy[1, 0, 0] == "w" && facesCopy[1, 0, 1] == facesCopy[5, 2, 2] && facesCopy[2, 2, 0] == facesCopy[2, 2, 1]) || (facesCopy[1, 0, 2] == "w" && facesCopy[1, 1, 2] == facesCopy[2, 2, 2] && facesCopy[4, 2, 0] == facesCopy[4, 2, 1]) || (facesCopy[1, 2, 2] == "w" && facesCopy[1, 2, 1] == facesCopy[4, 2, 2] && facesCopy[3, 2, 0] == facesCopy[3, 2, 1]))
                        {
                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int[7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[1, 0, 2] == "w" && facesCopy[1, 1, 2] == facesCopy[2, 2, 2] && facesCopy[4, 2, 0] == facesCopy[4, 2, 1])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 5;//r
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 6;//r'
                                f2LMoves[3] = 10;//b'
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 9;//b
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[1, 0, 0] == "w" && facesCopy[1, 0, 1] == facesCopy[5, 2, 2] && facesCopy[2, 2, 0] == facesCopy[2, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 11;//g
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 1;//g'
                                f2LMoves[3] = 6;//r'
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 5;//r
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[1, 2, 2] == "w" && facesCopy[1, 2, 1] == facesCopy[4, 2, 2] && facesCopy[3, 2, 0] == facesCopy[3, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 9;//b
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 10;//b'
                                f2LMoves[3] = 8;//o'
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 7;//o
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[1, 2, 0] == "w" && facesCopy[1, 1, 0] == facesCopy[3, 2, 2] && facesCopy[5, 2, 0] == facesCopy[5, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 7;//o
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 8;//o'
                                f2LMoves[3] = 1;//g'
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 11;//g
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }

                        if ((facesCopy[1, 2, 0] == "w" && facesCopy[3, 2, 2] == facesCopy[1, 2, 1] && facesCopy[5, 2, 0] == facesCopy[3, 2, 1]) || (facesCopy[1, 0, 0] == "w" && facesCopy[5, 2, 2] == facesCopy[1, 1, 0] && facesCopy[2, 2, 0] == facesCopy[5, 2, 1]) || (facesCopy[1, 0, 2] == "w" && facesCopy[2, 2, 2] == facesCopy[1, 0, 1] && facesCopy[4, 2, 0] == facesCopy[2, 2, 1]) || (facesCopy[1, 2, 2] == "w" && facesCopy[4, 2, 2] == facesCopy[1, 1, 2] && facesCopy[3, 2, 0] == facesCopy[4, 2, 1]))
                        {
                            Debug.Write(facesCopy[1, 0, 2]);
                            Debug.Write(facesCopy[2, 2, 2]);
                            Debug.Write(facesCopy[1, 0, 1]);
                            Debug.Write(facesCopy[4, 2, 0]);
                            Debug.Write(facesCopy[2, 2, 1]);

                            TurnSequence2(f2LMoves, false);
                            TurnSequence(f2LAdditionalMoves, false);
                            f2LMoves = new int[7];//clears move array
                            allowedSpecialCase = false;
                            Array.Copy(faces, facesCopy, 54);

                            if (facesCopy[0, 0, 0] != "w")//r/b
                            {
                                while (!((facesCopy[1, 0, 2] == "w" && facesCopy[2, 2, 2] == facesCopy[1, 0, 1] && facesCopy[4, 2, 0] == facesCopy[2, 2, 1])))
                                {
                                    f2LMoves[0] = 3;//y
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 10;//b'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 9;//b
                                f2LMoves[3] = 5;//r
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 6;//r'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 0, 2] != "w")//r/g
                            {
                                while (!((facesCopy[1, 0, 0] == "w" && facesCopy[5, 2, 2] == facesCopy[1, 1, 0] && facesCopy[2, 2, 0] == facesCopy[5, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 6;//r'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 5;//r
                                f2LMoves[3] = 11;//g
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 1;//g'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 0] != "w")//b/o
                            {
                                while (!((facesCopy[1, 2, 2] == "w" && facesCopy[4, 2, 2] == facesCopy[1, 1, 2] && facesCopy[3, 2, 0] == facesCopy[4, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 8;//o'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 7;//o
                                f2LMoves[3] = 9;//b
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 10;//b'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                            else if (facesCopy[0, 2, 2] != "w")//o/g
                            {
                                while (!((facesCopy[1, 2, 0] == "w" && facesCopy[3, 2, 2] == facesCopy[1, 2, 1] && facesCopy[5, 2, 0] == facesCopy[3, 2, 1])))
                                {
                                    f2LMoves[0] = 3;
                                    PrintCubeState();
                                    TurnSequence2(f2LMoves, false);
                                    f2LMoves = new int[7];//clears move array
                                    Array.Copy(faces, facesCopy, 54);
                                }
                                f2LMoves[0] = 1;//g'
                                f2LMoves[1] = 2;//y2
                                f2LMoves[2] = 11;//g
                                f2LMoves[3] = 7;//o
                                f2LMoves[4] = 3;//y
                                f2LMoves[5] = 8;//o'
                                TurnSequence2(f2LMoves, false);
                                f2LMoves = new int[7];//clears move array
                                Array.Copy(faces, facesCopy, 54);
                            }
                        }
                    }
                }


                TurnSequence(f2LAdditionalMoves, true);//performes the move sequence to the copy of trhe cube


                pairsInserted = 0;//checks how many pairs have been inserted
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

                if (pairsInserted > pairsCounted)//if a new pair was inserted
                {
                    TurnSequence2(f2LMoves, false);//performs the moves to the real cube
                    TurnSequence(f2LAdditionalMoves, false);
                    pairsCounted = pairsInserted;//counts all inserted pairs
                    f2LMoves = new int[7];//clears move array
                    allowedSpecialCase = false;
                }
                Debug.Write("  " + pairsInserted + pairsCounted);
                Debug.Write("\n\n\n");
            }
        }
        static void TurnSequence(int[] moves, bool isCopy)
        {
            //This function is called from the white cross and F2L functions. It applies the sequence of moves to the copy or the real cube
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
            //Performs the same function as the first turn sequence, but only has a limited subset of moves (as some cases do not require certain moves)
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
        {//performs same function as the other turn sequence but from a string list instead of int array
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
        static void PrintCubeState()//prints the current state of the cube and the moves executed so far
        {
            Debug.Write("\n\n");

            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        Debug.Write(faces[x, y, z]);
                        if (z == 2)
                            Debug.Write("\n");
                        if (y == 2 && z == 2)
                            Debug.Write("\n");
                        //Debug.Write("[{0}, {1}, {2}]: {3}\n", x, y, z, faces[x, y, z]);
                    }
                }
            }
            Debug.Write("Moves: ");
            foreach (var move in moves)
            {
                Debug.Write(move + " ");

            }
        }
        static void OLLEdges()
        {
            //For the OLL edges, there are 4 possible situations, each one has its own algorithm to solve. This function finds which of the situations the cube is in and applies the respective algorithm
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
            //For the OLL Corners, there are 7 possible situations, each one has its own algorithm to solve. This function finds which of the situations the cube is in and applies the respective algorithm.
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
            //For the PLL Corners, there are 2 possible situations (that aren't solved), each one has its own algorithm to solve. This function finds which of the situations the cube is in and applies the respective algorithm.
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
            //For the PLL Corners, there are 4 possible situations (that aren't solved), each one has its own algorithm to solve. This function finds which of the situations the cube is in and applies the respective algorithm.
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
        {//this function rotates the yellow layer untill it is in the right position after the rest of the cube has been solved
            while (faces[3, 2, 1] != "o")
            {
                Turn("y");
            }
        }
        static void Turn(string side)
        {//This function is called when a turn to the cube is made

            if (side == "w")//Determines which turn was made
            {
                ChangeFaces(0, 2, 4, 3, 5, 0, 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, false);//calls another function to turn switch particular colours
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
        {//is called from the Turn function. switches the colours around on the cube to simulate a turn.
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
            //Same as the Turn function, but performs the moves on the copy of the cube
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
        {//Same as the changefaces function but changes the copy of the cube
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

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        private void Start(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[CameraList.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            CameraPicture.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }

        private void findCamera_Click(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
                CameraList.Items.Add(Device.Name);
            CameraList.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void GetColour(object sender = null, EventArgs e = null)
        {
            xPos[0] = 122;
            yPos[0] = 122;

            xPos[1] = 202;
            yPos[1] = 122;

            xPos[2] = 302;
            yPos[2] = 122;

            xPos[3] = 142;
            yPos[3] = 222;

            xPos[4] = 202;
            yPos[4] = 222;

            xPos[5] = 282;
            yPos[5] = 222;

            xPos[6] = 142;
            yPos[6] = 302;

            xPos[7] = 202;
            yPos[7] = 302;

            xPos[8] = 302;
            yPos[8] = 302;

            PictureBox[] colourBox = new PictureBox[9];
            colourBox[0] = ColourBox0;
            colourBox[1] = ColourBox1;
            colourBox[2] = ColourBox2;
            colourBox[3] = ColourBox3;
            colourBox[4] = ColourBox4;
            colourBox[5] = ColourBox5;
            colourBox[6] = ColourBox6;
            colourBox[7] = ColourBox7;
            colourBox[8] = ColourBox8;

            PictureBox[] cubeColour = new PictureBox[9];
            cubeColour[0] = CubeColour0;
            cubeColour[1] = CubeColour1;
            cubeColour[2] = CubeColour2;
            cubeColour[3] = CubeColour3;
            cubeColour[4] = CubeColour4;
            cubeColour[5] = CubeColour5;
            cubeColour[6] = CubeColour6;
            cubeColour[7] = CubeColour7;
            cubeColour[8] = CubeColour8;

            label1.Text = Convert.ToString(((Bitmap)CameraPicture.Image).GetPixel(xPos[0], yPos[0]));

            for (int i = 0; i < 9; i++)
            {
                Color pixel = ((Bitmap)CameraPicture.Image).GetPixel(xPos[i], yPos[i]);
                colourBox[i].BackColor = Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B);
                if (pixel.R > 180 && pixel.G > 180 && pixel.B > 180)//white
                {
                    colour[i] = "w";
                }
                if (pixel.R > 150 && pixel.G <120 && pixel.B < 120)//red
                {
                    colour[i] = "r";
                }
                if (pixel.R > 180 && pixel.G > 100 && pixel.B < 150)//orange
                {
                    colour[i] = "o";
                }
                if (pixel.R < 150 && pixel.G < 200 && pixel.B > 100)//blue
                {
                    colour[i] = "b";
                }
                if (pixel.R < 220 && pixel.G > 100 && pixel.B < 170)//green
                {
                    colour[i] = "g";
                }
                if (pixel.R < 120 && pixel.G < 120 && pixel.B < 120)//yellow
                {
                    colour[i] = "y";
                }

                if (colour[i] == "w")
                {
                    cubeColour[i].BackColor = Color.FromArgb(255, 250, 250, 250);
                }
                if (colour[i] == "y")
                {
                    cubeColour[i].BackColor = Color.FromArgb(255, 250, 250, 20);
                }
                if (colour[i] == "r")
                {
                    cubeColour[i].BackColor = Color.FromArgb(255, 250, 20, 20);
                }
                if (colour[i] == "o")
                {
                    cubeColour[i].BackColor = Color.FromArgb(255, 250, 130, 20);
                }
                if (colour[i] == "b")
                {
                    cubeColour[i].BackColor = Color.FromArgb(255, 40, 0, 245);
                }
                if (colour[i] == "g")
                {
                    cubeColour[i].BackColor = Color.FromArgb(255, 0, 135, 30);
                }
            }
        }

        private void ScanCube(object sender, EventArgs e)
        {
            GetColour();
            string centreColour0 = colour[4];
            R02();
            GetColour();
            string centrecolour1 = colour[4];

            if (centreColour0 == "w" && centrecolour1 == "r")
            {
                R13();
                R02();
                R02();
                R02();
            }
            if (centreColour0 == "w" && centrecolour1 == "o")
            {

                R13();
                R13();
                R13();
                R02();
                R02();
                R02();
            }
            if (centreColour0 == "w" && centrecolour1 == "b")
            {
                R02();
                R02();
                R02();
            }
            if (centreColour0 == "w" && centrecolour1 == "g")
            {
                R13();
                R13();
                R02();
                R02();
                R02();
            }

            if (centreColour0 == "y" && centrecolour1 == "r")
            {
                R13();
                R02();
            }
            if (centreColour0 == "y" && centrecolour1 == "o")
            {

                R13();
                R13();
                R13();
                R02();
            }
            if (centreColour0 == "y" && centrecolour1 == "b")
            {
                R13();
                R13();
                R02();
            }
            if (centreColour0 == "y" && centrecolour1 == "g")
            {
                R02();
            }

            if (centreColour0 == "r" && centrecolour1 == "w")
            {
                R02();
                R13();
                R13();
                R13();
                R02();
                R02();
                R02();
            }
            if (centreColour0 == "r" && centrecolour1 == "y")
            {

                R02();
                R13();
                R13();
                R13();
                R02();
            }
            if (centreColour0 == "r" && centrecolour1 == "b")
            {
                R02();
                R13();
                R13();
                R13();
                R02();
                R02();
            }
            if (centreColour0 == "r" && centrecolour1 == "g")
            {
                R02();
                R13();
                R13();
                R13();
            }

            if (centreColour0 == "o" && centrecolour1 == "w")
            {
                R02();
                R13();
                R13();
                R13();
                R02();
            }
            if (centreColour0 == "o" && centrecolour1 == "y")
            {

                R02();
                R13();
                R13();
                R13();
                R02();
                R02();
                R02();
            }
            if (centreColour0 == "o" && centrecolour1 == "b")
            {
                R02();
                R13();
                R13();
                R13();
            }
            if (centreColour0 == "o" && centrecolour1 == "g")
            {
                R02();
                R13();
                R13();
                R13();
                R02();
                R02();
            }

            if (centreColour0 == "b" && centrecolour1 == "r")
            {
                R13();
                R02();
                R02();
            }
            if (centreColour0 == "b" && centrecolour1 == "o")
            {

                R13();
                R13();
                R13();
                R02();
                R02();
            }
            if (centreColour0 == "b" && centrecolour1 == "w")
            {
                R02();
                R13();
                R13();
                R02();
                R02();
                R02();
            }
            if (centreColour0 == "b" && centrecolour1 == "y")
            {
                R02();
                R02();
            }

            if (centreColour0 == "g" && centrecolour1 == "r")
            {
                R13();
            }
            if (centreColour0 == "g" && centrecolour1 == "o")
            {

                R13();
                R13();
                R13();
            }
            if (centreColour0 == "g" && centrecolour1 == "w")
            {

            }
            if (centreColour0 == "g" && centrecolour1 == "y")
            {
                R13();
                R13();
            }

            Thread.Sleep(longDelay);

            GetColour();//white
            faces[0, 0, 0] = colour[6];
            faces[0, 0, 1] = colour[3];
            faces[0, 0, 2] = colour[0];
            faces[0, 1, 0] = colour[7];
            faces[0, 1, 1] = colour[4];
            faces[0, 1, 2] = colour[1];
            faces[0, 2, 0] = colour[8];
            faces[0, 2, 1] = colour[5];
            faces[0, 2, 2] = colour[2];

            Thread.Sleep(longDelay);
            R02();
            Thread.Sleep(longDelay);

            GetColour();//blue
            faces[4, 0, 0] = colour[0];
            faces[4, 0, 1] = colour[1];
            faces[4, 0, 2] = colour[2];
            faces[4, 1, 0] = colour[3];
            faces[4, 1, 1] = colour[4];
            faces[4, 1, 2] = colour[5];
            faces[4, 2, 0] = colour[6];
            faces[4, 2, 1] = colour[7];
            faces[4, 2, 2] = colour[8];

            Thread.Sleep(longDelay);
            R13();
            Thread.Sleep(longDelay);

            GetColour();//orange
            faces[3, 0, 0] = colour[0];
            faces[3, 0, 1] = colour[1];
            faces[3, 0, 2] = colour[2];
            faces[3, 1, 0] = colour[3];
            faces[3, 1, 1] = colour[4];
            faces[3, 1, 2] = colour[5];
            faces[3, 2, 0] = colour[6];
            faces[3, 2, 1] = colour[7];
            faces[3, 2, 2] = colour[8];

            Thread.Sleep(longDelay);
            R02();
            Thread.Sleep(longDelay);

            GetColour();//yellow
            faces[1, 0, 0] = colour[8];
            faces[1, 0, 1] = colour[7];
            faces[1, 0, 2] = colour[6];
            faces[1, 1, 0] = colour[5];
            faces[1, 1, 1] = colour[4];
            faces[1, 1, 2] = colour[3];
            faces[1, 2, 0] = colour[2];
            faces[1, 2, 1] = colour[1];
            faces[1, 2, 2] = colour[0];

            Thread.Sleep(longDelay);
            R13();
            Thread.Sleep(longDelay);

            GetColour();//green
            faces[5, 0, 0] = colour[2];
            faces[5, 0, 1] = colour[5];
            faces[5, 0, 2] = colour[8];
            faces[5, 1, 0] = colour[1];
            faces[5, 1, 1] = colour[4];
            faces[5, 1, 2] = colour[7];
            faces[5, 2, 0] = colour[0];
            faces[5, 2, 1] = colour[3];
            faces[5, 2, 2] = colour[6];

            Thread.Sleep(longDelay);
            R02();
            Thread.Sleep(longDelay);

            GetColour();//red
            faces[2, 0, 0] = colour[2];
            faces[2, 0, 1] = colour[5];
            faces[2, 0, 2] = colour[8];
            faces[2, 1, 0] = colour[1];
            faces[2, 1, 1] = colour[4];
            faces[2, 1, 2] = colour[7];
            faces[2, 2, 0] = colour[0];
            faces[2, 2, 1] = colour[3];
            faces[2, 2, 2] = colour[6];

            Thread.Sleep(longDelay);
            R13();

            Debug.Write("\n\n");//prints the cube state
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        Debug.Write(faces[x, y, z]);
                        if (z == 2)
                            Debug.Write("\n");
                        if (y == 2 && z == 2)
                            Debug.Write("\n");
                    }
                }
            }
        }
    }
}
