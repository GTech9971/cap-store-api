using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStore.ApplicationServices.Shareds
{
    /// <summary>
    /// エラー情報
    /// </summary>
    sealed internal class Error
    {
        /// <summary>
        /// エラーコード
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string Message { get; set; } = null!;

        /// <summary>
        /// エラースタック
        /// </summary>
        public string? Stack { get; set; }
    }
}
