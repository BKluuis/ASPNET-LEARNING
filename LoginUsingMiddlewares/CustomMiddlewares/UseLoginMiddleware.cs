//=========UseLoginMiddleware.cs=========//

namespace LoginUsingMiddlewares.CustomMiddlewares
{
    public class UseLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public UseLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == "GET" && httpContext.Request.Path == "/")
            {
                Dictionary<string, string> login = new Dictionary<string, string>()
            {
                {"email", "admin@domain.com"},
                {"password", "admin123"}
            };
                string errors = string.Empty;
                bool isValid = true;

                if (!httpContext.Request.Query.ContainsKey("email") || string.IsNullOrEmpty(httpContext.Request.Query["email"][0]))
                {
                    isValid = false;
                    errors = errors.Insert(errors.Length, "Invalid input for 'email'\n");
                }
                if (!httpContext.Request.Query.ContainsKey("password") || string.IsNullOrEmpty(httpContext.Request.Query["password"][0]))
                {
                    isValid = false;
                    errors = errors.Insert(errors.Length, "Invalid input for 'password'\n");
                }
                if (isValid == false)
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync(errors);
                    return;
                }
                if (!(httpContext.Request.Query["password"] == login["password"] && httpContext.Request.Query["email"] == login["email"]))
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid login");
                    return;
                }

                await _next(httpContext);

                httpContext.Response.StatusCode = 200;
                await httpContext.Response.WriteAsync("Successful login");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UseLoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogin(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseLoginMiddleware>();
        }
    }
}
