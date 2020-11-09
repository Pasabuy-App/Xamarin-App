using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
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
        public bool isClicked = false;

        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ICommand RefreshCommand { protected set; get; }
        public DriverDocumentsViewModel()
        {

            this.AddDocumentsCommand = new Command(this.AddDocumentsClicked);
            _documentsList = new ObservableCollection<DriverDocuments>();

            RefreshDocument(); 
            RefreshCommand = new Command<string>((key) =>
            {
                RefreshDocument();
                IsRefreshing = false;
            });
        }
        public void RefreshDocument()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    _documentsList.Clear();
                    Http.HatidPress.Documents.Instance.Listing_VDocuments( "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            CultureInfo provider = new CultureInfo("fr-FR");
                            DriverDocuments docs = JsonConvert.DeserializeObject<DriverDocuments>(data);
                            for (int i = 0; i < docs.data.Length; i++)
                            {
                                DateTime date = DateTime.ParseExact(docs.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                                bool isGreen;
                                bool isRed;
                                if (docs.data[i].status != "Approve")
                                {
                                    isGreen = true;
                                    isRed = false;
                                }
                                else
                                {
                                    isGreen = false;
                                    isRed = true;
                                }
                                _documentsList.Add(new DriverDocuments()
                                {
                                    Type = docs.data[i].types,
                                    Status = docs.data[i].status,
                                    isGreen = isGreen,
                                    isRed = isRed,
                                    Date = date.ToString("MMM. dd, yyyy")
                                });
                            }
                            IsBusy = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsBusy = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2DOC-L1DDVM.", "OK");
                IsBusy = false;
            }
        }

        public static void LoadDocument()
        {
            try
            {
                _documentsList.Clear();
                Http.HatidPress.Documents.Instance.Listing_VDocuments("", (bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        DriverDocuments docs = JsonConvert.DeserializeObject<DriverDocuments>(data);
                        for (int i = 0; i < docs.data.Length; i++)
                        {
                            DateTime date = DateTime.ParseExact(docs.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            bool isGreen;
                            bool isRed;
                            if (docs.data[i].status != "Approve")
                            {
                                isGreen = true;
                                isRed = false;
                            }
                            else
                            {
                                isGreen = false;
                                isRed = true;
                            }
                            _documentsList.Add(new DriverDocuments()
                            {
                                Type = docs.data[i].types,
                                Status = docs.data[i].status,
                                isGreen = isGreen,
                                isRed = isRed,
                                Date = date.ToString("MMM. dd, yyyy")
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2DOC-L2DDVM.", "OK");
            }
        }

        private async void AddDocumentsClicked(object obj)
        {
            if (!isClicked)
            {
                isClicked = true;
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.Driver.AddDocumentPage());
                isClicked = false;
            }
        }
    }
}
