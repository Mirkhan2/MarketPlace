using System;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Http
{
    public static class JsonResponseStatus
    {
        public static JsonResult SendStatus(JsonResponsStatusType type,string message , object data)
        {
            return new JsonResult(new { status = type.ToString(),message = message, data = data });
        }


    }

    public enum JsonResponsStatusType
    {

        Success,
        Warning,
        Danger,
        Info
    }
}
