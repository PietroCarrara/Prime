using Microsoft.Xna.Framework;

namespace Prime.Helpers
{
	public static class Vector2Extensions
	{
		public static float DistanceBetween(this Vector2 p1, Vector2 p2)
		{
			return (float)System.Math.Sqrt(System.Math.Pow(p1.X - p2.X, 2) + System.Math.Pow(p1.Y - p2.Y, 2));
		}

		public static float AngleBetween(this Vector2 p1, Vector2 p2)
		{
			return (float)System.Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
		}

		public static Vector2 Abs(this Vector2 self)
		{
			return new Vector2
			{
				X = System.Math.Abs(self.X),
				Y = System.Math.Abs(self.Y)
			};
		}

	}
}
