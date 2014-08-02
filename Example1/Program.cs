using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Example1
{
    // Example found on http://www.aaubry.net/page/YamlDotNet-Documentation-Loading-a-YAML-stream
    class Program
    {
        static void Main(string[] args)
        {
            // Setup the input
            var input = new StringReader(Document);

            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);

            // Examine the stream
            var mapping =
                (YamlMappingNode)yaml.Documents[0].RootNode;

            // Loop trough all child entries and print our keys
            string key = string.Empty;
            foreach (var entry in mapping.Children)
            {
                var myKey = ((YamlScalarNode)entry.Key).Value;
                Console.WriteLine("Key: {0}", myKey);
                if(myKey != "items")
                {
                    continue;
                }

                YamlScalarNode myYamlScalarNode = new YamlScalarNode(myKey);
                var tmpItem = mapping.Children[myYamlScalarNode];

                // The next line will throw:
                // An unhandled exception of type 'System.InvalidCastException' occurred in yamldotnet.exe
                // Additional information: Unable to cast object of type 'YamlDotNet.RepresentationModel.YamlMappingNode' to type 'YamlDotNet.RepresentationModel.YamlSequenceNode'.
                var items = (YamlSequenceNode)tmpItem;
                foreach (YamlMappingNode item in items)
                {
                    Console.WriteLine(
                        "{0}\t{1}",
                        item.Children[new YamlScalarNode("part_no")],
                        item.Children[new YamlScalarNode("descrip")]
                    );
                }
                Console.WriteLine("Press any key to exit.");
                System.Console.ReadKey();
            }
        }

        private const string Document = @"---
            receipt:    Oz-Ware Purchase Invoice
            date:        2007-08-06
            customer:
                given:   Dorothy
                family:  Gale

            items:
                - part_no:   A4786
                  descrip:   Water Bucket (Filled)
                  price:     1.47
                  quantity:  4

                - part_no:   E1628
                  descrip:   High Heeled ""Ruby"" Slippers
                  price:     100.27
                  quantity:  1

            bill-to:  &id001
                street: |
                        123 Tornado Alley
                        Suite 16
                city:   East Westville
                state:  KS

            ship-to:  *id001

            specialDelivery:  >
                Follow the Yellow Brick
                Road to the Emerald City.
                Pay no attention to the
                man behind the curtain.
...";
    }
}
