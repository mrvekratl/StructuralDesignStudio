using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.DtoLayer.BeamDTOs
{
    public class BeamStatisticsDto
    {
        public int TotalCount { get; set; }
        public double TotalLength { get; set; }
        public double TotalVolume { get; set; }
        public double TotalWeight { get; set; }
        public int FloorLevel { get; set; }
    }
}
