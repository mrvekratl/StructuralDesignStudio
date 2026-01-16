using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.DataAccessLayer.Abstract
{
    public interface IMaterialDal : IGenericDal<StructuralMaterial>
    {
        List<StructuralMaterial> GetMaterialsWithElements();
        StructuralMaterial GetMaterialByName(string name);
    }
}
