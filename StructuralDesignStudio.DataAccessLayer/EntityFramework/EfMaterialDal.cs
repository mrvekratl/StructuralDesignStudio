using Microsoft.EntityFrameworkCore;
using StructuralDesignStudio.DataAccessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.Concrete.Context;
using StructuralDesignStudio.DataAccessLayer.Repository;
using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.DataAccessLayer.EntityFramework
{
    public class EfMaterialDal : GenericRepository<StructuralMaterial>, IMaterialDal
    {
        public StructuralMaterial GetMaterialByName(string name)
        {
            using var context = new StructuralContext();
            return context.StructuralMaterials
                .FirstOrDefault(x => x.MaterialName == name);
        }

        public List<StructuralMaterial> GetMaterialsWithElements()
        {
            using var context = new StructuralContext();
            return context.StructuralMaterials
            .Include(x => x.StructuralElements)
            .AsNoTracking()
            .ToList();
        }
    }
}
