using Microsoft.Extensions.DependencyInjection;
using StructuralDesignStudio.BusinessLayer.Abstract;
using StructuralDesignStudio.BusinessLayer.Concrete;
using StructuralDesignStudio.DataAccessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.Concrete.Context;
using StructuralDesignStudio.DataAccessLayer.EntityFramework;
using StructuralDesignStudio.UI.Services;
using StructuralDesignStudio.UI.ViewModels;
using StructuralDesignStudio.UI.Views;
using System.Configuration;
using System.Data;
using System.Windows;
using StructuralDesignStudio.DataAccessLayer.Abstract;
using StructuralDesignStudio.DataAccessLayer.Concrete;

namespace StructuralDesignStudio.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // 🔹 Business Services
            

            services.AddDbContext<StructuralContext>();

            services.AddScoped<IColumnDal, EfColumnDal>();
            services.AddScoped<IColumnService, ColumnManager>();        
            services.AddScoped<IBeamService, BeamManager>();
            services.AddScoped<IBeamDal, EfBeamDal>();
            services.AddScoped<ISlabService, SlabManager>();
            services.AddScoped<ISlabDal, EfSlabDal>();
            services.AddScoped<IMaterialService, MaterialManager>();
            services.AddScoped<IMaterialDal, EfMaterialDal>();

            // 🔹 ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<ColumnViewModel>();
            services.AddTransient<BeamViewModel>();
            services.AddTransient<SlabViewModel>();
            services.AddTransient<MaterialViewModel>();
            services.AddTransient<DashboardViewModel>();


            ServiceLocator.Provider = services.BuildServiceProvider();

            var mainWindow = new MainWindow(
                ServiceLocator.Get<MainViewModel>()
            );
            mainWindow.Show();
        }
    }

}
