using StructuralDesignStudio.DtoLayer.SlabDTOs;
using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.BusinessLayer.Abstract
{
    public interface ISlabService : IGenericService<StructuralSlab>
    {
        List<StructuralSlab> TGetSlabsWithMaterial();
        List<StructuralSlab> TGetSlabsByFloor(int floorLevel);
        double TGetTotalAreaByFloor(int floorLevel);
        StructuralSlab CreateDefaultSlab();

        SlabStatisticsDto TGetFloorStatistics(int floorLevel);
    }
}
