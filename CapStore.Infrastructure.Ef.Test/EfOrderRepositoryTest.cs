using System.Text;
using CapStore.Domains.Akizukies.Orders;
using CapStore.Domains.Components;
using CapStore.Infrastructure.Html.Orders;
using Xunit;

namespace CapStore.Infrastructure.Ef.Test;

[Collection("Ef Order")]
public class EfOrderRepositoryTest : IClassFixture<BaseEfRepositoryTest>
{
    private readonly CapStoreDbContext _context;
    private readonly IAkizukiOrderDetailSourceRepository _repository;

    public EfOrderRepositoryTest(BaseEfRepositoryTest fixture) : base()
    {
        _repository = new AkizukiOrderDetailHtmlRepository();
        _context = fixture._context;
    }

    [Fact(DisplayName = "注文詳細登録成功テスト")]
    [Trait("Category", "Akizuki")]
    public async Task TestSaveSuccess()
    {
        string html = await File.ReadAllTextAsync("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/order-detail.html", Encoding.UTF8);

        AkizukiOrderDetailSource source = new AkizukiOrderDetailSource(html);
        //登録データ用意
        IOrderDetail data = await _repository.Fetch(source);

        //仮の電子部品IDを付与
        ComponentId dummyId = new ComponentId(1);
        IOrderDetail applyComponentIdData = new OrderDetail(data.OrderId, data.OrderDate,
            data.Components.Select(x => new AkizukiOrderComponent(x.Quantity, x.Unit, x.CatalogId, dummyId))
        );

        IAkizukiOrderDetailRepository repository = new EfOrderRepository(_context);

        await repository.SaveAsync(applyComponentIdData);

    }
}
