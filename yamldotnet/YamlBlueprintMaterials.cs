using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;

namespace yamldotnet
{
    class YamlBlueprintMaterial
    {
        //[YamlAlias("quantity")]
        public int quantity { get; set; }
        public int typeID { get; set; }
    }
}
