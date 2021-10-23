using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.DomainLayer.Communication
{
    public class Response<T>
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public T Data;

        public Response(T data)
        {
            Success = true;
            Message = "";
            Data = data;
        }

        public Response(string message)
        {
            Success = false;
            Message = message;
        }

        public Response(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
