﻿using System.Threading.Tasks;

namespace PasaBuy.App.Services
{
    public interface IDialogService
    {
        Task<bool> ShowConfirmedAsync(string message, string title, string okText, string cancelText);
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        void ShowAlert(string message, string title, string buttonLabel);
        void ShowToast(string message, int duration = 3000);
    }
}
