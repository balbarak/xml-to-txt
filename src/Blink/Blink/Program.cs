using System;
using System.Threading.Tasks;

namespace Blink
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new BlinkClient();

            if (args.Length < 2)
            {
                Console.WriteLine("Please enter input file and output file paths");
                return;
            }

            var input = args[0];
            var output = args[1];

            client.ConvertToText(input, output);
        }
    }
}
