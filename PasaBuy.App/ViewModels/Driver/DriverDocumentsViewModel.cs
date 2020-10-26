using PasaBuy.App.Models.Driver;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Driver
{
    public class DriverDocumentsViewModel : BaseViewModel
    {
        public static ObservableCollection<DriverDocuments> _documentsList;

        public Command AddDocumentsCommand { get; set; }

        public ObservableCollection<DriverDocuments> DocumentsList
        {
            get
            {
                return _documentsList;
            }
            set
            {
                _documentsList = value;
                this.NotifyPropertyChanged();
            }
        }
        public DriverDocumentsViewModel()
        {

            this.AddDocumentsCommand = new Command(this.AddDocumentsClicked);
            _documentsList = new ObservableCollection<DriverDocuments>();

            for (int i = 0; i < 5; i++)
            {
                _documentsList.Add(new DriverDocuments()
                {
                    Name = "OR Vehicle Name",
                    Type = "OR Vehicle",
                    Date = "Sept. 5, 2020"
                });
            }
        }

        private void AddDocumentsClicked(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.Driver.AddDocumentPage());
            });

        }
    }
}
