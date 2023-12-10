using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapStore.ApplicationServices.Shareds
{
    /// <summary>
    /// エラー情報
    /// </summary>
    sealed public class Error
    {

        private readonly ErrorCode _code;
        private readonly ErrorMessage _message;
        private readonly string? _stack;

        public Error(ErrorCode code,
                    ErrorMessage message)
        {
            if (code == null)
            {
                throw new ArgumentNullException("エラーコードは必須です");
            }

            if (message == null)
            {
                throw new ArgumentNullException("エラーメッセージは必須です");
            }

            _code = code;
            _message = message;
            _stack = null;
        }

        public Error(ErrorCode code,
                    ErrorMessage message,
                    string stack) : this(code, message)
        {
            _stack = stack;
        }



        /// <summary>
        /// エラーコード
        /// </summary>
        [JsonIgnore]
        public ErrorCode Code => _code;

        [JsonPropertyName("code")]
        public string CodeValue => _code.Value;

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        [JsonIgnore]
        public ErrorMessage Message => _message;

        [JsonPropertyName("message")]
        public string MessageValue => _message.Value;

        /// <summary>
        /// エラースタック
        /// </summary>
        [JsonPropertyName("stack")]
        public string? Stack => _stack;
    }
}
