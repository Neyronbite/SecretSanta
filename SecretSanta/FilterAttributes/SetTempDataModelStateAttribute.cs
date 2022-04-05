using System.Web.Mvc;

public class SetTempDataModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        base.OnActionExecuted(filterContext);
        filterContext.Controller.TempData["ModelState"] =
           filterContext.Controller.ViewData.ModelState;
    }
}

