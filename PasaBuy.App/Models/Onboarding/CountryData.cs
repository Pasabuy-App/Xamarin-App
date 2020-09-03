
namespace PasaBuy.App.Models.Onboarding
{
    public class CountryData
    {
        public CountryList[] data;
        public class CountryList
        {
            public string ID = string.Empty;
            public string code = string.Empty;
            public string name = string.Empty;
            public string tzone = string.Empty;
        }
        private int code;
        public int Code
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
