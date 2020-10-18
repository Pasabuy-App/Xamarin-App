using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Marketplace
{
    public class Variants
    {
        private string name = string.Empty;
        private string id = string.Empty;
        private string baseprice = string.Empty;


        public ObservableCollection<Options> options { get; set; }

        public VariantsData[] data;
        public class VariantsData
        {
            public string ID = string.Empty;
            public string pdid = string.Empty;
            public string name = string.Empty;
            public string info = string.Empty;
            public string status = string.Empty;
            public string baseprice = string.Empty;
        }



        public string Baseprice
        {
            get
            {
                return this.baseprice;
            }

            set
            {
                this.baseprice = value;
                OnPropertyChanged("Baseprice");
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                OnPropertyChanged("Name");
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