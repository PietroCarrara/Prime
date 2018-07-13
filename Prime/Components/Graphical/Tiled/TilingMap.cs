using Prime;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

namespace Prime
{
	public class TilingMap : Entity
	{
		internal TiledMap map { get; private set; }
		private TiledMapRenderer renderer = new TiledMapRenderer(PrimeGame.Game.GraphicsDevice);

		public Vector2 Scale = Vector2.One;
		public float Width
		{
			get
			{
				return map.WidthInPixels * Scale.X;
			}
			set
			{
				Scale.X = value / map.WidthInPixels;
			}
		}

		public float Height 
		{
			get
			{
				return map.HeightInPixels * Scale.Y;
			}
			set
			{
				Scale.Y = value / map.HeightInPixels;
			}
		}

		public int TileWidth
		{
			get
			{
				return (int) (map.TileWidth * Scale.X);
			}
		}

		public int TileHeight
		{
			get
			{
				return (int) (map.TileHeight * Scale.Y);
			}
		}

		public int WidthInTiles
		{
			get
			{
				return map.Width;
			}
		}

		public int HeightInTiles
		{
			get
			{
				return map.Height;
			}
		}

		public Dictionary<string, TilingLayer> Layers = new Dictionary<string, TilingLayer>();

		public TilingMap(TiledMap t)
		{
			map = t;
		}

		public static TilingMap Load(ContentManager c, string path)
		{
			return new TilingMap(c.Load<TiledMap>(path));
		}

		public override void Initialize()
		{
			foreach ( var l in map.Layers )
			{
				var layer = new TilingLayer(renderer, this, l);

				Layers.Add(l.Name, layer);

				this.Scene.Add(layer);
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();

			foreach (var l in map.Layers)
			{
				Layers[l.Name].Destroy();
			}
		}
	}
}
