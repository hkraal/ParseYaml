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
            YamlMappingNode yamlRoot = (YamlMappingNode)yaml.Documents[0].RootNode;

            // Loop trough all child entries and print our keys
            string key = string.Empty;
            foreach (var entry in yamlRoot.Children)
            {
                var blueprintId = ((YamlScalarNode)entry.Key).Value;
                YamlScalarNode blueprintNode = new YamlScalarNode(blueprintId);
                var blueprint = yamlRoot.Children[blueprintNode];
                Console.WriteLine("{0} = {1}", blueprintId, blueprint);

                YamlMappingNode yamlBlueprintRoot = (YamlMappingNode)blueprint;
                foreach (var blueprintEntry in yamlBlueprintRoot.Children)
                {
                    var myKey = ((YamlScalarNode)blueprintEntry.Key).Value;
                    YamlScalarNode myYamlScalarNode = new YamlScalarNode(myKey);
                    var myValue = yamlBlueprintRoot.Children[myYamlScalarNode];

                    Console.WriteLine("\t{0} = {1}", myKey, myValue);
                }

                //Console.WriteLine("Press any key to continue.");
                //System.Console.ReadKey();
            }
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();

        }
    }
}
