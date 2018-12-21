using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFormatterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var widget = new Widget { Name = "Demo", Categories = new List<string> { "Red", "Green", "Blue" } };
            var formatter = new BinaryFormatter();
            byte[] buffer;
            WidgetPrinter.Print(widget);
            Console.WriteLine("Serializing...");
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, widget);
                buffer = new byte[stream.Length];
                stream.Flush();
                stream.Position = 0;
                stream.Read(buffer, 0, buffer.Length);
            }
            Console.WriteLine($"We have {buffer.Length} bytes!");

            Console.WriteLine("Deserializing");
            using (var stream = new MemoryStream(buffer))
            {
                var newWidget = (Widget)formatter.Deserialize(stream);
                WidgetPrinter.Print(newWidget);
            }
            Console.ReadKey();
        }
    }
    [Serializable]
    public class Widget
    {
        public Widget()
        {
            Categories = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Categories { get; set; }
    }

    public class WidgetPrinter
    {
        public static void Print(Widget widget)
        {
            Console.WriteLine($"Name: {widget.Name}");
            foreach (var cat in widget.Categories)
                Console.WriteLine($"\t{cat}");
        }

    }
}
}
