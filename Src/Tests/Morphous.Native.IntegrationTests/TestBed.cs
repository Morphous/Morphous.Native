using Morphous.Native.Factories;
using Morphous.Native.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.IntegrationTests
{
    public class TestBed
    {
        [Test]
        public async Task Run_TestBed()
        {
            var contentRequester = new ContentRequester();
            var contentItemFactory = new ContentItemFactory();

            var contentItemDto = await contentRequester.GetContentItem("http://localhost:96/api/Contents/Item/12");
            var contentItem = contentItemFactory.Create(contentItemDto);
        }
    }
}
