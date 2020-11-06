using System.Collections.ObjectModel;

namespace PasaBuy.App.Models.MobilePOS
{
    public class OrderModel
    {
        private string id { get; set; }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private int qty { get; set; }
        public int TotalQuantity
        {
            get { return qty; }
            set { qty = value; }
        }

        private string remarks { get; set; }
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private ObservableCollection<VariantModel> variants = new ObservableCollection<VariantModel>();
        public ObservableCollection<VariantModel> Variants
        {
            get
            {
                return this.variants;
            }
            set
            {
                this.variants = value;
            }
        }

        private ObservableCollection<VariantModel> payments = new ObservableCollection<VariantModel>();
        public ObservableCollection<VariantModel> Payments
        {
            get
            {
                return this.payments;
            }
            set
            {
                this.payments = value;
            }
        }
    }
}
