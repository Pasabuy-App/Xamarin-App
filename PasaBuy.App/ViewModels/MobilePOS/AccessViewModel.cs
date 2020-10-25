using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class AccessViewModel : BaseViewModel
    {
        private ObservableCollection<AccessGroup> _accessGroup;
        //private ObservableCollection<AccessModel> _accessList;

        public ObservableCollection<AccessGroup> AccessGroup
        {
            get
            {
                return _accessGroup;
            }
            set
            {
                _accessGroup = value;
                this.NotifyPropertyChanged();
            }
        }


        public AccessViewModel()
        {
            _accessGroup = new ObservableCollection<AccessGroup>();

            ObservableCollection<AccessModel> product_access = new ObservableCollection<AccessModel>();
            product_access.Add(new AccessModel() { AccessName = "Add Product" });
            product_access.Add(new AccessModel() { AccessName = "Edit Product" });
            product_access.Add(new AccessModel() { AccessName = "Delete Product" });

            ObservableCollection<AccessModel> category_access = new ObservableCollection<AccessModel>();
            category_access.Add(new AccessModel() { AccessName = "Add Category" });
            category_access.Add(new AccessModel() { AccessName = "Edit Category" });
            category_access.Add(new AccessModel() { AccessName = "Delete Category" });

            ObservableCollection<AccessModel> variants_access = new ObservableCollection<AccessModel>();
            variants_access.Add(new AccessModel() { AccessName = "Add Variants" });
            variants_access.Add(new AccessModel() { AccessName = "Edit Variants" });
            variants_access.Add(new AccessModel() { AccessName = "Delete Variants" });

            ObservableCollection<AccessModel> schedule_access = new ObservableCollection<AccessModel>();
            schedule_access.Add(new AccessModel() { AccessName = "Add Schedule" });
            schedule_access.Add(new AccessModel() { AccessName = "Edit Schedule" });
            schedule_access.Add(new AccessModel() { AccessName = "Delete Schedule" });

            _accessGroup.Add(new AccessGroup() { GroupName = "Product", AccessList = product_access });
            _accessGroup.Add(new AccessGroup() { GroupName = "Category", AccessList = category_access });
            _accessGroup.Add(new AccessGroup() { GroupName = "Variants", AccessList = variants_access });
            _accessGroup.Add(new AccessGroup() { GroupName = "Schedule", AccessList = schedule_access });
        }
    }
}
