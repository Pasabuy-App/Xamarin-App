using Android.Content;
using PasaBuy.App.Controls;
using PasaBuy.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PasaBuy.App.Controls.CustomTimePicker), typeof(BorderlessTimePickerRenderer))]
namespace PasaBuy.App.Droid.Renderers
{
    public class BorderlessTimePickerRenderer : TimePickerRenderer
    {
        public BorderlessTimePickerRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var element = e.NewElement as CustomTimePicker;
                if (!string.IsNullOrWhiteSpace(element.PlaceHolderText))
                {
                    Control.Text = element.PlaceHolderText;
                    //Control.SetTextColor(Color.FromHex("#a2a3a4").ToAndroid());
                }

                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(50, 0, 0, 0);



            }







        }


    }
}