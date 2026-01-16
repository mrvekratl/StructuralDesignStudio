using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.DtoLayer.BeamDTOs;
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
    public partial class BeamViewModel : ObservableObject
    {
        private readonly IBeamService _beamService;

        public ObservableCollection<StructuralBeam> Beams { get; } = new();

        [ObservableProperty] private StructuralBeam? selectedBeam;

        [ObservableProperty] private double width;
        [ObservableProperty] private double height;
        [ObservableProperty] private double length;
        [ObservableProperty] private int selectedFloor;
        [ObservableProperty] private Model3D? previewModel;
        partial void OnWidthChanged(double v) => Update3D();
        partial void OnLengthChanged(double v) => Update3D();
        partial void OnHeightChanged(double v) => Update3D();



        public BeamViewModel(IBeamService beamService)
        {
            _beamService = beamService;
            LoadBeams();
        }

        private void LoadBeams()
        {
            Beams.Clear();
            foreach (var beam in _beamService.TGetList())
                Beams.Add(beam);
        }

        partial void OnSelectedBeamChanged(StructuralBeam? value)
        {
            if (value == null)
            {
                PreviewModel = null;
                return;
            }
            if (value == null) return;            

            Width = value.Width;
            height = value.Height;
            Length = value.Length;
            Update3D();
        }
        private void Update3D()
        {
            double scale = 0.03;

            // Beam YATAY olmalı: Length uzunluk (X ekseni), Width genişlik (Y), Height yükseklik (Z)
            // Yatay görünmesi için Length'i X eksenine koyuyoruz
            PreviewModel = Model3DFactory.CreateBox(
                Length * scale,    // X ekseni - uzunluk (yatay)
                Height * scale,    // Y ekseni - yükseklik
                Width * scale,     // Z ekseni - genişlik
                Color.FromRgb(52, 152, 219) // Dashboard'la uyumlu orta mavi
            );
        }

        [RelayCommand]
        private void AddBeam()
        {
            var beam = _beamService.CreateDefaultBeam();
            Beams.Add(beam);
        }

        [RelayCommand]
        private void UpdateBeam()
        {
            if (SelectedBeam == null) return;

            SelectedBeam.Width = Width;
            SelectedBeam.Height = Height;
            SelectedBeam.Length = Length;

            _beamService.TUpdate(SelectedBeam);
            LoadBeams();
        }

        [RelayCommand]
        private void DeleteBeam()
        {
            if (SelectedBeam == null) return;

            _beamService.TDelete(SelectedBeam);
            LoadBeams();
        }
    }

}
