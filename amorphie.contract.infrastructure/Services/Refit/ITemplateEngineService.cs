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

}