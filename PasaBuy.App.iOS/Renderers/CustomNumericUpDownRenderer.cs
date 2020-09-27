using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PasaBuy.App.CustomRenderers;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;


[assembly: ExportRenderer(typeof(PasaBuy.App.CustomRenderers.CustomNumericUpDown),
    typeof(PasaBuy.App.iOS.Renderers.CustomNumericNumericUpDownRenderer))]

namespace PasaBuy.App.iOS.Renderers
{
    public class CustomNumericNumericUpDownRenderer : CustomNumericUpDown
    {
       
    }
}