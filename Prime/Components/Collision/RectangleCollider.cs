using System;
using Microsoft.Xna.Framework;

namespace Prime
{
	public class RectangleCollider : Shape
	{
		public float Width, Height;

		public Vector2 Origin;

		public Vector2 Position
		{
			get
			{
				return this.Owner.Position + Origin;
			}
		}

		public RectangleCollider(float w, float h)
		{
			this.Width = w;
			this.Height = h;

			this.Origin.X = -Width / 2;
			this.Origin.Y = -Height / 2;
		}

		public override bool CollidesWith(Shape s)
		{
			if (s is RectangleCollider)
			{
				var r = (RectangleCollider) s;

				return CollidesWith(r);
			}
			else
			{
				return 	inRange(s.Owner.Position.X, this.Position.X, this.Position.X + Width) &&
					 	inRange(s.Owner.Position.Y, this.Position.Y, this.Position.Y + Height);
			}
		}

		public bool CollidesWith(RectangleCollider r)
		{
			return rangeOverlap(this.Position.X, this.Position.X  + Width,
								r.Position.X, r.Position.X + r.Width)
					&&
					rangeOverlap(this.Position.Y, this.Position.Y + Height,
								r.Position.Y, r.Position.Y + r.Height);
		}

		bool rangeOverlap(float a1, float a2, float b1, float b2)
		{
			return Math.Max(a1, a2) >= Math.Min(b1, b2) &&
				   Math.Min(a1, a2) <= Math.Max(b1, b2);
		}
		
		bool inRange(float check, float val1, float val2)
		{
			return check <= Math.Min(val1, val2) && check >= Math.Max(val1, val2);
		}
	}
}
