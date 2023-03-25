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
    public class MultipleCornersClass : Polygon
    {
        public MultipleCornersClass() { }
        private Polygon _shapePolygon { get; set; }

        public Polygon shapePolygon
        {
            get => _shapePolygon;
            set
            {
                _shapePolygon = value;
            }
        }

        private Points GetPoints { get; set; }

        public Polygon PolygonFunc( string _Name,string _Points, string _LineColor, int _LineThickness, string _FillColor)
        {
            GetPoints = new Points();
            shapePolygon = new Polygon();
            shapePolygon.Name = _Name;
            string[] split = _Points.Split(',');
            foreach (var Item in split)
            {
                string[] NewSplit = Item.Split(" ");
                string NewPoint = NewSplit[0] + ',' + NewSplit[1];
                GetPoints.Add(Point.Parse(NewPoint));
            }
            shapePolygon.Points = GetPoints;
            shapePolygon.Stroke = Brush.Parse(_LineColor);
            shapePolygon.StrokeThickness = _LineThickness;
            shapePolygon.Fill = Brush.Parse(_FillColor);
            return shapePolygon;
        }
    }
}
