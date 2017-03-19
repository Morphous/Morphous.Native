using GalaSoft.MvvmLight;
using Morphous.Native.Models;
using Morphous.Native.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.ViewModels
{
    public interface IContentItemViewModel : INotifyPropertyChanged
    {
        bool Loading { get; }
        IContentItem ContentItem { get; }
    }

    public class ContentItemViewModel : ViewModelBase, IContentItemViewModel
    {
        #region ViewModel
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
        #endregion


        private readonly IContentService _contentService;
        private readonly int _contentItemId;

        public static IContentItemViewModel Create(int contentItemId)
        {
            return new ContentItemViewModel(ContentService.Instance, contentItemId);
        }

        public ContentItemViewModel(IContentService contentService, int contentItemId)
        {
            _contentService = contentService;
            _contentItemId = contentItemId;
            Init();
        }

        private async void Init()
        {
            Loading = true;
            ContentItem = await _contentService.GetContentItem(Mph.BaseUrl, _contentItemId);
            Loading = false;
        }
    }
}
