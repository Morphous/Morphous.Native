using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IMultiMediaField : IContentField
    {
        IList<IContentItem> Media { get; }
    }
    
    public class MultiMediaField : ContentField, IMultiMediaField
    {
        public IList<IContentItem> Media { get; } = new List<IContentItem>();
    }
}
