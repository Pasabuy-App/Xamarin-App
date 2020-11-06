using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class AddDocumentViewModel : BaseViewModel
    {
        public static ObservableCollection<DocumentData> _documentTypes;
        public ObservableCollection<DocumentData> DocumentTypes
        {
            get { return _documentTypes; }
            set { _documentTypes = value; this.NotifyPropertyChanged(); }
        }

        public AddDocumentViewModel()
        {
            _documentTypes = new ObservableCollection<DocumentData>();
            _documentTypes.Clear();

            LoadDocumentTypes();
            UpdateCommand = new Command<object>(UpdateClicked);
            DeleteCommand = new Command<object>(DeleteClicked);

        }

        private async void DeleteClicked(object obj)
        {
            IsBusy = true;
            var doc = obj as DocumentData;
            PopupConfirmDeleteDocumentStore.hash_id = doc.ID;
            await PopupNavigation.Instance.PushAsync(new PopupConfirmDeleteDocumentStore());
            IsBusy = false;
        }

        private async void UpdateClicked(object obj)
        {
            IsBusy = true;
            var doc = obj as DocumentData;
            PopupEditDocumentStore.doctype = doc.Title;
            PopupEditDocumentStore.image = doc.Preview;
            await PopupNavigation.Instance.PushAsync(new PopupEditDocumentStore());
            IsBusy = false;

        }

        public Command<object> UpdateCommand { get; set; }

        public Command<object> DeleteCommand { get; set; }


        public static void LoadDocumentTypes()
        {
            try
            {
                Http.TindaFeature.Instance.GetDocumentTypes("active", (bool success, string data) =>
                {
                    if (success)
                    {
                        DocumentData datas = JsonConvert.DeserializeObject<DocumentData>(data);

                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            _documentTypes.Add(new DocumentData()
                            {
                                ID = datas.data[i].ID,
                                Title = datas.data[i].title
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1FBVM.", "OK");
            }
        }
    }
}
