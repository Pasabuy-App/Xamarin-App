using PasaBuy.App.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Feeds
{
    public class Post : INotifyPropertyChanged
    {
        private string last_id = string.Empty;
        private string photo = string.Empty;
        private string author = string.Empty;
        private string types = "Status";
        private DateTime date = DateTime.Now;
        private int seen = 1234;
        private int image_height;
        private string title;
        private string description;
        private string images;
        private Boolean isaccept = false;
        private Boolean iscontent = false;
        private Boolean isimage = false;
        private string linkpost;

        public Post(string photo, string author, string types, string date, string seen, string title, string description, string images, string image_height, string last_id, string post_link)
        {
            this.last_id = last_id;
            this.photo = photo;
            this.author = author;
            this.types = types;
            this.date = System.Convert.ToDateTime(date);
            this.seen = System.Convert.ToInt32(seen);
            this.image_height = System.Convert.ToInt32(image_height);
            this.title = title;
            this.description = description;
            this.images = images;
            this.linkpost = post_link;

            if (images == "")
            {
                isImage = false;
            }
            else
            {
                isImage = true;
            }
            if (description == "")
            {
                isContent = false;
            }
            else
            {
                isContent = true;
            }
            if (types == "Status")
            {
                isAccept = false;
            }
            else
            {
                isAccept = true;
            }
        }

        public string LinkPost
        {
            get { return linkpost; }
            set
            {
                linkpost = value;
                OnPropertyChanged("LinkPost");
            }
        }

        public string Last_ID
        {
            get { return last_id; }
            set
            {
                last_id = value;
                OnPropertyChanged("Last_ID");
            }
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
        public Boolean isAccept
        {
            get
            {
                return isaccept;
            }
            set
            {
                isaccept = value;
                OnPropertyChanged("isAccept");
            }
        }
        public Boolean isContent
        {
            get
            {
                return iscontent;
            }
            set
            {
                iscontent = value;
                OnPropertyChanged("isAccept");
            }
        }
        public Boolean isImage
        {
            get
            {
                return isimage;
            }
            set
            {
                isimage = value;
                OnPropertyChanged("isAccept");
            }
        }

        public int ImageHeight
        {
            get
            {
                return image_height;
            }
            set
            {
                image_height = System.Convert.ToInt32(value);
                OnPropertyChanged("ImageHeight");
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
