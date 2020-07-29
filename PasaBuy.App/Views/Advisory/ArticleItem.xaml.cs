using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Advisory
{
    /// <summary>
    /// Article parallax header page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticleItem : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleItem"/> class.
        /// </summary>
        public ArticleItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invokes when back button is clicked.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}