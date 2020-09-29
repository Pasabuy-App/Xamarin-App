using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.ViewModels.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAddressView : ContentPage
    {
        public static string adid = "0";
        public AddAddressView()
        {
            InitializeComponent();
            ViewModels.Onboarding.CountryViewModel country = new ViewModels.Onboarding.CountryViewModel();
            AddressCountrys.BindingContext = country;
            AddressCountrys.DataSource = country.CountryCollection;
            AddressCountrys.DisplayMemberPath = "Name";
            if (adid == "0")
            {
                Title = "Add Address";
                //AddressTypes.Text = string.Empty;
                AddressBarangays.Text = string.Empty;
                AddressProvinces.Text = string.Empty;
                AddressCountrys.Text = string.Empty;
                AddressStreets.Text = string.Empty;
                AddressCitys.Text = string.Empty;
            }
            else
            {
                try
                {
                    Title = "Edit Address";
                    TindaPress.Address.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", adid, "1", (bool success, string data) =>
                    {
                        if (success)
                        {
                            SettingsAddressData add = JsonConvert.DeserializeObject<SettingsAddressData>(data);
                            for (int i = 0; i < add.data.Length; i++)
                            {
                                //AddressTypes.Text = add.data[i].type;
                                AddressBarangays.Text = add.data[i].brgy;
                                AddressProvinces.Text = add.data[i].province;
                                AddressCountrys.Text = add.data[i].country;
                                AddressStreets.Text = add.data[i].street;
                                AddressCitys.Text = add.data[i].city;
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                        }
                    });
                }
                catch (Exception ex)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
                }
            }
        }
        private void Discard(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void Add(object sender, EventArgs e)
        {
            try
            {
                //AddressType.HasError = AddressTypes.Text == null || string.IsNullOrWhiteSpace(AddressTypes.Text) ? true : false;
                AddressBarangay.HasError = AddressBarangays.Text == null || string.IsNullOrWhiteSpace(AddressBarangays.Text) ? true : false;
                AddressCity.HasError = AddressCitys.Text == null || string.IsNullOrWhiteSpace(AddressCitys.Text) ? true : false;
                AddressProvince.HasError = AddressProvinces.Text == null || string.IsNullOrWhiteSpace(AddressProvinces.Text) ? true : false;
                AddressCountry.HasError = AddressCountrys.Text == null || string.IsNullOrWhiteSpace(AddressCountrys.Text) ? true : false;
                AddressStreet.HasError = AddressStreets.Text == null || string.IsNullOrWhiteSpace(AddressStreets.Text) ? true : false;
                if (AddressStreet.HasError == false && AddressCountry.HasError == false && AddressProvince.HasError == false && AddressCity.HasError == false && AddressBarangay.HasError == false)
                {
                    //new Alert("Address", "Country: " + AddressVar.co + " " + AddressVar.pr + " " + AddressVar.ct + " " + AddressVar.br, "OK");
                    string type = string.Empty;
                    /*if (AddressTypes.Text == "Business") { type = "business"; }
                    if (AddressTypes.Text == "Office") { type = "office"; }*/
                        if (adid == "0")
                        {
                            TindaPress.Address.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "business", AddressStreets.Text, AddressVar.br, AddressVar.ct, AddressVar.pr, AddressVar.co, "", "", (bool success, string data) =>
                            {
                                if (success)
                                {
                                    SettingsAddressViewModel.addressList.Clear();
                                    SettingsAddressViewModel.LoadData();
                                    Navigation.PopAsync();
                                }
                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                }
                            });
                        }
                        else
                        {
                            TindaPress.Address.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, adid, AddressStreets.Text, AddressVar.br, AddressVar.ct, AddressVar.pr, AddressVar.co, (bool success, string data) =>
                            {
                                if (success)
                                {
                                    SettingsAddressViewModel.addressList.Clear();
                                    SettingsAddressViewModel.LoadData();
                                    Navigation.PopAsync();
                                    adid = "0";
                                }
                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                }
                            });
                        }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }

        private void AddressCountrys_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            AddressVar.co = AddressCountrys.SelectedValue.ToString();
            AddressProvinces.Clear();
            AddressCitys.Clear();
            AddressBarangays.Clear();
            ProvinceViewModel province = new ProvinceViewModel(AddressCountrys.SelectedValue.ToString());
            AddressProvinces.BindingContext = province;
            AddressProvinces.DataSource = province.ProvinceCollection;
            AddressProvinces.DisplayMemberPath = "Name";
            BrgyViewModel.ClearList();
            CityViewModel.ClearList();
        }

        private void AddressProvinces_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            AddressCitys.Clear();
            AddressBarangays.Clear();
            AddressVar.pr = AddressProvinces.SelectedValue.ToString();
            CityViewModel city = new CityViewModel(AddressProvinces.SelectedValue.ToString());
            AddressCitys.BindingContext = city;
            AddressCitys.DataSource = city.CityCollection;
            AddressCitys.DisplayMemberPath = "Name";
            BrgyViewModel.ClearList();
        }

        private void AddressCitys_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            AddressBarangays.Clear();
            AddressVar.ct = AddressCitys.SelectedValue.ToString();
            BrgyViewModel brgy = new BrgyViewModel(AddressCitys.SelectedValue.ToString());
            AddressBarangays.BindingContext = brgy;
            AddressBarangays.DataSource = brgy.BrgyCollection;
            AddressBarangays.DisplayMemberPath = "Name";
        }

        private void AddressBarangays_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            AddressVar.br = AddressBarangays.SelectedValue.ToString();
        }
    }
}