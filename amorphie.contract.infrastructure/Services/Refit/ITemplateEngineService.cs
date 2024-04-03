using amorphie.contract.core.Model.Proxy;
using amorphie.contract.Models.Proxy;
using Refit;

namespace amorphie.contract.infrastructure.Services.Refit;

public interface ITemplateEngineService
{

    /// <summary>
    /// Returns the selected render as string. Select is done with guid which can be found by querying all renders.
    /// </summary>
    /// <param name="renderId"></param>
    /// <returns></returns>
    [Get("/Template/Render/instance/{renderId}")]
    Task<string> GetRender(string renderId);

    /// <summary>
    /// https://base64.guru/converter/decode/pdf or any site that converts base64 to pdf online can be used to test the output.
    /// </summary>
    /// <param name="renderId"></param>
    /// <returns></returns>
    [Get("/Template/Render/instance/pdf/{renderId}")]
    Task<string> GetRenderPdf(string renderId);

    /// <summary>
    /// Used to render an existing template to pdf with render request. Render request is posted from body. Returns stream that has the file.
    /// </summary>
    /// <param name="renderRequestModel"></param>
    /// <returns></returns>
    [Headers("Content-Type: application/json")]
    [Post("/Template/Render/pdf")]
    Task<HttpResponseMessage> SendRenderPdf([Body] TemplateRenderRequestModel renderRequestModel);

    /// <summary>
    /// Used to render an existing template with render request. Render request is posted from body. Returns string.
    /// </summary>
    /// <param name="renderRequestModel"></param>
    /// <returns></returns>
    [Headers("Content-Type: application/json")]
    [Post("/Template/Render")]
    Task<HttpResponseMessage> SendRenderHtml([Body] TemplateRenderRequestModel renderRequestModel);

    /// <summary>
    /// Query parameter is being used like "SQL like" search. Example: looking for template "tr-manuel-payment-after-mail-content" use query "%manuel%" it will return all templates that has "manuel" in its name.
    /// </summary>
    /// <param name="names"></param>
    /// <returns></returns>
    [Get("/Template/Definition/name?query={names}")]
    Task<HttpResponseMessage> GetTemplateDefinitions(string names);
}