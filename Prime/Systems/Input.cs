using Microsoft.Xna.Framework.Input;

namespace Prime
{
	public static class Input
	{
		private static KeyboardState 	kbdState = Keyboard.GetState(),
								prevKbdState;

		internal static void Update()
		{
			prevKbdState = kbdState;
			kbdState = Keyboard.GetState();
		}

		public static bool IsKeyDown(Keys k)
		{
			return kbdState.IsKeyDown(k);
		}

		public static bool IsKeyUp(Keys k)
		{
			return kbdState.IsKeyUp(k);
		}

		// Returns whether or not the key
		// was just pressed
		public static bool IsKeyPressed(Keys k)
		{
			return prevKbdState.IsKeyUp(k) && kbdState.IsKeyDown(k);
		}

		// Returns whether or not the key
		// was just released
		public static bool IsKeyReleased(Keys k)
		{
			return prevKbdState.IsKeyDown(k) && kbdState.IsKeyUp(k);
		}
	}
}
