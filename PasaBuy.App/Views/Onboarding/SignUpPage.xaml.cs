using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Onboarding;
using Xamarin.Forms.Internals;
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
            ViewModels.Onboarding.CountryViewModel country = new ViewModels.Onboarding.CountryViewModel();
            CountryPicker.BindingContext = country;
            CountryPicker.DataSource = country.CountryCollection;
            CountryPicker.DisplayMemberPath = "Name";
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

        private void BarangayPicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            AddressVar.br = BarangayPicker.SelectedValue.ToString();
        }
    }
}