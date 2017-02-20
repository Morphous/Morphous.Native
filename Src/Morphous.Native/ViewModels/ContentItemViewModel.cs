using GalaSoft.MvvmLight;
using Morphous.Native.Models;
using Morphous.Native.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.ViewModels
{
    public interface IContentItemViewModel
    {
        bool Loading { get; }
        IContentItem ContentItem { get; }
    }

    public class ContentItemViewModel : ViewModelBase, IContentItemViewModel
    {
        private bool _loading;
        public bool Loading
        {
            get { return _loading; }
            private set { Set(ref _loading, value); }
        }

        private IContentItem _contentItem;
        public IContentItem ContentItem
        {
            get { return _contentItem; }
            private set { Set(ref _contentItem, value); }
        }




        private readonly IContentService _contentService;

        public static IContentItemViewModel Create()
        {
            return new ContentItemViewModel(ContentService.Instance);
        }

        public ContentItemViewModel(IContentService contentService)
        {
            _contentService = contentService;
            Init();
        }

        private async void Init()
        {
            Loading = true;
            ContentItem = await _contentService.GetContentItem("http://192.168.0.13:98", 12);
            Loading = false;
        }
    }
}
