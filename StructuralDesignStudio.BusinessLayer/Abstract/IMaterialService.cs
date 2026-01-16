using StructuralDesignStudio.DtoLayer.MaterialDTOs;
using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.BusinessLayer.Abstract
{
    public interface IMaterialService : IGenericService<StructuralMaterial>
    {
        List<StructuralMaterial> TGetMaterialsWithElements();
        StructuralMaterial TGetMaterialByName(string name);
        StructuralMaterial CreateDefaultMaterial();

        // Statistics
        MaterialUsageDto TGetMaterialUsageStatistics(int materialId);
    }
}
