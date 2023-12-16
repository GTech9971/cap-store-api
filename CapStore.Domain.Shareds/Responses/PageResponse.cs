using System.Text.Json.Serialization;

namespace CapStore.Domain.Shareds.Responses
{
    /// <summary>
    /// ページネーション付きレスポンス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResponse<T> : BaseResponse<T>
    {
        public PageResponse(
            List<T> data,
            int count,
            int pageIndex,
            int pageSize)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }



        /// <summary>
        /// ページ数 (0~)
        /// </summary>
        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; private set; }

        /// <summary>
        /// ページに表示させるデータ件数
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; private set; }

        /// <summary>
        /// データの総合件数
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; private set; }

        /// <summary>
        /// 総ページ数
        /// </summary>
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; private set; }

        /// <summary>
        /// 前ページが存在するかどうか
        /// </summary>
        [JsonPropertyName("hasPreviousPage")]
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        /// <summary>
        /// 次ページが存在するかどうか
        /// </summary>
        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage
        {
            get
            {
                return ((PageIndex + 1) < TotalPages);
            }
        }
    }
}
