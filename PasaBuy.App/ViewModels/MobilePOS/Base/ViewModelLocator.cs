using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS.Base
{
    public static class ViewModelLocator
    {
        private static TinyIoCContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();
            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            //_container.Register<MainViewModel>();
            //_container.Register<StockInViewModel>();
            //_container.Register<InventoryViewModel>();
            //_container.Register<LoginViewModel>();
            //_container.Register<SaleOrderViewModel>();
            //_container.Register<PurchaseOrderViewModel>();
            //_container.Register<RemindViewModel>();
            //_container.Register<ReportsViewModel>();
            //_container.Register<ReportSummaryViewModel>();
            //_container.Register<SaleReceiptSummaryViewModel>();
            //_container.Register<SaleReceiptDetailViewModel>();
            //_container.Register<PurchaseReceiptSummaryViewModel>();
            //_container.Register<PurchaseReceiptDetailViewModel>();
            //_container.Register<SettingViewModel>();
            //_container.Register<AboutMiniPOSViewModel>();
            //_container.Register<HelpViewModel>();
            //_container.Register<ManagementViewModel>();
            //_container.Register<ManageDatabaseViewModel>();
            //_container.Register<TermsOfServiceViewModel>();
            //_container.Register<PrivacyPoliciesViewModel>();
            //_container.Register<CustomersViewModel>();
            //_container.Register<AddCustomerViewModel>();
            //_container.Register<SuppliersViewModel>();
            //_container.Register<AddSupplierViewModel>();
            //_container.Register<CategoryViewModel>();
            //_container.Register<UnitViewModel>();
            _container.Register<ProductsViewModel>();
            //_container.Register<PopupUpdateItemInSaleOrderViewModel>();
            //_container.Register<PopupUpdateItemInUnitConverterViewModel>();
            _container.Register<AddProductViewModel>();
            //_container.Register<PopupDiscountViewModel>();
            //_container.Register<PopupScannerViewModel>();
            //_container.Register<PopupSelectDateRangeViewModel>();
            //_container.Register<StoreSettingViewModel>();
            _container.Register<NavigationViewModel>();
            //_container.Register<PopupUpdateItemInPurchaseOrderViewModel>();
            //_container.Register<AppSettingsViewModel>();
            //_container.Register<ReceiptPrinterViewModel>();
            //_container.Register<MasterViewModel>();
            //_container.Register<PopupSelectDateViewModel>();
            //_container.Register<DailySaleReportViewModel>();
            //_container.Register<PopupAddSaleItemViewModel>();
            //_container.Register<PopupAddStockItemViewModel>();
            //DbContext
            //_container.Register<DbContext>();

            //Repositories
            //_container.Register<IProductRepository, ProductRepository>();
            //_container.Register<ICustomerRepository, CustomerRepository>();
            //_container.Register<ICategoryRepository, CategoryRepository>();
            //_container.Register<ISupplierRepository, SupplierRepository>();
            //_container.Register<IPurchaseOrderRepository, PurchaseOrderRepository>();
            //_container.Register<IPurchaseOrderDetailRepository, PurchaseOrderDetailRepository>();
            //_container.Register<IOrderRepository, OrderRepository>();
            //_container.Register<IOrderDetailRepository, OrderDetailRepository>();
            //_container.Register<IUnitRepository, UnitRepository>();
            //_container.Register<ICategoryRepository, CategoryRepository>();
            //_container.Register<IStockBalanceRepository, StockBalanceRepository>();
            //_container.Register<ISettingRepository, SettingRepository>();
            // Services - by default, TinyIoC will register interface registrations as singletons.

            //_container.Register<IDialogService, DialogService>();

            //_container.Register<IProductService, ProductService>();
            //_container.Register<ISettingsService, SettingsService>();
            //_container.Register<INavigationService, NavigationService>();
            //_container.Register<ICustomerService, CustomerService>();
            //_container.Register<ICategoryService, CategoryService>();
            //_container.Register<ISupplierService, SupplierService>();
            //_container.Register<IOrderService, OrderService>();
            //_container.Register<IPurchaseOrderService, PurchaseOrderService>();
            //_container.Register<IOrderDetailService, OrderDetailService>();
            //_container.Register<IPurchaseOrderDetailService, PurchaseOrderDetailService>();
            //_container.Register<IUnitService, UnitService>();
            //_container.Register<ICategoryService, CategoryService>();
            //_container.Register<IRequestService, RequestService>();
            //_container.Register<IAuthenticationService, AuthenticationService>();
            //_container.Register<IMiniPosService, MiniPosService>();
            //_container.Register<SyncDataManager>();
        }

        public static void UpdateDependencies(bool useMockServices)
        {
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
