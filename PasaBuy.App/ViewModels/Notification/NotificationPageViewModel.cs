using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Notification;
using PasaBuy.App.Views.Notification;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Notification
{
    public class NotificationPageViewModel : BaseViewModel
    {
        #region Fields
        public static int count = 0;

        public static ObservableCollection<TaskNotification> taskNotificationList;

        public ObservableCollection<TaskNotification> TaskNotificationsList
        {
            get { return taskNotificationList; }
            set { taskNotificationList = value; this.NotifyPropertyChanged(); }
        }

        private Command<object> itemTappedCommand;

        bool _isRefreshing = false;

        #endregion

        #region Constructor

        public NotificationPageViewModel()
        {
            this.MarkReadAll = new Command(this.ReadAllClicked);
            RefreshCommand = new Command<string>((key) =>
            {
                taskNotificationList.Clear();
                LoadData();
                IsRefreshing = false;
            });

            taskNotificationList = new ObservableCollection<TaskNotification>();
            LoadData();
        }

        public static void LoadData()
        {
            try
            {
                NotificationPage.LastIndex = 11;
                SocioPress.Activity.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        taskNotificationList.Clear();
                        string iconcolor = string.Empty;
                        TaskNotificationData taskdata = JsonConvert.DeserializeObject<TaskNotificationData>(data);
                            for (int i = 0; i < taskdata.data.Length; i++)
                            {
                                bool isread = false;
                                string iconname = string.Empty;
                                string id = taskdata.data[i].id;
                                string wpid = taskdata.data[i].wpid;
                                string icon = taskdata.data[i].icon;
                                string activity_title = taskdata.data[i].activity_title;
                                string activity_info = taskdata.data[i].activity_info;
                                string open = taskdata.data[i].open;
                                string date_created = taskdata.data[i].date_created == string.Empty ? new DateTime().ToString() : taskdata.data[i].date_created;
                                if (open != "") { isread = true; }
                                if (icon == "info") { iconname = "?"; iconcolor = "#90CAF9"; }
                                if (icon == "warn") { iconname = "!"; iconcolor = "#FFB74D"; }
                                if (icon == "error") { iconname = "X"; iconcolor = "#EF5350"; }

                                taskNotificationList.Add(new TaskNotification()
                                {
                                    ID = id,
                                    UserName = iconname,
                                    BackgroundColor = iconcolor,
                                    Description = activity_title,
                                    Detail = activity_info,
                                    TaskID = "",
                                    Time = date_created,
                                    IsRead = isread
                                });
                            count = 1;
                            }
                    }
                });
                if (count == 0)
                {
                    taskNotificationList.Add(new TaskNotification()
                    {
                        ID = "0",
                        UserName = "?",
                        BackgroundColor = "#90CAF9",
                        Description = "No Notification.",
                        Detail = "You don't have any notification yet.",
                        TaskID = "",
                        Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        IsRead = false
                    });
                }
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
        }

        public static void LoadMore(string lastid)
        {
            try
            {
                SocioPress.Activity.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        string iconcolor = string.Empty;
                        TaskNotificationData taskdata = JsonConvert.DeserializeObject<TaskNotificationData>(data);
                        for (int i = 0; i < taskdata.data.Length; i++)
                        {
                            bool isread = false;
                            string iconname = string.Empty;
                            string id = taskdata.data[i].id;
                            string wpid = taskdata.data[i].wpid;
                            string icon = taskdata.data[i].icon;
                            string activity_title = taskdata.data[i].activity_title;
                            string activity_info = taskdata.data[i].activity_info;
                            string open = taskdata.data[i].open;
                            string date_created = taskdata.data[i].date_created == string.Empty ? new DateTime().ToString() : taskdata.data[i].date_created;
                            if (open != "") { isread = true; }
                            if (icon == "info") { iconname = "?"; iconcolor = "#90CAF9"; }
                            if (icon == "warn") { iconname = "!"; iconcolor = "#FFB74D"; }
                            if (icon == "error") { iconname = "X"; iconcolor = "#EF5350"; }

                            taskNotificationList.Add(new TaskNotification()
                            {
                                ID = id,
                                UserName = iconname,
                                BackgroundColor = iconcolor,
                                Description = activity_title,
                                Detail = activity_info,
                                TaskID = "",
                                Time = date_created,
                                IsRead = isread
                            });
                        }
                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
        }

        #endregion

        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.ItemSelected));
            }
        }
        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command MarkReadAll { get; set; }

        public ICommand RefreshCommand { set; get; }

        #endregion

        #region Property
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }

            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();

                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected from the task notifications list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void ItemSelected(object selectedItem)
        {
            try
            {
                string id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as TaskNotification).ID;
                if (id != "0")
                {
                    SocioPress.Activity.Instance.Open(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, id, "", (bool success, string data) =>
                    {
                        taskNotificationList.Clear();
                        LoadData();
                    });
                }
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
            //((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as TaskNotification).IsRead = true;
            //new Alert("Title", "example"+ ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as TaskNotification).ID, "OK");
        }

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ReadAllClicked(object obj)
        {
            try
            {
                SocioPress.Activity.Instance.MarkAll(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, (bool success, string data) =>
                {
                    taskNotificationList.Clear();
                    LoadData();
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
        }

        #endregion
    }
}
