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

		// Where the carretIs
		private int carretIndex = 1;

		private string text;
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				text = value;
				displayText = value.Split('\n').ToList();

				// TODO: Manipulate text so it doesn't escape boungins
				
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

		private void receiveChar(char c)
		{
			if(Font.Characters.Contains(c))
			{
				this.Text += c;
				carretIndex++;
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
						this.Text = this.Text.Remove(carretIndex - 1, 1);
						carretIndex--;
					}
					break;
			}
		}
	}
}
