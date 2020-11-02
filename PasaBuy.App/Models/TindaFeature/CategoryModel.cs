using System.ComponentModel;

namespace PasaBuy.App.Models.TindaFeature
{
    public class CategoryModel : INotifyPropertyChanged
    {
        public CategoryData[] data;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string id = string.Empty;
        public string ID
        {
            get 
            { 
                return id; 
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        private string title = string.Empty;
        public string Title
        {
            get 
            { 
                return title; 
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private string info = string.Empty;
        public string Info
        {
            get 
            { 
                return info; 
            }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }
    }

    public class CategoryData
    {
        public string ID = string.Empty;
        public string title = string.Empty;
        public string info = string.Empty;
    }
}
