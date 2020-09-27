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
using PasaBuy.App.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PasaBuy.App.Controls;
using PasaBuy.App.Droid.Renderers;


[assembly: ExportRenderer(typeof(CustomNumericUpDown), typeof(CustomNumericUpDownRenderer))]
namespace PasaBuy.App.Droid.Renderers
{
    public class CustomNumericUpDownRenderer : CustomNumericUpDown
    {
        
       
    }
}