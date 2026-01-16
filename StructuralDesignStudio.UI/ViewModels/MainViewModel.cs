using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.UI.Services;
using System.Windows.Input;

namespace StructuralDesignStudio.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject currentViewModel;

        public MainViewModel()
        {
            CurrentViewModel = ServiceLocator.Get<DashboardViewModel>();

        }
        [RelayCommand]
        private void OpenDashboard()
        {
            CurrentViewModel = ServiceLocator.Get<DashboardViewModel>();
        }

        [RelayCommand]
        private void OpenColumns()
        {
            CurrentViewModel = ServiceLocator.Get<ColumnViewModel>();
        }

        [RelayCommand]
        private void OpenBeams()
        {
            CurrentViewModel = ServiceLocator.Get<BeamViewModel>();
        }

        [RelayCommand]
        private void OpenSlabs()
        {
            CurrentViewModel = ServiceLocator.Get<SlabViewModel>();
        }

        [RelayCommand]
        private void OpenMaterials()
        {
            CurrentViewModel = ServiceLocator.Get<MaterialViewModel>();
        }
    }

}
