using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.Helpers
{
    public class AddonTemplateSelector : DataTemplateSelector
    {


        public DataTemplate AddonTemplate { get; set; }


        /// <summary>
        /// Returns Xamarin.Forms.DataTemplate.
        /// </summary>
        /// <param name="item">The Model</param>
        /// <param name="container">The bindable object</param>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return this.AddonTemplate;
        }

    }
}
