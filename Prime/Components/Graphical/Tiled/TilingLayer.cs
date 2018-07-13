using Prime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System.Collections.Generic;

namespace Prime
{
	public class TilingLayer : Entity
	{
		private TiledMapRenderer renderer;
		private TiledMapLayer layer;
		private TilingMap parent;

		public List<Point> Objects = new List<Point>();

		public TilingLayer(TiledMapRenderer r, TilingMap m, TiledMapLayer l)
		{
			renderer = r;
			layer = l;
			parent = m;

			if (l is TiledMapTileLayer sla)
			{
				int x = 0, y = 0;
				foreach (var tile in sla.Tiles)
				{
					if (!tile.IsBlank)
						Objects.Add(new Point(x, y));

					x++;
					if (x >= sla.Width)
					{
						y++;
						x = 0;
					}
				}
			}
		}

		public override void Draw(SpriteBatch sp)
		{
			base.Draw(sp);

			// Create a matrix to scale X and Y, we don't care about Z
			var matrix = Matrix.CreateScale(parent.Scale.X, parent.Scale.Y, 1);

			// Position it according to the camera and the parent position
			var pos = parent.Position - PrimeGame.Game.ActiveScene.Cam.Position;
			matrix *= Matrix.CreateTranslation(pos.X, pos.Y, 1);

			matrix *= PrimeGame.Game.ViewportAdapter.GetScaleMatrix();

			renderer.Draw(layer, matrix);
		}
	}
}

