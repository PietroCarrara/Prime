using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Prime
{
	public abstract class Shape : Component
	{
		public abstract bool CollidesWith(Shape s);

		public abstract CollisionResult DoCollision(Shape s);

		public abstract Vector2 Position{get;}

		public Action<Shape, CollisionResult> OnCollisionEnter;
		
		public Action<Shape, CollisionResult> OnCollision;

		public Action<Shape> OnCollisionExit;

		public Vector2 LastSafePos;

		internal List<Shape> IsCollidingWith = new List<Shape>();

		public override void OnDestroy()
		{
			Colliders.Unregister(this);
		}
	}
}
