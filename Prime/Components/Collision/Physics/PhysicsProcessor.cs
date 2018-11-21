using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Prime.Helpers;
using MonoGame.Extended;

namespace Prime.Components.Collision.Physics
{
	public class PhysicsProcessor : Component
	{
		List<Shape> colliders;

		// How much do we bounce back
		public float Elasticity = 1f;

		public float TerminalVelocity = -1f;

		private Vector2 velocity;

		public override void Initialize()
		{
			base.Initialize();

			colliders = this.Owner.GetComponents<Shape>();

			foreach (var c in colliders)
			{
				c.OnCollisionEnter += (s, cr) => onCollisionEnter(c, s, cr);

				Colliders.Register(c);
			}
		}

		public override void Update()
		{
			base.Update();

			this.Owner.Position += velocity * Time.DetlaTime;
		}

		public void ApplyForce(Vector2 force)
		{
			velocity += force;

			// TODO: Apply terminal velocity
			if (TerminalVelocity >= 0 && velocity.DistanceBetween(Vector2.Zero) > TerminalVelocity)
			{
				velocity = velocity.NormalizedCopy() * TerminalVelocity;
			}
		}

		/// <summary>
		/// Applies force pointing to an angle.
		/// </summary>
		/// <param name="force">The force to be applied.</param>
		/// <param name="angle">The angle in radians to apply the force.</param>
		public void ApplyForceAngle(float force, float angle)
		{
			var res = new Vector2();

			res.X = FloatMath.Cos(angle) * force;
			res.Y = FloatMath.Sin(angle) * force;

			this.ApplyForce(res * -1);
		}

		private void onCollisionEnter(Shape self, Shape shape, CollisionResult cr)
		{
			// velocity = cr.BounceVector * velocity * Elasticity;
			this.ApplyForce(cr.BounceVector * velocity * (1 + Elasticity));

			// this.Owner.Position -= cr.MinimumPenetration;
		}

	}
}
