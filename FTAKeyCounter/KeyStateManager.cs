using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FTAKeyCounter
{
	public class KeyStateManager
	{

		[DllImport( "user32.dll" )]
		static extern short GetAsyncKeyState( int VirtualKeyPressed );
		//key state cache system
		private static Dictionary<int, bool> cachedKeyStates = new Dictionary<int, bool>( );
		/// <summary>
		/// Set a key's state based on it's int value and whether it's pressed or not.
		/// Add a new state for the key if we don't have a profile for it.
		/// </summary>
		/// <param name="button"></param>
		/// <param name="state"></param>
		public static void SetCachedKeyState( int button, bool state )
		{
			if ( cachedKeyStates.ContainsKey( button ) )
				cachedKeyStates[ button ] = state;
			else
				cachedKeyStates.Add( button, state );
		}
		/// <summary>
		/// Get the current state of a key. 
		/// If we do not yet know what state a key is in assume it's not pressed.
		/// </summary>
		/// <param name="key"></param>
		public static bool GetCachedKeyState( int key )
		{
			return cachedKeyStates.ContainsKey( key ) && cachedKeyStates[ key ];
		}

		/// <summary>
		/// insert a int button, and a function for what you want to do in the case the button is pressed
		/// second parameter is used like this "() => count++"
		/// </summary>
		/// <param name="button"></param>
		/// <param name="OnMouseDown"></param>
		public static void CheckKeyChange( int button, Action OnMouseDown )
		{

			var rawState = GetAsyncKeyState( button );
			bool pressed = rawState != 0;
			bool mouseDown = GetCachedKeyState( button );
			if ( pressed != mouseDown )
			{
				SetCachedKeyState( button, pressed );
				if ( pressed && OnMouseDown != null )
					OnMouseDown.Invoke( );
			}
		}
	}
}
