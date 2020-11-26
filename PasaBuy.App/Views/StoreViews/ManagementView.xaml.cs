using PasaBuy.App.Resources.Texts;
using PasaBuy.App.Views.StoreViews.Management;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagementView : ContentPage
    {
        public int count = 0;
        public List<Models.MobilePOS.Management> managementList { get; set; }
        public ManagementView()
        {
            InitializeComponent();
            CheckPermission();
        }

        public void CheckPermission()
        {
            managementList = new List<Models.MobilePOS.Management>();

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "add_category" || per.action == "edit_category" || per.action == "delete_category")
                {
                    managementList.Add(new Models.MobilePOS.Management(typeof(CategoryView), "https://pasabuy.app/wp-content/uploads/2020/10/management-category.png", TextsTranslateManager.Translate("Categories"),
                                      TextsTranslateManager.Translate("View, add, edit, and delete product category.")));
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "add_products" || per.action == "edit_products" || per.action == "delete_products")
                {
                    managementList.Add(new Models.MobilePOS.Management(
                        typeof(ProductsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-product.png", TextsTranslateManager.Translate("Products"),
                                              TextsTranslateManager.Translate("View, add, edit, and delete products."))
                    );
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "add_variant" || per.action == "edit_variant" || per.action == "delete_variant")
                {
                    managementList.Add(new Models.MobilePOS.Management(
                        typeof(VariantsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-variants.png", TextsTranslateManager.Translate("Variants"),
                                              TextsTranslateManager.Translate("View, add, edit, and delete product variants and options."))
                    );
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "add_role" || per.action == "edit_role" || per.action == "delete_role")
                {
                    managementList.Add(new Models.MobilePOS.Management(
                        typeof(RolesView), "https://pasabuy.app/wp-content/uploads/2020/10/management.png", TextsTranslateManager.Translate("Roles"),
                                              TextsTranslateManager.Translate("Manage access roles of your personnel"))
                    );
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "add_personnel" || per.action == "edit_personnel" || per.action == "delete_personnel")
                {
                    managementList.Add(new Models.MobilePOS.Management(
                        typeof(PersonnelsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-personnel.jpg", TextsTranslateManager.Translate("Personnel"),
                                              TextsTranslateManager.Translate("View, add, edit, and delete store personnel."))
                    );
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "add_document" || per.action == "edit_document" || per.action == "delete_document")
                {
                    managementList.Add(new Models.MobilePOS.Management(
                        typeof(DocumentsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-documents.jpg", TextsTranslateManager.Translate("Documents"),
                                              TextsTranslateManager.Translate("View, add, and delete store documents."))
                    );
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "edit_schedule")
                {
                    managementList.Add(new Models.MobilePOS.Management(
                        typeof(SchedulesView), "https://pasabuy.app/wp-content/uploads/2020/10/management-schedule.png", TextsTranslateManager.Translate("Schedules"),
                                              TextsTranslateManager.Translate("View, add or update store schedule"))
                    );
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "show_operation")
                {
                    managementList.Add(new Models.MobilePOS.Management(
                        typeof(OperationsView), "https://pasabuy.app/wp-content/uploads/2020/10/management.png", TextsTranslateManager.Translate("Operations"),
                                              TextsTranslateManager.Translate("View list of operations and total sales"))
                    );
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "change_account")
                {
                    managementList.Add(new Models.MobilePOS.Management(
                        typeof(PaymentsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-payments.png", TextsTranslateManager.Translate("Payments"),
                                              TextsTranslateManager.Translate("Manage store wallet and payments"))
                    );
                    break;
                }
            }
            ManagementItem.ItemsSource = managementList;
        }

        async void Management_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (count == 0)
            {
                count = 1;
                if (e.ItemData != null)
                {
                    PasaBuy.App.Models.MobilePOS.Management pageData = (e.ItemData as PasaBuy.App.Models.MobilePOS.Management);
                    Page page = (Page)Activator.CreateInstance(pageData.Type);
                    await Navigation.PushAsync(page);
                    await Task.Delay(100);
                    count = 0;
                }
            }
        }
    }
}