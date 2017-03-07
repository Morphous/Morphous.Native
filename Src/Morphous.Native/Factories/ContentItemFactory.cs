using Morphous.Native.DTOs;
using Morphous.Native.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Factories
{
    public interface IContentItemFactory
    {
        IContentItem Create(ContentItemDto contentItemDto);
    }

    public class ContentItemFactory : IContentItemFactory
    {
        // ContentItem
        public IContentItem Create(ContentItemDto contentItemDto)
        {
            var contentItem = new ContentItem();
            contentItem.ContentType = contentItemDto.ContentType;
            contentItem.DisplayType = contentItemDto.DisplayType;
            
            foreach (var zoneDto in contentItemDto.Zones)
            {
                var zone = CreateZone(zoneDto, contentItem);
                contentItem.Zones.Add(zone);
            }

            return contentItem;
        }

        // Zone
        private ContentZone CreateZone(ZoneDto zoneDto, IContentItem contentItem)
        {
            var zone = new ContentZone();
            zone.Name = zoneDto.Name;
            zone.ContentItem = contentItem;

            foreach (var elementDto in zoneDto.Elements)
            {
                var element = CreateElement(elementDto, zone);
                zone.Elements.Add(element);
            }

            return zone;
        }

        // Elements
        private ContentElement CreateElement(ContentElementDto elementDto, IContentZone zone)
        {
            ContentElement element;

            if (elementDto is ContentPartDto)
            {
                element = CreatePart(elementDto as ContentPartDto);
            }
            else if (elementDto is ContentFieldDto)
            {
                element = CreateField(elementDto as ContentFieldDto);
            }
            else
            {
                element = new ContentElement();
            }

            element.Type = elementDto.Type;
            element.Zone = zone;

            return element;
        }

        // Parts
        private ContentElement CreatePart(ContentPartDto contentPartDto)
        {
            ContentPart contentPart;

            if (contentPartDto is CommonPartDto)
            {
                contentPart = CreateCommonPart(contentPartDto as CommonPartDto);
            }
            else if (contentPartDto is TitlePartDto)
            {
                var titlePartDto = contentPartDto as TitlePartDto;
                contentPart = new TitlePart { Title = titlePartDto.Title };
            }
            else if (contentPartDto is BodyPartDto)
            {
                var bodyPartDto = contentPartDto as BodyPartDto;
                contentPart = new BodyPart { Html = bodyPartDto.Html};
            }
            else
            {
                throw new NotSupportedException("No mapping from element dto " + contentPartDto.Type + " to element model.");
            }

            return contentPart;
        }

        private ContentPart CreateCommonPart(CommonPartDto commonPartDto)
        {
            var commonPart = new CommonPart();
            commonPart.Id = commonPartDto.Id;
            commonPart.ResourceUrl = commonPartDto.ResourceUrl;
            commonPart.CreatedDate = DateTime.Parse(commonPartDto.CreatedUtc);
            commonPart.PublishedDate = DateTime.Parse(commonPartDto.PublishedUtc);

            return commonPart;
        }

        // Fields
        private ContentElement CreateField(ContentFieldDto contentFieldDto)
        {
            ContentField contentField;

            if (contentFieldDto is BooleanFieldDto)
            {
                var booleanFieldDto = contentFieldDto as BooleanFieldDto;
                contentField = new BooleanField { Value = booleanFieldDto.Value };
            }
            else
            {
                throw new NotSupportedException("No mapping from element dto " + contentFieldDto.Type + " to element model.");
            }

            contentField.Name = contentFieldDto.Name;

            return contentField;
        }
    }
}
