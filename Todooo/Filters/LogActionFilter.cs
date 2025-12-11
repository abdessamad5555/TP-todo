using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Todooo.Filters
{
    // IActionFilter permet d'exécuter du code avant et après la méthode d'action.
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        // Injection de ILoggerFactory pour créer un logger avec une catégorie spécifique.
        public LogActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ActionFilterLog");
        }

        // Exécuté APRES l'exécution de la méthode d'action
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string controllerName = context.Controller.GetType().Name;
            string actionName = context.ActionDescriptor.DisplayName;
            string status = context.Exception == null ? "Success" : "Failure";

            // Log l'état de l'exécution
            _logger.LogInformation($"[Action Executed] Controller: {controllerName}, Action: {actionName}, Status: {status}");

            if (context.Exception != null)
            {
                // Log l'exception s'il y en a une
                _logger.LogError(context.Exception, $"[Action Failed] Controller: {controllerName}, Action: {actionName}");
            }
        }

        // Exécuté AVANT l'exécution de la méthode d'action
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string controllerName = context.Controller.GetType().Name;
            string actionName = context.ActionDescriptor.DisplayName;

            // Log le début de l'exécution
            _logger.LogInformation($"[Action Executing] Controller: {controllerName}, Action: {actionName}");
        }
    }
}