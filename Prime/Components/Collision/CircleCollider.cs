using System;
using Microsoft.Xna.Framework;

namespace Prime
{
	public class CircleCollider : Shape
	{
		public float Radius;

		public override Vector2 Position
		{
			get
			{
				return Owner.Position + new Vector2(Radius);
			}
		}

		public override bool CollidesWith(Shape s)
		{
			if (s is CircleCollider)
			{
				var c = (CircleCollider) s;

				return CollidesWith(c);
			}
			else if(s is RectangleCollider)
			{
				throw new NotImplementedException();
			}
			else
			{
				return this.Owner.Position.DistanceBetween(s.Owner.Position) <= this.Radius;
			}
		}

		public bool CollidesWith(CircleCollider c)
		{
			float dist = this.Owner.Position.DistanceBetween(c.Owner.Position);

			return dist <= this.Radius + c.Radius;
		}
		
		public override CollisionResult DoCollision(Shape s)
		{
			throw new NotImplementedException();
		}
	}
}
