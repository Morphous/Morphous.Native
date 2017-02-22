using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    public class ElementConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            reader.DateParseHandling = DateParseHandling.None;

            JToken token = JToken.ReadFrom(reader);
            string type = token["type"].Value<string>();

            ContentElementDto result;
            switch (type)
            {
                case "TitlePart":
                    result = new TitlePartDto();
                    break;
                case "CommonPart":
                    result = new CommonPartDto();
                    break;
                case "BodyPart":
                    result = new BodyPartDto();
                    break;
                case "BooleanField":
                    result = new BooleanFieldDto();
                    break;
                default:
                    result = new ContentElementDto();
                    break;
            }

            using (var subReader = token.CreateReader())
            {
                serializer.Populate(subReader, result);
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
