using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NPE.Core.Common.Responses;

namespace NPE.API.Filters
{
    public class ApiResponseFilter
        : IActionFilter
    {
        public void OnActionExecuted(
            ActionExecutedContext context)
        {
            if (context.Result is not ObjectResult objectResult)
                return;

            // Skip already wrapped responses
            if (objectResult.Value?.GetType().Name
                .StartsWith("ApiResponse") == true)
            {
                return;
            }

            // Skip non-success status codes
            if (objectResult.StatusCode >= 400)
                return;

            var wrapped =
                new ApiResponse<object>
                {
                    Success = true,
                    Data = objectResult.Value
                };

            context.Result =
                new ObjectResult(wrapped)
                {
                    StatusCode =
                        objectResult.StatusCode
                };
        }

        public void OnActionExecuting(
            ActionExecutingContext context)
        {
        }
    }
}