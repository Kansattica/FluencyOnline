using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace FluencyOnline.Models
{
    public class ExampleImporter : ComponentBase
    {
        protected static List<(string Filename, string Code)> examples;
        public ExampleImporter()
        {
            var assembly = typeof(ExampleImporter).GetTypeInfo().Assembly;
            examples = assembly.GetManifestResourceNames()
            .Select(x => (x.Substring(14),ReadEmbeddedFile(assembly.GetManifestResourceStream(x)))) //remove the 14 character long FluencyOnline. at the start of each key
            .ToList();
            examples.Insert(0, ("Pick an example", " "));
        }

        private string ReadEmbeddedFile(Stream stream)
        {
            using(stream)
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

    }
}