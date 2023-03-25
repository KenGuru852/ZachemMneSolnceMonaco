using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Paint.Models
{
    public class JSONLoader : IFigureLoader
    {
        public IEnumerable<Shape> Load(string path)
        {
            ObservableCollection<Shape> allShapes = new ObservableCollection<Shape>();

            Rectangle allRects = new Rectangle();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                allRects = JsonSerializer.Deserialize<Rectangle>(fs);
                if (allShapes == null)
                {
                    allRects = new Rectangle();
                }
            }
            allShapes.Add(allRects);
            return allShapes;
        }
    }
}
