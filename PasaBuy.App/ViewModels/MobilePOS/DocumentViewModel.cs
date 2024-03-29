﻿using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class DocumentViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<DocumentData> documentList;
        public ObservableCollection<DocumentData> DocumentList
        {
            get { return documentList; }
            set { documentList = value; this.NotifyPropertyChanged(); }
        }

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
        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ICommand RefreshCommand { protected set; get; }

        #endregion
        public DocumentViewModel()
        {
            documentList = new ObservableCollection<DocumentData>();
            documentList.Clear();

            LoadData();

            UpdateCommand = new Command<object>(UpdateClicked);
            DeleteCommand = new Command<object>(DeleteClicked);


            RefreshCommand = new Command<string>((key) =>
            {
                documentList.Clear();
                LoadData();
                IsRefreshing = false;
            });

        }

        private async void DeleteClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var doc = obj as DocumentData;
                PopupConfirmDeleteDocumentStore.hash_id = doc.ID;
                await PopupNavigation.Instance.PushAsync(new PopupConfirmDeleteDocumentStore());
                IsRunning = false;
            }
        }

        private async void UpdateClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var doc = obj as DocumentData;
                PopupEditDocumentStore.doctype = doc.Title;
                PopupEditDocumentStore.image = doc.Preview;
                PopupEditDocumentStore.docid = doc.ID;
                PopupEditDocumentStore.hash_id = doc.TypeID;
                await PopupNavigation.Instance.PushAsync(new PopupEditDocumentStore());
                IsRunning = false;
            }

        }

        public Command<object> UpdateCommand { get; set; }

        public Command<object> DeleteCommand { get; set; }

        public void LoadData()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    this.DocumentList.Clear();
                    Http.TindaPress.Document.Instance.Listing(PSACache.Instance.UserInfo.stid, "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            DocumentData datas = JsonConvert.DeserializeObject<DocumentData>(data);
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                bool update = false;
                                bool delete = false;
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "edit_document"))
                                {
                                    update = true;
                                }
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "delete_document"))
                                {
                                    delete = true;
                                }
                                this.DocumentList.Add(new DocumentData()
                                {
                                    ID = datas.data[i].ID,
                                    Title = datas.data[i].doctype,
                                    Date = datas.data[i].date_created,
                                    Status = datas.data[i].status,
                                    Preview = datas.data[i].preview,
                                    TypeID = datas.data[i].type_id,
                                    isUpdate = update,
                                    isDelete = delete,
                                    isDeleteCol = update == true ? 1 : 0
                                });
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }


            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-L1DVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-L1DVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-L1DVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-L1DVM-" + err.ToString());
                }
            }
        }


        public static void RefreshData()
        {
            try
            {
                documentList.Clear();
                Http.TindaPress.Document.Instance.Listing(PSACache.Instance.UserInfo.stid, "active", (bool success, string data) =>
                {
                        if (success)
                        {
                            DocumentData datas = JsonConvert.DeserializeObject<DocumentData>(data);
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                bool update = false;
                                bool delete = false;
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "edit_document"))
                                {
                                    update = true;
                                }
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "delete_document"))
                                {
                                    delete = true;
                                }
                                documentList.Add(new DocumentData()
                                {
                                    ID = datas.data[i].ID,
                                    Title = datas.data[i].doctype,
                                    Date = datas.data[i].date_created,
                                    Status = datas.data[i].status,
                                    Preview = datas.data[i].preview,
                                    TypeID = datas.data[i].type_id,
                                    isUpdate = update,
                                    isDelete = delete,
                                    isDeleteCol = update == true ? 1 : 0
                                });
                            }
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });

            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-L1DVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-L1DVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-L1DVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-L1DVM-" + err.ToString());
                }
            }
        }
    }
}
