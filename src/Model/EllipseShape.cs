using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Draw.src.Model
{
	[Serializable]
	class EllipseShape : Shape,ICloneable
	{
		#region Constructor

		public EllipseShape(RectangleF elipse) : base(elipse)
		{
		}

		public EllipseShape(EllipseShape ellipse) : base(ellipse)
		{
		}

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към елипса.
        /// </summary>
        public override bool Contains(PointF point)
		{
			var x = point.X;
			var y = point.Y;
			var ry = Rectangle.Height / 2;
			var rx = Rectangle.Width / 2;
			var k = (Rectangle.Height / 2) + this.Location.Y;
			var h = (Rectangle.Width / 2) + this.Location.X;
			var result = (((x - h) * (x - h)) / (rx * rx)) + (((y - k) * (y - k)) / (ry * ry));
			if (base.Contains(point) && result <= 0.9)
			{
				// Проверка дали е в обекта само, ако точката е в елипсата.
				return true;
			}
			else
				// Ако не е  => false
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

			grfx.FillEllipse(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(StrokeColor, StrokeWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.ResetTransform();
			if (base.Angle != 0)
			{
				base.ResetTransformation(grfx);

			}
		}
	}
}

