using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace amorphie.contract.infrastructure.Services.Refit
{
    public interface ICustomerApiService
    {
        [Get("/customer?keyword={keyword}&api-version={apiVersion}")]
        Task<HttpResponseMessage> GetCustomerInfo(string keyword, string apiVersion);
    }
}