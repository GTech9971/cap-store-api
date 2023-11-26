using System;
using CapStore.Infrastructure.Categories.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace CapStore.Infrastructure.Categories.Ef
{
	public class CategoryDbContext : DbContext
	{
		public CategoryDbContext() : base() { }

		public CategoryDbContext(DbContextOptions options) : base(options) { }

		public DbSet<CategoryData> CategoryDatas => Set<CategoryData>();
	}
}