using Prime;
using Microsoft.Xna.Framework;

namespace Prime
{
	public class UIEntity : Entity
	{
		public new Vector2 Position {get; set;}

		public override void Update()
		{
			base.Update();

			base.Position = this.Scene.Cam.Position + Position - new Vector2(1280, 720) / 2f;
		}
	}
}
