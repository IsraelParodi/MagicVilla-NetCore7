using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace MagicVilla_API.Utils
{
    public class ApiResponse<T>
    {

        public ApiResponse(int statusCode, string message, T item)
        {
            bool isList = typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>);

            StatusCode = statusCode;
            Message = message;

            if (isList) Items = item;
            else Item = item;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Item { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Items { get; set; }
    }
}
