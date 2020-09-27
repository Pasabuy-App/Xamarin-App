using Xamarin.Forms.Maps;


namespace PasaBuy.App.Models.Driver
{
    public class CustomPin : Pin
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Avatar { get; internal set; }
    }
}
