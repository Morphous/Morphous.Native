using Android.OS;
using Android.Support.V4.App;
using Android.Views;

namespace Morphous.Native.Droid.UI
{
    public class ContentItemFragment : Fragment
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_content_item, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

        }
    }
}