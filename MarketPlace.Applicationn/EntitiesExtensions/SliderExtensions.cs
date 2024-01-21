using MarketPlace.Applicationn.Utils;
using MarketPlace.DataLayerr.Entities.Site;

namespace MarketPlace.Applicationn.EntitiesExtensions
{
    public static class SliderExtensions
    {
        public static string GetSliderImageAddress(this Slider slider)
        {
            return PathExtension.SliderOrigin + slider.ImageName;
        }
    }
}
