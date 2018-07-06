using System;
using Microsoft.Xna.Framework;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Prime.UI
{
	public abstract class UIEntity
	{
		public GeonBit.UI.Entities.Entity Entity { get; protected set; }

		public Scene Scene { get; private set; }

		public void Initialize(Scene s)
		{
			this.Scene = s;
		}

		public void Destroy()
		{
			Scene.UI.RemoveEntity(this.Entity);
		}
	}
}
