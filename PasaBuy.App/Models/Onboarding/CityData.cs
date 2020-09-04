
namespace PasaBuy.App.Models.Onboarding
{
    public class CityData
    {
        public CityList[] data;
        public class CityList
        {
            public string code = string.Empty;
            public string name = string.Empty;
        }
        private string code;
        public string CityCode
        {
            get { return code; }
            set { code = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
