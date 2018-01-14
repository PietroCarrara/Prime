using System;
using Microsoft.Xna.Framework;

namespace Prime
{
	public class RectangleCollider : Shape
	{
		public float Width, Height;

		public Vector2 Origin;

		public override Vector2 Position
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

		public override CollisionResult DoCollision(Shape s)
		{
			var r = s as RectangleCollider;
			if(r != null)
				return DoCollision(r);

			throw new NotImplementedException();
		}

		public CollisionResult DoCollision(RectangleCollider r)
		{
			var cr = new CollisionResult();

			var top = Position.Y - Height / 2;
			var bottom = Position.Y + Height / 2;
			var left = Position.X - Width / 2;
			var right = Position.X + Width / 2;

			var top2 = r.Position.Y - r.Height / 2;
			var bottom2 = r.Position.Y + r.Height / 2;
			var left2 = r.Position.X - r.Width / 2;
			var right2 = r.Position.X + r.Width / 2;

			top = Math.Abs(top - top2);
			bottom = Math.Abs(bottom - bottom2);
			left = Math.Abs(left - left2);
			right = Math.Abs(right - right2);

			var vals = new float[]{top, bottom, left, right};

			int index = 0;
			for(int i = 1; i < vals.Length; i++)
			{
				if(vals[i] < vals[index])
					index = i;
			}

			switch(index)
			{
				case 1:
					cr.Direction = Direction.Up;
					break;
				case 2:
					cr.Direction = Direction.Down;
					break;
				case 3:
					cr.Direction = Direction.Left;
					break;
				case 4:
					cr.Direction = Direction.Right;
					break;
			}

			return cr;
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
