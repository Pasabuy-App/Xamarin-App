using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.Onboarding;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Onboarding
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private string name;

        private string fname;

        private string lname;

        private string gender;

        private string password;

        private string confirmPassword;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the fname from user in the Sign Up page.
        /// </summary>
        public string Fname
        {
            get
            {
                return this.fname;
            }

            set
            {
                if (this.fname == value)
                {
                    return;
                }

                this.fname = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the lname from user in the Sign Up page.
        /// </summary>
        public string Lname
        {
            get
            {
                return this.lname;
            }

            set
            {
                if (this.lname == value)
                {
                    return;
                }

                this.lname = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the lname from user in the Sign Up page.
        /// </summary>
        public string Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                if (this.gender == value)
                {
                    return;
                }

                this.gender = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the lname from user in the Sign Up page.
        /// </summary>
        public DateTime BDay
        {
            get;
            set;
        } = DateTime.Now;

        public string MyDateString
        {
            get
            {
                return this.BDay.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            //new Alert("Demoguy Notice", "Registration is not yet implemented. Thank you for your patience!", "AGREE");

            RestAPI.Instance.SignUp(Name, Email, Fname, Lname, Gender, MyDateString, "175", "1376", "137603", "36794", "street", (bool success, string data) =>
            {
                if (success)
                {
                    Application.Current.MainPage = new PasaBuy.App.Views.Onboarding.SignInPage();
                }

                else
                {
                    new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                }
            });
        }

        #endregion
    }
}