using MarketPlace.App.Utils;
using MarketPlace.Data.Entities.Site;

namespace MarketPlace.App.EntitiExtensions
{
    public static class SliderExtensions
    {
        public static string GetSliderImageAddress(this Slider slider)
        {
            return PathExtension.SliderOrigin + slider.ImageName;
        }
    }
}
