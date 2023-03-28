using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PathShape = Avalonia.Controls.Shapes.Path;

namespace Paint.Models
{
    public class JSONLoaderNew
    {
            public List<ExtraForJSON> Load(string path)
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    List<ExtraForJSON>? shapes = JsonSerializer.Deserialize<List<ExtraForJSON>>(fs);

                    if (shapes == null)
                    {
                        shapes = new List<ExtraForJSON>();
                    }

                    return shapes;
                }
            }
    }
}
