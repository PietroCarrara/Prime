using Prime;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Text : UIEntity
	{
		public TextComponent TextComponent;

		public Text(string content, SpriteFont font)
		{
			this.TextComponent = new TextComponent(content, font);
		}

		public override void Initialize()
		{
			this.Add(TextComponent);
		}
	}
}
