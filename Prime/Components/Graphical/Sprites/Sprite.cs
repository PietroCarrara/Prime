using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime.Graphics
{
	public class Sprite : Component
	{
		public Texture2D Tex;

		public Sprite(Texture2D tex)
		{
			Tex = tex;
		}

		protected Vector2 scale = Vector2.One;

		public virtual float Width
		{
			get
			{
				return Tex.Width * scale.X;
			}
			set
			{
				scale.X = value / Tex.Width;
			}
		}

		public virtual float Height
		{
			get
			{
				return Tex.Height * scale.Y;
			}
			set
			{
				scale.Y = value / Tex.Height;
			}
		}

		public override void Draw(SpriteBatch sp)
		{
			sp.Draw(Tex, Owner.Position, Color.White);
		}
	}
}
