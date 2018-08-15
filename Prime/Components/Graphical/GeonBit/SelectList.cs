using System;
using System.Collections.Generic;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Prime.UI
{
	public class SelectList<T> : UIEntity
	{
		public List<T> Items { get; private set; } = new List<T>();

		private GeonBit.UI.Entities.SelectList list;

		public T Selected
		{
			get
			{
				int index = list.SelectedIndex;

				if (index < 0)
					return default(T);
				else
					return Items[index];
			}
		}

		public SelectList(AnchorPoint a = AnchorPoint.Auto, Vector2? offset = null) : base()
		{
			list = new GeonBit.UI.Entities.SelectList((Anchor)a, offset);

			this.Entity = list;
		}

		public SelectList(Vector2 size, AnchorPoint a = AnchorPoint.Auto, Vector2? offset = null)
		{
			list = new GeonBit.UI.Entities.SelectList(size, (Anchor)a, offset, PanelSkin.ListBackground);

			this.Entity = list;
		}

		public void Add(string name, T item)
		{
			Items.Add(item);
			list.AddItem(name);
		}
	}
}
