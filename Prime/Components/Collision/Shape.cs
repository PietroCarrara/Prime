using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Prime.Components.Collision;
using Prime.Helpers;
using System.Linq;
using System.Diagnostics;
using MonoGame.Extended;
using Prime.Components.Collision.Physics;

namespace Prime
{
	public abstract class Shape : Component
	{
		public abstract Vector2 Position { get; }

		protected float Rotation = 0;

		public Action<Shape, CollisionResult> OnCollisionEnter;

		public Action<Shape, CollisionResult> OnCollision;

		public Action<Shape> OnCollisionExit;

		internal List<Shape> IsCollidingWith = new List<Shape>();

		public abstract IReadOnlyList<Vector2> Axes(float angle);

		public abstract IReadOnlyList<Vector2> Vertices { get; }

		public override void OnDestroy()
		{
			base.OnDestroy();

			Colliders.Unregister(this);
		}

		public abstract bool CollidesWith(Entity e);
		public CollisionResult CollisionInfo(Shape s)
		{
			var axes = new List<Vector2>();

			axes.AddRange(this.Axes(this.Rotation));
			axes.AddRange(s.Axes(s.Rotation));

			axes = axes.Select(a => { a.Normalize(); return a; }).ToList();

			// List containing all the axes where there was a collision
			var collisions = new List<Collision>();

			foreach (var axis in axes)
			{
				var dots = this.Vertices.Select(v => v.Dot(axis)).ToArray();
				var dots2 = s.Vertices.Select(v => v.Dot(axis)).ToArray();

				var projs = FloatMath.MinMax(dots);
				var projs2 = FloatMath.MinMax(dots2);

				// If there is a gap, there is no collision
				if (projs.Item2 < projs2.Item1 || projs2.Item2 < projs.Item1)
				{
					return null;
				}
				else
				{
					collisions.Add(new Collision
					{
						CollisionVector = axis,
						dots1 = projs,
						dots2 = projs2
					});
				}
			}

			// List of the minimum movements needed
			// to get out of collision for each axis
			// and the resulting bounce vector 
			var translations = new List<Tuple<Vector2, Vector2>>();
			foreach (var c in collisions)
			{
				var translation = c.CollisionVector * -1;

				var overlap = (c.dots1.Item2 > c.dots2.Item2) ? -(c.dots2.Item2 - c.dots1.Item1) : c.dots1.Item2 - c.dots2.Item1;

				translations.Add(Tuple.Create(
					translation * overlap,
					translation));
			}

			var res = new CollisionResult();

			var tuple = translations.OrderBy(t => t.Item1.Length()).First();

			res.MinimumPenetration = tuple.Item1;
			res.BounceVector = tuple.Item2;

			return res;
		}

		public bool CollidesWith(Shape s)
		{
			return CollisionInfo(s) != null;
		}
	}

	struct Collision
	{
		public Vector2 CollisionVector;
		public Tuple<float, float> dots1, dots2;
	}
}
