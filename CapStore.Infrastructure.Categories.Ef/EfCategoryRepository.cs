using System;
using Microsoft.EntityFrameworkCore;
using CapStore.Domain.Categories;
using CapStore.Infrastructure.Categories.Ef.Data;

namespace CapStore.Infrastructure.Categories.Ef
{
    /// <summary>
    /// EFを使用したカテゴリーの永続化を行う
    /// </summary>
    public class EfCategoryRepository : ICategoryRepository
    {

        private readonly CategoryDbContext _context;

        public EfCategoryRepository(CategoryDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> Fetch(CategoryId categoryId)
        {
           CategoryData? data = await _context.CategoryDatas
                .AsNoTracking()
                .Where(x => x.Id == categoryId.Value)
                .SingleOrDefaultAsync();

            if(data == null)
            {
                return null;
            }

            return ToModel(data);
        }

        public async Task<Category?> Fetch(CategoryName categoryName)
        {
            CategoryData? data = await _context.CategoryDatas
                .AsNoTracking()
                .Where(x => x.Name == categoryName.Value)
                .SingleOrDefaultAsync();

            if(data == null)
            {
                return null;
            }

            return ToModel(data);
        }

        public async Task<Category> Save(Category category)
        {
            CategoryData? found = await _context.CategoryDatas
                .Where(x => x.Id == category.Id.Value)
                .SingleOrDefaultAsync();

            CategoryData data;
            if (found == null)
            {
                data = new CategoryData(category);
                await _context.CategoryDatas.AddAsync(data);
            }
            else
            {
                data = Transfer(category, found);
                _context.CategoryDatas.Update(data);
            }

            await _context.SaveChangesAsync();

            return ToModel(data);
        }

        private Category ToModel(CategoryData from)
        {
            return new Category(
                new CategoryId(from.Id),
                new CategoryName(from.Name),
                from.Image == null
                ? null
                : new Domain.Shareds.ImageUrl(from.Image));
        }

        private CategoryData Transfer(Category from, CategoryData to)
        {
            to.Id = from.Id.Value;
            to.Name = from.Name.Value;
            to.Image = from.Image?.Value;
            return to;
        }
    }
}

