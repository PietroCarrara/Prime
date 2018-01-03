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
            int i = 0;

            foreach (var shape in colliders)
            {
                foreach (var s in colliders.Skip(i))
                {
                    if (shape != s)
					{
                        doCollision(shape, s);
						doCollision(s, shape);
                	}
				}

                i++;
            }
        }

        private static void doCollision(Shape s1, Shape s2)
        {
            if (s1.CollidesWith(s2))
            {
                if (!s1.IsCollidingWith.Contains(s2))
                {
					s1.OnCollisionEnter?.Invoke(s2.Owner);

                    s1.IsCollidingWith.Add(s2);
                }

				s1.OnCollisionEnter?.Invoke(s1.Owner);
				s1.OnCollisionExit?.Invoke(s1.Owner);
            }
            else if (s1.IsCollidingWith.Contains(s2))
            {
				s1.OnCollisionExit?.Invoke(s2.Owner);	

                s1.IsCollidingWith.Remove(s2);
            }
        }
    }
}
