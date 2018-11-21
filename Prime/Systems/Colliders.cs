using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Prime
{
	public static class Colliders
	{
		private static List<Shape> colliders = new List<Shape>();

		public static void Register(Shape s)
		{
			colliders.Add(s);
		}

		public static void Unregister(Shape s)
		{
			colliders.Remove(s);
		}

		internal static void Update()
		{
			int i = 1;

			foreach (var shape in colliders)
			{
				foreach (var s in colliders.Skip(i))
				{
					var info = shape.CollisionInfo(s);
					if (info != null)
					{
						doCollision(s, shape, info);

						info.MinimumPenetration *= -1;

						doCollision(shape, s, info);
					}
					else
					{
						checkOther(shape, s);
						checkOther(s, shape);
					}
				}
				i++;
			}
		}

		private static void doCollision(Shape s1, Shape s2, CollisionResult info)
		{
			if (!s1.IsCollidingWith.Contains(s2))
			{
				s1.OnCollisionEnter?.Invoke(s2, info);

				s1.IsCollidingWith.Add(s2);
			}

			s1.OnCollision?.Invoke(s2, info);
		}

		private static void checkOther(Shape s1, Shape s2)
		{
			if (s1.IsCollidingWith.Any())
			{
				if (s1.IsCollidingWith.Contains(s2))
				{
					s1.OnCollisionExit?.Invoke(s2);

					s1.IsCollidingWith.Remove(s2);
				}
			}
		}
	}
}
