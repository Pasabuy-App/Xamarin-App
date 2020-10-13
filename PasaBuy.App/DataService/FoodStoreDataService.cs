using PasaBuy.App.ViewModels.Marketplace;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.DataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class FoodStoreDataService
    {
        #region fields

        private static FoodStoreDataService instance;

        private FoodBrowserViewModel restaurantViewModel;

        #endregion

        #region Properties

        /// <summary>
        /// Gets an instance of the <see cref="FoodStoreDataService"/>.
        /// </summary>
        public static FoodStoreDataService Instance => instance ?? (instance = new FoodStoreDataService());

        /// <summary>
        /// Gets or sets the value of Restaurant view model.
        /// </summary>
        public FoodBrowserViewModel RestaurantViewModel =>
            this.restaurantViewModel ??
            (this.restaurantViewModel = PopulateData<FoodBrowserViewModel>("navigation.json"));

        #endregion

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "PasaBuy.App.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T obj;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }
        #endregion
    }
}
