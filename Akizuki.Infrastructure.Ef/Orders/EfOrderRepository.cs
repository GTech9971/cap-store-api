using Akizuki.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Akizuki.Infrastructure.Ef;

/// <summary>
/// EFを使用した秋月電子の注文の永続化を行う
/// </summary>
public class EfOrderRepository : IAkizukiOrderDetailRepository
{

    private readonly AkizukiDbContext _context;

    public EfOrderRepository(AkizukiDbContext context)
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


    /// <summary>
    /// 不要なので実装しない
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IOrderDetail> Fetch(AkizukiOrderDetailSource source)
    {
        throw new NotImplementedException();
    }
}
