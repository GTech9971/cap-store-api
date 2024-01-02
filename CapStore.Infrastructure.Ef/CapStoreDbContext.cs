using System;
using CapStore.Infrastructure.Ef.Categories.Data;
using CapStore.Infrastructure.Ef.Components.Data;
using CapStore.Infrastructure.Ef.Makers.Data;
using Microsoft.EntityFrameworkCore;

namespace CapStore.Infrastructure.Ef
{
	public class CapStoreDbContext : DbContext
	{
		public CapStoreDbContext() : base() { }

		public CapStoreDbContext(DbContextOptions options) : base(options) { }

		//マスター
		public DbSet<ComponentData> ComponentDatas => Set<ComponentData>();
		public DbSet<ComponentImageData> ComponentImageDatas => Set<ComponentImageData>();
		public DbSet<CategoryData> CategoryDatas => Set<CategoryData>();
		public DbSet<MakerData> MakerDatas => Set<MakerData>();

		//発注
		public DbSet<OrderData> OrderDatas => Set<OrderData>();
		public DbSet<OrderDetailData> OrderDetailDatas => Set<OrderDetailData>();
	}
}

