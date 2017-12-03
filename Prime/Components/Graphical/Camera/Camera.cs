using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Prime
{
	public class Camera : Camera2D
	{
		public Camera(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {  }

        public Camera(ViewportAdapter viewportAdapter) : base(viewportAdapter)
        {  }
	}
}
