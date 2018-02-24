using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prime.Graphics
{
    public class Sprite : Component
    {
        public Texture2D Tex;

		public bool IsVisible = true;

        public Vector2? Origin;

		public Vector2 RelativePosition;

        public float Rotation;

		public bool FlipX, FlipY;

		protected Rectangle? sourceRectangle;
		public Rectangle? SourceRectangle
		{
			get
			{
				return this.sourceRectangle;
			}
			set
			{
				var h = this.Height;
				var w = this.Width;

				this.sourceRectangle = value;

				this.Height = h;
				this.Width = w;
			}
		}

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
				if(SourceRectangle != null)
					return SourceRectangle.Value.Width * scale.X;

                return Tex.Width * scale.X;
            }
            set
            {
				if(SourceRectangle != null)
                	scale.X = value / SourceRectangle.Value.Width;
				else
                	scale.X = value / Tex.Width;
            }
        }

        public virtual float Height
        {
            get
            {
				if(SourceRectangle != null)
					return this.SourceRectangle.Value.Height * scale.Y;

                return Tex.Height * scale.Y;
            }
            set
            {
				if(SourceRectangle != null)
                	scale.Y = value / SourceRectangle.Value.Height;
				else
                	scale.Y = value / Tex.Height;
            }
        }

        public override void Draw(SpriteBatch sp)
        {
			if(!IsVisible)
				return;

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
					position: Owner.Position + RelativePosition,
					color: Color.White,
					origin: Origin,
					rotation: Rotation,
					scale: scale,
					effects: ef,
					sourceRectangle: SourceRectangle
					);
        }

		public virtual Sprite Clone()
		{
			var res = new Sprite(this.Tex);

			res.IsVisible = this.IsVisible;
			res.FlipX = this.FlipX;
			res.FlipY = this.FlipY;
			res.Height = this.Height;
			res.Width = this.Width;
			res.Origin = this.Origin;
			res.RelativePosition = this.RelativePosition;
			res.Rotation = this.Rotation;

			return res;
		}
    }
}
