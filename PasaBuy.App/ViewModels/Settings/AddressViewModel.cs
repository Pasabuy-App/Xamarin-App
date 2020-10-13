using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Settings;
using PasaBuy.App.Views.Settings;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
                            string status = address.data[i].status;
                            string types = address.data[i].types;
                            string type = string.Empty;
                            if (types == "home") { type = "Home"; }
                            if (types == "office") { type = "Office"; }
                            if (types == "business") { type = "Business"; }
                            addressDetails.Add(new Address()
                            {
                                isPhone = true,
                                isType = true,
                                isLocation = true,
                                SelectedAddress = address.data[i].ID,
                                AddressType = type,
                                Location = address.data[i].street + " " + address.data[i].brgy + " " + address.data[i].city + " " + address.data[i].province + ", " + address.data[i].country,
                                ContactNumber = address.data[i].contact + " - " + address.data[i].contact_type,

                                isPerson = string.IsNullOrEmpty(address.data[i].contact_person) ? false : true,
                                ContactPerson = string.IsNullOrEmpty(address.data[i].contact_person) ? "No contact person." : address.data[i].contact_person,
                                isPhoto = string.IsNullOrEmpty(address.data[i].preview) ? false : true,
                                AddressPhoto = string.IsNullOrEmpty(address.data[i].preview) ? PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl) : PSAProc.GetUrl(address.data[i].preview)
                            });
                        }
                        /* if (address.data.Length == 0)
                         {
                             addressDetails.Add(new Address()
                             {
                                 isPerson = false,
                                 isPhone = false,
                                 isPhoto = false,
                                 isType = true,
                                 isLocation = false,
                                 SelectedAddress = "0",
                                 AddressType = "No address found."
                             });
                         }
                         else
                         {
                         }*/
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
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
