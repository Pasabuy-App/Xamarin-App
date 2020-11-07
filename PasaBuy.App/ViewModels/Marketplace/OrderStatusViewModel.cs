using FFImageLoading.Transformations;
using FFImageLoading.Work;
using System.Collections.Generic;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class OrderStatusViewModel : BaseViewModel
    {
        public bool IsCurrentStatus { get; set; }
        public static double _fee;
        public static double _total;
        public static double _subtotal;

        public double fee;
        public double Fee
        {
            get
            {
                return fee;
            }
            set
            {
                fee = value;
                this.NotifyPropertyChanged();
            }
        }

        public double total;
        public double Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
                this.NotifyPropertyChanged();
            }
        }

        public double subTotal;
        public double SubTotal
        {
            get
            {
                return subTotal;
            }
            set
            {
                subTotal = value;
                this.NotifyPropertyChanged();
            }
        }

        public List<ITransformation> TurnGreyScale { get; set; }

        public OrderStatusViewModel()
        {
            this.Fee = _fee;
            this.Total = _total;
            this.SubTotal = _subtotal;
            TurnGreyScale = new List<ITransformation>()
            {
                new CustomTransformationSelector(),
            };
        }


        public class CustomTransformationSelector : ITransformation
        {
            readonly ITransformation PlaceholderTransformation = new CircleTransformation(5d, "#EEEEEE");
            readonly ITransformation ImageTransformation = new GrayscaleTransformation();

            public string Key
            {
                get
                {
                    return "CustomTransformationSelector";
                }
            }

            public IBitmap Transform(IBitmap sourceBitmap, string path, ImageSource source, bool isPlaceholder, string key)
            {
                if (isPlaceholder)
                {
                    return PlaceholderTransformation.Transform(sourceBitmap, path, source, isPlaceholder, key);
                }

                return ImageTransformation.Transform(sourceBitmap, path, source, isPlaceholder, key);
            }
        }
    }
}
