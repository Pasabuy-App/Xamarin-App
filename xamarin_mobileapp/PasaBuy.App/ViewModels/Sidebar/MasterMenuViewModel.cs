using PasaBuy.App.Views.Forms;
using PasaBuy.App.Views.Master;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using PasaBuy.App.Views.Notification;
using PasaBuy.App.Views.Settings;
using PasaBuy.App.Views.Chat;
using PasaBuy.App.Views.Social;
using PasaBuy.App.Views.Catalog;
using System.Linq;

namespace PasaBuy.App.ViewModels.Sidebar
{
    /// <summary>
    /// ViewModel for burger menu expand page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class MasterMenuViewModel : BaseViewModel
    {
        #region Fields

        private string profileName;

        private string profileImage;

        private string userBanner;

        private string email;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MasterMenuViewModel" /> class.
        /// </summary>
        public MasterMenuViewModel()
        {
            this.profileName = "Juan Dela Cruz";
            this.profileImage = App.BaseImageUrl + "ProfileImage1.png";
            this.userBanner = App.BaseImageUrl + "Sky-Image.png";
            this.email = "juandc20@pasabuy.app";

            this.HomeCommand = new Command(this.HomeButtonClicked);
            this.ProfileCommand = new Command(this.ProfileButtonClicked);
            this.MessageCommand = new Command(this.MessageButtonClicked);
            this.StoreCommand = new Command(this.StoreButtonClicked);
            this.UpdatesCommand = new Command(this.UpdatesButtonClicked);
            this.NotificationCommand = new Command(this.NotificationButtonClicked);
            this.SettingCommand = new Command(this.SettingButtonClicked);
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
        /// Gets or sets the command that is executed when the home view is clicked.
        /// </summary>
        public Command HomeCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the profile view is clicked.
        /// </summary>
        public Command ProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the message view is clicked.
        /// </summary>
        public Command MessageCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the mystore view is clicked.
        /// </summary>
        public Command StoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command UpdatesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command SettingCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the notification view is clicked.
        /// </summary>
        public Command NotificationCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the home button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void HomeButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);

            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new MainPageDetail());
        }

        /// <summary>
        /// Invoked when the profile button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ProfileButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);

            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new SocialProfileWithMessagePage());
        }

        /// <summary>
        /// Invoked when the message button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MessageButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);

            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new RecentChatPage());
        }

        /// <summary>
        /// Invoked when the mystore button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void StoreButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);

            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new EventListPage());
        }

        /// <summary>
        /// Invoked when the request button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void UpdatesButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);

            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new ArticleListPage());
        }

        /// <summary>
        /// Invoked when the notification button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void NotificationButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);

            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new TaskNotificationPage());
        }

        /// <summary>
        /// Invoked when the setting button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void SettingButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);

            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new SettingPage());
        }

        public Label iconLabel = null;
        public Label textLabel = null;

        /// <summary>
        /// Changes the selection color when an item is tapped.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void UpdateSelectedItemColor(object obj)
        {
            var grid = obj as Grid;
            Application.Current.Resources.TryGetValue("PrimaryColor", out var primeColor);
            Application.Current.Resources.TryGetValue("Gray-900", out var normalColor);
            //grid.BackgroundColor = (Color)retVal;
            if (iconLabel != null)
            {
                iconLabel.TextColor = (Color)normalColor;
            }
            if (textLabel != null)
            {
                textLabel.TextColor = (Color)normalColor;
            }

            var iconview = grid.Children.FirstOrDefault(v => Grid.GetRow(v) == 0 && Grid.GetColumn(v) == 0);
            var iconTar = iconview as Label;
            if (iconTar != null)
            {
                iconTar.TextColor = (Color)primeColor;
                iconLabel = iconTar;
            }

            var textView = grid.Children.FirstOrDefault(v => Grid.GetRow(v) == 0 && Grid.GetColumn(v) == 1);
            var textTar = textView as Label;
            if (textTar != null)
            {
                textTar.TextColor = (Color)primeColor;
                textLabel = textTar;
            }

            // Makes the selected item color change for 100 milliseconds.
            //await Task.Delay(100);
            //Application.Current.Resources.TryGetValue("PrimaryColor", out var retValue);
            //grid.BackgroundColor = (Color)retValue;

            try
            {
                ((MainPage)App.Current.MainPage).HideSidebar();
            } 
            catch(System.InvalidCastException err)
            {
                //Do nothing...
            }
            
        }

        #endregion
    }
}
