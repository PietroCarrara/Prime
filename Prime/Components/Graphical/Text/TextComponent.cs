using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class TextComponent : Component
	{
		public bool IsVisible = true;

		private string content;
		public string Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
				
				var size = Vector2.Zero;

				switch(this.Alignment)
				{
					case Alignment.Left:
						this.Origin = Vector2.Zero;
						break;
					case Alignment.Center:
						size = Font.MeasureString(value);
						this.Origin.X = -size.X / 2f;
						break;
					case Alignment.Right:
						size = Font.MeasureString(value);
						this.Origin.X = -size.X;
						break;
				}
			}
		}

		public SpriteFont Font;

		public Color Color = Color.White;

		public Vector2 Origin = Vector2.Zero;

		private Alignment alignment = Alignment.None;
		public Alignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
				this.Content = this.Content;
			}
		}

		public TextComponent(string text, SpriteFont font)
		{
			this.Content = text;

			this.Font = font;
		}

		public override void Draw(SpriteBatch sp)
		{
			base.Draw(sp);

			if ( !IsVisible )
				return;

			sp.DrawString(
					this.Font,
					this.Content,
					Owner.Position + this.Origin,
					this.Color);
		}
	}
}
