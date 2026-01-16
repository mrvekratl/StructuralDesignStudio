using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.DtoLayer.MaterialDTOs
{
    public class MaterialUsageDto
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int UsageCount { get; set; }
        public double TotalVolume { get; set; }
        public double TotalWeight { get; set; }
    }
}
