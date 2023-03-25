using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Models
{
    public interface IFigureSaver
    {
        void Save(ObservableCollection<Shape> shapes, ObservableCollection<ShapeName> shapeNames, string path, Canvas canv);
    }
}
