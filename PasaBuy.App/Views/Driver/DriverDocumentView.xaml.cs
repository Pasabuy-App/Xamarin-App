using PasaBuy.App.ViewModels.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverDocumentView : ContentPage
    {
        public DriverDocumentView()
        {
            InitializeComponent();
            this.BindingContext = new DriverDocumentsViewModel();
        }
    }
}