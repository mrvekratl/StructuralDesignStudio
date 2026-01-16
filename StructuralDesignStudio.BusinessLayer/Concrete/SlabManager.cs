using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.EntityFramework;
using StructuralDesignStudio.DtoLayer.BeamDTOs;
using StructuralDesignStudio.DtoLayer.ColumnDTOs;
using StructuralDesignStudio.DtoLayer.SlabDTOs;
using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.BusinessLayer.Concrete
{
    public class SlabManager : ISlabService
    {
        private readonly ISlabDal _slabDal;

        public SlabManager(ISlabDal slabDal)
        {
            _slabDal = slabDal;
        }

        public void TAdd(StructuralSlab entity)
        {
            _slabDal.Insert(entity);
        }

        public void TDelete(StructuralSlab entity)
        {
            _slabDal.Delete(entity);
        }

        public StructuralSlab TGetByID(int id)
        {
            return _slabDal.GetByID(id);
        }

        public SlabStatisticsDto TGetFloorStatistics(int floorLevel)
        {
            var slabs = TGetSlabsByFloor(floorLevel);
            if (!slabs.Any())
            {
                return new SlabStatisticsDto
                {
                    FloorLevel = floorLevel,
                    TotalCount = 0,
                    TotalArea = 0,
                    TotalVolume = 0,
                    TotalWeight = 0
                };
            }
            return new SlabStatisticsDto
            {
                FloorLevel = floorLevel,
                TotalCount = slabs.Count,
                TotalArea = slabs.Sum(x => x.Length * x.Width),
                TotalVolume = slabs.Sum(x => x.CalculateVolume()),
                TotalWeight = slabs.Sum(x => x.CalculateWeight())
            };
        }

        public List<StructuralSlab> TGetList()
        {
            return _slabDal.GetList();
        }

        public List<StructuralSlab> TGetListByFilter(Expression<Func<StructuralSlab, bool>> filter)
        {
            return _slabDal.GetListByFilter(filter);
        }

        public List<StructuralSlab> TGetSlabsByFloor(int floorLevel)
        {
            return _slabDal.GetListByFilter(s => s.FloorLevel == floorLevel);
        }

        public List<StructuralSlab> TGetSlabsWithMaterial()
        {
            return _slabDal.GetSlabsWithMaterial();
        }

        public double TGetTotalAreaByFloor(int floorLevel)
        {
            return _slabDal.GetListByFilter(s => s.FloorLevel == floorLevel).Sum(s => s.Length * s.Width);
        }

        public void TUpdate(StructuralSlab entity)
        {
            _slabDal.Update(entity);
        }
        private string GenerateSlabName()
        {
            int count = _slabDal.GetList().Count;
            return $"S{count + 1}";
        }
        public StructuralSlab CreateDefaultSlab()
        {
            var slab = new StructuralSlab
            {
                Name = GenerateSlabName(),
                Width = DefaultSlabValuesDto.Width,
                Length = DefaultSlabValuesDto.Length,
                Thickness = DefaultSlabValuesDto.Thickness,
                MaterialID = 1 // default concrete
            };

            _slabDal.Insert(slab);
            return slab;
        }
    }
}
