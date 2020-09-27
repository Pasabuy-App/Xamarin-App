using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
        #endregion
        public DocumentViewModel()
        {
            documentList = new ObservableCollection<DocumentData>();
            documentList.Clear();
            LoadData();
        }
        public static void LoadData()
        {
            try
            {
                TindaPress.Document.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", "", "1", (bool success, string data) =>
                {
                    if (success)
                    {
                        DocumentData post = JsonConvert.DeserializeObject<DocumentData>(data);
                        for (int i = 0; i < post.data.Length; i++)
                        {
                            string id = post.data[i].ID;
                            string doctype = post.data[i].doctype;
                            string approved = post.data[i].approved;
                            string date_created = post.data[i].date_created;

                            if (doctype == "dti_registration")
                            {
                                doctype = "DTI Registration";
                            }
                            if (doctype == "barangay_clearance") 
                            {
                                doctype = "Barangay Clearance";
                            }
                            if (doctype == "lease_contract") 
                            {
                                doctype = "Lease Contract";
                            }
                            if (doctype == "community_tax") 
                            {
                                doctype = "Community Tax";
                            }
                            if (doctype == "occupancy_permit")
                            {
                                doctype = "Occupancy Permit";
                            }
                            if (doctype == "sanitary_permit") 
                            {
                                doctype = "Sanitary Permit";
                            }
                            if (doctype == "fire_permit")
                            {
                                doctype = "Fire Permit"; 
                            }
                            if (doctype == "mayors_permit")
                            {
                                doctype = "Mayor's Permit"; 
                            }
                            documentList.Add(new DocumentData()
                            {
                                ID = id,
                                Name = doctype,
                                Type = date_created,
                                Status = approved
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: 20465.", "OK");
            }
        }
    }
}
