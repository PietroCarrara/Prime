using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Component
	{
		public bool Initialized { get; private set; }

		public Entity Owner;

		public virtual void Initialize()
		{
			this.Initialized = true;
		}

		public virtual void Draw(SpriteBatch sp)
		{

		}

		public virtual void Update()
		{

		}

		public virtual void OnDestroy()
		{ }
	}
}
