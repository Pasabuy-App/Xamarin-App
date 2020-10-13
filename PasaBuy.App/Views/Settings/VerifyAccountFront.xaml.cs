using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyAccountFront : ContentPage
    {
        private bool isEnable = false;
        public VerifyAccountFront()
        {
            InitializeComponent();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void StartVerification(object sender, EventArgs e)
        {
            if (!isEnable)
            {
                isEnable = true;
                Navigation.PushModalAsync(new TakeIdPhotoPage());
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(1000);
                    isEnable = false;
                });
            }
        }


    }
}