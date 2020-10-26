using Xamarin.Forms;

namespace PasaBuy.App.Helpers
{
    public class VariantsTemplateSelecteior : DataTemplateSelector
    {
        #region Public Properties



        public DataTemplate VariantsTemplate { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// Returns Xamarin.Forms.DataTemplate.
        /// </summary>
        /// <param name="item">The Model</param>
        /// <param name="container">The bindable object</param>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return this.VariantsTemplate;
        }

        #endregion
    }
}
