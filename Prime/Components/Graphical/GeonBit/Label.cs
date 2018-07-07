using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Prime.UI
{
	public class Label : UIEntity
	{
		private GeonBit.UI.Entities.Label label;

		public string Text
		{
			get
			{
				return label.Text;
			}
			set
			{
				label.Text = value;
			}
		}

		public Label(string text, AnchorPoint a = AnchorPoint.Auto, Vector2? size = null, Vector2? offset = null)
		{
			this.label = new GeonBit.UI.Entities.Label(text, (Anchor) a, size, offset);

			this.Entity = label;
		}
	}
}

