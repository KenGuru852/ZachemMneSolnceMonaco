using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Paint.Models
{
    public class XMLSavers : IFigureSaver
    {
        string[] Colors = { "Red", "Yellow", "Blue", "Green", "Black" };
        private int FindColor(string ColorName)
        {
            for (int i = 0; i < Colors.Length; i++)
            {
                if (ColorName == Colors[i])
                {
                    return i;
                }
            }
            return 0;
        }
        public void Save(ObservableCollection<Shape> shapes, ObservableCollection<ShapeName> shapeNames, string path, Canvas canv)
        {
           
            XDocument xDocument = new XDocument();
            XElement xElementFigures = new XElement("AllShapes");
            for (int i = 0; i < shapes.Count(); i++)
            {
                if (shapeNames[i].Type == "Line")
                {
                    Line EditLine = (Line)shapes[i];
                    //textBoxStart = EditLine.StartPoint.ToString();
                    //textBoxEnd = EditLine.EndPoint.ToString();
                    //textBoxName = allName[index].Name;
                    //comboBoxColor = FindColor(allShapes[index].Stroke.ToString());
                    //numericUpDownStroke = int.Parse(allShapes[index].StrokeThickness.ToString());
                    XElement xElementFigure = new XElement("figure");
                    XAttribute xAttributeFigureType = new XAttribute("Type", shapeNames[i].Type);
                    XElement xElementFigureName = new XElement("Name", shapeNames[i].Type);
                    XElement xElementFigureXPoint = new XElement("XPoint", EditLine.StartPoint.ToString());
                    XElement xElementFigureYPoint = new XElement("YPoint", EditLine.EndPoint.ToString());
                    XElement xElementFigureTickness = new XElement("Thickness", shapes[i].StrokeThickness.ToString());
                    XElement xElementFigureLineColor = new XElement("LineColor", shapes[i].Stroke.ToString());

                    xElementFigure.Add(xAttributeFigureType);
                    xElementFigure.Add(xElementFigureName);
                    xElementFigure.Add(xElementFigureXPoint);
                    xElementFigure.Add(xElementFigureYPoint);
                    xElementFigure.Add(xElementFigureTickness);
                    xElementFigure.Add(xElementFigureLineColor);

                    xElementFigures.Add(xElementFigure);
                }

                if (shapeNames[i].Type == "Polyline")
                {
                    Polyline EditPolyline = (Polyline)shapes[i];
                    string temp = "";
                    for (int j = 0; j < EditPolyline.Points.Count; j++)
                    {
                        string newTemp = EditPolyline.Points[j].ToString();
                        newTemp = newTemp.Replace(",", "");
                        temp += newTemp;
                        if (j != EditPolyline.Points.Count - 1)
                        {
                            temp += ",";
                        }
                    }
                    XElement xElementFigure = new XElement("figure");
                    XAttribute xAttributeFigureType = new XAttribute("Type", shapeNames[i].Type);
                    XElement xElementFigureName = new XElement("Name", shapeNames[i].Name);
                    XElement xElementFigurePoints = new XElement("FigPoints", temp);
                    XElement xElementFigureTickness = new XElement("Thickness", shapes[i].StrokeThickness.ToString());
                    XElement xElementFigureLineColor = new XElement("LineColor", shapes[i].Stroke.ToString());


                    xElementFigure.Add(xAttributeFigureType);
                    xElementFigure.Add(xElementFigureName);
                    xElementFigure.Add(xElementFigurePoints);
                    xElementFigure.Add(xElementFigureTickness);
                    xElementFigure.Add(xElementFigureLineColor);

                    xElementFigures.Add(xElementFigure);
                }
                if (shapeNames[i].Type == "Polygon")
                {
                    Polygon EditPolygon = (Polygon)shapes[i];
                    string temp = "";
                    for (int j = 0; j < EditPolygon.Points.Count; j++)
                    {
                        string newTemp = EditPolygon.Points[j].ToString();
                        newTemp = newTemp.Replace(",", "");
                        temp += newTemp;
                        if (j != EditPolygon.Points.Count - 1)
                        {
                            temp += ",";
                        }
                    }
                    XElement xElementFigure = new XElement("figure");
                    XAttribute xAttributeFigureType = new XAttribute("Type", shapeNames[i].Type);
                    XElement xElementFigureName = new XElement("Name", shapeNames[i].Name);
                    XElement xElementFigurePoints = new XElement("FigPoints", temp);
                    XElement xElementFigureTickness = new XElement("Thickness", shapes[i].StrokeThickness.ToString());
                    XElement xElementFigureLineColor = new XElement("LineColor", shapes[i].Stroke.ToString());
                    XElement xElementFigureFillColor = new XElement("FillColor", shapes[i].Fill.ToString());


                    xElementFigure.Add(xAttributeFigureType);
                    xElementFigure.Add(xElementFigureName);
                    xElementFigure.Add(xElementFigurePoints);
                    xElementFigure.Add(xElementFigureTickness);
                    xElementFigure.Add(xElementFigureLineColor);
                    xElementFigure.Add(xElementFigureFillColor);

                    xElementFigures.Add(xElementFigure);
                }
                if (shapeNames[i].Type == "Rectangle" || shapeNames[i].Type == "Ellipse")
                {
                    string temp = shapes[i].Margin.ToString();
                    string[] tempe = temp.Split(",");
                    temp = tempe[0] + " " + tempe[1];
                    XElement xElementFigure = new XElement("figure");
                    XAttribute xAttributeFigureType = new XAttribute("Type", shapeNames[i].Type);
                    XElement xElementFigureName = new XElement("Name", shapeNames[i].Name);
                    XElement xElementFigurePoints = new XElement("FigPoints", temp);
                    XElement xElementFigureHeight = new XElement("Height", shapes[i].Height.ToString());
                    XElement xElementFigureWidth = new XElement("Width", shapes[i].Width.ToString());
                    XElement xElementFigureTickness = new XElement("Thickness", shapes[i].StrokeThickness.ToString());
                    XElement xElementFigureLineColor = new XElement("LineColor", shapes[i].Stroke.ToString());
                    XElement xElementFigureFillColor = new XElement("FillColor", shapes[i].Fill.ToString());


                    xElementFigure.Add(xAttributeFigureType);
                    xElementFigure.Add(xElementFigureName);
                    xElementFigure.Add(xElementFigurePoints);
                    xElementFigure.Add(xElementFigureHeight);
                    xElementFigure.Add(xElementFigureWidth);
                    xElementFigure.Add(xElementFigureTickness);
                    xElementFigure.Add(xElementFigureLineColor);
                    xElementFigure.Add(xElementFigureFillColor);

                    xElementFigures.Add(xElementFigure);
                }

                if (shapeNames[i].Type == "Path")
                {
                    Path EditPath = (Path)shapes[i];
                    //textBoxCommandPath = EditPath.Data.ToString();
                    XElement xElementFigure = new XElement("figure");
                    XAttribute xAttributeFigureType = new XAttribute("Type", shapeNames[i].Type);
                    XElement xElementFigureName = new XElement("Name", shapeNames[i].Name);
                    XElement xElementFigureCommands = new XElement("Commands", EditPath.Data.ToString());
                    XElement xElementFigureTickness = new XElement("Thickness", shapes[i].StrokeThickness.ToString());
                    XElement xElementFigureLineColor = new XElement("LineColor", shapes[i].Stroke.ToString());
                    XElement xElementFigureFillColor = new XElement("FillColor", shapes[i].Fill.ToString());

                    xElementFigure.Add(xAttributeFigureType);
                    xElementFigure.Add(xElementFigureName);
                    xElementFigure.Add(xElementFigureCommands);
                    xElementFigure.Add(xElementFigureTickness);
                    xElementFigure.Add(xElementFigureLineColor);
                    xElementFigure.Add(xElementFigureFillColor);

                    xElementFigures.Add(xElementFigure);
                }
            }
            xDocument.Add(xElementFigures);
            xDocument.Save(path);
        }
    }
}
