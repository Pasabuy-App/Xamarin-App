using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.Onboarding;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using DataVice;
using PasaBuy.App.Controllers;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Local;

namespace PasaBuy.App.ViewModels.Onboarding
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private string username;

        private string lastname;

        private string firstname;

        private string gender;

        private Boolean _state = false;

        /*private string country_code;

        private string prov_code;

        private string city_code;

        private string brgy_id;*/

        private string street;

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
        /// Gets or sets the property that bounds with an entry that gets the username from user in the Sign Up page.
        /// </summary>
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                if (this.username == value)
                {
                    return;
                }

                this.username = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the lastname from users in the Sign Up page.
        /// </summary>
        public string Lname
        {
            get
            {
                return this.lastname;
            }

            set
            {
                if (this.lastname == value)
                {
                    return;
                }

                this.lastname = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the firstname from users in the Sign Up page.
        /// </summary>
        public string Fname
        {
            get
            {
                return this.firstname;
            }

            set
            {
                if (this.firstname == value)
                {
                    return;
                }

                this.firstname = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the birthday from user in the Sign Up page.
        /// </summary>
        public DateTime BirthDay
        {
            get;
            set;
        } = DateTime.Now;

        public string BornDate
        {
            get
            {
                return this.BirthDay.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the gender from user in the Sign Up page.
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
        /// Gets or sets the property that bounds with an entry that gets the street from users in the Sign Up page.
        /// </summary>
        public string StreetEntry
        {
            get
            {
                return this.street;
            }

            set
            {
                if (this.street == value)
                {
                    return;
                }

                this.street = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that is bound with stacklayout that gets the visibility state from user in the forgot page.
        /// </summary>
        public Boolean State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
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
            try
            {
                State = true;
                Users.Instance.SignUp(Username, Email, Fname, Lname, Gender, BornDate, AddressVar.co, AddressVar.pr, AddressVar.ct, AddressVar.br, StreetEntry, (bool success, string data) =>
                {
                    if (success)
                    {
                        State = false;
                        Application.Current.MainPage = new VerifyAccountPage();
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        State = false;
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }



        #endregion
    }
}