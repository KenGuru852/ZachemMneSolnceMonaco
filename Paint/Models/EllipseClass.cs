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
    public class EllipseClass : Ellipse
    { 
        public EllipseClass() { }
        private Ellipse _shapeEllipse { get; set; }

        public Ellipse shapeEllipse
        {
            get => _shapeEllipse;
            set
            {
                _shapeEllipse = value;
            }
        }

        private Points GetPoints { get; set; }

        public Ellipse EllipseFunc(string _Name ,string _Points, string _Width, string _Height, string _LineColor, int _LineThickness, string _FillColor)
        {
            GetPoints = new Points();
            shapeEllipse = new Ellipse();
            shapeEllipse.Name = _Name;
            string[] split = _Points.Split(',');
            string FirstPoint = "";
            string SecondPoint = "";
            foreach (var Item in split)
            {
                string[] NewSplit = Item.Split(" ");
                FirstPoint = NewSplit[0];
                SecondPoint = NewSplit[1];
            }
            shapeEllipse.Margin = new(double.Parse(FirstPoint), double.Parse(SecondPoint), double.Parse("0"), double.Parse("0"));
            shapeEllipse.Stroke = Brush.Parse(_LineColor);
            shapeEllipse.StrokeThickness = _LineThickness;
            shapeEllipse.Width = int.Parse(_Width);
            shapeEllipse.Height = int.Parse(_Height);
            shapeEllipse.Fill = Brush.Parse(_FillColor);
            return shapeEllipse;
        }
    }
}

