using Akizuki.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace CapStore.Infrastructure.Ef;

/// <summary>
/// EFを使用した秋月電子の注文の永続化を行う
/// </summary>
public class EfOrderRepository : IAkizukiOrderDetailRepository
{

    private readonly CapStoreDbContext _context;

    public EfOrderRepository(CapStoreDbContext context)
    {
        _context = context;
    }

    public async Task Save(IOrderDetail orderDetail)
    {
        OrderData? found = await _context.OrderDatas
            .Where(x => x.OrderId == orderDetail.OrderId.Value)
            .SingleOrDefaultAsync();

        if (found == null)
        {
            OrderData data = new OrderData(orderDetail);
            await _context.OrderDatas.AddAsync(data);
        }
        else
        {
            throw new NotSupportedException("注文の更新は実行できません");
        }

        await _context.SaveChangesAsync();
    }


    public async Task<IOrderDetail?> Fetch(OrderId orderId)
    {
        OrderData? found = await _context.OrderDatas
                               .AsNoTracking()
                               .Include(x => x.OrderDetailDatas)
                               .Where(x => x.OrderId == orderId.Value)
                               .SingleOrDefaultAsync();

        return found == null ? null : found.ToModel();
    }
}
