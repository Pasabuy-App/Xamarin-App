using PasaBuy.App.Resources.Texts;
using PasaBuy.App.Views.StoreViews;
using System;
using System.Collections.Generic;

namespace PasaBuy.App.Models.Driver
{
    public class DriverTransactions
    {


        #region Feilds

        public Type Type { private set; get; }

        public string Icon { private set; get; }

        public string Title { private set; get; }

        public string Description { private set; get; }


        #endregion

        public DriverTransactions(Type type, string icon, string title, string description)
        {
            Type = type;
            Icon = icon;
            Title = title;
            Description = description;
        }


        static DriverTransactions()
        {
            All = new List<DriverTransactions>
                    {

                        new DriverTransactions(typeof(ProductsView), "Idcard.png", TextsTranslateManager.Translate("ProductsTitle"),
                                              TextsTranslateManager.Translate("ProductManagementDescriptions")),

                         new DriverTransactions(typeof(CategoryView), "Idcard.png", TextsTranslateManager.Translate("CategoriesTitle"),
                                              TextsTranslateManager.Translate("CategoryManagementDescriptions")),

                         //new Management(typeof(VariantsView), "Idcard.png", TextsTranslateManager.Translate("VariantsTitle"),
                         //                     TextsTranslateManager.Translate("VariantsManagementDescriptions")),

                         new DriverTransactions(typeof(DocumentsView), "Idcard.png", TextsTranslateManager.Translate("DocumentsTitle"),
                                              TextsTranslateManager.Translate("DocumentsManagementDescriptions")),

                        //new Management(typeof(VouchersView), "Idcard.png", TextsTranslateManager.Translate("VouchersTitle"),
                        //                      TextsTranslateManager.Translate("VouchersManagementDescriptions")),

                    };
        }

        public static IList<DriverTransactions> All { private set; get; }
    }
}
