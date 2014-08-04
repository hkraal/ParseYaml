using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using ConsoleApplication1.Models;

namespace yamldotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            // We're going to read a Yaml file based on the example given on the following URL:
            //  http://www.aaubry.net/page/YamlDotNet-Documentation-Loading-a-YAML-stream

            var Blueprints = ParseYamlBlueprints();

            var bpo = (from item in Blueprints
                       where item.Id == 26365
                       select item);


            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static List<YamlBlueprint> ParseYamlBlueprints()
        {
            List<YamlBlueprint> Blueprints = new List<YamlBlueprint>();

            // Read our file and load the Yaml
            string text = System.IO.File.ReadAllText(@"blueprints.yaml");
            var input = new StringReader(text);
            var yaml = new YamlStream();
            yaml.Load(input);

            // Create the mapping 
            YamlMappingNode yamlRoot = (YamlMappingNode)yaml.Documents[0].RootNode;

            // Loop trough each blueprint and create objects
            foreach (var blueprintScalarNode in yamlRoot.Children)
            {
                var blueprintKey = ((YamlScalarNode)blueprintScalarNode.Key).Value;
                YamlScalarNode blueprintNode = new YamlScalarNode(Convert.ToString(blueprintKey));
                var blueprint = yamlRoot.Children[blueprintNode];

                var objBlueprint = ParseYamlBlueprint(Convert.ToInt32(blueprintKey), blueprint);
                Blueprints.Add(objBlueprint);
            }

            return Blueprints;
        }

        private static YamlBlueprint ParseYamlBlueprint(int blueprintId, YamlNode blueprint)
        {
            YamlBlueprint objBlueprint = new YamlBlueprint();
            objBlueprint.Id = blueprintId;
                
            //Console.WriteLine("{0} = {1}", objBlueprint.Id, blueprint);

            YamlMappingNode yamlBlueprintRoot = (YamlMappingNode)blueprint;
            foreach (var entry in yamlBlueprintRoot.Children)
            {
                var myKey = ((YamlScalarNode)entry.Key).Value;
                YamlScalarNode myYamlScalarNode = new YamlScalarNode(myKey);
                var myValue = yamlBlueprintRoot.Children[myYamlScalarNode];

                switch (myKey)
                {
                    case "blueprintTypeID":
                        objBlueprint.blueprintTypeID = Convert.ToInt32(myValue.ToString());
                        break;
                    case "maxProductionLimit":
                        objBlueprint.maxProductionLimit = Convert.ToInt32(myValue.ToString());
                        break;
                    case "activities":
                        List<YamlBlueprintActivity> Activities = ParseBlueprintActivities((YamlMappingNode)myValue);
                        objBlueprint.Activities = Activities;
                        break;
                        
                        
                }
                //Console.WriteLine("\t{0} = {1}", myKey, myValue);
            }
            //Console.WriteLine("objBlueprint = {0}", objBlueprint);
            return objBlueprint;
        }

        private static List<YamlBlueprintActivity> ParseBlueprintActivities(YamlMappingNode mapping)
        {
            List<YamlBlueprintActivity> activities = new List<YamlBlueprintActivity>();

            foreach (var entry in mapping.Children)
            {
                var myKey = ((YamlScalarNode)entry.Key).Value;
                YamlScalarNode activityYamlScalarNode = new YamlScalarNode(myKey);
                var myValue = mapping.Children[activityYamlScalarNode];

                //Console.WriteLine("\t\t{0} = {1}", myKey, myValue);

                YamlBlueprintActivity activity = ParseBlueprintActivity(Convert.ToInt32(myKey), myValue);
                activities.Add(activity);
            }
            return activities;
        }

        private static YamlBlueprintActivity ParseBlueprintActivity(int activityId, YamlNode activity)
        {
            YamlBlueprintActivity objActivity = new YamlBlueprintActivity();
            objActivity.Id = activityId;
           
            YamlMappingNode yamlBlueprintActivity = (YamlMappingNode)activity;
            foreach (var entry in yamlBlueprintActivity.Children)
            {
                var myKey = ((YamlScalarNode)entry.Key).Value;
                YamlScalarNode myYamlScalarNode = new YamlScalarNode(myKey);
                var myValue = yamlBlueprintActivity.Children[myYamlScalarNode];

                //Console.WriteLine("{0} = {1}", myKey, myValue);

                switch(myKey)
                {
                    case "materials":
                        var materials = ParseBlueprintActivityMaterials(myValue);
                        objActivity.Materials = materials;
                        break;
                    case "products":
                        var products = ParseBlueprintActivityMaterials(myValue);
                        objActivity.Products = products;
                        break;
                    case "time":
                        objActivity.Time = Convert.ToInt32(myValue.ToString());
                        break;
                }
            }
            return objActivity;
        }

        private static List<YamlBlueprintMaterials> ParseBlueprintActivityMaterials(YamlNode mapping)
        {
            List<YamlBlueprintMaterials> Materials = new List<YamlBlueprintMaterials>();

            YamlMappingNode yamlBlueprintActivityMaterial = (YamlMappingNode)mapping;
            foreach (var entry in yamlBlueprintActivityMaterial.Children)
            {
                var myKey = ((YamlScalarNode)entry.Key).Value;
                YamlScalarNode myYamlScalarNode = new YamlScalarNode(myKey);
                var myValue = yamlBlueprintActivityMaterial.Children[myYamlScalarNode];

                //Console.WriteLine("{0} = {1}", myKey, myValue);

                var quantity = ParseBlueprintActivityMaterialsQuantity(myValue);

                Materials.Add(new YamlBlueprintMaterials()
                {
                    Id = Convert.ToInt32(myKey.ToString()),
                    Quantity = quantity
                });

            }
            return Materials;
        }

        private static int ParseBlueprintActivityMaterialsQuantity(YamlNode mapping)
        {
            YamlMappingNode yamlBlueprintActivityMaterialQuantity = (YamlMappingNode)mapping;
            YamlScalarNode myYamlScalarNode = new YamlScalarNode("quantity");
            var quantity = yamlBlueprintActivityMaterialQuantity.Children[myYamlScalarNode];
            return Convert.ToInt32(quantity.ToString());
        }
    }
}
