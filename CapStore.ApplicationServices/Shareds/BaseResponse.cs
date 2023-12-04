using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapStore.ApplicationServices.Shareds
{
    /// <summary>
    /// 基本となるレスポンス
    /// </summary>
    internal class BaseResponse<T>
    {

        public bool Success { get; set; }


        public virtual List<T>? Data { get; set; }
    }
}
