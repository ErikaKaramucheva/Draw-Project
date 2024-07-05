using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
	class TriangleShape : Shape,ICloneable
	{

		#region Constructor

		public TriangleShape(RectangleF triangle) : base(triangle)
		{
		}

		public TriangleShape(TriangleShape triangle) : base(triangle)
		{
		}

		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към триъгълник.
		/// </summary>
		public override bool Contains(PointF point)
		{
			PointF a = new PointF(base.Rectangle.X, base.Rectangle.Y);
			PointF b = new PointF(base.Rectangle.X-Width, base.Rectangle.Height+base.Rectangle.Y);
			PointF c = new PointF(base.Rectangle.X+Width, base.Rectangle.Height + base.Rectangle.Y);
			double areaOfRealTriangle = Area(a, b, c);
			double areaOfTriangle1 = Area(point, b, c);
			double areaOfTriangle2 = Area(a,point, c);
			double areaOfTriangle3 = Area(a,b,point);
			if (areaOfRealTriangle==(areaOfTriangle1+areaOfTriangle2+areaOfTriangle3))
				// Проверка дали е в обекта само, ако точката е в обхващащия триъгълник.
				return true;
			else
				// Ако не е => false
				return false;
		}
		private double Area(PointF point1,PointF point2,PointF point3)
        {
			 return Math.Abs((point1.X * (point2.Y - point3.Y) +
						 point2.X * (point3.Y - point1.Y) +
						 point3.X * (point1.Y - point2.Y)) / 2.0);
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
			//ShapeName = "triangle";
			FillColor = Color.FromArgb(Opacity, FillColor);

			Point[] points = { 
				new Point((int)Rectangle.X,(int)Rectangle.Y),
				//new Point(((int)Rectangle.X-70),(int) Rectangle.Width),
				new Point(((int)Rectangle.X-(int)Width),(int) Rectangle.Y+(int)Height),
				//new Point((int)Rectangle.X+70,(int)Rectangle.Width) };
				new Point((int)Rectangle.X+(int)Width,(int)Rectangle.Y+(int)Height) };
			grfx.FillPolygon(new SolidBrush(FillColor), points);
			grfx.DrawPolygon(new Pen(StrokeColor,StrokeWidth),points);
			grfx.ResetTransform();
			if (Angle != 0)
			{
				base.ResetTransformation(grfx);
			}
			

		}

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
