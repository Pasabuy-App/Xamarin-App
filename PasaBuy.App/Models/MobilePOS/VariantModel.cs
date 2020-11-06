namespace PasaBuy.App.Models.MobilePOS
{
    public class VariantModel
    {
        private string id { get; set; }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        private string method { get; set; }
        public string Method
        {
            get { return method; }
            set { method = value; }
        }
        private string values { get; set; }
        public string Value
        {
            get { return values; }
            set { values = value; }
        }
    }
}
