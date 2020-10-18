using PasaBuy.App.ViewModels.eCommerce;
using Syncfusion.SfNumericUpDown.XForms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Templates
{
    /// <summary>
    /// Cart item list template.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartItemListTemplate : Grid
    {
        /// <summary>
        /// Bindable property to set the parent bindingcontext.
        /// </summary>
        public static BindableProperty ParentBindingContextProperty =
         BindableProperty.Create(nameof(ParentBindingContext), typeof(object),
         typeof(CartItemListTemplate), null);

        /// <summary>
        /// Gets or sets the parent bindingcontext.
        /// </summary>
        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        /// <summary>
        /// Bindable property to set the parent element.
        /// </summary>
        public static BindableProperty ParentElementProperty =
         BindableProperty.Create(nameof(ParentElement), typeof(object),
         typeof(CartItemListTemplate), null);

        /// <summary>
        /// Gets or sets the Parent element.
        /// </summary>
        public object ParentElement
        {
            get { return GetValue(ParentElementProperty); }
            set { SetValue(ParentElementProperty, value); }
        }

        /// <summary>
        /// Bindable property to set the child element.
        /// </summary>
        public static BindableProperty ChildElementProperty =
         BindableProperty.Create(nameof(ChildElement), typeof(object),
         typeof(CartItemListTemplate), null);

        /// <summary>
        /// Gets or sets the child element.
        /// </summary>
        public object ChildElement
        {
            get { return GetValue(ChildElementProperty); }
            set { SetValue(ChildElementProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemListTemplate"/> class.
        /// </summary>
		public CartItemListTemplate()
        {
            InitializeComponent();
        }

        private void numericUpDown_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            var btn = (SfNumericUpDown)sender;
            /*double total = Convert.ToDouble(btn.ClassId.ToString()) * Convert.ToDouble(e.Value);
            Console.WriteLine("Total Price: " + total);*/
            //Console.WriteLine("Total Quantity: " + e.Value + " ClassID: " + btn.ClassId.ToString());
            CartPageViewModel.InsertCart(ViewModels.Marketplace.StoreDetailsViewModel.store_id, btn.ClassId.ToString(), "", "", "", 0, Convert.ToInt32(e.Value));

            //CartPageViewModel cart = new CartPageViewModel();
            //cart.UpdatePrice();
            //Console.WriteLine("cart.TotalPrice: " + cart.TotalPrice);
            //CartPageViewModel cart = (CartPageViewModel)BindingContext;
            //cart.TotalPrice = Convert.ToDouble(e.Value) * 80;
            //cart.GetQuantity(Convert.ToDouble(e.Value), Convert.ToInt32(btn.ClassId.ToString()));
            //Console.WriteLine("ValueChanges: " + e.Value.ToString());
            //PasaBuy.App.ViewModels.eCommerce.CartPageViewModel.ChangeValue(Convert.ToInt32(e.Value.ToString()));
        }

        /*private void numericUpDown_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            //Console.WriteLine("ValueChanges: " + e.Value.ToString());
            PasaBuy.App.ViewModels.eCommerce.CartPageViewModel.ChangeValue(Convert.ToInt32(e.Value.ToString()));
        }*/
    }
}