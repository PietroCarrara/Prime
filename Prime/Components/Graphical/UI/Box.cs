using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Box : UIEntity
	{
		public float Width, Height;

		public Box(float w, float h)
		{
			this.Width = w;
			this.Height = h;
		}
	}
}
