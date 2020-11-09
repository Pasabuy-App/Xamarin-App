
using Newtonsoft.Json;

namespace PasaBuy.App.Models.Onboarding
{
    public class ProfileGetData
    {
        public ProfileData data = new ProfileData();
        public static int totaltransact;
        public static int totalpost;
        public static float totalratings;

    }

    public class ProfileData
    {
        public int count;
        public float ave_rating;
        public int transac;
    }
}
