using Microsoft.EntityFrameworkCore;

namespace Akizuki.Infrastructure.Ef;

public class AkizukiDbContext : DbContext
{
    public AkizukiDbContext() : base() { }

    public AkizukiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<OrderData> OrderDatas => Set<OrderData>();

    public DbSet<OrderDetailData> OrderDetailDatas => Set<OrderDetailData>();
}
