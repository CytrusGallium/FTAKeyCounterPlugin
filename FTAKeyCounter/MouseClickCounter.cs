using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace FTAKeyCounter
{
    internal class MouseClickCounter
    {
        public static bool IsCounting => clickCountThread.IsAlive;

        // for different Virtual-Keys view list:
        // https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        const Int32 LEFT_MOUSE_BUTTON = 0x01;
        const Int32 RIGHT_MOUSE_BUTTON = 0x02;

        private static bool waitingLeftUp = false;
        private static bool waitingRightUp = false;
        private static int leftClickCounter = 0;
        private static int rightClickCounter = 0;
        private static Thread clickCountThread;

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int VirtualKeyPressed);

        public static void UpdateClickCounters()
        {
            if (GetAsyncKeyState(LEFT_MOUSE_BUTTON) == 0)
            {
                if (waitingLeftUp)
                {
                    //Console.WriteLine("Left Click !");
                    leftClickCounter++;
                    waitingLeftUp = false;
                }
            }
            else
            {
                waitingLeftUp = true;
            }

            if (GetAsyncKeyState(RIGHT_MOUSE_BUTTON) == 0)
            {
                if (waitingRightUp)
                {
                    //Console.WriteLine("Right Click !");
                    rightClickCounter++;
                    waitingRightUp = false;
                }
            }
            else
            {
                waitingRightUp = true;
            }
        }

        /// <summary>
        /// Starts a thread that counts left and right mouse clicks.
        /// </summary>
        public static void StartMouseClickCounter()
        {
            clickCountThread = new Thread(ClickCountThread);
            clickCountThread.Start();
        }

        private static void ClickCountThread()
        {
            while(true)
            {
                UpdateClickCounters();
            }
        }

        /// <summary>
        /// Returns how many times the left mouse button was clicked and resets the counter to zero.
        /// </summary>
        public static int ConsumeLeftClickCount()
        {
            int tmp = leftClickCounter;
            leftClickCounter = 0;
            return tmp;
        }

        /// <summary>
        /// Returns how many times the right mouse button was clicked and resets the counter to zero.
        /// </summary>
        public static int ConsumeRightClickCount()
        {
            int tmp = rightClickCounter;
            rightClickCounter = 0;
            return tmp;
        }
    }
}