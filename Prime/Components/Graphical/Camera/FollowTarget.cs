using Microsoft.Xna.Framework;

namespace Prime
{
	public class FollowTarget : Component
	{
		public Entity Target;

		public FollowTarget(Entity e)
		{
			this.Target = e;
		}

		public override void Update()
		{
			base.Update();

			Owner.Position = Target.Position;
		}
	}
}
