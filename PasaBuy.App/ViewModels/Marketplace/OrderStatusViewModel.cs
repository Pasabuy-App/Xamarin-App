using FFImageLoading.Transformations;
using FFImageLoading.Work;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class OrderStatusViewModel : BaseViewModel
    {
        public bool IsCurrentStatus { get; set; }

        public List<ITransformation> TurnGreyScale { get; set; }

        public OrderStatusViewModel()
        {
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
