using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
   public class GroupShape:Shape
    {
       
			#region Constructor

			public GroupShape(RectangleF rect) : base(rect)
			{
			}
		
		public GroupShape(RectangleShape rectangle) : base(rectangle)
			{
			}

		#endregion

		public List<Shape> SubShape = new List<Shape>();
			/// <summary>
			/// Проверка за принадлежност на точка point към елипса.
			/// </summary>
			public override bool Contains(PointF point)
			{
			foreach(Shape item in SubShape)
            {
                if (item.Contains(point))
                {
					return true;
                }
            }
			return false;
			}
		public override Color FillColor
        {
			get => base.FillColor;
			set
			{
				foreach (Shape item in SubShape)
				{
					item.FillColor = value;
				}
			}
        }
		public override Color StrokeColor
		{
			get => base.StrokeColor;
			set
			{
				foreach (Shape item in SubShape)
				{
					item.StrokeColor = value;
				}
			}
		}
        public override int StrokeWidth
		{ get => base.StrokeWidth;
			set
			{
				foreach (Shape item in SubShape)
				{
					item.StrokeWidth = value;
				}
			}
		}

		public override PointF Location
		{
			get { return base.Location; }
			set
			{
				foreach (Shape item in SubShape)
				{
					item.Location = new PointF(item.Location.X - Location.X + value.X, item.Location.Y - Location.Y + value.Y);
				}
				base.Location = value;
			}

		}
		public override int Opacity
		{
			get => base.Opacity; 
			set
			{
				foreach (Shape item in SubShape)
				{
					item.Opacity = value;
				}
			}
		}
		public override float Angle
		{
			get => base.Angle;
			set
			{
				foreach (Shape item in SubShape)
				{
					item.Angle = value;
				}
			}
		}
        public override float Scale 
		{ get => base.Scale; 
			set
			{
				foreach (Shape item in SubShape)
				{
					item.Scale = value;
				}
			}
		}
		public override Matrix TransformationMatrix
		{
			get 
			{ return base.TransformationMatrix; }
			set
			{
				foreach (Shape item in SubShape)
				{
					item.TransformationMatrix.Multiply(value);
				}
			}
		}

		public void RotateGroup(GroupShape shape, int angle)
        {
			foreach(var item in shape.SubShape)
            {
				item.TransformationMatrix.RotateAt(
					angle,
						new PointF(
							item.Location.X + item.Width / 2,
							item.Location.Y + item.Height / 2
							));
				//this.TransformationMatrix = item.TransformationMatrix;
			}
        }
		public void ScaleGroup(GroupShape shape, float x, float y)
		{
			foreach (var item in shape.SubShape)
			{
				item.TransformationMatrix.Scale(x, y);
			}
		}
		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
			{
			//base.DrawSelf(grfx);
			foreach (Shape item in SubShape)
			{
				item.DrawSelf(grfx);
			}
			//for every item in subshape
			//visulize
		}
		
		}
	}
