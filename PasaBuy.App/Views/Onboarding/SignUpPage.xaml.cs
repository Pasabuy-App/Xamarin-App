using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

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

            genderPicker.ItemsSource = new List<string>
            {
                "Male",
                "Female"
            };

            RestAPI.Instance.Countries("datavice", (bool success, string data) =>
            {
                if (success)
                {
                    var countryList = new List<string>();
                    Country country = JsonConvert.DeserializeObject<Country>(data);
                    for (int i = 0; i < country.data.Length; i++)
                    {
                        string id = country.data[i].ID;
                        string code = country.data[i].code;
                        string name = country.data[i].name;
                        countryList.Add(name);
                    }
                    countryPicker.ItemsSource = countryList;
                }
                else
                {
                    new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                }
            });

        }

        private void countryPicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int selectedIndex = countryPicker.SelectedIndex;

            if (selectedIndex != -1)
            {
                RestAPI.Instance.Provinces("datavice", "PH", (bool success, string data) =>
                {
                    if (success)
                    {
                        var provinceList = new List<string>();
                        Province province = JsonConvert.DeserializeObject<Province>(data);
                        for (int i = 0; i < province.data.Length; i++)
                        {
                            string code = province.data[i].code;
                            string name = province.data[i].name;
                            provinceList.Add(name);
                        }
                        provincePicker.ItemsSource = provinceList;
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
        }

        private void provincePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = countryPicker.SelectedIndex;

            if (selectedIndex != -1)
            {
                RestAPI.Instance.Cities("datavice", "1376", (bool success, string data) =>
                {
                    if (success)
                    {
                        var cityList = new List<string>();
                        City city = JsonConvert.DeserializeObject<City>(data);
                        for (int i = 0; i < city.data.Length; i++)
                        {
                            string code = city.data[i].code;
                            string name = city.data[i].name;
                            cityList.Add(name);
                        }
                        cityPicker.ItemsSource = cityList;
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
        }

        private void cityPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = countryPicker.SelectedIndex;

            if (selectedIndex != -1)
            {
                RestAPI.Instance.Barangay("datavice", "137603", (bool success, string data) =>
                {
                    if (success)
                    {
                        var brgyList = new List<string>();
                        Barangay brgy = JsonConvert.DeserializeObject<Barangay>(data);
                        for (int i = 0; i < brgy.data.Length; i++)
                        {
                            string code = brgy.data[i].code;
                            string name = brgy.data[i].name;
                            brgyList.Add(name);
                        }
                        barangayPicker.ItemsSource = brgyList;
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
        }


    }
}