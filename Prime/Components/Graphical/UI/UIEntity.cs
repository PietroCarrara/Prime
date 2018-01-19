using Prime;
using Microsoft.Xna.Framework;

namespace Prime
{
	public class UIEntity : Entity
	{
		private Vector2 position;
		public new Vector2 Position
		{
			get
			{
				return position;
			}
			set
			{
				base.Position = value + this.Scene.Cam.Position;
				this.position = value;
			}
		}

		public override void Update()
		{
			base.Update();

			base.Position = this.Scene.Cam.Position + position;
		}
	}
}
