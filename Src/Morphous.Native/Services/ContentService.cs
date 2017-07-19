using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Factories;
using Morphous.Native.Messges;
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
        private readonly IMessenger _messenger;

        private static IContentService _instance;
        public static IContentService Instance => _instance ?? (_instance = new ContentService(ContentRequester.Instance, new ContentItemFactory(), Messenger.Default));

        public ContentService(
            IContentRequester contentRequester,
            IContentItemFactory contentItemFactory,
            IMessenger messenger)
        {
            _contentRequester = contentRequester;
            _contentItemFactory = contentItemFactory;
            _messenger = messenger;
        }

        public async Task<IContentItem> GetContentItem(string baseUrl, string resourceUrl)
        {
            var contentItemDto = await _contentRequester.GetContentItem($"{baseUrl}{resourceUrl}");
            var contentItem = _contentItemFactory.Create(contentItemDto);
            _messenger.Send(new ContentItemCreatedMessage(contentItem));
            return contentItem;
        }

        public Task<IContentItem> GetContentItem(string baseUrl, int id)
        {
            return GetContentItem(baseUrl, $"/api/Contents/Item/{id}");
        }
    }
}
