
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Library;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.Chat;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.Advisory;
using PasaBuy.App.Views.Chat;
using PasaBuy.App.Views.Currency;
using PasaBuy.App.Views.Driver;
using PasaBuy.App.Views.Feeds;
using PasaBuy.App.Views.Navigation;
using PasaBuy.App.Views.Notification;
using PasaBuy.App.Views.Settings;
using PasaBuy.App.Views.StoreViews;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Menu
{
    /// <summary>
    /// ViewModel for burger menu expand page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class MasterMenuViewModel : BaseViewModel
    {
        #region Fields

        public static string postbutton = string.Empty;

        public string profileName;

        private string profileImage;

        private string userBanner;

        private string email;

        private bool isEnable = false;
        public Boolean Status
        {
            get
            {
                return isEnable;
            }
            set
            {
                isEnable = value;
                this.NotifyPropertyChanged();
            }
        }

        bool _isDriver = false;
        public bool isDriver
        {
            get
            {
                return _isDriver;
            }
            set
            {
                if (_isDriver != value)
                {
                    _isDriver = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        bool _isStore = false;
        public bool isStore
        {
            get
            {
                return _isStore;
            }
            set
            {
                if (_isStore != value)
                {
                    _isStore = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private bool _isGpsEnable;

        public bool IsGpsEnable
        {
            get
            {
                return _isGpsEnable;
            }
            set
            {
                _isGpsEnable = value;
                this.NotifyPropertyChanged();
            }
        }

        public static ObservableCollection<Personnels> userinfoList;

        public ObservableCollection<Personnels> UserinfoList
        {
            get { return userinfoList; }
            set { userinfoList = value; this.NotifyPropertyChanged(); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MasterMenuViewModel" /> class.
        /// </summary>
        public MasterMenuViewModel()
        {
            /*isDriver = true;
            isStore = true;*/

            //isDriver = true;
            CheckMover();

            isStore = true;
            IsBusy = false;
            //isDriver = UserEnabledFeature.Instance.isMover;
            //isStore = UserEnabledFeature.Instance.isStore;

            this.profileName = PSACache.Instance.UserInfo.dname;
            this.UserPhoto = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl);
            this.UserBanner = PSAProc.GetUrl(PSACache.Instance.UserInfo.bannerUrl);
            this.email = PSACache.Instance.UserInfo.email;

            this.ProfileCommand = new Command(this.ProfileButtonClicked);
            this.MessageCommand = new Command(this.MessageButtonClicked);
            this.WalletCommand = new Command(this.WalletButtonClicked);
            this.TransactionCommand = new Command(this.TransactionButtonClicked);
            this.AdvisoryCommand = new Command(this.AdvisoryButtonClicked);
            this.NotificationCommand = new Command(this.NotificationButtonClicked);
            this.DriverCommand = new Command(this.DriverButtonClicked);
            this.StoreCommand = new Command(this.StoreButtonClicked);
            this.SettingCommand = new Command(this.SettingButtonClicked);
            this.PasabuyPlusCommand = new Command(this.PasabuyPlusClicked);

            userinfoList = new ObservableCollection<Personnels>();
            userinfoList.CollectionChanged += CollectionChanges;
        }

        private async void CollectionChanges(object sender, EventArgs e)
        {
            await Task.Delay(100);
            this.UserPhoto = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl);
            this.UserBanner = PSAProc.GetUrl(PSACache.Instance.UserInfo.bannerUrl);
        }

        public void CheckMover()
        {
            try
            {
                Http.HatidFeature.Instance.Mover_Data((bool success, string data) =>
                {
                    if (success)
                    {
                        isDriver = true;
                        CultureInfo provider = new CultureInfo("fr-FR");
                        MoverDataModel datas = JsonConvert.DeserializeObject<MoverDataModel>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            DateTime date = DateTime.ParseExact(datas.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            PSACache.Instance.UserInfo.mvid = datas.data[i].mvid;
                            VehicleListViewModel.rating = datas.data[i].rate;
                            VehicleListViewModel.status = datas.data[i].banned == false ? datas.data[i].mover_doc : "Banned";
                            VehicleListViewModel.expiry = date.ToString("MMM. dd, yyyy");
                        }
                    }
                    else
                    {
                        isDriver = false;
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string UserFullname
        {
            get
            {
                return this.profileName;
            }

            set
            {
                if (this.profileName != value)
                {
                    this.profileName = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string UserPhoto
        {
            get
            {
                return this.profileImage;
            }

            set
            {
                if (this.profileImage != value)
                {
                    this.profileImage = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the user banner.
        /// </summary>
        public string UserBanner
        {
            get
            {
                return this.userBanner;
            }

            set
            {
                if (this.userBanner != value)
                {
                    this.userBanner = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string UserEmail
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the transaction view is clicked.
        /// </summary>
        public Command TransactionCommand { get; set; }
        /// <summary>
        /// Gets or sets the command that is executed when the profile view is clicked.
        /// </summary>
        public Command ProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the message view is clicked.
        /// </summary>
        public Command MessageCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the wallet view is clicked.
        /// </summary>
        public Command WalletCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the notification view is clicked.
        /// </summary>
        public Command NotificationCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command AdvisoryCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the driver view is clicked.
        /// </summary>
        public Command DriverCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the store view is clicked.
        /// </summary>
        public Command StoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command SettingCommand { get; set; }
        public Command PasabuyPlusCommand { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the profile button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void TransactionButtonClicked(object obj)
        {
            if (!Status)
            {
                //postbutton = "Profile";
                /*MyProfileViewModel.LoadTotal(PSACache.Instance.UserInfo.wpid);
                MyProfileViewModel.user_id = string.Empty;*/

                Status = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyTransactionsPage()));
                    Status = false;
                });
            }
        }

        private void PasabuyPlusClicked(object obj)
        {
            if (!Status)
            {
                Status = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new PasabuyPlusPage()));
                    Status = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the profile button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ProfileButtonClicked(object obj)
        {
            if (!Status)
            {
                postbutton = "Profile";
                MyProfileViewModel.LoadTotal(PSACache.Instance.UserInfo.wpid);
                MyProfileViewModel.user_id = string.Empty;

                Status = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyProfile()));
                    Status = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the message button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MessageButtonClicked(object obj)
        {
            if (!Status)
            {
                Status = true;
                MessagePage.LastIndex = 11;
                MessagePage.isFirstID = false;
                MessagePage.ids = 0;
                //RecentChatViewModel.chatItems.Clear();
                RecentChatViewModel.LoadMesssage("");
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new PasaBuy.App.Views.Chat.MessagePage()));
                    Status = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the wallet button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void WalletButtonClicked(object obj)
        {
            if (!Status)
            {
                Status = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new WalletPage()));
                    Status = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the notification button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void NotificationButtonClicked(object obj)
        {
            if (!Status)
            {
                Status = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new NotificationPage()));
                    Status = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the request button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void AdvisoryButtonClicked(object obj)
        {
            if (!Status)
            {
                Status = true;
                //IsBusy = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new ArticleList()));
                    Status = false;
                    //IsBusy = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the driver button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void DriverButtonClicked(object obj)
        {
            IsGpsEnable = Xamarin.Forms.DependencyService.Get<IGpsDependencyService>().IsGpsEnable();

            if (!IsGpsEnable)
            {
                await Application.Current.MainPage.DisplayAlert("Notice to User", "Please enable your location first.", "OK");
                Xamarin.Forms.DependencyService.Get<IGpsDependencyService>().OpenSettings();
            } 
            else
            {
                if (!Status)
                {
                    Status = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        MasterView.MyType = "mover";
                        await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new VehicleListPage()));
                        Status = false;
                    });
                }
            }
            
        }

        /// <summary>
        /// Invoked when the store button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void StoreButtonClicked(object obj)
        {
            if (!Status)
            {
                //IsBusy = true;
                Status = true;
                //IsBusy = true;
                MasterView.MyType = "store";
                Device.BeginInvokeOnMainThread(async () =>
               {
                   await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyStoresList()));
                   Status = false;
               });
            }
        }

        /// <summary>
        /// Invoked when the setting button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void SettingButtonClicked(object obj)
        {
            if (!Status)
            {
                Status = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new SettingPage()));
                    Status = false;
                });
            }
        }
        #endregion
    }
}
