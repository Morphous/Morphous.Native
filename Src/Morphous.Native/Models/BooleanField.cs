using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IBooleanField : IContentField
    {
        bool Value { get; }
    }

    public class BooleanField : ContentField, IBooleanField
    {
        public bool Value { get; internal set; }
    }
}
