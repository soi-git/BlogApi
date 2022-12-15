using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            string? actionName = context.ActionDescriptor.DisplayName;
            string? exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;
            _logger.LogError(exceptionMessage, DateTime.UtcNow.ToLongDateString());
            context.Result = new ContentResult { Content = $"В методе {actionName} возникло исключение: \n {exceptionMessage} \n {exceptionStack}" };
            context.ExceptionHandled = true;
        }
    }
}
