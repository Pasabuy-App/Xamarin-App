using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
using Plugin.Media;
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
    public partial class AddDocumentPage : ContentPage
    {
        public string filepath = string.Empty;
        public string doc_type_id = string.Empty;
        public AddDocumentPage()
        {
            InitializeComponent();

            DocTypeListViewModel doctypelist = new DocTypeListViewModel();
            DocumentTypePicker.BindingContext = doctypelist;
            DocumentTypePicker.DataSource = doctypelist.DocTypeList;
            DocumentTypePicker.DisplayMemberPath = "Title";
            DocumentTypePicker.SelectedValuePath = "ID";
        }

        void RemoveDocumentImage(object sender, EventArgs args)
        {
            DocumentImage.Source = "document.png";
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
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                CompressionQuality = 30
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            DocumentImage.Source = imageSource;
            filepath = file.Path;
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

            DocumentImage.Source = imageSource;
            filepath = file.Path;

        }


        private void OKModal_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!isRunning.IsRunning)
                {
                    isRunning.IsRunning = true;
                    if (!string.IsNullOrEmpty(filepath) && !string.IsNullOrEmpty(DocumentTypePicker.Text))
                    {
                        if (MasterView.MyType == "mover")
                        {
                            Http.HatidPress.Documents.Instance.Insert_VDocuments(filepath, doc_type_id, "", (bool success, string data) =>
                            {
                                if (success)
                                {
                                    DriverDocumentsViewModel.LoadDocument();
                                    Navigation.PopModalAsync();
                                    isRunning.IsRunning = false ;
                                }
                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                    isRunning.IsRunning = false;
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
                isRunning.IsRunning = false;
            }
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void DocumentTypePicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            doc_type_id = DocumentTypePicker.SelectedValue.ToString();
        }
    }
}