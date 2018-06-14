using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class TextBox : UIEntity
	{
		// Actuall text being displayed
		// Each position indicates a line
		private List<string> displayText;

		// Special characters that can be handled
		private List<char> special = new List<char>{ '\t' };

		private string text = "";
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				text = value;
				
				displayText = escape(text).Split('\n').ToList();

				fit();
				
				TextComponent.Content = displayText.Aggregate((a, b) => a + '\n' + b);
			}
		}

		public TextComponent TextComponent;

		public Sprite Background;

		public SpriteFont Font;

		public TextBox(float w, float h, SpriteFont font, Sprite bg)
		{
			this.Width = w;
			this.Height = h;

			this.Font = font;

			this.Background = bg;

			this.TextComponent = new TextComponent("", font);
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Add(Background);

			this.Add(TextComponent);
			this.TextComponent.Origin = new Vector2(this.Width, this.Height) / -2;
		}

		private string escape(string s)
		{
			// Tabs
			s = s.Replace("\t", "    ");

			return s;
		}

		private void fit()
		{
			var result = new List<string>();

			foreach (var l in displayText)
			{
				var line = l;
				var newLine = "";

				while (Font.MeasureString(line).X > this.Width)
				{
					newLine = line[line.Length - 1] + newLine;

					line = line.Remove(line.Length - 1);
				}

				result.Add(line);
				if (newLine != "")
				{
					result.Add(newLine);
				}
			}

			displayText = result;
		}
	}
}
