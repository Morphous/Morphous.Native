using Morphous.Native.Factories;
using Morphous.Native.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Services
{
    public interface IContentService
    {
        Task<IContentItem> GetContentItem(string baseUrl, string resourceUrl);
        Task<IContentItem> GetContentItem(string baseUrl, int id);
    }
    public class ContentService : IContentService
    {
        private readonly IContentRequester _contentRequester;
        private readonly IContentItemFactory _contentItemFactory;

        private static IContentService _instance;
        public static IContentService Instance => _instance ?? (_instance = new ContentService(ContentRequester.Instance, new ContentItemFactory()));

        public ContentService(
            IContentRequester contentRequester,
            IContentItemFactory contentItemFactory)
        {
            _contentRequester = contentRequester;
            _contentItemFactory = contentItemFactory;
        }

        public async Task<IContentItem> GetContentItem(string baseUrl, string resourceUrl)
        {
            var contentItemDto = await _contentRequester.GetContentItem($"{baseUrl}{resourceUrl}");
            return _contentItemFactory.Create(contentItemDto);
        }

        public Task<IContentItem> GetContentItem(string baseUrl, int id)
        {
            return GetContentItem($"/api/Contents/Item/{id}", baseUrl);
        }
    }
}
