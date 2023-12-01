using System;
using CapStore.Domain.Components;

namespace CapStore.Infrastructure.Ef.Components
{
    /// <summary>
    /// EFを使用した電子部品の永続化を行う
    /// </summary>
    public class EfComponentRepository : IComponentRepository
    {
        private readonly CapStoreDbContext _context;

        public EfComponentRepository(CapStoreDbContext context)
        {
            _context = context;
        }

        public Task<Component> Fetch(ComponentId componentId)
        {
            throw new NotImplementedException();
        }

        public Task<Component> Fetch(ComponentName componentName)
        {
            throw new NotImplementedException();
        }

        public Task<Component> Save(Component component)
        {
            throw new NotImplementedException();
        }
    }
}

