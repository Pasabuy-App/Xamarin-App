using MobilePOS;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class OperationsViewModel : BaseViewModel
    {
        public static ObservableCollection<Operations> _daysOfTheWeek;

        public ObservableCollection<Operations> DaysOfTheWeek
        {
            get
            {
                return _daysOfTheWeek;
            }
            set
            {
                _daysOfTheWeek = value;
                this.NotifyPropertyChanged();
            }
        }

        public OperationsViewModel()
        {
            this.DaysOfTheWeek = new ObservableCollection<Operations>()
            {
                new Operations
                {
                    Day = "Monday"

                },
                new Operations
                {
                    Day = "Tuesday"

                },
                new Operations
                {
                    Day = "Wednesday"

                },
                new Operations
                {
                    Day = "Thursday"

                },
                new Operations
                {
                    Day = "Friday"

                },
                new Operations
                {
                    Day = "Saturday"

                },
                new Operations
                {
                    Day = "Sunday"

                }
            };
        }

    }
}
