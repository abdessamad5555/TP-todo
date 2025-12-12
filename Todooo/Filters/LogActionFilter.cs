using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Todooo.Filters
{
 
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LogActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ActionFilterLog");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string controllerName = context.Controller.GetType().Name;
            string actionName = context.ActionDescriptor.DisplayName;
            string status = context.Exception == null ? "Success" : "Failure";
            _logger.LogInformation($"[Action Executed] Controller: {controllerName}, Action: {actionName}, Status: {status}");

            if (context.Exception != null)
            {
                _logger.LogError(context.Exception, $"[Action Failed] Controller: {controllerName}, Action: {actionName}");
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string controllerName = context.Controller.GetType().Name;
            string actionName = context.ActionDescriptor.DisplayName;

            _logger.LogInformation($"[Action Executing] Controller: {controllerName}, Action: {actionName}");
        }
    }
}