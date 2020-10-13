using System;
using System.Globalization;
using Xamarin.Forms;

namespace PasaBuy.App.Converters
{
    #region ExpanderIconConverter
    public class ExpanderIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                //return ImageSource.FromResource("down.png");
                return ImageSource.FromFile("down.png");

            else
                //return ImageSource.FromResource("up.png");
                return ImageSource.FromFile("up.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
