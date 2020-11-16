
namespace PasaBuy.App.Models.Onboarding
{
    public class VerifyAccountVar
    {
        public VerifyAccountData data = new VerifyAccountData();

        public static string ak = string.Empty;
        public static string un = string.Empty;


        public class VerifyAccountData
        {
            public string key = string.Empty;
        }
    }
}
