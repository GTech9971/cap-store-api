using System.Text;
using Akizuki.Domain.Orders;
using Akizuki.Infrastructure.Html;
using CapStore.Domain.Components;
using Xunit;

namespace CapStore.Infrastructure.Ef.Test;

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
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        string html =
            await File.ReadAllTextAsync("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/orders-detail.html",
            Encoding.GetEncoding("SHIFT_JIS"));

        AkizukiOrderDetailSource source = new AkizukiOrderDetailSource(html);
        //登録データ用意
        IOrderDetail data = await _repository.Fetch(source);

        //仮の電子部品IDを付与
        ComponentId dummyId = new ComponentId(1);
        IOrderDetail applyComponentIdData = new OrderDetail(data.OrderId, data.SlipNumber, data.OrderDate,
            data.Components.Select(x => new AkizukiOrderComponent(x.Quantity, x.Unit, x.CatalogId, dummyId, x.ComponentName, true))
        );

        IAkizukiOrderDetailRepository repository = new EfOrderRepository(_context);

        await repository.Save(applyComponentIdData);

    }
}
