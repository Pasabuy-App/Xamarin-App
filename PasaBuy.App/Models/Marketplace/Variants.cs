using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PasaBuy.App.Models.Marketplace
{
    public class Variants
    {
        private string title = string.Empty;
        private string id = string.Empty;
        private string required = string.Empty;
        private string info = string.Empty;
        private string bases = string.Empty;
        public ObservableCollection<Options> options { get; set; }

        public ObservableCollection<Options> addons { get; set; }

        public VariantsData[] data;
        public class VariantsData
        {
            public string ID = string.Empty;
            public string pdid = string.Empty;
            public string title = string.Empty;
            public string info = string.Empty;
            public string status = string.Empty;
            public string required = string.Empty;
            public ObservableCollection<Options> options { get; set; }
            public ObservableCollection<Options> addons { get; set; }

        }
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
        public string Base
        {
            get
            {
                return this.bases;
            }

            set
            {
                this.bases = value;
                OnPropertyChanged("Base");
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

        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                OnPropertyChanged("Id");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}