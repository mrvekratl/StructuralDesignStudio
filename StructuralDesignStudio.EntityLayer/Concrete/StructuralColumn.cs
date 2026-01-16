using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.EntityLayer.Concrete
{
    public class StructuralColumn : StructuralElement
    {
        public double Width { get; set; } //cm
        public double Depth { get; set; } //cm
        public double Height { get; set; } //cm
        public override double CalculateVolume()
        {
            return (Width * Depth * Height) / 1000000; //m3
        }
        public override double CalculateWeight()
        {
            return CalculateVolume() * (Material?.Density ?? 0);
        }
        public override string GetElementType()
        {
            return "Column";
        }
        public double GetCrossSectionArea()
        {
            return (Width * Depth) / 10000; //m2
        }
        public double Volume => CalculateVolume();
        public double Weight => CalculateWeight();
        public double CrossSectionArea => GetCrossSectionArea();


    }
}
