
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PasaBuy.App.Models.TindaFeature
{
    public class VariantModel : INotifyPropertyChanged
    {
        private string title = string.Empty;
        private string id = string.Empty;
        private string required = string.Empty;
        private string info = string.Empty;
        private double price;
        public ObservableCollection<OptionModel> options { get; set; }

        public ObservableCollection<OptionModel> addons { get; set; }

        public VariantsData[] data;
        public string ID
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                OnPropertyChanged("ID");
            }
        }
        public double Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
                OnPropertyChanged("Price");
            }
        }
        public string Info
        {
            get
            {
                return this.info;
            }

            set
            {
                this.info = value;
                OnPropertyChanged("Info");
            }
        }
        public string Required
        {
            get
            {
                return this.required;
            }

            set
            {
                this.required = value;
                OnPropertyChanged("Baseprice");
            }
        }
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    public class VariantsData
    {
        public string ID = string.Empty;
        public string pdid = string.Empty;
        public string title = string.Empty;
        public string price = string.Empty;
        public string info = string.Empty;
        public string status = string.Empty;
        public string required = string.Empty;
        public ObservableCollection<OptionModel> options { get; set; }
        public ObservableCollection<OptionModel> addons { get; set; }

    }
}
