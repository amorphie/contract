using amorphie.contract.core;
using amorphie.contract.core.Response;
using amorphie.contract.infrastructure.Services.Refit;

namespace amorphie.contract.application.TemplateEngine
{
    public class TemplateEngineAppService : ITemplateEngineAppService
    {
        private readonly ITemplateEngineService _templateEngineService;
        public TemplateEngineAppService(ITemplateEngineService templateEngineService)
        {
            _templateEngineService = templateEngineService;
        }
        public async Task<string> GetRenderInstance(string instance)//TODO:dapr kullanılacak 
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(StaticValuesExtensions.TemplateEngineUrl + string.Format(StaticValuesExtensions.TemplateEngineRenderInstance, instance));

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

            }
            return "Template engine error";
        }

        public async Task<GenericResult<string>> GetRender(string renderId)
        {
            string result = String.Empty;
            try
            {
                result = await _templateEngineService.GetRender(renderId);
            }
            catch (Exception ex)
            {
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
            }
            catch (Exception ex)
            {
                return GenericResult<string>.Fail($"failed to fetch getRenderPdf {ex.Message}");
            }

            return GenericResult<string>.Success(result);
        }

    }
    public interface ITemplateEngineAppService
    {
        public Task<string> GetRenderInstance(string instance);//TODO:dapr kullanılacak 

        Task<GenericResult<string>> GetRender(string renderId);

        Task<GenericResult<string>> GetRenderPdf(string renderId);
    }

}