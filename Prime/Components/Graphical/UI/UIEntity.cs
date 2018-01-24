using Prime;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Prime
{
	public class UIEntity : Entity
	{
		public new Vector2 Position {get; set;}

		private List<UIEntity> children = new List<UIEntity>();

		private UIEntity parent;
			
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

		public override void Update()
		{
			base.Update();

			var pos = this.Scene.Cam.Position;
			if(parent != null)
				pos += parent.Position;

			base.Position = pos + Position - new Vector2(1280, 720) / 2f;
		}

		// Adds a UIEntity to this container
		public T Insert<T>(T e) where T : UIEntity
		{
			this.children.Add(e);

			this.Scene.Add(e);

			e.parent = this;
			e.DrawOrder = this.DrawOrder;

			return e;
		}

		public override void OnDestroy()
		{
			foreach(var e in children)
			{
				e.Destroy();
			}
		}
	}
}
