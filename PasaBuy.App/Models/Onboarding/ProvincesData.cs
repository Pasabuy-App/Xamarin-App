
namespace PasaBuy.App.Models.Onboarding
{
    public class ProvincesData
    {
        public ProvinceList[] data;
        public class ProvinceList
        {
            public string code = string.Empty;
            public string name = string.Empty;
        }
        private string code;
        public string ProvinceCode
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
