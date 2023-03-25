using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Models
{
    public class LineClass : Line
    {
        public LineClass() { }
        private Line _shapeLine { get; set; }

        public Line shapeLine
        {
            get => _shapeLine;
            set
            {
                _shapeLine = value;
            }
        }
        
        public Line LineFunc(string _Name ,string _FirstPoint, string _SecondPoint, string _LineColor, int _LineThickness)
        {
            shapeLine = new Line();
            shapeLine.Name = _Name;
            shapeLine.StartPoint = Point.Parse(_FirstPoint);
            shapeLine.EndPoint = Point.Parse(_SecondPoint);
            shapeLine.Stroke = Brush.Parse(_LineColor);
            shapeLine.StrokeThickness = _LineThickness;
            return shapeLine;
        }



    }
}
