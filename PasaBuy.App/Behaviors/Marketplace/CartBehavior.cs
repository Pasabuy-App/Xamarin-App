using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.Behaviors.Marketplace
{
    /// <summary>
    /// This class extends the behavior of the catalog page and detail page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CartBehavior : Behavior<ContentView>
    {
        #region Fields

        private ContentView bindablePage;

        #endregion

        #region Method

        /// <summary>
        /// Invoked when adding catalog page and detail page.
        /// </summary>
        /// <param name="bindableContentView">Bindable ContentPage</param>
        protected override void OnAttachedTo(ContentView bindableContentView)
        {
            if (bindableContentView != null)
            {
                base.OnAttachedTo(bindableContentView);
                this.bindablePage = bindableContentView;
            }
        }
        /// <summary>
        /// Invoked when exit from the page.
        /// </summary>
        /// <param name="bindableContentPage">Content Page</param>
        protected override void OnDetachingFrom(ContentView bindableContentView)
        {
            if (bindableContentView != null)
            {
                base.OnDetachingFrom(bindableContentView);
            }
        }

        /// <summary>
        /// Invoked when appearing the page.
        /// </summary>
        /// <param name="sender">Content Page</param>
        /// <param name="e">Event Args</param>
        private void Bindable_Appearing(object sender, EventArgs e)
        {
            // Do something
        }

        #endregion
    }
}
