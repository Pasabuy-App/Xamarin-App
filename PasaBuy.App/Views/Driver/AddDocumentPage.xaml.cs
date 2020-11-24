using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.Views.Navigation;
using Plugin.Media;
using System;
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
            if (!isRunning.IsRunning)
            {
                isRunning.IsRunning = true;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    new Controllers.Notice.Alert("Error", "No camera available", "Ok");
                    isRunning.IsRunning = false;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                    CompressionQuality = 30
                });

                if (file == null)
                {
                    isRunning.IsRunning = false;
                    return;
                }

                ImageSource imageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    isRunning.IsRunning = false;
                    return stream;
                });

                DocumentImage.Source = imageSource;
                filepath = file.Path;
                isRunning.IsRunning = false;
            }
        }

        async void BrowseGalleryCommand(object sender, EventArgs args)
        {
            if (!isRunning.IsRunning)
            {
                isRunning.IsRunning = true;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    new Controllers.Notice.Alert("Error", "No camera available", "Ok");
                    isRunning.IsRunning = false;
                }

                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    SaveMetaData = false,
                    CompressionQuality = 30,
                    MaxWidthHeight = 1024
                });

                if (file == null)
                {
                    isRunning.IsRunning = false;
                    return;
                }

                ImageSource imageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    isRunning.IsRunning = false;
                    return stream;
                });

                DocumentImage.Source = imageSource;
                filepath = file.Path;
                isRunning.IsRunning = false;
            }

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
                                    new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                    isRunning.IsRunning = false;
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: HPV2DOC-I1ADP", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2DOC-I1ADP-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2DOC-I1ADP.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2DOC-I1ADP-" + err.ToString());
                }
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