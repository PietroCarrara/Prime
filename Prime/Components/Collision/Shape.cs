using System;

namespace Prime
{
	public abstract class Shape : Component
	{
		public abstract bool CollidesWith(Shape s);

		public Action<Entity> OnCollisionEnter;

		public Action<Entity> OnCollisionExit;
	}
}
