using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PathShape = Avalonia.Controls.Shapes.Path;

namespace Paint.Models
{
    public class JSONLoader
    {
        public Tuple<ObservableCollection<Shape>, ObservableCollection<ShapeName>> Load(string path)
        {

            ObservableCollection<Shape> allShapes = new ObservableCollection<Shape>();
            ObservableCollection<ShapeName> allShapesName = new ObservableCollection<ShapeName>();

            Line newLines = new Line();
            Polygon newPolygon = new Polygon();
            Polyline newPolyline = new Polyline();
            Rectangle newRect = new Rectangle();
            Ellipse newEllipse = new Ellipse();
            PathShape newPath = new PathShape();
            ShapeName newShapeName = new ShapeName();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                newLines = JsonSerializer.Deserialize<Line>(fs);
                if (newLines == null)
                {
                    newLines = new Line();
                }
                newPolygon = JsonSerializer.Deserialize<Polygon>(fs);
                if (newPolygon == null)
                {
                    newPolygon = new Polygon();
                }
                newPolyline = JsonSerializer.Deserialize<Polyline>(fs);
                if (newPolyline == null)
                {
                    newPolyline = new Polyline();
                }
                newRect = JsonSerializer.Deserialize<Rectangle>(fs);
                if (newRect == null)
                {
                    newRect = new Rectangle();
                }
                newEllipse = JsonSerializer.Deserialize<Ellipse>(fs);
                if (newEllipse == null)
                {
                    newEllipse = new Ellipse();
                }
                newPath = JsonSerializer.Deserialize<PathShape>(fs);
                if (newPath == null)
                {
                    newPath = new PathShape();
                }
                newShapeName = JsonSerializer.Deserialize<ShapeName>(fs);
                if (newShapeName == null)
                {
                    newShapeName = new ShapeName();
                }
            }

            allShapes.Add(newPath);
            allShapes.Add(newRect);
            allShapes.Add(newEllipse);
            allShapes.Add(newLines); 
            allShapes.Add(newPolygon);
            allShapes.Add(newPolyline);
            allShapesName.Add(newShapeName);

            Tuple<ObservableCollection<Shape>, ObservableCollection<ShapeName>> tulup = Tuple.Create(allShapes, allShapesName);

            return tulup;
        }
    }
}
