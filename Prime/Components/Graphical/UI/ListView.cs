using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Prime.Graphics;

namespace Prime
{
	public class ListView<T> : UIEntity
	{
		public T Selected;

		public float Width, Height, CellWidth, CellHeight;

		private int cellsPerPage, cellsPerLine, linesPerPage, currentPage, elementCount;

		private float paddingX, paddingY;

		private List<T> elements = new List<T>();

		private Sprite selectedHightlight;

		public Action<T> OnSelected;

		private RectangleCollider hitBox;

		public ListView(float w, float h, float cellW, float cellH)
		{
			this.Width = w;
			this.Height = h;

			this.CellWidth = cellW;
			this.CellHeight = cellH;

			cellsPerLine = (int)(Width / CellWidth);
			linesPerPage = (int)(Height / CellHeight);
			cellsPerPage = cellsPerLine * linesPerPage;

			paddingX = (Width - CellWidth * cellsPerLine) / 2;
			paddingY = (Height - CellHeight * linesPerPage) / 2;
		}

		public override void Initialize()
		{
			base.Initialize();

			hitBox = new RectangleCollider(this.Width, Height - paddingY * 2);

			selectedHightlight = new RectangleSprite((int)CellWidth, (int)CellHeight);
			selectedHightlight.IsVisible = false;

			this.Add(hitBox);
			this.Add(selectedHightlight);
		}

		public override void Update()
		{
			base.Update();

			if (Input.IsButtonPressed(MouseButtons.Left) && hitBox.CollidesWith(Input.MousePosition(this.Scene.Cam)))
			{
				var pos = Input.MousePosition(this.Scene.Cam) - this.AbsolutePosition + new Vector2(Width, Height) / 2;

				float x = paddingX, y = paddingY + CellHeight;
				int i, page = 0, line = 0;
				for (i = 0; i < cellsPerPage; i++)
				{
					x += CellWidth;

					if (x - paddingX > cellsPerLine * CellWidth)
					{
						if (line >= linesPerPage)
						{
							line = 0;
							page++;
							x = paddingX;
							y = paddingY + CellHeight;
						}
						else
						{
							y += CellHeight;
							line++;
						}
					}

					if (x >= pos.X && y >= pos.Y)
						break;
				}
				x -= CellWidth;
				y -= CellHeight;

				if (i + page * cellsPerPage < elementCount)
				{
					Selected = elements[i + page * cellsPerPage];
					selectedHightlight.IsVisible = true;

					selectedHightlight.RelativePosition.X = - Width / 2 + CellWidth / 2;
					selectedHightlight.RelativePosition.Y = -paddingY;
					selectedHightlight.RelativePosition += new Vector2(x, y);

					OnSelected?.Invoke(Selected);
				}
			}
		}

		public T Add(T obj, Texture2D cell)
		{
			var spr = new Sprite(cell);

			return this.Add(obj, spr);
		}

		public T Add(T obj, Sprite cell)
		{
			elements.Add(obj);

			cell = cell.Clone();

			base.Add(cell);
			
			cell.Width = CellWidth;
			cell.Height = CellHeight;

			int page = 0, x = 0, y = 0;

			for (int i = 0; i < elementCount; i++)
			{
				x++;

				if (x >= cellsPerLine)
				{
					x = 0;
					y++;
				}

				if (y * cellsPerLine + x >= cellsPerPage)
				{
					page++;
					y = 1;
					x = 0;
				}
			}

			cell.RelativePosition = -new Vector2(Width, Height) / 2 + new Vector2(CellWidth, CellHeight) / 2;
			cell.RelativePosition += new Vector2(x * CellWidth, y * CellHeight);
			cell.RelativePosition += new Vector2(paddingX, paddingY);

			// Only visible if on current page
			cell.IsVisible = page == currentPage;

			elementCount++;

			return obj;
		}
	}
}
