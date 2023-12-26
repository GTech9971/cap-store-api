using System.Text;
using Akizuki.ApplicationServices.Data.Fetch;
using Akizuki.ApplicationServices.OrderDetails;
using Akizuki.Domain.Catalogs;
using Akizuki.Domain.Orders;
using Akizuki.Infrastructure.Html;
using CapStore.Domain.Components;
using CapStore.Domain.Inventories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Akizuki.Infrastructure.Ef.Test;

public class EfOrderRepositoryTest : BaseEfRepositoryTest
{
    private readonly OrderDetailApplicationService _appService;

    public EfOrderRepositoryTest(OrderDetailApplicationService appService)
    {
        ServiceCollection services = new ServiceCollection();
        services.AddTransient<IAkizukiOrderDetailRepository, AkizukiOrderDetailHtmlRepository>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        _appService = serviceProvider.GetService<OrderDetailApplicationService>()!;
    }


    [Fact(DisplayName = "注文詳細登録成功テスト")]
    [Trait("Category", "Akizuki")]
    public async Task TestSaveSuccess()
    {
        //電子部品 マスタ用意
        //TODO

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        string html =
            await File.ReadAllTextAsync("../../../../Akizuki.Infrastructure.Html.Test/Orders/Assets/orders-detail.html",
            Encoding.GetEncoding("SHIFT_JIS"));

        AkizukiOrderDetailSource source = new AkizukiOrderDetailSource(html);
        //登録データ用意
        FetchAkizukiOrderDetailDataDto data = await _appService.FetchAkizukiOrderDetailAsync(source);
        IOrderDetail orderDetail = new OrderDetail(
            new OrderId(data.OrderId),
            new SlipNumber(data.SlipNumber),
            new OrderDate(new DateOnly(2023, 6, 17)),
            data.Components.Select(x => new AkizukiOrderComponent(
                new Quantity(x.Quantity),
                new Unit(x.Unit),
                new CatalogId(x.CatalogId),
                new ComponentId(x.ComponentId),
                new ComponentName(x.ComponentName),
                x.Registered
            ))
        );

        using (AkizukiDbContext context = CreateAkizukiDbContext())
        {
            IAkizukiOrderDetailRepository repository = new EfOrderRepository(context);

            await repository.Save(orderDetail);
        }
    }
}
