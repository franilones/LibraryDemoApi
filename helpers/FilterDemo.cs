using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.helpers
{
    public class FilterDemo : IActionFilter
    {

        private readonly ILogger<FilterDemo> logger;
        public FilterDemo(ILogger<FilterDemo> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogWarning("Action Executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogWarning("Action Executing");
        }
    }
}
