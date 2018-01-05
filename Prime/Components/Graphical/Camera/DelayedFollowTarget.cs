using Microsoft.Xna.Framework;

namespace Prime
{
	public class DelayedFollowTarget : Component
	{
		public Entity Target;

		public float Speed;

		public DelayedFollowTarget(Entity e, float speed)
		{
			this.Target = e;

			this.Speed = speed;
		}
		
		public override void Update()
		{
			base.Update();
			
			var motion = Vector2.Zero;
				
			var dist = Target.Position - this.Owner.Position;

			this.Owner.Position.X += Speed * Time.DetlaTime * dist.X;
			this.Owner.Position.Y += Speed * Time.DetlaTime * dist.Y;
		}
	}
}
