using System;
using Microsoft.Xna.Framework;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Prime.UI
{
	public abstract class UIEntity
	{
		public GeonBit.UI.Entities.Entity Entity { get; protected set; }

		public virtual void Destroy()
		{
			UserInterface.Active.RemoveEntity(this.Entity);
		}
	}
}
