using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.DtoLayer.SlabDTOs
{
    public class SlabStatisticsDto
    {
        public int TotalCount { get; set; }
        public double TotalArea { get; set; }
        public double TotalVolume { get; set; }
        public double TotalWeight { get; set; }
        public int FloorLevel { get; set; }
    }
}
