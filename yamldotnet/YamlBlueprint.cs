using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Models
{
    class YamlBlueprint
    {
        private int _Id;

        public int Id;
        public List<YamlBlueprintActivity> Activities;
        public int blueprintTypeID;
        public int maxProductionLimit;
    }

    class YamlBlueprintActivity
    {
        public int Id;
        public List<YamlBlueprintMaterials> Materials;
        public List<YamlBlueprintMaterials> Products;
        public int Time;
    }

    class YamlBlueprintMaterials
    {
        public int Id;
        public int Quantity;
    }
}
