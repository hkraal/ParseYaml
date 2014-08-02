using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Models
{
    class YamlBlueprint
    {
        public int Id;
        public YamlBlueprintActivity[] Activities;
        public int blueprintTypeID;
        public int maxProductionLimit;
    }

    class YamlBlueprintActivity
    {
        public int Id;
        public YamlBlueprintMaterials[] Materials;
        public YamlBlueprintProduct[] Products;
        public int Time;
    }

    class YamlBlueprintMaterials
    {
        public int Id;
        public int Quantity;
    }

    class YamlBlueprintProduct
    {
        public int Id;
        public int Quantity;
    }
}
