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
    public partial class RGBAForm : Form
    {
        public RGBAForm()
        {
            InitializeComponent();
        }

        public List<Shape> SelectedShapes { get; set; }

        private void SetRGBAColor(object sender, EventArgs e)
        {
                int red = int.Parse(rText.Text);
                int green = int.Parse(gText.Text);
                int blue = int.Parse(bText.Text);
                int alpha = int.Parse(aText.Text);
                if (SelectedShapes.Count() > 0)
                {
                    foreach (Shape shape in SelectedShapes)
                    {
                    shape.FillColor = Color.FromArgb(alpha, red, green, blue);
                        // shape.ShapeName = "it works";
                    }
                    this.Close();
                }
                else
                {
                    label1.Text = "Няма селектирани примитиви!";
                }
            
        }
    }
}
