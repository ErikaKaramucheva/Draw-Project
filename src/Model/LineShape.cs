using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
    class LineShape:Shape,ICloneable
    {
		#region Constructor

		public LineShape(RectangleF rect) : base(rect)
		{
		}

		public LineShape(RectangleShape rectangle) : base(rectangle)
		{
		}

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към линията.
        /// </summary>
        public override bool Contains(PointF point)
		{
			if ((Rectangle.Y == point.Y || point.Y==Rectangle.Y+1 ||point.Y==Rectangle.Y-1) && (point.X >= Rectangle.X && point.X <= (Rectangle.X + Width))){
				// Проверка дали принадлежи на линията.
				// Ако принадлежи- true
				return true;
			}
			else
				// Ако не => false
				return false;
		}

		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			//ShapeName = "line";
			if (this.TransformationMatrix == null)
			{
				grfx.Transform = new System.Drawing.Drawing2D.Matrix();
				this.TransformationMatrix = grfx.Transform;
			}
			else
			{
				grfx.Transform = TransformationMatrix;
			}
			//grfx.Transform = TransformationMatrix;
			//grfx.FillRectangle(new SolidBrush(FillColor), points;

			grfx.DrawRectangle(new Pen(StrokeColor, StrokeWidth),
				Rectangle.X, Rectangle.Y, Width,Height);
			//grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 
			//new Point((int)Rectangle.X, (int)Rectangle.Y),
			//new Point((int)Rectangle.X + 300, (int)Rectangle.Y));
			grfx.ResetTransform();
			if (base.Angle != 0)
			{
				base.ResetTransformation(grfx);

			}
		}
	}
}
