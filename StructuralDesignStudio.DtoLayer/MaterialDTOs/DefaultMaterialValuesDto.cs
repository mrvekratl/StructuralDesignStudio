using StructuralDesignStudio.EntityLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.DtoLayer.MaterialDTOs
{
    public class DefaultMaterialValuesDto
    {
        public const double Density = 2500;
        public const double CompressiveStrength = 25;
        public static readonly MaterialType MaterialType = MaterialType.Concrete;
    }
}
