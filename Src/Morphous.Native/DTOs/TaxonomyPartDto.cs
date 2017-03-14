using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.DTOs
{
    [DataContract]
    public class TaxonomyPartDto : ContentPartDto
    {
        [DataMember(Name = "terms")]
        public IList<TaxonomyItemDto> Terms { get; set; }
    }

    [DataContract]
    public class TaxonomyItemDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "displayUrl")]
        public string DisplayUrl { get; set; }

        [DataMember(Name = "terms")]
        public IList<TaxonomyItemDto> Terms { get; set; }
    }
}