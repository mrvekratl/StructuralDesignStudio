using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.EntityLayer.Concrete;
using StructuralDesignStudio.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace StructuralDesignStudio.UI.ViewModels
{
    public partial class SlabViewModel : ObservableObject
    {
        private readonly ISlabService _slabService;

        public ObservableCollection<StructuralSlab> Slabs { get; } = new();

        [ObservableProperty]
        private StructuralSlab? selectedSlab;

        [ObservableProperty] private int floorLevel;
        [ObservableProperty] private double width;
        [ObservableProperty] private double length;
        [ObservableProperty] private double thickness;
        [ObservableProperty] private Geometry? columnGeometry;
        [ObservableProperty] private Model3D? previewModel;

        partial void OnWidthChanged(double v) => Update3D();
        partial void OnLengthChanged(double v) => Update3D();
        partial void OnThicknessChanged(double v) => Update3D();



        public SlabViewModel(ISlabService slabService)
        {
            _slabService = slabService;
            LoadSlabs();
        }

        private void LoadSlabs()
        {
            Slabs.Clear();
            foreach (var slab in _slabService.TGetList())
                Slabs.Add(slab);
        }

        partial void OnSelectedSlabChanged(StructuralSlab? value)
        {
            if (value == null)
            {
                PreviewModel = null;
                return;
            }
            if (value == null) return;

            FloorLevel = value.FloorLevel;
            Width = value.Width;
            Length = value.Length;
            Thickness = value.Thickness;
            Update3D();
        }
        private void Update3D()
        {
            double scale = 0.01;
            PreviewModel = Model3DFactory.CreateBox(
                Length * scale,
                Width * scale,
                Thickness * scale,
                Color.FromRgb(93, 173, 226) // Dashboard'la uyumlu açık mavi
            );
        }



        [RelayCommand]
        private void AddSlab()
        {
            var slab = _slabService.CreateDefaultSlab();
            Slabs.Add(slab);
        }

        [RelayCommand]
        private void UpdateSlab()
        {
            if (SelectedSlab == null) return;

            SelectedSlab.FloorLevel = FloorLevel;
            SelectedSlab.Width = Width;
            SelectedSlab.Length = Length;
            SelectedSlab.Thickness = Thickness;


            _slabService.TUpdate(SelectedSlab);
            LoadSlabs();
        }

        [RelayCommand]
        private void DeleteSlab()
        {
            if (SelectedSlab == null) return;

            _slabService.TDelete(SelectedSlab);
            LoadSlabs();
        }
    }
}

