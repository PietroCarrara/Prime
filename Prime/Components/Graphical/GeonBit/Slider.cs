using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Prime.UI
{
	public class Slider : UIEntity
	{
		public Action OnClick;

		private GeonBit.UI.Entities.Slider slider;

		public Slider(UInt32 min = 0, UInt32 max = 10, AnchorPoint anchor = AnchorPoint.Auto, Vector2? offset = null)
		{
			this.Entity = slider = new GeonBit.UI.Entities.Slider(min, max, SliderSkin.Default, (Anchor)anchor, offset);
		}

		public int Value
		{
			get
			{
				return this.slider.Value;
			}
			set
			{
				this.slider.Value = value;
			}
		}
	}
}
