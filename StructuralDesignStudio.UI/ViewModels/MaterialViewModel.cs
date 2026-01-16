using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.EntityLayer.Concrete;
using StructuralDesignStudio.EntityLayer.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.UI.ViewModels
{
    public partial class MaterialViewModel : ObservableObject
    {
        private readonly IMaterialService _materialService;

        public ObservableCollection<StructuralMaterial> Materials { get; } = new();

        [ObservableProperty]
        private StructuralMaterial? selectedMaterial;

        [ObservableProperty] private string materialName;
        [ObservableProperty] private double density;
        [ObservableProperty] private double compressiveStrength;
        [ObservableProperty] private MaterialType materialType;
        [ObservableProperty] private PlotModel materialUsageChart;

        public Array MaterialTypes => Enum.GetValues(typeof(MaterialType));

        public MaterialViewModel(IMaterialService materialService)
        {
            _materialService = materialService;
            LoadMaterials();
            LoadMaterialChart();
        }

        private void LoadMaterials()
        {
            Materials.Clear();
            foreach (var material in _materialService.TGetList())
                Materials.Add(material);
        }

        partial void OnSelectedMaterialChanged(StructuralMaterial? value)
        {
            if (value == null) return;

            MaterialName = value.MaterialName;
            Density = value.Density;
            CompressiveStrength = value.CompressiveStrength;
            MaterialType = value.MaterialType;
        }

        [RelayCommand]
        private void AddMaterial()
        {
            var material = _materialService.CreateDefaultMaterial();
            Materials.Add(material);
            LoadMaterialChart();

        }

        [RelayCommand]
        private void UpdateMaterial()
        {
            if (SelectedMaterial == null) return;

            SelectedMaterial.MaterialName = MaterialName;
            SelectedMaterial.Density = Density;
            SelectedMaterial.CompressiveStrength = CompressiveStrength;
            SelectedMaterial.MaterialType = MaterialType;

            _materialService.TUpdate(SelectedMaterial);
            LoadMaterials();
        }

        [RelayCommand]
        private void DeleteMaterial()
        {
            if (SelectedMaterial == null) return;

            _materialService.TDelete(SelectedMaterial);
            LoadMaterials();
            LoadMaterialChart();

        }
        private void LoadMaterialChart()
        {
            var model = new PlotModel
            {
                Title = "Material Usage Distribution",
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

            var materials = _materialService.TGetMaterialsWithElements();
            var totalVolume = materials
                .SelectMany(m => m.StructuralElements)
                .Sum(e => e.CalculateVolume());

            if (totalVolume == 0)
            {
                MaterialUsageChart = model;
                return;
            }

            // Dashboard ile uyumlu mavi tonları
            var colors = new[]
            {
        OxyColor.FromRgb(41, 128, 185),   // Koyu mavi
        OxyColor.FromRgb(52, 152, 219),   // Orta mavi
        OxyColor.FromRgb(93, 173, 226),   // Açık mavi
        OxyColor.FromRgb(0, 122, 204),    // Revit mavi
        OxyColor.FromRgb(108, 122, 137),  // Gri-mavi
        OxyColor.FromRgb(155, 89, 182),   // Mor aksan
        OxyColor.FromRgb(52, 73, 94)      // Koyu gri-mavi
    };

            int colorIndex = 0;
            foreach (var material in materials)
            {
                var volume = material.StructuralElements.Sum(e => e.CalculateVolume());
                if (volume <= 0) continue;

                pie.Slices.Add(new PieSlice(material.MaterialName, volume)
                {
                    Fill = colors[colorIndex % colors.Length],
                    IsExploded = false
                });

                colorIndex++;
            }

            model.Series.Add(pie);

            // Legend
            model.Legends.Add(new Legend
            {
                LegendTitle = "Materials",
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

            MaterialUsageChart = model;
        }

    }

}
