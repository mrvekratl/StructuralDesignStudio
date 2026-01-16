using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StructuralDesignStudio.EntityLayer.Concrete;
using StructuralDesignStudio.BusinessLayer.Abstract;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;
using StructuralDesignStudio.UI.Helpers;

namespace StructuralDesignStudio.UI.ViewModels
{
    public partial class ColumnViewModel : ObservableObject
    {
        private readonly IColumnService _columnService;

        public ObservableCollection<StructuralColumn> Columns { get; } = new();

        [ObservableProperty] private StructuralColumn? selectedColumn;
        [ObservableProperty] private int floorLevel;
        [ObservableProperty] private double width;
        [ObservableProperty] private double depth;
        [ObservableProperty] private double height;
        [ObservableProperty] private Model3D? previewModel;
        partial void OnWidthChanged(double v) => Update3D();
        partial void OnDepthChanged(double v) => Update3D();
        partial void OnHeightChanged(double v) => Update3D();





        public ColumnViewModel(IColumnService columnService)
        {
            _columnService = columnService;
            LoadColumns();
        }

        private void LoadColumns()
        {
            Columns.Clear();
            foreach (var column in _columnService.TGetList())
                Columns.Add(column);
        }

        partial void OnSelectedColumnChanged(StructuralColumn? value)
        {
            if (value == null)
            {
                PreviewModel = null;
                return;
            }

            FloorLevel = value.FloorLevel;
            Width = value.Width;
            Depth = value.Depth;
            Height = value.Height;

            Update3D();
        }
        private void Update3D()
        {
            double scale = 0.01; // Biraz daha büyük göster
            PreviewModel = Model3DFactory.CreateBox(
                Width * scale,
                Depth * scale,
                Height * scale,
                Color.FromRgb(41, 128, 185) // Dashboard'la uyumlu koyu mavi
            );
        }
        [RelayCommand]
        private void AddColumn()
        {
            var column = _columnService.CreateDefaultColumn();
            Columns.Add(column);
        }

        [RelayCommand]
        private void UpdateColumn()
        {
            if (SelectedColumn == null) return;

            SelectedColumn.FloorLevel = FloorLevel;
            SelectedColumn.Width = Width;
            SelectedColumn.Depth = Depth;
            SelectedColumn.Height = Height;

            _columnService.TUpdate(SelectedColumn);
            LoadColumns();
        }

        [RelayCommand]
        private void DeleteColumn()
        {
            if (SelectedColumn == null) return;

            _columnService.TDelete(SelectedColumn);
            LoadColumns();
        }
    }
}
