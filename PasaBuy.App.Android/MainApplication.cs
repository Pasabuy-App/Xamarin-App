using Plugin.CurrentActivity;
using PasaBuy.App.Local;


#if DEBUG
using System;
using Android.App;
using Android.Runtime;
#else
[Application(Debuggable = false)]
#endif

[Application(UsesCleartextTraffic = true, Debuggable = false)]
[MetaData("com.google.android.maps.v2.API_KEY",
              Value = PSAConfig.googleApiKey)]

public class MainApplication : Application
{
    public MainApplication(IntPtr handle, JniHandleOwnership transer)
      : base(handle, transer)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();
        CrossCurrentActivity.Current.Init(this);
    }
}
