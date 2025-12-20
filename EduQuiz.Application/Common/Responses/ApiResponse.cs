using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Application.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public ApiMeta? Meta { get; set; }
        public List<ApiError>? Errors { get; set; }

        public static ApiResponse<T> Ok(T data, ApiMeta? meta = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Meta = meta
            };
        }

        public static ApiResponse<T> Fail(params ApiError[] errors)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = errors.ToList()
            };
        }
    }
}
