using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
    class CurveLineShape:Shape,ICloneable
    {
		#region Constructor

		public CurveLineShape(RectangleF rect) : base(rect)
		{
		}

		public CurveLineShape(RectangleShape rectangle) : base(rectangle)
		{
		}

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към кривата линия.
        /// </summary>
        public override bool Contains(PointF point)
		{
			if((point.X>=Rectangle.X && point.X<=Rectangle.X+Width)&&
				(point.Y>=Rectangle.Y && point.Y<=Rectangle.Y+Height)) 
			//if (base.Contains(point))
				// Проверка дали е в обекта, ако е- true
				return true;
			else
				// Ако не е- false
				return false;
		}

		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			if (this.TransformationMatrix == null)
			{
				this.TransformationMatrix = new System.Drawing.Drawing2D.Matrix();
			}
			grfx.Transform = TransformationMatrix;

			FillColor = Color.FromArgb(Opacity, FillColor);
			Point[] points =
			{
				new Point((int)Rectangle.X,(int)Rectangle.Y+(int)(Height)),
				new Point((int)Rectangle.X+(int)(Width/5),(int)Rectangle.Y),
				new Point((int)Rectangle.X+(int)(Width/2.5),(int)Rectangle.Y+(int)Height),
				new Point((int)Rectangle.X+(int)(Width/1.6),(int)Rectangle.Y),
				new Point((int)Rectangle.X+(int)(Width/1.25),(int)Rectangle.Y+(int)(Height)),
				new Point((int)Rectangle.X+(int)Width,(int)Rectangle.Y)
			};

			grfx.DrawCurve(new Pen(StrokeColor, StrokeWidth),points);
			grfx.ResetTransform();
			if (base.Angle != 0)
			{
				base.ResetTransformation(grfx);

			}
		}
	}
}
