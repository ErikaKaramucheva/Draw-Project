using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
    class HexagonShape:Shape,ICloneable
    {
		#region Constructor

		public HexagonShape(RectangleF rect) : base(rect)
		{
		}

		public HexagonShape(RectangleShape rectangle) : base(rectangle)
		{
		}

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към шестоъгълника.
        /// </summary>
        public override bool Contains(PointF point)
		{
			if ((point.X>=base.Rectangle.X - (int)(Width / 4) && point.X<=base.Rectangle.X+ (int)(3*Width / 4))
				&& (point.Y>=base.Rectangle.Y && point.Y<=base.Rectangle.Y+Height))
				// Проверка дали е в обекта само, ако точката е в обхващащия шестоъгълник.
				// ако да-true
				return true;
			else
				// Ако не е => false
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
				grfx.Transform = new System.Drawing.Drawing2D.Matrix();
				this.TransformationMatrix = grfx.Transform;
			}
			else
			{
				grfx.Transform = TransformationMatrix;
			}
			//grfx.Transform = TransformationMatrix;
			FillColor = Color.FromArgb(Opacity, FillColor);
            Point[] points = { 
				new Point((int)Rectangle.X,(int)Rectangle.Y),
				new Point((int)Rectangle.X-(int)(Width/4),(int)Rectangle.Y+(int)Height/2),
				new Point((int)Rectangle.X,(int)Rectangle.Y+(int)Height),
				new Point((int)Rectangle.X+(int)(Width/2),(int)Rectangle.Y+(int)Height),
				new Point((int)Rectangle.X+(int)(3*Width/4),(int)Rectangle.Y+(int)Height/2),
				new Point((int)Rectangle.X+(int)(Width/2),(int)Rectangle.Y)
			};

            grfx.FillPolygon(new SolidBrush(FillColor),points );
			grfx.DrawPolygon(new Pen(StrokeColor, StrokeWidth),points );
			grfx.ResetTransform();
			if (base.Angle != 0)
			{
				base.ResetTransformation(grfx);

			}
		}
	}
}
