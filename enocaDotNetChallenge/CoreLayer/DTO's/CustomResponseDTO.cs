using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Core.DTO_s
{
    public class CustomResponseDTO<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<string> Errors { get; set; }
        public static CustomResponseDTO<T> Success(int statusCode, T data, string message)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Data = data, Message = message, Errors = null };
        }
        public static CustomResponseDTO<T> Success(int statusCode, string message)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Message = message };
        }
        public static CustomResponseDTO<T> Fail(List<string> errors, int statusCode)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Errors = errors };
        }
        public static CustomResponseDTO<T> Fail(string error, int statusCode)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
