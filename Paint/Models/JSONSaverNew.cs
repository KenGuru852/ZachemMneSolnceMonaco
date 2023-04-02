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
    public class JSONSaverNew
    {
        public void Save(IEnumerable<ExtraForJSON> shapes, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, shapes,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
            }
        }
    }
}
