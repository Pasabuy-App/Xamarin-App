
namespace PasaBuy.App.Models.Enums
{
    public class CurrencyTypes
    {
        private CurrencyTypes(string value) { Value = value; }

        public string Value { get; set; }

        public static CurrencyTypes Savings
        {
            get
            {
                return new CurrencyTypes("SVS");
            }
        }

        public static CurrencyTypes Credits
        {
            get
            {
                return new CurrencyTypes("CDT");
            }
        }
    }
}
