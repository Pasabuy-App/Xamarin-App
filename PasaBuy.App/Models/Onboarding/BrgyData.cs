
namespace PasaBuy.App.Models.Onboarding
{
    public class BrgyData
    {
        public BrgyList[] data;
        public class BrgyList
        {
            public string code = string.Empty;
            public string name = string.Empty;
        }
        private string code;
        public string BrgyCode
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
