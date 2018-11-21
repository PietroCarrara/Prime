using System;
using System.Collections;
using System.Collections.Generic;

namespace Prime.Helpers
{
	public static class FloatMath
	{
		public const float PI = (float)System.Math.PI;

		public static Tuple<float, float> MinMax(IReadOnlyList<float> arr)
		{
			float min, max;
			min = max = arr[0];

			foreach (var i in arr)
			{
				if (i > max) max = i;
				if (i < min) min = i;
			}

			return Tuple.Create(min, max);
		}

		public static float Sin(double rad)
		{
			return (float)System.Math.Sin(rad);
		}

		public static float Tan(double rad)
		{
			return (float)System.Math.Tan(rad);
		}

		public static float Atan(double rad)
		{
			return (float)System.Math.Atan(rad);
		}

		public static float Cos(double rad)
		{
			return (float)System.Math.Cos(rad);
		}

		public static float Pow(double bas, double exp)
		{
			return (float)System.Math.Pow(bas, exp);
		}

		public static float Sqrt(double x)
		{
			return (float)System.Math.Sqrt(x);
		}
	}
}
