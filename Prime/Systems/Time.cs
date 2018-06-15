using System;
using Microsoft.Xna.Framework;

namespace Prime
{
	public static class Time
	{
		private static TimeSpan totalGameTime;
		private static TimeSpan deltaTime;

		internal static GameTime GameTime
		{
			set
			{
				deltaTime = value.ElapsedGameTime;
				totalGameTime = value.TotalGameTime;
			}
		}

		public static TimeSpan DeltaGameTime
		{
			get
			{
				return deltaTime;
			}
		}

		public static TimeSpan TotalGameTime
		{
			get
			{
				return totalGameTime;
			}
		}

		public static float DetlaTime
		{
			get
			{
				return (float) deltaTime.TotalSeconds;
			}
		}
	}
}
