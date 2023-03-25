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
    public class MixLineClass : Path
    {
        public MixLineClass() { }
        private Path _shapePath { get; set; }

        public Path shapePath
        {
            get => _shapePath;
            set
            {
                _shapePath = value;
            }
        }

        private Points GetPoints { get; set; }

        public Path PathFunc(string _Name, string _Commands, string _LineColor, int _LineThickness, string _FillColor)
        {
            shapePath = new Path();
            shapePath.Name = _Name;
            shapePath.Data = Geometry.Parse(_Commands);
            shapePath.Stroke = Brush.Parse(_LineColor);
            shapePath.StrokeThickness = _LineThickness;
            shapePath.Fill = Brush.Parse(_FillColor);
            return shapePath;
        }
    }
}
