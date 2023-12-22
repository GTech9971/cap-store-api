using Akizuki.Domain.Catalogs;
using Akizuki.Domain.Orders;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CapStore.Domain.Components;
using CapStore.Domain.Inventories;

namespace Akizuki.Infrastructure.Html;

public class AkizukiOrderDetailHtmlRepository : IAkizukiOrderDetailRepository
{
    public async Task<IOrderDetail> Fetch(string source)
    {
        HtmlParser parser = new HtmlParser();
        IDocument parsedDocument = await parser.ParseDocumentAsync(source);

        //オーダーID
        IElement? orderIDRootElement = parsedDocument
                                        .GetElementsByClassName("order_list_")
                                        .FirstOrDefault();
        if (orderIDRootElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }

        IElement? orderIdElement = orderIDRootElement
                                    .GetElementsByTagName("a")
                                    .FirstOrDefault();
        if (orderIdElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }

        string orderIdStr = orderIdElement.TextContent;
        OrderId orderId = new OrderId(orderIdStr);

        //注文日
        IElement? headerElement = parsedDocument
                                    .GetElementsByClassName("table_top_")
                                    .FirstOrDefault();
        if (headerElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }

        IElement? orderElement = headerElement
                                    .GetElementsByTagName("td")
                                    .FirstOrDefault();
        if (orderElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }

        string[] orderDateArray = orderElement
                                    .TextContent
                                    .Split("注文日");
        if (orderDateArray.Length == 0) { throw new AkizukiOrderDetailHtmlParseException(); }

        string orderDateStr = orderDateArray[1].Trim();
        DateTime dateTime = DateTime.ParseExact(orderDateStr, "yyyy年MM月dd日", null);
        OrderDate orderDate = new OrderDate(new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day));

        //伝票番号
        const int HEADER_TR_COUNT = 3;
        IElement? orderDetailHeaderElement = parsedDocument
                                                .GetElementsByClassName("history_order_ top_")
                                                .FirstOrDefault();
        if (orderDetailHeaderElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }

        IHtmlCollection<IElement> orderDetailHeaderTrElmList = orderDetailHeaderElement.GetElementsByTagName("tr");
        if (orderDetailHeaderTrElmList.Count() != HEADER_TR_COUNT) { throw new AkizukiOrderDetailHtmlParseException(); }

        IElement slipNumberTrElement = orderDetailHeaderTrElmList.Skip(1).First();
        IElement slipNumberTdElement = slipNumberTrElement.Children[1];
        string[] slipNumberTextArray = slipNumberTdElement.TextContent.Split("伝票番号");
        if (slipNumberTextArray.Length != 2) { throw new AkizukiOrderDetailHtmlParseException(); }

        slipNumberTextArray = slipNumberTextArray[1].Split("[");
        if (slipNumberTextArray.Length != 2) { throw new AkizukiOrderDetailHtmlParseException(); }
        string slipNumberText = slipNumberTextArray[0].Trim();

        long value;
        if (long.TryParse(slipNumberText, out value) == false) { throw new AkizukiOrderDetailHtmlParseException(); }
        SlipNumber slipNumber = new SlipNumber(value);

        //注文詳細
        IElement? tableElement = parsedDocument
                                    .GetElementsByTagName("table")
                                    .Where(x => x.ClassName == "history_loop_")
                                    .FirstOrDefault();

        if (tableElement == null) { throw new AkizukiOrderDetailHtmlParseException(); }
        IEnumerable<AkizukiOrderComponent> components = CreateOrderDetailList(tableElement);

        return new OrderDetail(orderId, slipNumber, orderDate, components);
    }

    private IEnumerable<AkizukiOrderComponent> CreateOrderDetailList(IElement tableElement)
    {
        IEnumerable<IElement> trElementList = tableElement.GetElementsByTagName("tr");
        if (trElementList.Any() == false || trElementList.Count() == 1) { throw new AkizukiOrderDetailHtmlParseException(); }

        const int TD_COUNT = 4;

        return trElementList
            .Skip(1)
            .Select(x =>
            {
                IHtmlCollection<IElement> tdElementList = x.GetElementsByTagName("td");
                if (tdElementList.Count() != TD_COUNT) { throw new AkizukiOrderDetailHtmlParseException(); }

                //カタログID
                string catalogIdStr = tdElementList
                                        .First()
                                        .Children
                                        .First()
                                        .TextContent;
                CatalogId catalogId = new CatalogId(catalogIdStr);

                //電子部品名
                string componentNameStr = tdElementList
                                            .Skip(1)
                                            .First()
                                            .Children
                                            .First()
                                            .TextContent;
                ComponentName componentName = new ComponentName(componentNameStr);

                //個数
                string quantityUnitStr = tdElementList
                                        .Skip(2)
                                        .First()
                                        .TextContent;

                Unit unit = new Unit("個");
                string quantityStr = quantityUnitStr.Replace(unit.Value, "");

                int quantityVal;
                if (int.TryParse(quantityStr, out quantityVal) == false) { throw new AkizukiOrderDetailHtmlParseException(); }
                Quantity quantity = new Quantity(quantityVal);

                return new AkizukiOrderComponent(quantity,
                                unit,
                                catalogId,
                                ComponentId.UnDetectId(),
                                componentName);
            });
    }
}
