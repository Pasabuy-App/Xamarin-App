using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataVice;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Controllers;
using PasaBuy.App.Models.Onboarding;

namespace PasaBuy.App.Views.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyAccountPage : ContentPage
    {
        public VerifyAccountPage()
        {
            InitializeComponent();
        }
    }
}