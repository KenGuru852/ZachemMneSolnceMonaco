using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls.Shapes;
using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Controls;
using System.Collections.ObjectModel;

namespace Paint.Models
{
    public class PNGSaver : IFigureSaver
    {
        public void Save(ObservableCollection<Shape> shapes, ObservableCollection<ShapeName> shapeNames, string path, Canvas canv)
        {
            var pixelSize = new PixelSize(1000, 1000);
            var size = new Size(1000, 1000);
            using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pixelSize, new Vector(96, 96)))
            {
                canv.Measure(size);
                canv.Arrange(new Rect(size));
                bitmap.Render(canv);
                bitmap.Save(path);
            }
            /*File.WriteAllText(path, string.Empty);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, shapes,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                        ReferenceHandler = ReferenceHandler.Preserve
                    }); ; ;
            }*/
        }
    }
}
