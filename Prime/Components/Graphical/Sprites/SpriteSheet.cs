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
        private SpriteSheetAnimation curr;

        private SpriteSheetAnimationFactory factory;

        private int frames;

        public SpriteSheet(Texture2D tex, TextureAtlas atlas)
            : base(tex)
        {
            factory = new SpriteSheetAnimationFactory(atlas);
			
            frames = atlas.RegionCount;

			Origin = new Vector2(factory.Frames[0].Width / 2f, factory.Frames[0].Height / 2f);
        }

		public override float Width
		{
			get
			{
				return curr.CurrentFrame.Width * this.scale.X;
			}
			set
			{
				scale.X = value / curr.CurrentFrame.Width;
			}
		}

		public override float Height
		{
			get
			{
				return curr.CurrentFrame.Height * this.scale.Y;
			}
			set
			{
				scale.Y = value / curr.CurrentFrame.Height;
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

        public void Play(string name, System.Action post = default(System.Action))
        {
			// Don't rewind the animation if it's already playing
			if(curr != null && name == curr.Name)
				return;

            var anim = factory.Create(name);

			if (!anim.IsLooping && curr != null)
			{		
				var currName = curr.Name;
				var currPost = curr.OnCompleted;

				anim.OnCompleted = () =>
				{
					post?.Invoke();
					this.Play(currName, currPost);
				};
			} 
			else
			{
				anim.OnCompleted = post;	
			}

			curr = anim;
			curr.Play();
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
					effects: ef,
					sourceRectangle: curr.CurrentFrame.Bounds
					);
        }
    }
}
