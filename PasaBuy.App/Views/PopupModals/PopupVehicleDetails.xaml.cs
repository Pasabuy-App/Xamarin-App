using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Driver;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupVehicleDetails : PopupPage
    {
        public static string type_id;
        public static string maker_id;
        public static string model_id;
        public bool isClicked;
        public PopupVehicleDetails()
        {
            InitializeComponent();
            //this.BindingContext = new VehicleListViewModel();
            TypeListViewModel typelist = new TypeListViewModel();
            combo_Type.BindingContext = typelist;
            combo_Type.DataSource = typelist.TypeList;
            combo_Type.DisplayMemberPath = "Title";
            isClicked = false;
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void combo_Type_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(combo_Maker.Text))
            {
                combo_Maker.Clear();
            }
            if (!string.IsNullOrWhiteSpace(combo_Model.Text))
            {
                combo_Model.Clear();
                ModelListViewModel.ClearList();
            }
            type_id = combo_Type.SelectedValue.ToString();
            MakerListViewModel makerlist = new MakerListViewModel(combo_Type.SelectedValue.ToString());
            combo_Maker.BindingContext = makerlist;
            combo_Maker.DataSource = makerlist.MakerList;
            combo_Maker.DisplayMemberPath = "Title";
        }

        private void combo_Maker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(combo_Model.Text))
            {
                combo_Model.Clear();
            }
            maker_id = combo_Maker.SelectedValue.ToString();
            ModelListViewModel modellsit = new ModelListViewModel(combo_Maker.SelectedValue.ToString());
            combo_Model.BindingContext = modellsit;
            combo_Model.DataSource = modellsit.ModelList;
            combo_Model.DisplayMemberPath = "Title";
        }

        private void combo_Model_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            model_id = combo_Model.SelectedValue.ToString();
        }

        private void SubmitModal(object sender, EventArgs e)
        {
            try
            {
                if (!isClicked)
                {
                    isClicked = true;
                    if (!string.IsNullOrEmpty(type_id) && !string.IsNullOrEmpty(maker_id) && !string.IsNullOrEmpty(model_id))
                    {
                        Http.HatidPress.Vehicle.Instance.Vehicle_Insert(type_id, PlateNumber.Text, maker_id, model_id, async (bool success, string data) =>
                        {
                            if (success)
                            {
                                VehicleListViewModel.RefreshVehicle();
                                await PopupNavigation.Instance.PopAsync();
                                await Clipboard.SetTextAsync("Vehicle successfully added");
                                if (Clipboard.HasText)
                                {
                                    var text = await Clipboard.GetTextAsync();
                                    Plugin.Toast.CrossToastPopUp.Current.ShowToastMessage(text);
                                }
                                isClicked = false;
                                
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                isClicked = false;
                            }
                        });
                    }
                    else
                    {
                        new Alert("Notice to User", "Please select all.", "Try Again");
                        isClicked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
                isClicked = false;
            }
        }
    }
}