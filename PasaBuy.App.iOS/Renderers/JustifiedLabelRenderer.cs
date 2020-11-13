using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PasaBuy.App.CustomRenderers;
using PasaBuy.App.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(JustifiedLabelRenderer), typeof(JustifiedLabel))]
namespace PasaBuy.App.iOS.Renderers
{
    class JustifiedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TextAlignment = UITextAlignment.Justified;
            }
        }
    }
}