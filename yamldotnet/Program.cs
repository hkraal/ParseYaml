using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace yamldotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            // We're going to read a Yaml file based on the example given on the following URL:
            //  http://www.aaubry.net/page/YamlDotNet-Documentation-Loading-a-YAML-stream

            // Read our file and load the Yaml
            string text = System.IO.File.ReadAllText(@"blueprints.yaml");
            var input = new StringReader(text);
            var yaml = new YamlStream();
            yaml.Load(input);

            // Create the mapping 
            YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            // Loop trough all child entries and print our keys
            string key = string.Empty;
            foreach (var entry in mapping.Children)
            {
                // { { activities, { { 1, { { materials, { { 38, { { quantity, 86 } } } } }, { products, { { 165, { { quantity, 1 } } } } }, { time, 600 } } }, { 3, { { time, 210 } } }, { 4, { { time, 210 } } }, { 5, { { time, 480 } } } } }, { blueprintTypeID, 681 }, { maxProductionLimit, 300 } }
                var entryValue = entry.Value;

                // "681"
                var myKey = ((YamlScalarNode)entry.Key).Value;
                Console.WriteLine(myKey);

                // { { activities, { { 1, { { materials, { { 38, { { quantity, 86 } } } } }, { products, { { 165, { { quantity, 1 } } } } }, { time, 600 } } }, { 3, { { time, 210 } } }, { 4, { { time, 210 } } }, { 5, { { time, 480 } } } } }, { blueprintTypeID, 681 }, { maxProductionLimit, 300 } }
                YamlMappingNode blueprint = (YamlMappingNode)entry.Value;

                // "681"
                YamlScalarNode myYamlScalarNode = new YamlScalarNode(myKey);

                // { { activities, { { 1, { { materials, { { 38, { { quantity, 86 } } } } }, { products, { { 165, { { quantity, 1 } } } } }, { time, 600 } } }, { 3, { { time, 210 } } }, { 4, { { time, 210 } } }, { 5, { { time, 480 } } } } }, { blueprintTypeID, 681 }, { maxProductionLimit, 300 } }
                var tmpItem = mapping.Children[myYamlScalarNode];


                // The next line will throw:
                // An unhandled exception of type 'System.InvalidCastException' occurred in yamldotnet.exe
                // Additional information: Unable to cast object of type 'YamlDotNet.RepresentationModel.YamlMappingNode' to type 'YamlDotNet.RepresentationModel.YamlSequenceNode'.
                var items = (YamlSequenceNode)tmpItem;
                foreach (YamlMappingNode item in items)
                {
                    Console.WriteLine(
                        "{0}\t{1}",
                        item.Children[new YamlScalarNode("blueprintTypeID")],
                        item.Children[new YamlScalarNode("maxProductionLimit")]
                    );
                }
                Console.WriteLine("Press any key to exit.");
                System.Console.ReadKey();
            }
        }
    }
}
