using Android.Content;
using PasaBuy.App.Controls;
using PasaBuy.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PasaBuy.App.Controls.BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
namespace PasaBuy.App.Droid.Renderers
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
        public BorderlessDatePickerRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var element = e.NewElement as BorderlessDatePicker;
                if (!string.IsNullOrWhiteSpace(element.Placeholder))
                {
                    Control.Text = element.Placeholder;
                    //Control.SetTextColor(Color.FromHex("#a2a3a4").ToAndroid());
                }

                if (Control != null)
                {
                    Control.SetTextColor(Android.Graphics.Color.Rgb(0, 0, 0));
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