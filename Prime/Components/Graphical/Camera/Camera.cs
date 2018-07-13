using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Camera : Entity
	{
		public Prime.Camera2D Camera2D;

		private RectangleCollider rect;

		public RectangleCollider Bounds
		{
			get
			{
				return rect;
			}
		}

		public override void Initialize()
		{
			this.rect = new RectangleCollider(1280, 720);
			this.Add(this.rect);
		}

		public override void Update()
		{
			base.Update();

 			Camera2D.Position = this.Position;
		}
	}
}

