using System;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	[Serializable]
	public class RectangleShape : Shape,ICloneable
	{
		#region Constructor
		
		public RectangleShape(RectangleF rect) : base(rect)
		{
		}
		
		public RectangleShape(RectangleShape rectangle) : base(rectangle)
		{
		}
		public RectangleShape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.StrokeWidth = shape.StrokeWidth;
			this.Location = shape.Location;
			
			this.FillColor = shape.FillColor;
			this.StrokeColor = shape.StrokeColor;
			this.TransformationMatrix = shape.TransformationMatrix;

		}

		public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към правоъгълника.
        /// В случая на правоъгълник този метод може да не бъде пренаписван, защото
        /// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
        /// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
        /// елемента в този случай).
        /// </summary>
        public override bool Contains(PointF point)
		{
			//matrix invert
			//transforms point
			//matrix invert
			//transforms point- трансформиране на точката, в която е кликнал потребителя към сегашната координатна система.
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
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
            else { 
				grfx.Transform = TransformationMatrix;
		}
			//ShapeName = "rectangle";
			FillColor = Color.FromArgb(Opacity, FillColor);
			SolidBrush brush = new SolidBrush(FillColor);
			grfx.FillRectangle(brush,
				Rectangle.X, 
				Rectangle.Y,
				Rectangle.Width, 
				Rectangle.Height);

			grfx.DrawRectangle(new Pen(StrokeColor,StrokeWidth),
				Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.ResetTransform();
            if (base.Angle != 0)
            {
				grfx.ResetTransform();
				base.ResetTransformation(grfx);

			}
		}
	}
}
