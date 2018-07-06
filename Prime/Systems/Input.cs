using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Prime
{
	public static class Input
	{
		private static KeyboardState 	kbdState = Keyboard.GetState(),
								prevKbdState;

		private static MouseState 	mouse = Mouse.GetState(),
									prevMouse;

		internal static void Update()
		{
			prevKbdState = kbdState;
			kbdState = Keyboard.GetState();

			prevMouse = mouse;
			mouse = Mouse.GetState();
		}

		public static bool HasFocus()
		{
			return PrimeGame.Game.IsActive;
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

		public static bool IsButtonDown(MouseButtons b)
		{
			ButtonState btState;

			switch(b)
			{
				case MouseButtons.Middle:
					btState = mouse.MiddleButton;
					break;
				case MouseButtons.Left:
					btState = mouse.LeftButton;
					break;
				case MouseButtons.Right:
					btState = mouse.RightButton;
					break;
				default:
					return false;
			}

			return btState == ButtonState.Pressed;
		}

		public static bool IsButtonUp(MouseButtons b)
		{
			ButtonState btState;

			switch(b)
			{
				case MouseButtons.Middle:
					btState = mouse.MiddleButton;
					break;
				case MouseButtons.Left:
					btState = mouse.LeftButton;
					break;
				case MouseButtons.Right:
					btState = mouse.RightButton;
					break;
				default:
					return false;
			}

			return btState == ButtonState.Released;
		}

		public static bool IsButtonPressed(MouseButtons b)
		{
			ButtonState btState, prevBtState;

			switch(b)
			{
				case MouseButtons.Middle:
					btState = mouse.MiddleButton;
					prevBtState = prevMouse.MiddleButton;
					break;
				case MouseButtons.Left:
					btState = mouse.LeftButton;
					prevBtState = prevMouse.LeftButton;
					break;
				case MouseButtons.Right:
					btState = mouse.RightButton;
					prevBtState = prevMouse.RightButton;
					break;
				default:
					return false;
			}

			return btState == ButtonState.Pressed && prevBtState == ButtonState.Released;
		}

		public static bool IsButtonReleased(MouseButtons b)
		{
			ButtonState btState, prevBtState;

			switch(b)
			{
				case MouseButtons.Middle:
					btState = mouse.MiddleButton;
					prevBtState = prevMouse.MiddleButton;
					break;
				case MouseButtons.Left:
					btState = mouse.LeftButton;
					prevBtState = prevMouse.LeftButton;
					break;
				case MouseButtons.Right:
					btState = mouse.RightButton;
					prevBtState = prevMouse.RightButton;
					break;
				default:
					return false;
			}

			return btState == ButtonState.Released && prevBtState == ButtonState.Pressed;
		}

		public static Vector2 MousePosition(Camera c)
		{
			
			return c.Camera2D.ViewportAdapter.PointToScreen(mouse.Position).ToVector2() + c.Position - new Vector2(1280 / 2f, 720 / 2f);
		}
	}

	public enum MouseButtons
	{
		Middle,
		Left,
		Right
	}
}
