using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Settings;
using PasaBuy.App.Views.Settings;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Settings
{
    /// <summary>
    /// ViewModel for my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ContactViewModel : BaseViewModel
    {

        public ObservableCollection<Contact> ContactDetails { get; set; }



        public ContactViewModel()
        {
            this.EditCommand = new Command(this.EditButtonClicked);
            this.DeleteCommand = new Command(this.DeleteButtonClicked);
            this.AddCardCommand = new Command(this.AddCardButtonClicked);

            this.ContactDetails = new ObservableCollection<Contact>()
            {
                new Contact
                {
                    Type = "Phone",
                    Value = "09386965095"
                },
                new Contact
                {
                    Type = "Email",
                    Value = "dev@pasabuy.app"
                },
                new Contact
                {
                    Type = "Emergency",
                    Value = "09398229807"
                },
            };
        }


        #region Methods
        /// <summary>
        /// Invoked when the edit button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditButtonClicked(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new EditContact()));
        }

        /// <summary>
        /// Invoked when the delete button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void DeleteButtonClicked(object obj)
        {
            var answer = new ConfirmAlert("Delete Contact", "Are you sure you want to delete this contact", "Ok", "Cancel");

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
