using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Draw.src.GUI
{
    public partial class SizeForm : Form
    {
        public SizeForm()
        {
            InitializeComponent();
        }

       public List<Shape> SelectedShapes { get; set; }
       
        private void Btn_Click(object sender, EventArgs e)
        {
            if (WidthTextBox.Text != null && HeightTextBox != null)
            {
                float width = float.Parse(WidthTextBox.Text);
                float height = float.Parse(HeightTextBox.Text);
                if (SelectedShapes.Count() > 0)
                {
                    foreach (Shape shape in SelectedShapes)
                    {
                        shape.Width = width;
                        shape.Height = height;
                       // shape.ShapeName = "it works";

                    }
                    this.Close();
                }
                else
                {
                    label1.Text = "Няма селектирани примитиви";
                }
            }
            else
            {
                label1.Text = "Моля, задайте коректни стойности и за двете полета";
            }

            
        }
    }
}
