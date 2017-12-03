using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Prime
{
	public class Entity : IMovable
	{
		private List<Component> components = new List<Component>();
		public List<Component> Components
		{
			get
			{
				return components;
			}
		}

		public Scene Scene;

		private Vector2 pos;
		public Vector2 Position
		{
			get
			{
				return pos;
			}
			set 
			{
				pos = value;
			}
		}

		public T Add<T>(T c) where T : Component
		{
			this.components.Add(c);

			c.Initialize();
			
			c.Owner = this;

			return c;
		}

		public virtual void Initialize() 
		{

		}

		public virtual void Draw(SpriteBatch sp)
		{
			foreach(var component in components)
			{
				component.Draw(sp);
			}
		}

		public virtual void Update()
		{
			foreach(var c in components)
			{
				c.Update();
			}
		}
	}
}
