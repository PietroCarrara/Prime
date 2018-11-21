using Microsoft.Xna.Framework;

namespace Prime.UI
{
	public abstract class UIEntity
	{
		public delegate void EntityCallback(UIEntity e);

		public GeonBit.UI.Entities.Entity Entity { get; protected set; }

		public UIEntity Parent { get; private set; }

		private bool attached;

		public Scene Scene { get; private set; }

		public bool Enabled
		{
			get
			{
				return !this.Entity.Disabled;
			}
			set
			{
				this.Entity.Disabled = !value;
			}
		}

		public bool Draggable
		{
			get
			{
				return this.Entity.Draggable;
			}
			set
			{
				this.Entity.Draggable = value;
			}
		}

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
			e.Parent = this;
			this.Scene.AddUI(e);
		}

		public void GetFocus()
		{
			this.Scene.UI.ActiveEntity = this.Entity;
		}

		public void Attach()
		{
			if (!this.attached)
			{
				if (this.Parent != null)
					this.Parent.Entity.AddChild(this.Entity);
				else
					this.Scene.UI.AddEntity(this.Entity);

				this.attached = true;
			}
		}

		public void Unattach()
		{
			if (this.attached)
			{
				this.Scene.UI.RemoveEntity(this.Entity);
				this.attached = false;
			}
		}
	}
}
