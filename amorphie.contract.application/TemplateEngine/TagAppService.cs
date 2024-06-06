using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using amorphie.contract.application.TemplateEngine.Dto;
using amorphie.contract.core.Response;
using amorphie.contract.infrastructure.Services.Refit;
using Serilog;

namespace amorphie.contract.application.TemplateEngine
{
    public interface ITagAppService
    {
        Task<GenericResult<string>> SendRenderPdf(TemplateRenderTagInputDto input);
        Task<GenericResult<string>> SendRenderHtml(TemplateRenderTagInputDto input);
        Task<GenericResult<Dictionary<string, string>>> GetRenderValues(GetRenderDataTagInputDto inputDto);
    }

    public class TagAppService : ITagAppService
    {
        private readonly ITagService _tagService;
        private readonly ILogger _logger;

        public TagAppService(ITagService tagService, ILogger logger)
        {
            _tagService = tagService;
            _logger = logger;
        }

        public async Task<GenericResult<string>> SendRenderPdf(TemplateRenderTagInputDto input)
        {
            try
            {
                var result = await _tagService.RenderPdfFromTag(input.DomainName, input.EntityName, input.TagName, input.ViewTemplateName, input.Reference, input.Version);
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
                _logger.Error(ex, "failed to send render pdf. {ViewTemplateName}", input.ViewTemplateName);
                return GenericResult<string>.Fail($"failed to send render pdf {ex.Message}");
            }
        }

        public async Task<GenericResult<string>> SendRenderHtml(TemplateRenderTagInputDto input)
        {
            try
            {
                var result = await _tagService.RenderHtmlFromTag(input.DomainName, input.EntityName, input.TagName, input.ViewTemplateName, input.Reference, input.Version);
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
                _logger.Error(ex, "failed to send render html. {ViewTemplateName}", input.ViewTemplateName);
                return GenericResult<string>.Fail($"failed to send render html {ex.Message}");
            }
        }

        public async Task<GenericResult<Dictionary<string, string>>> GetRenderValues(GetRenderDataTagInputDto inputDto)
        {
            try
            {
                var result = await _tagService.GetRenderValuesFromTag(inputDto.DomainName, inputDto.EntityName, inputDto.TagName, inputDto.Reference);
                var responseContent = await result.Content.ReadAsStringAsync();

                if (!result.IsSuccessStatusCode)
                {
                    return GenericResult<Dictionary<string, string>>.Fail($"Failed to get render values {responseContent}");
                }

                if (!String.IsNullOrEmpty(responseContent))
                    responseContent = responseContent.Trim('\"');

                var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);

                return GenericResult<Dictionary<string, string>>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get render values. {TagName}", inputDto.TagName);
                return GenericResult<Dictionary<string, string>>.Fail($"Failed to get render values {ex.Message}");
            }
        }
    }
}