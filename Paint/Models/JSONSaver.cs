using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Paint.Models
{
    public class JSONSaver
    {
        public void Save(ObservableCollection<Shape> shapes, ObservableCollection<ShapeName> shapeNames, string path, Canvas canv)
        {
            Tuple<ObservableCollection<Shape>, ObservableCollection<ShapeName>> tulup = Tuple.Create(shapes, shapeNames);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, tulup,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                        ReferenceHandler = ReferenceHandler.Preserve
                    });
            }
        }
    }
}
