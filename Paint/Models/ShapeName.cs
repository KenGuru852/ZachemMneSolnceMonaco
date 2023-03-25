using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Models
{
    public class ShapeName
    {
        private string _Name;
        public string Name { get => _Name; set { _Name = value; } }

        private string _Type;

        public string Type { get => _Type; set { _Type = value; } }

        private string _pathCommands;

        public string pathCommands { get => _pathCommands; set { _pathCommands = value; } }


        private RotateTransform _rotateTransform;

        public RotateTransform rotateTransform
        {
            get => _rotateTransform;
            set
            {
                _rotateTransform = value;
            }
        }

        private ScaleTransform _scaleTransform;

        public ScaleTransform scaleTransform
        {
            get => _scaleTransform;
            set
            {
                _scaleTransform = value;
            }
        }

        private SkewTransform _skewTransform;

        public SkewTransform skewTransform
        {
            get => _skewTransform;
            set
            {
                _skewTransform = value;
            }
        }

        public ShapeName(string Names, string Types)
        {
            _Name = Names;
            _Type = Types;
        }

        public ShapeName(string Names, string Types, string RotAngle, string RotPoints, string ScaTrans, string SkeTrans)
        {
            _scaleTransform = new ScaleTransform();
            _skewTransform = new SkewTransform();
            _rotateTransform = new RotateTransform();
            _Name = Names;
            _Type = Types;
            string XPoint = "";
            string YPoint = "";
            _rotateTransform.Angle = double.Parse(RotAngle);
            if (RotPoints != "0 0")
            {
                string[] split = RotPoints.Split(' ');

                XPoint = split[0];
                YPoint = split[1];
                _rotateTransform.CenterX = double.Parse(XPoint);
                Debug.WriteLine(_rotateTransform.CenterX.ToString());
                _rotateTransform.CenterY = double.Parse(YPoint);
                Debug.WriteLine(_rotateTransform.CenterY.ToString());
            }
            if (ScaTrans != "0 0")
            {
                string[] split = ScaTrans.Split(' ');

                XPoint = split[0];
                YPoint = split[1];
                _scaleTransform.ScaleX = double.Parse(XPoint);
                _scaleTransform.ScaleY = double.Parse(YPoint);
            }
            if (SkeTrans != "0 0")
            {
                string[] split = SkeTrans.Split(' ');

                XPoint = split[0];
                YPoint = split[1];
                _skewTransform.AngleX = double.Parse(XPoint);
                _skewTransform.AngleY = double.Parse(YPoint);
            }
        }

        public ShapeName(string Names, string Types, string Commands, string RotAngle, string RotPoints, string ScaTrans, string SkeTrans)
        {
            _Name = Names;
            _Type = Types;
            _pathCommands = Commands;
            _scaleTransform = new ScaleTransform();
            _skewTransform = new SkewTransform();
            _rotateTransform = new RotateTransform();
            _Name = Names;
            _Type = Types;
            string XPoint = "";
            string YPoint = "";
            _rotateTransform.Angle = double.Parse(RotAngle);
            if (RotPoints != "0 0")
            {
                string[] split = RotPoints.Split(',');

                foreach (var Item in split)
                {
                    string[] NewSplit = Item.Split(" ");
                    XPoint = NewSplit[0];
                    YPoint = NewSplit[1];
                }
                _rotateTransform.CenterX = double.Parse(XPoint);
                _rotateTransform.CenterY = double.Parse(YPoint);
            }
            if (ScaTrans != "0 0")
            {
                string[] split = ScaTrans.Split(',');

                foreach (var Item in split)
                {
                    string[] NewSplit = Item.Split(" ");
                    XPoint = NewSplit[0];
                    YPoint = NewSplit[1];
                }
                _scaleTransform.ScaleX = double.Parse(XPoint);
                _scaleTransform.ScaleY = double.Parse(YPoint);
            }
            if (SkeTrans != "0 0")
            {
                string[] split = SkeTrans.Split(',');

                foreach (var Item in split)
                {
                    string[] NewSplit = Item.Split(" ");
                    XPoint = NewSplit[0];
                    YPoint = NewSplit[1];
                }
                _skewTransform.AngleX = double.Parse(XPoint);
                _skewTransform.AngleY = double.Parse(YPoint);
            }
        }
    }
}
