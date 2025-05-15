namespace QuickMock.Middlewares;

public class LayoutDataMiddleware
{
    private readonly RequestDelegate _next;

    public LayoutDataMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        context.Items["LayoutData"] = new
        {
            HostValue = context.Request.Host.Value,
        };
        
        await _next(context);
    }
}