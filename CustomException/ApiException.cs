using System;
using System.Net;

namespace webapi.CustomException ;

public class ApiException : Exception
{
    public int StatusCode{get;set;}
    public ApiException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

