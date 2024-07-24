using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using amorphie.contract.core.Response;
using amorphie.contract.infrastructure.Services.Refit;
using iText.Svg.Renderers.Path.Impl;
using Serilog;

namespace amorphie.contract.application.CustomerApi
{
    public interface ICustomerApiAppService
    {
        Task<GenericResult<string>> GetCustomerEmail(string reference);
    }

    public class CustomerApiAppService : ICustomerApiAppService
    {
        private readonly ICustomerApiService _customerApiService;
        private readonly ILogger _logger;

        public CustomerApiAppService(ICustomerApiService customerApiService, ILogger logger)
        {
            _customerApiService = customerApiService;
            _logger = logger;
        }

        public async Task<GenericResult<string>> GetCustomerEmail(string reference)
        {
            var response = await _customerApiService.GetCustomerInfo(reference, "3");

            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error("Failed to get Customer Info: {ResponseContent}", responseContent);
                return GenericResult<string>.Fail($"Failed to get Customer Info {responseContent}");
            }

            // if (String.IsNullOrEmpty(responseContent))
            // {
            //     return GenericResult<List<TemplateEngineDefinitionResponseModel>>.Success(new List<TemplateEngineDefinitionResponseModel>());
            // }

            try
            {
                var jsonObject = JsonDocument.Parse(responseContent);
                JsonElement root = jsonObject.RootElement;

                JsonElement customer = root.GetProperty("customerList")[0];

                string email = customer.GetProperty("email").GetString();

                if (String.IsNullOrEmpty(email))
                {
                    _logger.Error("Customer email address not found for reference: {Reference}", reference);
                    return GenericResult<string>.Fail($"Customer email address not found.");
                }

                return GenericResult<string>.Success(email);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error parsing customer info response");
                return GenericResult<string>.Fail("Error parsing customer info response.");
            }

        }
    }
}