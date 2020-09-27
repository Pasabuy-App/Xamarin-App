using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PasaBuy.App.Models.Driver
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty RouteCoordinatesProperty = BindableProperty.Create(nameof(RouteCoordinates), typeof(List<Position>), typeof(CustomMap), new List<Position>(), BindingMode.TwoWay);
        public List<CustomPin> CustomPins { get; set; }
        public List<Position> RouteCoordinates { get; set; }
    }
}
