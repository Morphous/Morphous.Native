using Morphous.Native.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Services
{
    public interface IContentRequester
    {
        Task<ContentItemDto> GetContentItem(string url);
        Task<HttpResponseMessage> GetContentItemResponse(string url);
    }

    public class ContentRequester : IContentRequester
    {
        private static IContentRequester _instance;
        public static IContentRequester Instance => _instance ?? (_instance = new ContentRequester());

        public async Task<ContentItemDto> GetContentItem(string url)
        {
            var response = await GetContentItemResponse(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var contentItem = JsonConvert.DeserializeObject<ContentItemDto>(json);
                return contentItem;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public Task<HttpResponseMessage> GetContentItemResponse(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Accept-Alternates", "forms");

            return client.GetAsync(url);
        }
    }
}
