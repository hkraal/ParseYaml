using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace yamldotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Deserializing an object graph example given on the following URL:
            //  http://www.aaubry.net/page/YamlDotNet-Documentation-Loading-a-YAML-stream


            var deserializer = new Deserializer();

            string text = System.IO.File.ReadAllText(@"blueprints.yaml");
            var input = new StringReader(text);

            var blueprintsById = deserializer.Deserialize<Dictionary<int, YamlBlueprint>>(input);

            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private const string Document = @"---
  activities:
    copying:
      time: 4800
    invention:
      materials:
      - quantity: 2
        typeID: 20411
      - quantity: 2
        typeID: 25887
      products:
      - probability: 0.3
        quantity: 1
        typeID: 11177
      - probability: 0.3
        quantity: 1
        typeID: 11179
      skills:
      - level: 1
        typeID: 11433
      - level: 1
        typeID: 21790
      - level: 1
        typeID: 11454
      time: 63900
    manufacturing:
      materials:
      - quantity: 20000
        typeID: 34
      - quantity: 4444
        typeID: 35
      - quantity: 2111
        typeID: 36
      - quantity: 556
        typeID: 37
      - quantity: 11
        typeID: 38
      - quantity: 1
        typeID: 39
      products:
      - quantity: 1
        typeID: 583
      skills:
      - level: 1
        typeID: 3380
      time: 6000
    research_material:
      time: 2100
    research_time:
      time: 2100
  blueprintTypeID: 684
  maxProductionLimit: 30
";
    }
}
