using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Models
{
    public interface IFigureLoader
    {
        IEnumerable<Shape> Load(string path);
    }
}