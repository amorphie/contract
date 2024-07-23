using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Model.Proxy;
using Refit;

namespace amorphie.contract.infrastructure.Services.Refit
{
    public interface IMessagingGatewayService
    {
        [Headers("Content-Type: application/json")]
        [Post("/api/v2/Messaging/email/templated")]
        Task<HttpResponseMessage> SendTemplatedMail([Body] SendTemplatedMailRequestModel mailRequestModel);
    }
}