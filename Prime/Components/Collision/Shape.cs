using System;
using System.Collections.Generic;

namespace Prime
{
	public abstract class Shape : Component
	{
		public abstract bool CollidesWith(Shape s);

		public Action<Entity> OnCollisionEnter;

		public Action<Entity> OnCollisionExit;

		internal List<Shape> IsCollidingWith = new List<Shape>();
	}
}
