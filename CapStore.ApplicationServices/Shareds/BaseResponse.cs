using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapStore.ApplicationServices.Shareds
{
    /// <summary>
    /// 基本となるレスポンス
    /// </summary>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 成功したかどうか
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// データ
        /// </summary>
        [JsonPropertyName("data")]
        public virtual List<T>? Data { get; set; }

        /// <summary>
        /// エラーリスト
        /// </summary>
        [JsonPropertyName("errors")]
        public virtual List<Error>? Errors { get; set; }

        /// <summary>
        /// HttpStatusCode
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
    }
}
