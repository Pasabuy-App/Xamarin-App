using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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

                if(Control != null)
                {
                    Control.SetTextColor(Android.Graphics.Color.Rgb(0, 0, 0));
                    Control.SetHintTextColor(Android.Graphics.Color.Rgb(182, 182, 182));
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