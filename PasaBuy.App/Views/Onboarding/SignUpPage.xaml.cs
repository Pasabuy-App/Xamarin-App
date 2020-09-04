using PasaBuy.App.Models.Locations;
using PasaBuy.App.ViewModels.Location;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.XForms.Cards;
using System;
using System.Collections.Generic;
using System.Drawing;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using DataVice;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using Newtonsoft.Json;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Onboarding;

namespace PasaBuy.App.Views.Onboarding
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage" /> class.
        /// </summary>
        public SignUpPage()
        {
            InitializeComponent();
            ViewModels.Onboarding.CountryViewModel country = new ViewModels.Onboarding.CountryViewModel();
            CountryPicker.BindingContext = country;
            CountryPicker.DataSource = country.CountryCollection;
            CountryPicker.DisplayMemberPath = "Name";
        }

        private void CountryPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            string val = CountryPicker.SelectedValue.ToString();
            AddressVar.co = val;
            if (val != "")
            {
                ProvincePicker.Clear();
                CityPicker.Clear();
                BarangayPicker.Clear();
                ProvinceViewModel province = new ProvinceViewModel(val);
                ProvincePicker.BindingContext = province;
                ProvincePicker.DataSource = province.ProvinceCollection;
                ProvincePicker.DisplayMemberPath = "Name";
            }
        }

        private void ProvincePicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            string val = ProvincePicker.SelectedValue.ToString();
            AddressVar.pr = val;
            if (val != "")
            {
                CityPicker.Clear();
                BarangayPicker.Clear();
                CityViewModel city = new CityViewModel(val);
                CityPicker.BindingContext = city;
                CityPicker.DataSource = city.CityCollection;
                CityPicker.DisplayMemberPath = "Name";
            }
        }

        private void CityPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            string val = CityPicker.SelectedValue.ToString();
            Console.WriteLine(val + "city");
            AddressVar.ct = val;
            if (val != "")
            {
                BarangayPicker.Clear();
                BrgyViewModel brgy = new BrgyViewModel(val);
                BarangayPicker.BindingContext = brgy;
                BarangayPicker.DataSource = brgy.BrgyCollection;
                BarangayPicker.DisplayMemberPath = "Name";
            }

        }

        private void BarangayPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            string val = BarangayPicker.SelectedValue.ToString();
            Console.WriteLine(val + "brgy");
            AddressVar.br = val;
        }
    }
}   