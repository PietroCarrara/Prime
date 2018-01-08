using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime.Graphics
{
    public class Sprite : Component
    {
        public Texture2D Tex;

        public Vector2 Origin;

        public float Rotation;

		public bool FlipX, FlipY;

        public Sprite(Texture2D tex)
        {
            Tex = tex;

            Origin = new Vector2(tex.Width / 2f, tex.Height / 2f);
        }

		internal Sprite()
		{  }

        public Sprite(Texture2D tex, Vector2 origin)
        {
            Tex = tex;

            Origin = origin;
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
			var ef = SpriteEffects.None;

			if(FlipX)
				ef = SpriteEffects.FlipHorizontally;
			
			if(FlipY)
			{
				if(ef != SpriteEffects.None)
					ef |= SpriteEffects.FlipVertically;
				else
					ef = SpriteEffects.FlipVertically;
			}

            sp.Draw(
					texture: Tex,
					position: Owner.Position, 
					color: Color.White, 
					origin: Origin, 
					rotation: Rotation,
					scale: scale,
					effects: ef
					);
        }
    }
}
