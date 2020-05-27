using PasaBuy.App.Views.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Profile
{
    /// <summary>
    /// ViewModel for burger menu expand page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class MasterPageViewModel : BaseViewModel
    {
        #region Fields

        private string profileName;

        private string profileImage;

        private string email;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MasterPageViewModel" /> class.
        /// </summary>
        public MasterPageViewModel()
        {
            this.profileName = "Juan Dela Cruz";
            this.profileImage = App.BaseImageUrl + "ProfileImage1.png";
            this.email = "juandelacruz@pasabuy.app";

            this.HomeCommand = new Command(this.HomeButtonClicked);
            this.ProfileCommand = new Command(this.ProfileButtonClicked);
            this.MessageCommand = new Command(this.MessageButtonClicked);
            this.MyStoreCommand = new Command(this.MyStoreButtonClicked);
            this.RequestCommand = new Command(this.RequestButtonClicked);
            this.SettingCommand = new Command(this.SettingButtonClicked);
            this.HelpCommand = new Command(this.HelpButtonClicked);
            this.LogoutCommand = new Command(this.LogoutButtonClicked);
            
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string ProfileName
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
        public string ProfileImage
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
        /// Gets or sets the email.
        /// </summary>
        public string Email
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
        public Command MyStoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command RequestCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command SettingCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the help view is clicked.
        /// </summary>
        public Command HelpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the logout view is clicked.
        /// </summary>
        public Command LogoutCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the home button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void HomeButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);
        }

        /// <summary>
        /// Invoked when the profile button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ProfileButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);
        }

        /// <summary>
        /// Invoked when the message button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MessageButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);
        }

        /// <summary>
        /// Invoked when the mystore button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MyStoreButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);
        }

        /// <summary>
        /// Invoked when the request button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void RequestButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);
        }

        /// <summary>
        /// Invoked when the setting button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void SettingButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);
        }

        /// <summary>
        /// Invoked when the help button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void HelpButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);
        }

        /// <summary>
        /// Invoked when the logout button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void LogoutButtonClicked(object obj)
        {
            this.UpdateSelectedItemColor(obj);

            Application.Current.MainPage = new NavigationPage(new LoginWithSocialIconPage());
        }



        /// <summary>
        /// Changes the selection color when an item is tapped.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void UpdateSelectedItemColor(object obj)
        {
            var grid = obj as Grid;
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            grid.BackgroundColor = (Color)retVal;

            // Makes the selected item color change for 100 milliseconds.
            await Task.Delay(100);
            Application.Current.Resources.TryGetValue("Gray-White", out var retValue);
            grid.BackgroundColor = (Color)retValue;
        }

        #endregion
    }
}
