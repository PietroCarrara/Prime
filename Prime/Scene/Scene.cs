using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MonoGame.Extended;
using GeonBit.UI;

namespace Prime
{
	public class Scene
	{
		public PrimeGame Game;

		public ContentManager Content;

		public Color ClearColor = Color.CornflowerBlue;

		public UserInterface UI { get; private set; }

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


		public T Add<T>(T e) where T : Prime.Entity
		{
			addQueue.Add(e);
		
			e.Scene = this;

			e.Initialize();

			return e;
		}

		public T AddUI<T>(T e) where T : Prime.UI.UIEntity
		{
			e.Initialize(this);

			this.UI.AddEntity(e.Entity);

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

			this.UI = new UserInterface();
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

			Cam.Update();

			foreach (var e in byUpdateOrder)
			{
				e.Update();
			}

			while(destroyQueue.Any())
			{
				var e = destroyQueue[0];

				e.OnDestroy();
				
				entities.Remove(e);
				byUpdateOrder.Remove(e);
				byDrawOrder.Remove(e);

				destroyQueue.RemoveAt(0);
			}
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

		protected void Destroy()
		{
			destroyQueue = entities;
			
			this.UI.Dispose();
		}
	}
}
