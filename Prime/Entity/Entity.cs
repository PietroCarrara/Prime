using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Prime
{
	public class Entity
	{
		private List<Component> components = new List<Component>();
		public List<Component> Components
		{
			get
			{
				return components;
			}
		}

		private float drawOrder;
		public virtual float DrawOrder
		{
			get
			{
				return drawOrder;
			}
			set
			{
				drawOrder = value;
				if(this.Scene != null)
					Scene.SortDraw(this);
			}
		}

		private float updateOrder;
		public virtual float UpdateOrder
		{
			get
			{
				return updateOrder;
			}
			set
			{
				updateOrder = value;
				if(this.Scene != null)
					Scene.SortUpdate(this);
			}
		}

		private List<Component> destroyQueue = new List<Component>();

		public Scene Scene;

		public Vector2 Position;

		public T Add<T>(T c) where T : Component
		{
			this.components.Add(c);

			c.Owner = this;

			c.Initialize();

			return c;
		}

		public T GetComponent<T>() where T : Component
		{
			foreach(var c in components)
			{
				if (c is T)
					return (T) c;
			}

			return null;
		}

		public void Destroy()
		{
			destroyQueue.AddRange(components);

			Scene.Destroy(this);
		}

		public void Destroy(Component c)
		{
			destroyQueue.Add(c);
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

			foreach (var c in destroyQueue)
			{
				c.OnDestroy();
				components.Remove(c);
			}

			destroyQueue.Clear();
		}

		public virtual void OnDestroy()
		{  }
	}
}
