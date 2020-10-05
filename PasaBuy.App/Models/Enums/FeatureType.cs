
namespace PasaBuy.App.Models.Enums
{
    public class FeatureType
    {
        private FeatureType(string value) { Value = value; }

        public string Value { get; set; }

        public static FeatureType Mover 
        { 
            get 
            { 
                return new FeatureType("/wp-json/hatidpress/v1/rider/verify"); 
            } 
        }

        public static FeatureType Merchant 
        { 
            get 
            { 
                return new FeatureType("/wp-json/tindapress/v1/personnel/role/verify"); 
            } 
        }
    }
}
