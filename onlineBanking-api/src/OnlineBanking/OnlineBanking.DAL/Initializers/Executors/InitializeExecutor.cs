using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OnlineBanking.DAL.Initializers.Abstract;

namespace OnlineBanking.DAL.Initializers.Executors
{
    public class InitializeExecutor : IInitializeExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public InitializeExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICollection<IInitializer> Initializers
            => Assembly
                .GetAssembly(typeof(InitializeExecutor))
                .GetTypes()
                .Where(type => typeof(IInitializer).IsAssignableFrom(type) && !type.IsAbstract)
                .Select(type => (IInitializer)Activator.CreateInstance(type))
                .OrderBy(initializer => initializer.Priority)
                .ToList();

        public async Task ExecuteAsync()
        {
            foreach (var initializer in Initializers)
            {
                await initializer.InitAsync(_serviceProvider);
            }
        }
    }
}
