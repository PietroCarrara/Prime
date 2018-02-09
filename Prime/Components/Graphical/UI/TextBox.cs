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

		// This says how many lines 
		// is the scrolling skipping
		private int scrollValue;

		// Where the carret is
		private int carretIndex = 0;

		private string text = "";
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				text = escape(value);
				
				displayText = text.Split('\n').ToList();

				fit();
				
				TextComponent.Content = displayText.Aggregate((a, b) => a + '\n' + b);
			}
		}

		public TextComponent TextComponent;

		public Sprite Background;

		public SpriteFont Font;

		public float Width, Height;

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

			this.Scene.Game.Window.TextInput += (obj, e) =>
			{
				receiveChar(e.Character);
				receiveKey(e.Key);
			};
		}

		public override void Update()
		{
			base.Update();

			if(Input.IsKeyPressed(Keys.Left) && carretIndex > 0)
				carretIndex--;
			
			if(Input.IsKeyPressed(Keys.Right) && carretIndex < this.Text.Length)
				carretIndex++;
		}

		public void ResetCarret()
		{
			carretIndex = this.Text.Length;
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

			foreach (var l in displayText.Skip(scrollValue))
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

		private void receiveChar(char c)
		{
			if(Font.Characters.Contains(c))
			{
				carretIndex++;
				this.Text = this.Text.Insert(carretIndex, c.ToString());
			}
			else
			{
				// Special characters
				switch(c)
				{
					case '\t':
						carretIndex += 4;
						this.Text = this.Text.Insert(carretIndex, c.ToString());
						break;
				}
			}
		}

		private void receiveKey(Keys k)
		{
			switch(k)
			{
				case Keys.Enter:
					this.Text += '\n';
					carretIndex++;
					break;
				case Keys.Back:
					if(this.Text.Length > 0 && carretIndex > 0)
					{
						carretIndex--;
						this.Text = this.Text.Remove(carretIndex - 1, 1);
					}
					break;
			}
		}
	}
}
