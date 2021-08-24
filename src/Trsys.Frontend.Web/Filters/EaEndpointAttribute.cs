﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Trsys.Frontend.Web.Filters
{
    public class EaEndpointAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var key = (string)context.HttpContext.Request.Headers["X-Ea-Id"];
            var type = (string)context.HttpContext.Request.Headers["X-Ea-Type"];
            var version = (string)context.HttpContext.Request.Headers["X-Ea-Version"];
            if (string.IsNullOrEmpty(key))
            {
                context.Result = new BadRequestObjectResult("X-Ea-Id is not set.");
                return;
            }
            if (string.IsNullOrEmpty(type))
            {
                context.Result = new BadRequestObjectResult("X-Ea-Type is not set.");
                return;
            }
            if (string.IsNullOrEmpty(version))
            {
                context.Result = new BadRequestObjectResult("X-Ea-Version is not set.");
                return;
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
