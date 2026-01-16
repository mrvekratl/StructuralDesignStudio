using StructuralDesignStudio.DtoLayer.BeamDTOs;
using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.BusinessLayer.Abstract
{
    public interface IBeamService : IGenericService<StructuralBeam>
    {
        List<StructuralBeam> TGetBeamsWithMaterial();
        List<StructuralBeam> TGetBeamsByFloor(int floorLevel);
        double TGetTotalLengthByFloor(int floorLevel);
        BeamStatisticsDto TGetFloorStatistics(int floorLevel);
        StructuralBeam CreateDefaultBeam();

    }   
    
}
