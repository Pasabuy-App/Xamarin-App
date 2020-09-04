
namespace PasaBuy.App.Models.Onboarding
{
        public class BrgyData
        {
            public BrgyList[] data;
            public class BrgyList
        {
                public string id = string.Empty;
                public string name = string.Empty;
            }
            private string id;
            public string BrgyID
            {
                get { return id; }
                set { id = value; }
            }
            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }
}
