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

namespace PasaBuy.App.ViewModels.Settings
{
    /// <summary>
    /// ViewModel for my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AddressViewModel : BaseViewModel
    {
        #region Properties
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
            /*DataVice.Address.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, (bool success, string data) =>
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
                        addressDetails.Add(new Address
                        {
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
                });*/
            /*
                        this.AddressDetails.Add(new Address
                        {
                            Name = "Juan Dela Cruz",
                            AddressType = "Work",
                            Location = "114 Ridge St NW, San Pedro City, PH 2401",
                            ContactNumber = "(123) 456-7890"
                        });*/
            /*this.AddressDetails = new ObservableCollection<Address>()
            {
                new Address
                {
                    Name = "Juan Dela Cruz",
                    AddressType = "Work",
                    Location = "114 Ridge St NW, San Pedro City, PH 2401",
                    ContactNumber = "(123) 456-7890"
                },
                new Address
                {
                    Name = "Maria Makiling",
                    AddressType = "Home",
                    Location = "100 S 4th St, Binan City, PH 4024",
                    ContactNumber = "(123) 456-7891"
                },
            };*/
        }
        public void LoadData()
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
                        addressDetails.Add(new Address
                        {
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
        public static void RefreshData()
        {
            addressDetails.Clear();
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
                        addressDetails.Add(new Address
                        {
                            Name = "Juan Dela Cruz",
                            AddressType = type,
                            Location = street + " " + brgy + ", " + city + " " + province + ", " + country,
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

        #endregion
        #region Methods
        /// <summary>
        /// Invoked when the edit button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the delete button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void DeleteButtonClicked(object obj)
        {
            // Do something
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
