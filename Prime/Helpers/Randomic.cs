namespace Prime
{
	public static class Randomic
	{
		private static System.Random r = new System.Random();

		public static bool Chanche(int chance)
		{
			return chance > r.Next(100);
		}

		private static int Rand(int max)
		{
			return r.Next(max);
		}
	}
}
