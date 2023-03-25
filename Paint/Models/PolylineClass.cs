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
    public class PolylineClass : Polyline
    {
        public PolylineClass() { }
        private Polyline _shapePolyline { get; set; }

        public Polyline shapePolyline
        {
            get => _shapePolyline;
            set
            {
                _shapePolyline = value;
            }
        }

        private Points GetPoints { get; set; }

        public Polyline PolyLineFunc(string _Name, string _Points, string _LineColor, int _LineThickness)
        {
            GetPoints = new Points();
            shapePolyline = new Polyline();
            shapePolyline.Name = _Name;
            string[] split = _Points.Split(',');
            foreach (var Item in split)
            {
                string[] NewSplit = Item.Split(" ");
                string NewPoint = NewSplit[0] + ',' + NewSplit[1];
                GetPoints.Add(Point.Parse(NewPoint));
            }
            shapePolyline.Points = GetPoints;
            shapePolyline.Stroke = Brush.Parse(_LineColor);
            shapePolyline.StrokeThickness = _LineThickness;
            return shapePolyline;
        }
    }
}
