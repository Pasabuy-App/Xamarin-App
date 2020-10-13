using System;
using Xamarin.Forms;

namespace PasaBuy.App.Controls
{
    public class BorderlessDatePicker : DatePicker
    {
        internal void SetTextColor(object p)
        {
            throw new NotImplementedException();
        }

        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string), declaringType: typeof(BorderlessDatePicker), defaultValue: default(string));
        public string Placeholder { get; set; }

    }
}
