using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core;

namespace amorphie.contract.application.TemplateEngine
{
    public class TemplateEngineService : ITemplateEngineService
    {
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

    }
    public interface ITemplateEngineService
    {
        public Task<string> GetRenderInstance(string instance);//TODO:dapr kullanılacak 
    }

}