using FluentAssertions;
using Morphous.Native.DTOs;
using Morphous.Native.Factories;
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

            var contentItem = contentItemFactory.Create(_testContentItemDto);

            contentItem.ShouldBeEquivalentTo(_testContentItemDto);
        }

        private readonly ContentItemDto _testContentItemDto = JsonConvert.DeserializeObject<ContentItemDto>("{\"contentType\":\"Thing\",\"displayType\":\"Detail\",\"zones\":[{\"name\":\"Meta\",\"items\":[{\"type\":\"CommonPart\",\"id\":12,\"resourceUrl\":\"/api/Contents/Item/12?displayType=Detail\",\"createdUtc\":\"2017-02-08T21:18:41.8420836Z\",\"publishedUtc\":\"2017-02-08T21:18:41.9261562Z\"}]},{\"name\":\"Header\",\"items\":[{\"type\":\"TitlePart\",\"title\":\"Firstthing\"}]},{\"name\":\"Content\",\"items\":[{\"type\":\"BodyPart\",\"html\":\"<p>somethingtext</p>\"},{\"type\":\"BooleanField\",\"name\":\"TestBool\",\"value\":false}]}]}");
    }
}
