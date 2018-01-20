using Prime;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Prime.Graphics
{
    public class SpriteSheet : Sprite
    {
		private Point frameDimensions;
		private Point texDimensions;

		private Animation current;

		private int currentFrame;

		private float elapsedTime;

		// Indicates how many frames there are
		// per 'row' and 'column' of the sprite
		private Point frameCount;

		private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        public SpriteSheet(Texture2D tex, Point texDimensions, Point frameDimensions)
        {
			this.Tex = tex;

			this.texDimensions = texDimensions;
			this.frameDimensions = frameDimensions;

			this.Origin = frameDimensions.ToVector2() / 2;

			frameCount.X = texDimensions.X / frameDimensions.X;
			frameCount.Y = texDimensions.Y / frameDimensions.Y;
        }

		public override float Width
		{
			get
			{
				return frameDimensions.X * this.scale.X;
			}
			set
			{
				scale.X = value / frameDimensions.X;
			}
		}

		public override float Height
		{
			get
			{
				return frameDimensions.Y * this.scale.Y;
			}
			set
			{
				scale.Y = value / frameDimensions.Y;
			}
		}

		public void Add(string name, int start, int end, float frameDuration)
		{
			var a = new Animation
			{
				Name = name,
				Start = start,
				End = end,
				FrameDuration = frameDuration
			};

			animations.Add(name, a);
		}

        public void Play(string name, System.Action post = null)
        {
			var anim = animations[name];

			Play(anim, post);
        }

		private void Play(Animation a, System.Action post = null)
		{
			if(current != null && current.Name == a.Name)
				return;

			elapsedTime = 0;
			
			if(post != null)
				a.OnComplete = post;

			current = a;
			currentFrame = current.Start;
		}

        public override void Update()
        {
            base.Update();

			elapsedTime += Time.DetlaTime;
			
			while(elapsedTime > current.FrameDuration)
			{
				elapsedTime -= current.FrameDuration;
				currentFrame++;
			}

			while(currentFrame >= current.End)
			{
				currentFrame -= current.End - current.Start;
				current.OnComplete?.Invoke();
				current.OnComplete = null;
			}
        }

        public override void Draw(SpriteBatch sp)
        {
			int y = currentFrame / frameCount.X;
			int x = currentFrame - y * frameCount.X;

			x *= frameDimensions.X;
			y *= frameDimensions.Y;

			base.sourceRectangle = new Rectangle(new Point(x, y), frameDimensions);
        
			base.Draw(sp);
		}
    }

	class Animation
	{
		public string Name;
		public int Start;
		public int End;
		public float FrameDuration;
		public Action OnComplete;
		public Animation Previous;
	}
}
