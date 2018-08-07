using System;
using Microsoft.Xna.Framework;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Prime.UI
{
	public abstract class UIEntity
	{
		public delegate void EntityCallback(UIEntity e);

		public GeonBit.UI.Entities.Entity Entity { get; protected set; }

		public Scene Scene { get; private set; }

		public EntityCallback OnValueChange;

		public bool IsVisible
		{
			get
			{
				return Entity.Visible;
			}
			set
			{
				Entity.Visible = value;
			}
		}

		public Vector2 Offset
		{
			set
			{
				this.Entity.SetOffset(value);
			}
		}

		public Vector2 Size
		{
			get
			{
				return this.Entity.Size;
			}
		}

		public void Initialize(Scene s)
		{
			this.Scene = s;

			this.Entity.OnValueChange = (e) => OnValueChange?.Invoke(this);
		}

		public void AddChild(UIEntity e)
		{
			this.Entity.AddChild(e.Entity);
		}
        
        public void GetFocus()
        {
			this.Scene.UI.ActiveEntity = this.Entity;
        }

		public void Destroy()
		{
			Scene.UI.RemoveEntity(this.Entity);
		}
	}
}
