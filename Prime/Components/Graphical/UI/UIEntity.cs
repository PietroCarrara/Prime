using Prime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections.Generic;

namespace Prime
{
	public class UIEntity : Entity
	{
		public new Vector2 Position {get; set;}

		public float Width, Height;

		private List<UIEntity> children = new List<UIEntity>();

		private UIEntity parent;

		private bool isVisible = true;
		public bool IsVisible
		{
			get
			{
				return isVisible;
			}
			set
			{
				this.isVisible = value;
				foreach(var c in children)
				{
					c.IsVisible = value;
				}
			}
		}

		protected Vector2 AbsolutePosition
		{
			get
			{
				return base.Position;
			}
		}
			
		public override float DrawOrder
		{
			get
			{
				return base.DrawOrder;
			}
			set
			{
				base.DrawOrder = value;
				foreach(var e in children)
				{
					e.DrawOrder = value;
				}
			}
		}

		public UIEntity()
		{  }

		public UIEntity( float w, float h )
		{
			this.Width = w;
			this.Height = h;
		}

		public override void Update()
		{
			base.Update();

			var pos = this.Scene.Cam.Position;
			if(parent != null)
				pos += parent.Position;

			base.Position = pos + Position - new Vector2(1280, 720) / 2f;
		}

		public override void Draw(SpriteBatch sp)
		{
			if ( IsVisible )
				base.Draw(sp);
		}

		// Adds a UIEntity to this container
		public T Insert<T>(T e) where T : UIEntity
		{
			this.children.Add(e);

			this.Scene.Add(e);

			e.parent = this;
			e.DrawOrder = this.DrawOrder;

			e.IsVisible = this.IsVisible;

			return e;
		}

		public override void OnDestroy()
		{
			base.OnDestroy();

			while(children.Any())
			{
				children[0].Destroy();
				children.RemoveAt(0);
			}
		}
	}
}
