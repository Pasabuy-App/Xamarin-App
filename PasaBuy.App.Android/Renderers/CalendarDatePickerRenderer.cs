using Android.Content;
using PasaBuy.App.Controls;
using PasaBuy.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CalenderDatePicker), typeof(CalendarDatePickerRenderer))]
namespace PasaBuy.App.Droid.Renderers
{
    public class CalendarDatePickerRenderer : DatePickerRenderer
    {
        public CalendarDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            if (this.Control != null)
            {
                var customPicker = e.NewElement as CalenderDatePicker;
                this.Control.SetBackground(null);
                Control.Text = (e.NewElement as Controls.CalenderDatePicker).PlaceHolderText;
                Control.Gravity = Android.Views.GravityFlags.CenterVertical;
                Control.SetPadding(20, 0, 0, 0);
                Control.SetHintTextColor(Android.Graphics.Color.ParseColor(customPicker.PlaceHolderColor));


            }
        }

    }
}