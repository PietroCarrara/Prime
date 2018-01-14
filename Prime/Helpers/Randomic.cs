namespace Prime
{
	public static class Randomic
	{
		private static System.Random r = new System.Random();

		public static bool Chanche(int chance)
		{
			return r.Next(100) < chance;
		}

		public static int Rand(int max)
		{
			return r.Next(max);
		}
	}

	public static class RandomArrayExtensions
	{
		public static T Rand<T>(this T[] self)
		{
			return self[Randomic.Rand(self.Length)];
		}
	}
}
