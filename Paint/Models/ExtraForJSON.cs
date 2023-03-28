using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Paint.Models
{
    public class ExtraForJSON
    {
        ///Line 10
        public ExtraForJSON(string Type, string Name, string XPoint, string YPoint, 
            int Thickness, string LineColor, string RotateAngle, string RotateCenter, string Scale, string Skew) 
        {
            type = "Line";
            name = Name;
            startPoint = XPoint;
            endPoint = YPoint;
            lineThickness= Thickness;
            lineColor = LineColor;
            rotateAngle = RotateAngle;
            rotateCenter = RotateCenter;
            scaleValue = Scale;
            skewValue = Skew;
        }

        //Polyline 9
        public ExtraForJSON(string Type, string Name, string FigPoints,
            int Thickness, string LineColor, string RotateAngle, string RotateCenter, string Scale, string Skew)
        {
            type = "Polyline"; name = Name;
            figPoints = FigPoints;
            lineThickness= Thickness; lineColor = LineColor;
            rotateAngle = RotateAngle;
            rotateCenter = RotateCenter;
            scaleValue = Scale;
            skewValue = Skew;
        }

        //Polygon 11

        public ExtraForJSON(string Type, string Name, string FigPoints, int Thickness, 
            string LineColor, string FillColor, string RotateAngle, string RotateCenter, string Scale, 
            string Skew, string ForOverload)
        {
            type = "Polygon";
            name= Name; figPoints = FigPoints;
            lineThickness = Thickness; lineColor = LineColor;
            rotateAngle = RotateAngle;
            rotateCenter = RotateCenter;
            scaleValue = Scale;
            skewValue = Skew;
            fillColor = FillColor;
        }

        //Rectangle 12

        public ExtraForJSON(string Type, string Name, string FigPoints,
            string Height, string Width,
            int Thickness, string LineColor, string FillColor, 
            string RotateAngle, string RotateCenter, string Scale,
            string Skew)

        {
            type = "Rectangle";
            name = Name; figPoints = FigPoints;
            lineThickness = Thickness; lineColor = LineColor;
            rotateAngle = RotateAngle;
            rotateCenter = RotateCenter;
            scaleValue = Scale;
            skewValue = Skew;
            fillColor = FillColor;
            height = Height; width = Width;
        }


        //Ellipse 13
        public ExtraForJSON(string Type, string Name, string FigPoints,
    string Height, string Width,
    int Thickness, string LineColor, string FillColor,
    string RotateAngle, string RotateCenter, string Scale,
    string Skew, string ForOverload)

        {
            type = "Ellipse";
            name = Name; figPoints = FigPoints;
            lineThickness = Thickness; lineColor = LineColor;
            rotateAngle = RotateAngle;
            rotateCenter = RotateCenter;
            scaleValue = Scale;
            skewValue = Skew;
            fillColor = FillColor;
            height = Height; width = Width;
        }

        public ExtraForJSON() { }

        //Path 14
        public ExtraForJSON(string Type, string Name, string Commands,
            int Thickness, string LineColor, string FillColor, string RotateAngle,
            string RotateCenter, string Scale, string Skew,
            string ForOverload, string ForOverload2, string ForOverload3, 
            string ForOverload4)
        {
            type = "Path";
            name = Name; pathCommands = Commands;
            lineThickness = Thickness; lineColor = LineColor;
            rotateAngle = RotateAngle;
            rotateCenter = RotateCenter;
            scaleValue = Scale;
            skewValue = Skew;
            fillColor = FillColor;
        }

        private string _name;

        public string name
        {
            get => _name; set { _name = value; }
        }


        private string _type;

        public string type
        {
            get => _type; set { _type = value; }
        }

        private string _lineColor;

        public string lineColor
        {
            get => _lineColor;
            set
            {
                _lineColor = value;
            }
        }


        private int _lineThickness;

        public int lineThickness
        {
            get => _lineThickness;
            set
            {
                _lineThickness = value;
            }
        }

        private string _rotateAngle;

        public string rotateAngle
        {
            get => _rotateAngle; set
            {
                _rotateAngle = value;
            }
        }

        private string _rotateCenter;

        public string rotateCenter
        {
            get => _rotateCenter; set
            {
                _rotateCenter = value;
            }
        }

        private string _scaleValue;

        public string scaleValue
        {
            get => _scaleValue;
            set
            {
                _scaleValue = value;
            }
        }
        private string _skewValue;

        public string skewValue
        {
            get => _skewValue;
            set
            {
                _skewValue = value;
            }
        }

        private string _startPoint;

        public string startPoint
        {
            get => _startPoint; set
            {
                _startPoint = value;
            }
        }

        private string _endPoint;

        public string endPoint
        {
            get => _endPoint;
            set
            {
                _endPoint = value;
            }
        }

        private string _figPoints;

        public string figPoints
        {
            get => _figPoints;
            set
            {
                _figPoints = value;
            }
        }

        private string _fillColor;

        public string fillColor

        {
            get => _fillColor;
            set
            {
                _fillColor = value;
            }
        }

        private string _height;

        public string height
        {
            get => _height; set
            {
                _height = value;
            }
        }

        private string _width;

        public string width
        {
            get => _width; set
            {
                _width = value;
            }
        }

        private string _pathCommands;

        public string pathCommands
        {
            get => _pathCommands; set
            {
                _pathCommands = value;
            }
        }
    }
}
