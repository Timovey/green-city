using GreenCity.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GreenCity.Http.Attributes
{
    public class ModelStateValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;

            if (!modelState.IsValid)
                context.Result = new CommonActionResult(modelState.GetBadRequestWithErrors());

            base.OnActionExecuting(context);
        }
    }
}
