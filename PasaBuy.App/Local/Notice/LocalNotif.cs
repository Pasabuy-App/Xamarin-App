using PasaBuy.App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.Local.Notice
{
    public class LocalNotif
    {
        public static LocalNotif instance;
        public static LocalNotif Instance 
        {
            get 
            { 
                if(instance == null)
                {
                    instance = new LocalNotif();
                }
                return instance;
            }
        }

        public LocalNotif()
        {
            
        }

    }
}
