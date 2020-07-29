using PasaBuy.App.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Feeds
{
    public class Post : INotifyPropertyChanged
    {
        private string photo = string.Empty;
        private string author = string.Empty;
        private string types = "Status";
        private DateTime date = DateTime.Now;
        private int seen = 1234;

        private string title;
        private string description;
        private string images;

        public Post(string photo, string author, string types, string date, string seen, string title, string description, string images)
        {
            this.photo = photo;
            this.author = author;
            this.types = types;
            this.date = System.Convert.ToDateTime(date);
            this.seen = System.Convert.ToInt32(seen); ;

            this.title = title;
            this.description = description;
            this.images = images;
        }

        public string Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        public string Types
        {
            get { return types; }
            set
            {
                types = value;
                OnPropertyChanged("Types");
            }
        }

        public string Date
        {
            get { return date.ToString("MMMM dd h:mm tt"); }
            set
            {
                date = System.Convert.ToDateTime(value);
                OnPropertyChanged("Date");
            }
        }

        public string Seen
        {
            get { return seen.ToString() + " seen"; }
            set
            {
                seen = System.Convert.ToInt32(value);
                OnPropertyChanged("Seen");
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Images
        {
            get { return images; }
            set
            {
                images = value;
                OnPropertyChanged("Images");
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
