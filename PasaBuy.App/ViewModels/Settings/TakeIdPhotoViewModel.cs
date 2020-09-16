using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.Settings;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Settings
{
    public class TakeIdPhotoViewModel : BaseViewModel
    {
        public TakeIdPhotoViewModel()
        {
            this.UmidCommand = new Command(this.UmidClicked);
            this.DriversLicenseCommand = new Command(this.DLClicked);
            this.PrcCommand = new Command(this.PRCClicked);
            this.OfwCommand = new Command(this.OFWClicked);
            this.VotersCommand = new Command(this.VoterClicked);
            this.PnpCommand = new Command(this.PNPClicked);
            this.SeniorCommand = new Command(this.SeniorClicked);
            this.PostalCommand = new Command(this.PostalClicked);
            this.SchoolCommand = new Command(this.SchoolClicked);
            this.PassportCommand = new Command(this.PassportClicked);
        }
        #region Command
        public Command UmidCommand { get; set; }
        public Command DriversLicenseCommand { get; set; }
        public Command PrcCommand { get; set; }
        public Command OfwCommand { get; set; }
        public Command VotersCommand { get; set; }
        public Command PnpCommand { get; set; }
        public Command SeniorCommand { get; set; }
        public Command PostalCommand { get; set; }
        public Command SchoolCommand { get; set; }
        public Command PassportCommand { get; set; }
        #endregion

        private void PassportClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take Passport Photo";
            VerificationFillPage.idType = "Passport";
            VerificationFillPage.idDocType = "passport";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }
        private void SchoolClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take School ID Photo";
            VerificationFillPage.idType = "School ID";
            VerificationFillPage.idDocType = "school_id";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }
        private void PostalClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take Postal ID Photo";
            VerificationFillPage.idType = "Postal ID";
            VerificationFillPage.idDocType = "postal_id";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }
        private void SeniorClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take Senior Citizen's ID Photo";
            VerificationFillPage.idType = "Senior Citizen's ID";
            VerificationFillPage.idDocType = "senior_id";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }
        private void PNPClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take PNP ID Photo";
            VerificationFillPage.idType = "Philippine National Police ID";
            VerificationFillPage.idDocType = "pnp";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }
        private void VoterClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take Voter's ID Photo";
            VerificationFillPage.idType = "Voter's ID";
            VerificationFillPage.idDocType = "voters_id";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }
        private void OFWClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take OFW e-CARD/OWWA ID Photo";
            VerificationFillPage.idType = "OFW e-CARD/OWWA ID";
            VerificationFillPage.idDocType = "owwa";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }
        private void PRCClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take PRC ID Photo";
            VerificationFillPage.idType = "Professional Identification ID";
            VerificationFillPage.idDocType = "prc";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }
        private void DLClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take Driver's License Photo";
            VerificationFillPage.idType = "Driver's License";
            VerificationFillPage.idDocType = "drivers_license";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
        }

        private void UmidClicked(object sender)
        {
            //IdPhotoPage.myTitle = "Take UMID/SSS ID Photo";
            VerificationFillPage.idType = "UMID/SSS ID";
            VerificationFillPage.idDocType = "sss";
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
            //await CrossMedia.Current.Initialize();
            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    new Alert("Error", "No camera available", "Failed");
            //}

            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    Directory = "DocumentsFolder",
            //    SaveToAlbum = true,
            //    CompressionQuality = 40
            //});

            //if (file == null)
            //    return;

            //ImageSource imageSource = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});

            //var filePath = file.Path;
        }


    }
}
