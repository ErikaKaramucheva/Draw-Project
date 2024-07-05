using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Aspose.Words;
using Draw.src.GUI;

namespace Draw
{

	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();

		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}

		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				Shape temp = dialogProcessor.ContainsPoint(e.Location);
				if (temp != null)
				{
					if (dialogProcessor.Selection.Contains(temp))
					{
						dialogProcessor.Selection.Remove(temp);
					}
					else
					{
						dialogProcessor.Selection.Add(temp);
					}
				}
				//dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				//if (dialogProcessor.Selection != null)
				//{
				statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
				dialogProcessor.IsDragging = true;
				dialogProcessor.LastLocation = e.Location;
				viewPort.Invalidate();
				//}
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging)
			{
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

		private void DrawEllipseBtn_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomEllipse();

			statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

			viewPort.Invalidate();
		}
		private void DrawTriangleBtn_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomTriangle();

			statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

			viewPort.Invalidate();
		}

		private void DrawLineBtn_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomLine();
			statusBar.Items[0].Text = "Последно действие: Рисуване на линия";

			viewPort.Invalidate();
		}
		private void FillCollorPaletteBtn_Click(object sender, EventArgs e)
		{

			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				foreach (Shape item in dialogProcessor.Selection)

					if (dialogProcessor.Selection != null)
					{
						item.FillColor = colorDialog1.Color;
						//dialogProcessor.Selection.FillColor = colorDialog1.Color;
					}
				FillCollorPaletteBtn.BackColor = colorDialog1.Color;
			}

			viewPort.Invalidate();
			statusBar.Items[0].Text = "Последно действие: Оцветяване на примитив";

		}

		private void StrokeColorPalette_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				foreach (Shape item in dialogProcessor.Selection)
				{

					if (dialogProcessor.Selection != null)
					{
						item.StrokeColor = colorDialog1.Color;
					}
				}
				//StrokeColorPalette.BackColor = colorDialog1.Color;
				StrokeColorPalette.BackColor = colorDialog1.Color;

			}

			viewPort.Invalidate();
			statusBar.Items[0].Text = "Последно действие: Оцветяване контура на примитива";
		}

		private void DrawHexagonBtn_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomHexagon();

			statusBar.Items[0].Text = "Последно действие: Рисуване на шестоъгълник";

			viewPort.Invalidate();
		}

		private void DrawCurveLineBtn_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomCurveLine();
			statusBar.Items[0].Text = "Последно действие: Рисуване на крива линия";

			viewPort.Invalidate();
		}
		private void OnePxBtn_Click(object sender, EventArgs e)
		{
			foreach (Shape shape in dialogProcessor.Selection)
			{
				if (shape != null)
				{
					shape.StrokeWidth = 1;
				}
			}
			viewPort.Invalidate();
			statusBar.Items[0].Text = "Последно действие: Оразмеряване дебелината на контура";
		}

		private void TwoPxBtn_Click(object sender, EventArgs e)
		{
			foreach (Shape shape in dialogProcessor.Selection)
			{
				if (shape != null)
				{
					shape.StrokeWidth = 2;
				}
			}
			viewPort.Invalidate();
			statusBar.Items[0].Text = "Последно действие: Оразмеряване дебелината на контура";
		}

		private void FivePxBtn_Click(object sender, EventArgs e)
		{
			foreach (Shape shape in dialogProcessor.Selection)
			{
				if (shape != null)
				{
					shape.StrokeWidth = 5;
				}
			}
			statusBar.Items[0].Text = "Последно действие: Оразмеряване дебелината на контура";
			viewPort.Invalidate();
		}

		private void TenPxBtn_Click(object sender, EventArgs e)
		{
			foreach (Shape shape in dialogProcessor.Selection)
			{
				if (shape != null)
				{
					shape.StrokeWidth = 10;
				}
			}
			viewPort.Invalidate();
			statusBar.Items[0].Text = "Последно действие: Оразмеряване дебелината на контура";

		}

		private void OpacityScrBr_Click(object sender, EventArgs e)
		{
			dialogProcessor.SetFillOpacity(OpacityScrBr.Value);
			viewPort.Invalidate();
			statusBar.Items[0].Text = "Последно действие: Промяна прозрачността на примитива";
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			save.Filter = "png(*.png)|*.png";
			if (save.ShowDialog() == DialogResult.OK)
			{
				dialogProcessor.Save(save.FileName);
			}
			statusBar.Items[0].Text = "Последно действие: Записване на изображението";
			viewPort.Invalidate();
			/*SaveFileDialog save = new SaveFileDialog();
			save.Filter = "PNG(*.PNG)|*.png";
			img = viewPort.BackgroundImage;
			img.Save(save.FileName);*/
		}

		private void ZeroDegreeRotateBtn(object sender, EventArgs e)
		{
			dialogProcessor.RotatePrimitive(45);
			statusBar.Items[0].Text = "Последно действие: Завъртане надясно";
			viewPort.Invalidate();
		}

		private void FortyFiveRotateBtn(object sender, EventArgs e)
		{
			dialogProcessor.RotatePrimitive(-45); statusBar.Items[0].Text = "Последно действие: Завъртане наляво";
			viewPort.Invalidate();
		}

		private void CreateGroup_Click(object sender, EventArgs e)
		{
			dialogProcessor.CreateGroupShape();
			statusBar.Items[0].Text = "Последно действие: Създаване на група";
			viewPort.Invalidate();
		}

		private void removeGroup_Click(object sender, EventArgs e)
		{
			dialogProcessor.RemoveGrouping();
			statusBar.Items[0].Text = "Последно действие: Разгрупиране на елементи";
			viewPort.Invalidate();
		}

		private void SelectAll(object sender, EventArgs e)
		{
			dialogProcessor.SelectAll();
			statusBar.Items[0].Text = "Последно действие: Селектиране на всичко";
			viewPort.Invalidate();
		}
		private void RemoveAll(object sender, EventArgs e)
		{
			dialogProcessor.RemoveAll();
			statusBar.Items[0].Text = "Последно действие: Премахване на всичко";
			viewPort.Invalidate();
		}

		/*private void ChangeName(object sender, EventArgs e)
        {
			foreach (Shape item in dialogProcessor.Selection)
			{
				if (dialogProcessor.Selection != null)
				{
					item.ShapeName = NameTextBox.Text;
				}
			}
			viewPort.Invalidate();
		}*/

		private void viewPort_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.G && e.Control == true && e.Shift == false)
			{
				dialogProcessor.CreateGroupShape();
				statusBar.Items[0].Text = "Последно действие: Групиране на елементи";
			}
			else if (e.KeyCode == Keys.U && e.Control == true && e.Shift == false)
			{
				dialogProcessor.RemoveGrouping();
				statusBar.Items[0].Text = "Последно действие: Разгрупиране на елементи";
			}
			else if (e.KeyCode == Keys.A && e.Control == true && e.Shift == false)
			{
				dialogProcessor.SelectAll();
				statusBar.Items[0].Text = "Последно действие: Селектиране на всичко";
			}
			else if (e.KeyCode == Keys.R && e.Control == true && e.Shift == false)
			{
				dialogProcessor.Unselect();
				statusBar.Items[0].Text = "Последно действие: Деселектиране на примитиви";
			}
			else if (e.KeyCode == Keys.D && e.Control == true)
			{
				dialogProcessor.RemoveAll();
				statusBar.Items[0].Text = "Последно действие: Премахване на всичко";
			}
			else if (e.KeyCode == Keys.C && e.Control == true)
			{
				dialogProcessor.CopyItem();
				statusBar.Items[0].Text = "Последно действие: Копиране на примитиви";
			}
			else if (e.KeyCode == Keys.V && e.Control == true)
			{
				dialogProcessor.PasteItem();
				statusBar.Items[0].Text = "Последно действие: Поставяне на примитиви";
			}
			else if (e.KeyCode == Keys.S && e.Control == true)
			{
				saveToolStripMenuItem_Click(sender, e);
				statusBar.Items[0].Text = "Последно действие: Записване на изображението";
			}
			else if (e.KeyCode == Keys.O && e.Control == true)
			{
				OpenBtn(sender, e);
				//statusBar.Items[0].Text = "Последно действие: Отваряне на съществуващ файл";
			}
			else if (e.KeyCode == Keys.Back && e.Control == true)
			{
				dialogProcessor.DeletePrimitive();
				statusBar.Items[0].Text = "Последно действие: Изтриване на примитив/и";
			}
			else if (e.KeyCode == Keys.D1 && e.Control == true)
			{
				dialogProcessor.AddRandomRectangle();
				statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			}
			else if (e.KeyCode == Keys.D2 && e.Control == true)
			{
				dialogProcessor.AddRandomEllipse();
				statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";
			}
			else if (e.KeyCode == Keys.D3 && e.Control == true)
			{
				dialogProcessor.AddRandomLine();
				statusBar.Items[0].Text = "Последно действие: Рисуване на линия";
			}
			else if (e.KeyCode == Keys.D4 && e.Control == true)
			{
				dialogProcessor.AddRandomCurveLine();
				statusBar.Items[0].Text = "Последно действие: Рисуване на крива линия";
			}
			else if (e.KeyCode == Keys.D5 && e.Control == true)
			{
				dialogProcessor.AddRandomTriangle();
				statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";
			}
			else if (e.KeyCode == Keys.D6 && e.Control == true)
			{
				dialogProcessor.AddRandomHexagon();
				statusBar.Items[0].Text = "Последно действие: Рисуване на шестоъгълник";
			}
			else if (e.KeyCode == Keys.D1 && e.Shift == true)
			{
				selectRectangleBtn(sender, e);
			}
			else if (e.KeyCode == Keys.D2 && e.Shift == true)
			{
				selectEllipseBtn(sender, e);
			}
			else if (e.KeyCode == Keys.D3 && e.Shift == true)
			{
				selectLineBtn(sender, e);
			}
			else if (e.KeyCode == Keys.D4 && e.Shift == true)
			{
				selectCurveLineBtn(sender, e);
			}
			else if (e.KeyCode == Keys.D5 && e.Shift == true)
			{
				selectTriangleBtn(sender, e);
			}
			else if (e.KeyCode == Keys.D6 && e.Shift == true)
			{
				selectHexagonBtn(sender, e);
			}
			else if (e.KeyCode == Keys.Right && e.Control == true)
			{
				dialogProcessor.RotatePrimitive(45);
				statusBar.Items[0].Text = "Последно действие: Завъртане надясно";
			}
			else if (e.KeyCode == Keys.Left && e.Control == true)
			{
				dialogProcessor.RotatePrimitive(-45);
				statusBar.Items[0].Text = "Последно действие: Завъртане наляво";
			}

			viewPort.Invalidate();
		}

		private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			contextMenuStrip2.Items.Clear();
			if (dialogProcessor.Selection.Count > 0)
			{
				contextMenuStrip2.Items.Add(
					"Rotate 90 C", null
						//	new EventHandler(
						//e.DrawRec

						//)
						);
			}
			else
			{
				contextMenuStrip2.Items.Add(
					"New Rectangle",
					null);
				//new EventHandler(
				//DrawRectangle()

				//});
			}

		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MainForm newForm = new MainForm();
			//this.Close();
			newForm.Show();
			
		}

		private void Delete(object sender, EventArgs e)
		{
			dialogProcessor.DeletePrimitive();
			statusBar.Items[0].Text = "Последно действие: Изтриване на примитив/и";
			viewPort.Invalidate();
		}

		private void PasteBtn(object sender, EventArgs e)
		{
			dialogProcessor.PasteItem();
			statusBar.Items[0].Text = "Последно действие: Поставяне на примитиви";
			viewPort.Invalidate();
		}

		private void CopyBtn(object sender, EventArgs e)
		{
			dialogProcessor.CopyItem();
			statusBar.Items[0].Text = "Последно действие: Копиране на примитиви";
			viewPort.Invalidate();
		}

		private void selectRectangleBtn(object sender, EventArgs e)
		{
			dialogProcessor.SelectPrimitiveByType("RectangleShape");
			statusBar.Items[0].Text = "Последно действие: Селектиране на всички правоъгълници";
			viewPort.Invalidate();
		}

		private void selectLineBtn(object sender, EventArgs e)
		{
			dialogProcessor.SelectPrimitiveByType("LineShape");
			statusBar.Items[0].Text = "Последно действие: Селектиране на всички линии";
			viewPort.Invalidate();
		}

		private void selectCurveLineBtn(object sender, EventArgs e)
		{
			dialogProcessor.SelectPrimitiveByType("CurveLineShape");
			statusBar.Items[0].Text = "Последно действие: Селектиране на всички криви линии";
			viewPort.Invalidate();
		}

		private void selectTriangleBtn(object sender, EventArgs e)
		{
			dialogProcessor.SelectPrimitiveByType("TriangleShape");
			statusBar.Items[0].Text = "Последно действие: Селектиране на всички триъгълници";
			viewPort.Invalidate();
		}

		private void selectEllipseBtn(object sender, EventArgs e)
		{
			dialogProcessor.SelectPrimitiveByType("EllipseShape");
			statusBar.Items[0].Text = "Последно действие: Селектиране на всички елипси";
			viewPort.Invalidate();
		}

		private void selectHexagonBtn(object sender, EventArgs e)
		{
			dialogProcessor.SelectPrimitiveByType("HexagonShape");
			statusBar.Items[0].Text = "Последно действие: Селектиране на всички шестоъгълници";
			viewPort.Invalidate();
		}

		private void ScaleMinusBtn(object sender, EventArgs e)
		{
			dialogProcessor.ScalePrimitive((float)0.9, (float)0.9);
			statusBar.Items[0].Text = "Последно действие: Намаляне размера на примитив";
			viewPort.Invalidate();
		}

		private void ScalePlusBtn(object sender, EventArgs e)
		{
			dialogProcessor.ScalePrimitive((float)1.1, (float)1.1);
			statusBar.Items[0].Text = "Последно действие: Мащабиране на примитив";
			viewPort.Invalidate();
		}

		private void FlipBtn(object sender, EventArgs e)
		{
			dialogProcessor.RotatePrimitive(180);
			statusBar.Items[0].Text = "Последно действие: Отражение";
			viewPort.Invalidate();
		}

		private void OpenBtn(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "png(*.png)|*.png";
			openFileDialog.Title = "Отваряне на файл";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				//dialogProcessor.ShapeList = dialogProcessor.Deserialization(openFileDialog.FileName);
				dialogProcessor.Deserialization(openFileDialog.FileName);
			}
			statusBar.Items[0].Text = "Последно действие: Отваряне на файл";
			viewPort.Invalidate();
		}

		private void ExportToBMPBtn(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			save.Filter = "bmp(*.bmp)|*.bmp";
			if (save.ShowDialog() == DialogResult.OK)
			{
				Bitmap bitmap = new Bitmap(viewPort.Width, viewPort.Height);
				viewPort.DrawToBitmap(bitmap, new Rectangle(Point.Empty, viewPort.Size));

				bitmap.Save(save.FileName, ImageFormat.Bmp);

			}
			statusBar.Items[0].Text = "Последно действие: Експортиране на изображението като bitmap файл";
			viewPort.Invalidate();
		}

		private void ExportToJPEG(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			save.Filter = "jpeg(*.jpeg)|*.jpeg";
			if (save.ShowDialog() == DialogResult.OK)
			{
				Bitmap bitmap = new Bitmap(viewPort.Width, viewPort.Height);
				viewPort.DrawToBitmap(bitmap, new Rectangle(Point.Empty, viewPort.Size));

				bitmap.Save(save.FileName, ImageFormat.Jpeg);
			}
			statusBar.Items[0].Text = "Последно действие: Експортиране на изображението като .jpeg файл";
			viewPort.Invalidate();
		}

		private void ExportToPDFBtn(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			save.Filter = "pdf(*.pdf)|*.pdf";
			if (save.ShowDialog() == DialogResult.OK)
			{
				Bitmap bitmap = new Bitmap(viewPort.Width, viewPort.Height);
				viewPort.DrawToBitmap(bitmap, new Rectangle(Point.Empty, viewPort.Size));
				var doc = new Document();
				var builder = new DocumentBuilder(doc);
				//builder.InsertImage(bitmap);
				builder.PageSetup.PageWidth = bitmap.Width;
				builder.PageSetup.PageHeight = bitmap.Height;
				builder.InsertImage(bitmap);

				doc.Save(save.FileName);
			}
			statusBar.Items[0].Text = "Последно действие: Експортиране на изображението като .pdf файл";
			viewPort.Invalidate();
		}

		private void PrimName(object sender, EventArgs e)
		{
			NameTextBox.Visible = Visible;
			NameTextBox.Focus();
			//statusBar.Items[0].Text = "Последно действие: Задаване име на примитивите "+NameTextBox.Text;
			//NameTextBox.Visible = !Visible;
			viewPort.Invalidate();

		}

		private void ChangeName(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				foreach (Shape item in dialogProcessor.Selection)
				{
					if (dialogProcessor.Selection != null)
					{
						item.ShapeName = NameTextBox.Text;
					}
				}

				statusBar.Items[0].Text = "Последно действие: Задаване име на примитивите " + NameTextBox.Text;
				NameTextBox.Visible = false;
				NameTextBox.Text = "";

			}
			viewPort.Invalidate();
		}

		private void CheckNameBtn(object sender, EventArgs e)
		{
			if (dialogProcessor.Selection.Count < 1 || dialogProcessor.Selection.Count > 1)
			{
				statusBar.Items[0].Text = "Моля, селектирайте само 1 примитив!";
			}
			else
			{
				var name = dialogProcessor.GetName(dialogProcessor.Selection[0]);
				statusBar.Items[0].Text = "Последно действие: Името на селектирания примитив е: " + name;
			}
			viewPort.Invalidate();
		}

		private void SetColorUsingNameBTN(object sender, EventArgs e)
		{
			textBox.Visible = Visible;
			textBox.Focus();
			statusBar.Items[0].Text = "Моля, въведете името на примитива!";
			viewPort.Invalidate();
			
		}
		private void GetValue(object sender, KeyEventArgs e)
		{
			string value="";
			List<Shape> shapes = new List<Shape>();
            if (e.KeyCode != Keys.Enter)
            {
				return;
            }
			if (e.KeyCode == Keys.Enter)
				{
					value = textBox.Text;
				foreach (var shape in dialogProcessor.ShapeList)
				{
					if (shape.ShapeName.Contains(value))
					{
						shapes.Add(shape);
					}
				}
				dialogProcessor.Selection.Clear();
				dialogProcessor.Selection.AddRange(shapes);
			}
			FillCollorPaletteBtn_Click(sender, e);
			statusBar.Items[0].Text = "Последно действие: промяна цвета на примитив";
			textBox.Text = "";
			textBox.Visible = false;
			viewPort.Invalidate();
		}

        private void GetAngle(object sender, KeyEventArgs e)
        {
			if (e.KeyCode == Keys.Enter)
			{
				int angle = int.Parse(angleText.Text);
				dialogProcessor.RotatePrimitive(angle);

				statusBar.Items[0].Text = "Последно действие: Задаване ъгъл на завъртане на примитивите: " + angleText.Text;
				angleText.Visible = false;
				angleText.Text = "";

			}
			viewPort.Invalidate();
		}

        private void RotateCustom(object sender, EventArgs e)
        {
			angleText.Visible = Visible;
			angleText.Focus();
			statusBar.Items[0].Text = "Моля, въведете стойността на ъгъла!";
			viewPort.Invalidate();
		}

        private void SetRGBA(object sender, KeyEventArgs e)
        {
			if (e.KeyCode == Keys.Enter)
			{
				string r = RGBA.Text;
				string[] val = r.Split(';');


				int[] res = new int[4];
				for (int i = 0; i < val.Length-1; i++)
				{
					res[i] = Int32.Parse(val[i]);
				}
			
				foreach(var shape in dialogProcessor.Selection)
                {
					shape.FillColor=(Color.FromArgb(res[3],res[0], res[1], res[2]));
                }

				statusBar.Items[0].Text = "Последно действие: Промяна цвета на примитивите, използвайки RGBA стойности. ";

				RGBA.Visible = false;
				RGBA.Text = "";

			}
			viewPort.Invalidate();
		}

        private void setPrimitiveColorUsingRGBAToolStripMenuItem_Click(object sender, EventArgs e)
        {
			//RGBA.Visible = Visible;
			//RGBA.Focus();
			RGBAForm rgbaForm = new RGBAForm();
			rgbaForm.SelectedShapes = dialogProcessor.Selection;
			rgbaForm.ShowDialog();
			statusBar.Items[0].Text = "Промяна на цвета на селектираните фигури, използвайки RGBA модела.";

			//statusBar.Items[0].Text = "Моля, въведете стойности за червения, синия, зеления цвят и за прозрачността, разделени с ; !";
			viewPort.Invalidate();
		}

        private void SetCustomSize(object sender, EventArgs e)
        {
				SizeForm form = new SizeForm();
			form.SelectedShapes = dialogProcessor.Selection;
				form.ShowDialog();
			
			statusBar.Items[0].Text = "Задаване на големина на селектираните фигури.";
			viewPort.Invalidate();
		}

        private void Other(object sender, EventArgs e)
        {
			numericUpDown1.Visible = Visible;
			numericUpDown1.Focus();
			statusBar.Items[0].Text = "Моля, въведете стойности за дебелина на контура в полето!";
			viewPort.Invalidate();
		}

        private void WidthBtn(object sender, KeyEventArgs e)
        {
			if(e.KeyCode == Keys.Enter)
			{
				dialogProcessor.SetStrokeWidth((int)numericUpDown1.Value);
				numericUpDown1.Visible = false;
				numericUpDown1.Value = 1;
				statusBar.Items[0].Text = "Последно действие: Промяна дебелината на контура на примитив";
			}
			viewPort.Invalidate();
		}

        private void UnselectBtn(object sender, EventArgs e)
        {
			dialogProcessor.Unselect();
			statusBar.Items[0].Text = "Последно действие: Деселектиране на примитиви";
			viewPort.Invalidate();
		}

        private void DeleteAllBtn(object sender, EventArgs e)
        {
			dialogProcessor.RemoveAll();
			statusBar.Items[0].Text = "Последно действие: Премахване на всичко";
			viewPort.Invalidate();
		}
    }
}
