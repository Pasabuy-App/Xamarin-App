using PasaBuy.App.Models.Feeds;
using System;
using System.Collections.Generic;
using PasaBuy.App.Controllers;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.Feeds
{
    public class HomepageViewModel
    {
        public HomepageViewModel()
        {
            //This is a Test.
            homePostList = new ObservableCollection<Post>();
            Random rnd = new Random();

            homePostList.Add(new Post(

                "https://scontent.fmnl4-3.fna.fbcdn.net/v/t1.0-9/106719775_4086437514762774_502557937090430002_n.jpg?_nc_cat=110&_nc_sid=85a577&_nc_oc=AQkcZ-BY-AieNPu8vCPiM95uBIF_0eDQJeZpcZyiM32uFg8ZBxXKq0fer6WGvCYJbIw&_nc_ht=scontent.fmnl4-3.fna&oh=0abe1bc6e34bdbb2b06756427fe79021&oe=5F2C4EEF",
                "Roselyn Madrona",
                "Selling",
                "2020-07-04 09:10 AM",
                rnd.Next(500, 10000).ToString(),
                "Ceramic serving dish",
                "CERAMIC HAND-PAINTED SERVING DISH Microwave, oven, freezer is 💯.",
                "https://scontent.fmnl4-5.fna.fbcdn.net/v/t1.0-9/106969425_3015120108557584_6246739416417392559_n.jpg?_nc_cat=106&_nc_sid=3b2858&_nc_oc=AQmDLpNqBugjnzD87aO5sr5wyCfIeyuTEImTK8P1JWHrED6p4zks83b7SKcXyHgGXdw&_nc_ht=scontent.fmnl4-5.fna&oh=10bbcb6208fe4ffe107349d204e6a882&oe=5F2CCF3B"
                ));

            homePostList.Add(new Post(

                "https://scontent.fmnl4-6.fna.fbcdn.net/v/t1.0-9/s960x960/74607784_2988560257824334_2938615648965623808_o.jpg?_nc_cat=108&_nc_sid=85a577&_nc_oc=AQlDfpsZAunw6E2swHcWtZCGYUf-nw_RKuqNp-MDS79WE-ZeH-TMzZse0VNbRaZkm4k&_nc_ht=scontent.fmnl4-6.fna&_nc_tp=7&oh=f04ee563b05f442655d894e23e1221bb&oe=5F2C4B73",
                "Nadjia Castro",
                "Selling",
                "2020-07-04 07:10 AM",
                rnd.Next(500, 10000).ToString(),
                "Sofa Cover 1set 3pcs.",
                "Quilted 1pc long 53x19inches 2pcs side 19x19 inches.",
                "https://scontent.fmnl4-3.fna.fbcdn.net/v/t1.0-9/s960x960/104118052_3538919462788408_2293159382818909140_o.jpg?_nc_cat=109&_nc_sid=843cd7&_nc_oc=AQkqeE6bulwRJdABC5LDX7FAGaAB9NYW8y3S73-TEV_9BJLCy2bTMUIt5xlQXtylIlA&_nc_ht=scontent.fmnl4-3.fna&_nc_tp=7&oh=24c1b3d7c722cfb45c398f488475e57e&oe=5F2DC6FF"
                ));

            homePostList.Add(new Post(

                UserPrefs.Instance.UserInfo.avatarUrl,
                "Juan Dela Cruz",
                "Status",
                "2020-07-04 09:10 AM",
                rnd.Next(500, 10000).ToString(),
                "Marmaing bagong Parating!",
                "Naku po mukang nasa 💯 percent na po ang ating supply, Salamat sa mga loyal customers.",
                "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/Image1.png"
                ));

            homePostList.Add(new Post(

                "https://scontent.fmnl4-6.fna.fbcdn.net/v/t1.0-1/p100x100/19225466_104763093474668_219154624944360317_n.jpg?_nc_cat=108&_nc_sid=dbb9e7&_nc_oc=AQk_iE19nTOoZ0pbmTSWY9kiYs3jgQvSzifQbWkFltnTKX3MaqxssTmKIPxN4Ij8L3I&_nc_ht=scontent.fmnl4-6.fna&_nc_tp=6&oh=fcd7b9032d61b73eafb89415bffc9f7e&oe=5F2B2DBA",
                "Jess Malvar",
                "Selling",
                "2020-07-04 12:10 AM",
                rnd.Next(500, 10000).ToString(),
                "RUSH‼️ RUSH‼️ Iphone 11 64gb factory unlock",
                "I phone 11 64gb fuctory unlock white good as new no dents rfs financial my personal use bawal sa buy n sell sure buyer only just read pick up to Mr. Speedy or meet up.",
                "https://scontent.fmnl4-3.fna.fbcdn.net/v/t1.0-9/p720x720/107492531_668519327099039_4224027106461395349_o.jpg?_nc_cat=110&_nc_sid=843cd7&_nc_oc=AQkreJEVMpfN07gHhDL50dMn3gF5K5b3y7D6A2rnfK-lTDd_xUlubw6Qo5-jg2uXyPE&_nc_ht=scontent.fmnl4-3.fna&_nc_tp=6&oh=46be947c331fa1d3335f7c10f24dda60&oe=5F2CB9D6"
                ));

            homePostList.Add(new Post(

                "https://scontent.fmnl4-3.fna.fbcdn.net/v/t1.0-1/p100x100/100765577_940769466382047_9052286704722903040_o.jpg?_nc_cat=109&_nc_sid=dbb9e7&_nc_oc=AQnA9a5ARvLqf5qWZMw93e-IbYq1B0KF5qptjRxcx4V1icH6vPtJXFm2WiXPEHFpd90&_nc_ht=scontent.fmnl4-3.fna&_nc_tp=6&oh=13477a53b0f5c7b4ceb7d5d4b67b2f00&oe=5F2BE8B4",
                "Nardo Pallasigue",
                "Selling",
                "2020-07-04 01:10 PM",
                rnd.Next(500, 10000).ToString(),
                "MOTORSTAR ZONGSHEN MOTO R 155 MOTARD BIKE",
                "Good running conditon sasakyan muna lg to. Issue.Xerox paper only wala nang ibang issue.",
                "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/Image1.png"
                ));

            homePostList.Add(new Post(

                "https://scontent.fmnl4-4.fna.fbcdn.net/v/t1.0-1/p100x100/106590990_280201293303425_3437561369088509344_o.jpg?_nc_cat=100&_nc_sid=dbb9e7&_nc_oc=AQnXQfvjHVWoq-8FVwjUxU0osVNlp-L-J-ToU5Ur1-943JezpGSJbJ8fWDK9my_DV5M&_nc_ht=scontent.fmnl4-4.fna&_nc_tp=6&oh=32f06ae39644f4134e557fbaea945af6&oe=5F2C1EBB",
                "Mccoy Santiago",
                "Selling",
                "2020-07-04 01:20 AM",
                rnd.Next(500, 10000).ToString(),
                "Yundai Alto Rush Sale",
                "For sale no to swap Hyundai i10 2012 mdl",
                "https://scontent.fmnl4-6.fna.fbcdn.net/v/t1.0-9/107812244_2659758400965348_2283788319506847969_n.jpg?_nc_cat=107&_nc_sid=3b2858&_nc_oc=AQknmFzPUk8BlT97H2Yjr1tZ4RypsXUPP3v6tDj2WRKmokbY3kqYRjaLa8ob7eOQo0I&_nc_ht=scontent.fmnl4-6.fna&oh=a26c82906e2ee6a9c67c7ec4af9f70a7&oe=5F2B8C7B"
                ));

            homePostList.Add(new Post(

                "https://scontent.fmnl4-6.fna.fbcdn.net/v/t1.0-1/p100x100/106958654_2659596417648213_2530979205868683406_o.jpg?_nc_cat=108&_nc_sid=dbb9e7&_nc_oc=AQkVg9LZ1A-tmcPorcbzdeXTboNu4BqCU6aWlvVmLJrGiCky8nPsg0NTZiubpPT774g&_nc_ht=scontent.fmnl4-6.fna&_nc_tp=6&oh=e33dcf4d82689d20e5f0ce59305a5c88&oe=5F2C010A",
                "Rachel Forte",
                "Selling",
                "2020-07-04 02:02 AM",
                rnd.Next(500, 10000).ToString(),
                "Car key programming and key duplicate with transponder chips",
                "Key duplicate with transponder chips Key fabrication of all lost key Key programming Remote cloning convert to flip key",
                "https://scontent.fmnl4-1.fna.fbcdn.net/v/t1.0-9/107456100_2922906087835627_6591716004901255292_n.jpg?_nc_cat=104&_nc_sid=843cd7&_nc_oc=AQkgP9PEHNjHtEcwuupGTcQo-JWcMz8iHALgtpCHcCnIoBr8-3YsBU1XY9j0nCsWDec&_nc_ht=scontent.fmnl4-1.fna&oh=0404a796389fef89a15a9c3de192021e&oe=5F2E024F"
                ));

            homePostList.Add(new Post(

                "https://scontent.fmnl4-6.fna.fbcdn.net/v/t1.0-1/p100x100/103293841_3628632897152686_8115501332169040963_n.jpg?_nc_cat=107&_nc_sid=dbb9e7&_nc_oc=AQm_wKE3H67cnVeIjTVoMYcbxEuezk5LCU0kII_3O4VGJRLsjlA1X5zrEo7EwKOSj3w&_nc_ht=scontent.fmnl4-6.fna&_nc_tp=6&oh=558a079fb853d750964b80b89677c51a&oe=5F2CE94B",
                "Jheanne Mary",
                "Selling",
                "2020-07-04 03:24 PM",
                rnd.Next(500, 10000).ToString(),
                "House and lot Imus",
                "House and lot for sale imus 115 sqm lot area",
                "https://scontent.fmnl4-5.fna.fbcdn.net/v/t1.0-9/p720x720/107295057_3696196757062966_7591404754667102853_o.jpg?_nc_cat=106&_nc_sid=3b2858&_nc_oc=AQnnZmsUzW8mDV2PodWTaSFzvK6SH1ZKgxatBgRLOQU6a52Jjzbfwu135BVkbh0Qm4k&_nc_ht=scontent.fmnl4-5.fna&_nc_tp=6&oh=b5765c15f0643731a60102ffa22e4161&oe=5F2DB87F"
                ));

            homePostList.Add(new Post(

                "https://scontent.fmnl4-2.fna.fbcdn.net/v/t1.0-1/p100x100/84313893_2911326832221845_7358689106005590016_n.jpg?_nc_cat=105&_nc_sid=dbb9e7&_nc_oc=AQn5Z6z9IyTImbeeaOxF0Fq3XFXYVuCJoww4XXRS8J8IBUwrcIxt8ysRsMQFkdbNMpI&_nc_ht=scontent.fmnl4-2.fna&_nc_tp=6&oh=f492f1eab143471242f9fdc295a2327f&oe=5F2AEF57",
                "Cherelyn Rasuela",
                "Selling",
                "2020-07-04 04:50 PM",
                rnd.Next(500, 10000).ToString(),
                "Ukay Bales and Prepack",
                "See the details below for ukay-ukay bundle&prepacked:",
                "https://scontent.fmnl4-3.fna.fbcdn.net/v/t1.0-9/106518087_3263352437019281_4132942570062861278_n.jpg?_nc_cat=109&_nc_sid=3b2858&_nc_oc=AQl62X9_MY8248W4S2SEiFMqDbPPlEtCtrx1o0bB-hpC_UgWPHx_jhBnBmbbutpATCA&_nc_ht=scontent.fmnl4-3.fna&oh=8895046e57db463cca4b357d83ef4e2b&oe=5F2E3762"
                ));

        }

        public string Placeholder
        {
            get
            {
                return "Write something here...";
            }
        }

        public string Photo
        {
            get
            {
                return UserPrefs.Instance.UserInfo.avatarUrl;
            }
        }

        public ObservableCollection<Post> homePostList;

        public ObservableCollection<Post> HomePosts
        {
            get { return homePostList; }
            set { this.homePostList = value; }
        }
    }
}
