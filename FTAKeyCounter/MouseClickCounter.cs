using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using FocusTimeAccumulator.Features.Pool;

namespace FTAKeyCounter
{
    public class MouseClickCounter
    {
        public static bool IsCounting => clickCountThread.IsAlive;
		// for different Virtual-Keys view list:
		// https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
		const Int32 LEFT_MOUSE_BUTTON = 0x01;
        const Int32 RIGHT_MOUSE_BUTTON = 0x02;
        
        const Int32 KEY_CHECK_PERIOD = 16; // 16 Milliseconds is 62 FPS approx.

        private static int leftClickCount, rightClickCount;
        private static Thread clickCountThread = new Thread(ClickCountThread);


        //simpler key change check code
        public static void UpdateClickCounters()
        {
            KeyStateManager.CheckKeyChange( LEFT_MOUSE_BUTTON, ( ) => leftClickCount++ );
			KeyStateManager.CheckKeyChange( RIGHT_MOUSE_BUTTON, ( ) => rightClickCount++ );
        }
        /// <summary>
        /// Starts a thread that counts left and right mouse clicks.
        /// </summary>
        public static void StartMouseClickCounter()
        {
            clickCountThread.Start();
        }

        /// <summary>
        /// Stop mouse click counter thread.
        /// </summary>
        public static void StopMouseClickCounter()
        {
            clickCountThread.Abort();
        }
        private static void ClickCountThread()
        {
            while(true)
            {
                UpdateClickCounters();
                Thread.Sleep(KEY_CHECK_PERIOD);
            }
        }
		/// <summary>
		/// Returns how many times the left mouse button was clicked and resets the counter to zero.
		/// </summary>
		public static int ConsumeLeftClickCount( )
        {
            var left = leftClickCount;
            leftClickCount = 0;
            return left;
        }
		/// <summary>
		/// Returns how many times the left mouse button was clicked and resets the counter to zero.
		/// </summary>
		public static int ConsumeRightClickCount( )
		{
			var right = rightClickCount;
			rightClickCount = 0;
			return right;
		}
	}
}