using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralDesignStudio.UI.Services
{
    public static class ServiceLocator
    {
        public static IServiceProvider Provider { get; set; } = null!;

        public static T Get<T>() where T : notnull
            => Provider.GetRequiredService<T>();
    }
}
