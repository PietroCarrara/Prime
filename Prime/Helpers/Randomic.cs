namespace Prime
{
	public static class Randomic
	{
		private static System.Random r = new System.Random();

		public static bool Chanche(int chance)
		{
			return  r.Next(100) < chance;
		}

		private static int Rand(int max)
		{
			return r.Next(max);
		}
	}
}
