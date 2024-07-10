using System.Text.Json;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.TemplateEngine.Dto;
using amorphie.contract.core.Model;
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
        Task<GenericResult<Dictionary<string, string>>> GetTagMetadata(List<MetadataDto> metadata, HeaderFilterModel headerModel);
    }

    public class TagAppService : ITagAppService
    {
        private readonly string[] allowedQueries = { "customerNo", "reference" };
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

        public async Task<GenericResult<Dictionary<string, string>>> GetTagMetadata(List<MetadataDto> metadata, HeaderFilterModel headerModel)
        {
            var cachedTagPath = new HashSet<GetRenderDataTagInputDto>();
            var cachedTagValue = new Dictionary<string, string>();
            var resultTags = new Dictionary<string, string>();

            foreach (var item in metadata)
            {
                if (item.IsValidTagData())
                {
                    var tagKeywords = item.Data.Split('.');
                    if (tagKeywords.Length <= 4)
                    {
                        throw new InvalidOperationException($"Tag keywords are incorrectly defined {item.Data}");
                    }

                    if (cachedTagValue.TryGetValue(item.Code, out var tagValue))
                    {
                        resultTags[item.Code] = tagValue;
                    }
                    else
                    {
                        var reference = tagKeywords[4];

                        int startIndex = reference.IndexOf('[');
                        int endIndex = reference.LastIndexOf(']');

                        string tagPath = "";

                        if (startIndex >= 0 && endIndex > startIndex)
                        {
                            string beforeBracket = reference.Substring(0, startIndex);
                            string insideBracket = reference.Substring(startIndex + 1, endIndex - startIndex - 1);
                            reference = beforeBracket;
                            tagPath = insideBracket;
                        }

                        if (!allowedQueries.Contains(reference))
                        {
                            throw new ArgumentException($"{reference} is not implemented reference keyword");
                        }
                        var referenceValue = reference == "customerNo"
                             ? headerModel?.CustomerNo?.ToString()
                             : headerModel?.UserReference;

                        var tagInput = new GetRenderDataTagInputDto
                        {
                            DomainName = tagKeywords[1],
                            EntityName = tagKeywords[2],
                            TagName = tagKeywords[3],
                            Reference = referenceValue,
                        };

                        if (!cachedTagPath.Contains(tagInput))
                        {
                            var tagServiceResponse = await GetRenderValues(tagInput);
                            cachedTagPath.Add(tagInput);
                            if (!tagServiceResponse.IsSuccess)
                            {
                                throw new Exception($"Failed to retrieve data from Tag service! {tagServiceResponse.ErrorMessage}");
                            }

                            if (tagServiceResponse.Data is null || tagServiceResponse.Data.Count == 0)
                            {
                                _logger.Warning("Tag service returned empty data {Data}", tagInput.ToString());
                                continue;
                            }

                            foreach (var kvp in tagServiceResponse.Data)
                            {
                                cachedTagValue[kvp.Key] = kvp.Value;
                            }
                        }
                        //pathMap varsa Metadatada code alanına denk gelecek yoksa item code ile tagdan gelen verinin keyi birebir eşleşmeli.
                        if (!String.IsNullOrEmpty(tagPath) && cachedTagValue.TryGetValue(tagPath, out var tagPathValue))
                        {
                            resultTags[item.Code] = tagPathValue;
                        }
                        else if (cachedTagValue.TryGetValue(item.Code, out var newTagValue))
                        {
                            resultTags[item.Code] = newTagValue;
                        }
                        else
                        {
                            _logger.Warning("Tag service returned empty data {Code} - {Data}", item.Code, item.Data);
                        }
                    }
                }
            }

            return GenericResult<Dictionary<string, string>>.Success(resultTags);
        }
    }
}