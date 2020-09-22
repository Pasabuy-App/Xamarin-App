﻿using PasaBuy.App.ViewModels.MobilePOS.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PasaBuy.App.Services
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }
        string PreviousPage { get; }
        Task InitializeAsync();
        Task RemoveModalAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToPageAsync(Page page, object parameter);
        Task<Page> NavigateToPopupAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task<Page> NavigateToPopupAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToRootPage();
        Task RemoveLastFromBackStackAsync();
        Task RemovePopupAsync();
        Task RemoveBackStackAsync();
    }
}
