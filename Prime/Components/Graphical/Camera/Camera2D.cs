using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Prime
{
	public class Camera2D : MonoGame.Extended.Camera2D
	{
		public ViewportAdapter ViewportAdapter;

		public Camera2D(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {  }

        public Camera2D(ViewportAdapter viewportAdapter) : base(viewportAdapter)
		{
			this.ViewportAdapter = viewportAdapter;
		}

		public new Vector2 Position
		{
			set
			{
				base.Position = value - new Vector2(1280 / 2f, 720 / 2f);
			}
			
			get
			{
				return base.Position + new Vector2(1280 / 2f, 720 / 2f);
			}
		}
	}
}
