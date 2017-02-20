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
        public IContentItem Create(ContentItemDto contentItemDto)
        {
            var contentItem = new ContentItem();
            contentItem.ContentType = contentItemDto.ContentType;
            contentItem.DisplayType = contentItemDto.DisplayType;
            
            foreach (var zoneDto in contentItemDto.Zones)
            {
                var zone = CreateZone(zoneDto);
                contentItem.Zones.Add(zone);
            }

            return contentItem;
        }

        private IZone CreateZone(ZoneDto zoneDto)
        {
            var zone = new Zone();
            zone.Name = zoneDto.Name;

            foreach (var elementDto in zoneDto.Elements)
            {
                var element = CreateElement(elementDto);
                zone.Elements.Add(element);
            }

            return zone;
        }

        private IContentElement CreateElement(ContentElementDto elementDto)
        {
            var element = new ContentElement();
            element.Type = elementDto.Type;

            return element;
        }
    }
}
