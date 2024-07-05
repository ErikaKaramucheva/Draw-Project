using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
	/// </summary>
	 [Serializable]
	public abstract class Shape
	{
		#region Constructors
		
		public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
		}
		
		public Shape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.Location = shape.Location;
			this.Rectangle = shape.Rectangle;
			this.FillColor =  shape.FillColor;
			this.Angle = shape.Angle;
			this.Opacity = shape.Opacity;
			this.Scale = shape.Scale;
			this.StrokeColor = shape.StrokeColor;
			this.StrokeWidth = shape.StrokeWidth;
		}
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Обхващащ правоъгълник на елемента.
		/// </summary>
		private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}
		[NonSerialized]
		private Matrix transformationMatrix;
		public virtual Matrix TransformationMatrix
		{
			get { return transformationMatrix; }
			set { transformationMatrix = value; }
		}

		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;		
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}

		private string shapeName;
		public virtual string ShapeName
		{
			get { return shapeName; }
			set { shapeName= value; }
		}

		private Color strokeColor = Color.Black;
		public virtual Color StrokeColor 
		{
			get { return strokeColor; }
			set { strokeColor = value; }
		}
		private int strokeWidth = 1;
		public virtual int StrokeWidth
		{
			get { return strokeWidth; }
			set { strokeWidth = value; }
		}
		private int opacity = 255;
		public virtual int Opacity
		{
			get { return opacity; }
			set { opacity = value; }
		}
		private float angle = 0;
		public virtual float Angle
		{
			get { return angle; }
			set { angle = value; }

		}
		private float scale = 1;
		public virtual float Scale
		{
			get { return scale; }
			set { scale = value; }

		}
		//private Brush brush;
		//public virtual Brush Brush { 
		//get { return brush; }
		//set { brush = value; } 
		//}
		#endregion


		/// <summary>
		/// Проверка дали точка point принадлежи на елемента.
		/// </summary>
		/// <param name="point">Точка</param>
		/// <returns>Връща true, ако точката принадлежи на елемента и
		/// false, ако не пренадлежи</returns>
		public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}
		
		/// <summary>
		/// Визуализира елемента.
		/// </summary>
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}

        public static implicit operator Graphics(Shape v)
        {
            throw new NotImplementedException();
        }

		public virtual void ResetTransformation(Graphics grfx)
        {
			grfx.TranslateTransform(
					-(Rectangle.X + Rectangle.Width / 2),
							(-(Rectangle.Y + Rectangle.Height / 2)));
		}
		
	}
}
