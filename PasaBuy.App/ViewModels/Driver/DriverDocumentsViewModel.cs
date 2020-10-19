using PasaBuy.App.Models.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.Driver
{
    public class DriverDocumentsViewModel : BaseViewModel
    {
        public static ObservableCollection<DriverDocuments> _documentsList;

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
            _documentsList = new ObservableCollection<DriverDocuments>();

            for(int i = 0; i < 5; i++)
            {
                _documentsList.Add(new DriverDocuments()
                {
                    Name = "OR Vehicle Name",
                    Type = "OR Vehicle",
                    Date = "Sept. 5, 2020"
                });
            }
        }
    }
}
