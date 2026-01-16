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
    public class EfBeamDal : GenericRepository<StructuralBeam>, IBeamDal
    {       

        public List<StructuralBeam> GetBeamsWithMaterial()
        {
            using var context = new StructuralContext();
            return context.Set<StructuralBeam>()
                .Include(x => x.Material)
                .ToList();

        }
    }
}
