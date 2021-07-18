using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Infrastructure.Filters
{
    public class ActionGlobalLogFilter: ActionFilterAttribute
    {
        ILogger<ActionGlobalLogFilter> _logger;
        public ActionGlobalLogFilter(ILogger<ActionGlobalLogFilter> logger)
        {
            _logger = logger;
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("START: Action Method: " + context.ActionDescriptor.DisplayName +
                                   ", Controller: "+ context.Controller +
                                   ", User: " + context.HttpContext.User +
                                   ", Time: " + DateTime.Now.ToString());
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("END:  Action Method: " + context.ActionDescriptor.DisplayName +
                                   ", Controller: " + context.Controller +
                                   ", User: " + context.HttpContext.User +
                                   ", Time: " + DateTime.Now.ToString());
        }
    }
}
