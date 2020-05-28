using PasaBuy.App.Models.Catalog;
using PasaBuy.App.Views.Master;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Catalog
{
    /// <summary>
    /// ViewModel for event list page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class EventListViewModel : BaseViewModel
    {
        #region Fields

        private List<EventList> eventItems;

        private List<EventList> popularEventItems;

        private List<EventList> upcomingEventItems;

        public int selectedIndex;

        public string searchText;

        public string allListSearchText;

        public string upcomingListSearchText;

        public string popularListSearchText;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="EventListViewModel" /> class.
        /// </summary>

        public EventListViewModel()
        {
            this.EventItems = new List<EventList>()
            {
                 new EventList { ImagePath = App.BaseImageUrl +"Event-Image-two.png" , EventMonth="RATING",
                                   EventName="Super8 San Pedro", EventDate="2.5", IsUpcoming=true,
                                   EventTime="SCHEDULE: 2:00 PM - 6:00 PM"},
                 new EventList {  ImagePath = App.BaseImageUrl +"Event-Image.png" , EventMonth="RATING",
                                   EventName="Tindahan ni Aling Nena", EventDate="4.1",IsPopular=true,
                                   EventTime="SCHEDULE: 7:00 PM - 11:00 PM"},
                 new EventList { ImagePath =  App.BaseImageUrl +"Event-Image-three.png" , EventMonth="RATING",
                                   EventName="Robinson Southwoods", EventDate="4.0", IsUpcoming=true,
                                   EventTime="SCHEDULE: 10:00 AM - 2:00 PM"},
                 new EventList { ImagePath = App.BaseImageUrl +"Event-Image-one.png", EventMonth="RATING",
                                   EventName="Puregold Halang", EventDate="4.5",IsPopular=true,
                                   EventTime="SCHEDULE: 9:00 PM - 1:00 PM"}
            };

            this.PopularEventItems = EventItems.Where(item => item.IsUpcoming == true).ToList();

            this.UpcomingEventItems = EventItems.Where(item => item.IsPopular == true).ToList();

            this.BackButtonCommand = new Command(this.BackButtonClicked);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the event items collection.
        /// </summary>
        public List<EventList> EventItems
        {
            get
            {
                return this.eventItems;
            }

            set
            {
                this.eventItems = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the upcoming events collection.
        /// </summary>
        public List<EventList> UpcomingEventItems
        {
            get
            {
                return this.upcomingEventItems;
            }

            set
            {
                this.upcomingEventItems = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the popular events collection.
        /// </summary>
        public List<EventList> PopularEventItems
        {
            get
            {
                return this.popularEventItems;
            }

            set
            {
                this.popularEventItems = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected index of the event.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                this.selectedIndex = value;
                SearchText = string.Empty;

            }
        }

        /// <summary>
        /// Gets or sets the search text in the event.
        /// </summary>
        public string SearchText
        {
            get
            {
                return this.searchText;
            }

            set
            {
                this.searchText = value;
                UpdateSelectedText();
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the all list search text in the collection.
        /// </summary>
        public string AllListSearchText
        {
            get
            {
                return this.allListSearchText;
            }

            set
            {
                allListSearchText = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the upcoming list search text in the collection.
        /// </summary>
        public string UpcomingListSearchText
        {
            get
            {
                return this.upcomingListSearchText;
            }

            set
            {
                upcomingListSearchText = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the popular list search text collection.
        /// </summary>
        public string PopularListSearchText
        {
            get
            {
                return this.popularListSearchText;
            }

            set
            {
                popularListSearchText = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when back button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }

        #endregion

        #region Methods

        private void UpdateSelectedText()
        {
            switch (selectedIndex)
            {
                case 0:
                    AllListSearchText = searchText;
                    break;

                case 1:
                    UpcomingListSearchText = searchText;
                    break;

                case 2:
                    PopularListSearchText = searchText;
                    break;
            }
        }

        /// <summary>
        /// Invoked when back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void BackButtonClicked(object obj)
        {
            Application.Current.MainPage = new MainPage();
        }
        #endregion
    }
}
