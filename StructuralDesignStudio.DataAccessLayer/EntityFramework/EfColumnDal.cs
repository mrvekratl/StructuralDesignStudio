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
    public class EfColumnDal : GenericRepository<StructuralColumn>, IColumnDal
    {
                public List<StructuralColumn> GetColumnsWithMaterial()
        {
            using var context = new StructuralContext();
            return context.Set<StructuralColumn>()
                .Include(c => c.Material)
                .ToList();
        }
    }
}
