using System;
using CapStore.Infrastructure.Makers.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace CapStore.Infrastructure.Makers.Ef
{
	public class MakerDbContext : DbContext
	{

		public MakerDbContext() : base() { }

		public MakerDbContext(DbContextOptions options) : base(options) { }

		public DbSet<MakerData> MakerDatas => Set<MakerData>();
	}
}

