using System.Text.Json;
using amorphie.contract.core.Model.Proxy;
using amorphie.contract.core.Response;
using amorphie.contract.infrastructure.Services.Refit;
using amorphie.contract.Models.Proxy;
using Serilog;

namespace amorphie.contract.application.TemplateEngine
{
    public class TemplateEngineAppService : ITemplateEngineAppService
    {
        private readonly ITemplateEngineService _templateEngineService;

        private readonly ILogger _logger;

        public TemplateEngineAppService(ITemplateEngineService templateEngineService, ILogger logger)
        {
            _templateEngineService = templateEngineService;
            _logger = logger;
        }

        public async Task<GenericResult<string>> GetRender(string renderId)
        {
            string result = String.Empty;
            try
            {
                result = await _templateEngineService.GetRender(renderId);
                if (!String.IsNullOrEmpty(result))
                    result = result.Trim('\"');

            }
            catch (Exception ex)
            {

                _logger.Error(ex, "Failed to get render. {RenderId}", renderId);

                return GenericResult<string>.Fail($"failed to fetch getRender {ex.Message}");
            }

            return GenericResult<string>.Success(result);
        }


        public async Task<GenericResult<string>> GetRenderPdf(string renderId)
        {
            string result = String.Empty;
            try
            {
                result = await _templateEngineService.GetRenderPdf(renderId);
                if (!String.IsNullOrEmpty(result))
                    result = result.Trim('\"');

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get render pdf. {RenderId}", renderId);
                return GenericResult<string>.Fail($"failed to fetch getRenderPdf {ex.Message}");
            }

            return GenericResult<string>.Success(result);
        }

        public async Task<GenericResult<string>> SendRenderPdf(TemplateRenderRequestModel requestModel)
        {
            try
            {
                var result = await _templateEngineService.SendRenderPdf(requestModel);
                var responseContent = await result.Content.ReadAsStringAsync();

                if (!result.IsSuccessStatusCode)
                {
                    return GenericResult<string>.Fail($"Failed to send render pdf data {responseContent}");
                }

                if (!String.IsNullOrEmpty(responseContent))
                    responseContent = responseContent.Trim('\"');

                return GenericResult<string>.Success(responseContent);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "failed to send render pdf. {Name}", requestModel.Name);
                return GenericResult<string>.Fail($"failed to send render pdf {ex.Message}");
            }

        }

        public async Task<GenericResult<string>> SendRenderHtml(TemplateRenderRequestModel requestModel)
        {
            try
            {
                var result = await _templateEngineService.SendRenderHtml(requestModel);
                var responseContent = await result.Content.ReadAsStringAsync();

                if (!result.IsSuccessStatusCode)
                {
                    return GenericResult<string>.Fail($"Failed to send render html data {responseContent}");
                }

                if (!String.IsNullOrEmpty(responseContent))
                    responseContent = responseContent.Trim('\"');

                return GenericResult<string>.Success(responseContent);

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "failed to send render html. {Name}", requestModel.Name);
                return GenericResult<string>.Fail($"failed to send render html {ex.Message}");
            }
        }

        public async Task<GenericResult<List<TemplateEngineDefinitionResponseModel>>> GetTemplateDefinitions(string name)
        {
            var result = await _templateEngineService.GetTemplateDefinitions(name);

            string responseContent = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                return GenericResult<List<TemplateEngineDefinitionResponseModel>>.Fail($"Failed to fetch template definitions {responseContent}");
            }

            if (String.IsNullOrEmpty(responseContent))
            {
                return GenericResult<List<TemplateEngineDefinitionResponseModel>>.Success(new List<TemplateEngineDefinitionResponseModel>());
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Dictionary<string, List<TemplateEngineDefinitionResponseModel>> responseDictionary = JsonSerializer.Deserialize<Dictionary<string, List<TemplateEngineDefinitionResponseModel>>>(responseContent, options);
            List<TemplateEngineDefinitionResponseModel> responseList = responseDictionary["templateDefinitionNames"];

            return GenericResult<List<TemplateEngineDefinitionResponseModel>>.Success(responseList);
        }

    }
    public interface ITemplateEngineAppService
    {
        Task<GenericResult<string>> GetRender(string renderId);

        Task<GenericResult<string>> GetRenderPdf(string renderId);

        Task<GenericResult<string>> SendRenderPdf(TemplateRenderRequestModel requestModel);

        Task<GenericResult<string>> SendRenderHtml(TemplateRenderRequestModel requestModel);

        Task<GenericResult<List<TemplateEngineDefinitionResponseModel>>> GetTemplateDefinitions(string name);
    }

}