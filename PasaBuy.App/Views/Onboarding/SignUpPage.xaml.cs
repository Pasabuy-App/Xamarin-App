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

            /*CountryViewModel country = new CountryViewModel();
            CountryPicker.BindingContext = country;
            CountryPicker.DataSource = country.CountryCollection;
            CountryPicker.DisplayMemberPath = "Name";*/

            /*Locations.Instance.Country("datavice", (bool success, string data) =>
            {
                if (success)
                {
                    var countryList = new List<string>();
                    CountryData country = JsonConvert.DeserializeObject<CountryData>(data);
                    for (int i = 0; i < country.data.Length; i++)
                    {
                        string id = country.data[i].ID;
                        string code = country.data[i].code;
                        string name = country.data[i].name;
                        countryList.Add(name);
                    }

                    CountryPicker.ComboBoxSource = countryList;
                }
                else
                {
                    new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                }
            });*/
        }
    }
}