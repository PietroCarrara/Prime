using Prime;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Text : UIEntity
	{
		private TextComponent textComponent;

		private string text;
		public string Content
		{
			get
			{
				return text;
			}
			set
			{
				text = value;

				textComponent.Content = text;
			}
		}

		public Text(string content, SpriteFont font)
		{
			this.textComponent = new TextComponent(content, font);
		}

		public override void Initialize()
		{
			this.Add(textComponent);
		}
	}
}
