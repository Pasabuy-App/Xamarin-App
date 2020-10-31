using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RolesView : ContentPage
    {
        public int count = 0;
        public RolesView()
        {
            InitializeComponent();
            this.BindingContext = new RolesViewModel();
        }

        /*private async void Update_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditRole());
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (count == 0)
                {
                    count = 1;
                    bool answer = await DisplayAlert("Delete Role", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var btn = sender as Xamarin.Forms.Grid;

                        Http.POSFeature.Instance.Role_Delete(btn.ClassId, (bool success, string data) =>
                        {
                            if (success)
                            {
                                RolesViewModel._rolesList.Clear();
                                RolesViewModel.LoadRoleList();
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                    await Task.Delay(200);
                    count = 0;
                }
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }*/

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupAddRole());
        }
    }
}