using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Prime.Graphics
{
	public class Sprite : Component
	{
		public Texture2D Tex;

		public bool IsVisible = true;

		public Vector2? Origin;

		public Vector2 RelativePosition;
        
        private float rotation;
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
				collider.Rotation = value;
                rotation = value;
            }
        }

		public bool FlipX, FlipY;

		private RectangleCollider collider;

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

        internal Sprite(int width, int height, Vector2 origin)
        {
			this.collider = new RectangleCollider(width, height);
			collider.Origin = origin;

			this.Origin = origin;
        }

		public Sprite(Texture2D tex) : this(tex, new Vector2(tex.Width / 2f, tex.Height / 2f))
		{ }
        
		public Sprite(Texture2D tex, Vector2 origin) : this(tex.Width, tex.Height, origin)
		{
			Tex = tex;
		}

		protected Vector2 scale = Vector2.One;

		public virtual float Width
		{
			get
			{
				if (SourceRectangle.HasValue)
					return SourceRectangle.Value.Width * scale.X;

				return Tex.Width * scale.X;
			}
			set
			{
				if (SourceRectangle.HasValue)
					scale.X = value / SourceRectangle.Value.Width;
				else
					scale.X = value / Tex.Width;
			}
		}

		public virtual float Height
		{
			get
			{
				if (SourceRectangle != null)
					return this.SourceRectangle.Value.Height * scale.Y;

				return Tex.Height * scale.Y;
			}
			set
			{
				if (SourceRectangle != null)
					scale.Y = value / SourceRectangle.Value.Height;
				else
					scale.Y = value / Tex.Height;
			}
		}

		public override void Draw(SpriteBatch sp)
		{
			if (!IsVisible)
				return;

			var e = new Entity
			{
				Position = Owner.Position + this.RelativePosition
			};
			e.Add(collider);
            
            if (!collider.CollidesWith(this.Owner.Scene.Cam.Bounds))
			{
				return;
			}

			var ef = SpriteEffects.None;

			if (FlipX)
				ef = SpriteEffects.FlipHorizontally;

			if (FlipY)
			{
				if (ef != SpriteEffects.None)
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
