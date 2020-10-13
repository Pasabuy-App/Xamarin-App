using Xamarin.Forms;

namespace PasaBuy.App.Views.Feeds.Templates
{
    public class AspecRatioContainer : ContentView
    {
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(widthConstraint, widthConstraint * this.AspectRatio));
        }

        public static BindableProperty AspectRatioProperty = BindableProperty.Create(nameof(AspectRatio), typeof(double), typeof(AspecRatioContainer), (double)1);

        public double AspectRatio
        {
            get
            {
                return (double)this.GetValue(AspectRatioProperty);
            }

            set
            {
                this.SetValue(AspectRatioProperty, value);
            }

        }
    }
}
