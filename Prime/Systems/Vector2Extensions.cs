using System;
using Microsoft.Xna.Framework;

public static class Vector2Extensions
{
	public static float DistanceBetween(this Vector2 p1, Vector2 p2)
	{
		return (float) Math.Sqrt( Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) );
	}

	public static float AngleBetween(this Vector2 p1, Vector2 p2)
	{
		return (float) Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
	}
}
