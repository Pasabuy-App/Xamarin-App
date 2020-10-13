using Xamarin.Forms;

namespace PasaBuy.App.Controls
{
    public class CustomTimePicker : TimePicker
    {
        public string PlaceHolderText { get; set; }

        public static BindableProperty PlaceholderColorProperty =
        BindableProperty.Create(nameof(PlaceHolderColor), typeof(string), typeof(CustomTimePicker), "#4287f5", BindingMode.TwoWay);

        public string PlaceHolderColor
        {
            get { return (string)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }
    }
}
