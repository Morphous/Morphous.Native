using FluentAssertions;
using Morphous.Native.DTOs;
using Morphous.Native.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.IntegrationTests
{
    [TestFixture]
    public class ContentRequesterTests
    {
        [Test]
        public async Task GetContentItem_returns_ContentItemDto_from_source()
        {
            ContentRequester contentRequester = new ContentRequester();

            var contentItem = await contentRequester.GetContentItem(12, "http://192.168.0.13:98");

            contentItem.ShouldBeEquivalentTo(_testContentItem);
        }

        private readonly ContentItemDto _testContentItem = JsonConvert.DeserializeObject<ContentItemDto>("{\"contentType\":\"Thing\",\"displayType\":\"Detail\",\"zones\":[{\"name\":\"Meta\",\"items\":[{\"type\":\"CommonPart\",\"id\":12,\"resourceUrl\":\"/api/Contents/Item/12?displayType=Detail\",\"createdUtc\":\"2017-02-08T21:18:41.8420836Z\",\"publishedUtc\":\"2017-02-08T21:18:41.9261562Z\"}]},{\"name\":\"Header\",\"items\":[{\"type\":\"TitlePart\",\"title\":\"Firstthing\"}]},{\"name\":\"Content\",\"items\":[{\"type\":\"BodyPart\",\"html\":\"<p>somethingtext</p>\"},{\"type\":\"BooleanField\",\"name\":\"TestBool\",\"value\":false}]}]}");
    }
}
