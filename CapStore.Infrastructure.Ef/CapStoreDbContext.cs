using System;
using CapStore.Infrastructure.Ef.Categories.Data;
using CapStore.Infrastructure.Ef.Makers.Data;
using Microsoft.EntityFrameworkCore;

namespace CapStore.Infrastructure.Ef
{
	public class CapStoreDbContext : DbContext
	{
		public CapStoreDbContext() : base() { }

		public CapStoreDbContext(DbContextOptions options) : base(options) { }



		public DbSet<CategoryData> CategoryDatas => Set<CategoryData>();
		public DbSet<MakerData> MakerDatas => Set<MakerData>();
	}
}

