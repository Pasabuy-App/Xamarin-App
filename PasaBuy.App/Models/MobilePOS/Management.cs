using PasaBuy.App.Resources.Texts;
using PasaBuy.App.Views.StoreViews;
using PasaBuy.App.Views.StoreViews.Management;
using System;
using System.Collections.Generic;

namespace PasaBuy.App.Models.MobilePOS
{
    public class Management
    {
        public Management(Type type, string icon, string title, string description)
        {
            Type = type;
            Icon = icon;
            Title = title;
            Description = description;
        }

        public Type Type { private set; get; }

        public string Icon { private set; get; }

        public string Title { private set; get; }

        public string Description { private set; get; }

        static Management()
        {
            All = new List<Management>
            {

                new Management(typeof(ProductsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-product.png", TextsTranslateManager.Translate("ProductsTitle"),
                                      TextsTranslateManager.Translate("ProductManagementDescriptions")),

                 new Management(typeof(CategoryView), "https://pasabuy.app/wp-content/uploads/2020/10/management-category.png", TextsTranslateManager.Translate("CategoriesTitle"),
                                      TextsTranslateManager.Translate("CategoryManagementDescriptions")),

                 new Management(typeof(VariantsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-variants.png", TextsTranslateManager.Translate("VariantsTitle"),
                                      TextsTranslateManager.Translate("VariantsManagementDescriptions")),

                 new Management(typeof(PersonnelsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-personnel.jpg", TextsTranslateManager.Translate("Personnel"),
                                      TextsTranslateManager.Translate("PersonnelManagementDescriptions")),

                 new Management(typeof(OperationsView), "https://pasabuy.app/wp-content/uploads/2020/10/management.png", TextsTranslateManager.Translate("Operations"),
                                      TextsTranslateManager.Translate("View list of operations and total sales")),

                 new Management(typeof(SchedulesView), "https://pasabuy.app/wp-content/uploads/2020/10/management-schedule.png", TextsTranslateManager.Translate("Schedules"),
                                      TextsTranslateManager.Translate("Add or update store schedule")),

                 new Management(typeof(DocumentsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-documents.jpg", TextsTranslateManager.Translate("DocumentsTitle"),
                                      TextsTranslateManager.Translate("DocumentsManagementDescriptions")),

                 new Management(typeof(RolesView), "https://pasabuy.app/wp-content/uploads/2020/10/management.png", TextsTranslateManager.Translate("Personnel Roles"),
                                      TextsTranslateManager.Translate("Manage access roles of your personnel")),

                 new Management(typeof(PaymentsView), "https://pasabuy.app/wp-content/uploads/2020/10/management-payments.png", TextsTranslateManager.Translate("Payments"),
                                      TextsTranslateManager.Translate("Manage store wallet and payments")),

                //new Management(typeof(VouchersView), "Idcard.png", TextsTranslateManager.Translate("VouchersTitle"),
                //                      TextsTranslateManager.Translate("VouchersManagementDescriptions")),

            };
        }

        public static IList<Management> All { private set; get; }
    }
}
