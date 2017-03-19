using FluentAssertions;
using Morphous.Native.DTOs;
using Morphous.Native.Factories;
using Morphous.Native.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.UnitTests
{
    [TestFixture]
    public class ContentItemFactoryTests
    {
        [Test]
        public void Create_maps_ContentItem_properties_from_DTO()
        {
            var contentItemFactory = new ContentItemFactory();
            var contentItemDto = CreateContentItemDto();

            var contentItem = contentItemFactory.Create(contentItemDto);

            contentItem.Id.Should().Be(10);
            contentItem.ContentType.Should().Be("TestContentType");
            contentItem.DisplayType.Should().Be("TestDisplayType");
        }

        [Test]
        public void Create_adds_alternates()
        {
            var contentItemFactory = new ContentItemFactory();
            var contentItemDto = CreateContentItemDto();
            var expectedAlternates = new List<string>
            {
                "ContentItem_10",
                "ContentItem_TestContentType_TestDisplayType",
                "ContentItem_TestContentType",
                "ContentItem_TestDisplayType",
                "ContentItem",
            };

            var contentItem = contentItemFactory.Create(contentItemDto);

            contentItem.Alternates.ShouldBeEquivalentTo(expectedAlternates);
        }

        [Test]
        public void Create_adds_Element_alternates()
        {
            var contentItemFactory = new ContentItemFactory();
            var contentItemDto = CreateContentItemDto();
            var expectedAlternates = new List<string>
            {
                "TitlePart_10",
                "TitlePart_TestContentType_TestDisplayType_zone1",
                "TitlePart_TestContentType_TestDisplayType",
                "TitlePart_TestContentType",
                "TitlePart_TestDisplayType_zone1",
                "TitlePart_TestDisplayType",
                "TitlePart_zone1",
                "TitlePart",
            };

            var contentItem = contentItemFactory.Create(contentItemDto);

            contentItem.As<TitlePart>().Alternates.ShouldBeEquivalentTo(expectedAlternates);
        }

        [Test]
        public void Create_maps_parent_relationships()
        {
            var contentItemFactory = new ContentItemFactory();
            var contentItemDto = CreateContentItemDto();
            var expectedContentItem = CreateContentItem();

            var contentItem = contentItemFactory.Create(contentItemDto);
            
            foreach (var zone in contentItem.Zones)
            {
                zone.ContentItem.Should().Be(contentItem);
                foreach (var element in zone.Elements)
                {
                    element.Zone.Should().Be(zone);
                }
            }
        }
        


        private static IContentItem CreateContentItem()
        {
            var contentItem = new FakeContentItem { Id = 10, ContentType = "TestContentType", DisplayType = "TestDisplayType" };
            

            var zone1 = new FakeZone { Name = "zone1", ContentItem = contentItem };
            contentItem.Zones.Add(zone1);

            var commonPart = new FakeCommonPart { Zone = zone1, Id = 10, ResourceUrl = "resourceUrl", CreatedDate = DateTime.Parse("2017-02-08T21:18:41.8420836Z"), PublishedDate = DateTime.Parse("2017-02-08T21:18:41.8420836Z") };
            zone1.Elements.Add(commonPart);

            var titlePart = new FakeTitlePart { Zone = zone1, Title = "title1" };
            zone1.Elements.Add(titlePart);


            var zone2 = new FakeZone { Name = "zone2", ContentItem = contentItem };
            contentItem.Zones.Add(zone2);

            var bodyPart = new FakeBodyPart { Zone = zone2, Html = "html" };
            zone2.Elements.Add(bodyPart);

            var booleanField = new FakeBooleanField { Zone = zone2, Value = true };
            zone2.Elements.Add(booleanField);


            return contentItem;
        }
        private static ContentItemDto CreateContentItemDto()
        {
            var contentItem = new ContentItemDto { Id = 10, ContentType = "TestContentType", DisplayType = "TestDisplayType" };
            contentItem.Zones = new List<ZoneDto>();


            var zone1 = new ZoneDto { Name = "zone1" };
            zone1.Elements = new List<ContentElementDto>();
            contentItem.Zones.Add(zone1);

            var commonPart = new CommonPartDto { Id = 10, ResourceUrl = "resourceUrl", CreatedUtc = "2017-02-08T21:18:41.8420836Z", PublishedUtc = "2017-02-08T21:18:41.8420836Z" };
            zone1.Elements.Add(commonPart);

            var titlePart = new TitlePartDto { Title = "title1", Type = "TitlePart" };
            zone1.Elements.Add(titlePart);
            

            var zone2 = new ZoneDto { Name = "zone2" };
            zone2.Elements = new List<ContentElementDto>();
            contentItem.Zones.Add(zone2);

            var bodyPart = new BodyPartDto { Html = "html" };
            zone2.Elements.Add(bodyPart);

            var booleanField = new BooleanFieldDto { Value = true };
            zone2.Elements.Add(booleanField);


            return contentItem;
        }

        private class FakeContentItem : IContentItem
        {
            public int Id { get; set; }
            public string ContentType { get; set; }
            public string DisplayType { get; set; }
            
            public IList<IContentZone> Zones { get; } = new List<IContentZone>();

            public IList<string> Alternates => new List<string>();

            public TElement As<TElement>() where TElement : IContentElement
            {
                throw new NotImplementedException();
            }
        }

        private class FakeZone : IContentZone
        {
            public string Name { get; set; }
            public IList<IContentElement> Elements { get; } = new List<IContentElement>();
            public IContentItem ContentItem { get; set; }
        }

        private class FakeElement : IContentElement
        {
            public string Type { get; set; }
            public IContentZone Zone { get; set; }
            public IList<string> Alternates { get; } = new List<string>();
        }

        private class FakeTitlePart : FakeElement, ITitlePart
        {
            public string Title { get; set; }
        }

        private class FakeCommonPart : FakeElement, ICommonPart
        {
            public DateTime CreatedDate { get; set; }
            public int Id { get; set; }
            public DateTime PublishedDate { get; set; }
            public string ResourceUrl { get; set; }
        }

        private class FakeBodyPart : FakeElement, IBodyPart
        {
            public string Html { get; set; }
        }

        private class FakeBooleanField : FakeElement, IBooleanField
        {
            public string Name { get; set; }
            public bool Value { get; set; }
        }
    }
}
