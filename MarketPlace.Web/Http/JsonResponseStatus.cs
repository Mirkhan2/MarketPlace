using System;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Http
{
    public static class JsonResponseStatus
    {
        //public static JsonResult Success(JsonResponsStatusType type,string message) 
        //{
        //    return new JsonResult(new { status = "success" ,message = message});
        //}
        public static JsonResult SendStatus(JsonResponsStatusType type,string message , object data)
        {
            return new JsonResult(new { status = type.ToString(),message = message, data = data });
        }

        internal static IActionResult SendStatus(string v)
        {
            throw new NotImplementedException();
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
