using System;
using System.Collections;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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

        // Without addNewtonsoft
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T? Item { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T? Items { get; set; }
    }
}
