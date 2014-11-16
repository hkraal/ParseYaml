using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;

namespace yamldotnet
{
    class YamlBlueprint
    {
        public YamlBlueprintActivities activities { get; set; }
        public int blueprintTypeID { get; set; }
        public int maxProductionLimit { get; set; }
    }
}
