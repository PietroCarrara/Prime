using System.Collections.Generic;
using System.Linq;

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
					if(shape.CollidesWith(s))
					{
       	         		doCollision(shape, s);
						doCollision(s, shape);
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

        private static void doCollision(Shape s1, Shape s2)
        {
			var cr = s1.DoCollision(s2);				

			s1.OnCollision?.Invoke(s2, cr);

            if (!s1.IsCollidingWith.Contains(s2))
            {
				s1.OnCollisionEnter?.Invoke(s2, cr);

            	s1.IsCollidingWith.Add(s2);
            }
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
			else
			{
				s1.LastSafePos = s1.Owner.Position;
			}
		}
    }
}
