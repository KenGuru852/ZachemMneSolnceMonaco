using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Models
{
    public class RectangleClass : Rectangle
    {
        public RectangleClass() { }
        private Rectangle _shapeRectangle { get; set; }

        public Rectangle shapeRectangle
        {
            get => _shapeRectangle;
            set
            {
                _shapeRectangle = value;
            }
        }

        private Points GetPoints { get; set; }

        public Rectangle RectangleFunc(string _Name, string _Points, string _Width, string _Height, string _LineColor, int _LineThickness, string _FillColor)
        {
            GetPoints = new Points();
            shapeRectangle = new Rectangle();
            shapeRectangle.Name = _Name;
            string[] split = _Points.Split(',');
            string FirstPoint = "";
            string SecondPoint = "";
            foreach (var Item in split)
            {
                string[] NewSplit = Item.Split(" ");
                FirstPoint = NewSplit[0];
                SecondPoint = NewSplit[1];
            }
            shapeRectangle.Margin = new(double.Parse(FirstPoint), double.Parse(SecondPoint));
            shapeRectangle.Stroke = Brush.Parse(_LineColor);
            shapeRectangle.StrokeThickness = _LineThickness;
            shapeRectangle.Width = int.Parse(_Width);
            shapeRectangle.Height = int.Parse(_Height);
            shapeRectangle.Fill = Brush.Parse(_FillColor);
            return shapeRectangle;
        }
    }
}
