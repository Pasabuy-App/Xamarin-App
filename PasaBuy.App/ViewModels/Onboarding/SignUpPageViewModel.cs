using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.Onboarding;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using DataVice;
using PasaBuy.App.Controllers;
using PasaBuy.App.Models.Onboarding;

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
/*
        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the street from users in the Sign Up page.
        /// </summary>
        public string CountryCode
        {
            get
            {
                return this.country_code;
            }

            set
            {
                if (this.country_code == value)
                {
                    return;
                }

                this.country_code = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the street from users in the Sign Up page.
        /// </summary>
        public string ProvinceCode
        {
            get
            {
                return this.prov_code;
            }

            set
            {
                if (this.prov_code == value)
                {
                    return;
                }

                this.prov_code = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the street from users in the Sign Up page.
        /// </summary>
        public string CityCode
        {
            get
            {
                return this.city_code;
            }

            set
            {
                if (this.city_code == value)
                {
                    return;
                }

                this.city_code = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the street from users in the Sign Up page.
        /// </summary>
        public string BrgyID
        {
            get
            {
                return this.brgy_id;
            }

            set
            {
                if (this.brgy_id == value)
                {
                    return;
                }

                this.brgy_id = value;
                this.NotifyPropertyChanged();
            }
        }*/
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
            //Console.WriteLine("un: " + Username + " em: " + Email + " ln : " + Lname + " fn: " + Fname + " bd: " + BornDate + " gd: " + Gender + " co: " + AddressVar.co + " pv: " + AddressVar.pr + " ct: " + AddressVar.ct + " br: " + AddressVar.br + " st: " + StreetEntry );
            Users.Instance.SignUp(Username, Email, Fname, Lname, Gender, BornDate, "AddressVar.co", "AddressVar.pv", "AddressVar.ct", "AddressVar.br", StreetEntry, (bool success, string data) =>
            {
                if (success)
                {
                    Application.Current.MainPage = new SignInPage();
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