using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime.Graphics
{
	public class RectangleSprite : Sprite
	{
		public Color Color = Color.Red;

		private int width, height;

		public RectangleSprite(int w, int h) : base(w, h, new Vector2(w, h) / 2f)
		{
			this.width = w;
			this.height = h;
		}

		public RectangleSprite(int w, int h, Color c) : this(w, h)
		{
			this.Color = c;
		}

		public override void Initialize()
		{
			base.Initialize();

			var tex = new Texture2D(this.Owner.Scene.Game.GraphicsDevice, width, height);

			var color = new Color[width * height];

			for (int i = 0; i < width * height; i++)
			{
				color[i] = this.Color;
			}

			tex.SetData(color);

			base.Tex = tex;
		}
	}
}
