using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataVice;
using PasaBuy.App.Local;
using PasaBuy.App.Controllers.Notice;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAddressPage : ContentPage
    {
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

        public void SubmitPostButton(object sender, EventArgs e)
        {
            string type = string.Empty;
            if (AddressTypePicker.Text == "Business") { type = "business"; }
            if (AddressTypePicker.Text == "Home") { type = "home"; }
            if (AddressTypePicker.Text == "Office") { type = "office"; }

            //Console.WriteLine("type: " + type + " co: " + AddressVar.co + " pv: " + AddressVar.pr + " ct: " + AddressVar.ct + " br: " + AddressVar.br + " st: " + StreetEntry.Text );
            Address.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, type, AddressVar.co, AddressVar.pr, AddressVar.ct, AddressVar.br, StreetEntry.Text, (bool success, string data) =>
            {
                if (success)
                {
                    Navigation.PopModalAsync();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });

        }

        private void CountryPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            AddressVar.co = CountryPicker.SelectedValue.ToString();
            ProvincePicker.Clear();
            CityPicker.Clear();
            BarangayPicker.Clear();
            ProvinceViewModel province = new ProvinceViewModel(CountryPicker.SelectedValue.ToString());
            ProvincePicker.BindingContext = province;
            ProvincePicker.DataSource = province.ProvinceCollection;
            ProvincePicker.DisplayMemberPath = "Name";
            BrgyViewModel.ClearList();
            CityViewModel.ClearList();
        }

        private void BarangayPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            AddressVar.br = BarangayPicker.SelectedValue.ToString();
        }

        private void CityPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            BarangayPicker.Clear();
            AddressVar.ct = CityPicker.SelectedValue.ToString();
            BrgyViewModel brgy = new BrgyViewModel(CityPicker.SelectedValue.ToString());
            BarangayPicker.BindingContext = brgy;
            BarangayPicker.DataSource = brgy.BrgyCollection;
            BarangayPicker.DisplayMemberPath = "Name";
        }

        private void ProvincePicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            CityPicker.Clear();
            BarangayPicker.Clear();
            AddressVar.pr = ProvincePicker.SelectedValue.ToString();
            CityViewModel city = new CityViewModel(ProvincePicker.SelectedValue.ToString());
            CityPicker.BindingContext = city;
            CityPicker.DataSource = city.CityCollection;
            CityPicker.DisplayMemberPath = "Name";
            BrgyViewModel.ClearList();
        }
    }
}