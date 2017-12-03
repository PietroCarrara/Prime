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

		public override void Draw(SpriteBatch sp)
		{
			sp.Draw(Tex, Owner.Position, Color.White);
		}
	}
}
