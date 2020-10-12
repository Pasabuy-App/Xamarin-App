using PasaBuy.App.Resources.Texts;
using PasaBuy.App.Views.StoreViews;
using PasaBuy.App.Views.StoreViews.Management;
using System;
using System.Collections.Generic;
using System.Text;

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

                new Management(typeof(ProductsView), "Idcard.png", TextsTranslateManager.Translate("ProductsTitle"),
                                      TextsTranslateManager.Translate("ProductManagementDescriptions")),

                 new Management(typeof(CategoryView), "Idcard.png", TextsTranslateManager.Translate("CategoriesTitle"),
                                      TextsTranslateManager.Translate("CategoryManagementDescriptions")),

                 //new Management(typeof(VariantsView), "Idcard.png", TextsTranslateManager.Translate("VariantsTitle"),
                 //                     TextsTranslateManager.Translate("VariantsManagementDescriptions")),

                 new Management(typeof(PersonnelsView), "Idcard.png", TextsTranslateManager.Translate("Personnel"),
                                      TextsTranslateManager.Translate("PersonnelManagementDescriptions")),

                 new Management(typeof(OperationsView), "Idcard.png", TextsTranslateManager.Translate("Operations"),
                                      TextsTranslateManager.Translate("View list of operations and total sales")),

                 new Management(typeof(SchedulesView), "Idcard.png", TextsTranslateManager.Translate("Schedules"),
                                      TextsTranslateManager.Translate("Add or update store schedule")),

                 new Management(typeof(DocumentsView), "Idcard.png", TextsTranslateManager.Translate("DocumentsTitle"),
                                      TextsTranslateManager.Translate("DocumentsManagementDescriptions")),

                //new Management(typeof(VouchersView), "Idcard.png", TextsTranslateManager.Translate("VouchersTitle"),
                //                      TextsTranslateManager.Translate("VouchersManagementDescriptions")),

            };
        }

        public static IList<Management> All { private set; get; }
    }
}
