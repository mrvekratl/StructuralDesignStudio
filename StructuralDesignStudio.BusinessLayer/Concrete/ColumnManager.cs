using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.Abstract;
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
    public class ColumnManager : IColumnService
    {
        private readonly IColumnDal _columnDal;

        public ColumnManager(IColumnDal columnDal)
        {
            _columnDal = columnDal;
        }

        public void TAdd(StructuralColumn entity)
        {
            _columnDal.Insert(entity);
        }

        public void TDelete(StructuralColumn entity)
        {
            _columnDal.Delete(entity);
        }

        public StructuralColumn TGetByID(int id)
        {
            return _columnDal.GetByID(id);
        }

        public int TGetColumnCountByFloor(int floorLevel)
        {
            return TGetColumnsByFloor(floorLevel).Count;
        }

        public List<StructuralColumn> TGetColumnsByFloor(int floorLevel)
        {
            return _columnDal.GetListByFilter(c => c.FloorLevel == floorLevel);
        }

        public List<StructuralColumn> TGetColumnsWithMaterial()
        {
            return _columnDal.GetColumnsWithMaterial();
        }

        public ColumnStatisticsDto TGetFloorStatistics(int floorLevel)
        {
            var columns = TGetColumnsByFloor(floorLevel);
            if (!columns.Any())
            {
                return new ColumnStatisticsDto
                {
                    FloorLevel = floorLevel,
                    TotalCount = 0,
                    AverageHeight = 0,
                    TotalVolume = 0,
                    TotalWeight = 0
                };
            }
            return new ColumnStatisticsDto
            {
                FloorLevel = floorLevel,
                TotalCount = columns.Count,
                AverageHeight = columns.Average(x => x.Height),
                TotalVolume = columns.Sum(x => x.CalculateVolume()),
                TotalWeight = columns.Sum(x => x.CalculateWeight())

            };
        }

        public List<StructuralColumn> TGetList()
        {
            return _columnDal.GetList();
        }

        public List<StructuralColumn> TGetListByFilter(Expression<Func<StructuralColumn, bool>> filter)
        {
            return _columnDal.GetListByFilter(filter);
        }

        public double TGetTotalVolumeByFloor(int floorLevel)
        {
            return TGetColumnsByFloor(floorLevel).Sum(c => c.CalculateVolume());
        }

        public void TUpdate(StructuralColumn entity)
        {
            _columnDal.Update(entity);
        }
        private string GenerateColumnName()
        {
            int count = _columnDal.GetList().Count;
            return $"C{count + 1}";
        }
        public StructuralColumn CreateDefaultColumn()
        {
            var column = new StructuralColumn
            {
                Name = GenerateColumnName(),
                Width = DefaultColumnValuesDto.Width,
                Depth = DefaultColumnValuesDto.Depth,
                Height = DefaultColumnValuesDto.Height,
                MaterialID = 1 // default concrete
            };

            _columnDal.Insert(column);
            return column;
        }


    }
}
