﻿using CoreGraphics;
using PasaBuy.App.CustomRenderers;
using PasaBuy.App.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedPicker), typeof(RoundedPickerRendererIos))]
namespace PasaBuy.App.iOS.Renderers
{
    public class RoundedPickerRendererIos : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Layer.CornerRadius = 15;
                Control.Layer.BorderWidth = 2f;
                Control.Layer.BorderColor = Color.Orange.ToCGColor();
                Control.Layer.BackgroundColor = Color.White.ToCGColor();

                Control.LeftView = new UIKit.UIView(new CGRect(0, 0, 10, 0));
                Control.LeftViewMode = UIKit.UITextFieldViewMode.Always;
            }
        }
    }
}