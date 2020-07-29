using Xamarin.Forms;
using PasaBuy.App.Models.Settings;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;
using PasaBuy.App.Views.Master;
using PasaBuy.App.Views.Settings;
using PasaBuy.App.Views;

namespace PasaBuy.App.ViewModels.Settings
{
    /// <summary>
    /// ViewModel for my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AddressViewModel : BaseViewModel
    {
        #region Properties
        public ObservableCollection<Address> AddressDetails { get; set; }
        #endregion

        #region Constructor
        public AddressViewModel()
        {
            this.EditCommand = new Command(this.EditButtonClicked);
            this.DeleteCommand = new Command(this.DeleteButtonClicked);
            this.AddCardCommand = new Command(this.AddCardButtonClicked);

            this.AddressDetails = new ObservableCollection<Address>()
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
            };
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
