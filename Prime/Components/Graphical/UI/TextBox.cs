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
				text = value;
				
				displayText = escape(text).Split('\n').ToList();

				fit();
				
				TextComponent.Content = displayText.Aggregate((a, b) => a + '\n' + b).Insert(carretIndex, "|");
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
			{
				carretIndex--;
				this.Text = this.Text;
			}

			if(Input.IsKeyPressed(Keys.Right) && carretIndex < this.Text.Length)
			{
				carretIndex++;
				this.Text = this.Text;
			}

			if(Input.IsKeyPressed(Keys.Up))
			{
				int totalChars = displayText[0].Length;
				int carretLine = 0;
				
				int inLineIndex;

				foreach(var l in displayText.Skip(1))
				{
					if (totalChars >= carretIndex)
						break;

					carretLine++;
					totalChars += l.Length + 1;
				}

				if (carretLine > 0)
				{
					int finalPos = totalChars - displayText[carretLine].Length - 1 - displayText[carretLine - 1].Length;

					// How far ahead is the carret
					int ahead = carretIndex - (finalPos + displayText[carretLine - 1].Length + 1);

					if (ahead < displayText[carretLine - 1].Length)
					{
						finalPos += ahead;
					}
					else
					{
						finalPos += displayText[carretLine -1].Length;
					}

					carretIndex = finalPos;

					this.Text = this.Text;
				}
			}

			if(Input.IsKeyPressed(Keys.Down) && carretIndex < this.Text.Length)
			{
				int totalChars = displayText[0].Length;
				int carretLine = 0;
				
				int inLineIndex;

				foreach(var l in displayText.Skip(1))
				{
					if (totalChars >= carretIndex)
						break;

					carretLine++;
					totalChars += l.Length + 1;
				}

				if (carretLine < displayText.Count - 1)
				{
					int finalPos = totalChars + 1;

					// How far ahead is the carret
					int ahead = carretIndex - (totalChars - displayText[carretLine].Length);

					if (ahead < displayText[carretLine + 1].Length)
					{
						finalPos += ahead;
					}
					else
					{
						finalPos += displayText[carretLine + 1].Length;
					}

					carretIndex = finalPos;

					this.Text = this.Text;
				}
			}
		}

		public void ResetCarret()
		{
			this.Text = this.Text;
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

			int carretLine = 0;

			// I don't think that fonts have variable height, so this is ok
			float pixelHeight = Font.MeasureString("ABC").Y;
			int totalLines = (int)(this.Height / pixelHeight);

			if (result.Any())
			{
				int totalChars = result[0].Length - 1;

				foreach(var l in result.Skip(1))
				{
					if (totalChars >= carretIndex)
						break;

					carretLine++;
					totalChars += l.Length + 1;
				}

				int topLine = scrollValue;
				int bottomLine = topLine + totalLines;

				if (topLine < 0)
					topLine = 0;
				if (bottomLine > displayText.Count())
					bottomLine = displayText.Count();

				// Scroll
				if (carretLine < topLine)
				{
					bottomLine = carretLine + totalLines;
					topLine = carretLine;
				}
				else if (carretLine >= bottomLine)
				{
					topLine += carretLine - bottomLine + 1;
					bottomLine = carretLine + 1;
				}

				// If we are not using all the possible lines
				if (bottomLine - topLine != totalLines && carretLine >= totalLines)
				{
					bottomLine = carretLine;
					topLine = bottomLine - totalLines + 1;
				}

				scrollValue = topLine;

				int len = result.Count();
				if (bottomLine + 1 < len)
					result.RemoveRange(bottomLine + 1, len - (bottomLine + 1));
			}

			displayText = result.Skip(scrollValue).ToList();
		}

		private void receiveChar(char c)
		{
			if (Font.Characters.Contains(c) || special.Contains(c))
			{
				var txt = this.Text.Insert(carretIndex, c.ToString());
				carretIndex += txt.Length - this.Text.Length;
				this.Text = txt;
			}
		}

		private void receiveKey(Keys k)
		{
			var txt = "";
			switch(k)
			{
				case Keys.Enter:
					txt = this.Text.Insert(carretIndex, "\n");
					carretIndex++;
					this.Text = txt;
					break;
				case Keys.Back:
					if(this.Text.Length > 0 && carretIndex > 0)
					{
						txt = this.Text.Remove(carretIndex - 1, 1);
						carretIndex--;
						this.Text = txt;
					}
					break;
			}
		}
	}
}
