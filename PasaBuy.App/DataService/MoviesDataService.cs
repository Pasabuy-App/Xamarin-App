using PasaBuy.App.ViewModels.StoreDetail;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.DataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class MoviesDataService
    {
        #region fields

        private static MoviesDataService instance;

        private StoreDetailPageViewModel moviesViewModel;

        #endregion

        #region Properties

        /// <summary>
        /// Gets an instance of the <see cref="MoviesDataService"/>.
        /// </summary>
        public static MoviesDataService Instance => instance ?? (instance = new MoviesDataService());

        /// <summary>
        /// Gets or sets the value of movie page view model.
        /// </summary>
        public StoreDetailPageViewModel MoviesPageViewModel =>
            this.moviesViewModel ??
            (this.moviesViewModel = PopulateData<StoreDetailPageViewModel>("navigation.json"));

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
