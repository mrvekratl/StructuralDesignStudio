using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.DataAccessLayer.Abstract
{
    public interface IColumnDal : IGenericDal<StructuralColumn>
    {
        List<StructuralColumn> GetColumnsWithMaterial();
    }
}
