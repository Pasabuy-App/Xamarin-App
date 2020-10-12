using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PasaBuy.App.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(PasaBuy.App.Controls.CalenderDatePicker), typeof(CalendarDatePickerRenderer))]
namespace PasaBuy.App.iOS.Renderers
{
    public class CalendarDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (this.Control != null)
            {
                this.Control.Text = (e.NewElement as Controls.CalenderDatePicker).PlaceHolderText;
                this.Control.TextColor = new UIColor(140 / 255, 140 / 255, 140 / 255, 1.0f);
                this.Control.BorderStyle = UITextBorderStyle.None;
                this.Control.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            }
        }

    }
}