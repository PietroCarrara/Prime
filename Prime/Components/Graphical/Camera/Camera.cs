using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Camera : Entity
	{
		public Prime.Camera2D Camera2D;

		public override void Update()
		{
			base.Update();

 			Camera2D.Position = this.Position;
		}
	}
}

