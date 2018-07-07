using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Prime.UI
{
	public class TextInput : UIEntity
	{
		private GeonBit.UI.Entities.TextInput text;

		public string Value
		{
			get
			{
				return text.Value;
			}
			set
			{
				text.Value = value;
			}
		}

		public TextInput(bool multiline, AnchorPoint a = AnchorPoint.Auto, Vector2? offset = null)
		{
			this.text = new GeonBit.UI.Entities.TextInput(multiline, (Anchor) a, offset);

			this.Entity = text;
		}

		public TextInput(bool multiline, Vector2 size, AnchorPoint a = AnchorPoint.Auto, Vector2? offset = null)
		{
			this.text = new GeonBit.UI.Entities.TextInput(multiline, size, (Anchor) a, offset, PanelSkin.ListBackground);

			this.Entity = text;
		}
	}
}
