using PasaBuy.App.CustomRenderers;
using PasaBuy.App.Droid.Renderers;
using Xamarin.Forms;


[assembly: ExportRenderer(typeof(CustomNumericUpDown), typeof(CustomNumericUpDownRenderer))]
namespace PasaBuy.App.Droid.Renderers
{
    public class CustomNumericUpDownRenderer : CustomNumericUpDown
    {


    }
}