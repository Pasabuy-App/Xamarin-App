using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Onboarding;
using PasaBuy.App.ViewModels.Settings;
using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAddressPage : ContentPage
    {
        private bool isEnable = false;
        private string filePath = string.Empty;
        public static string addPath;
        public AddAddressPage()
        {
            InitializeComponent();
            CountryViewModel country = new CountryViewModel();
            CountryPicker.BindingContext = country;
            CountryPicker.DataSource = country.CountryCollection;
            CountryPicker.DisplayMemberPath = "Name";
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SubmitAddressButton(object sender, EventArgs e)
        {
            try
            {
                if (!LoadingIndicator.IsRunning)
                {
                    LoadingIndicator.IsRunning = true;
                    if (!string.IsNullOrWhiteSpace(StreetEntry.Text) && !string.IsNullOrWhiteSpace(ContactEntry.Text) && !string.IsNullOrEmpty(ContactTypePicker.Text))
                    {
                        string type = string.Empty;
                        if (AddressTypePicker.Text == "Business") { type = "business"; }
                        if (AddressTypePicker.Text == "Home") { type = "home"; }
                        if (AddressTypePicker.Text == "Office") { type = "office"; }
                        filePath = !string.IsNullOrEmpty(filePath) ? filePath : "";
                        ContactPersonEntry.Text = !string.IsNullOrWhiteSpace(ContactPersonEntry.Text) ? ContactPersonEntry.Text : "";
                        //Console.WriteLine("." + filePath+ ". ."+ type + ". ." + AddressVar.co + ". ." + AddressVar.pr + ". ." + AddressVar.ct + ". ." + AddressVar.br + ". ." + StreetEntry.Text + ". ." + ContactEntry.Text + ". ." + ContactTypePicker.Text + ". ." + ContactPersonEntry.Text + ".");
                        Http.DataVice.Address.Instance.Insert( filePath, type, AddressVar.co, AddressVar.pr, AddressVar.ct, AddressVar.br, StreetEntry.Text, ContactEntry.Text, ContactTypePicker.Text, ContactPersonEntry.Text, "", "", (bool success, string data) =>
                        {
                            if (success)
                            {
                                if (addPath == "New")
                                {
                                    AddressViewModel.addressDetails.Clear();
                                    AddressViewModel.LoadData();
                                }
                                if (addPath == "Another")
                                {
                                    /* ViewModels.eCommerce.ChangeAddressViewModel._addressList.Clear();
                                     ViewModels.eCommerce.ChangeAddressViewModel.LoadData();*/
                                }
                                Navigation.PopModalAsync();
                                LoadingIndicator.IsRunning = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                LoadingIndicator.IsRunning = false;
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
                LoadingIndicator.IsRunning = false;
            }

        }

        private void CountryPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ProvincePicker.Text))
            {
                ProvincePicker.Clear();
            }
            if (!string.IsNullOrWhiteSpace(CityPicker.Text))
            {
                CityPicker.Clear();
                CityViewModel.ClearList();
            }
            if (!string.IsNullOrWhiteSpace(BarangayPicker.Text))
            {
                BarangayPicker.Clear();
                BrgyViewModel.ClearList();
            }
            AddressVar.co = CountryPicker.SelectedValue.ToString();
            ProvinceViewModel province = new ProvinceViewModel(CountryPicker.SelectedValue.ToString());
            ProvincePicker.BindingContext = province;
            ProvincePicker.DataSource = province.ProvinceCollection;
            ProvincePicker.DisplayMemberPath = "Name";
        }

        private void BarangayPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            AddressVar.br = BarangayPicker.SelectedValue.ToString();
        }

        private void CityPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(BarangayPicker.Text))
            {
                BarangayPicker.Clear();
            }
            AddressVar.ct = CityPicker.SelectedValue.ToString();
            BrgyViewModel brgy = new BrgyViewModel(CityPicker.SelectedValue.ToString());
            BarangayPicker.BindingContext = brgy;
            BarangayPicker.DataSource = brgy.BrgyCollection;
            BarangayPicker.DisplayMemberPath = "Name";
        }

        private void ProvincePicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CityPicker.Text))
            {
                CityPicker.Clear();
            }
            AddressVar.pr = ProvincePicker.SelectedValue.ToString();
            CityViewModel city = new CityViewModel(ProvincePicker.SelectedValue.ToString());
            CityPicker.BindingContext = city;
            CityPicker.DataSource = city.CityCollection;
            CityPicker.DisplayMemberPath = "Name";
            if (!string.IsNullOrWhiteSpace(BarangayPicker.Text))
            {
                BarangayPicker.Clear();
                BrgyViewModel.ClearList();
            }
        }

        async void TakePhoto(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "House-Front",
                Name = "front-house.jpg",
                CompressionQuality = 40,
                AllowCropping = true,
                SaveToAlbum = false,
                SaveMetaData = false,
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            HouseImage.Source = imageSource;
            //var filePath = file.Path;
            filePath = file.Path;


        }

        async void SelectPhoto(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                CompressionQuality = 40,

            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            HouseImage.Source = imageSource;
            //var filePath = file.Path;
            filePath = file.Path;

        }
    }
}