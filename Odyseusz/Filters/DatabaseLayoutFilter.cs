using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class DatabaseLayoutFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Path.StartsWithSegments("/Database"))
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                controller.ViewData["Layout"] = "~/Views/Shared/_DatabaseLayout.cshtml";
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
