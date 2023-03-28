using System.Collections.ObjectModel;
using ReactiveUI;
using Avalonia;
using System.Xml.Linq;
using DynamicData;
using Avalonia.Controls.Shapes;
using Paint.ViewModels;
using System.Drawing.Text;
using Avalonia.Controls;
using Paint.Views;
using System.Diagnostics;
using System.Reactive;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Avalonia.Input.Raw;
using Avalonia.Input;
using JetBrains.Annotations;
using Avalonia.Media;
using DynamicData.Binding;
using System.Collections.Specialized;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using Paint.Models;
using Path = Avalonia.Controls.Shapes.Path;
using PathFile = System.IO.Path;
using System;
using Avalonia.Skia;

namespace Paint.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        LineClass newLine = new LineClass();
        PolylineClass newPolyLine = new PolylineClass();
        MultipleCornersClass newPolygon = new MultipleCornersClass();
        RectangleClass newRectangle = new RectangleClass();
        EllipseClass newEllipse = new EllipseClass();
        MixLineClass newMixline = new MixLineClass();

        private ObservableCollection<ExtraForJSON> _shapesForParse;

        public ObservableCollection<ExtraForJSON> shapesForParse
        {
            get { return _shapesForParse; }
            set { this.RaiseAndSetIfChanged(ref _shapesForParse, value); }
        }

        private ObservableCollection<Shape> _allShapes;

        public ObservableCollection<Shape> allShapes
        {
            get { return _allShapes; }
            set { this.RaiseAndSetIfChanged(ref _allShapes, value); }
        }

        private ObservableCollection<ShapeName> _allName;

        public ObservableCollection<ShapeName> allName
        {
            get { return _allName; }
            set { this.RaiseAndSetIfChanged(ref _allName, value); }
        }


        private Canvas _newCanvas;

        public Canvas newCanvas
        {
            get { return _newCanvas; }
            set { this.RaiseAndSetIfChanged(ref _newCanvas, value); }
        }

        string[] Colors = { "Red", "Yellow", "Blue", "Green", "Black" };


        public void buttonClear()
        {
            listBoxIndex = -1;
            textBoxName = "";
            textBoxStart = "";
            textBoxEnd = "";
            comboBoxColor = 0;
            numericUpDownStroke = 0;
            textBoxPoints = "";
            comboBoxFillColor = 0;
            textBoxWidth = "";
            textBoxHeight = "";
            textBoxCommandPath = "";
        }

        public void buttonDeleteShape()
        {
            if (listBoxIndex != -1)
            {
                int temp = listBoxIndex;
                string ChooseLB = allName[listBoxIndex].Name;
                allShapes.RemoveAt(temp);
                allName.RemoveAt(temp);
                shapesForParse.RemoveAt(temp);
                newCanvas.Children.RemoveAt(temp);
                listBoxIndex = -1;
            }
        }
        public void buttonAdd()
        {
            if (listBoxIndex == -1)
            {
                bool buttonactive = true;
                for (int i = 0; i < allName.Count; i++)
                {
                    if (allName[i].Name == textBoxName)
                    buttonactive = false;
                }
                if (buttonactive)
                {
                    if (Choosing == 0)
                    {
                        allLines.Add(newLine.LineFunc(_textBoxName, _textBoxStart, _textBoxEnd, Colors[_comboBoxColor], _numericUpDownStroke));
                        allShapes.Add(allLines[allLines.Count - 1]);
                        allName.Add(new ShapeName(_textBoxName, "Line", textBoxRenderTransformAngle, textBoxRotateCenter,
                        textBoxScaleTransform, textBoxSkewTransform));

                        shapesForParse.Add(new ExtraForJSON("0", textBoxName, textBoxStart,
                            textBoxEnd, numericUpDownStroke,
                            Colors[comboBoxColor], textBoxRenderTransformAngle,
                            textBoxRotateCenter, textBoxScaleTransform,
                            textBoxSkewTransform));

                        var TransGroup = new TransformGroup();

                        TransGroup.Children.Add(allName[allName.Count - 1].rotateTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].skewTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].scaleTransform);

                        allLines[allLines.Count - 1].RenderTransform = TransGroup;

                        newCanvas.Children.Add(allShapes[allShapes.Count - 1]);                             
                        allLines.RemoveAt(allLines.Count - 1);
                    }
                    if (Choosing == 1)
                    {
                        allPolyLine.Add(newPolyLine.PolyLineFunc(_textBoxName, _textBoxPoints, Colors[_comboBoxColor], _numericUpDownStroke));
                        allShapes.Add(allPolyLine[allPolyLine.Count - 1]);
                        allName.Add(new ShapeName(_textBoxName, "Polyline", textBoxRenderTransformAngle, textBoxRotateCenter,
                        textBoxScaleTransform, textBoxSkewTransform));

                        shapesForParse.Add(new ExtraForJSON("0", textBoxName,
                            textBoxPoints, numericUpDownStroke,
                            Colors[comboBoxColor],
                            textBoxRenderTransformAngle, textBoxRotateCenter, textBoxScaleTransform,
                            textBoxSkewTransform));

                        var TransGroup = new TransformGroup();

                        TransGroup.Children.Add(allName[allName.Count - 1].rotateTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].skewTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].scaleTransform);

                        allPolyLine[allPolyLine.Count - 1].RenderTransform = TransGroup;

                        newCanvas.Children.Add(allShapes[allShapes.Count - 1]);
                        allPolyLine.RemoveAt(allPolyLine.Count - 1);
                    }
                    if (Choosing == 2)
                    {
                        allPolygon.Add(newPolygon.PolygonFunc(_textBoxName, _textBoxPoints, Colors[_comboBoxColor], _numericUpDownStroke, Colors[_comboBoxFillColor]));
                        allShapes.Add(allPolygon[allPolygon.Count - 1]);
                        allName.Add(new ShapeName(_textBoxName, "Polygon", textBoxRenderTransformAngle, textBoxRotateCenter,
                        textBoxScaleTransform, textBoxSkewTransform));

                        shapesForParse.Add(new ExtraForJSON("0", textBoxName,
                            textBoxPoints, numericUpDownStroke,
                            Colors[comboBoxColor], Colors[comboBoxFillColor],
                            textBoxRenderTransformAngle, textBoxRotateCenter,
                            textBoxScaleTransform, textBoxSkewTransform, "1"));

                        var TransGroup = new TransformGroup();

                        TransGroup.Children.Add(allName[allName.Count - 1].rotateTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].skewTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].scaleTransform);

                        allPolygon[allPolygon.Count - 1].RenderTransform = TransGroup;
                        newCanvas.Children.Add(allShapes[allShapes.Count - 1]);
                        allPolygon.RemoveAt(allPolygon.Count - 1);
                    }
                    if (Choosing == 3)
                    {
                        allRectangle.Add(newRectangle.RectangleFunc(_textBoxName, _textBoxPoints, _textBoxWidth, _textBoxHeight, Colors[_comboBoxColor], _numericUpDownStroke, Colors[_comboBoxFillColor]));
                        allShapes.Add(allRectangle[allRectangle.Count - 1]);
                        allName.Add(new ShapeName(_textBoxName, "Rectangle", textBoxRenderTransformAngle, textBoxRotateCenter,
                        textBoxScaleTransform, textBoxSkewTransform));

                        shapesForParse.Add(new ExtraForJSON("0", textBoxName,
                            textBoxPoints, textBoxHeight, textBoxWidth,
                            numericUpDownStroke, Colors[comboBoxColor],
                            Colors[comboBoxFillColor], textBoxRenderTransformAngle,
                            textBoxRotateCenter, textBoxScaleTransform, textBoxSkewTransform));

                        var TransGroup = new TransformGroup();

                        TransGroup.Children.Add(allName[allName.Count - 1].rotateTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].skewTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].scaleTransform);

                        allRectangle[allRectangle.Count - 1].RenderTransform = TransGroup;
                        newCanvas.Children.Add(allShapes[allShapes.Count - 1]);
                        allRectangle.RemoveAt(allRectangle.Count - 1);
                    }
                    if (Choosing == 4)
                    {
                        allEllipse.Add(newEllipse.EllipseFunc(_textBoxName, _textBoxPoints, _textBoxWidth, _textBoxHeight, Colors[_comboBoxColor], _numericUpDownStroke, Colors[_comboBoxFillColor]));
                        allShapes.Add(allEllipse[allEllipse.Count - 1]);
                        allName.Add(new ShapeName(_textBoxName, "Ellipse", textBoxRenderTransformAngle, textBoxRotateCenter,
                        textBoxScaleTransform, textBoxSkewTransform));

                        shapesForParse.Add(new ExtraForJSON("0", textBoxName,
                            textBoxPoints, textBoxHeight, textBoxWidth, numericUpDownStroke,
                            Colors[comboBoxColor], Colors[comboBoxFillColor],
                            textBoxRenderTransformAngle, textBoxRotateCenter, textBoxScaleTransform,
                            textBoxSkewTransform, "1"));
                        
                        var TransGroup = new TransformGroup();

                        TransGroup.Children.Add(allName[allName.Count - 1].rotateTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].skewTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].scaleTransform);

                        allEllipse[allEllipse.Count - 1].RenderTransform = TransGroup;
                        newCanvas.Children.Add(allShapes[allShapes.Count - 1]);
                        allEllipse.RemoveAt(allEllipse.Count - 1);
                    }
                    if (Choosing == 5)
                    {
                        allPath.Add(newMixline.PathFunc(_textBoxName, _textBoxCommandPath, Colors[comboBoxColor], _numericUpDownStroke, Colors[_comboBoxFillColor]));
                        allShapes.Add(allPath[allPath.Count - 1]);
                        allName.Add(new ShapeName(_textBoxName, "Path", textBoxCommandPath, textBoxRenderTransformAngle, textBoxRotateCenter,
                        textBoxScaleTransform, textBoxSkewTransform));

                        shapesForParse.Add(new ExtraForJSON("0", textBoxName, textBoxCommandPath,
                            numericUpDownStroke, Colors[comboBoxColor], Colors[comboBoxFillColor],
                            textBoxRenderTransformAngle, textBoxRotateCenter, textBoxScaleTransform,
                            textBoxSkewTransform, "1", "2", "3", "4"));

                        var TransGroup = new TransformGroup();

                        TransGroup.Children.Add(allName[allName.Count - 1].rotateTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].skewTransform);

                        TransGroup.Children.Add(allName[allName.Count - 1].scaleTransform);

                        allPath[allPath.Count - 1].RenderTransform = TransGroup;

                        newCanvas.Children.Add(allShapes[allShapes.Count - 1]);
                        allPath.RemoveAt(allPath.Count - 1);
                    }
                }
            }
            else
            {
                Editor(listBoxIndex);
            }
            foreach(var item in shapesForParse)
            {
                Debug.WriteLine(item.name + item.type);
            }
        }

        public void Editor(int index)
        {
           /* allLines.Add(newLine.LineFunc(_textBoxName, _textBoxStart, _textBoxEnd, Colors[_comboBoxColor], _numericUpDownStroke));
            allShapes.Add(allLines[allLines.Count - 1]);
            allName.Add(new ShapeName(_textBoxName, "Line", textBoxRenderTransformAngle, textBoxRotateCenter,
            textBoxScaleTransform, textBoxSkewTransform));
            allShapes[allShapes.Count - 1].RenderTransform = allName[allName.Count - 1].rotateTransform;
            newCanvas.Children.Add(allShapes[allShapes.Count - 1]);
            allLines.RemoveAt(allLines.Count - 1);*/
            if (Choosing == 0)
            {
                allLines.Add(newLine.LineFunc(textBoxName, textBoxStart, textBoxEnd, Colors[comboBoxColor], numericUpDownStroke));

                ShapeName Edits = new ShapeName(textBoxName, "Line", textBoxRenderTransformAngle, textBoxRotateCenter,
                textBoxScaleTransform, textBoxSkewTransform);

                ExtraForJSON tempJSON = new ExtraForJSON("0", textBoxName, textBoxStart,
                            textBoxEnd, numericUpDownStroke,
                            Colors[comboBoxColor], textBoxRenderTransformAngle,
                            textBoxRotateCenter, textBoxScaleTransform,
                            textBoxSkewTransform);

                shapesForParse.Replace(shapesForParse[index], tempJSON);

                allName.Replace(allName[index], Edits);

                var TransGroup = new TransformGroup();

                TransGroup.Children.Add(allName[index].rotateTransform);

                TransGroup.Children.Add(allName[index].skewTransform);

                TransGroup.Children.Add(allName[index].scaleTransform);

                allLines[allLines.Count - 1].RenderTransform = TransGroup;
                
                newCanvas.Children.Replace(allShapes[index], allLines[allLines.Count - 1]);
                
                allShapes.Replace(allShapes[index], allLines[allLines.Count-1]);
                
                allLines.RemoveAt(allLines.Count - 1);
            }
            if (Choosing == 1)
            {
                allPolyLine.Add(newPolyLine.PolyLineFunc(_textBoxName, _textBoxPoints, Colors[_comboBoxColor], _numericUpDownStroke));

                ShapeName Edits = new ShapeName(textBoxName, "Polyline", textBoxRenderTransformAngle, textBoxRotateCenter,
                textBoxScaleTransform, textBoxSkewTransform);

                ExtraForJSON forJSON = new ExtraForJSON("0", textBoxName,
                            textBoxPoints, numericUpDownStroke,
                            Colors[comboBoxColor],
                            textBoxRenderTransformAngle, textBoxRotateCenter, textBoxScaleTransform,
                            textBoxSkewTransform);

                shapesForParse.Replace(shapesForParse[index], forJSON);

                allName.Replace(allName[index], Edits);

                var TransGroup = new TransformGroup();

                TransGroup.Children.Add(allName[index].rotateTransform);

                TransGroup.Children.Add(allName[index].skewTransform);

                TransGroup.Children.Add(allName[index].scaleTransform);

                allPolyLine[allPolyLine.Count - 1].RenderTransform = TransGroup;

                newCanvas.Children.Replace(allShapes[index], allPolyLine[allPolyLine.Count - 1]);
                allShapes.Replace(allShapes[index], allPolyLine[allPolyLine.Count - 1]);
                allName.Replace(allName[index], Edits);
                allPolyLine.RemoveAt(allPolyLine.Count - 1);
            }
            if (Choosing == 2)
            {
                allPolygon.Add(newPolygon.PolygonFunc(_textBoxName, _textBoxPoints, Colors[_comboBoxColor], _numericUpDownStroke, Colors[_comboBoxFillColor]));

                ShapeName Edits = new ShapeName(textBoxName, "Polygon", textBoxRenderTransformAngle, textBoxRotateCenter,
                textBoxScaleTransform, textBoxSkewTransform);

                ExtraForJSON forJSON3 = new ExtraForJSON("0", textBoxName,
                            textBoxPoints, numericUpDownStroke,
                            Colors[comboBoxColor], Colors[comboBoxFillColor],
                            textBoxRenderTransformAngle, textBoxRotateCenter,
                            textBoxScaleTransform, textBoxSkewTransform, "1");

                shapesForParse.Replace(shapesForParse[index], forJSON3);

                allName.Replace(allName[index], Edits);

                var TransGroup = new TransformGroup();

                TransGroup.Children.Add(allName[index].rotateTransform);

                TransGroup.Children.Add(allName[index].skewTransform);

                TransGroup.Children.Add(allName[index].scaleTransform);

                allPolygon[allPolygon.Count - 1].RenderTransform = TransGroup;

                newCanvas.Children.Replace(allShapes[index], allPolygon[allPolygon.Count - 1]);
                allShapes.Replace(allShapes[index],allPolygon[allPolygon.Count - 1]);
                allName.Replace(allName[index], Edits);
                allPolygon.RemoveAt(allPolygon.Count - 1);
            }
            if (Choosing == 3)
            {
                allRectangle.Add(newRectangle.RectangleFunc(_textBoxName, _textBoxPoints, _textBoxWidth, _textBoxHeight, Colors[_comboBoxColor], _numericUpDownStroke, Colors[_comboBoxFillColor]));

                ShapeName Edits = new ShapeName(textBoxName, "Rectangle", textBoxRenderTransformAngle, textBoxRotateCenter,
                textBoxScaleTransform, textBoxSkewTransform);

                ExtraForJSON forJSON4 = new ExtraForJSON("0", textBoxName,
                            textBoxPoints, textBoxHeight, textBoxWidth,
                            numericUpDownStroke, Colors[comboBoxColor],
                            Colors[comboBoxFillColor], textBoxRenderTransformAngle,
                            textBoxRotateCenter, textBoxScaleTransform, textBoxSkewTransform);

                shapesForParse.Replace(shapesForParse[index], forJSON4);

                allName.Replace(allName[index], Edits);

                var TransGroup = new TransformGroup();

                TransGroup.Children.Add(allName[index].rotateTransform);

                TransGroup.Children.Add(allName[index].skewTransform);

                TransGroup.Children.Add(allName[index].scaleTransform);

                allRectangle[allRectangle.Count - 1].RenderTransform = TransGroup;

                newCanvas.Children.Replace(allShapes[index],allRectangle[allRectangle.Count - 1]);
                allShapes.Replace(allShapes[index],allRectangle[allRectangle.Count - 1]);
                allName.Replace(allName[index], Edits);
                allRectangle.RemoveAt(allRectangle.Count - 1);
            }
            if (Choosing == 4)
            {
                allEllipse.Add(newEllipse.EllipseFunc(_textBoxName, _textBoxPoints, _textBoxWidth, _textBoxHeight, Colors[_comboBoxColor], _numericUpDownStroke, Colors[_comboBoxFillColor]));

                ShapeName Edits = new ShapeName(textBoxName, "Ellipse", textBoxRenderTransformAngle, textBoxRotateCenter,
                textBoxScaleTransform, textBoxSkewTransform);

                ExtraForJSON forJSON5 =
       new ExtraForJSON("0", textBoxName,
                            textBoxPoints, textBoxHeight, textBoxWidth, numericUpDownStroke,
                            Colors[comboBoxColor], Colors[comboBoxFillColor],
                            textBoxRenderTransformAngle, textBoxRotateCenter, textBoxScaleTransform,
                            textBoxSkewTransform, "1");

                shapesForParse.Replace(shapesForParse[index], forJSON5);

                allName.Replace(allName[index], Edits);

                var TransGroup = new TransformGroup();

                TransGroup.Children.Add(allName[index].rotateTransform);

                TransGroup.Children.Add(allName[index].skewTransform);

                TransGroup.Children.Add(allName[index].scaleTransform);

                allEllipse[allEllipse.Count - 1].RenderTransform = TransGroup;

                newCanvas.Children.Replace(allShapes[index],allEllipse[allEllipse.Count - 1]);
                allShapes.Replace(allShapes[index],allEllipse[allEllipse.Count - 1]);
                allName.Replace(allName[index], Edits);
                allEllipse.RemoveAt(allEllipse.Count - 1);
            }
            if (Choosing == 5)
            {
                allPath.Add(newMixline.PathFunc(_textBoxName, _textBoxCommandPath, Colors[comboBoxColor], _numericUpDownStroke, Colors[_comboBoxFillColor]));

                ShapeName Edits = new ShapeName(textBoxName, "Path", _textBoxCommandPath, textBoxRenderTransformAngle, textBoxRotateCenter,
                textBoxScaleTransform, textBoxSkewTransform);

                ExtraForJSON forJSON6 = new ExtraForJSON("0", textBoxName, textBoxCommandPath,
                            numericUpDownStroke, Colors[comboBoxColor], Colors[comboBoxFillColor],
                            textBoxRenderTransformAngle, textBoxRotateCenter, textBoxScaleTransform,
                            textBoxSkewTransform, "1", "2", "3", "4");

                shapesForParse.Replace(shapesForParse[index], forJSON6);

                allName.Replace(allName[index], Edits);

                var TransGroup = new TransformGroup();

                TransGroup.Children.Add(allName[index].rotateTransform);

                TransGroup.Children.Add(allName[index].skewTransform);

                TransGroup.Children.Add(allName[index].scaleTransform);

                allPath[allPath.Count - 1].RenderTransform = TransGroup;

                newCanvas.Children.Replace(allShapes[index],allPath[allPath.Count - 1]);
                allShapes.Replace(allShapes[index] ,allPath[allPath.Count - 1]);
                allName.Replace(allName[index], Edits);
                allPath.RemoveAt(allPath.Count - 1);
            }
        }

        public void ChangeParametr(int index)
        {
            textBoxName = allName[index].Name;
            comboBoxColor = FindColor(allShapes[index].Stroke.ToString());
            numericUpDownStroke = int.Parse(allShapes[index].StrokeThickness.ToString());
            textBoxRenderTransformAngle = allName[index].rotateTransform.Angle.ToString();
            textBoxRotateCenter = allName[index].rotateTransform.CenterX.ToString();
            textBoxRotateCenter += " ";
            textBoxRotateCenter += allName[index].rotateTransform.CenterY.ToString();
            textBoxScaleTransform = allName[index].scaleTransform.ScaleX.ToString();
            textBoxScaleTransform += " ";
            textBoxScaleTransform += allName[index].scaleTransform.ScaleY.ToString();
            textBoxSkewTransform = allName[index].skewTransform.AngleX.ToString();
            textBoxSkewTransform += " ";
            textBoxSkewTransform += allName[index].skewTransform.AngleY.ToString();
            if (allName[index].Type == "Line")
            {
                Line EditLine = (Line)allShapes[index];
                textBoxStart = EditLine.StartPoint.ToString();
                textBoxEnd = EditLine.EndPoint.ToString();
                comboBoxShape = 0;
            }
            if (allName[index].Type == "Polyline")
            {
                Polyline EditPolyline = (Polyline)allShapes[index];
                string temp = "";
                for (int i = 0; i < EditPolyline.Points.Count; i++)
                {
                    string newTemp = EditPolyline.Points[i].ToString();
                    newTemp = newTemp.Replace(",", "");
                    temp += newTemp;
                    if (i != EditPolyline.Points.Count - 1)
                    {
                        temp += ",";
                    }
                }
                textBoxPoints = temp;
                comboBoxShape = 1;
            }
            if (allName[index].Type == "Polygon")
            {
                Polygon EditPolygon = (Polygon)allShapes[index];
                string temp = "";
                for (int i = 0; i < EditPolygon.Points.Count; i++)
                {
                    string newTemp = EditPolygon.Points[i].ToString();
                    newTemp = newTemp.Replace(",", "");
                    temp += newTemp;
                    if (i != EditPolygon.Points.Count - 1)
                    {
                        temp += ",";
                    }
                }
                textBoxPoints = temp;
                comboBoxShape = 2;
                comboBoxFillColor = FindColor(allShapes[index].Fill.ToString());
            }
            if (allName[index].Type == "Rectangle")
            {
                comboBoxShape = 3;
                comboBoxFillColor = FindColor(allShapes[index].Fill.ToString());
                textBoxHeight = allShapes[index].Height.ToString();
                textBoxWidth = allShapes[index].Width.ToString();
                string temp = allShapes[index].Margin.ToString();
                string[] tempe = temp.Split(",");
                temp = tempe[0] + " " + tempe[1];
                textBoxPoints = temp;
            }
            if (allName[index].Type == "Ellipse")
            {
                comboBoxShape = 4;
                comboBoxFillColor = FindColor(allShapes[index].Fill.ToString());
                textBoxHeight = allShapes[index].Height.ToString();
                textBoxWidth = allShapes[index].Width.ToString();
                string temp = allShapes[index].Margin.ToString();
                string[] tempe = temp.Split(",");
                temp = tempe[0] + " " + tempe[1];
                textBoxPoints = temp;
            }
            if (allName[index].Type == "Path")
            {
                comboBoxShape = 5;
                comboBoxFillColor = FindColor(allShapes[index].Fill.ToString());
                Path EditPath = (Path)allShapes[index];
                textBoxCommandPath = allName[index].pathCommands;
                Debug.WriteLine(allName[index].pathCommands);
                //Geometry EditGeom = (Geometry)EditPath.DataContext;
               // textBoxCommandPath = EditGeom.ToString();
                 
            }
        }

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

        private ObservableCollection<Line> _allLines;

        public ObservableCollection<Line> allLines
        {
            get { return _allLines; }
            set { this.RaiseAndSetIfChanged(ref _allLines, value); }
        }

        private ObservableCollection<Polyline> _allPolyLine;

        public ObservableCollection<Polyline> allPolyLine
        {
            get { return _allPolyLine; }
            set { this.RaiseAndSetIfChanged(ref _allPolyLine, value); }
        }

        private ObservableCollection<Polygon> _allPolygon;

        public ObservableCollection<Polygon> allPolygon
        {
            get { return _allPolygon; }
            set { this.RaiseAndSetIfChanged(ref _allPolygon, value); }
        }

        private ObservableCollection<Rectangle> _allRectangle;

        public ObservableCollection<Rectangle> allRectangle
        {
            get { return _allRectangle; }
            set { this.RaiseAndSetIfChanged(ref _allRectangle, value); }
        }

        private ObservableCollection<Ellipse> _allEllipse;

        public ObservableCollection<Ellipse> allEllipse
        {
            get { return _allEllipse; }
            set { this.RaiseAndSetIfChanged(ref _allEllipse, value); }
        }

        private ObservableCollection<Path> _allPath;

        public ObservableCollection<Path> allPath
        {
            get { return _allPath; }
            set { this.RaiseAndSetIfChanged(ref _allPath, value); }
        }
        // ("100,100", "500,500", "Red", 3)
        public int Choosing = 0;
        public static MainWindowViewModel? MaybeMainWindow { private get; set; }
        public MainWindowViewModel(MainWindow MaybeMainWindow)
        {
            newCanvas = new Canvas();
            newCanvas = MaybeMainWindow.Find<Canvas>("canvas");
            allName = new ObservableCollection<ShapeName>();
            content = allContent[0];
            allLines = new ObservableCollection<Line>();
            allPolyLine = new ObservableCollection<Polyline>();
            allPolygon = new ObservableCollection<Polygon>();
            allRectangle = new ObservableCollection<Rectangle>();
            allEllipse = new ObservableCollection<Ellipse>();
            allPath = new ObservableCollection<Path>();
            allShapes = new ObservableCollection<Shape>();
            listBoxIndex = -1;
            pngsavers = new PNGSaver();
            jsonloaders = new JSONLoaderNew();
            xmlsavers = new XMLSavers();
            xmlloaders = new XMLLoader();
            textBoxRenderTransformAngle = "0";
            textBoxRotateCenter = "0 0";
            textBoxScaleTransform = "0 0";
            textBoxSkewTransform = "0 0";
            shapesForParse = new ObservableCollection<ExtraForJSON>();
            jsonsaver = new JSONSaverNew();
        }
        private UserControl content;

        private readonly UserControl[] allContent = new UserControl[]
        {
            new LineUserControl(),
            new BrokenLineUserControl(),
            new MultipleCornersUserControl(),
            new RectangleFigureUserControl(),
            new EllpseFigureUserControl(),
            new MixLineUserControl()
        };
        public UserControl Content
        {
            get { return content; }
            set { this.RaiseAndSetIfChanged(ref content, value); }
        }

        public int SelectedFigure
        {
            get => Choosing;
            set
            {
                this.RaiseAndSetIfChanged(ref Choosing, value);
                Content = allContent[value];
                //CommandFormat = Formats[SelectedFigure];
            }
        }

        private string _textBoxName;

        public string textBoxName
        {
            get => _textBoxName;
            set { this.RaiseAndSetIfChanged(ref _textBoxName, value); }
        }

        private string _textBoxStart;
        public string textBoxStart
        {
            get => _textBoxStart;
            set { this.RaiseAndSetIfChanged(ref _textBoxStart, value);}
        }

        private string _textBoxEnd;
        public string textBoxEnd
        {
            get => _textBoxEnd;
            set { this.RaiseAndSetIfChanged(ref _textBoxEnd, value); }
        }

        private int _comboBoxColor;
        public int comboBoxColor
        {
            get => _comboBoxColor;
            set { this.RaiseAndSetIfChanged(ref _comboBoxColor, value); }
        }

        private int _numericUpDownStroke;
        public int numericUpDownStroke
        {
            get => _numericUpDownStroke;
            set { this.RaiseAndSetIfChanged(ref _numericUpDownStroke, value); }
        }

        private string _textBoxPoints;
        public string textBoxPoints
        {
            get => _textBoxPoints;
            set { this.RaiseAndSetIfChanged(ref _textBoxPoints, value); }
        }

        private int _comboBoxShape;
        public int comboBoxShape
        {
            get => Choosing;
            set
            {
                this.RaiseAndSetIfChanged(ref Choosing, value);
                Content = allContent[value];
            }
        }
        private int _comboBoxFillColor;
        public int comboBoxFillColor
        {
            get => _comboBoxFillColor;
            set { this.RaiseAndSetIfChanged(ref _comboBoxFillColor, value); }
        }

        private string _textBoxWidth;
        public string textBoxWidth
        {
            get => _textBoxWidth;
            set { this.RaiseAndSetIfChanged(ref _textBoxWidth, value); }
        }

        private string _textBoxHeight;
        public string textBoxHeight
        {
            get => _textBoxHeight;
            set { this.RaiseAndSetIfChanged(ref _textBoxHeight, value); }
        }

        private string _textBoxCommandPath;
        public string textBoxCommandPath
        {
            get => _textBoxCommandPath;
            set { this.RaiseAndSetIfChanged(ref _textBoxCommandPath, value); }
        }

        private int _listBoxIndex;
        public int listBoxIndex
        {
            get => _listBoxIndex;
            set { 
                this.RaiseAndSetIfChanged(ref _listBoxIndex, value);
                if (listBoxIndex == -1)
                {
                    textBoxName = "";
                    numericUpDownStroke = 0;
                    textBoxStart = "";
                    textBoxEnd = "";
                    textBoxRenderTransformAngle = "0";
                    textBoxRotateCenter = "0 0";
                    textBoxScaleTransform = "0 0";
                    textBoxSkewTransform = "0 0";
                }
                else ChangeParametr(listBoxIndex);
                }
        }

        private string _textBoxRenderTransformAngle;
        public string textBoxRenderTransformAngle
        {
            get => _textBoxRenderTransformAngle;
            set { this.RaiseAndSetIfChanged(ref _textBoxRenderTransformAngle, value); }
        }

        private string _textBoxRotateCenter;
        public string textBoxRotateCenter
        {
            get => _textBoxRotateCenter;
            set { this.RaiseAndSetIfChanged(ref _textBoxRotateCenter, value); }
        }

        private string _textBoxScaleTransform;
        public string textBoxScaleTransform
        {
            get => _textBoxScaleTransform;
            set { this.RaiseAndSetIfChanged(ref _textBoxScaleTransform, value); }
        }

        private string _textBoxSkewTransform;
        public string textBoxSkewTransform
        {
            get => _textBoxSkewTransform;
            set { this.RaiseAndSetIfChanged(ref _textBoxSkewTransform, value); }
        }
        public PNGSaver pngsavers;

        public JSONLoaderNew jsonloaders;

        public XMLSavers xmlsavers;

        public XMLLoader xmlloaders;

        public JSONSaverNew jsonsaver;

        public void SaveFigures(string path)
        {
            if (PathFile.GetExtension(path) == ".png")
            {
                pngsavers.Save(allShapes,allName, path, newCanvas);
            }
            if(PathFile.GetExtension(path) == ".xml")
            {
                xmlsavers.Save(allShapes, allName, path, newCanvas);
            }
            if (PathFile.GetExtension(path) == ".json")
            {
                jsonsaver.Save(shapesForParse, path);
                //jsonsavers.Save(allShapes, allName, path, newCanvas);
            }
        }

        public void LoadFigures(string path)
        {
            if (PathFile.GetExtension(path) == ".json")
            {
                newCanvas.Children.RemoveAll(allShapes);
                shapesForParse = new ObservableCollection<ExtraForJSON>();

                allName = new ObservableCollection<ShapeName>();

                allShapes = new ObservableCollection<Shape>();

                List<ExtraForJSON> newShapes = jsonloaders.Load(path);

                for (int i = 0; i < newShapes.Count; i++)
                {
                    if (newShapes[i].type == "Line")
                    {
                        shapesForParse.Add(new ExtraForJSON(newShapes[i].type,
                            newShapes[i].name, newShapes[i].startPoint,
                            newShapes[i].endPoint, newShapes[i].lineThickness,
                            newShapes[i].lineColor, newShapes[i].rotateAngle,
                            newShapes[i].rotateCenter, newShapes[i].scaleValue,
                            newShapes[i].skewValue));

                        allName.Add(new ShapeName(newShapes[i].name, newShapes[i].type, newShapes[i].rotateAngle, newShapes[i].rotateCenter, newShapes[i].scaleValue, newShapes[i].skewValue));

                        LineClass new1 = new LineClass();

                        allShapes.Add(new1.LineFunc(newShapes[i].name, newShapes[i].startPoint, newShapes[i].endPoint, newShapes[i].lineColor, newShapes[i].lineThickness));
                    }

                    if (newShapes[i].type == "Polyline")
                    {
                        shapesForParse.Add(new ExtraForJSON(newShapes[i].type,
                            newShapes[i].name, newShapes[i].figPoints,
                            newShapes[i].lineThickness,
                            newShapes[i].lineColor,newShapes[i].rotateAngle,
                            newShapes[i].rotateCenter, newShapes[i].scaleValue,
                            newShapes[i].skewValue));

                        allName.Add(new ShapeName(newShapes[i].name, newShapes[i].type, newShapes[i].rotateAngle, newShapes[i].rotateCenter, newShapes[i].scaleValue, newShapes[i].skewValue));

                        PolylineClass new2 = new PolylineClass();

                        allShapes.Add(new2.PolyLineFunc(newShapes[i].name, newShapes[i].figPoints, newShapes[i].lineColor, newShapes[i].lineThickness));
                    }
                    if (newShapes[i].type == "Polygon")
                    {
                        shapesForParse.Add(new ExtraForJSON(newShapes[i].type,
                            newShapes[i].name, newShapes[i].figPoints,
                            newShapes[i].lineThickness,
                            newShapes[i].lineColor, 
                            newShapes[i].fillColor, newShapes[i].rotateAngle,
                            newShapes[i].rotateCenter, newShapes[i].scaleValue,
                            newShapes[i].skewValue, "0"));

                        allName.Add(new ShapeName(newShapes[i].name, newShapes[i].type, newShapes[i].rotateAngle, newShapes[i].rotateCenter, newShapes[i].scaleValue, newShapes[i].skewValue));

                        MultipleCornersClass new3 = new MultipleCornersClass();

                        allShapes.Add(new3.PolygonFunc(newShapes[i].name, newShapes[i].figPoints, newShapes[i].lineColor, newShapes[i].lineThickness, newShapes[i].fillColor));
                    }
                    if (newShapes[i].type == "Rectangle")
                    {
                        shapesForParse.Add(new ExtraForJSON(newShapes[i].type,
                            newShapes[i].name, newShapes[i].figPoints,
                            newShapes[i].height, newShapes[i].width,
                            newShapes[i].lineThickness,
                            newShapes[i].lineColor,
                            newShapes[i].fillColor, newShapes[i].rotateAngle,
                            newShapes[i].rotateCenter, newShapes[i].scaleValue,
                            newShapes[i].skewValue));

                        allName.Add(new ShapeName(newShapes[i].name, newShapes[i].type, newShapes[i].rotateAngle, newShapes[i].rotateCenter, newShapes[i].scaleValue, newShapes[i].skewValue));

                        RectangleClass new4 = new RectangleClass();

                        allShapes.Add(new4.RectangleFunc(newShapes[i].name, newShapes[i].figPoints, newShapes[i].width, newShapes[i].height, newShapes[i].lineColor, newShapes[i].lineThickness,
                            newShapes[i].fillColor));
                    }
                    if (newShapes[i].type == "Ellipse")
                    {
                        shapesForParse.Add(new ExtraForJSON(newShapes[i].type,
                           newShapes[i].name, newShapes[i].figPoints,
                           newShapes[i].height, newShapes[i].width,
                           newShapes[i].lineThickness,
                           newShapes[i].lineColor,
                           newShapes[i].fillColor, newShapes[i].rotateAngle,
                           newShapes[i].rotateCenter, newShapes[i].scaleValue,
                           newShapes[i].skewValue, "0"));

                        allName.Add(new ShapeName(newShapes[i].name, newShapes[i].type, newShapes[i].rotateAngle, newShapes[i].rotateCenter, newShapes[i].scaleValue, newShapes[i].skewValue));

                        EllipseClass new5 = new EllipseClass();

                        allShapes.Add(new5.EllipseFunc(newShapes[i].name, newShapes[i].figPoints, newShapes[i].width, newShapes[i].height,
                            newShapes[i].lineColor, newShapes[i].lineThickness, newShapes[i].fillColor));
                    }
                    if (newShapes[i].type == "Path")
                    {
                        shapesForParse.Add(new ExtraForJSON(newShapes[i].type,
                          newShapes[i].name, newShapes[i].pathCommands,
                          newShapes[i].lineThickness,
                          newShapes[i].lineColor,
                          newShapes[i].fillColor, newShapes[i].rotateAngle,
                          newShapes[i].rotateCenter, newShapes[i].scaleValue,
                          newShapes[i].skewValue, "1", "2", "3", "4"));

                        allName.Add(new ShapeName(newShapes[i].name, newShapes[i].type, newShapes[i].pathCommands, newShapes[i].rotateAngle, newShapes[i].rotateCenter, newShapes[i].scaleValue, newShapes[i].skewValue));

                        MixLineClass new6 = new MixLineClass();

                        allShapes.Add(new6.PathFunc(newShapes[i].name, newShapes[i].pathCommands, newShapes[i].lineColor, newShapes[i].lineThickness, newShapes[i].fillColor));
                    }

                }

                //foreach(var item in allShapes)
                //{
                  //  newCanvas.Children.Add(item);
                //}
                Debug.WriteLine(allShapes.Count);
                for (int i = 0; i < allShapes.Count; i++)
                {
                    var TransGroup = new TransformGroup();
                    TransGroup.Children.Add(allName[i].rotateTransform);
                    TransGroup.Children.Add(allName[i].skewTransform);
                    TransGroup.Children.Add(allName[i].scaleTransform);
                    allShapes[i].RenderTransform = TransGroup;
                    newCanvas.Children.Add(allShapes[i]);
                }
            }
            if (PathFile.GetExtension(path) == ".xml")
            {
                newCanvas.Children.RemoveAll(allShapes);
                Tuple<ObservableCollection<Shape>, ObservableCollection<ShapeName>, ObservableCollection<ExtraForJSON>> tulup = xmlloaders.Load(path);
                ObservableCollection<Shape> temp = new ObservableCollection<Shape>(tulup.Item1);
                ObservableCollection<ShapeName> temp1 = new ObservableCollection<ShapeName>(tulup.Item2);
                allShapes = temp;
                allName = temp1;
                shapesForParse = new ObservableCollection<ExtraForJSON>(tulup.Item3);

                Debug.WriteLine(allShapes.Count);
                for (int i = 0; i < allShapes.Count; i++)
                {
                        var TransGroup = new TransformGroup();
                        TransGroup.Children.Add(allName[i].rotateTransform);
                        TransGroup.Children.Add(allName[i].skewTransform);
                        TransGroup.Children.Add(allName[i].scaleTransform);
                        allShapes[i].RenderTransform = TransGroup;
                        newCanvas.Children.Add(allShapes[i]);
                }
            }
        }
    }
}
