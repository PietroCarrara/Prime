using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.TextureAtlases;
using System.Collections.Generic;

namespace Prime.Graphics
{
    public class SpriteSheet : Sprite
    {
        private AnimatedSprite curr;

        private SpriteSheetAnimationFactory factory;

        private int frames;

        public SpriteSheet(Texture2D tex, TextureAtlas atlas)
            : base(tex)
        {
            factory = new SpriteSheetAnimationFactory(atlas);

            frames = atlas.RegionCount;
        }

		public override float Width
		{
			get
			{
				return curr.TextureRegion.Width * this.scale.X;
			}
			set
			{
				scale.X = value / curr.TextureRegion.Width;
			}
		}

		public override float Height
		{
			get
			{
				return curr.TextureRegion.Height * this.scale.Y;
			}
			set
			{
				scale.Y = value / curr.TextureRegion.Height;
			}
		}

        public void Add(string name, int start, int end, float frameDuration = 0.2f, bool loop = true, bool reversed = false, bool pingPong = false)
        {
			var frames = new int[end - start];

            for (int i = 0; i < end - start; i++)
            {
                frames[i] = start + i;
            }

            factory.Add(name, new SpriteSheetAnimationData(frames, frameDuration, loop, reversed, pingPong));
        }

        public void Add(string name, int[] frames = null, float frameDuration = 0.2f, bool loop = true, bool reversed = false, bool pingPong = false)
        {
            if (frames == null)
            {
                frames = new int[this.frames];

                for (int i = 0; i < this.frames; i++)
                {
                    frames[i] = i;
                }
            }

            factory.Add(name, new SpriteSheetAnimationData(frames, frameDuration, loop, reversed, pingPong));
        }

        public void Play(string name)
        {
            curr = new AnimatedSprite(factory, name);
            curr.Play(name);
        }

        public override void Update()
        {
            base.Update();

			var scale = new Vector2(Width, Height);

            curr.Update(Time.DetlaTime);

			this.Width = scale.X;
			this.Height = scale.Y;
        }

        public override void Draw(SpriteBatch sp)
        {
			sp.Draw(
					texture: Tex,
					position: Owner.Position,
					sourceRectangle: curr.TextureRegion.Bounds,
					color: Color.White,
					scale: scale
				   );
        }
    }
}
