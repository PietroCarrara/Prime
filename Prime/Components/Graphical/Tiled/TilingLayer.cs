using Prime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace Prime
{
	public class TilingLayer : Entity
	{
		private TiledMapRenderer renderer;
		private TiledMapLayer layer;
		private TilingMap parent;

		public TilingLayer(TiledMapRenderer r, TilingMap m, TiledMapLayer l)
		{
			renderer = r;
			layer = l;
			parent = m;
		}

		public override void Draw(SpriteBatch sp)
		{
			base.Draw(sp);
			
			// Create a matrix to scale X and Y, we don't care about Z
			var matrix = Matrix.CreateScale(parent.Scale.X, parent.Scale.Y, 1) * PrimeGame.Game.ViewportAdapter.GetScaleMatrix();

			// Position it according to the camera and the parent position
			var pos = parent.Position - PrimeGame.Game.ActiveScene.Cam.Position;
			matrix *= Matrix.CreateTranslation(pos.X, pos.Y, 1) * PrimeGame.Game.ViewportAdapter.GetScaleMatrix();

			renderer.Draw(layer, matrix);
		}
	}
}

