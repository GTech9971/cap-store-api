using CapStore.Domains.Components;
using CapStore.Infrastructure.Ef.Components.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using CapStore.Domains;

namespace CapStore.Infrastructure.Ef.Components
{
    /// <summary>
    /// EFを使用した電子部品の永続化を行う
    /// </summary>
    public class EfComponentRepository : IComponentRepository
    {
        private readonly CapStoreDbContext _context;
        private readonly FilterSortService<ComponentData> _filterSortService;

        public EfComponentRepository(CapStoreDbContext context,
                                    FilterSortService<ComponentData> filterSortService)
        {
            _context = context;
            _filterSortService = filterSortService;
        }

        public async Task<Component?> Fetch(ComponentId componentId)
        {
            ComponentData? data = await _context.ComponentDatas
                .AsNoTracking()
                .Where(x => x.ComponentId == componentId.Value)
                .Include(x => x.CategoryData)
                .Include(x => x.MakerData)
                .Include(x => x.ComponentImageDatas)
                .SingleOrDefaultAsync();

            if (data == null)
            {
                return null;
            }

            return data.ToModel();
        }

        public async Task<Component?> Fetch(ComponentName componentName)
        {
            ComponentData? data = await _context.ComponentDatas
                .AsNoTracking()
                .Where(x => x.Name == componentName.Value)
                .Include(x => x.CategoryData)
                .Include(x => x.MakerData)
                .Include(x => x.ComponentImageDatas)
                .SingleOrDefaultAsync();

            if (data == null)
            {
                return null;
            }

            return data.ToModel();
        }

        public async Task<Component?> Fetch(ComponentModelName modelName)
        {
            ComponentData? data = await _context.ComponentDatas
                .AsNoTracking()
                .Where(x => x.ModelName == modelName.Value)
                .Include(x => x.CategoryData)
                .Include(x => x.MakerData)
                .Include(x => x.ComponentImageDatas)
                .SingleOrDefaultAsync();

            if (data == null)
            {
                return null;
            }

            return data.ToModel();
        }

        public IQueryable<Component> FetchAll(string? sortColumn = null,
                                              string? sortOrder = null,
                                              string? filterColumn = null,
                                              string? filterQuery = null)
        {
            IQueryable<ComponentData> components = _context.ComponentDatas
                            .AsNoTracking()
                            .Include(x => x.CategoryData)
                            .Include(x => x.MakerData)
                            .Include(x => x.ComponentImageDatas);

            //フィルター
            if (string.IsNullOrWhiteSpace(filterColumn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                components = _filterSortService.filter(components, filterColumn, filterQuery);
            }

            //ソート
            if (string.IsNullOrEmpty(sortColumn) == false && string.IsNullOrWhiteSpace(sortOrder) == false)
            {
                components = _filterSortService.sort(components, sortColumn, sortOrder);
            }

            return components
                    .Select(x => x.ToModel());
        }

        public async Task<Component> Save(Component component)
        {
            ComponentData? found = await _context.ComponentDatas
                .Where(x => x.ComponentId == component.Id.Value)
                .Include(x => x.CategoryData)
                .Include(x => x.MakerData)
                .SingleOrDefaultAsync();

            ComponentData data;
            if (found == null)
            {
                data = new ComponentData(component);
                await _context.ComponentDatas.AddAsync(data);
                await _context.Entry(data).Reference(x => x.MakerData).LoadAsync();
                await _context.Entry(data).Reference(x => x.CategoryData).LoadAsync();
            }
            else
            {
                data = Transfer(component, found);
                _context.ComponentDatas.Update(data);
            }

            await _context.SaveChangesAsync();

            return data.ToModel();
        }

        private ComponentData Transfer(Component from, ComponentData to)
        {
            to.ComponentId = from.Id.Value;
            to.Name = from.Name.Value;
            to.ModelName = from.ModelName.Value;
            to.Description = from.Description.Value;
            //category
            to.CategoryId = from.Category.Id.Value;
            to.CategoryData.Id = from.Category.Id.Value;
            to.CategoryData.Name = from.Category.Name.Value;
            to.CategoryData.Image = from.Category.Image?.Value;
            //maker
            to.MakerId = from.Maker.Id.Value;
            to.MakerData.Id = from.Maker.Id.Value;
            to.MakerData.Name = from.Maker.Name.Value;
            to.MakerData.Image = from.Maker.Image?.Value;
            //images
            var fromImageList = from.Images.AsList();
            to.ComponentImageDatas.ToList().ForEach(x =>
            {
                //電子部品画像UrlIdと電子部品IDで一意に特定する
                ComponentImage? fromImage = fromImageList.SingleOrDefault(y => y.ComponentImageId.Value == x.Id && y.ComponentId.Value == x.ComponentId);
                x.ImageUrl = fromImage == null ? "" : fromImage.Image.Value;
            });
            return to;
        }
    }
}

