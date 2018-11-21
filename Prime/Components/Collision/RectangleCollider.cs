using System;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prime.Helpers;
using System.Diagnostics;
using Prime.Components.Collision;
using Prime.Components.Collision.Physics;

namespace Prime
{
	public class RectangleCollider : Shape
	{
		public float Width, Height;

		public Vector2 Origin;


		public new float Rotation
		{
			get
			{
				return base.Rotation;
			}
			set
			{
				if (base.Rotation == value)
					return;

				vertices = null;
				base.Rotation = value;
			}
		}

		public override Vector2 Position
		{
			get
			{
				return this.Owner.Position - Origin;
			}
		}

		public RectangleCollider(float w, float h)
		{
			this.Width = w;
			this.Height = h;

			this.Origin.X = Width / 2;
			this.Origin.Y = Height / 2;
		}

		public override IReadOnlyList<Vector2> Axes(float angle)
		{
			return new Vector2[]
			{
				new Vector2(FloatMath.Cos(angle), FloatMath.Sin(angle)),
				new Vector2(FloatMath.Cos(angle + FloatMath.PI/2), FloatMath.Sin(angle + FloatMath.PI/2))
			};
		}

		public override bool CollidesWith(Entity e)
		{
			return CollidesWith(e.Position);
		}

		public bool CollidesWith(Vector2 pos)
		{
			return inRange(pos.X, this.Position.X, this.Position.X + Width) &&
				 	inRange(pos.Y, this.Position.Y, this.Position.Y + Height);
		}

		bool inRange(float check, float val1, float val2)
		{
			return check > System.Math.Min(val1, val2) && check < System.Math.Max(val1, val2);
		}

		private IReadOnlyList<Vector2> vertices;
		public override IReadOnlyList<Vector2> Vertices
		{
			get
			{
				if (vertices != null)
				{
					return vertices.Select((v) => v + this.Owner.Position).ToArray();
				}

				var res = new List<Vector2>();

				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < 2; j++)
					{
						var point = new Vector2(this.Width * j, this.Height * i);

						var radius = Origin.DistanceBetween(point);
						var angle = Origin.AngleBetween(point) + Rotation;

						var pos = new Vector2
						{
							X = FloatMath.Cos(angle) * radius,
							Y = FloatMath.Sin(angle) * radius
						};

						res.Add(pos);
					}
				}

				vertices = res;
				return Vertices;
			}
		}
	}
}
