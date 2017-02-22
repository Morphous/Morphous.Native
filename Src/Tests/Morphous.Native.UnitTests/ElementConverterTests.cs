using FluentAssertions;
using Morphous.Native.DTOs;
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
    public class ElementConverterTests
    {
        [Test]
        public void Converts_CommonPart_Json_to_CommonPartDto()
        {
            var commentPartJson = "{\"type\":\"CommonPart\",\"id\":12,\"resourceUrl\":\"/api/Contents/Item/12?displayType=Detail\",\"createdUtc\":\"2017-02-08T21:18:41.8420836Z\",\"publishedUtc\":\"2017-02-08T21:18:41.9261562Z\"}";

            var elementDto = JsonConvert.DeserializeObject<ContentElementDto>(commentPartJson);

            elementDto.Should().BeOfType<CommonPartDto>();
            var commonPartDto = elementDto as CommonPartDto;
            commonPartDto.Id.Should().Be(12);
            commonPartDto.ResourceUrl.Should().Be("/api/Contents/Item/12?displayType=Detail");
            commonPartDto.CreatedUtc.Should().Be("2017-02-08T21:18:41.8420836Z");
            commonPartDto.PublishedUtc.Should().Be("2017-02-08T21:18:41.9261562Z");
        }

        [Test]
        public void Converts_TitlePart_Json_to_TitlePartDto()
        {            
            var titlePartJson = "{\"type\":\"TitlePart\",\"title\":\"First thing\"}";

            var elementDto = JsonConvert.DeserializeObject<ContentElementDto>(titlePartJson);

            elementDto.Should().BeOfType<TitlePartDto>();
            var titlePartDto = elementDto as TitlePartDto;
            titlePartDto.Title.Should().Be("First thing");
        }

        [Test]
        public void Converts_BodyPart_Json_to_BodyPartDto()
        {
            var json = "{\"type\":\"BodyPart\",\"html\":\"<p>some thing text</p>\"}";

            var elementDto = JsonConvert.DeserializeObject<ContentElementDto>(json);

            elementDto.Should().BeOfType<BodyPartDto>();
            var bodyPartDto = elementDto as BodyPartDto;
            bodyPartDto.Html.Should().Be("<p>some thing text</p>");
        }

        [Test]
        public void Converts_BooleanField_Json_to_BooleanFieldDto()
        {
            var json = "{\"type\":\"BooleanField\",\"name\":\"TestBool\",\"value\":true}";

            var elementDto = JsonConvert.DeserializeObject<ContentElementDto>(json);

            elementDto.Should().BeOfType<BooleanFieldDto>();
            var booleanFieldDto = elementDto as BooleanFieldDto;
            booleanFieldDto.Name.Should().Be("TestBool");
            booleanFieldDto.Value = true;
        }
    }
}
