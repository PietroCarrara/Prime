using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MonoGame.Extended;

namespace Prime
{
	public class Scene
	{
		public PrimeGame Game;

		public ContentManager Content;

		public Color ClearColor = Color.CornflowerBlue;

		private List<Entity> destroyQueue = new List<Entity>();
		
		private List<Entity> entities = new List<Entity>();
		public List<Entity> Entities
		{
			get
			{
				return entities;
			}
		}

		// Add a entity here and it will be scheduled
		// for deletion
		private List<Entity> deletion = new List<Entity>();

		public Camera Cam;

		public Scene()
		{  }

		public T Add<T>(T e) where T : Entity
		{
			entities.Add(e);
		
			e.Scene = this;

			e.Initialize();

			return e;
		}

		public void Destroy(Entity e)
		{
			this.destroyQueue.Add(e);
		}

		public virtual void Initialize()
		{ 
			this.Cam = new Camera(this.Game.ViewportAdapter);
		}

		public void Draw(SpriteBatch sp)
		{
			foreach(var e in entities)
			{
				e.Draw(sp);
			}
		}

		public virtual void Update()
		{
			foreach (var e in entities)
			{
				e.Update();
			}

			foreach(var e in destroyQueue)
			{
				e.OnDestroy();
				entities.Remove(e);
			}

			destroyQueue.Clear();
		}
	}
}
