using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Paint.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive;  

namespace Paint.Views
{
    public partial class MainWindow : Window
    {

        protected bool isDragging;
        private Avalonia.Point clickPosition;
        private TranslateTransform originTT;
        private TranslateTransform changedPoints;
        private TransformGroup trGpoup;

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
      

    }
}