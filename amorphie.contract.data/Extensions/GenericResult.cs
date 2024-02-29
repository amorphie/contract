using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Amazon.SecurityToken;

namespace amorphie.contract.data.Extensions;
public class ProblemDetails
{
    public int Status { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public override string ToString()
    {
        return "Status: " + Status + " Title: " + Title + " Detail: " + Detail;
    }

}
public class GenericResponse
{
    public string Success { get; set; }
    public object Data { get; set; }
    public string ErrorMessage { get; set; }
}
public class GenericResult<T>  
{
    private GenericResult() : this(true)
    { }

    private GenericResult(bool isSuccess)
    { IsSuccess = isSuccess; if (!isSuccess) { Data = default(T); } }


    [JsonPropertyName("success")]
    public bool IsSuccess { get; init; }

    public T Data { get; private init; } = default(T);

    // public ProblemDetails ProblemDetails { get; private init; } = new();
    public string ErrorMessage { get; private init; }


    public static GenericResult<T> Success(T data)
    {
        return new GenericResult<T>(true)
        {
            Data = data
        };
    }
    public static GenericResult<T> Fail(string message)
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.BadRequest,
            Title = "Operation Error",
            Detail = message
        };
        return new GenericResult<T>(false)
        {
            ErrorMessage = problemDetails.ToString()
        };
    }

    public static GenericResult<T> Exception(ProblemDetails problemDetails)
    {
        return new GenericResult<T>(false)
        {
            ErrorMessage = problemDetails.ToString()
        };
    }

    // public static GenericResult<T> Fail(string message)
    // {
    //     return new GenericResult<T>(false)
    //     {
    //         ErrorMessage = message
    //     };
    // }

    // public static GenericResult<T> Exception(string errorMessage)
    // {
    //     return new GenericResult<T>(false)
    //     {
    //         ErrorMessage = errorMessage
    //     };
    // }
}