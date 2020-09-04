using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.PopupLayout;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using Syncfusion.XForms.iOS.Expander;
using Syncfusion.XForms.iOS.TabView;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.XForms.iOS.Graphics;
using  Syncfusion.XForms.iOS.Graphics;
using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace PasaBuy.App.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            SfSegmentedControlRenderer.Init();
            SfLinearProgressBarRenderer.Init();
            SfPopupLayoutRenderer.Init();
            SfChartRenderer.Init();
            SfExpanderRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfTabViewRenderer.Init();
            SfRotatorRenderer.Init();
            Core.Init();
            SfCardViewRenderer.Init();
            SfBadgeViewRenderer.Init();
            SfListViewRenderer.Init();
            SfGradientViewRenderer.Init();
            SfRatingRenderer.Init();
            SfComboBoxRenderer.Init();
            SfBorderRenderer.Init();
            SfButtonRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
