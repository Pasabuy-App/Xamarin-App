using Xamarin.Forms;
using PasaBuy.App.Models.Settings;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;
using PasaBuy.App.Views.Master;
using PasaBuy.App.Views.Settings;
using PasaBuy.App.Views;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using Newtonsoft.Json;
using System;

namespace PasaBuy.App.ViewModels.Settings
{
    /// <summary>
    /// ViewModel for my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AddressViewModel : BaseViewModel
    {
        #region Fields

        public static string addressID = string.Empty;

        public static ObservableCollection<Address> addressDetails;
        public ObservableCollection<Address> AddressDetails { get { return addressDetails; } set { addressDetails = value; this.NotifyPropertyChanged(); } }

        #endregion

        #region Constructor
        public AddressViewModel()
        {
            this.EditCommand = new Command(this.EditButtonClicked);
            this.DeleteCommand = new Command(this.DeleteButtonClicked);
            this.AddCardCommand = new Command(this.AddCardButtonClicked);

            addressDetails = new ObservableCollection<Address>();
            LoadData();
        }
        public static void LoadData()
        {
            try
            {
                DataVice.Address.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, (bool success, string data) =>
                {
                    if (success)
                    {
                        AddressData address = JsonConvert.DeserializeObject<AddressData>(data);
                        for (int i = 0; i < address.data.Length; i++)
                        {
                            string id = address.data[i].id;
                            string types = address.data[i].types;
                            string status = address.data[i].status;
                            string street = address.data[i].street;
                            string brgy = address.data[i].brgy;
                            string city = address.data[i].city;
                            string province = address.data[i].province;
                            string country = address.data[i].country;
                            string type = string.Empty;
                            if (types == "home") { type = "Home"; }
                            if (types == "office") { type = "Office"; }
                            if (types == "business") { type = "Business"; }
                            addressDetails.Add(new Address()
                            {
                                SelectedAddress = id,
                                Name = "Juan Dela Cruz",
                                AddressType = type,
                                Location = street + " " + brgy + " " + city + " " + province + ", " + country,
                                ContactNumber = "(123) 456-7890"
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: 20440.", "OK");
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Invoked when the edit button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditButtonClicked(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new EditAddressPage()));
        }

        /// <summary>
        /// Invoked when the delete button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void DeleteButtonClicked(object obj)
        {
            //SelectedAddressvar answer = new ConfirmAlert("Delete Address", "Are you sure you want to delete this address", "Ok", "Cancel");
        }

        /// <summary>
        /// Invoked when the add card button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void AddCardButtonClicked(object obj)
        {
            // Do something
        }
        #endregion

        #region Command
        /// <summary>
        /// Gets or sets the command is executed when the edit button is clicked.
        /// </summary>
        public Command EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the delete button is clicked.
        /// </summary>
        public Command DeleteCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the add card button is clicked.
        /// </summary>
        public Command AddCardCommand { get; set; }

        #endregion
    }
}
