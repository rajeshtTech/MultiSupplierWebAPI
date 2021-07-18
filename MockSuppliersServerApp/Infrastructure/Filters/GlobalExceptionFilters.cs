using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Infrastructure.Filters
{
    public class GlobalExceptionFilters : ExceptionFilterAttribute
    {
        ILogger<GlobalExceptionFilters> _logger;
        public GlobalExceptionFilters(ILogger<GlobalExceptionFilters> logger)
        {
            _logger = logger;
        }
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            await Task.CompletedTask;
        }
    }
}
