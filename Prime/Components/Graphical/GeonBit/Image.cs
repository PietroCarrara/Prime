using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime.UI
{
	public class Image : UIEntity
	{
		private GeonBit.UI.Entities.Image img;

		public Image(Texture2D tex, Vector2 size, AnchorPoint a = AnchorPoint.Auto, Vector2? offset = null)
		{
			this.img = new GeonBit.UI.Entities.Image(tex, size, ImageDrawMode.Stretch, (Anchor) a, offset);

			this.Entity = img;
		}

		public Image(Texture2D tex, AnchorPoint a = AnchorPoint.Auto, Vector2? offset = null)
		{
			var size = tex.Bounds.Size.ToVector2();

			this.img = new GeonBit.UI.Entities.Image(tex, size, ImageDrawMode.Stretch, (Anchor) a, offset);

			this.Entity = img;
		}
	}
}

