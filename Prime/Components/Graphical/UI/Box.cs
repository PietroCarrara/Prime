using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Box : UIEntity
	{
		public float Width, Height;

		public Sprite Background;
		
		public Box(float w, float h, Sprite bg)
		{
			this.Width = w;
			this.Height = h;
			
			this.Background = bg;
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Add(Background);
		}
	}
}
