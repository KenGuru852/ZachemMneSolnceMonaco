using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Paint.Models
{
    public class XMLLoader
    {
        public Tuple<ObservableCollection<Shape>, ObservableCollection<ShapeName>> Load(string path)
        {
            string[] Colors = { "Red", "Yellow", "Blue", "Green", "Black" };
            XDocument xDocument = XDocument.Load(path);
            LineClass newLine = new LineClass();
            IEnumerable<Shape>? allLines = xDocument.Element("AllShapes")?
                .Elements("figure")
                .Select(
                    figure =>
                    {
                        var figureType = figure.Attribute("Type");
                        if (figureType.Value == "Line")
                        {
                            var figureName = figure.Element("Name");
                            var figureXPoint = figure.Element("XPoint");
                            var figureYPoint = figure.Element("YPoint");
                            var figureThickness = figure.Element("Thickness");
                            var figureLineColor = figure.Element("LineColor");
                            return (newLine.LineFunc(figureName.Value, figureXPoint.Value, figureYPoint.Value, figureLineColor.Value,
                                int.Parse(figureThickness.Value)));
                        };
                     return (newLine.LineFunc("UNDEFINED", "0 0", "0 0", "Red", 0));
                    }
                );
            IEnumerable<ShapeName>? allLineName = xDocument.Element("AllShapes")?
                .Elements("figure")
                .Select(figure =>
                {
                    var figureType = figure.Attribute("Type");
                    if (figureType.Value == "Line")
                    {
                        var figureName = figure.Element("Name");
                        return new ShapeName(figureName.Value, figureType.Value);
                    }
                    return new ShapeName("UNDEFINED", "UNDEFINED");
                });
            ObservableCollection<Shape> allShapes = new ObservableCollection<Shape>();
            ObservableCollection<ShapeName> allNames = new ObservableCollection<ShapeName>();
            foreach (var item in allLines)
            {
                allShapes.Add(item);
            }
            foreach(var item in allLineName)
            {
                allNames.Add(item);
            }



            
            return Tuple.Create(allShapes, allNames);
        }
    }
}
