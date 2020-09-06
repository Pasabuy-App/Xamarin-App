﻿using DataVice;
using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Views.Onboarding;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static PasaBuy.App.Models.Onboarding.VerifyAccountVar;

namespace PasaBuy.App.ViewModels.Onboarding
{
    public class VerifyResetViewModel : BaseViewModel
    {

        #region Fields

        private string ak;

        //private Boolean _state = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="VerifyResetViewModel" /> class.
        /// </summary>
        public VerifyResetViewModel()
        {
            this.SubmitCommand = new Command(this.SfButton_Clicked);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the activation key from users in Verify Reset page.
        /// </summary>
        public string ActivationKey
        {
            get
            {
                return this.ak;
            }

            set
            {
                if (this.ak == value)
                {
                    return;
                }

                this.ak = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that is bound with stacklayout that gets the visibility state from user in the Verify Reset  page.
        /// </summary>
        /*public Boolean State
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
        }*/

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Verify Reset button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>


        private void SfButton_Clicked(object obj)
        {
            //State = true;
            Users.Instance.Activate(ActivationKey, VerifyAccountVar.un, (bool success, string data) =>
            {
                if (success)
                {
                    //State = false;
                    VerifyAccountData akey = JsonConvert.DeserializeObject<VerifyAccountData>(data);
                        //Console.WriteLine(ActivationKey + " " + akey.key);
                    VerifyAccountVar.ak = akey.key;
                    Application.Current.MainPage = new CreatePassword();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    //State = false;
                }
            });
        }


        #endregion
    }
}