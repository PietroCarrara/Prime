using System.Linq;
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

		private List<Entity> byUpdateOrder = new List<Entity>();
		private List<Entity> byDrawOrder = new List<Entity>();

		private List<Entity> entities = new List<Entity>();
		public List<Entity> Entities
		{
			get
			{
				return entities;
			}
		}

		public List<T> GetEntities<T>() where T : Entity
		{
			return entities.OfType<T>().ToList();
		}

		// Add a entity here and it will be scheduled
		// for deletion
		private List<Entity> destroyQueue = new List<Entity>();

		// Add a entity here and it will be scheduled
		// for addition
		private List<Entity> addQueue = new List<Entity>();

		public Camera Cam = new Camera();

		public Scene()
		{  }

		public T Add<T>(T e) where T : Entity
		{
			addQueue.Add(e);
		
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
			this.Cam.Camera2D = new Camera2D(this.Game.ViewportAdapter);
			this.Cam.Position = new Vector2(1280 / 2f, 720 / 2f);
			this.Cam.Initialize();
		}

		public void Draw(SpriteBatch sp)
		{
			foreach(var e in byDrawOrder)
			{
				e.Draw(sp);
			}
		}

		public virtual void Update()
		{
			foreach(var e in addQueue)
			{
				entities.Add(e);
				SortUpdate(e);
				SortDraw(e);
			}

			addQueue.Clear();

			foreach (var e in byUpdateOrder)
			{
				e.Update();
			}

			Cam.Update();

			foreach(var e in destroyQueue)
			{
				e.OnDestroy();
				entities.Remove(e);
			}

			destroyQueue.Clear();
		}

		internal void SortDraw(Entity e)
		{
			byDrawOrder.Remove(e);

			int index = 0;
			foreach(var entity in byDrawOrder)
			{
				if(entity.DrawOrder > e.DrawOrder)
					break;
				else
					index++;
			}
			byDrawOrder.Insert(index, e);
		}

		internal void SortUpdate(Entity e)
		{
			byUpdateOrder.Remove(e);

			int index = 0;
			foreach(var entity in byUpdateOrder)
			{
				if(entity.UpdateOrder > e.UpdateOrder)
					break;
				else
					index++;
			}
			byUpdateOrder.Insert(index, e);
		}
	}
}
