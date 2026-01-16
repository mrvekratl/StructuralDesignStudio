using StructuralDesignStudio.DtoLayer.ColumnDTOs;
using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.BusinessLayer.Abstract
{
    public interface IColumnService : IGenericService<StructuralColumn>
    {
        // Private methods
        List<StructuralColumn> TGetColumnsWithMaterial();
        List<StructuralColumn> TGetColumnsByFloor(int floorLevel);
        double TGetTotalVolumeByFloor(int floorLevel);
        int TGetColumnCountByFloor(int floorLevel);
        StructuralColumn CreateDefaultColumn();

        // Statistical method
        ColumnStatisticsDto TGetFloorStatistics(int floorLevel);
    }
}
