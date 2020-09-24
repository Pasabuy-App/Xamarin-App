using PasaBuy.App.Controllers.Notice;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductView : ContentPage
    {
        
        public AddProductView()
        {
            //this.BindingContext = new AddProductViewModel();
            InitializeComponent();
            productImage.Source = "Idcard.png";
        }

        void RemoveProductImageCommand(object sender, EventArgs args)
        {
            productImage.Source = "Idcard.png";
        }

        async void OpenCameraCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Ok");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CompressionQuality = 30
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            productImage.Source = imageSource;
        }

        async void BrowseGalleryCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Ok");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                SaveMetaData = false,
                CompressionQuality = 30,
                MaxWidthHeight = 1024
            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            productImage.Source = imageSource;
           
        }

        public async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Scan_Clicked(object sender, EventArgs e)
        {
            //ScanAsync();
        }

        void OnSupplier_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenSupplierAsync();
        }
        void OnUnit_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenSourceUnitAsync();
        }

        void OnCategory_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenCategoryAsync();
        }

        //private AddProductViewModel Context => (AddProductViewModel)this.BindingContext;

        void Handle_SwipeStarted(object sender, Syncfusion.ListView.XForms.SwipeStartedEventArgs e)
        {
            //Context.ItemIndex = -1;
        }

        void Handle_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            //Context.ItemIndex = e.ItemIndex;
        }

        private void Discard(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}