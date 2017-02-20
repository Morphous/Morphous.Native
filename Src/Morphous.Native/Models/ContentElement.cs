using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphous.Native.Models
{
    public interface IContentElement
    {
        string Type { get; }
    }

    public class ContentElement : ObservableObject, IContentElement
    {
        public string Type { get; internal set; }
    }
}
