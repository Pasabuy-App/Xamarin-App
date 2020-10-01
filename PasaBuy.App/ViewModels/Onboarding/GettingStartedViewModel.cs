using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Views.Onboarding;
using Syncfusion.SfRotator.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Onboarding
{
    /// <summary>
    /// ViewModel for on-boarding gradient page with animation.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class GettingStartedViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<Boarding> boardings;

        private string nextButtonText = "NEXT";

        private bool isSkipButtonVisible = true;

        private int selectedIndex;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="GettingStartedViewModel" /> class.
        /// </summary>
        public GettingStartedViewModel()
        {
            this.SkipCommand = new Command(this.Skip);
            this.NextCommand = new Command(this.Next);
            this.Boardings = new ObservableCollection<Boarding>
            {
                new Boarding()
                {
                    ImagePath = "OnBoardingA.png",
                    Header = "PASABUY.APP",
                    Content = "The first social media delivery app for sellers, buyers, movers and merchants in the Philippines.",
                    RotatorItem = new WalkthroughItem()
                },
                new Boarding()
                {
                    ImagePath = "OnBoardingB.png",
                    Header = "On-demand Delivery",
                    Content = "PasaBuy movers deliver packages at the time you need it by upholding honesty & integrity.",
                    RotatorItem = new WalkthroughItem()
                },
                new Boarding()
                {
                    ImagePath = "OnBoardingC.png",
                    Header = "Food Delivery",
                    Content = "PasaBuy movers deliver food and drinks by maintaining quality at the most secure way. Relish restaurant meals at home!",
                    RotatorItem = new WalkthroughItem()
                },
                new Boarding()
                {
                    ImagePath = "OnBoardingD.png",
                    Header = "Online Shop",
                    Content = "Non-food and online stores can have their products delivered by demand through PasaBuy movers. No need to ship it! Just wait and accept!",
                    RotatorItem = new WalkthroughItem()
                }
            };

            // Set bindingcontext to content view.
            foreach (var boarding in this.Boardings)
            {
                boarding.RotatorItem.BindingContext = boarding;
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<Boarding> Boardings
        {
            get
            {
                return this.boardings;
            }

            set
            {
                if (this.boardings == value)
                {
                    return;
                }

                this.boardings = value;
                this.NotifyPropertyChanged();
            }
        }

        public string NextButtonText
        {
            get
            {
                return this.nextButtonText;
            }

            set
            {
                if (this.nextButtonText == value)
                {
                    return;
                }

                this.nextButtonText = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool IsSkipButtonVisible
        {
            get
            {
                return this.isSkipButtonVisible;
            }

            set
            {
                if (this.isSkipButtonVisible == value)
                {
                    return;
                }

                this.isSkipButtonVisible = value;
                this.NotifyPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                if (this.selectedIndex == value)
                {
                    return;
                }

                this.selectedIndex = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Skip button is clicked.
        /// </summary>
        public ICommand SkipCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Done button is clicked.
        /// </summary>
        public ICommand NextCommand { get; set; }

        #endregion

        #region Methods

        private bool ValidateAndUpdateSelectedIndex(int itemCount)
        {
            if (this.SelectedIndex >= itemCount - 1)
            {
                return true;
            }

            this.SelectedIndex++;
            return false;
        }

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip(object obj)
        {
            PSACache.SetGettingStartedAction(true);
            this.EndGettingStarted();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Next(object obj)
        {
            var itemCount = (obj as SfRotator).ItemsSource.Count();
            if (this.ValidateAndUpdateSelectedIndex(itemCount))
            {
                PSACache.SetGettingStartedAction(false);
                this.EndGettingStarted();
            }
        }

        private void EndGettingStarted()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = new SignInPage();
            });
        }

        #endregion
    }
}
