using System;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Prime.UI
{
	public class Button : Prime.UI.UIEntity
	{
		public Action OnClick;

		private GeonBit.UI.Entities.Button bt;

		public Button(string text, AnchorPoint a = AnchorPoint.Auto, Vector2? size = null, Vector2? offset = null)
		{
			bt = new GeonBit.UI.Entities.Button(text, ButtonSkin.Default, (Anchor) a, size, offset);
			this.Entity = bt;

			bt.OnClick = (e) => OnClick?.Invoke();
		}
	}
}
