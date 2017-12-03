using Microsoft.Xna.Framework;

namespace Prime
{
	public static class Time
	{
		internal static GameTime GameTime
		{
			set
			{
				dt = (float) value.ElapsedGameTime.TotalSeconds;
			}
		}

		private static float dt;

		public static float DetlaTime
		{
			get
			{
				return dt;
			}
		}
	}
}
