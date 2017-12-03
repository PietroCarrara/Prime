using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

namespace Prime
{
	public class Component
	{
		public Entity Owner;		

		public virtual void Initialize() 
		{

		}

		public virtual void Draw(SpriteBatch sp)
		{

		}

		public virtual void Update()
		{

		}

		public virtual void OnDestroy()
		{  }
	}
}
