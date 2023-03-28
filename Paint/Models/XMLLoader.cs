using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Paint.Models
{
    public class XMLLoader
    {
        public Tuple<ObservableCollection<Shape>, ObservableCollection<ShapeName>, ObservableCollection<ExtraForJSON>> Load(string path)
        {
            string[] Colors = { "Red", "Yellow", "Blue", "Green", "Black" };

            var ParseAllShapes = new ObservableCollection<Shape>();

            var ParseAllNames = new ObservableCollection<ShapeName>();

            var ParseAllExtra = new ObservableCollection<ExtraForJSON>();

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(path);

            var ShapesList = xmlDoc.SelectNodes("//figure");

            foreach(XmlNode ShapeSelect in ShapesList)
            {
                string ShapeThickness = ShapeSelect.SelectSingleNode("Thickness")?.InnerText;

                string ShapeStroke = ShapeSelect.SelectSingleNode("LineColor")?.InnerText;

                string ShapeName = ShapeSelect.SelectSingleNode("Name")?.InnerText;

                string ShapeType = ShapeSelect.SelectSingleNode("Type")?.InnerText;

                string ShapePoints = "";

                string ShapeFill = "";

                string ShapeHeight = "";

                string ShapeWidth = "";

                if (ShapeType == "Ellipse" || ShapeType == "Rectangle" || ShapeType == "Polygon" || ShapeType == "Polyline")
                {
                    ShapePoints = ShapeSelect.SelectSingleNode("FigPoints")?.InnerText;
                }

                if (ShapeType == "Ellipse" || ShapeType == "Rectangle" || ShapeType == "Polygon" || ShapeType == "Path")
                {
                    ShapeFill = ShapeSelect.SelectSingleNode("FillColor")?.InnerText;
                }

                if (ShapeType == "Ellipse" || ShapeType == "Rectangle")
                {
                    ShapeHeight = ShapeSelect.SelectSingleNode("Height")?.InnerText;
                    ShapeWidth = ShapeSelect.SelectSingleNode("Width")?.InnerText;
                }

                /////////////////////////////// RENDER TRANSFORM AND PATH //////////////////////////////////////

                string ShapeSkew = ShapeSelect.SelectSingleNode("Skew")?.InnerText;

                string ShapeScale = ShapeSelect.SelectSingleNode("Scale")?.InnerText;

                string ShapeRotateCenter = ShapeSelect.SelectSingleNode("RotateCenter")?.InnerText;

                string ShapeRotateAngle = ShapeSelect.SelectSingleNode("RotateAngel")?.InnerText;

                if (ShapeType == "Path" )

                {
                    string PathCommands = ShapeSelect.SelectSingleNode("Commands")?.InnerText;

                    MixLineClass newMixLine = new MixLineClass();

                    ParseAllShapes.Add(newMixLine.PathFunc(ShapeName, PathCommands, ShapeStroke, int.Parse(ShapeThickness), ShapeFill));
                   
                    ParseAllNames.Add(new ShapeName(ShapeName, ShapeType, PathCommands, ShapeRotateAngle, ShapeRotateCenter, ShapeScale, ShapeSkew));

                    ParseAllExtra.Add(new ExtraForJSON(ShapeType, ShapeName, PathCommands,
                        int.Parse(ShapeThickness), ShapeStroke, ShapeFill, ShapeRotateAngle,
                        ShapeRotateCenter, ShapeScale, ShapeSkew,
                        "1", "2", "3", "4"));
                }
                else
                {
                    ParseAllNames.Add(new ShapeName(ShapeName, ShapeType, ShapeRotateAngle, ShapeRotateCenter, ShapeScale, ShapeSkew));
                }
                
                /////////////////////////////////////////////// PARSE LINE //////////////////////////////////////////////

                if (ShapeType == "Line")
                {
                    string LineXPoint = ShapeSelect.SelectSingleNode("XPoint")?.InnerText;
                    string LineYPoint = ShapeSelect.SelectSingleNode("YPoint")?.InnerText;
                    LineClass newLine = new LineClass();
                    ParseAllShapes.Add(newLine.LineFunc(ShapeName, LineXPoint, LineYPoint, ShapeStroke, int.Parse(ShapeThickness)));

                    ParseAllExtra.Add(new ExtraForJSON(
                        ShapeType, ShapeName, LineXPoint, LineYPoint,
                        int.Parse(ShapeThickness), ShapeStroke, ShapeRotateAngle,
                        ShapeRotateCenter, ShapeScale, ShapeSkew));
                }

                /////////////////////////////////////////////// PARSE POLYLINE //////////////////////////////////////////////

                if (ShapeType == "Polyline")
                {
                    PolylineClass newPoly = new PolylineClass();
                    ParseAllShapes.Add(newPoly.PolyLineFunc(ShapeName, ShapePoints, ShapeStroke, int.Parse(ShapeThickness)));
                
                    ParseAllExtra.Add(new ExtraForJSON(
                        ShapeType, ShapeName, ShapePoints,
                        int.Parse(ShapeThickness), ShapeStroke,
                        ShapeRotateAngle, ShapeRotateCenter, ShapeScale,
                        ShapeSkew));
                }

                /////////////////////////////////////////////// PARSE POLYGON //////////////////////////////////////////////

                if (ShapeType == "Polygon")
                {
                    MultipleCornersClass newPolygon = new MultipleCornersClass();
                    ParseAllShapes.Add(newPolygon.PolygonFunc(ShapeName, ShapePoints, ShapeStroke, int.Parse(ShapeThickness), ShapeFill));

                    ParseAllExtra.Add(new ExtraForJSON(
                        ShapeType, ShapeName, ShapePoints,
                        int.Parse(ShapeThickness), ShapeStroke,
                        ShapeFill, ShapeRotateAngle, ShapeRotateCenter,
                        ShapeScale, ShapeSkew, "1"));
                }

                /////////////////////////////////////////////// PARSE ELLIPSE //////////////////////////////////////////////

                if (ShapeType == "Ellipse")
                {
                    EllipseClass newEllipse = new EllipseClass();
                    ParseAllShapes.Add(newEllipse.EllipseFunc(ShapeName, ShapePoints, ShapeWidth, ShapeHeight, ShapeStroke, int.Parse(ShapeThickness), ShapeFill));

                    ParseAllExtra.Add(new ExtraForJSON(
                        ShapeType, ShapeName, ShapePoints,
                        ShapeHeight, ShapeWidth, int.Parse(ShapeThickness),
                        ShapeStroke, ShapeFill, ShapeRotateAngle, ShapeRotateCenter,
                        ShapeScale, ShapeSkew, "1"));
                }

                /////////////////////////////////////////////// PARSE Rectangle //////////////////////////////////////////////

                if (ShapeType == "Rectangle")
                {
                    RectangleClass newRectangle = new RectangleClass();
                    ParseAllShapes.Add(newRectangle.RectangleFunc(ShapeName, ShapePoints, ShapeWidth, ShapeHeight, ShapeStroke, int.Parse(ShapeThickness), ShapeFill));

                    ParseAllExtra.Add(new ExtraForJSON(
                        ShapeType, ShapeName, ShapePoints,
                        ShapeHeight, ShapeWidth, int.Parse(ShapeThickness),
                        ShapeStroke, ShapeFill, ShapeRotateAngle, ShapeRotateCenter,
                        ShapeScale, ShapeSkew));
                }

            }



            return Tuple.Create(ParseAllShapes, ParseAllNames, ParseAllExtra);
        }
    }
}
