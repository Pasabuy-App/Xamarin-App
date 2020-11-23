using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Feeds
{
    public class PopupPasabuyModel
    {
        private string title = string.Empty;
        private string details = string.Empty;
        private string clicked_function = string.Empty;

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


        public string Details
        {
            get
            {
                return this.details;
            }

            set
            {
                this.details = value;
                OnPropertyChanged("Details");
            }
        }

        public string ClickedFunction
        {
            get
            {
                return this.clicked_function;
            }

            set
            {
                this.clicked_function = value;
                OnPropertyChanged("ClickedFunction");
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
