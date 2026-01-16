using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.DtoLayer.BeamDTOs;
using StructuralDesignStudio.DtoLayer.ColumnDTOs;
using StructuralDesignStudio.DtoLayer.SlabDTOs;

namespace StructuralDesignStudio.UI.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IColumnService _columnService;
        private readonly IBeamService _beamService;
        private readonly ISlabService _slabService;

        [ObservableProperty] private int selectedFloor;

        [ObservableProperty] private ColumnStatisticsDto columnStats;
        [ObservableProperty] private BeamStatisticsDto beamStats;
        [ObservableProperty] private SlabStatisticsDto slabStats;

        [ObservableProperty] private PlotModel structuralDistributionChart;

        public DashboardViewModel(
            IColumnService columnService,
            IBeamService beamService,
            ISlabService slabService)
        {
            _columnService = columnService;
            _beamService = beamService;
            _slabService = slabService;

            SelectedFloor = 0;
            LoadStatistics();
        }

        [RelayCommand]
        private void LoadStatistics()
        {
            ColumnStats = _columnService.TGetFloorStatistics(SelectedFloor);
            BeamStats = _beamService.TGetFloorStatistics(SelectedFloor);
            SlabStats = _slabService.TGetFloorStatistics(SelectedFloor);

            LoadStructuralDistributionChart();
        }

        private void LoadStructuralDistributionChart()
        {
            var model = new PlotModel
            {
                Title = $"Structural Volume Distribution – Floor {SelectedFloor}",
                TitleColor = OxyColor.FromRgb(241, 241, 241),
                TitleFontSize = 16,
                TitleFontWeight = FontWeights.Normal,
                PlotAreaBorderColor = OxyColor.FromRgb(63, 63, 70),
                Background = OxyColor.FromRgb(37, 37, 38),
                TextColor = OxyColor.FromRgb(204, 204, 204),
                PlotAreaBorderThickness = new OxyThickness(0)
            };

            var pie = new PieSeries
            {
                InsideLabelFormat = "",
                OutsideLabelFormat = "{1}: {2:F1} m³",
                StrokeThickness = 2,
                Stroke = OxyColor.FromRgb(37, 37, 38),
                AngleSpan = 360,
                StartAngle = 0,
                FontSize = 12,
                TextColor = OxyColor.FromRgb(204, 204, 204)
            };

            var columnVolume = ColumnStats?.TotalVolume ?? 0;
            var beamVolume = BeamStats?.TotalVolume ?? 0;
            var slabVolume = SlabStats?.TotalVolume ?? 0;
            var total = columnVolume + beamVolume + slabVolume;

            if (total <= 0)
            {
                StructuralDistributionChart = model;
                return;
            }

            // REVIT STYLE COLORS
            pie.Slices.Add(new PieSlice("Columns", columnVolume)
            {
                Fill = OxyColor.FromRgb(41, 128, 185), // Koyu mavi
                IsExploded = false
            });

            pie.Slices.Add(new PieSlice("Beams", beamVolume)
            {
                Fill = OxyColor.FromRgb(52, 152, 219), // Orta mavi
                IsExploded = false
            });

            pie.Slices.Add(new PieSlice("Slabs", slabVolume)
            {
                Fill = OxyColor.FromRgb(93, 173, 226), // Açık mavi
                IsExploded = false
            });

            model.Series.Add(pie);

            // Legend
            model.Legends.Add(new Legend
            {
                LegendTitle = "Elements",
                LegendPosition = LegendPosition.RightMiddle,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorder = OxyColor.FromRgb(63, 63, 70),
                LegendBorderThickness = 1,
                LegendBackground = OxyColor.FromRgb(45, 45, 48),
                LegendTextColor = OxyColor.FromRgb(204, 204, 204),
                LegendPadding = 10,
                LegendMargin = 10,
                LegendFontSize = 11
            });

            StructuralDistributionChart = model;
        }
    }
}
