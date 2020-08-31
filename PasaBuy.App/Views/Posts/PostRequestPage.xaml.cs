using PasaBuy.App.CustomRenderers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Posts
{   
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostRequestPage : ContentPage
    {

        public PostRequestPage()
        {
            InitializeComponent();

         

            vehicleType.ItemsSource = new List<string>
            {
                "bicycle",
                "motorcycle",
                "four-wheeled vehicle"
            };

            postStyle.ItemsSource = new List<string>
            {
                "option 1",
                "option 2"

            };

        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        //async void OnImageButtonClicked(object sender, System.EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        await DisplayAlert("Not Supported", "Your current device does not currently suppport this functionality", "Ok");
        //        return;
        //    }

        //    var mediaOptions = new PickMediaOptions()
        //    {
        //        PhotoSize = PhotoSize.Medium
        //    };

        //    var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);


        //    if (selectedImage == null)
        //    {
        //        await DisplayAlert("Error", "Could not get the image, please try again", "Ok");
        //        return;
        //    }

        //    selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

        //}



    }
}