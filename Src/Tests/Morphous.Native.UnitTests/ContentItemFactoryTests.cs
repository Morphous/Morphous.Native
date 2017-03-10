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
        public void Create_maps_all_properties_from_DTO()
        {
            var contentItemFactory = new ContentItemFactory();
            var contentItemDto = CreateContentItemDto();
            var expectedContentItem = CreateContentItem();

            var contentItem = contentItemFactory.Create(contentItemDto);

            contentItem.ShouldBeEquivalentTo(expectedContentItem, options => options.IgnoringCyclicReferences());
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
            var contentItem = new FakeContentItem { Id = 10, ContentType = "test1", DisplayType = "test2" };
            

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
            var contentItem = new ContentItemDto { ContentType = "test1", DisplayType = "test2" };
            contentItem.Zones = new List<ZoneDto>();


            var zone1 = new ZoneDto { Name = "zone1" };
            zone1.Elements = new List<ContentElementDto>();
            contentItem.Zones.Add(zone1);

            var commonPart = new CommonPartDto { Id = 10, ResourceUrl = "resourceUrl", CreatedUtc = "2017-02-08T21:18:41.8420836Z", PublishedUtc = "2017-02-08T21:18:41.8420836Z" };
            zone1.Elements.Add(commonPart);

            var titlePart = new TitlePartDto { Title = "title1" };
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
            public string ContentType { get; set; }
            public string DisplayType { get; set; }

            public int? Id { get; set; }

            public IList<IContentZone> Zones { get; } = new List<IContentZone>();

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

        private class FakeTitlePart : ITitlePart
        {
            public string Title { get; set; }
            public string Type { get; set; }
            public IContentZone Zone { get; set; }
        }

        private class FakeCommonPart : ICommonPart
        {
            public DateTime CreatedDate { get; set; }
            public int Id { get; set; }
            public DateTime PublishedDate { get; set; }
            public string ResourceUrl { get; set; }
            public string Type { get; set; }
            public IContentZone Zone { get; set; }
        }

        private class FakeBodyPart : IBodyPart
        {
            public string Html { get; set; }
            public string Type { get; set; }
            public IContentZone Zone { get; set; }
        }

        private class FakeBooleanField : IBooleanField
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public bool Value { get; set; }
            public IContentZone Zone { get; set; }
        }
    }
}
