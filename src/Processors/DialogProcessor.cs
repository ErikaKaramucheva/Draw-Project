using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Draw

{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor

		public DialogProcessor()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Избран елемент.
		/// </summary>
		private List<Shape> selection = new List<Shape>();
		public List<Shape> Selection
		{
			get { return selection; }
			set { selection = value; }
		}
		/*private Shape selection;
		public Shape Selection
		{
			get { return selection; }
			set { selection = value; }
		}
		*/
		private List<GroupShape> groupList = new List<GroupShape>();
		public List<GroupShape> GroupList
		{
			get { return groupList; }
			set { groupList = value; }
		}
		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging
		{
			get { return isDragging; }
			set { isDragging = value; }
		}

		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation
		{
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		#endregion

		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			RectangleShape rect = new RectangleShape(new Rectangle(x, y, 100, 200));
			rect.FillColor = Color.White;
			rect.StrokeColor = Color.Black;
            rect.TransformationMatrix = new Matrix();
			rect.ShapeName = "Правоъгълник";
			ShapeList.Add(rect);
		}
		public void AddRandomEllipse()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 150));
			ellipse.FillColor = Color.White;
			ellipse.StrokeColor = Color.Black;
			ellipse.TransformationMatrix = new Matrix();
			ellipse.ShapeName = "Елипса";
			ShapeList.Add(ellipse);
		}

		public void AddRandomTriangle()
		{
			Random rnd = new Random();
			//x и y указват местоположението на върха на триъгълника
			int x = rnd.Next(100, 1000);
			//разминаване между y и z, за да гарантираме, че триъгълникът ще е обърнат с върха нагоре
			//иначе има вероятност да е завъртян с основата нагоре (към лентата с инструменти).
			int y = rnd.Next(100, 300);
			int z = rnd.Next(300, 600);

			TriangleShape triangle = new TriangleShape(new Rectangle(x, y, 70,200));
			triangle.FillColor = Color.White;
			triangle.StrokeColor = Color.Black;
			triangle.TransformationMatrix = new Matrix();
			triangle.ShapeName = "Триъгълник";
			ShapeList.Add(triangle);
		}

		public void AddRandomLine()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			LineShape line = new LineShape(new Rectangle(x, y, 300, 1));
			line.StrokeColor = Color.Black;
			line.TransformationMatrix = new Matrix();
			line.ShapeName = "Линия";
			ShapeList.Add(line);
		}
		public void AddRandomCurveLine()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			CurveLineShape line = new CurveLineShape(new Rectangle(x, y, 150, 10));
			line.StrokeColor = Color.Black;
			line.TransformationMatrix = new Matrix();
			line.ShapeName = "Крива линия";
			ShapeList.Add(line);
		}
		public void AddRandomHexagon()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			HexagonShape hexagon = new HexagonShape(new Rectangle(x, y, 120, 80));
			hexagon.FillColor = Color.White;
			hexagon.StrokeColor = Color.Black;
			hexagon.TransformationMatrix = new Matrix();
			hexagon.ShapeName = "Шестоъгълник";
			ShapeList.Add(hexagon);
		}

		/// <summary>
		/// Проверява дали дадена точка е в елемента.
		/// Обхожда в ред обратен на визуализацията с цел намиране на
		/// "най-горния" елемент т.е. този който виждаме под мишката.
		/// </summary>
		/// <param name="point">Указана точка</param>
		/// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
		public Shape ContainsPoint(PointF point)
		{

				for (int i = ShapeList.Count - 1; i >= 0; i--)
				{
					if (ShapeList[i].Contains(point))
					{
						//ShapeList[i].FillColor = Color.Red;

						return ShapeList[i];
					}
				}
			return null;
		}

		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{
			if (selection.Count > 0)
			{
				foreach (Shape item in Selection)
				{
					if (item != null)
					{
						item.Location = new PointF(item.Location.X + p.X - lastLocation.X,
							item.Location.Y + p.Y - lastLocation.Y);

					}
				}
			}
			lastLocation = p;

			/*if (selection!= null)
			{
				selection.Location = new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);
				lastLocation = p;
			}*/
		}

		public void SetStrokeColor(Color c)
		{
			foreach (Shape shape in Selection)
			{
				if (shape != null)
				{
					shape.StrokeColor = c;
				}
			}
		}
		public void SetFillColor(Color c)
		{
			foreach (Shape shape in Selection)
			{
				if (shape != null)
				{
					shape.FillColor = c;
				}
			}

		}
		public override void DrawShape(Graphics grfx, Shape items)
		{
			//if (Selection.Count > 0)
			//{
			base.DrawShape(grfx, items);
			float[] dashValues = { 4, 2 };
			Pen dashPen = new Pen(Color.Black, 3);
			dashPen.DashPattern = dashValues;
			//for every element in shape
			foreach (Shape item in Selection)
			{
				//GraphicsState state = grfx.Save();
				
				if (item.GetType().Name.ToString().Equals("GroupShape"))
				{
					//при коментиране на if-а не се показва прекъснатата линия
					if (item.TransformationMatrix != null)
					{
						grfx.Transform = item.TransformationMatrix;
                    }
                    else
                    {
						grfx.Transform = new Matrix();
                    }
					//item.TransformationMatrix.Multiply(grfx.Transform);
					grfx.DrawRectangle(
								//Pens.Black,
								dashPen,
								group.Location.X - 8,
								group.Location.Y - 8,
								group.Width + 16,
								group.Height + 16
								);
					
				}
				else
				{
					grfx.Transform = item.TransformationMatrix;
					if (item.GetType().Name.ToString().Equals("TriangleShape"))
					{
						grfx.DrawRectangle(
							//Pens.Black,
							dashPen,
							item.Location.X -item.Width-5,
							item.Location.Y - 5,
							item.Width + item.Width+10,
							item.Height + 12
							);
					}
					else if (item.GetType().Name.ToString().Equals("LineShape"))
					{
						grfx.DrawRectangle(
							//Pens.Black,
							dashPen,
							item.Location.X - 7,
							item.Location.Y - 5,
							item.Width + 16,
							item.Height + 12
							);
					}
					else if (item.GetType().Name.ToString().Equals("CurveLineShape"))
					{
						grfx.DrawRectangle(
							//Pens.Black,
							dashPen,
							item.Location.X - 7,
							item.Location.Y - 5,
							item.Width + 16,
							item.Height + 20
							);
					}
					else if (item.GetType().Name.ToString().Equals("HexagonShape"))
					{
						grfx.DrawRectangle(
							//Pens.Black,
							dashPen,
							item.Location.X - (item.Width/4)-5,
							item.Location.Y - 5,
							item.Width + 12,
							item.Height + 12
							);
					}
					else
					{
						grfx.DrawRectangle(
							//Pens.Black,
							dashPen,
							item.Location.X - 8,
							item.Location.Y - 8,
							item.Width + 16,
							item.Height + 16
							);
						//grfx.Restore(state);
					}
				}
				//grfx.Restore(state);
			}
			
			
		}
		public void RotatePrimitive(int angle)
		{
			foreach (Shape shape in Selection)
			{
				//priviousMatrix = shape.TransformationMatrix.Clone();
				shape.Angle = angle;
				if (shape != null)
				{
					if (shape.GetType().Name.ToString().Equals("GroupShape"))
					{
						group.RotateGroup((GroupShape)shape, angle);
					}
					else
					{
						shape.TransformationMatrix.RotateAt(
						angle,
						new PointF(
							shape.Location.X + shape.Width / 2,
							shape.Location.Y + shape.Height / 2
							));
					}
				}
			}
		}
		public void ScalePrimitive(float scaleFactorX,float scaleFactorY)
		{
			//или просто вземаме scale и долу го подаваем 2 пъти
			foreach (Shape shape in Selection)
			{
				if (shape != null)
				{
					if (shape.GetType().Name.ToString().Equals("GroupShape"))
					{
						group.ScaleGroup((GroupShape)shape, scaleFactorX,scaleFactorY);
					}else
					shape.TransformationMatrix.Scale(scaleFactorX, scaleFactorY);
				}
			}
		}

		public void SetStrokeWidth(int strokeWidth)
		{
			foreach (Shape shape in Selection)
			{
				shape.StrokeWidth = strokeWidth;
			}

		}

		public void SelectAll()
		{
			foreach (Shape item in ShapeList)
			{
				selection.Add(item);
			}

		}
		public void Unselect()
		{
			
			Selection = new List<Shape>();

		}
		public void RemoveAll()
        {
			
			ShapeList = new List<Shape>();
			Selection = new List<Shape>();
			
        }

		public void SetFillOpacity(int value)
		{
			foreach (Shape item in Selection)
			{
				if (item != null)
				{
					item.Opacity = value;
				}
			}

		}
		GroupShape group;
		public void CreateGroupShape()
        {
			if (selection.Count < 2)
			{
				return;
			}
			float minX=float.MaxValue;
			float maxX=float.MinValue;
			float minY=float.MaxValue;
			float maxY=float.MinValue;
			foreach (Shape item in Selection) {
				if (item.Location.X < minX)
				{
					minX = item.Location.X;

                }
                if (item.Location.Y < minY)
                {
					minY = item.Location.X;
                }
				if(item.Location.X+item.Width > maxX)
                {
					maxX = item.Location.X + item.Width;
                }
				if (item.Location.Y + item.Height > maxY)
				{
					maxY = item.Location.Y + item.Height;
				}
				//iff item.X+item.Width>maxX ->maxX=item.X+item.Width
				//iff item.Y+item.Width>maxY ->maxY=item.Y+item.Width


			}
			int width = (int)maxX - (int)minX;
			int height = (int)maxY - (int)minY;
			group = new GroupShape(new Rectangle((int)minX, (int)minY,width,height));
			//group = new GroupShape(new Rectangle((int)maxX, (int)maxY, width, height));
			group.ShapeName = "Група";
			foreach (Shape shape in Selection)
			{
				group.SubShape.Add(shape);
				ShapeList.Remove(shape);
			}

			ShapeList.Add(group);
			GroupList.Add(group);
			Selection = new List<Shape>();
			Selection.Add(group);

			//group.SetGroupFillColor(color:Color.Red);
        }
		public void RemoveGrouping()
        {
			List<Shape> shape = new List<Shape>();
			groupList.Remove(group);
			if (group != null && group.SubShape.Count > 1)
			{
				foreach (Shape item in group.SubShape)
				{
					shape.Add(item);
					ShapeList.Remove(item);
					selection.Remove(item);
					//group.SubShape.Remove(item);
				}


				ShapeList.AddRange(shape);
				group.SubShape.Clear();
				//groupList.Clear();

				Selection.Clear();
				//Selection = new List<Shape>();
				Selection.AddRange(shape);
			}

		}

		private List<Shape> copy = new List<Shape>();
		public List<Shape> Copy
		{
			get { return copy; }
			set {copy = value; }
		}
		public void CopyItem()
		{
			if (Selection.Count > 0)
			{
				foreach (Shape shape in Selection)
				{
					if (shape != null)
					{
						Copy.Add(shape);
					}
				}
			}
		}
		public void PasteItem()
        {
			if (Copy.Count > 0)
			{
				foreach (Shape shape in Copy)
				{
					var x = shape.Location.X + 150;
					var y = shape.Location.Y + 150;
					PointF loc = new PointF(x, y);
					Shape newShape;

					// if (shape.ShapeName.Equals( "triangle"))
					if (shape.GetType().Name.ToString().Equals("TriangleShape"))
					{
						newShape = new TriangleShape(shape.Rectangle);
					}
					else if (shape.GetType().Name.ToString().Equals("EllipseShape"))
					{
						newShape = new EllipseShape(shape.Rectangle);
					}
					else if (shape.GetType().Name.ToString().Equals("LineShape"))
					{
						newShape = new LineShape(shape.Rectangle);
					}
					else if (shape.GetType().Name.ToString().Equals("CurveLineShape"))
					{
						newShape = new CurveLineShape(shape.Rectangle);
					}
					else if (shape.GetType().Name.ToString().Equals("HexagonShape"))
					{
						newShape = new HexagonShape(shape.Rectangle);
					}
					else if (shape.GetType().Name.ToString().Equals("GroupShape"))
					{

						newShape = new GroupShape(shape.Rectangle);
					}
					else
					{
						newShape = new RectangleShape(shape.Rectangle);
					}

					newShape.Location = loc;
					newShape.Width = shape.Width;
					newShape.FillColor = shape.FillColor;
					newShape.StrokeColor = shape.StrokeColor;
					newShape.Angle = shape.Angle;
					newShape.Height = shape.Height;
					newShape.ShapeName = shape.ShapeName;
					newShape.Opacity = shape.Opacity;
					newShape.Rectangle = shape.Rectangle;
					newShape.Scale = shape.Scale;
					newShape.StrokeWidth = shape.StrokeWidth;
					newShape.TransformationMatrix = shape.TransformationMatrix.Clone();

					ShapeList.Add(newShape);
					
				}
				//Copy = new List<Shape>();
				Copy.Clear();
			}
        }

       public void Save(string name)
        {
			BinaryFormatter formatter = new BinaryFormatter();
			using (FileStream fileStream = new FileStream(name, FileMode.Create,FileAccess.Write))
			{
				/*foreach(var shape in ShapeList)
                {
					formatter.Serialize(fileStream, shape);
				}*/
				formatter.Serialize(fileStream, ShapeList);
			}

		}
		public void Deserialization(string name)
		{ 
			BinaryFormatter formatter = new BinaryFormatter();
			using (FileStream fileStream = new FileStream(name, FileMode.Open, FileAccess.Read))
			{
				ShapeList.Clear();
				var shapes=(List<Shape>) formatter.Deserialize(fileStream);
				ShapeList.AddRange(shapes);
				
			}
		}
		public void DeletePrimitive()
        {
            if (Selection.Count > 0)
            {
				foreach(Shape shape in Selection)
                {
					ShapeList.Remove(shape);
                }
				
            }
			Selection = new List<Shape>();
        }
		public void SelectPrimitiveByType(String shape)
        {
			Selection.Clear();
			foreach (Shape item in ShapeList)
			{
				if (item.GetType().Name.ToString().Equals(shape)) {
					Selection.Add(item);

			}
			}
        }

		public string GetName(Shape shape)
        {
			return shape.ShapeName;
        }

		public void GetPrimitivesByName(string name)
        {
            if (name=="")
            {
				return;
            }
			List<Shape> shapes = new List<Shape>();
			foreach(var shape in ShapeList)
            {
				
                if (shape.ShapeName.Contains(name))
                {
					shapes.Add(shape);
                }
            }
			Selection.Clear();
			Selection.AddRange(shapes);


        }
    }
	}

    
