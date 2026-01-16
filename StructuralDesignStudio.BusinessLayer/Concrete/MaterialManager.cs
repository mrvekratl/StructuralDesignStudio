using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.EntityFramework;
using StructuralDesignStudio.DtoLayer.ColumnDTOs;
using StructuralDesignStudio.DtoLayer.MaterialDTOs;
using StructuralDesignStudio.DtoLayer.SlabDTOs;
using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace StructuralDesignStudio.BusinessLayer.Concrete
{
    public class MaterialManager : IMaterialService
    {
        private readonly IMaterialDal _materialDal;

        public MaterialManager(IMaterialDal materialDal)
        {
            _materialDal = materialDal;
        }

        public void TAdd(StructuralMaterial entity)
        {
            _materialDal.Insert(entity);
        }

        public void TDelete(StructuralMaterial entity)
        {
            _materialDal.Delete(entity);
        }

        public StructuralMaterial TGetByID(int id)
        {
            return _materialDal.GetByID(id);
        }

        public List<StructuralMaterial> TGetList()
        {
            return _materialDal.GetList();
        }

        public List<StructuralMaterial> TGetListByFilter(Expression<Func<StructuralMaterial, bool>> filter)
        {
            return _materialDal.GetListByFilter(filter);
        }

        public StructuralMaterial TGetMaterialByName(string name)
        {
            return _materialDal.GetMaterialByName(name);
        }

        public List<StructuralMaterial> TGetMaterialsWithElements()
        {
            return _materialDal.GetMaterialsWithElements();
        }

        public MaterialUsageDto TGetMaterialUsageStatistics(int materialId)
        {
            var material = _materialDal.GetMaterialsWithElements()
                .FirstOrDefault(m => m.MaterialId == materialId);
            if (material == null){
                return null;
            }
            var elements = material.StructuralElements;
            if (!elements.Any())
            {
                return new MaterialUsageDto
                {
                    MaterialId = 0,
                    MaterialName = "",
                    UsageCount = 0,
                    TotalVolume = 0,
                    TotalWeight = 0
                };
            }
            return new MaterialUsageDto
            {
                MaterialId = material.MaterialId,
                MaterialName = material.MaterialName,
                UsageCount = elements.Count,
                TotalVolume = elements.Sum(e => e.CalculateVolume()),
                TotalWeight = elements.Sum(e => e.CalculateWeight())
            };
        }

        public void TUpdate(StructuralMaterial entity)
        {
            _materialDal.Update(entity);
        }
        private string GenerateMaterialName()
        {
            int count = _materialDal.GetList().Count;
            return $"M{count + 1}";
        }
        public StructuralMaterial CreateDefaultMaterial()
        {
            var material = new StructuralMaterial
            {
                MaterialName = GenerateMaterialName(),
                Density = DefaultMaterialValuesDto.Density,
                CompressiveStrength = DefaultMaterialValuesDto.CompressiveStrength,
                MaterialType = DefaultMaterialValuesDto.MaterialType
            };

            _materialDal.Insert(material);
            return material;
        }
    }
}
