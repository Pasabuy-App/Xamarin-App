using PasaBuy.App.Services;
using PasaBuy.App.ViewModels.MobilePOS.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class NavigationViewModel : ViewModelBase
    {
        //private ImageSource _storeImageSource = ImageSource.FromStream(() =>
        //{
        //    //var stream = new MemoryStream();
        //    //return stream;
        //});
        private readonly ISettingsService _settingsService;
        //private readonly IAuthenticationService _authenticationService;

        //public NavigationViewModel(ISettingsService settingService, IAuthenticationService authenticationService)
        //{
        //    _settingsService = settingService;
        //    _authenticationService = authenticationService;
        //}

        //public async Task LoadSetting()
        //{
        //    IsBusy = true;
        //    var setting = await _settingsService.GetSettingAsync(x => x.SettingType == (int)SettingType.StoreSetting);
        //    if (setting != null)
        //    {
        //        StoreSettings storeSettings = JsonConvert.DeserializeObject<StoreSettings>(setting.Data);
        //        if (storeSettings != null)
        //        {
        //            StoreName = storeSettings.StoreName;
        //            StorePhoneNumer = storeSettings.Mobile;
        //        }
        //        if (setting.Logo != null)
        //            StoreImageSource = ImageSource.FromStream(() => new MemoryStream(setting.Logo));
        //    }
        //    IsBusy = false;
        //}
        //public async Task CheckLogin()
        //{
        //    if (!_authenticationService.IsAuthenticated)
        //        await NavigationService.NavigateToModalAsync<LoginViewModel>(null);
        //    else
        //        MessagingCenter.Send<string>("", MiniPOS.AppSettings.Settings.TimerRequestStart);
        //}
        //public ImageSource StoreImageSource
        //{
        //    get => _storeImageSource;
        //    set
        //    {
        //        _storeImageSource = value;
        //        RaisePropertyChanged(() => StoreImageSource);
        //    }
        //}

        private string _storeName;
        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                RaisePropertyChanged(() => StoreName);
            }
        }

        private string _storePhoneNumer;
        public string StorePhoneNumer
        {
            get => _storePhoneNumer;
            set
            {
                _storePhoneNumer = value;
                RaisePropertyChanged(() => StorePhoneNumer);
            }
        }

        //public async Task NavigateToStoreSetting()
        //{
        //    await NavigationService.NavigateToAsync<StoreSettingViewModel>();
        //}
    }
}
