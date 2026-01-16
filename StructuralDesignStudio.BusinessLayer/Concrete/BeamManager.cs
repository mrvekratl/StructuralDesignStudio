using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.EntityFramework;
using StructuralDesignStudio.DtoLayer.BeamDTOs;
using StructuralDesignStudio.DtoLayer.ColumnDTOs;
using StructuralDesignStudio.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.BusinessLayer.Concrete
{
    public class BeamManager : IBeamService
    {
        private readonly IBeamDal _beamDal;

        public BeamManager(IBeamDal beamDal)
        {
            _beamDal = beamDal;
        }

        public void TAdd(StructuralBeam entity)
        {
            _beamDal.Insert(entity);
        }

        public void TDelete(StructuralBeam entity)
        {
            _beamDal.Delete(entity);
        }

        public List<StructuralBeam> TGetBeamsByFloor(int floorLevel)
        {
           return _beamDal.GetListByFilter(b => b.FloorLevel == floorLevel);
        }

        public List<StructuralBeam> TGetBeamsWithMaterial()
        {
            return _beamDal.GetBeamsWithMaterial();
        }

        public StructuralBeam TGetByID(int id)
        {
            return _beamDal.GetByID(id);
        }

        public BeamStatisticsDto TGetFloorStatistics(int floorLevel)
        {
            var beams = TGetBeamsByFloor(floorLevel);
            if (!beams.Any())
            {
                return new BeamStatisticsDto
                {
                    FloorLevel = floorLevel,
                    TotalCount = 0,
                    TotalLength = 0,
                    TotalVolume = 0,
                    TotalWeight = 0
                };
            }

            return new BeamStatisticsDto
            {
                FloorLevel = floorLevel,
                TotalCount = beams.Count,
                TotalLength = beams.Sum(x => x.Length),
                TotalVolume = beams.Sum(x => x.CalculateVolume()),
                TotalWeight = beams.Sum(x => x.CalculateWeight())
            };
        }

        public List<StructuralBeam> TGetList()
        {
            return _beamDal.GetList();
        }

        public List<StructuralBeam> TGetListByFilter(Expression<Func<StructuralBeam, bool>> filter)
        {
            return _beamDal.GetListByFilter(filter);
        }

        public double TGetTotalLengthByFloor(int floorLevel)
        {
            return TGetBeamsByFloor(floorLevel).Sum(x => x.Length);
        }

        public void TUpdate(StructuralBeam entity)
        {
            _beamDal.Update(entity);
        }
        private string GenerateBeamName()
        {
            int count = _beamDal.GetList().Count;
            return $"B{count + 1}";
        }
        public StructuralBeam CreateDefaultBeam()
        {
            var beam = new StructuralBeam
            {
                Name = GenerateBeamName(),
                Width = DefaultBeamValuesDto.Width,
                Length = DefaultBeamValuesDto.Length,
                Height = DefaultBeamValuesDto.Height,
                MaterialID = 1 // default concrete
            };

            _beamDal.Insert(beam);
            return beam;
        }

    }
}
