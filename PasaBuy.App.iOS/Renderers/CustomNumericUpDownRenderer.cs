using PasaBuy.App.CustomRenderers;
using Xamarin.Forms;


[assembly: ExportRenderer(typeof(PasaBuy.App.CustomRenderers.CustomNumericUpDown),
    typeof(PasaBuy.App.iOS.Renderers.CustomNumericNumericUpDownRenderer))]

namespace PasaBuy.App.iOS.Renderers
{
    public class CustomNumericNumericUpDownRenderer : CustomNumericUpDown
    {

    }
}