using Akizuki.Domain.Catalogs;
using Akizuki.Domain.Orders;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CapStore.Domain.Components;

namespace Akizuki.Infrastructure.Html;

public class AkizukiOrderHtmlRepository : IAkizukiOrderRepository
{

    /// <summary>
    /// １つの注文の表示に使用されるtrタグの数
    /// </summary>
    private const int ORDER_TR_TAG_COUNT = 5;

    public async Task<IEnumerable<IOrder>> Fetch(string source)
    {
        HtmlParser parser = new HtmlParser();
        IDocument parsedDocument = await parser.ParseDocumentAsync(source);

        IElement? tableElement = parsedDocument.GetElementsByClassName("cart_table history_").FirstOrDefault();
        if (tableElement == null)
        {
            //TODO
            throw new Exception();
        }

        IHtmlCollection<IElement> trElemList = tableElement.GetElementsByTagName("tr");
        if (trElemList.Any() == false)
        {
            //TODO
            throw new Exception();
        }
        int orderCount = trElemList.Count() / ORDER_TR_TAG_COUNT;
        //trデータの数がおかしい
        if (trElemList.Count() % ORDER_TR_TAG_COUNT != 0)
        {
            //TODO
            throw new Exception();
        }

        IEnumerable<OrderElementList> groupList = GroupBy(trElemList);
        if (groupList.Count() != orderCount)
        {
            //TODO
            throw new Exception();
        }


        return groupList.Select(x => CreateOrder(x));
    }

    /// <summary>
    /// 1 スキップ
    /// 2 注文日、注文合計、支払い方法
    /// 3 会社名、部署名、氏名
    /// 4 配送業者
    /// 5 注文内容        
    /// <td class="order_id_ order_detail_" colspan="3" nowrap=""><b>オーダーＩＤ</b>　<a href="/catalog/customer/historydetail.aspx?order_id=E230617-031873-01">E230617-031873-01</a>　<b>合計</b>　<span class="red">￥1,710</span>
    /// 		<br><b>伝票番号</b>　569321486926<br>
    /// 		<br>【P-01306】　ターミナルブロック　２Ｐ　青　縦　小<br>
    /// 【C-05779】　分割ロングピンソケット　１×４２　（４２Ｐ）<br>
    /// 【C-09862】　Ｌ型ピンソケット　１×６（６Ｐ）<br>
    /// 【I-04430】　ＰＩＣマイコン　ＰＩＣ１６Ｆ１８２７－Ｉ／Ｐ<br>
    /// </td>
    /// </summary>
    /// <param name="elementList"></param>
    /// <returns></returns>
    private IOrder CreateOrder(OrderElementList elementList)
    {
        //2 注文日、注文合計、支払い方法
        IElement secondElement = elementList.Value
                                    .Skip(1)
                                    .First();

        string orderDateStr = secondElement
                                .Children[0]
                                .TextContent
                                .Split("注文日　")[1];

        DateTime dateTime = DateTime.ParseExact(orderDateStr, "yyyy年MM月dd日", null);
        OrderDate orderDate = new OrderDate(new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day));

        string orderTotal = secondElement
                                .Children[1]
                                .TextContent
                                .Split("￥")[1]
                                .Split("　")[0];
        //3 会社名、部署名、氏名
        IElement thirdElement = elementList.Value
                                    .Skip(2)
                                    .First();
        //4 配送業者
        IElement fourthElement = elementList.Value
                                    .Skip(3)
                                    .First();
        string deliverStr = fourthElement
                                .Children[0]
                                .TextContent
                                .Split("配送業者　")[1];
        //5 注文内容        
        IElement fiveElement = elementList.Value
                                    .Skip(4)
                                    .First();

        //キャンセル確認
        bool isCancel = fiveElement.Children[1].InnerHtml.Contains("cancel.gif");

        IElement headerElement = fiveElement
                                    .Children[0];
        //オーダーID
        IElement? orderIdElement = headerElement
                                    .GetElementsByTagName("a")
                                    .FirstOrDefault();
        if (orderIdElement == null)
        {
            //TODO
            throw new Exception();
        }
        string orderIdStr = orderIdElement.TextContent;
        OrderId orderId = new OrderId(orderIdStr);

        //伝票番号、注文内容
        IEnumerable<string> list = fiveElement.TextContent
                                .Replace("\t", "")
                                .Split("\n")
                                .AsEnumerable()
                                .Select(x => x.Replace("\n", ""));

        if (list.Any() == false)
        {
            //TODO
            throw new Exception();
        }

        //伝票番号
        string slipNumberStr = list
                                .Where(x => x.Contains("伝票番号"))
                                .First()
                                .Split("伝票番号　")[1];
        //キャンセルの場合、伝票番号が空になる

        IEnumerable<AkizukiComponent> akizukiComponents
            = list
                .Where(x => x.Contains("【"))
                .Select(x =>
        {
            string[] contexts = x.Split("】");
            if (contexts.Length != 2)
            {
                //TODO
                throw new Exception();
            }
            string catalogIdStr = contexts[0].Replace("【", "");
            CatalogId catalogId = new CatalogId(catalogIdStr);

            ComponentName componentName = new ComponentName(contexts[1].TrimStart());
            return new AkizukiComponent(catalogId, ComponentId.UnDetectId(), componentName);
        });


        // IOrder order = isCancel
        //                 ? new CancelOrder(orderId, orderDate, akizukiComponents)
        //                 : new Order(orderId, slipNumber!, orderDate, akizukiComponents);
        // return order;

        return new Order(orderId, orderDate, akizukiComponents);
    }

    private IEnumerable<OrderElementList> GroupBy(IHtmlCollection<IElement> source)
    {
        return source
                  .Select((value, index) => new { value, index })
                  .GroupBy(pair => pair.index / ORDER_TR_TAG_COUNT)
                  .Select(group => new OrderElementList(group.Select(pair => pair.value)));
    }

    private sealed class OrderElementList
    {
        private readonly IReadOnlyCollection<IElement> _elements;

        public OrderElementList(IEnumerable<IElement> elements)
        {
            if (elements.Count() != ORDER_TR_TAG_COUNT)
            {
                //TODO
                throw new Exception();
            }

            _elements = elements.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<IElement> Value => _elements;
    }
}
