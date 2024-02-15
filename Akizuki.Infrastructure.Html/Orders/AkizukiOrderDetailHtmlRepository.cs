using Akizuki.Domain.Catalogs;
using Akizuki.Domain.Orders;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CapStore.Domain.Components;
using CapStore.Domain.Inventories;

namespace Akizuki.Infrastructure.Html;

public class AkizukiOrderDetailHtmlRepository : IAkizukiOrderDetailSourceRepository
{
    public async Task<IOrderDetail> Fetch(AkizukiOrderDetailSource source)
    {
        HtmlParser parser = new HtmlParser();
        IDocument parsedDocument = await parser.ParseDocumentAsync(source.Value);

        //オーダーID
        IElement? orderIdElement = parsedDocument
                                        .GetElementsByClassName("block-purchase-history-detail--order-id")
                                        .FirstOrDefault();
        if (orderIdElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }

        string orderIdStr = orderIdElement.TextContent;
        OrderId orderId = new OrderId(orderIdStr);

        //注文日
        IElement? orderAtElement = parsedDocument
                                    .GetElementsByClassName("block-purchase-history-detail--order-dt")
                                    .FirstOrDefault();
        if (orderAtElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }

        string orderDateStr = orderAtElement.TextContent;
        DateTime dateTime = DateTime.ParseExact(orderDateStr, "yyyy年MM月dd日", null);
        OrderDate orderDate = new OrderDate(new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day));

        //注文詳細
        IElement? tableElement = parsedDocument
                                    .GetElementsByClassName("block-purchase-history-detail--requests-for-order--add-cart")
                                    .FirstOrDefault();

        if (tableElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }

        IEnumerable<AkizukiOrderComponent> components = CreateOrderDetailList(tableElement);

        return new OrderDetail(orderId, orderDate, components);
    }



    private IEnumerable<AkizukiOrderComponent> CreateOrderDetailList(IElement tableElement)
    {
        List<IElement> catalogList = tableElement
                                                .GetElementsByTagName("input")
                                                .Where(x => x.GetAttribute("name") == "goods")
                                                .ToList();

        List<IElement> quantityList = tableElement
                                                .GetElementsByTagName("input")
                                                .Where(x => x.GetAttribute("name") == "qty")
                                                .ToList();

        if (catalogList.Any() == false || quantityList.Any() == false) { throw new AkizukiOrderDetailHtmlParseException(); }
        if (catalogList.Count != quantityList.Count) { throw new AkizukiOrderDetailHtmlParseException(); }

        Unit pieces = Unit.Pieces();
        return catalogList.Select((catalogElem, index) =>
        {
            string catalogIdStr = catalogElem.GetAttribute("value")!;
            CatalogId catalogId = new CatalogId(catalogIdStr);

            //注文個数
            string quantityStr = quantityList[index].GetAttribute("value")!;
            int quantityVal = int.Parse(quantityStr);
            Quantity quantity = new Quantity(quantityVal);

            return new AkizukiOrderComponent(
                quantity,
                pieces,
                catalogId,
                ComponentId.UnDetectId()
            );
        });
    }
}
