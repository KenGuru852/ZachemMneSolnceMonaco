using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using Paint.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive;
using APoint = Avalonia.Point;

namespace Paint.Views
{
    public partial class MainWindow : Window
    {

        protected bool isDragging;
        private Avalonia.Point clickPosition;
        private TranslateTransform originTT;
        private TranslateTransform changedPoints;
        private TransformGroup trGpoup;
        private APoint pointPointerPressed;
        private APoint pointerPositionIntoShape;
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindow();
            DataContext = new MainWindowViewModel(this);
            AddHandler(DragDrop.DropEvent, DragEnterFunc);
            AddHandler(DragDrop.DropEvent, DragDropFunc);
        }
        
        public void DragEnterFunc(object sender, DragEventArgs e)
        {
            e.DragEffects = DragDropEffects.Move;
        }
        public void DragDropFunc(object sender, DragEventArgs e)
        {
            List<string> path = (List<string>)e.Data.Get(DataFormats.FileNames);
            if (DataContext is MainWindowViewModel dataContext)
            {
               dataContext.LoadFigures(path.ElementAt(0));
            }
        }
        public async void ExportClick(object sender, RoutedEventArgs eventArgs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filters.Add(
                new FileDialogFilter
                {
                    Name = "Xml files",
                    Extensions = new string[] { "xml" }.ToList()
                });
            saveFileDialog.Filters.Add(
                new FileDialogFilter
                {
                    Name = "JSON files",
                    Extensions = new string[] { "json" }.ToList()
                });
            saveFileDialog.Filters.Add(
                new FileDialogFilter
                {
                    Name = "PNG files",
                    Extensions = new string[] { "png" }.ToList()
                });
            string? path = await saveFileDialog.ShowAsync(this);
            if (path != null)
            {
                if (this.DataContext is MainWindowViewModel dataContext)
                {
                    dataContext.SaveFigures(path);
                }
            }
        }
        
        private async void ImportClick(object sender, RoutedEventArgs eventArgs)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filters.Add(
                new FileDialogFilter
                {
                    Name = "Xml files",
                    Extensions = new string[] { "xml" }.ToList()
                });

            openFileDialog.Filters.Add(
                new FileDialogFilter
                {
                    Name = "JSON files",
                    Extensions = new string[] { "json" }.ToList()
                });

            string[]? path = await openFileDialog.ShowAsync(this);

            if (path != null)
            {
                if (this.DataContext is MainWindowViewModel dataContext)
                {
                    dataContext.LoadFigures(path[0]);
                }
            }
        }
        private void OnPointerPressed(object? sender, PointerPressedEventArgs pointerPressedEventArgs)
        {
            
            if (pointerPressedEventArgs.Source is Shape control)
            {
                if (control.DataContext is Paint.ViewModels.MainWindowViewModel rectangle)
                {
                    pointPointerPressed = pointerPressedEventArgs
                        .GetPosition(
                        this.GetVisualDescendants()
                        .OfType<Canvas>()
                        .FirstOrDefault(canvas => string.IsNullOrEmpty(canvas.Name) == false &&
                        canvas.Name.Equals("canvas")));
                    

                        pointerPositionIntoShape = pointerPressedEventArgs.GetPosition(control);
                        this.PointerMoved += PointerMoveDragShape;
                        this.PointerReleased += PointerPressedReleasedDragShape;
                }
            }
        }

        private void PointerMoveDragShape(object? sender, PointerEventArgs pointerEventArgs)
        {
            string First = "";
            string Second = "";
            double StartXLine = 0;
            double StartYLine = 0;

            double EndXLine = 0;
            double EndYLine = 0;
            if (pointerEventArgs.Source is Avalonia.Controls.Shapes.Shape rectangle)
            {
                    Avalonia.Point currentPointerPosition = pointerEventArgs
                    .GetPosition(
                    this.GetVisualDescendants()
                    .OfType<Canvas>()
                    .FirstOrDefault());

                    First = (currentPointerPosition.X - pointerPositionIntoShape.X).ToString();
                    Second = (currentPointerPosition.Y - pointerPositionIntoShape.Y).ToString();

                    rectangle.Margin = new(double.Parse(First), double.Parse(Second));
            }
            /*else if (pointerEventArgs.Source is Avalonia.Controls.Shapes.Ellipse ellipse)
            {
                Avalonia.Point currentPointerPosition = pointerEventArgs
                .GetPosition(
                this.GetVisualDescendants()
                .OfType<Canvas>()
                .FirstOrDefault());

                First = (currentPointerPosition.X - pointerPositionIntoShape.X).ToString();
                Second = (currentPointerPosition.Y - pointerPositionIntoShape.Y).ToString();

                ellipse.Margin = new(double.Parse(First), double.Parse(Second));
            }
            else if (pointerEventArgs.Source is Avalonia.Controls.Shapes.Line Line)
            {
                Avalonia.Point currentPointerPosition = pointerEventArgs
                .GetPosition(
                this.GetVisualDescendants()
                .OfType<Canvas>()
                .FirstOrDefault());

                First = (currentPointerPosition.X - pointerPositionIntoShape.X).ToString();
                Second = (currentPointerPosition.Y - pointerPositionIntoShape.Y).ToString();

                Debug.WriteLine(First);
                Debug.WriteLine(Second);

                StartXLine = Line.StartPoint.X;
                StartYLine = Line.StartPoint.Y;

                EndXLine = Line.EndPoint.X;
                EndYLine = Line.EndPoint.Y;

                StartXLine = StartXLine + double.Parse(First);
                StartYLine = StartYLine + double.Parse(Second);

                EndXLine = EndXLine + double.Parse(First);
                EndYLine = EndYLine + double.Parse(Second);

                Line.Margin = new(double.Parse(First), double.Parse(Second));

                //Line.StartPoint = new APoint(StartXLine, StartYLine);
                //Line.EndPoint = new APoint(EndXLine, EndYLine);
            }*/
        }           
               
        private void PointerPressedReleasedDragShape(object? sender,
            PointerReleasedEventArgs pointerReleasedEventArgs)
        {
            this.PointerMoved -= PointerMoveDragShape;
            this.PointerReleased -= PointerPressedReleasedDragShape;
        }

    }
}