﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Data.DTO.Products
{
    public class CreateOrEditProductGalleryDTO
    {
        [Display(Name = "اولویت نمایش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int DisplayPriority { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
    }

    public enum CreateOrEditProductGalleryResult
    {
        Success,
        NotForUserProduct,
        ImageIsNull,
        ProductNotFound
    }
}
