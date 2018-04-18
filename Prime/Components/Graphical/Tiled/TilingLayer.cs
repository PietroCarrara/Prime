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
			var matrix = Matrix.CreateScale(parent.Scale.X, parent.Scale.Y, 1);

			// Position it according to the camera and the parent position
			var pos = parent.Position - PrimeGame.Game.ActiveScene.Cam.Position;
			matrix *= Matrix.CreateTranslation(pos.X - parent.Width / 2f, pos.Y - parent.Height / 2f, 1);

			// TODO: Modify this (or justify why does this kind of works)
			matrix.M41 -= parent.Width * 11 / 300f;
			matrix.M42 += parent.Height * 11 / 300f;

			matrix *= PrimeGame.Game.ViewportAdapter.GetScaleMatrix();

			renderer.Draw(layer, matrix);
		}
	}
}

