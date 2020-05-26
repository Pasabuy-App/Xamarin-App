using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using PasaBuy.App.Views.Forms;
using PasaBuy.App.Views;

namespace PasaBuy.App.Controllers
{
    public class SplashScreen : ContentPage
    {
        Image splashImage;

        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "Logo.png",
                WidthRequest = 100,
                HeightRequest = 100
            };

            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                    new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
                );

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#f4f4f4");
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Check if currently login. TO DO!

            //Animate log here.
            await splashImage.ScaleTo(1, 1000);
            await splashImage.ScaleTo(1.1, 1500, Easing.Linear);

            //IF LOGIN
            //Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());

            //IF NOT
            Application.Current.MainPage = new NavigationPage(new LoginWithSocialIconPage());
        }
    }

}
