using Dapr.Client;

namespace amorphie.contract.infrastructure.Extensions;

public static class DaprCacheExtensions
{

    public static async Task<TModel> CacheReadAsync<TModel>(this DaprClient daprClient, string cacheStoreName, string cacheKey)
    {
        return await daprClient.GetStateAsync<TModel>(cacheStoreName, cacheKey);
    }

    public static async Task<TModel> CacheGetOrSetAsync<TModel>(this DaprClient daprClient, Func<Task<TModel>> readAction, string cacheStoreName, string cacheKey, int TTLSeconds)
    {
        TModel result = await daprClient.GetStateAsync<TModel>(cacheStoreName, cacheKey);

        if (result is null)
        {
            Dictionary<string, string> metadata = new();
            if (TTLSeconds > 0)
            {
                metadata = new Dictionary<string, string>() { { "ttlInSeconds", TTLSeconds.ToString() } };
            }

            result = await readAction();
            await daprClient.SaveStateAsync<TModel>(cacheStoreName, cacheKey, result, metadata: metadata);
        }

        return result;
    }

    public static async Task CacheDeleteAsync(this DaprClient daprClient, string cacheStoreName, string cacheKey)
    {
        await daprClient.DeleteStateAsync(cacheStoreName, cacheKey);

    }

}
