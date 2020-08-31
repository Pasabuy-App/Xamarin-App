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

            RestAPI.Instance.Countries((bool success, string data) =>
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


    }
}