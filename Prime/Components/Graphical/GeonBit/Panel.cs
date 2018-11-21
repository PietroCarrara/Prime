using System;
using Microsoft.Xna.Framework;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Prime.UI
{
	public class Panel : UIEntity
	{
		private GeonBit.UI.Entities.Panel panel;

		public Panel(Vector2 size, AnchorPoint a = AnchorPoint.Auto, Vector2? offset = null)
		{
			this.panel = new GeonBit.UI.Entities.Panel(size, PanelSkin.Default, (Anchor)a, offset);
			this.Entity = this.panel;

			this.panel.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
            this.panel.Scrollbar.AdjustMaxAutomatically = true;
		}
	}
}
