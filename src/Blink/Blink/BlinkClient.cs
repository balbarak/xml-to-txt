using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Blink
{
    public class BlinkClient
    {

        public void ReadFile()
        {
            var input = @"C:\Users\balba\Desktop\workshop\inputFixed.xml";

            XmlReaderSettings settings = new XmlReaderSettings
            {
                Async = true,
            };


            using (var fileStream = new FileStream(input, FileMode.Open))
            {
                XmlReader reader = XmlReader.Create(fileStream,settings);

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.WriteLine("Start Element {0}", reader.Name);
                            break;
                        case XmlNodeType.Text:

                            Console.WriteLine("Text Node: {0}",reader.Value);

                            break;
                        case XmlNodeType.EndElement:
                            
                            Console.WriteLine("End Element {0}", reader.Name);

                            break;
                        default:
                            
                            Console.WriteLine("Other node {0} with value {1}",reader.NodeType, reader.Value);

                            break;
                    }
                }
            }


        }

        public void ConvertToText(string input,string output)
        {
            var settings = new XmlReaderSettings
            {
                CheckCharacters = false,
            };

            Console.WriteLine($"Processing file: {input}");

            var progress = new ConsoleProgress();

            using var inputStream = new FileStream(input, FileMode.Open,FileAccess.Read);
            using var outputStream = new FileStream(output, FileMode.Create);

            var fileSize = (double)inputStream.Length;

            XmlReader reader = XmlReader.Create(inputStream, settings);

            var writer = new StreamWriter(outputStream);

            
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    var text = reader.Value;

                    if (string.IsNullOrWhiteSpace(text))
                        continue;

                    writer.WriteLine(text);

                    var currentPosition = (double)inputStream.Position;

                    double percentage = (currentPosition / fileSize);

                    progress.Report(percentage);
                }
            }

            progress.Report(1);
        }

    }
}
