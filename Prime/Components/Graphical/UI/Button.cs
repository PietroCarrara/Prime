using Prime;
using Prime.Graphics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Button : UIEntity
	{
		public Sprite BG, BGOnclick;

		public TextComponent Text;

		public RectangleCollider HitBox;

		public Action OnClick;

		public Button(float width, float height, Sprite bg, Sprite bgOnClick, string text, SpriteFont font, Action onClick)
		{
			HitBox = new RectangleCollider(width, height);

			this.BG = bg;

			this.BGOnclick = bgOnClick;
			this.BGOnclick.IsVisible = false;

			this.Text = new TextComponent(text, font);
			Text.Alignment = Alignment.Center;
			Text.Origin.Y = font.MeasureString(text).Y / -2f;
			this.OnClick = onClick;
		}

		public override void Initialize()
		{
			base.Initialize();
			
			this.Add(BG);
			this.Add(BGOnclick);
			this.Add(Text);
			this.Add(HitBox);
		}

		public override void Update()
		{
			base.Update();

			if(HitBox.CollidesWith(Input.MousePosition(this.Scene.Cam)))
			{
				this.BG.IsVisible = false;
				this.BGOnclick.IsVisible = true;

				if(Input.IsButtonPressed(MouseButtons.Left))
					OnClick?.Invoke();
			}
			else
			{
				this.BG.IsVisible = true;
				this.BGOnclick.IsVisible = false;
			}
		}
	}
}
