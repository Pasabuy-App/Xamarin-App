using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Feeds;
using System;
using System.ComponentModel;

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
        private Boolean iscontent2 = false;
        private Boolean isimage = false;
        private string linkpost = string.Empty;
        private string post_author = string.Empty;
        private int column = 2;
        private int colspan = 1;
        private int procolumn = 1;
        private int procolspan = 1;
        private string accept_text = "Accept";
        private string vehicle = string.Empty;
        private string pickup = string.Empty;
        private string do_price = string.Empty;

        public Post(string photo, string author, string types, string date, string seen, string title, string description, string images, string image_height,
            string last_id, string post_link, string post_author, string pickup, string vehicle, string do_price)
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
            this.post_author = post_author;
            this.pickup = "<b>" + pickup + "</b>";
            this.vehicle = "<b>" + vehicle + "</b>";
            this.do_price = "<b>" + do_price + "</b>";

            if (images == "")
            {
                isImage = false;
            }
            else
            {
                isImage = true;
            }
            /*if (description == "")
            {
                isContent = false;
            }
            else
            {
                isContent = true;
            }*/
            isContent = false;
            if (types == "Status")
            {
                isContent2 = false;
                isAccept = false; // If post is status, accept button is visible
                HomeColSpan = 3;
                HomeCol = 0;
                ProfileColSpan = 3;
                ProfileCol = 0;
            }
            else
            {
                isContent2 = true;
                if (types == "Selling")
                {
                    isContent = true; // set Content visible to true if post type is selling
                    AcceptText = "Inquire"; // renamed accept button if post type is selling
                }
                if (types == "Pasabay")
                {
                    AcceptText = "Pasabay";// renamed accept button if post type is pasabay
                }
                if (types == "Pabili")
                {
                    isContent = true;
                }
                isAccept = true;
                if (post_author == PSACache.Instance.UserInfo.wpid) //PSACache.Instance.UserInfo.user_type == "User"
                {
                    HomeColSpan = 2; // only share button if the post author is me
                    HomeCol = 0;
                }
                else
                {
                    if (PSACache.Instance.UserInfo.user_type == "User")
                    {
                        HomeColSpan = 2;
                        HomeCol = 0;
                    }
                    else
                    {
                        HomeColSpan = 1;
                        HomeCol = 1;
                    }
                }
                if (MyProfileViewModel.user_id == string.Empty)
                {
                    ProfileColSpan = 2;
                    ProfileCol = 0;
                }
                else
                {
                    if (PSACache.Instance.UserInfo.user_type == "User")
                    {
                        ProfileColSpan = 2;
                        ProfileCol = 0;
                    }
                    else
                    {
                        ProfileColSpan = 1;
                        ProfileCol = 1;
                    }
                }
            }

        }

        public string Vehicle
        {
            get { return vehicle; }
            set
            {
                vehicle = value;
                OnPropertyChanged("Vehicle");
            }
        }
        public string PickUp
        {
            get { return pickup; }
            set
            {
                pickup = value;
                OnPropertyChanged("PickUp");
            }
        }
        public string DO_Price
        {
            get { return do_price; }
            set
            {
                do_price = value;
                OnPropertyChanged("DO_Price");
            }
        }
        public string Post_author
        {
            get { return post_author; }
            set
            {
                post_author = value;
                OnPropertyChanged("Post_author");
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

        public string AcceptText
        {
            get { return accept_text; }
            set
            {
                accept_text = value;
                OnPropertyChanged("AcceptText");
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
                OnPropertyChanged("isContent");
            }
        }
        public Boolean isContent2
        {
            get
            {
                return iscontent2;
            }
            set
            {
                iscontent2 = value;
                OnPropertyChanged("isContent2");
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
                OnPropertyChanged("isImage");
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

        public int HomeColSpan
        {
            get
            {
                return colspan;
            }
            set
            {
                colspan = System.Convert.ToInt32(value);
                OnPropertyChanged("HomeColSpan");
            }
        }

        public int HomeCol
        {
            get
            {
                return column;
            }
            set
            {
                column = System.Convert.ToInt32(value);
                OnPropertyChanged("HomeCol");
            }
        }

        public int ProfileColSpan
        {
            get
            {
                return procolspan;
            }
            set
            {
                procolspan = System.Convert.ToInt32(value);
                OnPropertyChanged("ProfileColSpan");
            }
        }

        public int ProfileCol
        {
            get
            {
                return procolumn;
            }
            set
            {
                procolumn = System.Convert.ToInt32(value);
                OnPropertyChanged("ProfileCol");
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