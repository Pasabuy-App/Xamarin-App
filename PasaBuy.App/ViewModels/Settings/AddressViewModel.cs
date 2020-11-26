using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Locations;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Models.Settings;
using PasaBuy.App.Views.Settings;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
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

        public static ObservableCollection<CountryData> _countryList;

        public ObservableCollection<CountryData> CountryList
        {
            get
            {
                return _countryList;
            }
            set
            {
                _countryList = value;
                this.NotifyPropertyChanged();
            }
        }

        public static ObservableCollection<Address> addressDetails;
        public ObservableCollection<Address> AddressDetails 
        { 
            get
            {
                return addressDetails; 
            } 
            set 
            { 
                addressDetails = value; 
                this.NotifyPropertyChanged(); 
            }
        }

        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ICommand RefreshCommand { protected set; get; }
        public bool _isRunning = false;
        public bool isRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public AddressViewModel()
        {
            this.EditCommand = new Command(this.EditButtonClicked);
            this.DeleteCommand = new Command(this.DeleteButtonClicked);
            this.AddCardCommand = new Command(this.AddCardButtonClicked);
            _countryList = new ObservableCollection<CountryData>();
            addressDetails = new ObservableCollection<Address>();
            LoadAddress();
            LoadCountry();
            RefreshCommand = new Command<string>((key) =>
            {
                addressDetails.Clear();
                LoadAddress();
                IsRefreshing = false;
            });
        }

        public void LoadCountry()
        {
            try
            {
                Http.DataVice.Locations.Instance.Countries("datavice", (bool success, string data) =>
                {
                    if (success)
                    {
                        CountryData country = JsonConvert.DeserializeObject<CountryData>(data);

                        for (int i = 0; i < country.data.Length; i++)
                        {
                            Console.WriteLine("Demoguy! " + country.data[i].name);
                            string code = country.data[i].code;
                            string name = country.data[i].name;
                            _countryList.Add(new CountryData() { CountryCode = code, Name = name });
                        }
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1LOC-C1CVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1LOC-C1CVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1LOC-C1CVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1LOC-C1CVM-" + err.ToString());
                }
            }
        }

        public void LoadAddress()
        {
            try
            {
                if (!isRunning)
                {
                    isRunning = true;
                    Http.DataVice.Address.Instance.Listing((bool success, string data) =>
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
                            isRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            isRunning = false;

                        }
                    });
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1ADD-L1AVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1ADD-L1AVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1ADD-L1AVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1ADD-L1AVM-" + err.ToString());
                }
                isRunning = false;
            }
        }
        public static void LoadData()
        {
            try
            {
                Http.DataVice.Address.Instance.Listing( (bool success, string data) =>
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
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1ADD-L1AVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1ADD-L1AVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1ADD-L1AVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1ADD-L1AVM-" + err.ToString());
                }
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
