
using Newtonsoft.Json;

namespace PasaBuy.App.Models.Onboarding
{
    public class ProfileGetData
    {
        public ProfileData data = new ProfileData();
        public static int totaltransact;
        public static int totalpost;
        public static float totalratings;

        public static void CountPost(string wpid, string snky)
        {
            SocioPress.Transaction.Instance.GetTotal(wpid, snky, (bool success, string data) =>
            {
                if (success)
                {
                    ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                    totaltransact = getdata.data.transac;
                }
                else
                {
                    totaltransact = 0;
                }
            });

            SocioPress.Posts.Instance.Count(wpid, snky, wpid, (bool success, string data) =>
            {
                if (success)
                {
                    ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                    totalpost = getdata.data.count;
                }
                else
                {
                    totalpost = 0;
                }
            });

            SocioPress.Reviews.Instance.Get(wpid, snky, "", (bool success, string data) =>
            {
                if (success)
                {
                    ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                    totalratings = getdata.data.ave_rating;
                }
                else
                {
                    totalratings = 0;
                }
            });
        }
    }

    public class ProfileData
    {
        public int count;
        public float ave_rating;
        public int transac;
    }
}
