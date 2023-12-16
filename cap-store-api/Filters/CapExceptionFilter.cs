using System;
using System.Text.Json;
using CapStore.Domain.Shareds;
using CapStore.Domain.Shareds.Exceptions;
using CapStore.Domain.Shareds.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace cap_store_api.Filters
{
    /// <summary>
    /// 例外発生時のフィルター
    /// </summary>
	public class CapExceptionFilter : IExceptionFilter
    {
        public CapExceptionFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            bool isC400 = exception is ValidationException || exception is ValidationArgumentException ||
                exception is ValidationArgumentNullException;

            BaseResponse<Object> response;

            if (isC400)
            {
                response = new C400Response(exception as ValidationException);
            }
            else
            {
                response = new C999Response(exception);
            }


            string contentJson = JsonSerializer.Serialize(response);
            context.Result = new ContentResult
            {
                Content = contentJson,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode
            };
        }
    }
}

